using InPost.Helpers.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPost.Model
{
    public class Odebranie : IOperacja
    {
        private int _numerPaczki;
        private int _ktoryPaczkomat;
        public int NumerPaczki { get => _numerPaczki;}
        public Odebranie(int numerPaczki, int gdzieOdbieramy)
        {
            _numerPaczki = numerPaczki;
            _ktoryPaczkomat = gdzieOdbieramy;
        }
        public Odebranie(int gdzieOdbieramy)
        {
            DialogOdebranie inputDialog = new DialogOdebranie("Podaj numer paczki", "9999");
            if (inputDialog.ShowDialog() == true && inputDialog.Answer != null)
            {
                _numerPaczki = inputDialog.Answer;
            }
            _ktoryPaczkomat = gdzieOdbieramy;
        }
        public int KtoryPaczkomat =>_ktoryPaczkomat;
        public string Nazwa => "Odebranie";

        public string OperacjaName => "Odebranie: " + _numerPaczki.ToString();

    }
}
