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
        private readonly string _focusPointsFileName = "FocusPoints.xml";

        private ArduinoDevice _device;

        private double _calMaxOutInmm = 0;
        private int _calMovedSteps = 0;
        private DataSetFocusPoints _dataSetFocusPoints;


        public FormSetupFocuser(ArduinoDevice device)
        {
            _dataSetFocusPoints = new DataSetFocusPoints();
            _device = device;
            InitializeComponent();
            try
            {
                _dataSetFocusPoints.ReadXml(_focusPointsFileName);
                bindingSourceFocusPoints.DataSource = _dataSetFocusPoints.DataTableFocusPoints;
                dataGridViewFocusPoints.DataSource = bindingSourceFocusPoints;
            }
            catch (Exception ex)
            {
                // Can happen when file not exist
            }
            
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

        private void dataGridViewFocusPoints_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridViewFocusPoints.Rows[e.Row.Index - 1].Cells[1].Value = _device.FocuserPosition();
        }

        private void bindingSourceFocusPoints_CurrentChanged(object sender, EventArgs e)
        {
                _dataSetFocusPoints.WriteXml(_focusPointsFileName);
        }

        private void dataGridViewFocusPoints_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
           _dataSetFocusPoints.AcceptChanges();
        }

        private void buttonFocuserGoTo_Click(object sender, EventArgs e)
        {
            if (dataGridViewFocusPoints.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Move focuser to selected porition?", "Focuser move", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        int pos = Convert.ToInt32(dataGridViewFocusPoints.SelectedRows[0].Cells[1].Value);
                        _device.FocuserSetAbsPos(pos);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

        }
    }
}
