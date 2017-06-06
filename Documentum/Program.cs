using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documentum
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DocumentumFactory.InitializeApplication();
            FormLogin login = new FormLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }
            
        }
    }
}
