using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    //klasa sluzi za provjeru je li odredeni red/stupac rjesen kod Vogela i min troskova
    public class ProvjereniRjesenost
    {
        public bool red;
        public bool rjeseni = false;
        public int brojRedaIliStupca;

        public ProvjereniRjesenost(bool red, bool rjeseni, int brojRedaIliStupca)
        {
            this.red = red;
            this.rjeseni = rjeseni;
            this.brojRedaIliStupca = brojRedaIliStupca;
        }
    }
}
