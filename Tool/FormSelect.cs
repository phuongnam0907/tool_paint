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
        String choosenImage = null;
        public delegate void Sample(string pictureName);
        public event Sample ChoosenSample;

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

            
            for (int i = 0; i < 30; i++)
            {
                int index = i;
                PictureBox picture = new PictureBox();
                picture.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                picture.AutoSize = true;
                picture.Image = GetImageResource(stringList[index]);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.Click += delegate (object sender, EventArgs e) { clickImage(sender, e, stringList[index]); };
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

        private void clickImage(object sender, EventArgs e, string message)
        {
            pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(message);
            choosenImage = message;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            if (choosenImage != null)
            {
                ChoosenSample(choosenImage);
                this.Close();
            }
        }
    }
}
