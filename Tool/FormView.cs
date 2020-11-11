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
        private struct Coordinates
        {
            public Coordinates(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public override string ToString() => $"({X}, {Y}, {Z})";
        }

        private int _x;
        private int _y;
        List<Coordinates> list = new List<Coordinates>();
        Bitmap bmp;
        Graphics g;

        public FormView()
        {
            InitializeComponent();

            _x = pictureBox.Width / 2;
            _y = pictureBox.Height / 2;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (g = Graphics.FromImage(bmp))
            {
                int cX = _x;
                int cY = _y;
                int tX = 0;
                int tY = 0;
                list.Add(new Coordinates(_x, _y, 0));
                foreach (Coordinates element in list)
                {
                    tX = cX;
                    tY = cY;
                    cX = element.X;
                    cY = element.Y;
                    g.DrawLine(new Pen(Color.Red), tX, tY, cX, cY);
                }
                g.FillRectangle(Brushes.BlueViolet, _x, _y, 10, 10);

            }
            pictureBox.Image = bmp;
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            _x += int.Parse(textBox1.Text);
            _y += 2;
            list.Add(new Coordinates(_x, _y, 0));
            Invalidate();
        }
    }
}
