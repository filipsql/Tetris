using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Podaci;

namespace CustomControl
{
    public partial class TetrisUser : UserControl
    {
       
        public TetrisUser()
        {
            InitializeComponent();
            pictureBox1.Width = Width;
            pictureBox1.Height = Height;
            
           
        }
       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //DoubleBuffered = true;
            for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja), MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Kocka);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Kocka);

                }
        }


        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
