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
        Keypad numKey = new Keypad();
        private string dataViewConnectString;

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
                textBoxCurentNumber.Text = "20";
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

        }
    }
}
