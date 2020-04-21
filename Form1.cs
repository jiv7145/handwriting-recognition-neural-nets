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
        string inputFile;
        bool learned = false;
        Network net;

        public Form1() {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, (float) 1.5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            bmp = new Bitmap(28, 28);
            gr = Graphics.FromImage(bmp);
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            moving = false;
            x = -1;
            y = -1;
            panel1.Cursor = Cursors.Default;

            //imgValues.Clear();
            //for (int i = 0; i < bmp.Width; i++) {
            //    for (int j = 0; j < bmp.Height; j++) {
            //        int pixel = bmp.GetPixel(i, j).ToArgb();
            //        imgValues.Add(pixel);
            //    }
            //}

            //panel1.Image = bmp;
            //panel1.Image.Save("./temp.bmp");
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
            panel1.Image = new Bitmap(28, 28);
            panel1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int epochs = 1;
            if (Util.isNum(textbox_epochs.Text)) {
                epochs = Int32.Parse(textbox_epochs.Text);
            }

            net = new Network(new int[] { 784, 30, 10 }, this);
            Loader ld = new Loader();

            updateTextBox("Learning...");
            List<List<NDarray>> all_data = ld.load_data("./pickledata.pkl.npy");
            net.SGD(all_data[0], epochs, 10, 3.0, all_data[2]);
        }

        public void updateTextBox(string text) {

            richTextBox1.AppendText($"{text} \n");
            richTextBox1.Refresh();
        }

        //select learning file
        private void button_selectlf_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Numpy (*.npy)|*.npy";
            if (open.ShowDialog() == DialogResult.OK)
            {
                inputFile = open.FileName;
            }
        }

   

        public void setLearned() {            
            learned = true;
        }

     
        private void button_chkFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {              
                pictureBox1.BackgroundImage = Image.FromFile(open.FileName);
                Bitmap img = new Bitmap(open.FileName);
                Bitmap test = new Bitmap(img, new Size(28, 28));
                Console.WriteLine(test.PixelFormat);

                byte[] rgbValues = bmToByte(test);
                double[] rgbDouble = normalize(rgbValues);
                string result = net.evaluate(rgbDouble);
                Console.WriteLine("Number is: " + result);
                label1.Text = "Number is: " + result;
                label1.Refresh();
            }
        }

        private double[] normalize(byte[] rgb)
        {
            double[] rgbDouble = new double[784];
            if (rgb.Length == rgbDouble.Length)
            {
                Console.WriteLine("Same Length");
            }
            int count = 0;
            for (int i = 0; i < rgb.Length; i += 4)
            {
                rgbDouble[count++] = rgb[i] / 255;
            }
            return rgbDouble;
        }

        private byte[] bmToByte(Bitmap bm)
        {

            Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
            BitmapData bmpData =
            bm.LockBits(rect, ImageLockMode.ReadWrite, bm.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bm.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            bm.UnlockBits(bmpData);

            return rgbValues;
        }

        private void button_chknum_Click(object sender, EventArgs e)
        {
            if (learned)
            {
                Bitmap test = new Bitmap(bmp, new Size(28, 28));
                Console.WriteLine(test.PixelFormat);

                byte[] rgbValues = bmToByte(test);
                double[] rgbDouble = normalize(rgbValues);
                string result = net.evaluate(rgbDouble);
                Console.WriteLine("Number is: " + result);
                label1.Text = "Number is: " + result;
                label1.Refresh();
            }
            else
            {
                MessageBox.Show("Please learn first");
            }
        }
    }







}
