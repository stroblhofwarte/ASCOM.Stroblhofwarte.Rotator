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
        
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void buttonReCal_Click(object sender, EventArgs e)
        {
            _device.FocuserMotorPowerOff();
            MessageBox.Show("Please move the Focuser to the \"in\" position (Cxx turn right) and press OK");
            _device.FocuserSetPosition(0);
            if (Settings.Default.FocMotorOff)
            {
                _device.FocuserMotorPowerOff();
            }
            else
            {
                _device.FocuserMotorPowerOn();
            }
        }
    }
}
