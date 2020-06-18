using System;
using System.IO;
using System.Xml.Serialization;
namespace Podaci
{
    [Serializable]
    public class Osoba : Object
    {
        #region Atributes

        String _ime;
        String _prezime;
        int _poeni;

        #endregion

        #region Properties
        [XmlElementAttribute("Ime")]
        public String Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        [XmlElementAttribute("Prezime")]
        public String Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }
        [XmlElementAttribute("Poeni")]
        public int Poeni
        {
            get { return _poeni; }
            set { _poeni = value; }
        }
        [XmlIgnore]
        public String ZaPrikaz
        {
            get
            {
                return _ime + " " + _prezime + " " + Poeni;
            }
        }
        public void Upis(StreamWriter a)
        {
            a.WriteLine(_ime + " " + _prezime + " " + Poeni);
        }

        #endregion

        #region Constructors

        public Osoba()
        {
        }

        public Osoba(String ime, String prezime)
        {
            _ime = ime;
            _prezime = prezime;
            Poeni = 0;
        }

        #endregion
    }
}
