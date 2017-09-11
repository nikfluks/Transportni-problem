using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class Optimizacija
    {
        public List<Celija> listaCelija;
        public double sumaAi = 0;
        public double brojOptimalnihRjesenja;
        bool prviProlaz = true;
        int brojIshodista;
        int brojOdredista;
        List<DualnaVarijablaIliIndeks> listaDualnihVarijabli = new List<DualnaVarijablaIliIndeks>();

        public Optimizacija(List<Celija> listaCelija, int brojIshodista, int brojOdredista)
        {
            this.listaCelija = listaCelija;
            this.brojIshodista = brojIshodista;
            this.brojOdredista = brojOdredista;
        }

        public void MODI()
        {
            bool pronadenoOptimalnoRjesenje = false;

            while (!pronadenoOptimalnoRjesenje)
            {
                listaDualnihVarijabli.Clear();
                PostaviRelativniTrosak();
                IsprazniListuPluseva();
                ProvjeriZauzetaPolja();

                ProvjeriDegeneraciju();

                IzracunajDualneVarijable();
                IzracunajRelativneTroskove();
                List<Celija> listaCelijaSNjavecimRelativnimTroskom = PronadiNajveciPozitivniRelativniTrosak();

                if (listaCelijaSNjavecimRelativnimTroskom != null)//ako je lista != null znaci da imamo barem 1 pozitivni relativni trosak
                {
                    //pronalazimo sve zatvorene puteve (ako imamo vise celija s najvecim poz rel troskom, imamo i vise zatvorenih puteva)
                    List<ZatvoreniPut> listaZatvorenihPuteva = PronadiZatvoreniPut(listaCelijaSNjavecimRelativnimTroskom);
                    ZatvoreniPut zatvoreniPutSMaxTeret;

                    if (listaZatvorenihPuteva.Count == 1)//postoji samo 1 najveci poz rel trosak, pa postoji i samo 1 zatvoreni put
                    {
                        zatvoreniPutSMaxTeret = listaZatvorenihPuteva[0];
                    }
                    else//inace imamo vise zatvorenih puteva, pa treba naci zatvoreni put koji moze prenijeti najvise tereta
                    {
                        zatvoreniPutSMaxTeret = PronadiNajveciTeret(listaZatvorenihPuteva);
                    }
                    PreraspodjeliTeret(zatvoreniPutSMaxTeret);
                }

                else//ako je lista == null onda je(su) najveci relativnim trosak negativni, pa tu zavrsava MODI metoda
                {
                    pronadenoOptimalnoRjesenje = true;
                }
            }

            PronadiBrojOptimalnihRjesenja();
        }

        public void PronadiBrojOptimalnihRjesenja()//trazimo broj nula na Cij* poziciji, gledaju se samo nezauzete nule; broj optimalnih rjesenja je 2^brojNula
        {
            int brojNula = 0;

            foreach (Celija celija in listaCelija)
            {
                if (celija.opis == "Obicna" && celija.zauzetoPolje == false && celija.relativniTrosak == 0)
                {
                    brojNula++;
                }
            }

            brojOptimalnihRjesenja = Math.Pow(2, brojNula);
        }

        public int BrojZauzetihPolja()
        {
            int brojZauzetihPolja = 0;

            foreach (Celija celija in listaCelija)//trazimo broj zauzetih polja
            {
                if (celija.zauzetoPolje == true)
                {
                    brojZauzetihPolja++;
                }
            }

            return brojZauzetihPolja;
        }

        public void ProvjeriDegeneraciju()
        {
            int brojZauzetihPolja = BrojZauzetihPolja();

            while (brojZauzetihPolja < (brojIshodista + brojOdredista - 1))//trazimo u while petplji za slucaj da nam fali vise od 1 zauzeto polje
            {
                //ako je brojZauzetihPolja < (red+stupac-1) tj broj zaueih polja je manji od ranga,
                //onda imamo degeneraciju pa moramo jos neko polje oznaciti kao zauzeto i dodati mu teret = 0
                //PREPORUKA je polje s najmanjim troskom oznaciti kao zauzeto

                List<Celija> listaSortiranihCelija = listaCelija.OrderBy(x => x.stvarniTrosak).ToList();
                foreach (Celija celija in listaSortiranihCelija)
                {
                    if (celija.opis == "Obicna" && celija.zauzetoPolje == false)
                    {
                        celija.zauzetoPolje = true;
                        celija.kolicinaTereta = 0;
                        break;
                    }
                }

                brojZauzetihPolja = BrojZauzetihPolja();
            }
        }

        //metoda iz liste zatvorenih puteva, trazi onaj zatvoreni put na kojem mozemo prenijeti najvise tereta
        public ZatvoreniPut PronadiNajveciTeret(List<ZatvoreniPut> listaZatvorenihPuteva)
        {
            List<Celija> listaCelijaNaMinusPoljima = new List<Celija>();

            foreach (ZatvoreniPut zatvoreniPut in listaZatvorenihPuteva)
            {
                listaCelijaNaMinusPoljima.Clear();

                foreach (Celija celija in zatvoreniPut.listaCelijaNaZatvorenomPutu)
                {
                    foreach (Predznak predznak in celija.predznak)
                    {
                        if (predznak.plus == false && predznak.redniBrojZatvorenogPuta == zatvoreniPut.redniBrojZatvorenogPuta)
                        {
                            listaCelijaNaMinusPoljima.Add(celija);
                        }
                    }
                }
                zatvoreniPut.maxKolicinaTereta = listaCelijaNaMinusPoljima.OrderBy(x => x.kolicinaTereta).First().kolicinaTereta;
            }

            ZatvoreniPut zatvoreniPutSMaxTeret = listaZatvorenihPuteva.OrderByDescending(x => x.maxKolicinaTereta).First();
            return zatvoreniPutSMaxTeret;
        }

        public void IsprazniListuPluseva()
        {
            foreach (Celija celija in listaCelija)
            {
                celija.predznak.Clear();
            }
        }

        public void PostaviRelativniTrosak()
        {
            //na pocetku svim celijama postavljamo vrijednost relativnih troškova na neutralnu vrijednost, a to je bilo koji negativni broj
            foreach (Celija celija in listaCelija)
            {
                celija.relativniTrosak = -1;
            }
        }

        public void PostaviDobarPut()
        {
            //na pocetku svim celijama postavljamo oznaku da su na dobrom putu
            foreach (Celija celija in listaCelija)
            {
                celija.dobarPut = true;
            }
        }

        public void ProvjeriZauzetaPolja()
        {
            foreach (Celija celija in listaCelija)
            {
                if (celija.zauzetoPolje)
                {
                    if (celija.kolicinaTereta <= 0)
                    {
                        celija.zauzetoPolje = false;
                    }
                }
            }
        }

        public void IzracunajDualneVarijable()
        {
            DualnaVarijablaIliIndeks prvaDualnaVarijabla = new DualnaVarijablaIliIndeks(true, 1, 0);//postavili smo U1 = 0
            listaDualnihVarijabli.Add(prvaDualnaVarijabla);

            double vrijednostNoveDualneVarijable;

            //tako dugo trazimo dualne varijable dok ih nema brojIshodista + brojOdredista
            //moramo staviti while jer je moguce da se u prvom koraku nece moci izracunate sve dualne varijable pa cemo ih imate manje od brojIshodista + brojOdredista
            while (brojIshodista + brojOdredista > listaDualnihVarijabli.ToList().Count)
            {
                foreach (Celija obicnaCelija in listaCelija)//idemo po svim celijama
                {
                    if (obicnaCelija.opis == "Obicna" && obicnaCelija.zauzetoPolje == true)//gledamo zauzeta polja, tj ona polja kojima je kolicina tereta != 0
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
                                        break;
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
                                        break;
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
                if (obicnaCelija.opis == "Obicna" && obicnaCelija.zauzetoPolje == false)
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

        public List<Celija> PronadiNajveciPozitivniRelativniTrosak()
        {
            //sortiramo listu tako da su na početku celije s najvecim relativnim troskom
            List<Celija> sortiranaListaCelija = listaCelija.OrderByDescending(x => x.relativniTrosak).ToList();

            if (sortiranaListaCelija[0].relativniTrosak > 0)//provjeravamo ima li celija s najvecim relativnim troskom pozitivan relativni trosak
            {
                //odaberemo sve celije koje imaju najveci relativni trosak (moze ih biti 1 ili vise)
                List<Celija> listaCelijaSNjavecimRelativnimTroskom = sortiranaListaCelija.FindAll(x => x.relativniTrosak == sortiranaListaCelija[0].relativniTrosak);
                //vratimo listu gdje se nalaze samo celije s najvecim relativnim troskom
                return listaCelijaSNjavecimRelativnimTroskom;
            }
            //TODO else if sortiranaListaCelija[0].relativniTrosak == 0, imamo vise optimlanih rjesenja -> postaviti neku globalnu varijablu 
            else
            {
                return null;
            }
        }

        public List<ZatvoreniPut> PronadiZatvoreniPut(List<Celija> listaCelijaSNjavecimRelativnimTroskom)
        {
            List<ZatvoreniPut> listaZatvorenihPuteva = new List<ZatvoreniPut>();
            int redniBrojZatvorenogPuta = 0;

            foreach (Celija celijaSNjavecimRelativnimTroskom in listaCelijaSNjavecimRelativnimTroskom)
            {
                PostaviDobarPut();
                List<Celija> listaCelijaNaZatvorenomPutu = new List<Celija>();

                Predznak novi1 = new Predznak(true, redniBrojZatvorenogPuta);
                celijaSNjavecimRelativnimTroskom.predznak.Add(novi1);
                celijaSNjavecimRelativnimTroskom.zauzetoPolje = true;
                listaCelijaNaZatvorenomPutu.Add(celijaSNjavecimRelativnimTroskom);

                bool red = true;
                bool postojiCelijaUStupcuIliRetku = false;
                bool pronadenzatvoreniPut = false;

                while (!pronadenzatvoreniPut)
                {
                    postojiCelijaUStupcuIliRetku = false;
                    foreach (Celija celija in listaCelija)
                    {
                        if (red == true)
                        {
                            //zapocinjemo traziti zatvoreni put s celijom koja je u istom redu kao i celijaSNjavecimRelativnimTroskom (listaCelijaNaZAtvorenomPutu.Last()) i ta celija mora imati kolicinuTereta != 0
                            //celija koja je u istom redu s zadnje dodanom celijom ne smije biti ona sama te mora biti oznacena kao celija na dobrom putu
                            if (celija != listaCelijaNaZatvorenomPutu.Last() && celija.opis == "Obicna" && celija.zauzetoPolje == true && celija.red == listaCelijaNaZatvorenomPutu.Last().red && celija.dobarPut)
                            {
                                postojiCelijaUStupcuIliRetku = true;

                                if (listaCelijaNaZatvorenomPutu.Contains(celija))//vratili smo se do celijaSNjavecimRelativnimTroskom, tj nasli smo zatvoreni put
                                {
                                    pronadenzatvoreniPut = true;
                                    ZatvoreniPut noviZatvoreniPut = new ZatvoreniPut(listaCelijaNaZatvorenomPutu, redniBrojZatvorenogPuta);
                                    listaZatvorenihPuteva.Add(noviZatvoreniPut);
                                    redniBrojZatvorenogPuta++;
                                    break;//jer smo nasli zatvoreni put
                                }

                                Predznak novi2 = new Predznak(false, redniBrojZatvorenogPuta);
                                celija.predznak.Add(novi2);
                                listaCelijaNaZatvorenomPutu.Add(celija);
                                red = !red;
                                break;//cisto radi toga da se krene od prve celije (1,1)
                            }
                        }
                        else
                        {
                            if (celija != listaCelijaNaZatvorenomPutu.Last() && celija.opis == "Obicna" && celija.zauzetoPolje == true && celija.stupac == listaCelijaNaZatvorenomPutu.Last().stupac && celija.dobarPut)
                            {
                                postojiCelijaUStupcuIliRetku = true;

                                if (listaCelijaNaZatvorenomPutu.Contains(celija))
                                {
                                    pronadenzatvoreniPut = true;
                                    ZatvoreniPut noviZatvoreniPut = new ZatvoreniPut(listaCelijaNaZatvorenomPutu, redniBrojZatvorenogPuta);
                                    listaZatvorenihPuteva.Add(noviZatvoreniPut);
                                    redniBrojZatvorenogPuta++;
                                    break;
                                }

                                Predznak novi3 = new Predznak(true, redniBrojZatvorenogPuta);
                                celija.predznak.Add(novi3);
                                listaCelijaNaZatvorenomPutu.Add(celija);
                                red = !red;
                                break;
                            }
                        }
                    }

                    if (!postojiCelijaUStupcuIliRetku)//brise zadnje dodanu celiju ako nije na dobro putu, tj preko nje se ne moze niti do jedne druge zauzete celije
                    {
                        red = !red;
                        listaCelijaNaZatvorenomPutu.Last().dobarPut = false;
                        listaCelijaNaZatvorenomPutu.Last().predznak.Clear();
                        listaCelijaNaZatvorenomPutu.Remove(listaCelijaNaZatvorenomPutu.Last());

                    }
                }
            }
            return listaZatvorenihPuteva;
        }

        public Celija PronadiNajmanjuMinusCeliju(ZatvoreniPut zatvoreniPutSMaxTeret)
        {
            List<Celija> listaMinusCelija = new List<Celija>();

            foreach (Celija celija in zatvoreniPutSMaxTeret.listaCelijaNaZatvorenomPutu)
            {
                foreach (Predznak predznak in celija.predznak)
                {
                    if (predznak.plus == false && predznak.redniBrojZatvorenogPuta == zatvoreniPutSMaxTeret.redniBrojZatvorenogPuta)
                    {
                        listaMinusCelija.Add(celija);
                    }
                }
            }

            //List<Celija> listaMinusCelija = zatvoreniPutSMaxTeret.listaCelijaNaZatvorenomPutu.FindAll(x => x.plus.FindAll(y => y.plus == false));
            listaMinusCelija = listaMinusCelija.OrderBy(x => x.kolicinaTereta).ToList();//ako ima vise najmanjih minus celija -> DEGENERACIJA
            return listaMinusCelija[0];//najmanja minus celija se nalazi prva u sortiranoj listi
        }

        public void PreraspodjeliTeret(ZatvoreniPut zatvoreniPutSMaxTeret)
        {
            Celija najmanjaMinusCelija = PronadiNajmanjuMinusCeliju(zatvoreniPutSMaxTeret);

            double kolicinaTeretaZaPremjestanje = najmanjaMinusCelija.kolicinaTereta;

            foreach (Celija celija in zatvoreniPutSMaxTeret.listaCelijaNaZatvorenomPutu)
            {
                foreach (Predznak predznak in celija.predznak)
                {
                    if (predznak.plus == true && predznak.redniBrojZatvorenogPuta == zatvoreniPutSMaxTeret.redniBrojZatvorenogPuta)
                    {
                        celija.kolicinaTereta += kolicinaTeretaZaPremjestanje;
                    }
                    else if (predznak.plus == false && predznak.redniBrojZatvorenogPuta == zatvoreniPutSMaxTeret.redniBrojZatvorenogPuta)
                    {
                        celija.kolicinaTereta -= kolicinaTeretaZaPremjestanje;
                    }
                }
            }
        }

        public void Kamen()
        {

        }
    }
}
