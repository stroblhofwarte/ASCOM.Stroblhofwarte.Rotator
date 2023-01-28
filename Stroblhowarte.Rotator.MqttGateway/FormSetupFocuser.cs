using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stroblhofwarte.Rotator.MqttGateway
{
    public partial class FormSetupFocuser : Form
    {
        private ArduinoDevice _device;
        public FormSetupFocuser(ArduinoDevice device)
        {
            _device = device;
            InitializeComponent();
            textBoxLeftOvershoot.Text = Settings.Default.LeftOvershoot.ToString();
            textBoxRightOvershoot.Text = Settings.Default.RightOvershoot.ToString();
            textBoxMaxMovement.Text = Settings.Default.FocuserMaxMovement.ToString();
        }

        private void buttonRightSet_Click(object sender, EventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(textBoxRightOvershoot.Text);
                if(val <= 0)
                {
                    textBoxRightOvershoot.Text = "0";
                    return;
                }
                Settings.Default.RightOvershoot = val;
                Settings.Default.Save();
                _device.FocuserSetRightOvershoot(val);
                textBoxLeftOvershoot.Text = "0";
                _device.FocuserSetLeftOvershoot(0);
            } catch (Exception ex)
            {
                textBoxRightOvershoot.Text = "0";
                textBoxLeftOvershoot.Text = "0";
            }
        }

        private void buttonLeftSet_Click(object sender, EventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(textBoxLeftOvershoot.Text);
                if (val <= 0)
                {
                    textBoxLeftOvershoot.Text = "0";
                    return;
                }
                Settings.Default.LeftOvershoot = val;
                Settings.Default.Save();
                _device.FocuserSetLeftOvershoot(val);
                textBoxRightOvershoot.Text = "0";
                _device.FocuserSetRightOvershoot(0);
            }
            catch (Exception ex)
            {
                textBoxRightOvershoot.Text = "0";
                textBoxLeftOvershoot.Text = "0";
            }
        }

        private void buttonSetMax_Click(object sender, EventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(textBoxMaxMovement.Text);
                if (val <= 0)
                {
                    textBoxLeftOvershoot.Text = "0";
                    return;
                }
                Settings.Default.FocuserMaxMovement = val;
                Settings.Default.Save();
                
            }
            catch (Exception ex)
            {
                textBoxLeftOvershoot.Text = "30000";
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
