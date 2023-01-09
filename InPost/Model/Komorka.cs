using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InPost.Model
{
    public class Komorka
    {
        double _width;
        double _height;
        bool _czy_zajeta;
        Paczka _paczka;
        public bool CzyZajeta => _czy_zajeta;
        public Komorka()
        {
        }
        public bool Wrzuc(Paczka paczkaDoNadania)
        {
            if (_czy_zajeta is not true)
            {
                _paczka = paczkaDoNadania;
                _czy_zajeta = true;
            }
            else
            {
                return false;
            }
           return true;
        }
        public Paczka Odbierz
        {
            get
            {
                Paczka tmp;
                if (_czy_zajeta is true)
                {
                    _czy_zajeta = false;
                    tmp = _paczka;
                    _paczka = null;
                }
                return _paczka;
            }
        }
    }
}

