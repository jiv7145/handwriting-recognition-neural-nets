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
        Graphics g;
        bool moving = false;
        Pen pen;
        int x = -1;
        int y = -1;
        Bitmap image;

        public Form1() {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 1);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            
            Loader ld = new Loader();
            ld.load_data("../../pickledata.pkl.npy");

           
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            moving = false;
            x = -1;
            y = -1;
            panel1.Cursor = Cursors.Default;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (moving && x != -1 && y != -1) {
                g.DrawLine(pen, new Point(x, y), e.Location);
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
