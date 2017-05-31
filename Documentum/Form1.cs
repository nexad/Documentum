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
    public partial class FormMain : MetroFramework.Forms.MetroForm
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                var smerovi = from smer in context.Smers
                              select new
                              {
                                  smer.Id,
                                  smer.naziv
                              };
                metroGridSmerovi.DataSource = smerovi.ToList();
                metroGridSmerovi.Columns[0].Visible = false;
            }
        }
    }
}
