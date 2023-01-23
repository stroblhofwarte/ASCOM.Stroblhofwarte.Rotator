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
    public partial class FormSetupDialogRotator : Form
    {
        private ArduinoDevice _device;
        public FormSetupDialogRotator(ArduinoDevice device)
        {
            _device = device;
            InitializeComponent();
            textBoxRotatorMaximalMovement.Text = device.RotatorMaxMovement.ToString();
            textBoxSpeedFactor.Text = device.RotatorGetSpeed().ToString();
            checkBoxSwitchMotorOff.Checked = device.RotatorMotorOff;
        }

        private void buttonSetSpeedFactor_Click(object sender, EventArgs e)
        {
            try
            {
                float speed = (float)Convert.ToDouble(textBoxSpeedFactor.Text, CultureInfo.InvariantCulture);
                _device.RotatorSetSpeed(speed);
            } catch(Exception ex)
            {
                textBoxSpeedFactor.Text = _device.RotatorGetSpeed().ToString();
            }
            
        }

        private void buttonSetMaximalMovement_Click(object sender, EventArgs e)
        {
            try
            {
                float maxmove = (float)Convert.ToDouble(textBoxRotatorMaximalMovement.Text, CultureInfo.InvariantCulture);
                _device.RotatorMaxMovement = maxmove;
            }
            catch (Exception ex)
            {
                textBoxSpeedFactor.Text = _device.RotatorMaxMovement.ToString();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void checkBoxSwitchMotorOff_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxSwitchMotorOff.Checked)
            {
                _device.RotatorMotorPowerOff();
            }
            else
            {
                _device.RotatorMotorPowerOn();
            }
        }
    }
}
