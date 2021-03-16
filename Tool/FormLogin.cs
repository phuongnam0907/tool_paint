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
    public partial class FormLogin : Form
    {
        public delegate void Login(int permissionUser);
        public event Login LoginEvent;
        Keypad numKey = new Keypad();

        public FormLogin()
        {
            InitializeComponent();
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numKey.Close();
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            int permissionUser = 0;
            string username = textBoxLoginUser.Text.Trim();
            string password = textBoxLoginPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || (!username.Equals(password)))
            {
                new FormNotice("Sai tài khoản hoặc mật khẩu").Show();
                return;
            }

            if (username.Equals("1") && password.Equals("1")) permissionUser = 1;
            else if (username.Equals("2") && password.Equals("2")) permissionUser = 2;
            else if (username.Equals("3") && password.Equals("3")) permissionUser = 3;
            else if (username.Equals("4") && password.Equals("4")) permissionUser = 4;
            else permissionUser = 0;

            new FormNotice("Đăng nhập thành công!").Show();

            if (LoginEvent != null)
            {
                LoginEvent(permissionUser);
                numKey.Close();
                this.Close();
            }
            else
            {
                numKey.Close();
                this.Close();
            }
        }

        private void textBoxLoginUser_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxLoginUser);
            numKey.Show();
        }

        private void textBoxLoginPass_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxLoginPass);
            numKey.Show();
        }
    }
}
