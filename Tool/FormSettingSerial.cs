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
    public partial class FormSettingSerial : Form
    {
        private int userPermission = 0;
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
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

        }
    }
}
