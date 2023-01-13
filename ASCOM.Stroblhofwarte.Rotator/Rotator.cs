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
// ASCOM Rotator driver for Stroblhofwarte Rotator
//
// Description:	The Stroblhof Rotator is a simple and sturdy rotator device
//              for astronomical observations. It could be build without access to
//              a lathe or milling machine. The rotator device is made out of aluminium
//              and steel. It is driven by a NEMA 14 stepper motor. The rotator could 
//              rotate (appromimately) between between 0° and 330°. 
//              The electronics are made out of a arduino uno and a shield with ST820 driver.
//              Each stepper driver can be used, the DRV8825 driver supports 32 microsteps for
//              smooth operation.
//              The communication between this driver and the arduino is done with a simple
//              serial protocol, 9600 baud. A command from this driver to the arduino ends with
//              a colon (:), the answer of the arduino ends with a hash (#).
//              
//              Command         Response        Description
//              -----------------------------------------------------------------------------
//              ID:             ROTATOR#        Device identification
//              TRxxx:          1#              Move right xxx degrees (float with decimal point)
//              TLxxx:          1#              Move left xxx degrees
//              TAxxx:          1#              Move absolute to xxx degrees
//              GP:             xxxx#           Return the current position
//              ST:             1#              Stop the current movement
//              MV:             0# or 1#        #1: Rotator is moving, otherwise 0#
//              MOFF:           1#              The motor is disabled after movement
//              MON:            1#              The motor is always powerd
//
// Implements:	ASCOM Rotator interface version: 3.0
// Author:		Othmar Ehrhardt, <othmar.ehrhardt@t-online.de>, https://astro.stroblhof-oberrohrbach.de
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 19.05.2021               Prototype setup is working
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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.Stroblhofwarte
{
    //
    // DeviceID is ASCOM.Stroblhofwarte.Rotator
    //
    // The Guid attribute sets the CLSID for ASCOM.Stroblhofwarte.Rotator
    // The ClassInterface/None attribute prevents an empty interface called
    // _Stroblhofwarte from being created and used as the [default] interface
    //


    /// <summary>
    /// ASCOM Rotator Driver for Stroblhofwarte.
    /// </summary>
    [Guid("e606b00b-1b17-4ddf-97b3-c0ddc2b501e9")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Rotator : IRotatorV3
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.Stroblhofwarte.Rotator";
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Rotator Driver for Stroblhofwarte.Rotator";

        internal static string comPortProfileName = "COM Port";
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";
        internal static string comPort = "COM1";

        private bool _isReverse = false;

        internal static string _doNotSwitchPoerOffProfileName = "DoNotSwitchPowerOff";
        private bool _doNotSwitchPowerOff = false;

        private float _targetPosition = 0.0f;

        internal static string _parkPositionName = "ParkPos";
        private float _parkPosition = 0.0f;

        internal static string _initSpeedName = "InitSpeed";
        private float _initSpeed = 1.0f;

        internal static string _speedName = "Speed";
        private float _speed = 1.0f;

        internal static string _maxMovementName = "MaxMovement";
        private float _maxMovement = 350.0f;

        private object _lock = new object();
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
        public TraceLogger Logger { get { return tl; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stroblhofwarte"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Rotator()
        {
            tl = new TraceLogger("", "Stroblhofwarte");
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl.LogMessage("Rotator", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro-utilities object
           
            tl.LogMessage("Rotator", "Completed initialisation");
            HwAccess.Instance().Setup(tl);
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
            
            using (SetupDialogForm F = new SetupDialogForm(this))
            {
                if(!connectedState) F.SetSetupMode();
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public bool DoNotSwitchOffMotorPower
        {
            get
            {
                return _doNotSwitchPowerOff;
            }
            set
            {
                _doNotSwitchPowerOff = value;
                WriteProfile();
                
                if(value)
                {
                    HwAccess.Instance().RO_MotorPowerOn();
                }
                else
                {
                    HwAccess.Instance().RO_MotorPowerOff();
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
            this.CommandString(command, raw);
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            return true;
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            string ret = CommandString(command, raw);
            return ret;
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
                if(value && HwAccess.Instance().Connected)
                {
                    // Hardware is connected from other device, update 
                    // some rotator specific variables:
                    connectedState = true;
                    bool state = DoNotSwitchOffMotorPower;
                    DoNotSwitchOffMotorPower = state;
                    HwAccess.Instance().RO_SetPark(_parkPosition);
                    HwAccess.Instance().RO_SetInitSpeed(_initSpeed);
                    HwAccess.Instance().RO_SetSpeed(_speed);
                    MaxMovement = _maxMovement;
                    HwAccess.Instance().RotatorUsage = true;
                    return;
                }
                if (value)
                {
                    LogMessage("Connected Set", "Connecting to address {0}", comPort);
                    try
                    {
                        LogMessage("Connected Set", "Connecting to port {0}", comPort);
                        lock (_lock)
                        {
                            HwAccess.Instance().Connect(comPort);

                            connectedState = HwAccess.Instance().Connected;
                            // set the motor power off state again 
                            // to transmit this setting also to the arduino device:
                            bool state = DoNotSwitchOffMotorPower;
                            DoNotSwitchOffMotorPower = state;
                            HwAccess.Instance().RO_SetPark(_parkPosition);
                            HwAccess.Instance().RO_SetInitSpeed(_initSpeed);
                            HwAccess.Instance().RO_SetSpeed(_speed);
                            MaxMovement = _maxMovement;
                            if(connectedState)
                            {
                                HwAccess.Instance().InitHardware();
                            }
                            else
                                connectedState = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        connectedState = false;
                        LogMessage("Connected Set", ex.ToString());
                    }
                }
                else
                {
                    connectedState = false;
                    HwAccess.Instance().RotatorUsage = false;
                    HwAccess.Instance().Close();
                    LogMessage("Connected Set", "Disconnecting for Rotator from adress {0}", comPort);
                }
 
            }
        }

        public string Description
        {
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
                string driverInfo = "Stroblhowarte Rotator: " + HwAccess.Instance().GetInfoString();
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
                string name = "Stroblhofwarte.Rotator";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region IRotator Implementation

        public bool CanReverse
        {
            get
            {
                tl.LogMessage("CanReverse Get", true.ToString());
                return true;
            }
        }

        public void Park()
        {
            HwAccess.Instance().RO_Park();
        }

        public void SetPark(float pp)
        {
            _parkPosition = pp;
            WriteProfile();
            HwAccess.Instance().RO_SetPark(pp);
        }

        public float Position
        {
            get
            {
                return FromMechanicalPositionToSyncPosition(HwAccess.Instance().RO_Position());
            }
        }
        public float InitSpeed
        {
            set
            {
                if (value < 0.5) return;
                _initSpeed = value;
                WriteProfile();
                HwAccess.Instance().RO_SetInitSpeed(value);
            }
            get
            {
                return _initSpeed;
            }
        }

        public float Speed
        {
            set
            {
                if (value < 0.5) return;
                _speed = value;
                WriteProfile();
                HwAccess.Instance().RO_SetSpeed(value);
            }
            get
            {
                return _speed;
            }
        }

        public float MaxMovement
        {
            set
            {
                _maxMovement = value;
                if (value > 359.0f) _maxMovement = 359.0f;
                if (value < 0.0f) _maxMovement = 0.0f;
                WriteProfile();
            }
            get
            {
                return _maxMovement;
            }
        }
        public void Halt()
        {
            HwAccess.Instance().RO_Halt();
        }

        public bool IsMoving
        {
            get
            {
                return HwAccess.Instance().RO_IsMoving();
            }
        }

        public void Move(float pos)
        {
            if (!connectedState) return;
            _targetPosition = HwAccess.Instance().RO_Position() + pos;
            // Check if the maximal rotation value is exceeded:
            if (FromSyncPositionToMechanicalPosition(_targetPosition) > _maxMovement) return;
             HwAccess.Instance().RO_Move(pos);
        }

        public void MoveAbsolute(float pos)
        {
            if (!connectedState) return;
            _targetPosition = pos;
            // Check if the maximal rotation value is exceeded:
            if (FromSyncPositionToMechanicalPosition(_targetPosition) > _maxMovement) return;
            float truePos = FromSyncPositionToMechanicalPosition(pos);
            HwAccess.Instance().RO_MoveAbsolute(truePos);
        }

        public float FromMechanicalPositionToSyncPosition(float mechPos)
        {
            return mechPos;
        }

        public float FromSyncPositionToMechanicalPosition(float syncPos)
        {
            return syncPos;
        }

        public bool Reverse
        {
            get
            {
                return _isReverse;
            }
            set
            {
                _isReverse = value;
            }
        }

        public float StepSize
        {
            get
            {
                return HwAccess.Instance().RO_StepSize();
            }
        }

        public float TargetPosition
        {
            get
            {
                return _targetPosition;
            }
        }

        // IRotatorV3 methods

        public float MechanicalPosition
        {
            get
            {
                return HwAccess.Instance().RO_Position();
            }
        }

        public void MoveMechanical(float pos)
        {
            // Check if the maximal rotation value is exceeded:
            if (pos > _maxMovement) return;
            HwAccess.Instance().RO_MoveAbsolute(pos);
        }

        public void Sync(float syncPos)
        {
            HwAccess.Instance().RO_Sync(syncPos);
        }

        public void ResetSync()
        {
            
        }

        public float SyncValue()
        {
            return 0.0f;
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
                comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
                _doNotSwitchPowerOff = Convert.ToBoolean(driverProfile.GetValue(driverID, _doNotSwitchPoerOffProfileName, string.Empty, "false"));
                _parkPosition = (float)Convert.ToDouble(driverProfile.GetValue(driverID, _parkPositionName, string.Empty, "0.0"), CultureInfo.InvariantCulture);
                _initSpeed = (float)Convert.ToDouble(driverProfile.GetValue(driverID, _initSpeedName, string.Empty, "1.0"), CultureInfo.InvariantCulture);
                _speed = (float)Convert.ToDouble(driverProfile.GetValue(driverID, _speedName, string.Empty, "1.0"), CultureInfo.InvariantCulture);
                _maxMovement = (float)Convert.ToDouble(driverProfile.GetValue(driverID, _maxMovementName, string.Empty, "350.0"),CultureInfo.InvariantCulture);

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
                driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString());
                driverProfile.WriteValue(driverID, _doNotSwitchPoerOffProfileName, _doNotSwitchPowerOff.ToString());
                driverProfile.WriteValue(driverID, _parkPositionName, _parkPosition.ToString(CultureInfo.InvariantCulture));
                driverProfile.WriteValue(driverID, _initSpeedName, _initSpeed.ToString(CultureInfo.InvariantCulture));
                driverProfile.WriteValue(driverID, _speedName, _speed.ToString(CultureInfo.InvariantCulture));
                driverProfile.WriteValue(driverID, _maxMovementName, _maxMovement.ToString(CultureInfo.InvariantCulture));
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
