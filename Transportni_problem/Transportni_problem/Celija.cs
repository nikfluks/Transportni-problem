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
        public double maxKolicinaTereta;//maksimalna kolicina tereta koju celija moze prenesti kod Vogela kada imamo vise najmanjih troskova
        public double relativniTrosak;
        public bool dobarPut = true; //na pocetku predpostavimo da su sve celije na dobrom putu kada trazimo zatvoreni put MODI metodom
        public bool plus = false;

        public Celija(string opis, int red, int stupac, double stvarniTrosak)
        {
            this.opis = opis;
            this.red = red;
            this.stupac = stupac;
            this.stvarniTrosak = stvarniTrosak;
        }

        public override bool Equals(object obj)
        {
            Celija celija = obj as Celija;
            if (celija.opis == opis && celija.red == red && celija.stupac == stupac && celija.stvarniTrosak == stvarniTrosak && celija.kolicinaTereta == kolicinaTereta
                && celija.maxKolicinaTereta == maxKolicinaTereta && celija.relativniTrosak == relativniTrosak && celija.dobarPut == dobarPut && celija.plus == plus)
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
