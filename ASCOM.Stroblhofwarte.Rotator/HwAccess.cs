using ASCOM.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.Stroblhofwarte
{
    /// <summary>
    /// Hardware access to a multiple purpose device
    /// </summary>
    public class HwAccess
    {
        #region Simple Singelton

        private static HwAccess _instance = null;
        public static HwAccess Instance()
        {
            if(_instance == null)
            {
                _instance = new HwAccess();
            }
            return _instance;
        }

        #endregion
        public bool Connected { get; private set; }
        private float _RO_initSpeed;
        private float _RO_speed;
        private float _RO_maxMovement;
        public bool RO_IsReverse { get; private set; } 
        private string comPort;
        private ASCOM.Utilities.Serial _serial;
        private object _lock = new object();
        private TraceLogger _tl = null;
        public bool SetupRequired { get; private set; }


        #region Ctor

        private HwAccess()
        {
            SetupRequired = true;
        }

        #endregion

        public void Setup(TraceLogger tl, string port)
        {
            _tl = tl;
            comPort = port;
            SetupRequired = false;
        }

        public bool Connect()
        {
            if (SetupRequired) return false;
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
                    if (CheckForDevice())
                    {
                        Connected = true;
                        string inf = GetInfoString();
                        if (inf == "Not initialized yet.")
                        {
                            InitHardware();
                        }
                    }
                    else
                        Connected = false;
                }
            }
            catch (Exception ex)
            {
                _serial.Connected = false;
                _serial.Dispose();
                Connected = false;
                LogMessage("Connected Set", ex.ToString());
            }
            return Connected;
        }

        private bool CheckForDevice()
        {
            lock (_lock)
            {
                string idString = String.Empty;
                int retry = 3;
                while (idString != "ROTATOR#")
                {
                    try
                    {
                        _serial.Transmit("ID:");
                        idString = _serial.ReceiveTerminated("#");
                    }
                    catch (Exception ex)
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
            if (SetupRequired) return "Not connected.";
            if (!Connected) return "Not connected.";
            lock (_lock)
            {
                _serial.Transmit("IF:");
                string ret = _serial.ReceiveTerminated("#");
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                return ret;
            }
        }

        private void InitHardware()
        {
            if (SetupRequired) return;
            if (!Connected) return;
            lock (_lock)
            {
                _serial.Transmit("IN:");
                string ret = _serial.ReceiveTerminated("#");
            }
        }

        #region Rotator device access
        public void RO_Park()
        {
            if (SetupRequired) return;
            if (!Connected) return;
            _serial.Transmit("PA:");
            string ret = _serial.ReceiveTerminated("#");
        }

        public void RO_SetPark(float pp)
        {
            if (SetupRequired) return;
            if (!Connected) return;
            _serial.Transmit("PP" + pp.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
        }

        public void RO_SetInitSpeed(float val)
        {
            if (val < 0.5) return;
            if (SetupRequired) return;
            if (!Connected) return;
            _RO_initSpeed = val;
            _serial.Transmit("IS" + _RO_initSpeed.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
        }

        public float RO_GetInitSpeed()
        {
                return _RO_initSpeed;
        }

        public void RO_SetSpeed(float val)
        {
            if (val < 0.5) return;
            if (SetupRequired) return;
            if (!Connected) return;
            _RO_speed = val;
               
            _serial.Transmit("SP" + _RO_speed.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
        }

        public float RO_GetSpeed()
        {
            return _RO_speed;
        }

        public void RO_Halt()
        {
            if (SetupRequired) return;
            if (!Connected) return;
            _serial.Transmit("ST:");
            string ret = _serial.ReceiveTerminated("#");
        }

        public bool RO_IsMoving()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            LogMessage("IsMoving Get", false.ToString()); // This rotator has instantaneous movement
            _serial.Transmit("MV:");
            string ret = _serial.ReceiveTerminated("#");
            if (ret == "1#") return true;
            return false;
        }

        public void RO_Move(float pos)
        {
            if (SetupRequired) return;
            if (!Connected) return;
           
            lock (_lock)
            {
                LogMessage("Move", pos.ToString()); // Move by this amount
                if (pos > 0)
                {
                    _serial.Transmit("TR" + pos.ToString(CultureInfo.InvariantCulture) + ":");
                    RO_IsReverse = false;
                }
                if (pos < 0)
                {
                    _serial.Transmit("TL" + (-pos).ToString(CultureInfo.InvariantCulture) + ":");
                    RO_IsReverse = true;
                }
                string ret = _serial.ReceiveTerminated("#");
            }
        }

        public bool RO_MoveAbsolute(float pos)
        {
            if (SetupRequired) return false;
            if (!Connected) return false;

            lock (_lock)
            {
                LogMessage("MoveAbsolute", pos.ToString()); // Move to this position
                string cmd = "TA" + pos.ToString(CultureInfo.InvariantCulture) + ":";
                _serial.Transmit(cmd);
                string ret = _serial.ReceiveTerminated("#");
            }
            return true;
        }


        public bool RO_MotorPowerOn()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;

            _serial.Transmit("MON:");
            _serial.ReceiveTerminated("#");
            return true;
        }

        public bool RO_MotorPowerOff()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;

            _serial.Transmit("MOFF:");
            _serial.ReceiveTerminated("#");
            return true;
        }

        #endregion

        private void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            _tl.LogMessage(identifier, msg);
        }
    }
}
