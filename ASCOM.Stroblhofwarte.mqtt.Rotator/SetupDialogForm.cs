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

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Rotator.mqttHost = textBoxBroker.Text;
            try
            {
                Rotator.mqttPort = Convert.ToInt32(textBoxPort.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Rotator.mqttPort = 1883;
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
            // set the list of com ports to those that are currently available
            textBoxBroker.Text = Rotator.mqttHost;
            textBoxPort.Text = Rotator.mqttPort.ToString(CultureInfo.InvariantCulture);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            int port = 1883;
            try
            {
                port = Convert.ToInt32(textBoxPort.Text, CultureInfo.InvariantCulture);
            } catch (Exception ex)
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
            } catch (Exception ex)
            {
                labelTestInfo.Text = "Connection failed.";
            }

        }
    }
}