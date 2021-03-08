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
    public partial class ExitApp : Form
    {
        FormView formView;
        public ExitApp(FormView form)
        {
            
            InitializeComponent();
            this.formView = form;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.formView.Close();
        }
    }
}
