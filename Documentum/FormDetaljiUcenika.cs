using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            ReloadGridDokumenta();
        }

        public void ReloadGridDokumenta()
        {
            if (UcenikId == 0)
                return;

            using (var context = new documentumEntities())
            {

                var dokumenta = from UcenikDokument in context.UcenikDokuments.Where(u => u.ucenikId == UcenikId)
                                select new
                                {
                                    UcenikDokument.Id,
                                    UcenikDokument.DokumentTip.naziv,
                                    UcenikDokument.dokumentPath,
                                    Status = UcenikDokument.status
                            };
                metroGridDokumenta.DataSource = dokumenta.OrderBy(o => o.naziv).ToList();
                if (dokumenta.Count() > 0)
                    metroGridDokumenta.Columns[0].Visible = false;
            }
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

        private void MbPregled_Click(object sender, EventArgs e)
        {
            string documentPath = DocumentumFactory.GetSelectedGridCellValue(metroGridDokumenta, "dokumentPath").ToString();
            //TODO: Check if file exists
            if (!File.Exists(documentPath))
            {
                MessageBox.Show(String.Format("Ne postoji fajl {0} ", documentPath), "Greska prilikom ucitavanja fajla",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = true;
            Microsoft.Office.Interop.Word.Document docPrint = wordApp.Documents.Open(documentPath);
        }
    }
}
