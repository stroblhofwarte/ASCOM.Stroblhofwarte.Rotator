using ASCOM.Stroblhofwarte.mqtt;
using ASCOM.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ASCOM.Stroblhofwarte.mqtt
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        TraceLogger tl; // Holder for a reference to the driver's trace logger
        private FilterWheel _instance;
        public SetupDialogForm(TraceLogger tlDriver, FilterWheel driver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;
            _instance = driver;
            
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            FilterWheel.mqttHost = textBoxBroker.Text;
            try
            {
                FilterWheel.mqttPort = Convert.ToInt32(textBoxPort.Text, CultureInfo.InvariantCulture);
                FilterWheel.rawR = Convert.ToInt32(textBoxR.Text, CultureInfo.InvariantCulture);
                FilterWheel.rawG = Convert.ToInt32(textBoxG.Text, CultureInfo.InvariantCulture);
                FilterWheel.rawB = Convert.ToInt32(textBoxB.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                FilterWheel.mqttPort = 1883;
            }
            tl.Enabled = chkTrace.Checked;
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkTrace.Checked = tl.Enabled;
            textBoxBroker.Text = FilterWheel.mqttHost;
            textBoxPort.Text = FilterWheel.mqttPort.ToString(CultureInfo.InvariantCulture);
            textBoxR.Text = FilterWheel.rawR.ToString();
            textBoxG.Text = FilterWheel.rawG.ToString();
            textBoxB.Text = FilterWheel.rawB.ToString();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            int port = 1883;
            try
            {
                port = Convert.ToInt32(textBoxPort.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                port = 1883;
            }
            try
            {
                labelTestInfo.Text = "Setup broker...";
                uPLibrary.Networking.M2Mqtt.MqttClient test = new uPLibrary.Networking.M2Mqtt.MqttClient(textBoxBroker.Text, port, false, null, null, uPLibrary.Networking.M2Mqtt.MqttSslProtocols.None);
                labelTestInfo.Text = "try to connect broker...";
                test.Connect("testClient");
                labelTestInfo.Text = "Successfully connected.";
            }
            catch (Exception ex)
            {
                labelTestInfo.Text = "Connection failed.";
            }
        }

        private void UpdateOffsets()
        {
            try
            {
                FilterWheel.rawR = Convert.ToInt32(textBoxR.Text, CultureInfo.InvariantCulture);
                FilterWheel.rawG = Convert.ToInt32(textBoxG.Text, CultureInfo.InvariantCulture);
                FilterWheel.rawB = Convert.ToInt32(textBoxB.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return;
            }
            _instance.CalculateOffsets();
            labelOffsetR.Text = FilterWheel.offsetRG.ToString();
            labelOffsetG.Text = FilterWheel.offsetGB.ToString();
            labelOffsetB.Text = FilterWheel.offsetBR.ToString();
        }

        private void textBoxR_TextChanged(object sender, EventArgs e)
        {
            UpdateOffsets();
        }

        private void textBoxG_TextChanged(object sender, EventArgs e)
        {
            UpdateOffsets();
        }

        private void textBoxB_TextChanged(object sender, EventArgs e)
        {
            UpdateOffsets();
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            textBoxR.Text = _instance.FocuserPosition.ToString();
            UpdateOffsets();
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            textBoxG.Text = _instance.FocuserPosition.ToString();
            UpdateOffsets();
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            textBoxB.Text = _instance.FocuserPosition.ToString();
            UpdateOffsets();
        }
    }
}