using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    //klasa nam sluzi kako bi za celije na zatvorenom putu dodali jesu li one "+" ili "-" celije, te na koji zatvoreni put se odnose
    public class Predznak
    {
        public bool plus;
        public int redniBrojZatvorenogPuta;

        public Predznak(bool plus, int redniBrojZatvorenogPuta)
        {
            this.plus = plus;
            this.redniBrojZatvorenogPuta = redniBrojZatvorenogPuta;
        }
    }
}
