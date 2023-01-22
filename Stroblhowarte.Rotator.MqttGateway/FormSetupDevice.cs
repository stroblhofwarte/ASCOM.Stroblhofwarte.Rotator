using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stroblhowarte.Rotator.MqttGateway
{
    public partial class FormSetupDevice : Form
    {
        public FormSetupDevice()
        {
            string comPort = Settings.Default.ComPort;
            InitializeComponent();
            FillSerialPorts(comPort);
            checkBoxAutoconnect.Checked = Settings.Default.Autoconnect;
            checkBoxMqtt.Checked = Settings.Default.UseMQtt;
            textBoxMqttBroker.Text = Settings.Default.MQTTHost;
            textBoxMQTTPort.Text = Settings.Default.MQTTPort.ToString();
        }


        private void FillSerialPorts(string setTo)
        {
            int idx = -1;
            comboBoxComPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            int cnt = 0;
            foreach (string port in ports)
            {
                if (port == setTo)
                    idx = cnt;
                cnt++;
                comboBoxComPort.Items.Add(port);
            }
            if (idx > -1)
                comboBoxComPort.SelectedIndex = idx;
        }

        private void buttonSerialTest_Click(object sender, EventArgs e)
        {
            labelSerialTestInfo.Text = "busy";
            ArduinoDevice test = new ArduinoDevice(comboBoxComPort.Text);
            if(test.Open())
            {
                labelSerialTestInfo.Text = "Rotator multi device found";
                test.Close();
            }
            else
            {
                labelSerialTestInfo.Text = "No Rotator device found";
            }
            
        }

        private void comboBoxComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comPort = comboBoxComPort.Text;
            Settings.Default.ComPort = comPort;
            Settings.Default.Save();
        }

        private void checkBoxAutoconnect_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Autoconnect = checkBoxAutoconnect.Checked;
            Settings.Default.Save();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Settings.Default.MQTTHost = textBoxMqttBroker.Text;
            try
            {
                Settings.Default.MQTTPort = Convert.ToInt32(textBoxMQTTPort.Text);
            }
            catch(Exception ex)
            {
                Settings.Default.MQTTPort = 1883;
                textBoxMQTTPort.Text = "1883";
            }
            Settings.Default.Save();
            Hide();
        }

        private void buttonMqttTest_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            int port = 1883;
            try
            {
                port = Convert.ToInt32(textBoxMQTTPort.Text);
            } catch (Exception ex)
            {
                port = 1883;
            }
            try
            {
                labelMqttTestInfo.Text = "Setup broker...";
                uPLibrary.Networking.M2Mqtt.MqttClient test = new uPLibrary.Networking.M2Mqtt.MqttClient(textBoxMqttBroker.Text, port, false, null, null, uPLibrary.Networking.M2Mqtt.MqttSslProtocols.None);
                labelMqttTestInfo.Text = "try to connect broker...";
                test.Connect("testClient");
                labelMqttTestInfo.Text = "Connected to broker";
            } catch (Exception ex)
            {
                labelMqttTestInfo.Text = "Failed to connect!";
            }
            Application.UseWaitCursor = false;
        }

        private void checkBoxMqtt_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UseMQtt = checkBoxMqtt.Checked;
            Settings.Default.Save();
        }
    }
}
