using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace Podaci
{
    public class Polje
    {
        private Rectangle _kocka;
        private int _ID;
        private Color _boja;
        public Polje(int a, int b)
        {
            _boja = Color.White;
            _kocka = new Rectangle(b * 25, a * 25, 25, 25);
        }
        public int ID { get { return _ID; } set { _ID = value; } }
        public Rectangle Kocka { get { return _kocka; } set { _kocka = value; } }
        public Color Boja { get { return _boja; } set { _boja = value; } }


    }
}
