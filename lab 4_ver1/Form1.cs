using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControl;
using Podaci;
using Extension;

namespace lab_4_ver1
{
    public partial class Form1 : Form
    {
        public ListaOsoba _listaosoba = new ListaOsoba();
        private Form2 form2;
        private List<String> p = new List<String>();
        private int Bodovi = 0;
        private static int o;
        private Stopwatch stopwatch = new Stopwatch();
        private MatricaPolja l = new MatricaPolja();
        public Form1()
        {
            InitializeComponent();
            MatricaPolja.Instance.GlavniObjekat = new MatricaPolja(20, 12);
            l.GlavniObjekat = new MatricaPolja(3, 3);
            form2 = new Form2(this);
            p.Add("kocka");
            p.Add("linija");
            p.Add("plus");
            p.Add("zelenakocka");
            p.Add("crveno");
            citaj();
            listBox1.ClearSelected();
        }
        public void Start(bool j)
        {
            Random rnd = new Random();
            if (!prviredpopunjen())
                krajigre();
            else
            {
                if (!timer1.Enabled)
                {
                    stopwatch.Start();
                    Bodovi = 0;
                    MatricaPolja.Instance.GlavniObjekat.ID = 0;
                    timer1.Start();
                    Start(true);
                }
                else
                {
                    if (j == true)
                    {
                        MatricaPolja.Instance.ucitajelement(new Figura(p[rnd.Next() % 5]), true);
                        o = rnd.Next() % 5;
                        l.ocistimatricu();
                        l.ucitajelement(new Figura(p[o]), false);
                    }
                    else
                    {

                        MatricaPolja.Instance.ucitajelement(new Figura(p[o]), true);
                        o = rnd.Next() % 5;
                        l.ocistimatricu();
                        l.ucitajelement(new Figura(p[o]), false);
                    }
                }
            }

            timer1.Interval = 500;

            //  MatricaPolja.Instance.ucitajelement(new Figura("linija"));
            // MatricaPolja.Instance.ucitajelement(new Figura("zelenakocka"));

        }

        void krajigre()
        {
            timer1.Stop();
            stopwatch.Reset();
            MessageBox.Show("Izgubili ste");
            _listaosoba.azurirajpoene(Bodovi);
            _listaosoba.sortiraj();
            button2.Enabled = true;
            snimixml();
            citaj();
            listBox1.ClearSelected();


        }
        void snimixml()
        {
            //   SaveFileDialog sfd = new SaveFileDialog();
            //  sfd.Filter = "xml files (*.xml)|*.xml";

            //   if (sfd.ShowDialog() == DialogResult.OK)
            // {
            String path = "filip.xml";

            _listaosoba.Serialize(path);
            //   }
        }
        void citaj()
        {
            //  OpenFileDialog ofd = new OpenFileDialog();
            //   ofd.Filter = "xml files (*.xml)|*.xml";

            //   if (ofd.ShowDialog() == DialogResult.OK)
            {
                // ListaOsoba.Instance.ListaOsobaValues = ListaOsoba.Instance.ListaOsobaValues.DeSerialize(ofd.FileName);
                _listaosoba = _listaosoba.DeSerialize("filip.xml");
                LoadPodaci();
            }
        }
        void LoadPodaci()
        {
            listBox1.DisplayMember = "ZaPrikaz";

            listBox1.DataSource = _listaosoba.ListaOsobaValues.ToList();
        }
        bool prviredpopunjen()
        {
            for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                if (MatricaPolja.Instance.GlavniObjekat.Matra[0, j].ID != 0)
                    return false;
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Ukupno.Text = MatricaPolja.Instance.GlavniObjekat.ID.ToString();
            Poeni.Text = Bodovi.ToString();
            label3.Text = stopwatch.Elapsed.Minutes.ToString() + ":" + stopwatch.Elapsed.Seconds.ToString();
            if (MatricaPolja.Instance.GlavniObjekat.ID != 0 && MatricaPolja.Instance.GlavniObjekat.Okrece == false)
            {

                pomerifigure(MatricaPolja.Instance.GlavniObjekat.ID);
                Refresh();
            }



        }
        void spustisve(int s)
        {
            for (int i = s - 1; i >= 0; i--)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    MatricaPolja.Instance.GlavniObjekat.Matra[i + 1, j].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                    MatricaPolja.Instance.GlavniObjekat.Matra[i + 1, j].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;


                }
        }
        int racunajpoene()
        {
            for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                if (dalijeredpopunjen(i))
                    return i;
            return -1;
        }
        bool dalijeredpopunjen(int i)
        {
            for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
            {
                if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID == 0)
                    return false;
            }
            return true;
        }

        void pomerifigure(int p)
        {
            if (provera(p) && MatricaPolja.Instance.GlavniObjekat.Okrece == false)
            {
                for (int i = MatricaPolja.Instance.GlavniObjekat.N - 1; i >= 0; i--)
                    for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                    {
                        //if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja != Color.White)
                        if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID == p)
                        {
                            if (i + 1 < MatricaPolja.Instance.GlavniObjekat.N && MatricaPolja.Instance.GlavniObjekat.Matra[i + 1, j].Boja == Color.White)
                            {
                                MatricaPolja.Instance.GlavniObjekat.Matra[i + 1, j].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                                MatricaPolja.Instance.GlavniObjekat.Matra[i + 1, j].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;
                                MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja = Color.White;
                                MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID = 0;


                            }
                        }
                    }
            }

            else
            {
                while (racunajpoene() != -1)
                {
                    spustisve(racunajpoene());
                    Bodovi += 1000;
                    Refresh();
                }
                Start(false);
            }

        }
        bool provera(int s)
        {
            MatricaPolja P = new MatricaPolja(MatricaPolja.Instance.GlavniObjekat.N, MatricaPolja.Instance.GlavniObjekat.M);
            for (int i = MatricaPolja.Instance.GlavniObjekat.N - 1; i >= 0; i--)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    P.Matra[i, j].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;
                    P.Matra[i, j].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                }
            for (int i = MatricaPolja.Instance.GlavniObjekat.N - 1; i >= 0; i--)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    if (P.Matra[i, j].ID == s && s != 0)
                    {
                        if (i + 1 < MatricaPolja.Instance.GlavniObjekat.N && P.Matra[i + 1, j].Boja == Color.White)
                        {
                            P.Matra[i + 1, j].Boja = P.Matra[i, j].Boja;
                            P.Matra[i, j].Boja = Color.White;
                        }
                        else
                            return false;
                    }

                }
            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (timer1.Enabled)
            {
                if (keyData == Keys.Right)
                {
                    if (provera2(MatricaPolja.Instance.GlavniObjekat.ID))
                    {
                        for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                            for (int j = MatricaPolja.Instance.GlavniObjekat.M - 1; j >= 0; j--)
                            {

                                if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID == MatricaPolja.Instance.GlavniObjekat.ID)
                                {
                                    if (j + 1 < MatricaPolja.Instance.GlavniObjekat.M && MatricaPolja.Instance.GlavniObjekat.Matra[i, j + 1].Boja == Color.White)
                                    {
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j + 1].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j + 1].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja = Color.White;
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID = 0;


                                    }
                                }
                            }
                    }
                }
                else if (keyData == Keys.Left)
                {
                    if (provera3(MatricaPolja.Instance.GlavniObjekat.ID))
                    {
                        for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                            for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                            {

                                if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID == MatricaPolja.Instance.GlavniObjekat.ID)
                                {
                                    if (j - 1 >= 0 && MatricaPolja.Instance.GlavniObjekat.Matra[i, j - 1].Boja == Color.White)
                                    {
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j - 1].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j - 1].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja = Color.White;
                                        MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID = 0;


                                    }
                                }
                            }
                    }
                }
                if (keyData == Keys.Down)
                {
                    timer1.Interval = 50;
                }
                if (keyData == Keys.Up)
                {
                    timer1.Interval = 500;
                }
                if (keyData == Keys.Space)
                {
                    MatricaPolja.Instance.GlavniObjekat.okreni(MatricaPolja.Instance.GlavniObjekat.ID);
                }

            }
            if (keyData == Keys.P)
            {
                if (MatricaPolja.Instance.GlavniObjekat.ID != 0)
                {
                    if (stopwatch.IsRunning)
                    {
                        stopwatch.Stop();
                        timer1.Stop();
                    }
                    else
                    {
                        stopwatch.Start();
                        timer1.Start();
                    }
                }
            }


            Refresh();
            //  timer1.Interval = 500;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private bool provera3(int s)
        {

            MatricaPolja P = new MatricaPolja(MatricaPolja.Instance.GlavniObjekat.N, MatricaPolja.Instance.GlavniObjekat.M);
            for (int i = MatricaPolja.Instance.GlavniObjekat.N - 1; i >= 0; i--)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    P.Matra[i, j].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;
                    P.Matra[i, j].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                }
            for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    if (P.Matra[i, j].ID == s && s != 0)
                    {
                        if (j - 1 >= 0 && P.Matra[i, j - 1].Boja == Color.White)
                        {
                            P.Matra[i, j - 1].Boja = P.Matra[i, j].Boja;
                            P.Matra[i, j].Boja = Color.White;
                        }
                        else
                            return false;
                    }

                }
            return true;
        }
        private bool provera2(int s)
        {

            MatricaPolja P = new MatricaPolja(MatricaPolja.Instance.GlavniObjekat.N, MatricaPolja.Instance.GlavniObjekat.M);
            for (int i = MatricaPolja.Instance.GlavniObjekat.N - 1; i >= 0; i--)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    P.Matra[i, j].ID = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID;
                    P.Matra[i, j].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja;
                }
            for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                for (int j = MatricaPolja.Instance.GlavniObjekat.M - 1; j >= 0; j--)
                {
                    if (P.Matra[i, j].ID == s && s != 0)
                    {
                        if (j + 1 < MatricaPolja.Instance.GlavniObjekat.M && P.Matra[i, j + 1].Boja == Color.White)
                        {
                            P.Matra[i, j + 1].Boja = P.Matra[i, j].Boja;
                            P.Matra[i, j].Boja = Color.White;
                        }
                        else
                            return false;
                    }

                }
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
            (sender as Button).Enabled = false;


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < l.GlavniObjekat.N; i++)
                for (int j = 0; j < l.GlavniObjekat.M; j++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(l.GlavniObjekat.Matra[i, j].Boja), l.GlavniObjekat.Matra[i, j].Kocka);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), l.GlavniObjekat.Matra[i, j].Kocka);

                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }
        void reset()
        {
            timer1.Stop();
            MatricaPolja.Instance.ocistimatricu();
            l.ocistimatricu();
            Refresh();
            form2.ShowDialog();
            //    button2.Enabled = false;

        }
    }
}
