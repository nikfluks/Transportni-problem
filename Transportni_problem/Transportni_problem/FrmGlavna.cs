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

            }
            catch (FormatException)
            {
                MessageBox.Show("Pogrešan unos!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (pnlTablica.Height < y)//ako je panel premali, tj sljedece ishodiste se nebi vidjelo, povecamo ga
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

            if (pnlTablica.Width < x)//ako je panel premali, tj sljedece odrediste se nebi vidjelo, povecamo ga
            {
                pnlTablica.Width = x + 65;
            }

            pnlTablica.Controls.Add(labelaOd);
        }

        private void NacrtajCelije()
        {
            int x = 40;
            int y = 40;
            bool zadnji = false;

            for (int i = 1; i <= (brojIshodista + 1) * (brojOdredista + 1); i++) //+1 ide da crtam celije za Ai i Bj
            {
                TextBox celija = new TextBox();


                if (i % (brojOdredista + 1) == 0) //provjera dal je doslo do kraja obicnih celija, tj sljedeca ce biti Ai celija
                {
                    x += 10; //+10 je radi estetike da bude razmak izmedu obicnih celija

                    if (i == (brojIshodista + 1) * (brojOdredista + 1))
                    {
                        celija.Tag = "Sum";
                    }

                    else
                    {
                        celija.Tag = "Ai";
                    }
                }

                else if (i > ((brojIshodista + 1) * (brojOdredista + 1)) - (brojOdredista + 1) && !zadnji) //provjera dal je doslo do kraja obicnih celija, tj sljedeca ce biti Bj celija
                {
                    y += 10;
                    zadnji = true;
                    celija.Tag = "Bj";
                }

                else
                {
                    celija.Tag = "Obicna";
                }

                
                celija.Location = new Point(x, y);
                celija.Size = new Size(60, 40);
                celija.Multiline = true;

                if (i % (brojOdredista + 1) == 0) //ako smo dosli do kraja reda, prebacujemo se u novi red
                {
                    x = 40;
                    y += 45;
                }

                else //inace se samo pomicemo udesno
                {
                    x += 65;
                }

                pnlTablica.Controls.Add(celija);
            }
        }

        private void PrikaziMetodeZaPocetniRaspored()
        {
            groupOdabirPocetnogRasporeda.Location = new Point(25, pnlTablica.Location.Y + pnlTablica.Height + 15);
            groupOdabirPocetnogRasporeda.Visible = true;
        }

        private void PrikaziGumbZaPocetniRaspored()
        {
            btnPrikaziPocetniRaspored.Location = new Point(25, groupOdabirPocetnogRasporeda.Location.Y + groupOdabirPocetnogRasporeda.Height + 20);
            btnPrikaziPocetniRaspored.Visible = true;
        }

        private void btnPrikaziPocetniRaspored_Click(object sender, EventArgs e)
        {
            FrmPocetniRaspored frmPocetniRaspored = new FrmPocetniRaspored();
            frmPocetniRaspored.ShowDialog();
        }
    }
}
