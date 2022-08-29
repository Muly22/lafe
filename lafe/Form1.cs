using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lafelib;

namespace lafe
{
    public partial class Form1 : Form
    {
        Graphics g;
        int resolution;
        GameEngin gameEngin;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Print();
        }

        private void startb_Click(object sender, EventArgs e)
        {
            Start_cubs();
        }
        void Start_cubs()
        {
            if (timer1.Enabled)
                return;
            numResolution.Enabled = false;
            numericUpDown1.Enabled = false;
            resolution = (int)numResolution.Value;
            gameEngin = new GameEngin
            (
                rows: pictureBox1.Height / resolution,
                cols: pictureBox1.Width / resolution,
                (int)numericUpDown1.Value
            );
            Text = $"поколение {gameEngin.current}";
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }
        void Print()
        {
            g.Clear(Color.Black);
            var field = gameEngin.GetCurr();
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y])
                        g.FillRectangle(Brushes.Green, x * resolution, y * resolution, resolution - 1, resolution - 1);
                }
            }
            pictureBox1.Refresh();
            Text = $"поколение {gameEngin.current}";
            gameEngin.Print();
        }
        private void stopb_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }
            timer1.Stop();
            numResolution.Enabled = true;
            numericUpDown1.Enabled = true;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngin.AddCall(x, y);
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngin.RemoveCall(x, y);
            }
        }

    }
}