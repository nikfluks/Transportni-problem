using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class ProvjereniVogel
    {
        public bool red;
        public bool rjeseni = false;
        public int brojRedaIliStupca;

        public ProvjereniVogel(bool red, bool rjeseni, int brojRedaIliStupca)
        {
            this.red = red;
            this.rjeseni = rjeseni;
            this.brojRedaIliStupca = brojRedaIliStupca;
        }
    }
}
