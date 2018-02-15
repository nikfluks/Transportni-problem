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
    public partial class FrmGlavna : Form
    {
        Panel pnlMatricaTroska = null;
        List<Celija> listaCelija = null;
        int brojIshodista;
        int brojOdredista;

        public FrmGlavna()
        {
            InitializeComponent();
        }

        private void FrmGlavna_Load(object sender, EventArgs e)
        {
            SakrijSveKontrole();
            listaCelija = new List<Celija>();
        }

        private void SakrijSveKontrole()
        {
            groupOdabirPocetnogRasporeda.Visible = false;
            btnPrikaziPocetniRaspored.Visible = false;
            groupOdabirOptimizacije.Visible = false;
            btnPrikaziOptimalnoRjesenje.Visible = false;
        }

        private void btnKreirajPraznuTablicu_Click(object sender, EventArgs e)
        {
            SakrilKontroleZaOptimizaciju();

            try
            {
                brojIshodista = int.Parse(txtBrojIshodista.Text);
                brojOdredista = int.Parse(txtBrojOdredista.Text);

                if (brojIshodista <= 0 || brojOdredista <= 0)
                {
                    MessageBox.Show("Pogrešan unos!" + Environment.NewLine + "Niste unijeli pozitivan broj!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Pogrešan unos!" + Environment.NewLine + "Niste unijeli (cijeli) broj!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pnlMatricaTroska != null)
            {
                pnlMatricaTroska.Dispose();
            }

            CrtanjeMatrice crtanje = new CrtanjeMatrice(brojIshodista, brojOdredista);
            pnlMatricaTroska = crtanje.NacrtajMatricu();
            pnlMatricaTroska.Location = new Point(25, 122);
            pnlMatricaTroska.TabIndex = 4;
            pnlMatricaTroska.TabStop = true;
            this.Controls.Add(pnlMatricaTroska);

            PrikaziMetodeZaPocetniRaspored();
            PrikaziGumbZaPocetniRaspored();
        }

        private void PrikaziMetodeZaPocetniRaspored()
        {
            groupOdabirPocetnogRasporeda.Location = new Point(25, pnlMatricaTroska.Location.Y + pnlMatricaTroska.Height + 20);
            groupOdabirPocetnogRasporeda.Visible = true;
        }

        private void PrikaziGumbZaPocetniRaspored()
        {
            btnPrikaziPocetniRaspored.Location = new Point(25, groupOdabirPocetnogRasporeda.Location.Y + groupOdabirPocetnogRasporeda.Height + 10);
            btnPrikaziPocetniRaspored.Visible = true;
        }

        private void PrikaziMetodeZaOptimizaciju()
        {
            groupOdabirOptimizacije.Location = new Point(25, btnPrikaziPocetniRaspored.Location.Y + btnPrikaziPocetniRaspored.Height + 30);
            groupOdabirOptimizacije.Visible = true;
        }

        private void PrikaziGumbZaOptimizaciju()
        {
            btnPrikaziOptimalnoRjesenje.Location = new Point(25, groupOdabirOptimizacije.Location.Y + groupOdabirOptimizacije.Height + 10);
            btnPrikaziOptimalnoRjesenje.Visible = true;
        }

        private void btnPrikaziPocetniRaspored_Click(object sender, EventArgs e)
        {
            listaCelija.Clear();
            double stvarniTrosak;
            string[] poljeTagova = new string[3];

            foreach (Control kontrola in pnlMatricaTroska.Controls)
            {
                if (typeof(RichTextBox) == kontrola.GetType())
                {
                    poljeTagova = kontrola.Tag.ToString().Split('-');

                    if (poljeTagova[0] != "Sum")
                    {
                        if (double.TryParse(kontrola.Text, out stvarniTrosak))
                        {
                            if (stvarniTrosak >= 0)
                            {
                                Celija novaCelija = new Celija(poljeTagova[0], int.Parse(poljeTagova[1]), int.Parse(poljeTagova[2]), stvarniTrosak);
                                listaCelija.Add(novaCelija);
                            }
                            else
                            {
                                MessageBox.Show("Greška na poziciji " + poljeTagova[1] + "-" + poljeTagova[2] + "." + Environment.NewLine + "Niste unijeli pozitivan broj!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Greška na poziciji " + poljeTagova[1] + "-" + poljeTagova[2] + "." + Environment.NewLine + "Niste unijeli (decimalni) broj!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else//samo dodamo celiju za sum u listu
                    {
                        Celija novaCelija = new Celija(poljeTagova[0], int.Parse(poljeTagova[1]), int.Parse(poljeTagova[2]), 0);
                        listaCelija.Add(novaCelija);
                    }
                }
            }

            string odabraniPocetniRaspored;

            if (radioSZKut.Checked)
            {
                odabraniPocetniRaspored = "Sjeverozapadni kut";
            }

            else if (radioMinTros.Checked)
            {
                odabraniPocetniRaspored = "Minimalni troškovi";
            }

            else if (radioVogel.Checked)
            {
                odabraniPocetniRaspored = "Vogel";
            }

            else
            {
                MessageBox.Show("Niste odabrani metodu za početni raspored!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmPocetniRaspored frmPocetniRaspored = new FrmPocetniRaspored(listaCelija, odabraniPocetniRaspored, brojIshodista, brojOdredista);
            frmPocetniRaspored.ShowDialog();

            PrikaziMetodeZaOptimizaciju();
            PrikaziGumbZaOptimizaciju();
        }

        private void btnPrikaziOptimalnoRjesenje_Click(object sender, EventArgs e)
        {
            string odabranaMetodaZaOptimizaciju = "";

            if (radioMODI.Checked)
            {
                odabranaMetodaZaOptimizaciju = "MODI metoda";
            }

            else if (radioKamen.Checked)
            {
                odabranaMetodaZaOptimizaciju = "Metoda s kamena na kamen";
            }

            else
            {
                MessageBox.Show("Niste odabrani metodu za početni optimizaciju!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmOptimizacija frmOptimizacija = new FrmOptimizacija(listaCelija, odabranaMetodaZaOptimizaciju, brojIshodista, brojOdredista);
            frmOptimizacija.ShowDialog();
        }

        private void radioSZKut_CheckedChanged(object sender, EventArgs e)
        {
            SakrilKontroleZaOptimizaciju();
        }

        private void radioMinTros_CheckedChanged(object sender, EventArgs e)
        {
            SakrilKontroleZaOptimizaciju();
        }

        private void radioVogel_CheckedChanged(object sender, EventArgs e)
        {
            SakrilKontroleZaOptimizaciju();
        }

        private void SakrilKontroleZaOptimizaciju()
        {
            groupOdabirOptimizacije.Visible = false;
            btnPrikaziOptimalnoRjesenje.Visible = false;
        }

        private void txtBrojOdredista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnKreirajPraznuTablicu.PerformClick();
            }
        }
    }
}
