using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportni_problem
{
    public class PocetniRaspored
    {
        public List<Celija> listaCelija;
        int brojIshodista;
        int brojOdredista;
        public double sumaAi;

        public PocetniRaspored(List<Celija> listaCelija, int brojIshodista, int brojOdredista)
        {
            this.listaCelija = listaCelija;
            this.brojIshodista = brojIshodista;
            this.brojOdredista = brojOdredista;
            this.sumaAi = 0;
        }

        public void SjeveroZapadniKut()
        {
            double Ai = 0;
            double Bj = 0;
            double sumaKolicinePoRedu;
            double sumaKolicinePoStupcu;
            bool prviProlaz = true;

            for (int r = 1; r <= brojIshodista; r++)//idemo po svim celijama pocevsi s gornjom lijevom
            {
                for (int s = 1; s <= brojOdredista; s++)//i završavamo s donjom desnom
                {
                    foreach (Celija obicnaCelija in listaCelija)
                    {
                        sumaKolicinePoRedu = 0;
                        sumaKolicinePoStupcu = 0;

                        if (obicnaCelija.opis == "Obicna" && obicnaCelija.red == r && obicnaCelija.stupac == s)
                        {
                            //int i = 0;
                            foreach (Celija celija in listaCelija)
                            {
                                //i++;
                                if (celija.opis == "Ai" && celija.red == obicnaCelija.red)//trazimo kapacitet ishodista za taj red
                                {
                                    Ai = celija.stvarniTrosak;
                                }

                                if (celija.opis == "Bj" && celija.stupac == obicnaCelija.stupac)//trazimo potrebe odredista za taj stupac
                                {
                                    Bj = celija.stvarniTrosak;
                                }

                                if (celija.opis == "Obicna" && celija.red == obicnaCelija.red)
                                {
                                    sumaKolicinePoRedu += celija.kolicinaTereta;
                                }

                                if (celija.opis == "Obicna" && celija.stupac == obicnaCelija.stupac)
                                {
                                    sumaKolicinePoStupcu += celija.kolicinaTereta;
                                }

                                //trazimo sumu kapaciteta ishodista
                                //isto se moze napraviviti i za potrebe odredista i usporediti jesu li te vrijedosti iste (ZTP vs OTP) 
                                if (prviProlaz && celija.opis == "Ai")
                                {
                                    sumaAi += celija.stvarniTrosak;
                                }
                            }
                            prviProlaz = false;
                            //provjeravamo jesu li potrebe odredista manje od kapaciteta ishodista umanjene za potrosenu robu po tom ishodistu (redu)
                            //ako jesu znaci da imamo dovoljno robe da zadovoljimo potrebe odredista i to upisujemo

                            //Bj = potreba
                            //Ai - sumaKolicinePoRedu = trenutna raspolozivost
                            if (Bj < Ai - sumaKolicinePoRedu)
                            {
                                obicnaCelija.kolicinaTereta = Bj;
                            }

                            //inace upisujemo maximalno koliko mozemo dati (koliko nam je robe ostalo u tom ishodistu) 
                            //a to je pocetni kapacitet umanjen za ono sto smo potrosili po tom ishodistu
                            else
                            {
                                obicnaCelija.kolicinaTereta = Ai - sumaKolicinePoRedu;
                            }

                            //na kraju jos provjeravamo da nismo previse upisali po stupcu, ako jesmo onda to korigiramo
                            //na nacin da provjerimo je li trenutno zapisana kolicina veca od razlike potrebe odredista i kolicine robe koja je vec dopremljena tom odredistu

                            //obicnaCelija.kolicinaTereta = vec zapisana kolicina tereta
                            //Bj - sumaKolicinePoStupcu = potreba
                            if (obicnaCelija.kolicinaTereta > Bj - sumaKolicinePoStupcu)
                            {
                                obicnaCelija.kolicinaTereta = Bj - sumaKolicinePoStupcu;
                            }

                            break;
                        }
                    }
                }
            }
        }

        public void MinTrosak()
        {
            double Ai = 0;
            double Bj = 0;
            double sumaKolicinePoRedu;
            double sumaKolicinePoStupcu;
            bool prviProlaz = true;

            List<Celija> listaCelijaSortirana = listaCelija.OrderBy(x => x.stvarniTrosak).ToList();

            foreach (Celija obicnaCelija in listaCelijaSortirana)//idemo po svim celija, pocevsi od one s najmanjim troskom
            {
                sumaKolicinePoRedu = 0;
                sumaKolicinePoStupcu = 0;

                if (obicnaCelija.opis == "Obicna")
                {
                    foreach (Celija celija in listaCelija)
                    {
                        if (celija.opis == "Ai" && celija.red == obicnaCelija.red)//trazimo kapacitet ishodista za taj red
                        {
                            Ai = celija.stvarniTrosak;
                        }

                        if (celija.opis == "Bj" && celija.stupac == obicnaCelija.stupac)//trazimo potrebe odredista za taj stupac
                        {
                            Bj = celija.stvarniTrosak;
                        }

                        if (celija.opis == "Obicna" && celija.red == obicnaCelija.red)
                        {
                            sumaKolicinePoRedu += celija.kolicinaTereta;
                        }

                        if (celija.opis == "Obicna" && celija.stupac == obicnaCelija.stupac)
                        {
                            sumaKolicinePoStupcu += celija.kolicinaTereta;
                        }

                        if (prviProlaz && celija.opis == "Ai")
                        {
                            sumaAi += celija.stvarniTrosak;
                        }
                    }

                    prviProlaz = false;

                    if (Bj < Ai - sumaKolicinePoRedu)
                    {
                        obicnaCelija.kolicinaTereta = Bj;
                    }

                    else
                    {
                        obicnaCelija.kolicinaTereta = Ai - sumaKolicinePoRedu;
                    }

                    if (obicnaCelija.kolicinaTereta > Bj - sumaKolicinePoStupcu)
                    {
                        obicnaCelija.kolicinaTereta = Bj - sumaKolicinePoStupcu;
                    }
                }
            }
        }

        public void Vogel()
        {

        }
    }
}
