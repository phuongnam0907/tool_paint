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
    public partial class WarningConnect : Form
    {
        private FormView _formView;
        public WarningConnect(FormView formView)
        {
            InitializeComponent();
            this._formView = formView;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ExitApp exitApp = new ExitApp(this._formView);
            exitApp.Show();
        }
    }
}
