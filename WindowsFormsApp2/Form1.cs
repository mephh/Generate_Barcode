using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string infile = "C:\\bastrza\\security.t";
        //string infile = "Adrian Nowak:012688;87213NBJBU3213B23J1";
        Hashtable tabela = new Hashtable();
        string code = " ";
        string name = " ";
        string barcode = " ";
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = (string)tabela[listBox1.Text];
            barcode = (string)tabela[listBox1.Text];
            name = listBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using(var reader = new StreamReader(infile))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    name = line.Remove(line.IndexOf(':'));
                    code = line.Remove(0, line.IndexOf(';') + 1);
                    tabela.Add(name, code);
                    listBox1.Items.Add(name);
                }
            }
        }
          
        private void button1_Click(object sender, EventArgs e)
        {
            //barcode = listBox1.Text;
            Bitmap bitmap = new Bitmap(barcode.Length * 20, 60);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Font ofont = new System.Drawing.Font("IDAutomationHC39M", 10);
                PointF point = new PointF(2f, 2f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush White = new SolidBrush(Color.White);
                graphics.FillRectangle(White, 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawString("*" + name + "*", ofont, black, point);
            }
            using(MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                pictureBox1.Image = bitmap;
                pictureBox1.Height = bitmap.Height;
                pictureBox1.Width = bitmap.Width;
            }
        }
    }
}
