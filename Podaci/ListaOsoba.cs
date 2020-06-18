using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Podaci
{
    [Serializable]
    public class ListaOsoba
    {      
        #region Atributes

        private List<Osoba> _listaOsoba;

        #endregion

        #region Properties
        [XmlArrayItem("Osoba", typeof(Osoba))]
        public List<Osoba> ListaOsobaValues
        {
            get
            {
                return _listaOsoba;
            }
            set
            {
                _listaOsoba = value;
            }
        }
        #endregion

        #region Constructors

        public ListaOsoba()
        {
            _listaOsoba = new List<Osoba>();
        }

        #endregion

        #region Methodes

        public bool DodajOsobu(Osoba o)
        {
            if (PostojiOsobaUListi(o))
                return false;

            _listaOsoba.Insert(0, o);
            return true;
        }
        public void azurirajpoene(int p)
        {
            _listaOsoba[0].Poeni = p;
        }
        public void sortiraj()
        {
            bool swapped = true;
            int j = 0;
            while (swapped)
            {
                Osoba tmp;
                swapped = false;
                j++;
                for (int i = 0; i < _listaOsoba.Count - j; i++)
                {
                    if (_listaOsoba[i].Poeni < _listaOsoba[i + 1].Poeni)
                    {
                        tmp = _listaOsoba[i];
                        _listaOsoba[i] = _listaOsoba[i + 1];
                        _listaOsoba[i + 1] = tmp;
                        swapped = true;
                    }
                }
            }
        }
        public bool IzmeniOsobu(Osoba o)
        {
            var tmp = GetOsoba(o.Ime, o.Prezime);

            if (tmp == null)
                return false;

            tmp.Ime = o.Ime;
            tmp.Prezime = o.Prezime;

            return true;
        }

        public bool ObrisiOsobu(Osoba o)
        {
            if (!PostojiOsobaUListi(o))
                return false;

            _listaOsoba.Remove(o);
            return true;
        }
        public List<Osoba> vratilistu()
        {
            return _listaOsoba;
        }
        public bool obrisilistu()
        {
            _listaOsoba.Clear();
            return true;
        }
        public bool ObrisiOsobu(String ime, String prezime)
        {
            Osoba tmpOsoba = null;

            foreach (var v in _listaOsoba)
            {
                if (v.Ime == ime && v.Prezime == prezime)
                {
                    tmpOsoba = v;
                    break;
                }
            }

            if (tmpOsoba != null)
            {
                _listaOsoba.Remove(tmpOsoba);
                return true;
            }

            return false;
        }

        public bool PostojiOsobaUListi(Osoba o)
        {
            foreach (var v in _listaOsoba)
            {
                if (v.Ime == o.Ime && v.Prezime == o.Prezime)
                    return true;
            }

            return false;
        }

        public bool PostojiOsobaUListi(String ime, String prezime)
        {
            foreach (var v in _listaOsoba)
            {
                if (v.Ime == ime && v.Prezime == prezime)
                    return true;
            }

            return false;
        }
        public void upisi()
        {
            using (StreamWriter sw = new StreamWriter("imena.txt"))
            {
                //foreach (string s in imena)
                foreach (Osoba s in _listaOsoba)
                {
                    sw.WriteLine(s.Ime + " " + s.Prezime);
                }
                sw.Close();
            }

        }


        public Osoba GetOsoba(String ime, String prezime)
        {
            foreach (var v in _listaOsoba)
            {
                if (v.Ime == ime && v.Prezime == prezime)
                    return v;
            }

            return null;
        }

        
        public int brojlistova()
        {
            return _listaOsoba.Count;
        }

        #endregion


        private static ListaOsoba _instance = null;
        public static ListaOsoba Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ListaOsoba();

                return _instance;
            }
        }
    }
}

