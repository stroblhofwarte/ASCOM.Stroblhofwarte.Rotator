using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stroblhofwarte.Rotator.MqttGateway
{
    public partial class FormSetupFocuser : Form
    {
        private ArduinoDevice _device;

        private double _calMaxOutInmm = 0;
        private int _calMovedSteps = 0;
        
        public FormSetupFocuser(ArduinoDevice device)
        {
            _device = device;
            InitializeComponent();
            textBoxMaxMovement.Text = Settings.Default.FocuserMaxMovement.ToString();
            checkBoxReverse.Checked = _device.RotatorIsReverse;
            if (_device.FocuserIsAbsoluteDevice()) checkBoxAbsolute.Checked = true;
            else checkBoxAbsolute.Checked = false;
            ResetCalibrationWizard();
        }

        private void buttonSetMax_Click(object sender, EventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(textBoxMaxMovement.Text);
                Settings.Default.FocuserMaxMovement = val;
                Settings.Default.Save();
                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void checkBoxReverse_CheckedChanged(object sender, EventArgs e)
        {
            _device.FocuserSetReverse(checkBoxReverse.Checked);
        }

        #region Calibartion wizard

        private void ResetCalibrationWizard()
        {
            checkBoxStep1.Checked = false;
            checkBoxStep2.Checked = false;
            checkBoxStep3.Checked = false;
            checkBoxStep4.Checked = false;

            checkBoxStep1.Enabled = false;
            checkBoxStep2.Enabled = false;
            checkBoxStep3.Enabled = false;
            checkBoxStep4.Enabled = false;
            // Step 1
            textBoxMaxLength.Enabled = false;
            buttonSetMaxLength.Enabled = false;
            // Step 3
            textBoxMeasMovement.Enabled = false;
            buttonMove.Enabled = false;
            checkBoxReverse.Enabled = false;

            // Step 4
            textBoxTravel.Enabled = false;
            buttonSetTravel.Enabled = false;
            
        }

        private void Step1()
        {
            checkBoxStep1.Enabled = false;
            checkBoxStep2.Enabled = false;
            checkBoxStep3.Enabled = false;
            checkBoxStep4.Enabled = false;
            // Step 1
            textBoxMaxLength.Enabled = true;
            buttonSetMaxLength.Enabled = true;
            // Step 3
            textBoxMeasMovement.Enabled = false;
            buttonMove.Enabled = false;
            checkBoxReverse.Enabled = false;

            // Step 4
            textBoxTravel.Enabled = false;
            buttonSetTravel.Enabled = false;
        }

        private void Step2()
        {
            checkBoxStep1.Enabled = false;
            checkBoxStep2.Enabled = true;
            checkBoxStep3.Enabled = false;
            checkBoxStep4.Enabled = false;
            // Step 1
            textBoxMaxLength.Enabled = false;
            buttonSetMaxLength.Enabled = false;
            // Step 3
            textBoxMeasMovement.Enabled = false;
            buttonMove.Enabled = false;
            checkBoxReverse.Enabled = false;

            // Step 4
            textBoxTravel.Enabled = false;
            buttonSetTravel.Enabled = false;
        }

        private void Step3()
        {
            checkBoxStep1.Enabled = false;
            checkBoxStep2.Enabled = false;
            checkBoxStep3.Enabled = false;
            checkBoxStep4.Enabled = false;
            // Step 1
            textBoxMaxLength.Enabled = false;
            buttonSetMaxLength.Enabled = false;
            // Step 3
            textBoxMeasMovement.Enabled = true;
            buttonMove.Enabled = true;
            checkBoxReverse.Enabled = true;

            // Step 4
            textBoxTravel.Enabled = false;
            buttonSetTravel.Enabled = false;
        }

        private void Step4()
        {
            checkBoxStep1.Enabled = false;
            checkBoxStep2.Enabled = false;
            checkBoxStep3.Enabled = false;
            checkBoxStep4.Enabled = false;
            // Step 1
            textBoxMaxLength.Enabled = false;
            buttonSetMaxLength.Enabled = false;
            // Step 3
            textBoxMeasMovement.Enabled = false;
            buttonMove.Enabled = false;
            checkBoxReverse.Enabled = false;

            // Step 4
            textBoxTravel.Enabled = true;
            buttonSetTravel.Enabled = true;
        }

        #endregion

        private void buttonStartCalib_Click(object sender, EventArgs e)
        {
            Step1();
        }

        private void checkBoxStep1_CheckedChanged(object sender, EventArgs e)
        {
            Step2();
        }

        private void checkBoxStep2_CheckedChanged(object sender, EventArgs e)
        {
            Step3();
        }

        private void checkBoxStep3_CheckedChanged(object sender, EventArgs e)
        {
            Step4();
        }

        private void checkBoxStep4_CheckedChanged(object sender, EventArgs e)
        {
            ResetCalibrationWizard();
            checkBoxAbsolute.Checked = true;
            _device.FocuserSetAbsoluteDevice(true);
            int maxSteps = (int)(_calMaxOutInmm * _device.FocuserGetCoefficient());
            _device.FocuserSetMaximalPos(maxSteps);
        }

        private void buttonSetTravel_Click(object sender, EventArgs e)
        {
            try
            {
                double travel = Convert.ToDouble(textBoxTravel.Text, CultureInfo.InvariantCulture);
                double coeff = (double)_calMovedSteps / travel;
                _device.FocuserSetCoefficient(coeff);
                _device.FocuserSetPosition(_calMovedSteps);
                checkBoxStep4.Enabled = true;
            }
            catch (Exception ex)
            {
                textBoxMeasMovement.Text = "!WRONGFORMAT";
            }
        }

        private void buttonSetMaxLength_Click(object sender, EventArgs e)
        {
            try
            {
                _calMaxOutInmm = Convert.ToDouble(textBoxMaxLength.Text, CultureInfo.InvariantCulture);
                checkBoxStep1.Enabled = true;
            } catch(Exception ex)
            {
                _calMaxOutInmm = 0.0;
                textBoxMaxLength.Text = "!WRONGFORMAT";
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            try
            {
                int move = Convert.ToInt32(textBoxMeasMovement.Text, CultureInfo.InvariantCulture);
                _device.FocuserMoveRight(move);
                _calMovedSteps = move;
                checkBoxStep3.Enabled = true;
            }
            catch(Exception ex)
            {
                textBoxMeasMovement.Text = "!WRONGFORMAT";
            }
        }

        private void checkBoxAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            _device.FocuserSetAbsoluteDevice(checkBoxAbsolute.Checked);
        }

        private void buttonReCal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please move the Focuser to the \"in\" position and press OK");
            _device.FocuserSetPosition(0);
        }
    }
}
