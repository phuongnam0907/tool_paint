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
    public partial class Keypad : Form
    {
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private const int MaxLength = 6;

        private TextBox textBox;

        public Keypad()
        {
            InitializeComponent();
        }

        public void setTextBox(TextBox tb)
        {
            textBox = tb;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Keypad_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);

        }

        private void Keypad_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Keypad_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "1";
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "2";
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "3";
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "4";
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "5";
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "6";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "7";
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "8";
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "9";
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < MaxLength)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = "";
                }
                textBox.Text += "0";
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "0")
            {
                if (!textBox.Text.Contains("-"))
                {
                    textBox.Text = "-" + textBox.Text;
                }
                else
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                }
            } 
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (textBox.Text == "") textBox.Text = "0";
            if (!textBox.Text.Contains("."))
            {
                textBox.Text += ".";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
