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
        public FrmPocetniRaspored(List<Celija> listaCelija, string odabraniPocetniRaspored, Panel pnlTablica)
        {
            InitializeComponent();
            PocetniRaspored pocetniRaspored = new PocetniRaspored(listaCelija);
            pnlTablica.Location = new Point(20, 20);
            //pnlTablica.Show();
            this.Controls.Add(pnlTablica);

            if (odabraniPocetniRaspored == "SjeveroZpadniKut")
            {
                pocetniRaspored.SjeveroZapadniKut();
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
    }
}
