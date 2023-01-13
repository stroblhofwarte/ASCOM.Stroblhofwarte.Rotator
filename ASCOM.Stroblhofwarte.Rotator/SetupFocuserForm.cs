using ASCOM.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASCOM.Stroblhofwarte
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupFocuserForm : Form
    {
        TraceLogger tl; // Holder for a reference to the driver's trace logger
        Focuser _driver;
        public SetupFocuserForm(Focuser driver)
        {
            InitializeComponent();
            // Save the provided trace logger for use within the setup dialogue
            _driver = driver;
            tl = _driver.Logger;
            _driver.ReadProfile();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            tl.Enabled = chkTrace.Checked;
            Focuser.comPort = (string)comboBoxComPort.SelectedItem;
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
            textBoxFineSteps.Text = _driver.FineSteps.ToString();
            textBoxFastSteps.Text = _driver.FastSteps.ToString();
            textBoxOvershootValue.Text = _driver.OvershootValue.ToString();
            if (_driver.OvershootSetting == 0)
            {
                radioButtonNoOvershoot.Checked = true;
            }
            if (_driver.OvershootSetting == 1)
            {
                radioButtonOvershootRight.Checked = true;
            }
            if (_driver.OvershootSetting == 2)
            {
                radioButtonOvershootLeft.Checked = true;
            }
            // set the list of com ports to those that are currently available
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Focuser.comPort))
            {
                comboBoxComPort.SelectedItem = Focuser.comPort;
            }
        }

        private void buttonSetFineSteps_Click(object sender, EventArgs e)
        {
            try
            {
                _driver.FineSteps = Convert.ToInt32(textBoxFineSteps.Text);
                _driver.WriteProfile();
            } catch(Exception ex)
            {
                // Keep old value
                textBoxFineSteps.Text = _driver.FineSteps.ToString();
            }
        }

        private void buttonSetFastSteps_Click(object sender, EventArgs e)
        {
            try
            {
                _driver.FastSteps = Convert.ToInt32(textBoxFastSteps.Text);
                _driver.WriteProfile();
            }
            catch (Exception ex)
            {
                // Keep old value
                textBoxFastSteps.Text = _driver.FastSteps.ToString();
            }
        }

        private void buttonMoveAbsolute_Click(object sender, EventArgs e)
        {
            try
            {
                long newPos = Convert.ToInt32(textBoxMoveAbsolute.Text);
                long pos = HwAccess.Instance().FOC_Position();
                long movement = newPos - pos;
                if (movement > 0)
                    HwAccess.Instance().FOC_MoveRight(movement);
                if (movement < 0)
                    HwAccess.Instance().FOC_MoveLeft(-movement);
            }
            catch (Exception ex)
            {
                // no movement at all!
            }
        }

        private void buttonFineLeft_Click(object sender, EventArgs e)
        {
            HwAccess.Instance().FOC_MoveLeft(_driver.FineSteps);
        }

        private void buttonFineRight_Click(object sender, EventArgs e)
        {
            HwAccess.Instance().FOC_MoveRight(_driver.FineSteps);
        }

        private void buttonFastLeft_Click(object sender, EventArgs e)
        {
            HwAccess.Instance().FOC_MoveLeft(_driver.FastSteps);
        }

        private void buttonFastRight_Click(object sender, EventArgs e)
        {
            HwAccess.Instance().FOC_MoveRight(_driver.FastSteps);
        }

        private void buttonOvershoot_Click(object sender, EventArgs e)
        {
            try
            {
                _driver.OvershootValue = Convert.ToInt32(textBoxOvershootValue.Text);
            } catch(Exception ex)
            {
                textBoxOvershootValue.Text = _driver.OvershootValue.ToString();
            }
            if (radioButtonNoOvershoot.Checked)
                _driver.OvershootSetting = 0;
            if (radioButtonOvershootRight.Checked)
                _driver.OvershootSetting = 1;
            if (radioButtonOvershootLeft.Checked)
                _driver.OvershootSetting = 2;
            _driver.WriteProfile();
            if(_driver.OvershootSetting == 0)
            {
                HwAccess.Instance().FOC_SetLeftOvershoot(0);
            }
            if (_driver.OvershootSetting == 1)
            {
                HwAccess.Instance().FOC_SetRightOvershoot(_driver.OvershootValue);
            }
            if (_driver.OvershootSetting == 2)
            {
                HwAccess.Instance().FOC_SetLeftOvershoot(_driver.OvershootValue);
            }

        }

        private void timerPosition_Tick(object sender, EventArgs e)
        {
            long pos = HwAccess.Instance().FOC_Position();
            labelPosition.Text = pos.ToString();
        }
    }
}
