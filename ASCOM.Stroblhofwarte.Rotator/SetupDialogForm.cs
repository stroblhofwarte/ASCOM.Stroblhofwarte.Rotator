// This file is part of the Stroblhofwarte.Rotator project 
// (https://github.com/stroblhofwarte/ASCOM.Stroblhofwarte.Rotator).
// Copyright (c) 2021, Othmar Ehrhardt, https://astro.stroblhof-oberrohrbach.de
//
// This program is free software: you can redistribute it and/or modify  
// it under the terms of the GNU General Public License as published by  
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License 
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using ASCOM.Stroblhofwarte;
using ASCOM.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ASCOM.Stroblhofwarte
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        TraceLogger tl; // Holder for a reference to the driver's trace logger
        Rotator _driver;
        public SetupDialogForm(Rotator driver)
        {
            InitializeComponent();

            _driver = driver;
            tl = driver.Logger;
            driver.ReadProfile();
            checkBoxPower.Checked = _driver.DoNotSwitchOffMotorPower;
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();

            labelPosition.Text = _driver.Position.ToString(CultureInfo.InvariantCulture) + "°";
            textBoxInitSpeed.Text = _driver.InitSpeed.ToString(CultureInfo.InvariantCulture);
            textBoxSpeed.Text = _driver.Speed.ToString(CultureInfo.InvariantCulture);
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Rotator.comPort = (string)comboBoxComPort.SelectedItem;
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
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
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
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Rotator.comPort))
            {
                comboBoxComPort.SelectedItem = Rotator.comPort;
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            try
            {
                float pos = (float)Convert.ToDouble(textBoxAngle.Text, CultureInfo.InvariantCulture);
                _driver.MoveAbsolute(pos);
            }
            catch (Exception ex)
            {
                textBoxAngle.Text = "0.0";
            }
        }

        private void timerPosition_Tick(object sender, EventArgs e)
        {
            labelPosition.Text = _driver.Position.ToString(CultureInfo.InvariantCulture) + "°";
            labelSyncValue.Text = _driver.SyncValue().ToString(CultureInfo.InvariantCulture) + "°";
            labelMechanicalPosition.Text = _driver.MechanicalPosition.ToString(CultureInfo.InvariantCulture) + "°";
            labelTargetPosition.Text = _driver.TargetPosition.ToString(CultureInfo.InvariantCulture) + "°";
        }

        private void checkBoxPower_CheckStateChanged(object sender, EventArgs e)
        {
            _driver.DoNotSwitchOffMotorPower = checkBoxPower.Checked;
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            try
            {
                float pos = (float)Convert.ToDouble(textBoxSync.Text, CultureInfo.InvariantCulture);
                _driver.Sync(pos);
            }
            catch (Exception ex)
            {
                textBoxSync.Text = "0.0";
            }
        }

        private void buttonUnsync_Click(object sender, EventArgs e)
        {
            _driver.ResetSync();
        }

        private void buttonPark_Click(object sender, EventArgs e)
        {
            _driver.Park();
        }

        private void buttonSetPark_Click(object sender, EventArgs e)
        {
            _driver.SetPark(_driver.MechanicalPosition);
        }

        private void buttonSetSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                float speed = (float)Convert.ToDouble(textBoxSpeed.Text, CultureInfo.InvariantCulture);
                _driver.Speed = speed;
            }
            catch (Exception ex)
            {
                textBoxSync.Text = "0.0";
            }
        }

        private void buttonSetInitSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                float speed = (float)Convert.ToDouble(textBoxInitSpeed.Text, CultureInfo.InvariantCulture);
                _driver.InitSpeed= speed;
            }
            catch (Exception ex)
            {
                textBoxSync.Text = "0.0";
            }
        }
    }
}
