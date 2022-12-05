using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ellipse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.MouseWheel += new MouseEventHandler(this_MouseWheel);
            SetSize();
        }
        private void SetSize()
        {
            Rectangle rec = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rec.Width, rec.Height);
            Graf = Graphics.FromImage(map);
        }
        Graphics Graf;
        Bitmap map = new Bitmap(500, 500);
        double ab;
        double ty;
        int rad = 100;
        float Gr;

        private Matrix RotateAroundPoint(float angle, Point center, bool u)
        {
            // Переведите точку в начало.;
            if (u)
            {
                Gr = angle;
            }
            else
                Gr = Gr + angle;


            if (Gr > 180)
            {
                Gr = -180;
            }
            if (Gr < -180)
            {
                Gr = 180;
            }
            Matrix result = new Matrix();
            result.RotateAt(Gr, center);
            return result;
        }

        int s = 270;
        float ratio = 1;
        int tu = 360;
        public void Polygons(int y, int radius)
        {

            int n = tu;
            ab = 360 / n;
            int f = 60;
            ty = n / 1;
            double a = 0;
            PointF[] curvePoints = new PointF[n + 2];
            PointF points1 = new PointF(250, 250);
            curvePoints[n + 1] = points1;
            for (int i = 0; i <= n; i++)
            {
                PointF points = new PointF((float)((pictureBox1.Size.Width / 2) + radius * (Math.Cos((i + s) * ab * Math.PI / 180f) * (ratio))), (float)((pictureBox1.Size.Height / 2) + radius * Math.Sin((i + s) * ab * Math.PI / 180f)));
                //  MessageBox.Show(points.ToString());
                a += ab;
                curvePoints[i] = points;
            }
            Graf.FillPolygon(Brushes.Cyan, curvePoints);
            Graf.DrawPolygon(Pens.Black, curvePoints);

            pictureBox1.Image = map;
            //MessageBox.Show(" ");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graf.Clear(Color.Silver);
            Graf.Transform = RotateAroundPoint(t, center, false);
            Polygons(t, rad);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Graf.Clear(Color.Silver);
            for (int i = 0; true; i++)
            {
                if (i == 360)
                {
                    i = 0;
                }
                // Polygons(i,10);
            }
        }
        int asf = 100;
        bool Mouses;
        bool MousesD;
        int kx;
        int ky;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MousesD = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousesD)
                if (kx < e.X)
                {
                    Polygons(kx, rad);
                }
        }
        void this_MouseWheel(object sender, MouseEventArgs e)
        {
            Graf.Clear(Color.Silver);
            if (Mouses)
                if (e.Delta > 0)
                {
                    rad++;
                    if (rad > 0 && rad < 201)
                    {
                        Polygons(t, rad);
                        trackBar1.Value = rad;
                    }
                    else
                        rad = 200;
                }
                else
                {
                    rad--;
                    if (rad < 201 && rad > 0)
                    {
                        trackBar1.Value = rad;
                    }
                    else rad = 1;
                }

            Polygons(t, rad);

        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        int t = 0;
        int h;
        Point center = new Point(250, 250);
        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            Mouses = true;
            if (MousesD)
            {
                Graf.Clear(Color.Silver);
                t = e.X - kx;
                h = Int32.Parse(Gr.ToString());
                Graf.Transform = RotateAroundPoint(t, center, false);
                Polygons(t, rad);
                trackBar2.Value = h;
                textBox2.Text = h.ToString();
            }
            kx = e.X;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Mouses = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            t = 0;
            MousesD = false;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            Graf.Clear(Color.Silver);
            Graf.Transform = RotateAroundPoint(trackBar2.Value, center, true);
            Polygons(t, rad);
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Graf.Clear(Color.Silver);
            Polygons(t, trackBar1.Value);
            rad = trackBar1.Value;
            textBox1.Text = rad.ToString();
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            t = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Graf.Transform = RotateAroundPoint(0, center, true);
            try
            {
                Graf.Clear(Color.Silver);
                Gr = Int32.Parse(textBox2.Text);
                trackBar2.Value = Int32.Parse(textBox2.Text);
                Graf.Transform = RotateAroundPoint(Gr, center, true);
                Polygons(t, rad);
            }
            catch (System.FormatException)
            {
                if (textBox2.Text != "" && textBox2.Text != "-")
                    MessageBox.Show("Только цифры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Graf.Clear(Color.Silver);
                rad = Int32.Parse(textBox1.Text);
                Polygons(t, rad);
            }
            catch (System.FormatException)
            {
                if (textBox1.Text != "" && textBox1.Text != "-")
                    MessageBox.Show("Только цифры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                    if (!hovers)
                        textBox1.Text = "0";
                }
            }
        }
        bool hovers;
        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            hovers = true;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            hovers = false;
            if (textBox1.Text == "")
                textBox1.Text = "0";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Polygons(t, rad);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ratio = 1;
            try
            {
                ratio = Convert.ToSingle(textBox3.Text) / 10;
                Graf.Clear(Color.Silver);
                Polygons(t, rad);
            }
            catch (System.FormatException)
            {
                if (textBox3.Text != "" && textBox3.Text != ".")
                    MessageBox.Show("Только цифры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                    if (!hovers)
                        textBox3.Text = "0";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case ("Hexagon"):
                    tu = 6;
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                    break;
                case ("Circle"):
                    tu = 360;
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                    break;
                case ("Triangle"):
                    tu = 3;
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                    break;
                case ("Square"):
                    tu = 4;
                    Graf.Clear(Color.Silver);
                    Polygons(t, rad);
                    break;
            }
        }

        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            hovers = true;
        }

        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
            hovers = false;
            if (textBox1.Text == "")
                textBox1.Text = "0";
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Graf.Clear(Color.Silver);
            Polygons(t, trackBar1.Value);
            rad = trackBar1.Value;
            textBox1.Text = rad.ToString();
        }
    }
}
