using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.Properties;

namespace Tool
{
    public partial class FormFirstUsed : Form
    {
        public delegate void FirsTime(bool isFirstTime);
        public event FirsTime SetFirstTime;

        private List<string> list = new List<string>();

        public FormFirstUsed(bool modeFirst)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;

            if (modeFirst == true)
            {
                label1.Text = Settings.Default.APPLICATION_NAME.ToString() + " xin chào!";
                this.Opacity = 1;
                buttonOK.Text = "BẮT ĐẦU";
                pictureBox1.Image = Properties.Resources.icon;
            }
            else
            {
                label1.Text = "Chọn cổng kết nối.";
                this.Opacity = 0.9;
                buttonOK.Text = "KẾT NỐI";
            }
            

            list.AddRange(SerialPort.GetPortNames());
            if (list.Count > 0)
            {
                comboBox1.Items.AddRange(list.ToArray());
                if ((Settings.Default.COMPORT.ToString().Equals("COM?") == true) || (Settings.Default.COMPORT.ToString().Equals("") == true))
                {
                    comboBox1.Text = "";
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    if (list.Contains(Settings.Default.COMPORT.ToString()) == true) comboBox1.Text = Settings.Default.COMPORT.ToString();
                    else comboBox1.SelectedIndex = 0;
                }
                
            }
            else
            {
                comboBox1.Text = "";
                comboBox1.SelectedIndex = -1;
            }

            System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(MyIntervalFunction));
            timer.Change(0, 500);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals(""))
            {
                MessageBox.Show("HÃY CHỌN CỔNG KẾT NỐI");
            }
            else
            {

                if (!SerialCommunicator.SerialPort.IsOpen)
                {
                    SerialCommunicator.SerialPort.PortName = comboBox1.GetItemText(comboBox1.SelectedItem);
                    SerialCommunicator.SerialPort.Open();
                    if (SerialCommunicator.SerialPort.IsOpen)
                    {
                        Settings.Default["COMPORT"] = comboBox1.GetItemText(comboBox1.SelectedItem);
                        Settings.Default.Save();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể kết nới với " + comboBox1.GetItemText(comboBox1.SelectedItem));
                    }
                }
                else
                {
                    SerialCommunicator.SerialPort.Close();
                    MessageBox.Show("KẾT NỐI LẠI ...");
                }
                SetFirstTime(false);
            }
        }

        private void MyIntervalFunction(object obj)
        {
            List<string> temp = new List<string>();
            temp.AddRange(SerialPort.GetPortNames());
            if (temp.ToArray().SequenceEqual(list.ToArray()) == false)
            {
                list.Clear();
                list = temp.GetClone();
                comboBox1.Invoke(() =>
                {
                    comboBox1.Items.Clear();
                    if (list.Count > 0)
                    {
                        comboBox1.Items.AddRange(list.ToArray());
                        comboBox1.Refresh();
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox1.Refresh();
                        comboBox1.SelectedIndex = -1;
                        comboBox1.Text = "";
                    }
                });
                
            }
        }
    }

    public static class ControlExtensions
    {
        public static void Invoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(action), null);
            }
            else
            {
                action.Invoke();
            }
        }

        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0, source.Count);
        }
    }
}
