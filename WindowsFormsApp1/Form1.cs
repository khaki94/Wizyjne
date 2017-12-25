using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        struct Point
        {
            public int x;
            public int y;
            public Point(int x,int y) { this.x = x; this.y = y; }
        }

        List<Point> wspList = new List<Point>();
        Bitmap backup;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picture.Load(@"..\..\obiekty_kolor.bmp");
            backup = new Bitmap(picture.Image);
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            picture.Image = backup;
            Bitmap image = new Bitmap(picture.Image);
            int x = e.X;
            int y = e.Y;
            int[,] bitArray = CreateFigureArray(image, x, y);
            // pole figury
            long area = GetFigureArea(bitArray);
            // wspolrzedne srodka 
            Point center = GetFigureCenter(bitArray);
            picture.Image = GetNewPicture(image.Width, image.Height, center, bitArray, image);
        }

        private int[,] CreateFigureArray(Bitmap bitmap, int x, int y)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int[,] array = new int[width, height];
            Color baseColor = bitmap.GetPixel(x, y);
            wspList.Add(new Point(x, y));
            int iteration = 1;
            width--; height--;
            while( wspList.Count != 0)
            {
                Point tmp = wspList.First();
                wspList.RemoveAt(0);
                if ( tmp.x != width && bitmap.GetPixel(tmp.x + 1, tmp.y) == baseColor && array[tmp.x + 1, tmp.y] != 1)
                {
                    wspList.Add(new Point(tmp.x + 1, tmp.y));
                    array[tmp.x + 1, tmp.y] = iteration;
                }
                if (tmp.x != 0 && bitmap.GetPixel(tmp.x - 1, tmp.y) == baseColor && array[tmp.x - 1, tmp.y] != 1)
                {
                    wspList.Add(new Point(tmp.x - 1, tmp.y));
                    array[tmp.x -1, tmp.y] = iteration;
                }
                if (tmp.y != height && bitmap.GetPixel(tmp.x, tmp.y + 1) == baseColor && array[tmp.x, tmp.y + 1] != 1)
                {
                    wspList.Add(new Point(tmp.x, tmp.y + 1));
                    array[tmp.x, tmp.y + 1] = iteration;
                }
                if (tmp.y != 0 && bitmap.GetPixel(tmp.x, tmp.y - 1) == baseColor && array[tmp.x, tmp.y - 1] != 1) 
                {
                    wspList.Add(new Point(tmp.x, tmp.y - 1));
                    array[tmp.x, tmp.y - 1] = iteration;
                }
                if (tmp.y != 0 && tmp.x != 0 && bitmap.GetPixel(tmp.x -1 , tmp.y - 1) == baseColor && array[tmp.x - 1, tmp.y - 1] != 1)
                {
                    wspList.Add(new Point(tmp.x - 1, tmp.y - 1));
                    array[tmp.x - 1, tmp.y - 1] = iteration;
                }
                if (tmp.y != 0 && tmp.x != width && bitmap.GetPixel(tmp.x + 1, tmp.y - 1) == baseColor && array[tmp.x + 1, tmp.y - 1] != 1)
                {
                    wspList.Add(new Point(tmp.x + 1, tmp.y - 1));
                    array[tmp.x + 1, tmp.y - 1] = iteration;
                }
                if (tmp.y != height && tmp.x != 0 && bitmap.GetPixel(tmp.x - 1, tmp.y + 1) == baseColor && array[tmp.x - 1, tmp.y + 1] != 1)
                {
                    wspList.Add(new Point(tmp.x - 1, tmp.y + 1));
                    array[tmp.x - 1, tmp.y + 1] = iteration;
                }
                if (tmp.y != height && tmp.x != width && bitmap.GetPixel(tmp.x + 1, tmp.y + 1) == baseColor && array[tmp.x + 1, tmp.y + 1] != 1)
                {
                    wspList.Add(new Point(tmp.x + 1, tmp.y + 1));
                    array[tmp.x + 1, tmp.y + 1] = iteration;
                }
            }
            return array;
        }
        private int GetFigureArea(int [,] figure)
        {
            int sum = 0;
            foreach( int pixel in figure)
            {
                if( pixel > 0) sum++;
            }
            return sum;
        }
        private int GetFigureTop(int [,] array)
        {
            int widthLenght = array.GetLength(0);
            int heightLenght = array.GetLength(1);
            int toReturn = -1;
            for (int y = 0; y < heightLenght; y++) 
            {
                for (int x = 0; x < widthLenght; x++)
                {
                    if (array[x, y] == 1)
                    {
                        if (toReturn == -1) toReturn = y;
                        toReturn = toReturn < y ? toReturn : y;
                    }
                        
                }
            }
            return toReturn;
        }
        private int GetFigureBottom(int[,] array)
        {
            int widthLenght = array.GetLength(0);
            int heightLenght = array.GetLength(1);
            int toReturn = -1;
            for (int y = heightLenght - 1; y > 0; y--)
            {
                for (int x = 0; x < widthLenght; x++)
                {
                    if (array[x, y] == 1)
                    {
                        if (toReturn == -1) toReturn = y;
                        toReturn = toReturn < y ? y : toReturn;
                    }
                }
            }
            return toReturn;
        }
        private int GetFigureLeft(int[,] array)
        {
            int widthLenght = array.GetLength(0);
            int heightLenght = array.GetLength(1);
            int toReturn = -1;
            for (int x = 0; x < widthLenght; x++) 
            {
                for (int y = 0; y < heightLenght; y++)
                {
                    if (array[x, y] == 1)
                    {
                        if (toReturn == -1) toReturn = x;
                        toReturn = toReturn < x ? toReturn : x;
                    }
                }
            }
            return toReturn;
        }
        private int GetFigureRight(int[,] array)
        {
            int widthLenght = array.GetLength(0);
            int heightLenght = array.GetLength(1);
            int toReturn = -1;
            for (int x = widthLenght - 1; x > 0 ; x--) 
            {
                for (int y = 0; y < heightLenght; y++)
                {
                    if (array[x, y] == 1)
                    {
                        if (toReturn == -1) toReturn = x;
                        toReturn = toReturn < x ? x : toReturn;
                    }
                }
            }
            return toReturn;
        }
        private Point GetFigureCenter(int[,] array)
        {
            int bot = GetFigureBottom(array);
            int top = GetFigureTop(array);
            int left = GetFigureLeft(array);
            int right = GetFigureRight(array);

            int h = top + ((bot - top) / 2);
            int w = left + ((right - left) / 2);

            return new Point(w, h);
        }
        private Bitmap GetNewPicture(int width, int height, Point Center, int[,] array, Bitmap oldBitMap)
        {
            Bitmap tmp = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (array[i, j] == 1)
                        tmp.SetPixel(i, j, Color.Purple);
                    else
                        tmp.SetPixel(i, j, oldBitMap.GetPixel(i,j));
                }
            }
            tmp.SetPixel(Center.x, Center.y, Color.White);
            return tmp;
        }
    }
}