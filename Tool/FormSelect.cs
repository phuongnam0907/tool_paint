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
    public partial class FormSelect : Form
    {
        List<string> stringList = new List<string>();
        List<PictureBox> pictureBoxList = new List<PictureBox>();

        public FormSelect()
        {
            InitializeComponent();
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            initFirstTime();
        }

        private void initFirstTime()
        {
            
            for (int i = 1; i <= 30; i++)
            {
                string temp = "sample_" + i;
                stringList.Add(temp);
            }

            
            for (int i = 0; i < 20; i++)
            {
                PictureBox picture = new PictureBox();
                picture.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                picture.AutoSize = true;
                picture.Image = GetImageResource(stringList[i]);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.DoubleClick += tableLayoutPanel1_Click;
                pictureBoxList.Add(picture);
            }
            int rowCount = 0;
            int columnCount = 0;
            foreach (PictureBox item in pictureBoxList) {
                tableLayoutPanel1.Controls.Add(item, columnCount, rowCount);
                if (rowCount > 4)
                {
                    columnCount++;
                    rowCount = 0;
                }
                else
                {
                    rowCount++;
                }
            }            
        }

        private Bitmap GetImageResource(string filename)
        {
            string resourceName = filename;
            return (Bitmap)Properties.Resources.ResourceManager.GetObject(resourceName);
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            //label2.Text = "Cell chosen: (" +
            //         tableLayoutPanel1.GetRow((Panel)sender) + ", " +
            //         tableLayoutPanel1.GetColumn((Panel)sender) + ")";
            //MessageBox.Show("Cell chosen: (" +
            //         tableLayoutPanel1.GetRow((Panel)sender) + ", " +
            //         tableLayoutPanel1.GetColumn((Panel)sender) + ")");
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
