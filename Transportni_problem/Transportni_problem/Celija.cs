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
        public bool zauzetoPolje = false;//oznacimo je li polje zauzeto (kolicina tereta != 0), da bi ih kasnije mogli prebrojiti i odrediti imamo li degeneraciju
        public List<Predznak> predznak = new List<Predznak>();
        //public bool degenerirarana = false;
        
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
            if (celija.opis == opis && celija.red == red && celija.stupac == stupac)
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
