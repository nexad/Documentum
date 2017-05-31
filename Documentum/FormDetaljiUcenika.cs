using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documentum
{
    public partial class FormDetaljiUcenika : MetroFramework.Forms.MetroForm
    {
        private int ucenikId;

        public FormDetaljiUcenika()
        {
            InitializeComponent();
        }

        public int UcenikId { get => ucenikId; set => ucenikId = value; }

        private void FormDetaljiUcenika_Load(object sender, EventArgs e)
        {
            ReloadGridOcene();
        }

        public void ReloadGridOcene()
        {
            if (UcenikId == 0)
                return;
            
            using (var context = new documentumEntities())
            {
                Ucenik ucenik = context.Uceniks.SingleOrDefault(u => u.Id == UcenikId);
                mlImePrezime.Text = ucenik.prezime +" "+ucenik.ime;
                var ocene = from UcenikOcena in context.UcenikOcenas.Where(u => u.ucenikId == UcenikId)
                            select new
                            {
                                UcenikOcena.SmerGodinaPredmet.redniBroj,
                                UcenikOcena.SmerGodinaPredmet.Predmet.naziv,
                                UcenikOcena.ocena,
                                UcenikOcena.ocenaOpis
                            };
                metroGridOcene.DataSource = ocene.OrderBy(o => o.redniBroj).ToList();
            }
        }
    }
}
