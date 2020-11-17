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
    public partial class FormPaint : Form
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
        List<Coordinates> mListCoordinates = new List<Coordinates>();
        Bitmap mBitmap;
        Graphics mGraphics;

        public FormPaint()
        {
            InitializeComponent();

            _x = pictureBox.Width / 2;
            _y = pictureBox.Height / 2;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            mBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (mGraphics = Graphics.FromImage(mBitmap))
            {
                int cX = _x;
                int cY = _y;
                int tX = 0;
                int tY = 0;
                mListCoordinates.Add(new Coordinates(_x, _y, 0));
                foreach (Coordinates element in mListCoordinates)
                {
                    tX = cX;
                    tY = cY;
                    cX = element.X;
                    cY = element.Y;
                    mGraphics.DrawLine(new Pen(Color.Red), tX, tY, cX, cY);
                }
                mGraphics.FillRectangle(Brushes.BlueViolet, _x, _y, 10, 10);

            }
            pictureBox.Image = mBitmap;
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (textLength != null && !string.IsNullOrWhiteSpace(textLength.Text))
            {
                _x += int.Parse(textLength.Text);
                _y += int.Parse(textLength.Text);
                mListCoordinates.Add(new Coordinates(_x, _y, 0));
                Invalidate();
            }
        }
    }
}
