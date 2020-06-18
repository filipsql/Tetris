using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Podaci
{
    public class MatricaPolja
    {
        private Polje[,] _matra;
        private static int _ID = 0;
        private bool _okrecese;
        private int _n;
        private int _m;
        private MatricaPolja _glavniobjekat;
        public MatricaPolja()
        { }

        public MatricaPolja(int a, int b)
        {
            N = a;
            M = b;
            Matra = new Polje[N,M];
            for (int i = 0; i < a; i++)
                for (int j = 0; j < b; j++)
                {
                    Matra[i, j] = new Polje(i, j);
                }
        }
        public bool Okrece { get { return _okrecese; } set { _okrecese = value; } }
        public int ID { get { return _ID; } set { _ID = value; } }
        public MatricaPolja GlavniObjekat { get { return _glavniobjekat; } set { _glavniobjekat = value; } }
        public Polje[,] Matra {get {return _matra;} set { _matra = value; } }
        public int N { get { return _n; } set { _n = value; } }
        public int M { get { return _m; } set { _m = value; } }

        public void dodajumatricu(int a, int b, Rectangle w)
        {
            _matra[a,b].Kocka = w;
        }
        
        public void okreni(int q)
        {
            MatricaPolja.Instance.GlavniObjekat.Okrece = true;
            int z = nadjiJ();
            int x = nadjiI();
            int o=2, k=0;
            if (x > 0 && z >= 0)
            {
                MatricaPolja P = new MatricaPolja(3, 3);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        P.Matra[i, j].ID = MatricaPolja.Instance.GlavniObjekat.Matra[x + i, z + j].ID;
                        P.Matra[i, j].Boja = MatricaPolja.Instance.GlavniObjekat.Matra[x + i, z + j].Boja;
                        if (P.Matra[i, j].ID!=0 && P.Matra[i,j].ID!= MatricaPolja.Instance.GlavniObjekat.ID)
                        {
                            MatricaPolja.Instance.GlavniObjekat.Okrece = false;
                        }
                    }
                if (MatricaPolja.Instance.GlavniObjekat.Okrece == true)
                {
                    for (int i = x; i < x + 3; i++)
                    {
                        o = 2;
                        for (int j = z; j < z + 3; j++)
                        {
                            MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja = P.Matra[o, k].Boja;
                            MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID = P.Matra[o, k].ID;
                            o--;
                        }
                        k++;
                    }
                }
            }
            MatricaPolja.Instance.GlavniObjekat.Okrece = false;

        }
        private int nadjiJ()
        {
            for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                for(int j=0; j< MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID == MatricaPolja.Instance.GlavniObjekat.ID && (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja == Color.Red || MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja == Color.Blue))
                    {
                        if (j + 1 < MatricaPolja.Instance.GlavniObjekat.M)
                        {
                            if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j + 1].ID == MatricaPolja.Instance.GlavniObjekat.ID)
                                return j;
                            else
                                return j - 1;
                        }
                        return 0;
                    }
                }
            return 0;
        }
        public void ocistimatricu()
        {
            for(int i=0; i<GlavniObjekat.N; i++)
                for(int j=0; j<GlavniObjekat.M; j++)
                {
                    GlavniObjekat.Matra[i, j].ID = 0;
                    GlavniObjekat.Matra[i, j].Boja = Color.White;
                }
        }
        private int nadjiI()
        {
            for (int i = 0; i < MatricaPolja.Instance.GlavniObjekat.N; i++)
                for (int j = 0; j < MatricaPolja.Instance.GlavniObjekat.M; j++)
                {
                    if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j].ID == MatricaPolja.Instance.GlavniObjekat.ID &&( MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja == Color.Red || MatricaPolja.Instance.GlavniObjekat.Matra[i, j].Boja == Color.Blue))
                    {
                        if (j + 1 < MatricaPolja.Instance.GlavniObjekat.M)
                        {
                            if (MatricaPolja.Instance.GlavniObjekat.Matra[i, j + 1].ID == MatricaPolja.Instance.GlavniObjekat.ID)
                                return i - 1;
                            else
                                return i;
                        }
                        return 0;
                    }
                }
            return 0;
        }
        public void ucitajelement(Figura p, bool h)
        {
            if(h)
                _ID++;
            if(p.ImeFigure=="kocka")
            {
                p.Boja = Color.Yellow;
                GlavniObjekat.Matra[0, GlavniObjekat.M / 2].Boja = p.Boja;
                GlavniObjekat.Matra[0, GlavniObjekat.M / 2].ID = ID;
            }
            if(p.ImeFigure=="linija")
            {
                p.Boja = Color.Blue;
                for (int j = GlavniObjekat.M / 2 - 1; j < GlavniObjekat.M / 2 + 2; j++)
                {
                    GlavniObjekat.Matra[0, j].Boja = p.Boja;
                    GlavniObjekat.Matra[0, j].ID = ID;
                }
            }
            if(p.ImeFigure=="plus")
            {
                p.Boja = Color.Violet;
                GlavniObjekat.Matra[0, GlavniObjekat.M / 2].Boja = p.Boja;
                GlavniObjekat.Matra[0, GlavniObjekat.M / 2].ID = ID;

                GlavniObjekat.Matra[1, GlavniObjekat.M / 2-1].Boja = p.Boja;
                GlavniObjekat.Matra[1, GlavniObjekat.M / 2-1].ID = ID;
                GlavniObjekat.Matra[1, GlavniObjekat.M / 2].Boja = p.Boja;
                GlavniObjekat.Matra[1, GlavniObjekat.M / 2].ID = ID;
                GlavniObjekat.Matra[1, GlavniObjekat.M / 2+1].Boja = p.Boja;
                GlavniObjekat.Matra[1, GlavniObjekat.M / 2+1].ID = ID;

                GlavniObjekat.Matra[2, GlavniObjekat.M / 2].Boja = p.Boja;
                GlavniObjekat.Matra[2, GlavniObjekat.M / 2].ID = ID;

            }
            if(p.ImeFigure== "zelenakocka")
            {
                p.Boja = Color.Green;

                for(int i=0; i<3; i++)
                    for(int j=GlavniObjekat.M/2-1; j<GlavniObjekat.M/2+2; j++)
                    {
                        GlavniObjekat.Matra[i, j].Boja = p.Boja;
                        GlavniObjekat.Matra[i, j].ID = ID;
                    }
            }
            if(p.ImeFigure=="crveno")
            {
                p.Boja = Color.Red;
                GlavniObjekat.Matra[0, GlavniObjekat.M / 2].Boja = p.Boja;
                GlavniObjekat.Matra[0, GlavniObjekat.M / 2].ID = ID;
                for (int j = GlavniObjekat.M / 2 - 1; j < GlavniObjekat.M / 2 + 2; j++)
                {
                    GlavniObjekat.Matra[1, j].Boja = p.Boja;
                    GlavniObjekat.Matra[1, j].ID = ID;
                }
            }
        }
        private static MatricaPolja _instance = null;
        public static MatricaPolja Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MatricaPolja();

                return _instance;
            }
        }
    }
}
