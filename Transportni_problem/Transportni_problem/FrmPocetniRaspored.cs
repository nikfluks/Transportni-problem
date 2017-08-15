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
    public partial class FrmPocetniRaspored : Form
    {
        PocetniRaspored pocetniRaspored = null;
        Panel pnlPocetniRaspored;

        public FrmPocetniRaspored(List<Celija> listaCelija, string odabraniPocetniRaspored, Panel pnlMatricaTroskova, int brojIshodista, int brojOdredista)
        {
            InitializeComponent();

            pocetniRaspored = new PocetniRaspored(listaCelija, brojIshodista, brojOdredista);
            pnlPocetniRaspored = pnlMatricaTroskova;
            pnlPocetniRaspored.Location = new Point(20, 20);

            this.Controls.Add(pnlPocetniRaspored);

            if (odabraniPocetniRaspored == "SjeveroZpadniKut")
            {
                pocetniRaspored.SjeveroZapadniKut();
                PrikaziPocetniRaspored(pocetniRaspored.listaCelija);
            }

            else if (odabraniPocetniRaspored == "MinTrosak")
            {
                pocetniRaspored.MinTrosak();
            }

            else if (odabraniPocetniRaspored == "Vogel")
            {
                pocetniRaspored.Vogel();
            }
        }

        private void PrikaziPocetniRaspored(List<Celija> listaCelija)
        {
            string[] poljeTagova = new string[pnlPocetniRaspored.Controls.Count];

            foreach (Control kontrola in pnlPocetniRaspored.Controls)
            {
                if (kontrola.GetType() == typeof(RichTextBox))
                {
                    RichTextBox richTextBox = (RichTextBox)kontrola;
                    poljeTagova = richTextBox.Tag.ToString().Split('-');

                    if (poljeTagova[0] == "Obicna")
                    {
                        foreach (Celija celija in listaCelija)
                        {
                            if (celija.red == int.Parse(poljeTagova[1]) && celija.stupac == int.Parse(poljeTagova[2]))
                            {
                                if (celija.kolicinaTereta != 0)
                                {
                                    richTextBox.Text += "   " + celija.kolicinaTereta;
                                    richTextBox.SelectionStart = 4;
                                    richTextBox.SelectionLength = richTextBox.TextLength;
                                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);
                                    richTextBox.DeselectAll();
                                }
                                
                            }
                        }
                    }
                    //TODO else if (poljeTagova[0] == "Sum") onda desti boju npr u crveno, ali prvo izracunati sumu
                    //TODO rjesiti problem s panelom
                    else
                    {
                        richTextBox.SelectAll();
                        richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, 14, FontStyle.Bold);
                    }

                }
            }
        }
    }
}
