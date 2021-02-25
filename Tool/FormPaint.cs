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
            public Coordinates(int d, int x, int y, int z)
            {
                D = d;
                X = x;
                Y = y;
                Z = z;
            }

            public int D { get; }
            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public override string ToString() => $"( {D}, {X}, {Y}, {Z})";
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
        int current_degree = 0;

        public FormPaint()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

        }

        private void draw(int mode, int sizeBrush)
        {
            int degree = -90;
            mBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (mGraphics = Graphics.FromImage(mBitmap))
            {
                if (mode == 0)
                {

                    int diaM = int.Parse(textLength.Text) / 5;
                    int cX = pictureBox.Width / 2;
                    int cY = pictureBox.Height / 2;
                    int tX = 0;
                    int tY = 0;
                    foreach (Coordinates element in mListCoordinates)
                    {
                        mGraphics.DrawArc(new Pen(Color.Red, sizeBrush), cX - diaM - 5, cY - diaM, diaM, diaM, degree, element.D);
                        degree += element.D;
                        tX = cX;
                        tY = cY;
                        cX = element.X;
                        cY = element.Y;
                        mGraphics.DrawLine(new Pen(Color.Red, sizeBrush), tX, tY, cX, cY);
                    }
                    mGraphics.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("arrow"), cX - 22, cY - 16, 44, 32);
                    Invalidate();
                }
                else if (mode == 2)
                {
                    mGraphics.DrawArc(new Pen(Color.Red, sizeBrush), );
                }
            }
            pictureBox.Image = mBitmap;
            Invalidate();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            int diameter = 1;
            int angle = 0;
            int length = 0;
            double length_radius = 0;
            int radius = 0;
            int degree = 0;
            if (mListCoordinates.Count == 0)
            {
                start_x = pictureBox.Width / 2;
                start_y = pictureBox.Height / 2;
                mListCoordinates.Add(new Coordinates(0, start_x, start_y, 0));
            }
            if (tabControl1.SelectedIndex == 0)
            {
                if (textLength != null && !string.IsNullOrWhiteSpace(textLength.Text) && textBoxDegreeOut != null && !string.IsNullOrWhiteSpace(textBoxDegreeOut.Text))
                {
                    diameter = int.Parse(textBoxDM.Text);
                    angle = (int.Parse(textBoxDegreeOut.Text)) % 360;
                    length = int.Parse(textLength.Text) * 2;

                    current_degree += angle;
                    if (current_degree >= 360) current_degree = current_degree % 360;

                    int end_x = (int)(start_x + Math.Cos(current_degree * Math.PI / 180) * length);
                    int end_y = (int)(start_y + Math.Sin(current_degree * Math.PI / 180) * length);

                    if (!(start_x == end_x && start_y == end_y))
                    {
                        mListCoordinates.Add(new Coordinates(angle, end_x, end_y, 0));
                        mListData.Add(new Data(angle, 0, 0, length));
                    }

                    start_x = end_x;
                    start_y = end_y;
                    Invalidate();
                    draw(0, diameter);
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (textBoxRadius != null && !string.IsNullOrWhiteSpace(textBoxRadius.Text) && textBoxDegree != null && !string.IsNullOrWhiteSpace(textBoxDegree.Text))
                {
                    diameter = int.Parse(textBoxDM1.Text);
                    degree = (int.Parse(textBoxDegree.Text));
                    radius = int.Parse(textBoxRadius.Text);
                    length_radius = (Math.PI * radius * degree) / 180;

                    MessageBox.Show(length_radius.ToString());
                    current_degree += degree;
                    if (current_degree >= 360) current_degree = current_degree % 360;

                    //int end_x = (int)(start_x + Math.Cos(current_degree * Math.PI / 180) * length);
                    //int end_y = (int)(start_y + Math.Sin(current_degree * Math.PI / 180) * length);

                    //    mListCoordinates.Add(new Coordinates(angle, end_x, end_y, 0));
                    //    mListData.Add(new Data(angle, 0, 0, length));

                    //start_x = end_x;
                    //start_y = end_y;
                    Invalidate();
                    draw(0, diameter);
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (textBoxRadiusCircle != null && !string.IsNullOrWhiteSpace(textBoxRadiusCircle.Text) && textBoxNumberCircle != null && !string.IsNullOrWhiteSpace(textBoxNumberCircle.Text))
                {
                    int r = int.Parse(textBoxRadiusCircle.Text);
                    int end_x = (int)(start_x + Math.Cos(current_degree * Math.PI / 180) * r);
                    int end_y = (int)(start_y + Math.Sin(current_degree * Math.PI / 180) * r);

                    //mListCoordinates.Add(new Coordinates(angle, end_x, end_y, 0));
                    //mListData.Add(new Data(angle, 0, 0, length));

                    Invalidate();
                    draw(2, diameter);
                }
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
                current_degree = mListCoordinates.ElementAt(mListCoordinates.Count - 1).D;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //string str = "Coordinates Count: " + mListCoordinates.Count + "\n";
            //foreach (Coordinates item in mListCoordinates)
            //{
            //    str += "D = " + item.D + "X = " + item.X + " | Y = " + item.Y + "\n";
            //}
            //str += "Step Count: " + mListData.Count + "\n";
            //foreach (Data item in mListData)
            //{
            //    str += "Alpha = " + item.Alpha + " | Length = " + item.Length + "\n";
            //}
            //MessageBox.Show(str);
            MessageBox.Show(tabControl1.SelectedIndex.ToString());
        }

    }
}
