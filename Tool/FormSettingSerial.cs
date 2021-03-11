using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.Properties;

namespace Tool
{
    public partial class FormSettingSerial : Form
    {
        private int userPermission = 0;

        private string[] baudRate = { "75", "150", "300", "600", "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", "230400" };
        private string[] dataBits = { "5", "6", "7", "8", "9" };
        private string[] parity = { "None", "Odd", "Even", "Mark", "Space" };
        private string[] stopBits = { "One", "Two", "OnePointFive" };

        private static string DEFAULT_BAUDRATE = "115200";
        private static string DEFAULT_DATABITS = "8";
        private static string DEFAULT_PARITY = "None";
        private static string DEFAULT_STOPBITS = "One";

        public FormSettingSerial(int userLevel)
        {
            InitializeComponent();
            this.userPermission = userLevel;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            if (this.userPermission == 4)
            {
                this.tableLayoutPanel2.Enabled = true;
                this.buttonReset.Enabled = true;
                this.buttonSave.Enabled = true;
            }
            else
            {
                this.tableLayoutPanel2.Enabled = false;
                this.buttonReset.Enabled = false;
                this.buttonSave.Enabled = false;
            }

            cbBaudRate.Items.AddRange(baudRate);
            cbDataBits.Items.AddRange(dataBits);
            cbParity.Items.AddRange(parity);
            cbStopBits.Items.AddRange(stopBits);

            cbBaudRate.SelectedItem = Settings.Default["BAUDRATE"].ToString();
            cbDataBits.SelectedItem = Settings.Default["DATABITS"].ToString();
            cbParity.SelectedItem = Settings.Default["PARITY"].ToString();
            cbStopBits.SelectedItem = Settings.Default["STOPBITS"].ToString();

            Invalidate();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SetSerialConfiguration();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            cbBaudRate.SelectedItem = DEFAULT_BAUDRATE;
            cbDataBits.SelectedItem = DEFAULT_DATABITS;
            cbParity.SelectedItem = DEFAULT_PARITY;
            cbStopBits.SelectedItem = DEFAULT_STOPBITS;

            SetSerialConfiguration();
        }

        private void SetSerialConfiguration()
        {
            Settings.Default["BAUDRATE"] = cbBaudRate.GetItemText(cbBaudRate.SelectedItem);
            Settings.Default["DATABITS"] = cbDataBits.GetItemText(cbDataBits.SelectedItem);
            Settings.Default["PARITY"] = cbParity.GetItemText(cbParity.SelectedItem);
            Settings.Default["STOPBITS"] = cbStopBits.GetItemText(cbStopBits.SelectedItem);
            Settings.Default.Save();

            SerialCommunicator.SerialPort.BaudRate = Convert.ToInt32(cbBaudRate.GetItemText(cbBaudRate.SelectedItem));
            SerialCommunicator.SerialPort.DataBits = Convert.ToInt16(cbDataBits.GetItemText(cbDataBits.SelectedItem));
            SerialCommunicator.SerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), cbParity.GetItemText(cbParity.SelectedItem));
            SerialCommunicator.SerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.GetItemText(cbStopBits.SelectedItem));
        }
    }
}
