//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Rotator driver for Stroblhofwarte
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Rotator interface version: <To be completed by driver developer>
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code can be deleted and this definition removed.
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

namespace ASCOM.Stroblhofwarte
{
    //
    // Your driver's DeviceID is ASCOM.Stroblhofwarte.Rotator
    //
    // The Guid attribute sets the CLSID for ASCOM.Stroblhofwarte.Rotator
    // The ClassInterface/None attribute prevents an empty interface called
    // _Stroblhofwarte from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
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
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Rotator Driver for Stroblhofwarte.Rotator";

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        internal static string comPort; // Variables to hold the current device configuration
        private ASCOM.Utilities.Serial _serial;

        private bool _isReverse = false;

        internal static string _doNotSwitchPoerOffProfileName = "DoNotSwitchPowerOff";
        private bool _doNotSwitchPowerOff = false;

        internal static string _syncDiffProfileName = "SyncDiff";
        private float _syncDiff = 0.0f;

        private float _targetPosition = 0.0f;

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

            using (SetupDialogForm F = new SetupDialogForm(this))
            {
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
                if (!connectedState) return;
                if(value)
                {
                    _serial.Transmit("MON:");
                    _serial.ReceiveTerminated("#");
                }
                else
                {
                    _serial.Transmit("MOFF:");
                    _serial.ReceiveTerminated("#");
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
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
            // DO NOT have both these sections!  One or the other
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
            // DO NOT have both these sections!  One or the other
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

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

        private bool CheckForStroblCapDevice()
        {
            lock (_lock)
            {
                // Device does not connect the first time. It might be
                // that one or more charcters are put on the serial bus
                // on startup. This characters are readed also and the
                // ID is not recocnised. Simple hack: Try it twice!
                _serial.Transmit("ID:");
                string ret = _serial.ReceiveTerminated("#");

                if (ret == "ROTATOR#")
                    return true;
                _serial.Transmit("ID:");
                ret = _serial.ReceiveTerminated("#");

                if (ret == "ROTATOR#")
                    return true;
                return false;
            }
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
                    LogMessage("Connected Set", "Connecting to address {0}", comPort);
                    try
                    {
                        LogMessage("Connected Set", "Connecting to port {0}", comPort);
                        lock (_lock)
                        {
                            _serial = new ASCOM.Utilities.Serial();
                            _serial.PortName = comPort;
                            _serial.StopBits = SerialStopBits.One;
                            _serial.Parity = SerialParity.None;
                            _serial.Speed = SerialSpeed.ps9600;
                            _serial.DTREnable = false;
                            _serial.Connected = true;
                            if (CheckForStroblCapDevice())
                            {
                                connectedState = true;
                                // set the motor power off state again 
                                // to transmit this setting also to the arduino device:
                                bool state = DoNotSwitchOffMotorPower;
                                DoNotSwitchOffMotorPower = state;
                            }
                            else
                                connectedState = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Connected Set", ex.ToString());
                    }
                }
                else
                {
                    connectedState = false;
                    _serial.Dispose();
                    LogMessage("Connected Set", "Disconnecting from adress {0}", comPort);
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
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
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
            if (!connectedState) return;
            _serial.Transmit("ST:");
            string ret = _serial.ReceiveTerminated("#");
        }

        public bool IsMoving
        {
            get
            {
                if (!connectedState) return false;
                tl.LogMessage("IsMoving Get", false.ToString()); // This rotator has instantaneous movement
                _serial.Transmit("MV:");
                string ret = _serial.ReceiveTerminated("#");
                if (ret == "1#") return true;
                return false;
            }
        }

        public void Move(float pos)
        {
            if (!connectedState) return;
            _targetPosition = Position + pos;
            lock (_lock)
            {
                tl.LogMessage("Move", pos.ToString()); // Move by this amount
                if (pos > 0)
                {
                    _serial.Transmit("TR" + pos.ToString(CultureInfo.InvariantCulture) + ":");
                    _isReverse = false;
                }
                if (pos < 0)
                {
                    _serial.Transmit("TL" + (-pos).ToString(CultureInfo.InvariantCulture) + ":");
                    _isReverse = true;
                }
                string ret = _serial.ReceiveTerminated("#");
            }
        }

        public void MoveAbsolute(float pos)
        {
            if (!connectedState) return;
            _targetPosition = pos;
            float truePos = FromSyncPositionToMechanicalPosition(pos);
            lock (_lock)
            {
                tl.LogMessage("MoveAbsolute", truePos.ToString()); // Move to this position
                string cmd = "TA" + truePos.ToString(CultureInfo.InvariantCulture) + ":";
                _serial.Transmit(cmd);
                string ret = _serial.ReceiveTerminated("#");
            }
        }

        public float FromMechanicalPositionToSyncPosition(float mechPos)
        {
            float pos = mechPos + _syncDiff;
            if (pos > 360) pos = pos - 360;
            if (pos < 0) pos = 360 + pos;
            return pos;
        }

        public float FromSyncPositionToMechanicalPosition(float syncPos)
        {
            float pos = syncPos - _syncDiff;
            if (pos > 360) pos = pos - 360;
            if (pos < 0) pos = 360 + pos;
            return pos;
        }

        public float Position
        {
            get
            {
                if (!connectedState) return 0.0f;
                lock (_lock)
                {
                    _serial.Transmit("GP:");
                    string ret = _serial.ReceiveTerminated("#");
                    ret = ret.Replace('#', ' ');
                    ret = ret.Trim();
                    float pos = (float)Convert.ToDouble(ret, CultureInfo.InvariantCulture);
                    return FromMechanicalPositionToSyncPosition(pos);
                }
            }
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
                return 0.1f;
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
                if (!connectedState) return 0.0f;
                lock (_lock)
                {
                    _serial.Transmit("GP:");
                    string ret = _serial.ReceiveTerminated("#");
                    ret = ret.Replace('#', ' ');
                    ret = ret.Trim();
                    float pos = (float)Convert.ToDouble(ret, CultureInfo.InvariantCulture);
                    return pos;
                }
            }
        }

        public void MoveMechanical(float pos)
        {
            if (!connectedState) return;
            lock (_lock)
            {
                tl.LogMessage("MoveAbsolute", pos.ToString()); // Move to this position
                string cmd = "TA" + pos.ToString(CultureInfo.InvariantCulture) + ":";
                _serial.Transmit(cmd);
                string ret = _serial.ReceiveTerminated("#");
            }
        }

        public void Sync(float syncPos)
        {
            tl.LogMessage("Sync", Position.ToString()); // Sync to this position
            _serial.Transmit("GP:");
            string ret = _serial.ReceiveTerminated("#");
            ret = ret.Replace('#', ' ');
            ret = ret.Trim();
            float pos = (float)Convert.ToDouble(ret, CultureInfo.InvariantCulture);

            _syncDiff = syncPos - pos;
            WriteProfile();
        }

        public void ResetSync()
        {
            _syncDiff = 0.0f;
        }

        public float SyncValue()
        {
            return _syncDiff;
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
                comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
                _doNotSwitchPowerOff = Convert.ToBoolean(driverProfile.GetValue(driverID, _doNotSwitchPoerOffProfileName, string.Empty, "false"));
                _syncDiff = (float)Convert.ToDouble(driverProfile.GetValue(driverID, _syncDiffProfileName, string.Empty, "0.0"));
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
                driverProfile.WriteValue(driverID, _syncDiffProfileName, _syncDiff.ToString(CultureInfo.InvariantCulture));
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
