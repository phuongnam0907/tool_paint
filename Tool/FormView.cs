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
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FormSelect formSelect = new FormSelect();
            formSelect.Show();
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
    }

}
