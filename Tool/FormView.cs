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
using Microsoft.Win32;
using System.Configuration;
using System.Data.SqlClient;
using Tool.Properties;
using System.Threading;
using System.IO.Ports;
using System.IO;

namespace Tool
{
    public partial class FormView : Form
    {
        enum MATERIAL
        {
            IRON,
            INOX
        }

        private SqlConnection sqlConnection;
        private MATERIAL whichMaterial = MATERIAL.INOX;
        private bool isRunAuto = false;
        private bool isRunManual = false;
        private bool isKeypadShown = false;
        private bool isWarningConnect = false;
        Keypad numKey = new Keypad();
        private string dataViewConnectString;
        private Thread backgroundThread;
        private Thread sendDataThread;
        private WarningConnect warningConnect;
        private const int MAX_IMAGE_COUNT = 30;
        private List<ImageObject> listImages = new List<ImageObject>();

        public FormView()
        {
#if DEBUG
            RegisterInStartup(false);
#else
            RegisterInStartup(true);
#endif
            InitializeComponent();

            if(string.Equals(Settings.Default["BUILD_TYPE"].ToString(), Settings.Default["TYPE_MINI"].ToString()))
            {
                // Mini version
                buttonDraw.Visible = false;
                pictureBoxVietnamese.Visible = false;
                pictureBoxEnglish.Visible = false;
                RemoveArbitraryRow(tableLayoutPanel1, 2);
                RemoveArbitraryColumn(tableLayoutPanel4, 1);
                RemoveArbitraryColumn(tableLayoutPanel4, 2);
            }
           

            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;


            pictureBoxShow.Paint += pictureBoxShow_Paint;

            dataViewConnectString = ConfigurationManager.ConnectionStrings["Tool.Properties.Settings.DataGocCanhConnectionString"].ConnectionString;

            ThreadStart ts = new ThreadStart(checkSerialConnection);
            backgroundThread = new Thread(ts);

            ThreadStart thread = new ThreadStart(sendData);
            sendDataThread = new Thread(thread);
            sendDataThread.Start();

            warningConnect = new WarningConnect(this);

            Invalidate();

        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FormSelect formSelect = new FormSelect();
            formSelect.Show();
            formSelect.ChoosenSample += update_ChoosenSample;
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            FormPaint formPaint = new FormPaint();
            formPaint.Show();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSetting formSetting = new FormSetting();
            formSetting.Show();
        }

        private void buttonRunManual_Click(object sender, EventArgs e)
        {
            if (buttonRunManual == null) return;
            if (isRunManual)
            {
                buttonRunManual.Text = "CHẠY TAY\nOFF";
                buttonRunAuto.BackColor = Color.CornflowerBlue;
            }
            else
            {
                buttonRunManual.Text = "CHẠY TAY\nON";
                buttonRunAuto.BackColor = Color.Lavender;
            }
            buttonRunAuto.Enabled = isRunManual;
            isRunManual = !isRunManual;
        }

        private void buttonRunAuto_Click(object sender, EventArgs e)
        {
            if (buttonRunAuto == null) return;
            if (isRunAuto)
            {
                buttonRunAuto.Text = "TỰ ĐỘNG\nOFF";
                buttonRunManual.BackColor = Color.CornflowerBlue; 
            }
            else
            {
                buttonRunAuto.Text = "TỰ ĐỘNG\nON";
                buttonRunManual.BackColor = Color.Lavender;
                if (textBoxCurentNumber.Text == "0") textBoxCurentNumber.Text = textBoxTotalNumber.Text;
            }
            buttonRunManual.Enabled = isRunAuto;
            isRunAuto = !isRunAuto;
        }

        private void pictureBoxPower_Click(object sender, EventArgs e)
        {
            ExitApp exitApp = new ExitApp(this);
            exitApp.Show();
        }

        private void buttonClearCounter_Click(object sender, EventArgs e)
        {
            textBoxCurentNumber.Text = "0";
        }

        private void update_ChoosenSample(string pictureName)
        {
            pictureBoxShow.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(pictureName + "_choosen");
        }

        private void pictureBoxShow_Paint(object sender, PaintEventArgs e)
        {
            int widthImage = pictureBoxShow.Width - pictureBoxShow.Image.Width;
            int heightImage = pictureBoxShow.Height - pictureBoxShow.Image.Height;
            Padding padding = new System.Windows.Forms.Padding();
            padding.Left = widthImage / 2;
            padding.Top = heightImage / 2;
            pictureBoxShow.Padding = padding;
            Invalidate();
        }

        private void buttonChooseMaterial_Click(object sender, EventArgs e)
        {
            FormMaterial formMaterial = new FormMaterial();
            formMaterial.Show();
            formMaterial.ChoosenMaterial += update_ChoosenMaterial;
        }

        private void update_ChoosenMaterial(int material)
        {
            string textChoose = "SẮT";
            switch (material)
            {
                case (int)MATERIAL.IRON:
                    textChoose = "SẮT";
                    break;
                case (int)MATERIAL.INOX:
                    textChoose = "INOX";
                    break;
                default:
                    textChoose = "SẮT";
                    break;
            }
            buttonChooseMaterial.BackColor = Color.LightGreen;
            buttonChooseMaterial.Font = new System.Drawing.Font("Times New Roman", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonChooseMaterial.AutoSize = true;
            buttonChooseMaterial.Location = new System.Drawing.Point(37, 123);
            buttonChooseMaterial.Size = new System.Drawing.Size(83, 38);
            buttonChooseMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            buttonChooseMaterial.Text = textChoose;
        }

        public static void RemoveArbitraryRow(TableLayoutPanel panel, int rowIndex)
        {
            if (rowIndex >= panel.RowCount)
            {
                return;
            }

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                var control = panel.GetControlFromPosition(i, rowIndex);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = rowIndex + 1; i < panel.RowCount; i++)
            {
                for (int j = 0; j < panel.ColumnCount; j++)
                {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        panel.SetRow(control, i - 1);
                    }
                }
            }

            var removeStyle = panel.RowCount - 1;

            if (panel.RowStyles.Count > removeStyle)
                panel.RowStyles.RemoveAt(removeStyle);

            panel.RowCount--;
        }

        public static void RemoveArbitraryColumn(TableLayoutPanel panel, int columnIndex)
        {
            if (columnIndex >= panel.ColumnCount)
            {
                return;
            }

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.RowCount; i++)
            {
                var control = panel.GetControlFromPosition(i, columnIndex);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = columnIndex + 1; i < panel.ColumnCount; i++)
            {
                for (int j = 0; j < panel.RowCount; j++)
                {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        panel.SetColumn(control, i - 1);
                    }
                }
            }

            var removeStyle = panel.ColumnCount - 1;

            if (panel.ColumnStyles.Count > removeStyle)
                panel.ColumnStyles.RemoveAt(removeStyle);

            panel.ColumnCount--;
        }

        private void textBoxG1_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxG1);
            numKey.Show();
        }

        private void textBoxG2_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxG2);
            numKey.Show();
        }

        private void textBoxG3_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxG3);
            numKey.Show();
        }

        private void textBoxG4_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxG4);
            numKey.Show();
        }

        private void textBoxTotalNumber_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTotalNumber);
            numKey.Show();
        }

        private void textBoxL1_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL1);
            numKey.Show();
        }

        private void textBoxL2_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL2);
            numKey.Show();
        }

        private void textBoxL3_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL3);
            numKey.Show();
        }

        private void textBoxL4_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL4);
            numKey.Show();
        }

        private void textBoxL5_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL5);
            numKey.Show();
        }

        private void textBoxL6_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL6);
            numKey.Show();
        }

        private void textBoxL7_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxL7);
            numKey.Show();
        }

        private void RegisterInStartup(bool isChecked)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue(Settings.Default["APPLICATION_NAME"].ToString(), Application.ExecutablePath);
            }
            else
            {
                if (registryKey.GetValueNames().Equals(Settings.Default["APPLICATION_NAME"].ToString()))
                {
                    registryKey.DeleteValue(Settings.Default["APPLICATION_NAME"].ToString());
                }
                
            }
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            parseDataImage();
#if DEBUG
            //Settings.Default["COMPORT"] = "COM?";
            //Settings.Default["FIRST_USED"] = true;
            //Settings.Default.Save();
#endif
            if (Settings.Default.FIRST_USED == true)
            {
                isWarningConnect = true;
                FormFirstUsed formFirstUsed = new FormFirstUsed(Settings.Default.FIRST_USED);
                formFirstUsed.Show();
                formFirstUsed.SetFirstTime += FormFirstUsed_SetFirstTime;
            }
            else
            {
                backgroundThread.Start();
            }

            textBoxL1.Text = Settings.Default.L1.ToString();
            textBoxL2.Text = Settings.Default.L2.ToString();
            textBoxL3.Text = Settings.Default.L3.ToString();
            textBoxL4.Text = Settings.Default.L4.ToString();
            textBoxL5.Text = Settings.Default.L5.ToString();
            textBoxL6.Text = Settings.Default.L6.ToString();
            textBoxL7.Text = Settings.Default.L7.ToString();
            textBoxG1.Text = Settings.Default.G1.ToString();
            textBoxG2.Text = Settings.Default.G2.ToString();
            textBoxG3.Text = Settings.Default.G3.ToString();
            textBoxG4.Text = Settings.Default.G4.ToString();
            textBoxCurentNumber.Text = Settings.Default.SANPHAMCHAY.ToString();
            textBoxTotalNumber.Text = Settings.Default.SANPHAM.ToString();
        }

        private void FormFirstUsed_SetFirstTime(bool isFirstTime)
        {
            Settings.Default["FIRST_USED"] = isFirstTime;
            Settings.Default.Save();
            backgroundThread.Start();
            isWarningConnect = false;
        }

        private async void checkSerialConnection()
        {
            while (true)
            {
                try
                {
                    
                    string[] listPort = SerialPort.GetPortNames();
                    if (listPort.Contains(Settings.Default.COMPORT.ToString()) == true)
                    {
                        if (SerialCommunicator.SerialPort.IsOpen == false)
                        {
                            //    warningConnect.Invoke(() =>
                            //    {
                            //        this.BeginInvoke((Action)delegate ()
                            //        {
                            //            warningConnect.Show();
                            //        });

                            //        while (!SerialCommunicator.SerialPort.IsOpen)
                            //        {
                            //            string[] listPort = SerialPort.GetPortNames();
                            //            if (listPort.Contains(Settings.Default.COMPORT.ToString()) == true)
                            //            {
                            //                if (!SerialCommunicator.SerialPort.IsOpen)
                            //                {
                            //                    SerialCommunicator.SerialPort.Open();
                            //                    if (SerialCommunicator.SerialPort.IsOpen)
                            //                    {
                            //                        this.BeginInvoke((Action)delegate ()
                            //                        {
                            //                            warningConnect.Hide();
                            //                        });
                            //                    }
                            //                }

                            //            }
                            //        }

                            //    });
                            buttonChooseMaterial.Invoke(() => { buttonChooseMaterial.Enabled = false; });
                            buttonSelect.Invoke(() => { buttonSelect.Enabled = false; });
                            buttonDraw.Invoke(() => { buttonDraw.Enabled = false; });
                            buttonClearCounter.Invoke(() => { buttonClearCounter.Enabled = false; });
                            buttonRunManual.Invoke(() => { buttonRunManual.Enabled = false; });
                            buttonRunAuto.Invoke(() => { buttonRunAuto.Enabled = false; });
                            tableLayoutPanel2.Invoke(() => { tableLayoutPanel2.Enabled = false; });
                            tableLayoutPanel3.Invoke(() => { tableLayoutPanel3.Enabled = false; });
                            pictureBoxShow.Invoke(() => { pictureBoxShow.Enabled = false; });
                            SerialCommunicator.SerialPort.PortName = Settings.Default.COMPORT.ToString();
                            SerialCommunicator.SerialPort.Open();
                        }
                        else
                        {
                            buttonChooseMaterial.Invoke(() => { buttonChooseMaterial.Enabled = true; });
                            buttonSelect.Invoke(() => { buttonSelect.Enabled = true; });
                            buttonDraw.Invoke(() => { buttonDraw.Enabled = true; });
                            buttonClearCounter.Invoke(() => { buttonClearCounter.Enabled = true; });
                            buttonRunManual.Invoke(() => { buttonRunManual.Enabled = true; });
                            buttonRunAuto.Invoke(() => { buttonRunAuto.Enabled = true; });
                            tableLayoutPanel2.Invoke(() => { tableLayoutPanel2.Enabled = true; });
                            tableLayoutPanel3.Invoke(() => { tableLayoutPanel3.Enabled = true; });
                            pictureBoxShow.Invoke(() => { pictureBoxShow.Enabled = true; });
                        }
                    }
                    else
                    {
                        try
                        {
                            if (SerialCommunicator.SerialPort.IsOpen == true)
                            {
                                SerialCommunicator.SerialPort.Close();
                                
                            }
                        }
                        catch (ObjectDisposedException e) { Console.WriteLine("Caught: {0}", e.Message); }
                        catch (IOException e) { Console.WriteLine("Caught: {0}", e.Message); }

                        buttonChooseMaterial.Invoke(() => { buttonChooseMaterial.Enabled = false; });
                        buttonSelect.Invoke(() => { buttonSelect.Enabled = false; });
                        buttonDraw.Invoke(() => { buttonDraw.Enabled = false; });
                        buttonClearCounter.Invoke(() => { buttonClearCounter.Enabled = false; });
                        buttonRunManual.Invoke(() => { buttonRunManual.Enabled = false; });
                        buttonRunAuto.Invoke(() => { buttonRunAuto.Enabled = false; });
                        tableLayoutPanel2.Invoke(() => { tableLayoutPanel2.Enabled = false; });
                        tableLayoutPanel3.Invoke(() => { tableLayoutPanel3.Enabled = false; });
                        pictureBoxShow.Invoke(() => { pictureBoxShow.Enabled = false; });
                    }
                }
                catch (IOException e) { Console.WriteLine("Caught: {0}", e.Message); }
                catch (ObjectDisposedException e) { Console.WriteLine("Caught: {0}", e.Message); }
                await Task.Delay(500);
            }
        }

        private void FormView_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundThread.Abort();
        }

        private void textBoxL1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL1.Text == "") Settings.Default["L1"] = "0";
            else Settings.Default["L1"] = textBoxL1.Text;
            Settings.Default.Save();
        }

        private void textBoxL2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL2.Text == "") Settings.Default["L2"] = "0";
            else Settings.Default["L2"] = textBoxL2.Text;
            Settings.Default.Save();
        }

        private void textBoxL3_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL3.Text == "") Settings.Default["L3"] = "0";
            else Settings.Default["L3"] = textBoxL3.Text;
            Settings.Default.Save();
        }

        private void textBoxL4_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL4.Text == "") Settings.Default["L4"] = "0";
            else Settings.Default["L4"] = textBoxL4.Text;
            Settings.Default.Save();
        }

        private void textBoxL5_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL5.Text == "") Settings.Default["L5"] = "0";
            else Settings.Default["L5"] = textBoxL5.Text;
            Settings.Default.Save();
        }

        private void textBoxL6_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL6.Text == "") Settings.Default["L6"] = "0";
            else Settings.Default["6"] = textBoxL6.Text;
            Settings.Default.Save();
        }

        private void textBoxL7_TextChanged(object sender, EventArgs e)
        {
            if (textBoxL7.Text == "") Settings.Default["L7"] = "0";
            else Settings.Default["L7"] = textBoxL7.Text;
            Settings.Default.Save();
        }

        private void textBoxG1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxG1.Text == "") Settings.Default["G1"] = "0";
            else Settings.Default["G1"] = textBoxG1.Text;
            Settings.Default.Save();
        }

        private void textBoxG2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxG2.Text == "") Settings.Default["G2"] = "0";
            else Settings.Default["G2"] = textBoxG2.Text;
            Settings.Default.Save();
        }

        private void textBoxG3_TextChanged(object sender, EventArgs e)
        {
            if (textBoxG3.Text == "") Settings.Default["G3"] = "0";
            else Settings.Default["G3"] = textBoxG3.Text;
            Settings.Default.Save();
        }

        private void textBoxG4_TextChanged(object sender, EventArgs e)
        {
            if (textBoxG4.Text == "") Settings.Default["G4"] = "0";
            else Settings.Default["G4"] = textBoxG4.Text;
            Settings.Default.Save();
        }

        private void textBoxTotalNumber_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTotalNumber.Text == "") Settings.Default["SANPHAM"] = "0";
            else Settings.Default["SANPHAM"] = textBoxTotalNumber.Text;
            Settings.Default.Save();
        }

        private void sendData()
        {
            while (true)
            {
                if (isRunAuto)
                {
                    int start = Int32.Parse(textBoxTotalNumber.Text);
                    if (start > 0)
                    {
                        int count = Int32.Parse(textBoxCurentNumber.Text);
                        if (count > 0)
                        {
                            count--;
                            textBoxCurentNumber.Invoke(() => {
                                textBoxCurentNumber.Text = count.ToString();
                                Settings.Default["SANPHAMCHAY"] = count.ToString();
                                Settings.Default.Save();
                            });
                            Thread.Sleep(1000);

                        }
                    }
                }
            }     
        }

        private void textBoxCurentNumber_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCurentNumber.Text == "") Settings.Default["SANPHAMCHAY"] = "0";
            else Settings.Default["SANPHAMCHAY"] = textBoxCurentNumber.Text;
            Settings.Default.Save();
        }

        private void parseDataImage()
        {
            XmlClass xml = new XmlClass();
            
            if (!xml.Exist())
            {
                for (int i = 1; i <= MAX_IMAGE_COUNT; i++)
                {
                    xml.Data.ListImages.Add(new ImageObject(i, "sample_" + i.ToString(), new ImageParameters(
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true),
                        new ImageValues(0, true))));
                }
                xml.Create();
            }

            if (xml.Exist())
            {

            }


        }
    }
}
