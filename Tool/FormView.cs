using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class FormView : Form
    {
        private bool isRunAuto = false;
        private bool isRunManual = false;

        public FormView()
        {
            InitializeComponent();

            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            pictureBoxShow.Paint += pictureBoxShow_Paint;

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
            }
            else
            {
                buttonRunManual.Text = "CHẠY TAY\nON";
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
            }
            else
            {
                buttonRunAuto.Text = "TỰ ĐỘNG\nON";
            }
            buttonRunManual.Enabled = isRunAuto;
            isRunAuto = !isRunAuto;
        }

        private void pictureBoxPower_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
