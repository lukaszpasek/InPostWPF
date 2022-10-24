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
        public int NumerPaczki { get => _numerPaczki; }
        public Odebranie(int numerPaczki, int gdzieOdbieramy)
        {
            _numerPaczki = numerPaczki;
            _ktoryPaczkomat = gdzieOdbieramy;
        }
        public int KtoryPaczkomat =>_ktoryPaczkomat;
    }
}
