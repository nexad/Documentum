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
    public partial class ProgressForm : MetroFramework.Forms.MetroForm
    {
        public int Progress { get; set; }
        public string Label { get; set; }

        public ProgressForm()
        {
            InitializeComponent();
        }

        public void SetProgress(int progress, string label)
        {
            if (progress > 100)
                progress = 100;
            Progress = progress;
            Label = label;
            GetMlStatus().Text = Label;
            mpbProgress.Value = Progress;
            Text = Label;
        }

        private MetroFramework.Controls.MetroLabel GetMlStatus()
        {
            return mlStatus;
        }
    }
}
