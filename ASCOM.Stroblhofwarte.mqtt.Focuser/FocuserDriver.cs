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
// ASCOM Focuser mqtt driver.
// Precondition for a working setup:
//    - A running Stroblhofwarte.Rotator.MqttGateway (short: Gateway)
//    - A mqtt broker, without SSL and authentification
//
// Description:	The Stroblhof Rotator is a simple and sturdy rotator device
//              for astronomical observations. The electronic supports also to connect
//              beside the rotator stepper motor also a focuser stepper motor.
//              The electronic itself support four stepper driver. 
//              The communication between the hardware and the ASCOM driver is done via MQTT.
//              The mqtt communication to the focuser device:
//             
//              Topic: 
//
//              Stroblhofwarte/Focuser/Position         Current focuser position (relative seen from device switch on), published by the gateway
//              Stroblhofwarte/Focuser/Move             "1" when focuser move, otherwise "0", published by the gateway
//              Stroblhofwarte/Focuser/Right            Move focuser "xxx" steps right, subscribed by the gateway
//              Stroblhofwarte/Focuser/Left             Move focuser "xxx" steps left, subscribed by the gateway
//              Stroblhofwarte/Focuser/Halt             Stop a running movemenet. Subscribed by the gateway.
//              Stroblhofwarte/Focuser/MaxMovement      Max. steps left and right. Published by the gateway.
//
// Implements:	ASCOM Focuser interface
// Author:		Othmar Ehrhardt, <othmar.ehrhardt@t-online.de>, https://astro.stroblhof-oberrohrbach.de
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 28.01.2023               Prototype setup is working
// --------------------------------------------------------------------------------
//

#define Focuser

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
    //
    // Your driver's DeviceID is ASCOM.Stroblhofwarte.mqtt.Focuser
    //
    // The Guid attribute sets the CLSID for ASCOM.Stroblhofwarte.mqtt.Focuser
    // The ClassInterface/None attribute prevents an empty interface called
    // _Stroblhofwarte.mqtt from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM Focuser Driver for Stroblhofwarte.mqtt.
    /// </summary>
    [Guid("bd663152-eb4e-4fee-bb7c-ee7fde24dc11")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Focuser : IFocuserV3
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.Stroblhofwarte.mqtt.Focuser";
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Focuser Driver for Stroblhofwarte.mqtt.";

        internal static string mqttHostName = "MqttHost";
        internal static string mqttHostDefault = "192.168.0.1";
        internal static string mqttPortName = "MqttPort";
        internal static string mqttPortDefault = "1883";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        private readonly static string MQTT_FOCUSER_PREFIX = "Stroblhofwarte/Focuser/";
        private readonly string MQTT_FOCUSER_POSITION = MQTT_FOCUSER_PREFIX + "Position";
        private readonly string MQTT_FOCUSER_MOVE = MQTT_FOCUSER_PREFIX + "Move";
        private readonly string MQTT_FOCUSER_RIGHT_TO = MQTT_FOCUSER_PREFIX + "Right";
        private readonly string MQTT_FOCUSER_LEFT_TO = MQTT_FOCUSER_PREFIX + "Left";
        private readonly string MQTT_FOCUSER_HALT = MQTT_FOCUSER_PREFIX + "Halt";
        private readonly string MQTT_FOCUSER_MAX_MOVEMENT = MQTT_FOCUSER_PREFIX + "MaxMovement";

        internal static string mqttHost; // Variables to hold the current device configuration
        internal static int mqttPort;
        private uPLibrary.Networking.M2Mqtt.MqttClient _mqtt = null;

        private bool _isMoving;
        private int _position;
        private int _maxMovement;

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
        public Focuser()
        {
            tl = new TraceLogger("", "Stroblhofwarte.mqtt");
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl.LogMessage("Focuser", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro-utilities object
            //TODO: Implement your additional construction here

            tl.LogMessage("Focuser", "Completed initialisation");
        }


        //
        // PUBLIC COM INTERFACE IFocuserV3 IMPLEMENTATION
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
                    _mqtt.Subscribe(new string[] { MQTT_FOCUSER_POSITION }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.Subscribe(new string[] { MQTT_FOCUSER_MOVE }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.Subscribe(new string[] { MQTT_FOCUSER_MAX_MOVEMENT }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    _mqtt.MqttMsgPublishReceived += _mqtt_MqttMsgPublishReceived;
                    _mqtt.Connect("Stroblhofwarte.mqtt.Focuser");
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
            if (e.Topic == MQTT_FOCUSER_POSITION)
            {
                try
                {
                    int pos = Convert.ToInt32(msg, CultureInfo.InvariantCulture);
                    _position = pos;
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if (e.Topic == MQTT_FOCUSER_MOVE)
            {
                _isMoving = false;
                if (msg == "1")
                {
                    _isMoving = true;
                }
            }
            if (e.Topic == MQTT_FOCUSER_MAX_MOVEMENT)
            {
                try
                {
                    int pos = Convert.ToInt32(msg, CultureInfo.InvariantCulture);
                    _maxMovement = pos;
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
                string driverInfo = "Stroblhofwarte.mqtt.Focuser.  Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
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
                string name = "mqtt.Focuser";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region IFocuser Implementation

        private int focuserPosition = 0; // Class level variable to hold the current focuser position
        private const int focuserSteps = 300000;
        public bool Absolute
        {
            get
            {
                tl.LogMessage("Absolute Get", true.ToString());
                return false; // This is an relative focuser
            }
        }

        public void Halt()
        {
            if (IsConnected)
            {
                _mqtt.Publish(MQTT_FOCUSER_HALT, Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
        }

        public bool IsMoving
        {
            get
            {
                return _isMoving;
            }
        }

        public bool Link
        {
            get
            {
                tl.LogMessage("Link Get", this.Connected.ToString());
                return this.Connected; // Direct function to the connected method, the Link method is just here for backwards compatibility
            }
            set
            {
                tl.LogMessage("Link Set", value.ToString());
                this.Connected = value; // Direct function to the connected method, the Link method is just here for backwards compatibility
            }
        }

        public int MaxIncrement
        {
            get
            {
                tl.LogMessage("MaxIncrement Get", _maxMovement.ToString());
                return focuserSteps; // Maximum change in one move
            }
        }

        public int MaxStep
        {
            get
            {
                tl.LogMessage("MaxStep Get", _maxMovement.ToString());
                return focuserSteps; // Maximum extent of the focuser, so position range is 0 to 10,000
            }
        }

        public void Move(int Position)
        {
            if(Position > 0)
            {
                if (IsConnected)
                {
                    _mqtt.Publish(MQTT_FOCUSER_RIGHT_TO, Encoding.UTF8.GetBytes(Position.ToString()), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
            }
            if (Position < 0)
            {
                if (IsConnected)
                {
                    _mqtt.Publish(MQTT_FOCUSER_LEFT_TO, Encoding.UTF8.GetBytes((-Position).ToString()), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
            }
        }

        public int Position
        {
            get
            {
                tl.LogMessage("Position", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Position", false);
            }
        }

        public double StepSize
        {
            get
            {
                return 1.0;
            }
        }

        public bool TempComp
        {
            get
            {
                tl.LogMessage("TempComp Get", false.ToString());
                return false;
            }
            set
            {
                tl.LogMessage("TempComp Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("TempComp", false);
            }
        }

        public bool TempCompAvailable
        {
            get
            {
                tl.LogMessage("TempCompAvailable Get", false.ToString());
                return false; // Temperature compensation is not available in this driver
            }
        }

        public double Temperature
        {
            get
            {
                tl.LogMessage("Temperature Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Temperature", false);
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
                P.DeviceType = "Focuser";
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
                driverProfile.DeviceType = "Focuser";
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
                driverProfile.DeviceType = "Focuser";
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
