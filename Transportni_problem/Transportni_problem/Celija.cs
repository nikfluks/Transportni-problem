using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class Celija
    {
        public string opis;
        public int red;
        public int stupac;
        public double stvarniTrosak;
        public double kolicinaTereta;
        //public double relativniTrosak;

        public Celija(string opis, int red, int stupac, double stvarniTrosak)
        {
            this.opis = opis;
            this.red = red;
            this.stupac = stupac;
            this.stvarniTrosak = stvarniTrosak;
        }
    }
}
