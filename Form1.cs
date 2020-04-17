using Numpy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3 {
    public partial class Form1: Form {
        //------UI Elements----
        Graphics g;
        bool moving = false;
        Pen pen;
        int x = -1;
        int y = -1;
        Bitmap image;
        Graphics gr;
        Bitmap bmp;
        List<int> imgValues = new List<int>(); //ARGB 32-bit values
       //---------------------


        public Form1() {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, (float) 1.5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            bmp = new Bitmap(28, 28);

            gr = Graphics.FromImage(bmp);
            Network net = new Network(new int[] { 784, 30, 10});

            Loader ld = new Loader();
            List<List<Tuple<NDarray, NDarray>>> all_data = ld.load_data("../../pickledata.pkl.npy");

            net.SGD(all_data[0], 30, 10, 10, 3.0, all_data[2]);

           
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            moving = false;
            x = -1;
            y = -1;
            panel1.Cursor = Cursors.Default;

            imgValues.Clear();
            for (int i = 0; i < bmp.Width; i++) {
                for (int j = 0; j < bmp.Height; j++) {
                    int pixel = bmp.GetPixel(i, j).ToArgb();
                    imgValues.Add(pixel);
                }
            }

            panel1.Image = bmp;
            panel1.Image.Save("./temp.bmp");
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (moving && x != -1 && y != -1) {
                g.DrawLine(pen, new Point(x, y), e.Location);
                gr.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            moving = true;
            x = e.X;
            y = e.Y;
            panel1.Cursor = Cursors.Cross;
        }

        private void button1_Click(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e) {
            label1.Text = "The number is: -1";
        }

        
    }



   



}
