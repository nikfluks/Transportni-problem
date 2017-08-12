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
        public FrmGlavna()
        {
            InitializeComponent();
            groupOdabirPocetnogRasporeda.Visible = false;
            btnPrikaziPocetniRaspored.Visible = false;
        }

        int brojIshodista;
        int brojOdredista;

        private void btnKreirajPraznuTablicu_Click(object sender, EventArgs e)
        {
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

            pnlTablica.Controls.Clear();

            NacrtajRetke();
            NacrtajStupce();
            NacrtajCelije();

            PrikaziMetodeZaPocetniRaspored();
            PrikaziGumbZaPocetniRaspored();

        }

        private void NacrtajRetke()
        {
            int y = 55;

            for (int i = 1; i <= brojIshodista; i++)
            {
                Label labelaI = new Label();
                labelaI.Text = "I" + i;
                labelaI.Location = new Point(10, y);
                labelaI.Size = new Size(30, 15);
                //labelaI.Font = new Font(labelaI.Font.FontFamily, 15, FontStyle.Bold);
                y += 45;

                pnlTablica.Controls.Add(labelaI);
            }

            Label labelaIs = new Label();
            labelaIs.Text = "Bj";
            labelaIs.Location = new Point(10, y + 10); //+10 je radi estetike da bude razmak izmedu obicnih celija
            labelaIs.Size = new Size(30, 15);

            if (pnlTablica.Height < y)//ako je panel pre niski, povecamo ga
            {
                pnlTablica.Height = y + 45;
            }

            pnlTablica.Controls.Add(labelaIs);
        }

        private void NacrtajStupce()
        {
            int x = 60;

            for (int i = 1; i <= brojOdredista; i++)
            {
                Label labelaO = new Label();
                labelaO.Text = "O" + i;
                labelaO.Location = new Point(x, 10);
                labelaO.Size = new Size(30, 15);
                x += 65;

                pnlTablica.Controls.Add(labelaO);
            }

            Label labelaOd = new Label();
            labelaOd.Text = "Ai";
            labelaOd.Location = new Point(x + 10, 10); //+10 je radi estetike da bude razmak izmedu obicnih celija
            labelaOd.Size = new Size(30, 15);

            if (pnlTablica.Width < x)//ako je panel pre uski, povecamo ga
            {
                pnlTablica.Width = x + 65;
            }

            pnlTablica.Controls.Add(labelaOd);
        }

        private void NacrtajCelije()
        {
            int x = 40;
            int y = 40;
            

            for (int i = 1; i <= brojIshodista + 1; i++)//broj ishodišta/redova, +1 je za Bj
            {
                bool zadnji = false;

                for (int j = 1; j <= brojOdredista + 1; j++)//broj odredišta/stupaca, +1 je za Ai
                {
                    TextBox celija = new TextBox();

                    if (i == brojIshodista + 1 && j == brojOdredista + 1)//provjera je li trenutna celija zadnja
                    {
                        celija.Tag = "Sum-" + i + "-" + j;
                        celija.ReadOnly = true;
                        x += 10;
                    }

                    else if (i == brojIshodista + 1)//provjera je li redak zadnji
                    {
                        celija.Tag = "Bj-" + i + "-" + j;

                        if (!zadnji)
                        {
                            zadnji = true;
                            y += 10;
                        }
                    }

                    else if (j == brojOdredista + 1)//provjera je li stupac zadnji
                    {
                        celija.Tag = "Ai-" + i + "-" + j;

                        if (!zadnji)
                        {
                            zadnji = true;
                            x += 10;
                        }
                    }

                    else//sve ostale celije
                    {
                        celija.Tag = "Obicna-" + i + "-" + j;
                    }

                    celija.Location = new Point(x, y);
                    celija.Size = new Size(60, 40);
                    celija.Multiline = true;

                    pnlTablica.Controls.Add(celija);

                    if (j == brojOdredista + 1) //ako smo dosli do kraja reda, prebacujemo se u novi red
                    {
                        x = 40;
                        y += 45;
                    }

                    else //inace se samo pomicemo udesno
                    {
                        x += 65;
                    }

                }
            }
        }

        private void PrikaziMetodeZaPocetniRaspored()
        {
            groupOdabirPocetnogRasporeda.Location = new Point(25, pnlTablica.Location.Y + pnlTablica.Height + 20);
            groupOdabirPocetnogRasporeda.Visible = true;
        }

        private void PrikaziGumbZaPocetniRaspored()
        {
            btnPrikaziPocetniRaspored.Location = new Point(25, groupOdabirPocetnogRasporeda.Location.Y + groupOdabirPocetnogRasporeda.Height + 20);
            btnPrikaziPocetniRaspored.Visible = true;
        }

        private void btnPrikaziPocetniRaspored_Click(object sender, EventArgs e)
        {
            List<double> listaVrijednostiCelija = new List<double>();
            double vrijednostCelije;
            string[] poljeTagova = new string[pnlTablica.Controls.Count];

            foreach (Control kontrola in pnlTablica.Controls)
            {
                if (typeof(TextBox) == kontrola.GetType())
                {
                    poljeTagova = kontrola.Tag.ToString().Split('-');

                    if (poljeTagova[0] != "Sum")
                    {
                        if (double.TryParse(kontrola.Text, out vrijednostCelije))
                        {
                            if (vrijednostCelije>=0)
                            {
                                listaVrijednostiCelija.Add(vrijednostCelije);
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
                }
            }

            string odabraniPocetniRaspored;

            if (radioSZKut.Checked)
            {
                odabraniPocetniRaspored = "SjeveroZpadniKut";
            }

            else if (radioMinTros.Checked)
            {
                odabraniPocetniRaspored = "MinTrosak";
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

            FrmPocetniRaspored frmPocetniRaspored = new FrmPocetniRaspored(listaVrijednostiCelija, odabraniPocetniRaspored);
            frmPocetniRaspored.ShowDialog();
        }
    }
}
