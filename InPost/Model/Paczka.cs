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
    public class Paczka
    {
        private double _wysokosc;
        private double _szerokosc;
        private double _waga;
        private int _numerPaczki;
        private string _nazwiskoNadawcy;
        private string _imieNadawcy;
        private string _nazwiskoOdbiorcy;
        private string _imieOdbiorcy;

        public string ImieNadawcy => _imieNadawcy;
        public string NazwiskoNadawcy => _nazwiskoNadawcy;

        public string ImieOdbiorcy => _imieOdbiorcy;
        public string NazwiskoOdbiorcy => _nazwiskoOdbiorcy;

        public Paczka(string imieNadawcy,string nazwiskoNadawcy,string imieOdbiorcy,string nazwiskoOdbiorcy,int numerPaczki)
        {
            _imieNadawcy = imieNadawcy;
            _nazwiskoNadawcy = nazwiskoNadawcy;
            _imieOdbiorcy = imieOdbiorcy;
            _nazwiskoOdbiorcy = nazwiskoOdbiorcy;
            _numerPaczki = numerPaczki;
        }
        public int NumerPaczki => _numerPaczki;
    }
}

