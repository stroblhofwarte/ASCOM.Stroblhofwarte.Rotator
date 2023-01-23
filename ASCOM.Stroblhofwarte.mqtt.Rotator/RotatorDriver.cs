//tabs=4
// --------------------------------------------------------------------------------
//
// This file is part of the Stroblhofwarte.Rotator project 
// (https://github.com/stroblhofwarte/ASCOM.Stroblhofwarte.Rotator).
// Copyright (c) 2021, Othmar Ehrhardt, https://astro.stroblhof-oberrohrbach.de
//
// This program is free software: you can redistribute it and/or modify  
// it under the terms of the GNU General Public License as published by  
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License 
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
// ASCOM Rotator mqtt driver.
// Precondition for a working setup:
//    - A running Stroblhofwarte.Rotator.MqttGateway (short: Gateway)
//    - A mqtt broker, without SSL and authentification
//
// Description:	The Stroblhof Rotator is a simple and sturdy rotator device
//              for astronomical observations. It could be build without access to
//              a lathe or milling machine. The rotator device is made out of aluminium
//              and steel. It is driven by a NEMA 14 stepper motor. The rotator could 
//              rotate (appromimately) between between 0° and 330°. 
//              The electronics are made out of a arduino uno and a shield with ST820 driver.
//              Each stepper driver can be used, the DRV8825 driver supports 32 microsteps for
//              smooth operation.
//              The communication between the hardware and the ASCOM driver is done via MQTT.
//              This enables the hardware to support more than one device type. It is possible 
//              to support also a focuser and a filter wheel. Each device can be used from
//              a different ASCOM client.
//              The mqtt communication to the rotator device:
//             
//              Topic: 
//
//              Stroblhofwarte/Rotator/Position: Current Position, published by Gateway
//              Stroblhofwarte/Rotator/Move: Move to (absolute position) subscribed by Gateway
//              Stroblhofwarte/Rotator/Halt: Halt movement, subscribed by Gateway
//              Stroblhofwarte/Rotator/Sync: Sync to position, subscribed by Gateway
//              Stroblhofwarte/Rotator/MaxMovement: Max. movement of Rotator, published by Gateway
//              Stroblhofwarte/Rotator/State: "1": rotator move, "0": rotator not moving, published by Gateway
//
// Implements:	ASCOM Rotator interface version: 3.0
// Author:		Othmar Ehrhardt, <othmar.ehrhardt@t-online.de>, https://astro.stroblhof-oberrohrbach.de
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 23.01.2023               Prototype setup is working
// --------------------------------------------------------------------------------
//

#define Rotator

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ASCOM.Stroblhofwarte.mqtt
{
  
    /// <summary>
    /// ASCOM Rotator Driver for Stroblhofwarte.mqtt.
    /// </summary>
    [Guid("ad3f1b6b-05a9-41a4-9a5f-b55e3ecce861")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Rotator : IRotatorV3
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.Stroblhofwarte.mqtt.Rotator";
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Rotator Driver for Stroblhofwarte.mqtt.";

        internal static string mqttHostName = "MqttHost";
        internal static string mqttHostDefault = "192.168.0.1";
        internal static string mqttPortName = "MqttPort";
        internal static string mqttPortDefault = "1883";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        private readonly static string MQTT_ROTATOR_PREFIX = "Stroblhofwarte/Rotator/";
        private readonly string MQTT_ROTATOR_POSITION = MQTT_ROTATOR_PREFIX + "Position";
        private readonly string MQTT_ROTATOR_MOVE_TO = MQTT_ROTATOR_PREFIX + "Move";
        private readonly string MQTT_ROTATOR_HALT = MQTT_ROTATOR_PREFIX + "Halt";
        private readonly string MQTT_ROTATOR_SYNC = MQTT_ROTATOR_PREFIX + "Sync";
        private readonly string MQTT_ROTATOR_MAX_MOVEMENT = MQTT_ROTATOR_PREFIX + "MaxMovement";
        private readonly string MQTT_ROTATOR_STATE = MQTT_ROTATOR_PREFIX + "State";
        private readonly string MQTT_ROTATOR_STEPSIZE = MQTT_ROTATOR_PREFIX + "Stepsize";

        internal static string mqttHost; // Variables to hold the current device configuration
        internal static int mqttPort;
        private uPLibrary.Networking.M2Mqtt.MqttClient _mqtt = null;

        private bool _isMoving;
        private float _position;
        private float _maxMovement;
        private float _stepSize = -1.0f;
        private bool _reverse = false;
        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stroblhofwarte.mqtt"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Rotator()
        {
            tl = new TraceLogger("", "Stroblhofwarte.mqtt");
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl.LogMessage("Rotator", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro-utilities object
            //TODO: Implement your additional construction here

            tl.LogMessage("Rotator", "Completed initialisation");
        }


        //
        // PUBLIC COM INTERFACE IRotatorV3 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(tl))
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // TODO The optional CommandBlind method should either be implemented OR throw a MethodNotImplementedException
            // If implemented, CommandBlind must send the supplied command to the mount and return immediately without waiting for a response

            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            // TODO The optional CommandBool method should either be implemented OR throw a MethodNotImplementedException
            // If implemented, CommandBool must send the supplied command to the mount, wait for a response and parse this to return a True or False value

            // string retString = CommandString(command, raw); // Send the command and wait for the response
            // bool retBool = XXXXXXXXXXXXX; // Parse the returned string and create a boolean True / False value
            // return retBool; // Return the boolean value to the client

            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // TODO The optional CommandString method should either be implemented OR throw a MethodNotImplementedException
            // If implemented, CommandString must send the supplied command to the mount and wait for a response before returning this to the client

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the trace logger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", IsConnected);
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                if (value)
                {
                    _mqtt = new uPLibrary.Networking.M2Mqtt.MqttClient(mqttHost, mqttPort, false, null, null, uPLibrary.Networking.M2Mqtt.MqttSslProtocols.None);
                    _mqtt.Subscribe(new string[] { MQTT_ROTATOR_POSITION }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.Subscribe(new string[] { MQTT_ROTATOR_STATE }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.Subscribe(new string[] { MQTT_ROTATOR_MAX_MOVEMENT }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.Subscribe(new string[] { MQTT_ROTATOR_STEPSIZE }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.MqttMsgPublishReceived += _mqtt_MqttMsgPublishReceived;
                    _mqtt.Connect("Stroblhofwarte.mqtt.Rotator");
                    connectedState = true;
                   
                }
                else
                {
                    connectedState = false;
                    //LogMessage("Connected Set", "Disconnecting from port {0}", comPort);
                    // TODO disconnect from the device
                }
            }
        }

        private void _mqtt_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (IsConnected == false) return;
            string msg = Encoding.ASCII.GetString(e.Message);
            if (e.Topic == MQTT_ROTATOR_POSITION)
            {
                try
                {
                    float pos = (float)Convert.ToDouble(msg, CultureInfo.InvariantCulture);
                    _position = pos;
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if (e.Topic == MQTT_ROTATOR_STATE)
            {
                _isMoving = false;
                if (msg == "1")
                {
                    _isMoving = true;
                }
            }
            if (e.Topic == MQTT_ROTATOR_MAX_MOVEMENT)
            {
                try
                {
                    float pos = (float)Convert.ToDouble(msg, CultureInfo.InvariantCulture);
                    _maxMovement = pos;
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if(e.Topic == MQTT_ROTATOR_STEPSIZE)
            {
                try
                {
                    float pos = (float)Convert.ToDouble(msg, CultureInfo.InvariantCulture);
                    _stepSize = pos;
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
        }

        public string Description
        {
            // TODO customise this device description
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver description
                string driverInfo = "Stroblhofwarte.mqtt.Rotator. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "3");
                return Convert.ToInt16("3");
            }
        }

        public string Name
        {
            get
            {
                string name = "mqtt.Rotator";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region IRotator Implementation

        private float rotatorPosition = 0; // Synced or mechanical position angle of the rotator
        private float mechanicalPosition = 0; // Mechanical position angle of the rotator

        public bool CanReverse
        {
            get
            {
                tl.LogMessage("CanReverse Get", true.ToString());
                return true;
            }
        }

        public void Halt()
        {
            if(IsConnected)
            {
                _mqtt.Publish(MQTT_ROTATOR_HALT, Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
        }

        public bool IsMoving
        {
            get
            {
                return _isMoving;
            }
        }

        public void Move(float Position)
        {
            tl.LogMessage("Move", Position.ToString()); // Move by this amount
            rotatorPosition += Position;
            rotatorPosition = (float)astroUtilities.Range(rotatorPosition, 0.0, true, 360.0, false); // Ensure value is in the range 0.0..359.9999...
            if(IsConnected)
            {
                _mqtt.Publish(MQTT_ROTATOR_MOVE_TO, Encoding.UTF8.GetBytes(rotatorPosition.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        public void MoveAbsolute(float Position)
        {
            tl.LogMessage("MoveAbsolute", Position.ToString()); // Move to this position
            rotatorPosition = Position;
            rotatorPosition = (float)astroUtilities.Range(rotatorPosition, 0.0, true, 360.0, false); // Ensure value is in the range 0.0..359.9999...
            if (IsConnected)
            {
                _mqtt.Publish(MQTT_ROTATOR_MOVE_TO, Encoding.UTF8.GetBytes(rotatorPosition.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        public float Position
        {
            get
            {
                tl.LogMessage("Position Get", rotatorPosition.ToString()); // This rotator has instantaneous movement
                rotatorPosition = _position;
                return rotatorPosition;
            }
        }

        public bool Reverse
        {
            get
            {
                return _reverse;
            }
            set
            {
                _reverse = value;
            }
        }

        public float StepSize
        {
            get
            {
                tl.LogMessage("StepSize Get", _stepSize.ToString());
                return _stepSize;
            }
        }

        public float TargetPosition
        {
            get
            {
                rotatorPosition = _position;
                tl.LogMessage("TargetPosition Get", rotatorPosition.ToString()); // This rotator has instantaneous movement
                return rotatorPosition;
            }
        }

        // IRotatorV3 methods

        public float MechanicalPosition
        {
            get
            {
                mechanicalPosition = _position;
                tl.LogMessage("MechanicalPosition Get", mechanicalPosition.ToString());
                return mechanicalPosition;
            }
        }

        public void MoveMechanical(float Position)
        {
            tl.LogMessage("MoveMechanical", Position.ToString()); // Move to this position

            // TODO: Implement correct sync behaviour. i.e. if the rotator has been synced the mechanical and rotator positions won't be the same
            rotatorPosition = (float)astroUtilities.Range(Position, 0.0, true, 360.0, false); // Ensure value is in the range 0.0..359.9999...
            tl.LogMessage("MoveAbsolute", Position.ToString()); // Move to this position
            if (IsConnected)
            {
                _mqtt.Publish(MQTT_ROTATOR_MOVE_TO, Encoding.UTF8.GetBytes(rotatorPosition.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        public void Sync(float Position)
        {
            tl.LogMessage("Sync", Position.ToString()); // Sync to this position

            // TODO: Implement correct sync behaviour. i.e. the rotator mechanical and rotator positions may not be the same
            rotatorPosition = (float)astroUtilities.Range(Position, 0.0, true, 360.0, false); // Ensure value is in the range 0.0..359.9999...
            if (IsConnected)
            {
                _mqtt.Publish(MQTT_ROTATOR_SYNC, Encoding.UTF8.GetBytes(rotatorPosition.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Rotator";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                mqttHost = driverProfile.GetValue(driverID, mqttHostName, string.Empty, mqttHostDefault);
                mqttPort = Convert.ToInt32(driverProfile.GetValue(driverID, mqttPortName, string.Empty, mqttPortDefault));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(driverID, mqttHostName, mqttHost.ToString());
                driverProfile.WriteValue(driverID, mqttPortName, mqttPort.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }
        #endregion
    }
}
