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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picture.Load(@"..\..\372.jpg");
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            int a;
            Bitmap image = new Bitmap(picture.Image);
            int x = e.X;
            int y = e.Y;
            Color color = image.GetPixel(x, y);
        }
    }
}
