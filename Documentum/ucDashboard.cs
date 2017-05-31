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
    public partial class UcDashboard : MetroFramework.Controls.MetroUserControl
    {
        public UcDashboard()
        {
            InitializeComponent();

        }

        private void UcDashboard_Load(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                var smerovi = from smer in context.Smers
                              select new
                              {
                                  smer.Id,
                                  Naziv = smer.naziv
                              };
                metroGridSmer.DataSource = smerovi.ToList();
                metroGridSmer.Columns[0].Visible = false;
            }
        }

        private void metroGridSmer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (metroGridSmer.SelectedRows.Count > 0)
            {
                selectedRow = metroGridSmer.SelectedRows[0];
            }
            if (selectedRow == null)
                return;
            int smerId = Convert.ToInt32(selectedRow.Cells["Id"].Value.ToString());

            if (!FormMain.Instance.Container.Controls.ContainsKey("UcRazredi"))
            {
                UcRazredi uc = new UcRazredi(smerId)
                {
                    Dock = DockStyle.Fill
                };
                FormMain.Instance.Container.Controls.Add(uc);
            }
            UcRazredi muc = (UcRazredi) FormMain.Instance.Container.Controls["UcRazredi"];
            muc.SmerId = smerId;
            FormMain.Instance.Container.Controls["UcRazredi"].BringToFront();

        }
    }
}
