using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroblhofwarte.Rotator.MqttGateway
{
    public class ArduinoDevice
    {
        private readonly int _SERIAL_RECEIVE_TIMEOUT = 600; // ms

        private SerialPort _serial = null;
        private string _comPort;
        private bool _serialIsConnected = false;

        private double _rotatorInitSpeed = 0.0;
        private double _rotatorSpeed = 0.0;
        private object _lock = new object();

        public bool RotatorIsReverse { private set; get;}
        public float RotatorMaxMovement { set; get; }
        public bool RotatorMotorOff { get; set; }

        #region Ctor

        public ArduinoDevice(string comPort)
        {
            _comPort = comPort;
            RotatorMaxMovement = 359.0f;
        }

        #endregion

        public bool Open()
        {
            try
            {
                if (_serial == null)
                    _serial = new SerialPort(_comPort, 115200, Parity.None, 8, StopBits.One);
                _serial.Open();
                if (!_serial.IsOpen)
                {
                    _serial.Dispose();
                    _serial = null;
                    _serialIsConnected = false;
                    return false;
                }
                _serialIsConnected = true;
                // Check for rotator device
                string id = SendAndReceive("ID:", '#');
                if (id != "ROTATOR#")
                {
                    _serial.Close();
                    _serial.Dispose();
                    _serial = null;
                    _serialIsConnected = false;
                    return false;
                }
                SendAndReceive("IN:", '#');
                return true;
            }catch (Exception ex)
            {
                _serialIsConnected = false;
                return false;
            }
        }

        public void Close()
        {
            if(_serial != null)
            {
                if (_serial.IsOpen)
                {
                    _serial.Close();
                }
                _serial.Dispose();
                _serial = null;
            }
        }

        private string SendAndReceive(string command, char endSign)
        {
            if (_serial == null) return String.Empty;
            if (!_serial.IsOpen) return string.Empty;
            _serial.Write(command);
            string str = string.Empty;
            int timeout = _SERIAL_RECEIVE_TIMEOUT;
            char c = '\0';
            while (c != endSign && timeout > 0)
            {
                if (_serial.BytesToRead > 0)
                {
                    c = (char)_serial.ReadChar();
                    str += c;
                }
                else
                {
                    timeout--;
                    Thread.Sleep(1);
                }
            }
            return str;
        }

        #region Rotatordevice

        public string RotatorGetInfoString()
        {
            if (_serial == null) return "Not connected.";
            if (!_serial.IsOpen) return "Not connected.";
            lock (_lock)
            {
                string str = SendAndReceive("IF:", '#');
                str = str.Replace('#', ' ');
                str = str.Trim();
                return str;
            }
        }

        public void RotatorInitHardware()
        {
            if (_serial == null) return;
            if (!_serial.IsOpen) return;
            lock (_lock)
            {
                SendAndReceive("IN:", '#');
            }
        }

        public void RotatorPark()
        {
            if (_serial == null) return;
            if (!_serial.IsOpen) return;
            lock(_lock)
                SendAndReceive("PA:", '#');
        }

        public void RotatorSetPark(float pp)
        {
            if (_serial == null) return;
            if (!_serial.IsOpen) return;
            lock (_lock)
                SendAndReceive("PP" + pp.ToString(CultureInfo.InvariantCulture) + ":", '#');
        }

        public void RotatorSetInitSpeed(float val)
        {
            if (_serial == null) return;
            if (val < 0.5) return;
            if (!_serial.IsOpen) return;
            _rotatorInitSpeed = (double)val;
            lock (_lock)
                SendAndReceive("IS" + val.ToString(CultureInfo.InvariantCulture) + ":", '#');
        }

        public float RotatorGetInitSpeed()
        {
            return (float)_rotatorInitSpeed;
        }

        public void RotatorSetSpeed(float val)
        {
            if (_serial == null) return;
            if (val < 0.5) return;
            if (!_serial.IsOpen) return;
            _rotatorSpeed = val;
            lock (_lock)
                SendAndReceive("SP" + val.ToString(CultureInfo.InvariantCulture) + ":", '#');
        }

        public float RotatorGetSpeed()
        {
            return (float)_rotatorSpeed;
        }

        public void RotatorHalt()
        {
            if (_serial == null) return;
            if (!_serial.IsOpen) return;
            lock (_lock)
                SendAndReceive("ST:", '#');
        }

        public bool RotatorIsMoving()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("MV:", '#');
                if (ret == "1#") return true;
                return false;
            }
        }

        public void RotatorMove(float pos)
        {
            if (_serial == null) return;
            if (!_serial.IsOpen) return;
            lock (_lock)
            {
                if (pos > 0)
                {
                    SendAndReceive("TR" + pos.ToString(CultureInfo.InvariantCulture) + ":", '#');
                    RotatorIsReverse = false;
                }
                if (pos < 0)
                {
                    SendAndReceive("TL" + (-pos).ToString(CultureInfo.InvariantCulture) + ":", '#');
                    RotatorIsReverse = true;
                }
            }
        }

        public bool RotatorMoveAbsolute(float pos)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string cmd = "TA" + pos.ToString(CultureInfo.InvariantCulture) + ":";
                SendAndReceive(cmd ,'#');
            }
            return true;
        }


        public bool RotatorMotorPowerOn()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
                SendAndReceive("MON:",'#');
            RotatorMotorOff = false;
            return true;
        }

        public bool RotatorMotorPowerOff()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
                SendAndReceive("MOFF:", '#');
            RotatorMotorOff = true;
            return true;
        }

        public float RotatorPosition()
        {
            if (_serial == null) return 0.0f;
            if (!_serial.IsOpen) return 0.0f;
            lock (_lock)
            {
RepeatPositionRead:
                string ret =  SendAndReceive("GP:",'#');
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                if(ret == "")
                {
                    goto RepeatPositionRead;
                }    
                float pos = (float)Convert.ToDouble(ret, CultureInfo.InvariantCulture);
                return pos;
            }
        }

        public float RotatorStepSize()
        {
            if (_serial == null) return 0.0f;
            if (!_serial.IsOpen) return 0.0f;
            lock (_lock)
            {
                string ret = SendAndReceive("SZ:", '#');
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                float size = (float)Convert.ToDouble(ret, CultureInfo.InvariantCulture);
                return size;
            }
        }

        public void RotatorSync(float syncPos)
        {
            if (!_serialIsConnected) return;
            lock (_lock)
            {
                string cmd = "SY" + syncPos.ToString(CultureInfo.InvariantCulture) + ":";
                SendAndReceive(cmd, '#');
            }
        }
        #endregion

        #region Focuserdevice

        public bool FocuserMoveLeft(long steps)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                if(FocuserIsAbsoluteDevice())
                {
                    if (FocuserPosition() <= 0)
                        return false;
                }
                string ret = SendAndReceive("FOCTL" + steps.ToString(CultureInfo.InvariantCulture) + ":", '#');

                if (ret == "1#") return true;
                return false;
            }
        }

        public bool FocuserMoveRight(long steps)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                if (FocuserIsAbsoluteDevice())
                {
                    if (FocuserPosition() > FocuserGetMaximalPos())
                        return false;
                }
                string ret = SendAndReceive("FOCTR" + steps.ToString(CultureInfo.InvariantCulture) + ":", '#');

                if (ret == "1#") return true;
                return false;
            }
        }

        public void FocuserHalt()
        {
            if (_serial == null) return;
            if (!_serial.IsOpen) return;
            lock (_lock)
            {
                SendAndReceive("FOCST:", '#');
            }
        }

        public bool FocuserIsMoving()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCMV:", '#');
                if (ret == "1#") return true;
                return false;
            }
        }

        public long FocuserPosition()
        {
            if (_serial == null) return 0;
            if (!_serial.IsOpen) return 0;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCPO:", '#');
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                long pos = (long)Convert.ToInt32(ret, CultureInfo.InvariantCulture);
                return pos;
            }
        }

        public bool FocuserMotorPowerOn()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                SendAndReceive("FOCMON:", '#');
            }
            return true;
        }

        public bool FocuserMotorPowerOff()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                SendAndReceive("FOCMOFF:", '#');
            }
            return true;
        }
        public bool FocuserSetCoefficient(double val /* steps/mm */)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCCOEFF" + val.ToString(CultureInfo.InvariantCulture) + ":", '#');
                if (ret == "1#") return true;
                return false;
            }
        }
        public double FocuserGetCoefficient()
        {
            if (_serial == null) return -1.0;
            if (!_serial.IsOpen) return -1.0;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCGCOEFF:", '#');
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                try
                {
                    return Convert.ToDouble(ret, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    return -1.0;
                }
            }
        }

        public bool FocuserSetAbsoluteDevice(bool val)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                if (val)
                {
                    string ret = SendAndReceive("FOCABS:", '#');
                }
                else
                {
                    string ret = SendAndReceive("FOCREL:", '#');
                }
                return true;
            }
            
        }

        public bool FocuserIsAbsoluteDevice()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCTYP:", '#');
                if (ret == "ABS#") return true;
                return false;
            }
        }

        public bool FocuserSetPosition(long val)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCSPOS" + val.ToString() + ":", '#');
                return true;
            }
        }

        public bool FocuserSetMaximalPos(long val)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCMPOS" + val.ToString() + ":", '#');
                return true;
            }
            
        }

        public long FocuserGetMaximalPos()
        {
            if (_serial == null) return -1;
            if (!_serial.IsOpen) return -1;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCMGPOS:", '#');
                ret = ret.Replace('#', ' ');
                ret = ret.Trim();
                try
                {
                    return Convert.ToInt32(ret, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public bool FocuserSetAbsPos(long val)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCMOVABS" + val.ToString() + ":", '#');
                return true;
            }
        }

        public bool FocuserSetReverse(bool val)
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                if (val)
                {
                    string ret = SendAndReceive("FOCREV:", '#');
                }
                else
                {
                    string ret = SendAndReceive("FOCNOR:", '#');
                }
                return true;
            }
        }

        public bool FocuserIsReverse()
        {
            if (_serial == null) return false;
            if (!_serial.IsOpen) return false;
            lock (_lock)
            {
                string ret = SendAndReceive("FOCGREV:", '#');
                if (ret == "0#") return false;
                if (ret == "1#") return true;
                return false;
            }
        }
        #endregion

    }
}
