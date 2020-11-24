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
    public partial class FormSetting : Form
    {
        private int permissionUser = 0;

        public FormSetting()
        {
            InitializeComponent();

            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonLoginFromSetting_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            formLogin.LoginEvent += setPermission;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setPermission(int level)
        {
            permissionUser = level;
            string lv;
            switch (level)
            {
                case 1:
                    lv = "1";
                    break;
                case 2:
                    lv = "2";
                    break;
                case 3:
                    lv = "3";
                    break;
                case 4:
                    lv = "4";
                    break;
                case 0:
                default:
                    lv = "0";
                    break;
            }
            labelStatusUser.Text = "Người Dùng\n Mức " + lv;
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
