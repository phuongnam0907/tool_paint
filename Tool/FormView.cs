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

namespace Tool
{
    public partial class FormView : Form
    {
        enum MATERIAL
        {
            IRON,
            INOX
        }

        private MATERIAL whichMaterial = MATERIAL.INOX;
        private bool isRunAuto = false;
        private bool isRunManual = false;

        public FormView()
        {
            InitializeComponent();

            // Mini version
            buttonDraw.Visible = false;
            RemoveArbitraryRow(tableLayoutPanel1, 2);

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
    }
}
