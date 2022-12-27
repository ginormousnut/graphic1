using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap myBitmap;
        Graphics gr, gScreen;       //объявляем объект - графику, на которой будем рисовать
        Pen p, p1, p2, basketball, p3;      //p-мяч, р1 - солнце, р2-земля, p3-кольцо
        SolidBrush fon;    //трава
        SolidBrush fig;  //внутренности мяча
        LinearGradientBrush sky;
        SolidBrush sun, basketball1;
        HatchBrush ground1;

        int timerCounter = 0; //счётчик для таймера(для прыгающего мяча)
        int flag = 0;


        int rad;          // переменная для хранения радиуса мяча
        Random rand;      // объект, для получения случайных чисел

        Point[] ground, shield, post;
        public Form1()
        {
            InitializeComponent();
        }
        void DrawCircle(int x, int y)
        {

            gr.FillEllipse(fig, x, y, rad, rad);
            gr.DrawEllipse(p, x, y, rad, rad);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            gScreen= CreateGraphics();
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(myBitmap);
            MyDraw();
            timer1.Enabled = true;  //включим в работу наш таймер
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MyDraw();
        }
        public void MyDraw()
        {
            p = new Pen(Color.BlueViolet);
            fig = new SolidBrush(Color.BlueViolet);

            rad = 40;
            rand = new Random();

            gr.Clear(Color.Black);
            fon = new SolidBrush(Color.Blue);
            sky = new LinearGradientBrush(new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height - 100), Color.Purple, Color.Yellow, LinearGradientMode.Vertical);
            gr.FillRectangle(sky, 0, 0, pictureBox1.Width, pictureBox1.Height - 100); // закрасим черным 
            fon = new SolidBrush(Color.Green);
            gr.FillRectangle(fon, 0, pictureBox1.Height - 100, pictureBox1.Width, pictureBox1.Height - 50);                                                                    // нашу область рисования

            p1 = new Pen(Color.DarkRed);
            sun = new SolidBrush(Color.DarkRed);
            gr.DrawPie(p1, 50, pictureBox1.Height - 150, 100, 100, 180, 180);
            gr.FillPie(sun, 50, pictureBox1.Height - 150, 100, 100, 180, 180);

            Point[] ground = { new Point(pictureBox1.Width /2, pictureBox1.Height - 100),
                new Point(pictureBox1.Width , pictureBox1.Height - 100),
                new Point(pictureBox1.Width , pictureBox1.Height),
                 new Point(pictureBox1.Width /2-50, pictureBox1.Height),
                 };
            gr.DrawLines(p, ground);
            ground1 = new HatchBrush(HatchStyle.DottedDiamond, Color.DarkGray, Color.Gray);
            gr.FillPolygon(ground1, ground);

            Point[] post = { new Point(pictureBox1.Width -100, pictureBox1.Height - 80),
                new Point(pictureBox1.Width-100 , pictureBox1.Height - 300),
                new Point(pictureBox1.Width-90 , pictureBox1.Height-290),
                 new Point(pictureBox1.Width -90, pictureBox1.Height-70),
                 };
            basketball = new Pen(Color.RosyBrown);
            gr.DrawLines(basketball, post);
            basketball1 = new SolidBrush(Color.RosyBrown);
            gr.FillPolygon(basketball1, post);

            Point[] shield = { new Point(pictureBox1.Width-130, pictureBox1.Height - 330),
                new Point(pictureBox1.Width-130 , pictureBox1.Height - 390),
                new Point(pictureBox1.Width-60 , pictureBox1.Height-320),
                 new Point(pictureBox1.Width -60, pictureBox1.Height-260),
                 };
            gr.DrawLines(basketball, shield);
            gr.FillPolygon(basketball1, shield);

            p3 = new Pen(Color.Black, 3);
            gr.DrawEllipse(p3, pictureBox1.Width - 150, pictureBox1.Height - 320, 60, 20);


            //sun = new HatchBrush(HatchStyle.DottedDiamond, Color.DarkRed, Color.Red);

            int x, y;
            x = pictureBox1.Width / 3 * 2;

            if ((pictureBox1.Height - 100 - timerCounter * 10 >= pictureBox1.Height - 200) && (flag == 0))
            {
                timerCounter++;
                if ((pictureBox1.Height - 100 - timerCounter * 10 == pictureBox1.Height - 200))
                    flag = 1;
            }
            if ((pictureBox1.Height - 100 - timerCounter * 10 <= pictureBox1.Height - 100) && (flag == 1))
            {
                timerCounter--;
                if ((pictureBox1.Height - 100 - timerCounter * 10 == pictureBox1.Height - 100))
                    flag = 0;
            }
            y = pictureBox1.Height - 100 - timerCounter * 10;
            DrawCircle(x, y);

            pictureBox1.Image = myBitmap;
        }
      
    }
}
