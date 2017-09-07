using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class Vogel
    {
        public bool red;
        public int brojRedaIliStupca;
        public double indeks;

        public Vogel(bool red, int brojRedaIliStupca, double indeks)
        {
            this.red = red;
            this.brojRedaIliStupca = brojRedaIliStupca;
            this.indeks = indeks;
        }
    }
}
