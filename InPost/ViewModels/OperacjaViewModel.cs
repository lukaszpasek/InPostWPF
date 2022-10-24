using InPost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPost.ViewModels
{

    public class OperacjaViewModel
    {
        public int NrPaczkomatu;
        public string Numer => NrPaczkomatu.ToString();
        public string TypOperacji
        {
            get
            {
                if (Operacja is Nadanie)
                {
                    return "Nadanie";
                }
                else
                {
                    return "Odebranie";
                }
            }
        }
        public IOperacja Operacja;
        public OperacjaViewModel(int nrPaczkomatu, IOperacja operacja)
        {
            NrPaczkomatu = nrPaczkomatu;
            Operacja = operacja;
        }
    }
}
