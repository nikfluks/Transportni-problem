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
        public double sumaAi = 0;
        double Ai = 0;
        double Bj = 0;
        double sumaKolicinePoRedu = 0;
        double sumaKolicinePoStupcu = 0;
        List<ProvjereniRjesenost> listaRjesenih = new List<ProvjereniRjesenost>();
        bool prviProlaz = true;
        Celija zadnjaCelija = null;

        public PocetniRaspored(List<Celija> listaCelija, int brojIshodista, int brojOdredista)
        {
            this.listaCelija = listaCelija;
            this.brojIshodista = brojIshodista;
            this.brojOdredista = brojOdredista;
        }

        public void SjeveroZapadniKut()
        {
            for (int r = 1; r <= brojIshodista; r++)//idemo po svim celijama pocevsi s gornjom lijevom
            {
                for (int s = 1; s <= brojOdredista; s++)//i završavamo s donjom desnom
                {
                    foreach (Celija obicnaCelija in listaCelija)
                    {
                        if (obicnaCelija.opis == "Obicna" && obicnaCelija.red == r && obicnaCelija.stupac == s)
                        {
                            ZapisiKolicinuTereta(obicnaCelija);
                            break;
                        }
                    }
                }
            }
        }

        public void MinTrosak()
        {
            listaRjesenih.Clear();
            List<Celija> listaCelijaSortirana = listaCelija.OrderBy(x => x.stvarniTrosak).ToList();

            foreach (Celija obicnaCelija in listaCelijaSortirana)//uzimamo redom sve celije, pocevsi od one s najmanjim troskom
            {
                if (obicnaCelija.opis == "Obicna" /*&& !ProvjeriJeLiCelijaRjesena(obicnaCelija)*/)
                {
                    //u listaNajmanjihTrosokva spremamo sve celije koje imaju isti trosak kao i promatrana celija
                    List<Celija> listaNajmanjihTrosokva = listaCelijaSortirana.FindAll(x => x.stvarniTrosak == obicnaCelija.stvarniTrosak);
                    Celija najmanjiTrosak;

                    if (listaNajmanjihTrosokva.Count == 1)//ako je samo jedna celija s najmanji troskom, uzmemo ju
                    {
                        najmanjiTrosak = listaNajmanjihTrosokva[0];
                    }
                    else//ako ih je vise, uzimamo onu koja moze prenijeti najvise tereta
                    {
                        najmanjiTrosak = PronadiNajveciTeret(listaNajmanjihTrosokva);
                    }
                    ZapisiKolicinuTereta(najmanjiTrosak);
                    ProvjeriRjesenostCelije(najmanjiTrosak);
                }
            }
        }

        public void Vogel()
        {
            listaRjesenih.Clear();
            for (int i = 0; i < brojIshodista + brojOdredista - 1; i++)
            {
                List<DualnaVarijablaIliIndeks> listaNajvecihIndeksa = IzracunajNajveciIndeks();

                //ako zadnja celija vise nije null, dosli smo do zadnje celije i samo trebamo za nju izracunati kolicinu tereta i zavrsavamo
                //isto bi bilo i if(listaNajvecihIndeksa.Count == 0) jer dobivamo praznu listu
                if (zadnjaCelija != null)
                {
                    ZapisiKolicinuTereta(zadnjaCelija);
                    return;
                }

                List<Celija> listaNajmanjihTroskova = PronadiNajmanjiTrosak(listaNajvecihIndeksa);
                Celija najmanjiTrosak;

                if (listaNajmanjihTroskova.Count == 1)//postoji samo 1 minimalni trosak
                {
                    najmanjiTrosak = listaNajmanjihTroskova[0];
                }

                else
                {
                    najmanjiTrosak = PronadiNajveciTeret(listaNajmanjihTroskova);
                }

                ZapisiKolicinuTereta(najmanjiTrosak);
                ProvjeriRjesenostCelije(najmanjiTrosak);
            }
        }

        public void ZapisiKolicinuTereta(Celija obicnaCelija)
        {
            if (obicnaCelija.kolicinaTereta <= 0)
            {


                sumaKolicinePoRedu = 0;
                sumaKolicinePoStupcu = 0;
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

                    //trazimo sumu kapaciteta ishodista
                    //isto se moze napraviviti i za potrebe odredista i usporediti jesu li te vrijedosti iste (ZTP vs OTP) 
                    if (prviProlaz && celija.opis == "Ai")
                    {
                        sumaAi += celija.stvarniTrosak;
                    }
                }
                prviProlaz = false;

                double trenutniKapacitet = Ai - sumaKolicinePoRedu;//trenutni kapacitet ishodista/retka
                double trenutnePotrebe = Bj - sumaKolicinePoStupcu;//trenutne potrebe odredista/stupca

                //trazimo manji od ta 2 broja te njega upisujemo kao kolicinu tereta za tu celiju
                if (trenutniKapacitet < trenutnePotrebe)
                {
                    obicnaCelija.kolicinaTereta = trenutniKapacitet;

                    if (trenutniKapacitet > 0)//ako smo u kolicinu upisali neki poz broj, tj broj koji nije 0, oznacimo celiju kao zauzetu
                    {
                        obicnaCelija.zauzetoPolje = true;
                    }
                }

                else
                {
                    obicnaCelija.kolicinaTereta = trenutnePotrebe;

                    if (trenutnePotrebe > 0)
                    {
                        obicnaCelija.zauzetoPolje = true;
                    }
                }
            }
        }

        //metoda nam iz liste celija, koja sadrzi celije s najmanjim troskovima,
        //vraca celiju koja moze prenjeti najvise tereta
        public Celija PronadiNajveciTeret(List<Celija> listaNajmanjihTroskova)
        {
            foreach (Celija najmanjiTrosak in listaNajmanjihTroskova)
            {
                sumaKolicinePoRedu = 0;
                sumaKolicinePoStupcu = 0;

                foreach (Celija celija in listaCelija)
                {
                    if (celija.opis == "Ai" && celija.red == najmanjiTrosak.red)
                    {
                        Ai = celija.stvarniTrosak;
                    }

                    if (celija.opis == "Bj" && celija.stupac == najmanjiTrosak.stupac)
                    {
                        Bj = celija.stvarniTrosak;
                    }

                    if (celija.opis == "Obicna" && celija.red == najmanjiTrosak.red)
                    {
                        sumaKolicinePoRedu += celija.kolicinaTereta;
                    }

                    if (celija.opis == "Obicna" && celija.stupac == najmanjiTrosak.stupac)
                    {
                        sumaKolicinePoStupcu += celija.kolicinaTereta;
                    }
                }

                double trenutniKapacitet = Ai - sumaKolicinePoRedu;
                double trenutnePotrebe = Bj - sumaKolicinePoStupcu;

                if (trenutniKapacitet < trenutnePotrebe)
                {
                    najmanjiTrosak.maxKolicinaTereta = trenutniKapacitet;
                }

                else
                {
                    najmanjiTrosak.maxKolicinaTereta = trenutnePotrebe;
                }
            }
            //listaNajmanjihTroskova se sortira tako da je najveci moguci teret na pocetku liste
            listaNajmanjihTroskova = listaNajmanjihTroskova.OrderByDescending(x => x.maxKolicinaTereta).ToList();

            //vracamo celiju koja moze prevesti najvise tereta, tj prvu celiju u tako sortiranoj listi
            //ako ih vise moze prevesti istu kolicinu tereta, onda nam nije ni bitno koju cemo uzeti, pa samo uzmemo prvu
            return listaNajmanjihTroskova[0];
        }

        //metoda provjerava jesu li potroseni kapaciteti ishodista u redu u kojem se nalazi celija u koju smo upravo upisali kolicinu tereta
        //i provjerava jesu zadovoljene potrebe odredista u stupcu u kojem se nalazi celija u koju smo upravo upisali kolicinu tereta
        //ako je nesto od toga istina, celija se dodaje u listu rjesenih celija
        public void ProvjeriRjesenostCelije(Celija najmanjiTrosak)
        {
            if (najmanjiTrosak.kolicinaTereta + sumaKolicinePoRedu == Ai)
            {
                ProvjereniRjesenost rjeseniRed = new ProvjereniRjesenost(true, true, najmanjiTrosak.red);//true=red,true=rjeseni,broj reda
                listaRjesenih.Add(rjeseniRed);
            }

            if (najmanjiTrosak.kolicinaTereta + sumaKolicinePoStupcu == Bj)
            {
                ProvjereniRjesenost rjeseniStupac = new ProvjereniRjesenost(false, true, najmanjiTrosak.stupac);//false=stupac,true=rjeseni,broj stupca
                listaRjesenih.Add(rjeseniStupac);
            }
        }

        public List<DualnaVarijablaIliIndeks> IzracunajNajveciIndeks()
        {
            List<DualnaVarijablaIliIndeks> listaIndeksa = new List<DualnaVarijablaIliIndeks>();

            List<Celija> listaCelijaPoRedu = new List<Celija>();
            List<Celija> listaCelijaPoStupcu = new List<Celija>();

            bool pronadenIndeksPoRedu;
            bool pronadenIndeksPoStupcu;

            foreach (Celija obicnaCelija in listaCelija)//idemo po svim celijama
            {
                if (obicnaCelija.opis == "Obicna" && !ProvjeriJeLiCelijaRjesena(obicnaCelija))//celija mora biti obicna i ne smije biti u rjesenom redu/stupcu
                {
                    pronadenIndeksPoRedu = false;
                    pronadenIndeksPoStupcu = false;
                    if (listaIndeksa.Count != 0)//ako lista indexa nije prazna, provjeriti je li za trenutnu celiju vec izracunat indeks po redu/stupcu 
                    {
                        foreach (DualnaVarijablaIliIndeks indeks in listaIndeksa)
                        {
                            if (indeks.red == true)
                            {
                                if (indeks.brojRedaIliStupca == obicnaCelija.red)//za taj red je vec pronaden indeks
                                {
                                    pronadenIndeksPoRedu = true;
                                }
                            }
                            else
                            {
                                if (indeks.brojRedaIliStupca == obicnaCelija.stupac)//za taj stupac je vec pronaden indeks
                                {
                                    pronadenIndeksPoStupcu = true;
                                }
                            }
                        }
                    }

                    listaCelijaPoRedu.Clear();
                    listaCelijaPoStupcu.Clear();

                    foreach (Celija celija in listaCelija)//trazimo celije koje su u istom redu/stupcu kao i trenutno gledana celija
                    {
                        if (!pronadenIndeksPoStupcu && celija.opis == "Obicna" && celija.stupac == obicnaCelija.stupac && !ProvjeriJeLiCelijaRjesena(celija))//ako su u istom stupcu i ako celija nije u rjesenom stupcu dodamo ju u listu stupaca
                        {
                            listaCelijaPoStupcu.Add(celija);
                        }

                        if (!pronadenIndeksPoRedu && celija.opis == "Obicna" && celija.red == obicnaCelija.red && !ProvjeriJeLiCelijaRjesena(celija))//ako su u istom redu i ako celija nije u rjesenom redu dodamo ju u listu redova
                        {
                            listaCelijaPoRedu.Add(celija);
                        }
                    }

                    if (listaCelijaPoStupcu.Count > 1)
                    {
                        listaCelijaPoStupcu = listaCelijaPoStupcu.OrderBy(x => x.stvarniTrosak).ToList();
                        DualnaVarijablaIliIndeks noviIndeksStupac = new DualnaVarijablaIliIndeks(false, obicnaCelija.stupac, listaCelijaPoStupcu[1].stvarniTrosak - listaCelijaPoStupcu[0].stvarniTrosak);//false=stupac,broj stupca, indeks
                        listaIndeksa.Add(noviIndeksStupac);
                    }

                    if (listaCelijaPoRedu.Count > 1)
                    {
                        listaCelijaPoRedu = listaCelijaPoRedu.OrderBy(x => x.stvarniTrosak).ToList();//tu listu sortiramo te dobimo 2 najmanja troska na prve 2 pozicije liste
                        DualnaVarijablaIliIndeks noviIndeksRed = new DualnaVarijablaIliIndeks(true, obicnaCelija.red, listaCelijaPoRedu[1].stvarniTrosak - listaCelijaPoRedu[0].stvarniTrosak);//true=red, broj reda, indeks
                        listaIndeksa.Add(noviIndeksRed);
                    }

                    if (listaCelijaPoRedu.Count == 1 && listaCelijaPoStupcu.Count == 1)
                    {
                        zadnjaCelija = obicnaCelija;
                        return null;
                    }
                }
            }
            listaIndeksa = listaIndeksa.OrderByDescending(x => x.vrijednost).ToList();
            List<DualnaVarijablaIliIndeks> listaNajvecihIndeksa = listaIndeksa.FindAll(x => x.vrijednost == listaIndeksa[0].vrijednost);
            return listaNajvecihIndeksa;
        }

        //metoda provjera je li celija rjesena
        //a celija je rjesena ako se nalazi u listi rjesenih redova/stupaca
        public bool ProvjeriJeLiCelijaRjesena(Celija celijaZaProvjeru)
        {
            foreach (ProvjereniRjesenost provjera in listaRjesenih)
            {
                if (provjera.red == true)//red je vec rjeseni
                {
                    if (provjera.brojRedaIliStupca == celijaZaProvjeru.red)//ako je celija u rjesenom redu, vrati true
                    {
                        return true;
                    }
                }

                else//stupac je vec rjeseni
                {
                    if (provjera.brojRedaIliStupca == celijaZaProvjeru.stupac)//ako je celija u rjesenom stupcu, vrati true
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Celija> PronadiNajmanjiTrosak(List<DualnaVarijablaIliIndeks> listaNajvecihIndeksa)
        {
            List<Celija> listaTroskova = new List<Celija>();

            foreach (DualnaVarijablaIliIndeks najveciIndeks in listaNajvecihIndeksa)
            {
                foreach (Celija celija in listaCelija)
                {
                    if (najveciIndeks.red == true)//najveci indeks se nalazi u retku
                    {
                        if (celija.opis == "Obicna" && celija.red == najveciIndeks.brojRedaIliStupca && !ProvjeriJeLiCelijaRjesena(celija))
                        {
                            listaTroskova.Add(celija);
                        }
                    }

                    else//najveci indeks se nalazi u stupcu
                    {
                        if (celija.opis == "Obicna" && celija.stupac == najveciIndeks.brojRedaIliStupca && !ProvjeriJeLiCelijaRjesena(celija))
                        {
                            listaTroskova.Add(celija);
                        }
                    }
                }
            }

            listaTroskova = listaTroskova.OrderBy(x => x.stvarniTrosak).ToList();
            List<Celija> listaNajmanjihTroskova = listaTroskova.FindAll(x => x.stvarniTrosak == listaTroskova[0].stvarniTrosak);

            return listaNajmanjihTroskova;
        }
    }
}
