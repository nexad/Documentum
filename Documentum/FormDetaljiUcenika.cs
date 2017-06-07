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
        private Ucenik selectedUcenik;

        public FormDetaljiUcenika()
        {
            InitializeComponent();
        }

        public int UcenikId { get => ucenikId; set => ucenikId = value; }

        public Ucenik SelectedUcenik { get => selectedUcenik; set => selectedUcenik = value; }

        private void FormDetaljiUcenika_Load(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                mcbPredmet.DataSource = context.Predmets.ToList();
                mcbPredmet.DisplayMember = "naziv";
                mcbPredmet.ValueMember = "Id";
            }

            ReloadGridOcene();
            ReloadGridDokumenta();
            ReloadGridUcenikBookmarks();
            ReloadDetaljeUcenika();
        }

      
        private void ReloadDetaljeUcenika()
        {
            mlImePrezime.Text = selectedUcenik.prezime + " " + selectedUcenik.ime;
            mtbIme.Text = selectedUcenik.ime;
            mtbPrezime.Text = selectedUcenik.prezime;
            mdtDatmRodj.Value = (DateTime) selectedUcenik.datum_rodjenja;
            mtbMaticniBroj.Text = selectedUcenik.brojMaticneKnjige;
            mtbImeRoditelja.Text = selectedUcenik.imeRoditelja;
            mtbMestoRodjenja.Text = selectedUcenik.mestoRodjenja;
            mtbDelovodniBroj.Text = selectedUcenik.delovodniBroj;
            mtbPut.Text = selectedUcenik.kojiput;
            mtbOpsitna.Text = selectedUcenik.opstina;
            mtbDrzava.Text = selectedUcenik.drzava;
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

                selectedUcenik = ucenik;

                mlImePrezime.Text = ucenik.prezime +" "+ucenik.ime;

                var ocene = from UcenikOcena in context.UcenikOcenas.Where(u => u.ucenikId == UcenikId)
                            select new
                            {
                                UcenikOcena.Id,
                                UcenikOcena.SmerGodinaPredmet.redniBroj,
                                UcenikOcena.SmerGodinaPredmet.Predmet.naziv,
                                UcenikOcena.ocena,
                                UcenikOcena.ocenaOpis
                            };

                metroGridOcene.DataSource = ocene.OrderBy(o => o.redniBroj).ToList();
                
                if (ocene.Count() > 0)
                    metroGridOcene.Columns[0].Visible = false;

            }
        }

        public void ReloadGridUcenikBookmarks()
        {
            int ucenikDokumentId = DocumentumFactory.GetSelectedGridId(metroGridDokumenta);

            if (ucenikDokumentId>0)
            {
                using (var context = new documentumEntities())
                {
                    UcenikDokument ucenikDokument = context.UcenikDokuments.SingleOrDefault(u => u.Id == ucenikDokumentId);
               
                    BindingSource bs = new BindingSource();
                    bs.DataSource = (from UcenikBookmark in context.UcenikBookmarks.Where(u => u.ucenikDokumentId == ucenikDokumentId)
                                     select new
                                     {
                                         UcenikBookmark.Id,
                                         UcenikBookmark.Bookmark.bookmarkTitle,
                                         UcenikBookmark.Bookmark.bookmarkName,
                                         UcenikBookmark.value,
                                         UcenikBookmark.Bookmark.format
                                     }).ToList();
                    metroGridUcenikBookmarks.DataSource = bs;
                    if (metroGridUcenikBookmarks.Columns[0] != null)
                        metroGridUcenikBookmarks.Columns[0].Visible = false;

                }
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

        private void metroGridOcene_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int ucenikOcenaId = DocumentumFactory.GetSelectedGridId(metroGridOcene);

            if (ucenikOcenaId > 0)
            {
                using (var context = new documentumEntities())
                {
                    UcenikOcena ucenikOcena = context.UcenikOcenas.SingleOrDefault(u => u.Id == ucenikOcenaId);
                    mcbPredmet.SelectedValue = ucenikOcena.SmerGodinaPredmet.predmetId;
                    mtbOcena.Text = ucenikOcena.ocena.ToString();
                    mtbOcenaOpis.Text = ucenikOcena.ocenaOpis.ToString();
                }
            }
        }

        private void mbSave_Click(object sender, EventArgs e)
        {
            int ucenikOcenaId = DocumentumFactory.GetSelectedGridId(metroGridOcene);

            if (ucenikOcenaId > 0)
            {
                using (var context = new documentumEntities())
                {
                    int predmetId = int.Parse(mcbPredmet.SelectedValue.ToString());
                    UcenikOcena ucenikOcenaOther = context.UcenikOcenas.SingleOrDefault(u => u.Id == ucenikOcenaId);
                    UcenikOcena ucenikOcena  = context.UcenikOcenas.SingleOrDefault(u => u.ucenikId == UcenikId && u.SmerGodinaPredmet.predmetId == predmetId);
                    if (ucenikOcena == null)
                    {
                        SmerGodinaPredmet sgp = context.SmerGodinaPredmets.SingleOrDefault(s => s.smerGodinaId == ucenikOcenaOther.SmerGodinaPredmet.smerGodinaId && s.predmetId == predmetId);
                        ucenikOcena = new UcenikOcena()
                        {
                            ucenikId = UcenikId,
                            smerGodinaPredmetId = sgp.Id,
                        };
                        ucenikOcena.ocena = int.Parse(mtbOcena.Text.ToString());
                        ucenikOcena.ocenaOpis = mtbOcenaOpis.Text;
                        context.UcenikOcenas.Add(ucenikOcena);
                    }
                    else
                    {
                        ucenikOcena.ocena = int.Parse(mtbOcena.Text.ToString());
                        ucenikOcena.ocenaOpis = mtbOcenaOpis.Text;
                    }
                    context.SaveChanges();
                }
                ReloadGridOcene();
            }
        }

        private void mtbOcena_TextChanged(object sender, EventArgs e)
        {
            string ocenaOpis = "";
            try
            {
                ocenaOpis = DocumentumFactory.marks[int.Parse(mtbOcena.Text.ToString())].ToString();
            } catch
            {
                ocenaOpis = "";
            }
            if (Convert.ToInt32(mtbOcena.Text.ToString()) != 0)
                mtbOcenaOpis.Text = ocenaOpis;
        }

        private void metroGridUcenikBookmarks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string ucenikBookmarValue = DocumentumFactory.GetSelectedGridCellValue(metroGridUcenikBookmarks, "value").ToString();

            mtbBookmarkValue.Text = ucenikBookmarValue;

        }

        private void mbSaveBookmark_Click(object sender, EventArgs e)
        {
            int ucenikBookmarkId = DocumentumFactory.GetSelectedGridId(metroGridUcenikBookmarks);

            using (var context = new documentumEntities())
            {
                UcenikBookmark ucenikBookmark = context.UcenikBookmarks.SingleOrDefault(u => u.Id == ucenikBookmarkId);
                ucenikBookmark.value = mtbBookmarkValue.Text;

                context.SaveChanges();
            }
            ReloadGridUcenikBookmarks();

        }

        private void metroGridDokumenta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ReloadGridUcenikBookmarks();
        }

        private void mbSaveStudent_Click(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                Ucenik ucenik = context.Uceniks.SingleOrDefault(u => u.Id == UcenikId);

                ucenik.ime = mtbIme.Text;
                ucenik.prezime = mtbPrezime.Text;
                ucenik.datum_rodjenja = mdtDatmRodj.Value;
                ucenik.brojMaticneKnjige =  mtbMaticniBroj.Text;
                ucenik.imeRoditelja = mtbImeRoditelja.Text;
                ucenik.mestoRodjenja = mtbMestoRodjenja.Text;
                ucenik.delovodniBroj = mtbDelovodniBroj.Text;
                ucenik.kojiput = mtbPut.Text;
                ucenik.opstina = mtbOpsitna.Text;
                ucenik.drzava = mtbDrzava.Text;

                context.SaveChanges(); 
            }
            ReloadGridOcene();
            ReloadDetaljeUcenika();
            
        }
    }
}
