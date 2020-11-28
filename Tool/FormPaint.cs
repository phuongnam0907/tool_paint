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
        //===========================================================//
        private struct Data
        {
            public Data(int alpha, int beta, int delta, int length)
            {
                Alpha = alpha;
                Beta = beta;
                Delta = delta;
                Length = length;
            }

            public int Alpha { get; }
            public int Beta { get; }
            public int Delta { get; }
            public int Length { get; }

            public override string ToString() => $"({Alpha}, {Beta}, {Delta}, {Length})";
        }
        //===========================================================//
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
        //===========================================================//
        private int start_x;
        private int start_y;
        private int end_x;
        private int end_y;
        List<Coordinates> mListCoordinates = new List<Coordinates>();
        List<Data> mListData = new List<Data>();
        Bitmap mBitmap;
        Graphics mGraphics;

        public FormPaint()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

        }

        private void drawLine(int sizeBrush)
        {
            mBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (mGraphics = Graphics.FromImage(mBitmap))
            {
                int cX = pictureBox.Width / 2;
                int cY = pictureBox.Height / 2;
                int tX = 0;
                int tY = 0;
                foreach (Coordinates element in mListCoordinates)
                {
                    tX = cX;
                    tY = cY;
                    cX = element.X;
                    cY = element.Y;
                    mGraphics.DrawLine(new Pen(Color.Red, sizeBrush), tX, tY, cX, cY);
                }
                mGraphics.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("arrow"), cX-22, cY-16, 44, 32);
                Invalidate();
            }
            pictureBox.Image = mBitmap;
            Invalidate();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (mListCoordinates.Count == 0)
            {
                start_x = pictureBox.Width / 2;
                start_y = pictureBox.Height / 2;
                mListCoordinates.Add(new Coordinates(start_x, start_y, 0));
            }
            int diameter = 1;
            int angle = 0;
            int length = 0;
            if (textLength != null && !string.IsNullOrWhiteSpace(textLength.Text) && textBoxAngle != null && !string.IsNullOrWhiteSpace(textBoxAngle.Text))
            {
                diameter = int.Parse(textBoxDM.Text);
                angle = (int.Parse(textBoxAngle.Text)) % 360;
                length = int.Parse(textLength.Text) * 2;

                int end_x = (int)(start_x + Math.Cos(angle * Math.PI / 180) * length);
                int end_y = (int)(start_y + Math.Sin(angle * Math.PI / 180) * length);

                if (!(start_x == end_x && start_y == end_y))
                {
                    mListCoordinates.Add(new Coordinates(end_x, end_y, 0));
                    mListData.Add(new Data(angle, 0, 0, length));
                }

                start_x = end_x;
                start_y = end_y;
                Invalidate();
                drawLine(diameter);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (mListCoordinates.Count > 1)
            {
                mListCoordinates.RemoveAt(mListCoordinates.Count - 1);
                mListData.RemoveAt(mListData.Count - 1);
                drawLine(int.Parse(textBoxDM.Text));
                start_x = mListCoordinates.ElementAt(mListCoordinates.Count - 1).X;
                start_y = mListCoordinates.ElementAt(mListCoordinates.Count - 1).Y;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string str = "Coordinates Count: " + mListCoordinates.Count + "\n";
            foreach (Coordinates item in mListCoordinates)
            {
                str += "X = " + item.X + " | Y = " + item.Y + "\n";
            }
            str += "Step Count: " + mListData.Count + "\n";
            foreach (Data item in mListData)
            {
                str += "Alpha = " + item.Alpha + " | Length = " + item.Length + "\n";
            }
            MessageBox.Show(str);
        }
    }
}
