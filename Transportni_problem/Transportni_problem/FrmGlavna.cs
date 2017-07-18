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
            labelaIs.Location = new Point(10, y + 10);
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

            if (pnlTablica.Width < x)//ako je panel premali, tj sljedece odrediste se nebi vidjelo, povecamo ga
            {
                pnlTablica.Width = x + 65;
            }

            Label labelaOd = new Label();
            labelaOd.Text = "Ai";
            labelaOd.Location = new Point(x + 10, 10);
            labelaOd.Size = new Size(30, 15);

            pnlTablica.Controls.Add(labelaOd);
        }

        private void NacrtajCelije()
        {
            int x = 40;
            int y = 40;
            bool zadnji = false;

            for (int i = 1; i <= (brojIshodista + 1) * (brojOdredista + 1); i++)
            {
                if (i % (brojOdredista + 1) == 0) 
                {
                    x += 10;
                }

                if (i > ((brojIshodista + 1) * (brojOdredista + 1)) - (brojOdredista + 1) && !zadnji)
                {
                    y += 10;
                    zadnji = true;
                }

                TextBox celija = new TextBox();
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
    }
}
