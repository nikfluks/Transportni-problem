using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportni_problem
{
    public partial class FrmOptimizacija : Form
    {
        Optimizacija optimizacija = null;
        Panel pnlOptimizacija = null;

        public FrmOptimizacija(List<Celija> listaCelija, string odabranaMetodaZaOptimizaciju, int brojIshodista, int brojOdredista)
        {
            InitializeComponent();
            optimizacija = new Optimizacija(listaCelija, brojIshodista, brojOdredista);

            if (pnlOptimizacija != null)
            {
                pnlOptimizacija.Dispose();
            }

            CrtanjeMatrice crtanje = new CrtanjeMatrice(brojIshodista, brojOdredista);
            pnlOptimizacija = crtanje.NacrtajMatricu();
            pnlOptimizacija.Location = new Point(20, 20);

            if (odabranaMetodaZaOptimizaciju == "MODI metoda")
            {
                this.Text += " - " + odabranaMetodaZaOptimizaciju;
                optimizacija.MODI();
            }
            else if (odabranaMetodaZaOptimizaciju == "Metoda s kamena na kamen")
            {
                this.Text += " - " + odabranaMetodaZaOptimizaciju;
                optimizacija.Kamen();
            }

            PrikaziOptimalnoRjesenje();
            this.Controls.Add(pnlOptimizacija);
        }

        private void PrikaziOptimalnoRjesenje()
        {
            string[] poljeTagova = new string[3];
            string ukupniMinTrosakString = "Z = ";
            double ukupniMinTrosak = 0;

            foreach (Control kontrola in pnlOptimizacija.Controls)
            {
                if (kontrola.GetType() == typeof(RichTextBox))
                {
                    RichTextBox richTextBox = (RichTextBox)kontrola;
                    poljeTagova = richTextBox.Tag.ToString().Split('-');

                    foreach (Celija celija in optimizacija.listaCelija)
                    {
                        if (celija.red == int.Parse(poljeTagova[1]) && celija.stupac == int.Parse(poljeTagova[2]))
                        {
                            richTextBox.ReadOnly = true;
                            if (poljeTagova[0] == "Obicna")
                            {
                                richTextBox.Text = celija.stvarniTrosak.ToString();//obicnim celijama dodajemo stvarni trosak
                                if (celija.zauzetoPolje == true)
                                {
                                    richTextBox.Text += "   " + celija.kolicinaTereta;//i ako im je kolicina tereta !=0 dodajemo razmake i tu kolicinu tereta
                                    richTextBox.SelectionStart = 4;
                                    richTextBox.SelectionLength = richTextBox.TextLength - 4;
                                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);//te na kraju kolicinu tereta povecamo

                                    ukupniMinTrosakString += "(" + celija.stvarniTrosak + " * " + celija.kolicinaTereta + ")" + " + ";//za ispis min troska
                                    ukupniMinTrosak += celija.stvarniTrosak * celija.kolicinaTereta;//racunanje min troska
                                }
                                else
                                {
                                    richTextBox.Text += "       " + celija.relativniTrosak;
                                }
                            }
                            else
                            {
                                if (poljeTagova[0] == "Sum")
                                {
                                    richTextBox.Text = optimizacija.sumaAi.ToString();
                                }
                                else//Ai i Bj celije
                                {
                                    richTextBox.Text = celija.stvarniTrosak.ToString();
                                }
                                richTextBox.SelectAll();
                                richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);
                            }
                            richTextBox.DeselectAll();
                            break;
                        }
                    }
                }
            }

            ukupniMinTrosakString = ukupniMinTrosakString.Remove(ukupniMinTrosakString.Length - 2);
            ukupniMinTrosakString += " = " + ukupniMinTrosak;

            Label ukupniMinTrosakLabela = new Label();
            ukupniMinTrosakLabela.Text = ukupniMinTrosakString;
            ukupniMinTrosakLabela.Location = new Point(pnlOptimizacija.Location.X, pnlOptimizacija.Location.Y + pnlOptimizacija.Height);
            ukupniMinTrosakLabela.AutoSize = true;

            pnlOptimizacija.Controls.Add(ukupniMinTrosakLabela);//ispis min troska

            Label brojOptimalnihRjesenjaLabela = new Label();
            brojOptimalnihRjesenjaLabela.Font = new Font(brojOptimalnihRjesenjaLabela.Font.FontFamily, 12, FontStyle.Bold);
            brojOptimalnihRjesenjaLabela.Location = new Point(ukupniMinTrosakLabela.Location.X, ukupniMinTrosakLabela.Location.Y + 35);
            brojOptimalnihRjesenjaLabela.AutoSize = true;
            brojOptimalnihRjesenjaLabela.Text = "Broj optimalnih rješenja: " + optimizacija.brojOptimalnihRjesenja.ToString();
            
            pnlOptimizacija.Controls.Add(brojOptimalnihRjesenjaLabela);//ispis broja optimlanih rjesenja
        }
    }
}
