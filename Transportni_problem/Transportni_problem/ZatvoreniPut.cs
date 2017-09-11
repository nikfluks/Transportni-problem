using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class ZatvoreniPut
    {
        public List<Celija> listaCelijaNaZatvorenomPutu;
        public double maxKolicinaTereta;//maksimalna kolicina tereta koju mozemo prenijeti po zatvorenom putu, kada imamo vise zatvorenih puteva
        public int redniBrojZatvorenogPuta;

        public ZatvoreniPut(List<Celija> listaCelijaNaZatvorenomPutu, int redniBrojZatvorenogPuta)
        {
            this.listaCelijaNaZatvorenomPutu = listaCelijaNaZatvorenomPutu;
            this.redniBrojZatvorenogPuta = redniBrojZatvorenogPuta;
        }
    }
}
