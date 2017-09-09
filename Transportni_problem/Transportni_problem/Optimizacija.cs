using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class Optimizacija
    {
        public List<Celija> listaCelija;
        public double sumaAi = 0;
        bool prviProlaz = true;
        int brojIshodista;
        int brojOdredista;
        List<DualnaVarijablaIliIndeks> listaDualnihVarijabli = new List<DualnaVarijablaIliIndeks>();
        Celija najmanjaMinusCelija = null;

        public Optimizacija(List<Celija> listaCelija, int brojIshodista, int brojOdredista)
        {
            this.listaCelija = listaCelija;
            this.brojIshodista = brojIshodista;
            this.brojOdredista = brojOdredista;
        }

        public void MODI()
        {
            while (true)
            {
                listaDualnihVarijabli.Clear();
                PostaviRelativniTrosak(); 
                DualnaVarijablaIliIndeks prvaDualnaVarijabla = new DualnaVarijablaIliIndeks(true, 1, 0);//postavili smo U1 = 0
                listaDualnihVarijabli.Add(prvaDualnaVarijabla);

                IzracunajDualneVarijable();
                IzracunajRelativneTroskove();
                Celija celijaSNjavecimRelativnimTroskom = PronadiNajveciRelativniTrosak();

                if (celijaSNjavecimRelativnimTroskom.relativniTrosak > 0)
                {
                    List<Celija> listaCelijaNaZatvorenomPutu = PronadiZatvoreniPut(celijaSNjavecimRelativnimTroskom);
                    PronadiNajmanjiMinusBroj(listaCelijaNaZatvorenomPutu);
                    PreraspodjeliTeret(listaCelijaNaZatvorenomPutu);
                }
                //else if == 0 znaci da ima jos optimalnih rjesenja 
                else return;
            }
        }

        public void PostaviRelativniTrosak()
        {
            //svim celijama postavljamo vrijednost relativnih troškova na neutralnu vrijednost, a to je bilo koji negativni broj
            foreach (Celija celija in listaCelija)
            {
                celija.relativniTrosak = -1;
            }
        }

        public void IzracunajDualneVarijable()
        {
            double vrijednostNoveDualneVarijable;

            //tako dugo trazimo dualne varijable dok ih nema brojIshodista + brojOdredista
            //moramo staviti while jer je moguce da se u prvom koraku nece moci izracunate sve dualne varijable pa cemo ih imate manje od brojIshodista + brojOdredista
            while (brojIshodista + brojOdredista > listaDualnihVarijabli.ToList().Count)
            {
                foreach (Celija obicnaCelija in listaCelija)//idemo po svim celijama
                {
                    if (obicnaCelija.opis == "Obicna" && obicnaCelija.kolicinaTereta != 0)//gledamo zauzeta polja, tj ona polja kojima je kolicina tereta != 0
                    {
                        foreach (DualnaVarijablaIliIndeks dualnaVarijabla in listaDualnihVarijabli.ToList())//idemo po svim dualni varijablima
                        {
                            if (dualnaVarijabla.red == true)//ako je dualna varijabla u retku
                            {
                                if (obicnaCelija.red == dualnaVarijabla.brojRedaIliStupca)//ako je celija u istom redu kao i dualna varijabla
                                {
                                    vrijednostNoveDualneVarijable = obicnaCelija.stvarniTrosak - dualnaVarijabla.vrijednost;//izracunamo vrijednost nove dualne varijable
                                    //ako je trenutna dualna varijabla u retku, nova ce biti u stupcu, zato je prvi argument false
                                    DualnaVarijablaIliIndeks novaDualnaVarijabla = new DualnaVarijablaIliIndeks(false, obicnaCelija.stupac, vrijednostNoveDualneVarijable);

                                    if (!listaDualnihVarijabli.Contains(novaDualnaVarijabla))
                                    {
                                        listaDualnihVarijabli.Add(novaDualnaVarijabla);//novu dualnu varijablu dodajemo u listu
                                    }

                                }
                            }
                            else//ako je dualna varijabla u stupcu
                            {
                                if (obicnaCelija.stupac == dualnaVarijabla.brojRedaIliStupca)//ako je celija u istom stupcu kao i dualna varijabla
                                {
                                    vrijednostNoveDualneVarijable = obicnaCelija.stvarniTrosak - dualnaVarijabla.vrijednost;//izracunamo vrijednost nove dualne varijable
                                    //ako je trenutna dualna varijabla u stupcu, nova ce biti u retku, zato je prvi argument true
                                    DualnaVarijablaIliIndeks novaDualnaVarijabla = new DualnaVarijablaIliIndeks(true, obicnaCelija.red, vrijednostNoveDualneVarijable);

                                    if (!listaDualnihVarijabli.Contains(novaDualnaVarijabla))
                                    {
                                        listaDualnihVarijabli.Add(novaDualnaVarijabla);//novu dualnu varijablu dodajemo u listu
                                    }
                                }
                            }
                        }
                    }
                    else if (prviProlaz && obicnaCelija.opis == "Ai")//samo za ispis sume
                    {
                        sumaAi += obicnaCelija.stvarniTrosak;
                    }
                }
                prviProlaz = false;
            }
        }

        public void IzracunajRelativneTroskove()
        {
            double vrijednostDualneVarijablePoRetku = 0;
            double vrijednostDualneVarijablePoStupcu = 0;

            foreach (Celija obicnaCelija in listaCelija)
            {
                if (obicnaCelija.opis == "Obicna" && obicnaCelija.kolicinaTereta == 0)
                {
                    foreach (DualnaVarijablaIliIndeks dualnaVarijabla in listaDualnihVarijabli)
                    {
                        if (dualnaVarijabla.red == true)
                        {
                            if (obicnaCelija.red == dualnaVarijabla.brojRedaIliStupca)
                            {
                                vrijednostDualneVarijablePoRetku = dualnaVarijabla.vrijednost;
                            }
                        }
                        else
                        {
                            if (obicnaCelija.stupac == dualnaVarijabla.brojRedaIliStupca)
                            {
                                vrijednostDualneVarijablePoStupcu = dualnaVarijabla.vrijednost;
                            }
                        }
                    }
                    obicnaCelija.relativniTrosak = (vrijednostDualneVarijablePoRetku + vrijednostDualneVarijablePoStupcu) - obicnaCelija.stvarniTrosak;
                }
            }
        }

        public Celija PronadiNajveciRelativniTrosak()
        {
            List<Celija> sortiranaListaCelija = listaCelija.OrderByDescending(x => x.relativniTrosak).ToList();
            return sortiranaListaCelija[0];
        }

        public List<Celija> PronadiZatvoreniPut(Celija celijaSNjavecimRelativnimTroskom)
        {
            List<Celija> listaCelijaNaZatvorenomPutu = new List<Celija>();
            celijaSNjavecimRelativnimTroskom.plus = true;
            celijaSNjavecimRelativnimTroskom.kolicinaTereta = -1;//namjerno stavljam ovdje -1, iako to nije bas pravilni nacin
            listaCelijaNaZatvorenomPutu.Add(celijaSNjavecimRelativnimTroskom);

            bool red = true;
            bool postojiCelijaUStupcuIliRetku = false;

            while (true)
            {
                postojiCelijaUStupcuIliRetku = false;
                foreach (Celija celija in listaCelija)
                {
                    if (red == true)
                    {
                        //zapocinjemo traziti zatvoreni put s celijom koja je u istom redu kao i celijaSNjavecimRelativnimTroskom (listaCelijaNaZAtvorenomPutu.Last()) i ta celija mora imati kolicinuTereta != 0
                        //celija koja je u istom redu s zadnje dodanom celijom ne smije biti ona sama te mora biti oznacena kao celija na dobrom putu
                        if (celija != listaCelijaNaZatvorenomPutu.Last() && celija.opis == "Obicna" && celija.kolicinaTereta != 0 && celija.red == listaCelijaNaZatvorenomPutu.Last().red && celija.dobarPut)
                        {
                            if (listaCelijaNaZatvorenomPutu.Contains(celija))
                            {
                                return listaCelijaNaZatvorenomPutu;
                            }
                            celija.plus = false;
                            listaCelijaNaZatvorenomPutu.Add(celija);
                            postojiCelijaUStupcuIliRetku = true;
                            red = !red;
                            break;
                        }
                    }
                    else
                    {
                        if (celija != listaCelijaNaZatvorenomPutu.Last() && celija.opis == "Obicna" && celija.kolicinaTereta != 0 && celija.stupac == listaCelijaNaZatvorenomPutu.Last().stupac && celija.dobarPut)
                        {
                            if (listaCelijaNaZatvorenomPutu.Contains(celija))
                            {
                                return listaCelijaNaZatvorenomPutu;
                            }
                            celija.plus = true;
                            listaCelijaNaZatvorenomPutu.Add(celija);
                            postojiCelijaUStupcuIliRetku = true;
                            red = !red;
                            break;
                        }
                    }
                }

                if (!postojiCelijaUStupcuIliRetku)
                {
                    red = !red;
                    listaCelijaNaZatvorenomPutu.Last().dobarPut = false;
                    listaCelijaNaZatvorenomPutu.Remove(listaCelijaNaZatvorenomPutu.Last());

                }
            }
        }

        public void PronadiNajmanjiMinusBroj(List<Celija> listaCelijaNaZatvorenomPutu)
        {
            List<Celija> listaMinusCelija = new List<Celija>();

            foreach (Celija celija in listaCelijaNaZatvorenomPutu)
            {
                if (celija.plus == false)//trazimo celije koje su oznacene kao minus
                {
                    listaMinusCelija.Add(celija);
                }
            }

            listaMinusCelija = listaMinusCelija.OrderBy(x => x.kolicinaTereta).ToList();
            najmanjaMinusCelija = listaMinusCelija[0];//najmanja minus celija se nalazi prva u sortiranoj listi
        }

        public void PreraspodjeliTeret(List<Celija> listaCelijaNaZatvorenomPutu)
        {
            double kolicinaTeretaZaPremjestanje = najmanjaMinusCelija.kolicinaTereta;

            foreach (Celija celija in listaCelijaNaZatvorenomPutu)
            {
                if (celija.plus == true)
                {
                    if (celija.kolicinaTereta == -1)//to je celija s najvecim relativnim troskom, kojoj sam namjerno stavil -1 za kolicinu tereta, a zapravo je ta kolicina bila 0
                    {
                        celija.kolicinaTereta += kolicinaTeretaZaPremjestanje + 1;
                    }
                    else
                    {
                        celija.kolicinaTereta += kolicinaTeretaZaPremjestanje;
                    }
                }
                else
                {
                    celija.kolicinaTereta -= kolicinaTeretaZaPremjestanje;
                }
            }
        }

        public void Kamen()
        {

        }
    }
}
