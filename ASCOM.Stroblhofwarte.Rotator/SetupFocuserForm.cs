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
            
        }

        private void buttonFineLeft_Click(object sender, EventArgs e)
        {

        }

        private void buttonFineRight_Click(object sender, EventArgs e)
        {

        }

        private void buttonFastLeft_Click(object sender, EventArgs e)
        {

        }

        private void buttonFastRight_Click(object sender, EventArgs e)
        {

        }
    }
}
