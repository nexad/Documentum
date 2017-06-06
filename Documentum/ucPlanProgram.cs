using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documentum
{
    public partial class UcPlanProgram : MetroFramework.Controls.MetroUserControl
    {
        public UcPlanProgram()
        {
            InitializeComponent();
        }

        private void UcPlanProgram_Load(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                mcbSmer.DataSource = context.Smers.ToList();
                mcbSmer.DisplayMember = "naziv";
                mcbSmer.ValueMember = "Id";

                mcbGrupa.DataSource = context.Grupas.ToList();
                mcbGrupa.DisplayMember = "naziv";
                mcbGrupa.ValueMember = "Id";

                mcbPredmet.DataSource = context.Predmets.ToList();
                mcbPredmet.DisplayMember = "naziv";
                mcbPredmet.ValueMember = "Id";

                mcbSmer.SelectedIndex = 0;
                InitializeMcbGodina();
                ReloadGridData();
                ReloadGridDokumenta();
                ReloadGridBookmarks();
            }
        }

        private void InitializeMcbGodina()
        {
            int smerId = 0;
            try
            {
                smerId = Convert.ToInt32(mcbSmer.SelectedValue.ToString());
            }
            catch
            {
                smerId = 0;
            }

            if (smerId != 0)
            {
                using (var context = new documentumEntities())
                {
                    var smerGodine = from SmerGodina in context.SmerGodinas.Where(s => s.Smer.Id == smerId)
                                     select new
                                     {
                                         SmerGodina.Id,
                                         SmerGodina.Smer,
                                         Godina = SmerGodina.godina
                                     };
                    mcbGodina.DataSource = smerGodine.ToList();
                    mcbGodina.DisplayMember = "Godina";
                    mcbGodina.ValueMember = "Id";
                }
            }
        }
        private void mcbSmer_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeMcbGodina();
        }

        private void ReloadGridData()
        {
            int smerGodinaId = 0;
            try
            {
                smerGodinaId = Convert.ToInt32(mcbGodina.SelectedValue.ToString());
            }
            catch
            {
                smerGodinaId = 0;
            }
            if (smerGodinaId != 0)
            {
                using (var context = new documentumEntities())
                {
                    var predmeti = from SmerGodinaPredmet in context.SmerGodinaPredmets.Where(s => s.smerGodinaId == smerGodinaId)
                                     select new
                                     {
                                         SmerGodinaPredmet.Id,
                                         SmerGodinaPredmet.redniBroj,
                                         Naziv = SmerGodinaPredmet.Predmet.naziv,
                                         Grupa = SmerGodinaPredmet.Grupa.naziv
                                     };
                    metroGridPredmeti.DataSource = predmeti.OrderBy(p => p.redniBroj).ToList();
                    if (predmeti.Count() > 0)
                        metroGridPredmeti.Columns[0].Visible = false;
                }
            }
            
        }

        private void ReloadGridDokumenta()
        {
            int smerGodinaId = 0;
            try
            {
                smerGodinaId = Convert.ToInt32(mcbGodina.SelectedValue.ToString());
            }
            catch
            {
                smerGodinaId = 0;
            }
            if (smerGodinaId != 0)
            {
                using (var context = new documentumEntities())
                {
                    var dokumenta = from SmerGodinaDokument in context.SmerGodinaDokuments.Where(s => s.smerGodinaId == smerGodinaId)
                                   select new
                                   {
                                       SmerGodinaDokument.Id,
                                       SmerGodinaDokument.DokumentTip.naziv,
                                       SmerGodinaDokument.DokumentTip.templatePath,
                                       SmerGodinaDokument.DokumentTip.outputPath,
                                       SmerGodinaDokument.dokumentTipId
                                   };
                    metroGridDokumenta.DataSource = dokumenta.ToList();
                    if (dokumenta.Count() > 0)
                    {
                        metroGridDokumenta.Columns[0].Visible = false;
                        metroGridDokumenta.Columns["dokumentTipId"].Visible = false;
                    }
                }
            }

        }

        private void ReloadGridBookmarks()
        {
            int documentTipId = 0;
            try
            {
                documentTipId = int.Parse(DocumentumFactory.GetSelectedGridCellValue(metroGridDokumenta, "dokumentTipId").ToString());
            }
            catch
            {
                documentTipId = 0;
            }
            if (documentTipId > 0)
            {
                using (var context = new documentumEntities())
                {
                    var bookmarks = from Bookmark in context.Bookmarks.Where(s => s.dokumentTipId == documentTipId)
                                    select new
                                    {
                                        Bookmark.Id,
                                        Bookmark.bookmarkTitle,
                                        Bookmark.bookmarkName,
                                        Bookmark.format
                                    };
                    metroGridBookmarks.DataSource = bookmarks.ToList();
                    if (bookmarks.Count() > 0)
                        metroGridBookmarks.Columns[0].Visible = false;
                }
            }

        }


        private void mcbGodina_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadGridData();
            ReloadGridDokumenta();
        }

        private void mbSave_Click(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                int smerGodinaId = Convert.ToInt32(mcbGodina.SelectedValue.ToString());
                int predmetId = Convert.ToInt32(mcbPredmet.SelectedValue.ToString());
                int grupaId = Convert.ToInt32(mcbGrupa.SelectedValue.ToString());
                int redniBroj = Convert.ToInt32(mcbRedniBroj.SelectedItem.ToString());
                int uticeNaUspeh = (byte)Convert.ToInt16(mcUspeh.Checked);
                SmerGodinaPredmet predmet = context.SmerGodinaPredmets.SingleOrDefault(s => s.smerGodinaId == smerGodinaId && s.predmetId == predmetId);
                if (predmet == null)
                {
                    predmet = new SmerGodinaPredmet()
                    {
                        smerGodinaId = Convert.ToInt32(mcbGodina.SelectedValue.ToString()),
                        predmetId = Convert.ToInt32(mcbPredmet.SelectedValue.ToString()),
                        grupaId = Convert.ToInt32(mcbGrupa.SelectedValue.ToString()),
                        redniBroj = Convert.ToInt32(mcbRedniBroj.SelectedItem.ToString()),
                        uticeNaUspeh = (byte)Convert.ToInt16(mcUspeh.Checked)
                    };

                    context.SmerGodinaPredmets.Add(predmet);
                } else
                {
                    predmet.redniBroj = Convert.ToInt32(mcbRedniBroj.SelectedItem.ToString());
                    predmet.grupaId = Convert.ToInt32(mcbGrupa.SelectedValue.ToString());
                    predmet.uticeNaUspeh = (byte)Convert.ToInt16(mcUspeh.Checked);
                }
                context.SaveChanges();
            }
            ReloadGridData();
        }

        private void mbDelete_Click(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                int smerGodinaId = Convert.ToInt32(mcbGodina.SelectedValue.ToString());
                int predmetId = Convert.ToInt32(mcbPredmet.SelectedValue.ToString());
                int grupaId = Convert.ToInt32(mcbGrupa.SelectedValue.ToString());
                int redniBroj = Convert.ToInt32(mcbRedniBroj.SelectedItem.ToString());
                int uticeNaUspeh = (byte)Convert.ToInt16(mcUspeh.Checked);
                SmerGodinaPredmet predmet = context.SmerGodinaPredmets.SingleOrDefault(s => s.smerGodinaId == smerGodinaId && s.predmetId == predmetId&& s.redniBroj == redniBroj);
                if (predmet != null)
                {
                    context.SmerGodinaPredmets.Remove(predmet);
                }
                
                context.SaveChanges();
            }
            ReloadGridData();
        }

        private void metroGridBookmarks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string bookmarkName = DocumentumFactory.GetSelectedGridCellValue(metroGridBookmarks, "bookmarkName").ToString();
            string bookmarkTitle = DocumentumFactory.GetSelectedGridCellValue(metroGridBookmarks, "bookmarkTitle").ToString();
            string format = DocumentumFactory.GetSelectedGridCellValue(metroGridBookmarks, "format").ToString();

            mtbBookmarkName.Text = bookmarkName;
            mtbBookmarkTitle.Text = bookmarkTitle;
            mtbFormat.Text = format;
        }

        private void mbSaveBookmark_Click(object sender, EventArgs e)
        {
            string bookmarkName = mtbBookmarkName.Text;
            string bookmarkTitle = mtbBookmarkTitle.Text;
            string format = mtbFormat.Text;

            int documentTipId = 0;
            try
            {
                documentTipId = int.Parse(DocumentumFactory.GetSelectedGridCellValue(metroGridDokumenta, "dokumentTipId").ToString());
            }
            catch
            {
                documentTipId = 0;
            }
            if (documentTipId > 0)
            {
                using (var context = new documentumEntities())
                {
                    Bookmark bookmark = context.Bookmarks.SingleOrDefault(s => s.dokumentTipId == documentTipId && s.bookmarkName.Equals(bookmarkName));
                    if (bookmark != null)
                    {
                        bookmark.bookmarkTitle = bookmarkTitle;
                        bookmark.format = format;
                    } else
                    {
                        bookmark = new Bookmark()
                        {
                            dokumentTipId = documentTipId,
                            bookmarkTitle = bookmarkTitle,
                            bookmarkName = bookmarkName,
                            format = format
                        };
                        context.Bookmarks.Add(bookmark);

                    }
                    context.SaveChanges();
                }
                ReloadGridBookmarks();
            }
        }

        private void mbDeleteBookmark_Click(object sender, EventArgs e)
        {
            int bookmarkId = DocumentumFactory.GetSelectedGridId(metroGridBookmarks);

            using (var context = new documentumEntities())
            {
                Bookmark bookmark = context.Bookmarks.SingleOrDefault(b => b.Id == bookmarkId);

                context.Bookmarks.Remove(bookmark);

                context.SaveChanges();
            }
        }

        private void metroGridPredmeti_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int smerGodinaPredmetId = DocumentumFactory.GetSelectedGridId(metroGridPredmeti);
            if (smerGodinaPredmetId > -1)
            {
                using (var context = new documentumEntities())
                {
                    SmerGodinaPredmet smerGodinaPredmet = context.SmerGodinaPredmets.SingleOrDefault(s => s.Id == smerGodinaPredmetId);
                    mcbGrupa.SelectedValue = smerGodinaPredmet.grupaId;
                    mcbPredmet.SelectedValue = smerGodinaPredmet.predmetId;
                    mcbRedniBroj.SelectedItem = smerGodinaPredmet.redniBroj.ToString();
                }

            }
        }

        private void metroGridDokumenta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ReloadGridBookmarks();
        }
    }
}
