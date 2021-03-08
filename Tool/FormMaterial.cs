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
    public partial class FormMaterial : Form
    {
        enum MATERIAL
        {
            IRON,
            INOX
        }

        public delegate void Material(int material);
        public event Material ChoosenMaterial;

        public FormMaterial()
        {
            InitializeComponent();
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonIron_Click(object sender, EventArgs e)
        {
            ChoosenMaterial((int)MATERIAL.IRON);
            this.Close();
        }

        private void buttonInox_Click(object sender, EventArgs e)
        {
            ChoosenMaterial((int)MATERIAL.INOX);
            this.Close();
        }
    }
}
