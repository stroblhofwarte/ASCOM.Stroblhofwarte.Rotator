//tabs=4
// --------------------------------------------------------------------------------
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

        internal static string comPort; 
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

        private bool CheckForStroblRotatorDevice()
        {
            lock (_lock)
            {
                string idString = String.Empty;
                int retry = 3;
                while(idString != "ROTATOR#")
                {
                    try
                    {
                        _serial.Transmit("ID:");
                        idString = _serial.ReceiveTerminated("#");
                    }
                    catch(Exception ex)
                    {
                        retry--;
                        if (retry == 0) return false;
                        continue;
                    }
                }
                return true;
            }
        }

        private string GetInfoString()
        {
            if (!connectedState) return "Not connected.";
            lock (_lock)
            {
                _serial.Transmit("IF:");
                string ret = _serial.ReceiveTerminated("#");
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                return ret;
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
                            if (CheckForStroblRotatorDevice())
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
                        _serial.Connected = false;
                        _serial.Dispose();
                        LogMessage("Connected Set", ex.ToString());
                    }
                }
                else
                {
                    connectedState = false;
                    _serial.Connected = false;
                    _serial.Dispose();
                    LogMessage("Connected Set", "Disconnecting from adress {0}", comPort);
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
                string driverInfo = "Stroblhowarte Rotator: " + GetInfoString();
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
                if (!connectedState) return 0.0f;
                lock (_lock)
                {
                    _serial.Transmit("SZ:");
                    string ret = _serial.ReceiveTerminated("#");
                    ret = ret.Replace('#', ' ');
                    ret = ret.Trim();
                    float size = (float)Convert.ToDouble(ret, CultureInfo.InvariantCulture);
                    return size;
                }
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
