using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPost.Model
{
    internal class Dostarczenie : IOperacja
    {
        private Paczka _paczka;
        public Paczka Paczka => _paczka;
        public Dostarczenie(Paczka paczkaDoDostarczenia)
        {
            _paczka = paczkaDoDostarczenia;
        }
    }
}
