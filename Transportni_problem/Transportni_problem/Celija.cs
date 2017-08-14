using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class Celija
    {
        string opis;
        int red;
        int stupac;
        double stvarniTrosak;
        //double kolicinaTereta;
        //double relativniTrosak;

        public Celija(string opis, int red, int stupac, double stvarniTrosak)
        {
            this.opis = opis;
            this.red = red;
            this.stupac = stupac;
            this.stvarniTrosak = stvarniTrosak;
        }
    }
}
