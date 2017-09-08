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

            PrikaziPocetniRaspored();
            this.Controls.Add(pnlOptimizacija);
        }

        private void PrikaziPocetniRaspored()
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
                                if (celija.kolicinaTereta != 0)
                                {
                                    richTextBox.Text += "   " + celija.kolicinaTereta;//i ako im je kolicina tereta !=0 dodajemo razmake i tu kolicinu tereta
                                    richTextBox.SelectionStart = 4;
                                    richTextBox.SelectionLength = richTextBox.TextLength - 4;
                                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);//te na kraju kolicinu tereta povecamo

                                    ukupniMinTrosakString += "(" + celija.stvarniTrosak + " * " + celija.kolicinaTereta + ")" + " + ";//za ispis min troska
                                    ukupniMinTrosak += celija.stvarniTrosak * celija.kolicinaTereta;//racunanje min troska
                                }
                            }
                            else
                            {
                                if (poljeTagova[0] == "Sum")
                                {
                                    //TODO!!!!!
                                    //richTextBox.Text = optimizacija.sumaAi.ToString();
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
        }
    }
}
