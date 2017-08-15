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

        public PocetniRaspored(List<Celija> listaCelija, int brojIshodista, int brojOdredista)
        {
            this.listaCelija = listaCelija;
            this.brojIshodista = brojIshodista;
            this.brojOdredista = brojOdredista;
        }

        public void SjeveroZapadniKut()
        {
            int r = 1;
            int s = 1;
            double Ai = 0;
            double Bj = 0;
            double sumaKolicinePoRedu;
            double sumaKolicinePoStupcu;

            foreach (Celija obicnaCelija in listaCelija)
            {
                sumaKolicinePoRedu = 0;
                sumaKolicinePoStupcu = 0;

                if (obicnaCelija.opis == "Obicna" && obicnaCelija.red == r && obicnaCelija.stupac == s)//idemo po svim celijama pocevsi s gornjom lijevom, a završavamo s donjom desnom
                {
                    int i = 0;
                    foreach (Celija celija in listaCelija)
                    {
                        i++;
                        if (celija.opis == "Ai" && celija.red == r)//trazimo kapacitet ishodista za taj red
                        {
                            Ai = celija.stvarniTrosak;
                        }

                        if (celija.opis == "Bj" && celija.stupac == s)//trazimo potrebe odredista za taj stupac
                        {
                            Bj = celija.stvarniTrosak;
                        }

                        if (celija.opis == "Obicna" && celija.red == r)
                        {
                            sumaKolicinePoRedu += celija.kolicinaTereta;
                        }

                        if (celija.opis == "Obicna" && celija.stupac == s)
                        {
                            sumaKolicinePoStupcu += celija.kolicinaTereta;
                        }
                    }

                    //provjeravamo jesu li potrebe odredista manje od kapaciteta ishodista umanjene za potrosenu robu po tom ishodistu (redu)
                    //ako jesu znaci da imamo dovoljno robe da zadovoljimo potrebe odredista i to upisujemo
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
                    if (obicnaCelija.kolicinaTereta > Bj - sumaKolicinePoStupcu)
                    {
                        obicnaCelija.kolicinaTereta = Bj - sumaKolicinePoStupcu;
                    }

                    if (s < brojOdredista)//pomicemo se udesno po stupcima ako nismo dosli do kraja
                    {
                        s++;
                    }

                    else if (r + 1 <= brojIshodista) //kad dodemo do kraja idemo na sljedeci red, a stupac vracamo na pocetak, ako imamo jos redova
                    {
                        s = 1;
                        r++;
                    }

                    else
                    {
                        return;
                    }
                }
            }
        }

        public void MinTrosak()
        {

        }

        public void Vogel()
        {

        }
    }
}
