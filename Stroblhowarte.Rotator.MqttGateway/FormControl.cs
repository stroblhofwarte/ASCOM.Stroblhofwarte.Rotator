using System.Globalization;
using System.IO.Ports;

namespace Stroblhowarte.Rotator.MqttGateway
{
    public partial class FormControl : Form
    {
        private string _comPort;
        private ArduinoDevice _arduinoDevice = null;

        private float _rotatorOldPos;
        private bool _rotatorMoveLeft = false;
        private bool _rotatorMoveRight = false;

        private uPLibrary.Networking.M2Mqtt.MqttClient _mqtt = null;
        private bool _mqttConnected = false;

        public FormControl()
        {
            LoadSettings();
            InitializeComponent();
            if(Settings.Default.Autoconnect)
            {
                buttonConnect_Click(null, null);
            }
            if(Settings.Default.UseMQtt)
            {
                if(ConnectMQTT())
                {
                    buttonMqtt.Enabled = false;
                }
            }
        }

        private void LoadSettings()
        {
            _comPort = Settings.Default.ComPort;
        }

        private void SaveSettings()
        {
            Settings.Default.ComPort = _comPort;

            Settings.Default.Save();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (_arduinoDevice != null)
            {
                _arduinoDevice.Close();
                _arduinoDevice = null;
                buttonConnect.Text = "Connect";
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
                }
                else
                {
                    _arduinoDevice.Close();
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
                _mqttConnected = true;
                return true;
            } catch (Exception ex)
            {
                _mqttConnected = false;
                _mqtt = null;
                return false;
            }
        }

        private void FormControl_Paint(object sender, PaintEventArgs e)
        {
            if (_arduinoDevice == null) return;
            Graphics g = this.CreateGraphics();
            //the central point of the rotation
            g.DrawLine(Pens.Silver, 200, 210, 200, 210 - 10);
            g.DrawString("N", SystemFonts.DefaultFont, Brushes.Silver, 200 - 5, 186);
            g.DrawLine(Pens.Silver, 200, 210, 210, 210);
            g.DrawString("E", SystemFonts.DefaultFont, Brushes.Silver, 210 + 3, 202);
            g.TranslateTransform(200, 200);
            //rotation procedure
            g.RotateTransform(_arduinoDevice.RotatorPosition());
            g.DrawEllipse(Pens.Silver, -100, -100, 200, 200);
            g.DrawRectangle(Pens.Silver, new Rectangle(-50, -30, 100, 60));

        }

        private void timerRotatorPosition_Tick(object sender, EventArgs e)
        {
            if (_arduinoDevice == null) return;
            if (_arduinoDevice.RotatorPosition() != _rotatorOldPos)
            {
                this.Invalidate();
                _rotatorOldPos = _arduinoDevice.RotatorPosition();
                labelPos.Text = _rotatorOldPos.ToString() + "°";
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
            try
            {
                float pos = (float)Convert.ToDouble(textBoxPosition.Text, CultureInfo.InvariantCulture);
                if (pos > _arduinoDevice.RotatorMaxMovement)
                    pos = _arduinoDevice.RotatorMaxMovement;
                if (pos < 0)
                    pos = 0.0f;
                 _arduinoDevice.RotatorMoveAbsolute(pos);
            } catch(Exception ex)
            {
                textBoxPosition.Text = _arduinoDevice.RotatorPosition().ToString();
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
                buttonMqtt.Enabled = true;
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
            if(ConnectMQTT())
            {
                buttonMqtt.Enabled = false;
            }
        }
    }
}