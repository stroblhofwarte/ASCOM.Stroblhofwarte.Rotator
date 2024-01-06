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
        public bool RotatorUsage { get; set; }
        public bool FocuserUsage { get; set; }

        #region Ctor

        private HwAccess()
        {
            SetupRequired = true;
        }

        #endregion

        public void Close()
        {
            if(!RotatorUsage && !FocuserUsage)
            {
                _serial.Connected = false;
                _serial.Dispose();
                Connected = false;
            }
        }

        public void Setup(TraceLogger tl)
        {
            _tl = tl;
            SetupRequired = false;
        }

        public bool Connect(string port)
        {
            if (SetupRequired) return false;
            comPort = port;
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

        public string GetInfoString()
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

        public void InitHardware()
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
            if (!RotatorUsage) return;
            _serial.Transmit("PA:");
            string ret = _serial.ReceiveTerminated("#");
        }

        public void RO_SetPark(float pp)
        {
            if (SetupRequired) return;
            if (!Connected) return;
            if (!RotatorUsage) return;
            _serial.Transmit("PP" + pp.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
        }

        public void RO_SetInitSpeed(float val)
        {
            if (val < 0.5) return;
            if (SetupRequired) return;
            if (!Connected) return;
            if (!RotatorUsage) return;
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
            if (!RotatorUsage) return;
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
            if (!RotatorUsage) return;
            _serial.Transmit("ST:");
            string ret = _serial.ReceiveTerminated("#");
        }

        public bool RO_IsMoving()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!RotatorUsage) return false;
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
            if (!RotatorUsage) return;
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
            if (!RotatorUsage) return false;
            lock (_lock)
            {
                LogMessage("MoveAbsolute", pos.ToString()); // Move to this position
                string cmd = "TA" + pos.ToString(CultureInfo.InvariantCulture) + ":";
                _serial.Transmit(cmd);
                string ret = _serial.ReceiveTerminated("#");
            }
            return true;
        }

        public bool RO_SetDerotationRate(double rate)
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!RotatorUsage) return false;
            lock (_lock)
            {
                LogMessage("SetDerotationRate", rate.ToString()); // derotate with this rate (steps/sec)
                string cmd = "RA" + rate.ToString(CultureInfo.InvariantCulture) + ":";
                _serial.Transmit(cmd);
                string ret = _serial.ReceiveTerminated("#");
            }
            return true;
        }


        public bool RO_MotorPowerOn()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!RotatorUsage) return false;
            _serial.Transmit("MON:");
            _serial.ReceiveTerminated("#");
            return true;
        }

        public bool RO_MotorPowerOff()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!RotatorUsage) return false;
            _serial.Transmit("MOFF:");
            _serial.ReceiveTerminated("#");
            return true;
        }

        public float RO_Position()
        {
            if (SetupRequired) return 0.0f;
            if (!Connected) return 0.0f;
            if (!RotatorUsage) return 0.0f;
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

        public float RO_StepSize()
        {
            if (SetupRequired) return 0.0f;
            if (!Connected) return 0.0f;
            if (!RotatorUsage) return 0.0f;
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

        public void RO_Sync(float syncPos)
        {
            if (SetupRequired) return;
            if (!Connected) return;
            if (!RotatorUsage) return;
            LogMessage("Sync", RO_Position().ToString()); // Sync to this position
            string cmd = "SY" + syncPos.ToString(CultureInfo.InvariantCulture) + ":";
            _serial.Transmit(cmd);
            string ret = _serial.ReceiveTerminated("#");
        }
        #endregion

        #region Focus device access

        public bool FOC_MoveLeft(long steps)
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            LogMessage("FOC_MoveLeft Get", steps.ToString());
            _serial.Transmit("FOCTL" + steps.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
            if (ret == "1#") return true;
            return false;
        }

        public bool FOC_MoveRight(long steps)
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            LogMessage("FOC_MoveRight Get", steps.ToString());
            _serial.Transmit("FOCTR" + steps.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
            if (ret == "1#") return true;
            return false;
        }

        public void FOC_Halt()
        {
            if (SetupRequired) return;
            if (!Connected) return;
            if (!FocuserUsage) return;
            _serial.Transmit("FOCST:");
            string ret = _serial.ReceiveTerminated("#");
        }

        public bool FOC_IsMoving()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            LogMessage("FOC_IsMoving Get", true.ToString()); // This rotator has instantaneous movement
            _serial.Transmit("FOCMV:");
            string ret = _serial.ReceiveTerminated("#");
            if (ret == "1#") return true;
            return false;
        }

        public long FOC_Position()
        {
            if (SetupRequired) return 0;
            if (!Connected) return 0;
            if (!FocuserUsage) return 0;
            lock (_lock)
            {
                _serial.Transmit("FOCPO:");
                string ret = _serial.ReceiveTerminated("#");
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                long pos = (long)Convert.ToInt32(ret, CultureInfo.InvariantCulture);
                return pos;
            }
        }

        public bool FOC_MotorPowerOn()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            _serial.Transmit("FOCMON:");
            _serial.ReceiveTerminated("#");
            return true;
        }

        public bool FOC_MotorPowerOff()
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            _serial.Transmit("FOCMOFF:");
            _serial.ReceiveTerminated("#");
            return true;
        }

        public bool FOC_SetRightOvershoot(long val)
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            LogMessage("FOC_SetRightOvershoot Get", val.ToString());
            _serial.Transmit("FOCOVERR" + val.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
            if (ret == "1#") return true;
            return false;
        }

        public bool FOC_SetLeftOvershoot(long val)
        {
            if (SetupRequired) return false;
            if (!Connected) return false;
            if (!FocuserUsage) return false;
            LogMessage("FOC_SetLeftOvershoot Get", val.ToString());
            _serial.Transmit("FOCOVERL" + val.ToString(CultureInfo.InvariantCulture) + ":");
            string ret = _serial.ReceiveTerminated("#");
            if (ret == "1#") return true;
            return false;
        }

        #endregion

        private void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            _tl.LogMessage(identifier, msg);
        }
    }
}
