using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportni_problem
{
    public class CrtanjeMatrice
    {
        int brojIshodista;
        int brojOdredista;
        Panel pnlMatrica;

        public CrtanjeMatrice(int brojIshodista, int brojOdredista)
        {
            this.brojIshodista = brojIshodista;
            this.brojOdredista = brojOdredista;
            pnlMatrica = new Panel();
            pnlMatrica.AutoSize = true;
            pnlMatrica.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        public Panel NacrtajMatricu()
        {
            NacrtajRetke();
            NacrtajStupce();
            NacrtajCelije();
            return pnlMatrica;
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
                y += 50;

                pnlMatrica.Controls.Add(labelaI);
            }

            Label labelaIs = new Label();
            labelaIs.Text = "Bj";
            labelaIs.Location = new Point(10, y + 10); //+10 je radi estetike da bude razmak izmedu obicnih celija
            labelaIs.Size = new Size(30, 15);

            if (pnlMatrica.Height < y)//ako je panel pre niski, povecamo ga
            {
                pnlMatrica.Height = y + 50;
            }

            pnlMatrica.Controls.Add(labelaIs);
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

                pnlMatrica.Controls.Add(labelaO);
            }

            Label labelaOd = new Label();
            labelaOd.Text = "Ai";
            labelaOd.Location = new Point(x + 10, 10); //+10 je radi estetike da bude razmak izmedu obicnih celija
            labelaOd.Size = new Size(30, 15);

            if (pnlMatrica.Width < x)//ako je panel pre uski, povecamo ga
            {
                pnlMatrica.Width = x + 65;
            }

            pnlMatrica.Controls.Add(labelaOd);
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
                    RichTextBox richTextBox = new RichTextBox();
                    richTextBox.Click += delegate
                    {
                        richTextBox.SelectAll();
                    };

                    if (i == brojIshodista + 1 && j == brojOdredista + 1)//provjera je li trenutna celija zadnja
                    {
                        richTextBox.Tag = "Sum-" + i + "-" + j;
                        richTextBox.ReadOnly = true;
                        x += 10;
                    }

                    else if (i == brojIshodista + 1)//provjera je li redak zadnji
                    {
                        richTextBox.Tag = "Bj-" + i + "-" + j;

                        if (!zadnji)
                        {
                            zadnji = true;
                            y += 10;
                        }
                    }

                    else if (j == brojOdredista + 1)//provjera je li stupac zadnji
                    {
                        richTextBox.Tag = "Ai-" + i + "-" + j;

                        if (!zadnji)
                        {
                            zadnji = true;
                            x += 10;
                        }
                    }

                    else//sve ostale celije
                    {
                        richTextBox.Tag = "Obicna-" + i + "-" + j;
                    }

                    richTextBox.Location = new Point(x, y);
                    richTextBox.Size = new Size(60, 45);
                    richTextBox.Multiline = true;

                    pnlMatrica.Controls.Add(richTextBox);

                    if (j == brojOdredista + 1) //ako smo dosli do kraja reda, prebacujemo se u novi red
                    {
                        x = 40;
                        y += 50;
                    }

                    else //inace se samo pomicemo udesno
                    {
                        x += 65;
                    }
                }
            }
        }
    }
}
