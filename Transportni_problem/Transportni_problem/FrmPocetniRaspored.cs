using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportni_problem
{
    public partial class FrmPocetniRaspored : Form
    {
        PocetniRaspored pocetniRaspored = null;
        Panel pnlPocetniRaspored = null;

        public FrmPocetniRaspored(List<Celija> listaCelija, string odabraniPocetniRaspored, int brojIshodista, int brojOdredista)
        {
            InitializeComponent();
            pocetniRaspored = new PocetniRaspored(listaCelija, brojIshodista, brojOdredista);

            if (pnlPocetniRaspored != null)
            {
                pnlPocetniRaspored.Dispose();
            }

            CrtanjeMatrice crtanje = new CrtanjeMatrice(brojIshodista, brojOdredista);
            pnlPocetniRaspored = crtanje.NacrtajMatricu();
            pnlPocetniRaspored.Location = new Point(20, 20);

            if (odabraniPocetniRaspored == "Sjeverozapadni kut")
            {
                this.Text += " - " + odabraniPocetniRaspored;
                pocetniRaspored.SjeveroZapadniKut();
            }

            else if (odabraniPocetniRaspored == "Minimalni troškovi")
            {
                this.Text += " - " + odabraniPocetniRaspored;
                pocetniRaspored.MinTrosak();
            }

            else if (odabraniPocetniRaspored == "Vogel")
            {
                this.Text += " - " + odabraniPocetniRaspored;
                pocetniRaspored.Vogel();
            }

            PrikaziPocetniRaspored();
        }

        private void PrikaziPocetniRaspored()
        {
            string[] poljeTagova = new string[3];
            string ukupniMinTrosakString = "Z = ";
            double ukupniMinTrosak = 0;

            foreach (Control kontrola in pnlPocetniRaspored.Controls)
            {
                if (kontrola.GetType() == typeof(RichTextBox))
                {
                    RichTextBox richTextBox = (RichTextBox)kontrola;
                    poljeTagova = richTextBox.Tag.ToString().Split('-');

                    foreach (Celija celija in pocetniRaspored.listaCelija)
                    {
                        if (celija.red == int.Parse(poljeTagova[1]) && celija.stupac == int.Parse(poljeTagova[2]))
                        {
                            richTextBox.ReadOnly = true;
                            if (poljeTagova[0] == "Obicna")
                            {
                                richTextBox.Text = celija.stvarniTrosak.ToString();//obicnim celijama dodajemo stvarni trosak
                                if (celija.zauzetoPolje == true)
                                {
                                    richTextBox.Text += Environment.NewLine + celija.kolicinaTereta;//i ako im je kolicina tereta !=0 dodajemo razmake i tu kolicinu tereta
                                    richTextBox.SelectionStart = celija.stvarniTrosak.ToString().Length + 1;//+1 radi newline
                                    richTextBox.SelectionLength = celija.kolicinaTereta.ToString().Length;
                                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);//te na kraju kolicinu tereta povecamo
                                    richTextBox.SelectionAlignment = HorizontalAlignment.Right;
                                    
                                    ukupniMinTrosakString += "(" + celija.stvarniTrosak + " * " + celija.kolicinaTereta + ")" + " + ";//za ispis min troska
                                    ukupniMinTrosak += celija.stvarniTrosak * celija.kolicinaTereta;//racunanje min troska
                                }
                            }
                            else
                            {
                                if (poljeTagova[0] == "Sum")
                                {
                                    richTextBox.Text = pocetniRaspored.sumaAi.ToString();
                                }
                                else//Ai i Bj celije
                                {
                                    richTextBox.Text = celija.stvarniTrosak.ToString();
                                }
                                richTextBox.SelectAll();
                                richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);
                                richTextBox.SelectionAlignment = HorizontalAlignment.Center;
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
            ukupniMinTrosakLabela.Location = new Point(pnlPocetniRaspored.Location.X, pnlPocetniRaspored.Location.Y + pnlPocetniRaspored.Height);
            ukupniMinTrosakLabela.AutoSize = true;

            pnlPocetniRaspored.Controls.Add(ukupniMinTrosakLabela);//ispis min troska

            this.Controls.Add(pnlPocetniRaspored);
        }
    }
}
