using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;



namespace Podaci
{
    public class Figura
    {
        private Color _boja;
        private String _ime;
        private int _stanje;

        public int Stanje { get { return _stanje; } set { _stanje = value; } }
        public Color Boja { get { return _boja; } set { _boja = value; } }
        public String ImeFigure { get { return _ime; } set { _ime = value; } }



        public Figura(String name)
        {
            //Boja = p;
            Stanje = 1;
            ImeFigure = name;
        }


    }
}
