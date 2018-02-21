using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    //klasa sluzi kako bi zapisali indekse po retku/stupcu kod Vogela
    //ili kako bi zapisali Ui i Vj (dualne varijable) kod MODI metode
    public class DualnaVarijablaIliIndeks
    {
        public bool red;
        public int brojRedaIliStupca;
        public double vrijednost;

        public DualnaVarijablaIliIndeks(bool red, int brojRedaIliStupca, double vrijednost)
        {
            this.red = red;
            this.brojRedaIliStupca = brojRedaIliStupca;
            this.vrijednost = vrijednost;
        }

        public override bool Equals(object obj)
        {
            DualnaVarijablaIliIndeks dualnaVarijabla = obj as DualnaVarijablaIliIndeks;
            if (dualnaVarijabla.red == red && dualnaVarijabla.brojRedaIliStupca == brojRedaIliStupca)
            {
                return true;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
