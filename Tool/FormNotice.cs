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
    public partial class FormNotice : Form
    {
        public FormNotice(string _data)
        {
            InitializeComponent();
            label1.Text = _data;
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;


            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.okP;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.okN;
            this.Close();
        }
    }
}
