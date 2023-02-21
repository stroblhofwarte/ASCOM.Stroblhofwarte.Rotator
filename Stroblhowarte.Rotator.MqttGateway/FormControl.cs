using System.Globalization;
using System.IO.Ports;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Stroblhofwarte.Rotator.MqttGateway
{
    public partial class FormControl : Form
    {
        private string _comPort;
        private ArduinoDevice _arduinoDevice = null;

        private float _rotatorOldPos;
        private bool _rotatorMoveLeft = false;
        private bool _rotatorMoveRight = false;

        private int _focuserOldPos;
        private int _focuserFastSteps = 1000;
        private int _focuserSlowSteps = 100;


        private uPLibrary.Networking.M2Mqtt.MqttClient _mqtt = null;
        private bool _mqttConnected = false;
        private bool _rotatorIsMoving = false;
        private bool _stepsizeSend = false;
        private bool _expandView = false;

        protected int _focuserMaxMovement;
        private bool _focuserIsMoving;

        #region MQTT Topics

        private readonly static string MQTT_ROTATOR_PREFIX = "Stroblhofwarte/Rotator/";
        private readonly string MQTT_ROTATOR_POSITION = MQTT_ROTATOR_PREFIX + "Position";
        private readonly string MQTT_ROTATOR_MOVE_TO = MQTT_ROTATOR_PREFIX + "Move";
        private readonly string MQTT_ROTATOR_HALT = MQTT_ROTATOR_PREFIX + "Halt";
        private readonly string MQTT_ROTATOR_SYNC = MQTT_ROTATOR_PREFIX + "Sync";
        private readonly string MQTT_ROTATOR_MAX_MOVEMENT = MQTT_ROTATOR_PREFIX + "MaxMovement";
        private readonly string MQTT_ROTATOR_STATE = MQTT_ROTATOR_PREFIX + "State";
        private readonly string MQTT_ROTATOR_STEPSIZE = MQTT_ROTATOR_PREFIX + "Stepsize";

        private readonly static string MQTT_FOCUSER_PREFIX = "Stroblhofwarte/Focuser/";
        private readonly string MQTT_FOCUSER_POSITION = MQTT_FOCUSER_PREFIX + "Position";
        private readonly string MQTT_FOCUSER_MOVE = MQTT_FOCUSER_PREFIX + "Move";
        private readonly string MQTT_FOCUSER_RIGHT_TO = MQTT_FOCUSER_PREFIX + "Right";
        private readonly string MQTT_FOCUSER_LEFT_TO = MQTT_FOCUSER_PREFIX + "Left";
        private readonly string MQTT_FOCUSER_HALT = MQTT_FOCUSER_PREFIX + "Halt";
        private readonly string MQTT_FOCUSER_MAX_MOVEMENT = MQTT_FOCUSER_PREFIX + "MaxMovement";
        private readonly string MQTT_FOCUSER_IS_ABS = MQTT_FOCUSER_PREFIX + "IsAbs";
        private readonly string MQTT_FOCUSER_MOVE_ABS = MQTT_FOCUSER_PREFIX + "MoveAbs";

        #endregion
        public FormControl()
        {
            LoadSettings();
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            buttonRotatorSetup.Enabled = false;
            buttonFocuserSetup.Enabled = false;
           
            ExpandView(_expandView);
        }

        private void timerStartup_Tick(object sender, EventArgs e)
        {
            timerStartup.Stop();
            if (Settings.Default.Autoconnect)
            {
                buttonConnect_Click(null, null);
            }
            if (Settings.Default.UseMQtt)
            {
                if (ConnectMQTT())
                {
                    buttonMqtt.Text = "Close";
                }
            }
        }
        private void LoadSettings()
        {
            _comPort = Settings.Default.ComPort;
            _focuserMaxMovement = Settings.Default.FocuserMaxMovement;
        }

        private void SaveSettings()
        {
            Settings.Default.ComPort = _comPort;

            Settings.Default.Save();
        }

        private void ExpandView(bool val)
        {
            if(val)
            {
                Size oldSize = this.Size;
                this.Size = new Size(700, oldSize.Height);
            }
            else
            {
                Size oldSize = this.Size;
                this.Size = new Size(400, oldSize.Height);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice != null)
            {
                _arduinoDevice.Close();
                _arduinoDevice = null;
                buttonConnect.Text = "Connect";
                buttonRotatorSetup.Enabled = false;
                buttonFocuserSetup.Enabled = false;
                return;
            }
            else
            {
                _arduinoDevice = new ArduinoDevice(_comPort);
                if (_arduinoDevice.Open())
                {
                    buttonConnect.Text = "Close";
                    _arduinoDevice.RotatorMaxMovement = (float)Settings.Default.RotatorMaxMovement;
                    _arduinoDevice.RotatorSetSpeed((float)Settings.Default.RotatorSpeed);
                    if (Settings.Default.MotorOff)
                    {
                        _arduinoDevice.RotatorMotorPowerOff();
                    }
                    else
                    {
                        _arduinoDevice.RotatorMotorPowerOn();
                    }
                    textBoxPosition.Text = _arduinoDevice.RotatorPosition().ToString();
                    _arduinoDevice.FocuserMotorPowerOff();

                    buttonRotatorSetup.Enabled = true;
                    buttonFocuserSetup.Enabled = true;
                    pictureBoxRotator.Invalidate();
                    if(_mqttConnected)
                    {
                        _mqtt.Publish(MQTT_ROTATOR_MAX_MOVEMENT, Encoding.UTF8.GetBytes(_arduinoDevice.RotatorMaxMovement.ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                        _mqtt.Publish(MQTT_FOCUSER_IS_ABS, Encoding.UTF8.GetBytes(_arduinoDevice.FocuserIsAbsoluteDevice().ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                    }
                }
                else
                {
                    _arduinoDevice.Close();
                    _arduinoDevice = null;
                    buttonRotatorSetup.Enabled = false;
                    buttonFocuserSetup.Enabled = false;
                    buttonConnect.Text = "Connect";
                    
                }
            }
        }

        private bool ConnectMQTT()
        {
            _mqttConnected = false;
            try
            {
                _mqtt = new uPLibrary.Networking.M2Mqtt.MqttClient(Settings.Default.MQTTHost, Settings.Default.MQTTPort, false, null, null, uPLibrary.Networking.M2Mqtt.MqttSslProtocols.None);
                _mqtt.Connect("Stroblhofwarte.Rotator");
                _mqtt.Subscribe(new string[] { MQTT_ROTATOR_MOVE_TO }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                _mqtt.Subscribe(new string[] { MQTT_ROTATOR_HALT }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                _mqtt.Subscribe(new string[] { MQTT_ROTATOR_SYNC }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

                _mqtt.Subscribe(new string[] { MQTT_FOCUSER_RIGHT_TO }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                _mqtt.Subscribe(new string[] { MQTT_FOCUSER_LEFT_TO }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                _mqtt.Subscribe(new string[] { MQTT_FOCUSER_HALT }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                _mqtt.Subscribe(new string[] { MQTT_FOCUSER_MOVE_ABS }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                
                _mqtt.MqttMsgPublishReceived += _mqtt_MqttMsgPublishReceived;

                _mqtt.Publish(MQTT_ROTATOR_STATE, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

                _mqtt.Publish(MQTT_FOCUSER_MAX_MOVEMENT, Encoding.UTF8.GetBytes(_focuserMaxMovement.ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                
                if(_arduinoDevice!= null)
                {
                    _mqtt.Publish(MQTT_ROTATOR_MAX_MOVEMENT, Encoding.UTF8.GetBytes(_arduinoDevice.RotatorMaxMovement.ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                    _mqtt.Publish(MQTT_FOCUSER_IS_ABS, Encoding.UTF8.GetBytes(_arduinoDevice.FocuserIsAbsoluteDevice().ToString()), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                }
                _mqttConnected = true;
                return true;
            } catch (Exception ex)
            {
                _mqttConnected = false;
                _mqtt = null;
                return false;
            }
        }

        private void _mqtt_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (_arduinoDevice == null) return;
            string msg = Encoding.ASCII.GetString(e.Message);
            if (e.Topic == MQTT_ROTATOR_MOVE_TO)
            {
                try
                {
                    float pos = (float)Convert.ToDouble(msg, CultureInfo.InvariantCulture);
                    _arduinoDevice.RotatorMoveAbsolute(pos);
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if (e.Topic == MQTT_ROTATOR_HALT)
            {
                if (msg == "1")
                {
                    _mqtt.Publish(MQTT_ROTATOR_HALT, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                    _arduinoDevice.RotatorHalt();
                }
            }
            if (e.Topic == MQTT_ROTATOR_SYNC)
            {
                try
                {
                    float pos = (float)Convert.ToDouble(msg, CultureInfo.InvariantCulture);
                    _arduinoDevice.RotatorSync(pos);
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if (e.Topic == MQTT_FOCUSER_RIGHT_TO)
            {
                try
                {
                    long pos = Convert.ToUInt32(msg, CultureInfo.InvariantCulture);
                    _arduinoDevice.FocuserMoveRight(pos);
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if (e.Topic == MQTT_FOCUSER_LEFT_TO)
            {
                try
                {
                    long pos = Convert.ToUInt32(msg, CultureInfo.InvariantCulture);
                    _arduinoDevice.FocuserMoveLeft(pos);
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            if (e.Topic == MQTT_FOCUSER_MOVE_ABS)
            {
                try
                {
                    long pos = Convert.ToUInt32(msg, CultureInfo.InvariantCulture);
                    _arduinoDevice.FocuserSetAbsPos(pos);
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
            
            if (e.Topic == MQTT_FOCUSER_HALT)
            {
                try
                {
                    if (msg == "1")
                    {
                        _mqtt.Publish(MQTT_FOCUSER_HALT, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                        _arduinoDevice.FocuserHalt();
                    }
                    _arduinoDevice.FocuserHalt();
                }
                catch (Exception ex)
                {
                    // Position value invalid! Do nothing!
                }
            }
        }

        private void timerFocuser_Tick(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            bool focuserIsMoving = _arduinoDevice.FocuserIsMoving();
            if (!focuserIsMoving) buttonFocuserGo.Text = "go";
            if (_focuserOldPos != _arduinoDevice.FocuserPosition())
            {
                _focuserOldPos = (int)_arduinoDevice.FocuserPosition();
                if (!_arduinoDevice.FocuserIsAbsoluteDevice())
                {
                    labelFocuserPosition.Text = _focuserOldPos.ToString();
                    panelFocuserImg.Hide();
                }
                else
                {
                    panelFocuserImg.Show();
                    long basePos = 50;
                    long maxPos = 125;
                    long maxSteps = _arduinoDevice.FocuserGetMaximalPos();
                    double factor = ((double)maxPos - (double)basePos) / (double)maxSteps;
                    labelFocuserPosition.Text = String.Format("{0:0.00}", ((double)_focuserOldPos / _arduinoDevice.FocuserGetCoefficient())) + " mm";
                    labelFocuserMax.Text = String.Format("{0:0.00}", (_arduinoDevice.FocuserGetMaximalPos() / _arduinoDevice.FocuserGetCoefficient())) + " mm";
                    pictureBoxFocuserMove.Location = new Point((int)(basePos + (int)((double)_focuserOldPos * factor)), pictureBoxFocuserMove.Location.Y);
                }
                if (_mqttConnected)
                    _mqtt.Publish(MQTT_FOCUSER_POSITION, Encoding.UTF8.GetBytes(_focuserOldPos.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
            if (_focuserIsMoving != focuserIsMoving)
            {
                if (focuserIsMoving)
                {
                    if (_mqttConnected)
                        _mqtt.Publish(MQTT_FOCUSER_MOVE, Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
                else
                {
                    if (_mqttConnected)
                        _mqtt.Publish(MQTT_FOCUSER_MOVE, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
                _focuserIsMoving = focuserIsMoving;
            }
        }
        private void timerRotatorPosition_Tick(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            bool rotatorIsMoving = _arduinoDevice.RotatorIsMoving();
            if(!rotatorIsMoving) buttonGoto.Text = "go";
            if (!_stepsizeSend && _mqttConnected)
            {
                float stepsize = _arduinoDevice.RotatorStepSize();
                _mqtt.Publish(MQTT_ROTATOR_STEPSIZE, Encoding.UTF8.GetBytes(stepsize.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                _stepsizeSend = true;
            }
            if (rotatorIsMoving != _rotatorIsMoving)
            {
                _rotatorIsMoving = rotatorIsMoving;
                if (rotatorIsMoving)
                {
                    if (_mqttConnected)
                        _mqtt.Publish(MQTT_ROTATOR_STATE, Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
                else 
                {
                    if (_mqttConnected)
                        _mqtt.Publish(MQTT_ROTATOR_STATE, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                }
            }
            if (_arduinoDevice.RotatorPosition() != _rotatorOldPos)
            {
                pictureBoxRotator.Invalidate();
                _rotatorOldPos = _arduinoDevice.RotatorPosition();
                labelPos.Text = _rotatorOldPos.ToString() + "°";
                if(_mqttConnected)
                    _mqtt.Publish(MQTT_ROTATOR_POSITION, Encoding.UTF8.GetBytes(_rotatorOldPos.ToString(CultureInfo.InvariantCulture)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
            if (_rotatorMoveLeft)
            {
                float pos = _arduinoDevice.RotatorPosition();
                if (pos > 10.0)
                    pos = pos - 10.0f;
                else
                    pos = 0.0f;
                _arduinoDevice.RotatorMoveAbsolute(pos);
            }
            if (_rotatorMoveRight)
            {
                float pos = _arduinoDevice.RotatorPosition();
                if (pos > (_arduinoDevice.RotatorMaxMovement - 10.0))
                    pos = _arduinoDevice.RotatorMaxMovement;
                else
                    pos = pos + 10.0f;
                _arduinoDevice.RotatorMoveAbsolute(pos);
            }

        }

        private void buttonRotatorMoveRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (_arduinoDevice == null) return;
            _rotatorMoveRight = true;
        }

        private void buttonRotatorMoveRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (_arduinoDevice == null) return;
            _rotatorMoveRight = false;
            _arduinoDevice.RotatorHalt();
            _arduinoDevice.RotatorMoveAbsolute(_arduinoDevice.RotatorPosition());
        }

        private void buttonRotatorMoveLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (_arduinoDevice == null) return;
            _rotatorMoveLeft = true;
        }

        private void buttonRotatorMoveLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (_arduinoDevice == null) return;
            _rotatorMoveLeft = false;
            _arduinoDevice.RotatorHalt();
            _arduinoDevice.RotatorMoveAbsolute(_arduinoDevice.RotatorPosition());
        }

        private void buttonGoto_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            if(_arduinoDevice.RotatorIsMoving())
            {
                _arduinoDevice.RotatorHalt();
                buttonGoto.Text = "go";
                return;
            }
            try
            {
                float pos = (float)Convert.ToDouble(textBoxPosition.Text, CultureInfo.InvariantCulture);
                if (pos > _arduinoDevice.RotatorMaxMovement)
                    pos = _arduinoDevice.RotatorMaxMovement;
                if (pos < 0)
                    pos = 0.0f;
                 _arduinoDevice.RotatorMoveAbsolute(pos);
                buttonGoto.Text = "X";
            } catch(Exception ex)
            {
                textBoxPosition.Text = _arduinoDevice.RotatorPosition().ToString();
                buttonGoto.Text = "go";
            }
        }

        private void buttonRotatorSetup_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            FormSetupDialogRotator dlg = new FormSetupDialogRotator(_arduinoDevice);
            dlg.ShowDialog();
            Settings.Default.RotatorMaxMovement = _arduinoDevice.RotatorMaxMovement;
            Settings.Default.RotatorSpeed = _arduinoDevice.RotatorGetSpeed();
            Settings.Default.MotorOff = _arduinoDevice.RotatorMotorOff;
            Settings.Default.Save();
        }

        private void buttonBaseSetup_Click(object sender, EventArgs e)
        {
            FormSetupDevice dlg = new FormSetupDevice();
            dlg.ShowDialog();
            if(Settings.Default.UseMQtt)
            {
                //buttonMqtt.Enabled = true;
                buttonMqtt.Text = "Close";
                ConnectMQTT();
            }
            else
            {
                buttonMqtt.Enabled = true;
                if(_mqtt != null)
                {
                    _mqtt = null;
                }
            }
        }

        private void buttonMqtt_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            if (!_mqttConnected)
            {
                if (ConnectMQTT())
                {
                    buttonMqtt.Text = "Close";
                    //buttonMqtt.Enabled = false;
                }
                else
                {
                    buttonMqtt.Text = "Connect";
                }
            }
            else
            {
                if(_mqtt != null)
                {
                    _mqtt.Disconnect();
                    _mqtt = null;
                    _mqttConnected = false;
                }
                buttonMqtt.Text = "Connect";
            }
        }

        private void FormControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_mqtt != null)
            {
                _mqtt.Disconnect();
            }
        }
        private void FormControl_Load(object sender, EventArgs e)
        {
            this.SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer,
                    true);
        }

        private void pictureBoxRotator_Paint(object sender, PaintEventArgs e)
        {
            if (_arduinoDevice == null) return;
            Graphics g = e.Graphics;
            //the central point of the rotation
            g.DrawLine(Pens.Silver, 100, 110, 100, 110 - 10);
            g.DrawString("N", SystemFonts.DefaultFont, Brushes.Silver, 100 - 5, 86);
            g.DrawLine(Pens.Silver, 100, 110, 110, 110);
            g.DrawString("E", SystemFonts.DefaultFont, Brushes.Silver, 110 + 3, 102);
            g.TranslateTransform(100, 100);
            //rotation procedure
            g.RotateTransform(_arduinoDevice.RotatorPosition());
            g.DrawEllipse(Pens.Silver, -100, -100, 200, 200);
            g.DrawRectangle(Pens.Silver, new Rectangle(-50, -30, 100, 60));
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if(_expandView)
            {
                _expandView = false;
                buttonExpand.Text = ">";
            }
            else
            {
                _expandView = true;
                buttonExpand.Text = "<";
            }
            ExpandView(_expandView);
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            try
            {
                float pos = (float)Convert.ToDouble(textBoxSync.Text, CultureInfo.InvariantCulture);
                _arduinoDevice.RotatorSync(pos);
            } catch(Exception ex)
            {
                textBoxSync.Text = "";
            }
        }

        private void buttonFastRight_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            _arduinoDevice.FocuserMoveRight(_focuserFastSteps);
        }

        private void buttonFastLeft_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            _arduinoDevice.FocuserMoveLeft(_focuserFastSteps);
        }

        private void buttonSlowRight_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            _arduinoDevice.FocuserMoveRight(_focuserSlowSteps);
        }

        private void buttonSlowLeft_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            _arduinoDevice.FocuserMoveLeft(_focuserSlowSteps);
        }

        private void buttonFocuserGo_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            try
            {
                if (!_arduinoDevice.FocuserIsAbsoluteDevice())
                {
                    int istPos = (int)_arduinoDevice.FocuserPosition();
                    int pos = Convert.ToInt32(textBoxFocuserGo.Text);
                    int move = pos - istPos;
                    if (move > 0)
                    {
                        _arduinoDevice.FocuserMoveRight(move);
                        buttonFocuserGo.Text = "X";
                    }
                    if (move < 0)
                    {
                        _arduinoDevice.FocuserMoveLeft(-move);
                        buttonFocuserGo.Text = "X";
                    }
                }
                else
                {
                    double pos = Convert.ToDouble(textBoxFocuserGo.Text, CultureInfo.InvariantCulture);
                    int steps = (int)(pos * _arduinoDevice.FocuserGetCoefficient());
                    _arduinoDevice.FocuserSetAbsPos(steps);
                }

            } catch (Exception ex)
            {

            }
        }

        private void buttonFocuserSetup_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            FormSetupFocuser dlg = new FormSetupFocuser(_arduinoDevice);
            dlg.ShowDialog();
            _focuserMaxMovement = Settings.Default.FocuserMaxMovement;
        }

    }
}