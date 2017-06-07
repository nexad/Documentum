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
        static FormMain _instance;

        public static FormMain Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FormMain();
                return _instance;
            }
        }

        public MetroFramework.Controls.MetroPanel Container
        {
            get { return metroPanelMain; }
            set { metroPanelMain = value; }
        }
        public FormMain()
        {
            InitializeComponent();
        }
       
        private void FormMain_Load(object sender, EventArgs e)
        {

            UcRazredi uc = new UcRazredi()
            {
                Dock = DockStyle.Fill
            };
            metroPanelMain.Controls.Add(uc);
            metroPanelMain.Controls["UcRazredi"].BringToFront();
            Text = Text + " - " + DocumentumFactory.Login.ime + " " + DocumentumFactory.Login.prezime;
        }

        private void mbMain_Click(object sender, EventArgs e)
        {
            metroPanelMain.Controls["UcRazredi"].BringToFront();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (!metroPanelMain.Controls.ContainsKey("UcPlanProgram"))
            {
                UcPlanProgram uc = new UcPlanProgram()
                {
                    Dock = DockStyle.Fill
                };
                metroPanelMain.Controls.Add(uc);
            }
            metroPanelMain.Controls["UcPlanProgram"].BringToFront();
        }
    }
}
