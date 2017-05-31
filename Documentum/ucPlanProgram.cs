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
                                       SmerGodinaDokument.DokumentTip.outputPath
                                   };
                    metroGridDokumenta.DataSource = dokumenta.ToList();
                    if (dokumenta.Count() > 0)
                        metroGridDokumenta.Columns[0].Visible = false;
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
            SmerGodinaPredmet predmet = new SmerGodinaPredmet()
            {
                smerGodinaId = Convert.ToInt32(mcbGodina.SelectedValue.ToString()),
                predmetId = Convert.ToInt32(mcbPredmet.SelectedValue.ToString()),
                grupaId = Convert.ToInt32(mcbGrupa.SelectedValue.ToString()),
                redniBroj = Convert.ToInt32(mcbRedniBroj.SelectedItem.ToString()),
                uticeNaUspeh = (byte)Convert.ToInt16(mcUspeh.Checked)
            };
            using (var context = new documentumEntities())
            {
                context.SmerGodinaPredmets.Add(predmet);
                context.SaveChanges();
            }
            ReloadGridData();
        }
    }
}
