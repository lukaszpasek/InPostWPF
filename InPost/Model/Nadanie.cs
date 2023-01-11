using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPost.Model
{
    public class Nadanie : IOperacja
    {
        private Paczka _paczka;
        private int _ktoryPaczkomat;
        public int DoKtoregoPaczkomatu=>_ktoryPaczkomat;

        public Paczka Paczka=> _paczka;
        public Nadanie(Paczka paczkaDoNadania,int gdzieNadajemy)
        {
            _paczka = paczkaDoNadania;
            _ktoryPaczkomat = gdzieNadajemy;
        }
    }
}
