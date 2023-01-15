using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPost.Model
{
    public interface IOperacja
    {
        public string Nazwa { get; }
        public int NumerPaczki { get; }

        public string OperacjaName { get; }
    }
}
