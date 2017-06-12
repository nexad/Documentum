using DocumentFormat.OpenXml.Office.CustomXsn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documentum
{
    public partial class FormLogin : MetroFramework.Forms.MetroForm
    {
        

        public FormLogin()
        {
            InitializeComponent();
        }

        private void mbLogin_Click(object sender, EventArgs e)
        {
            string userName = mtbUserName.Text;
            string password = mtbPassword.Text;

            SimpleAES simpleAES = new SimpleAES();
            //string crypted = simpleAES.EncryptToString(password);

            using (var context = new documentumEntities())
            {
                Nastavnik nastavnik = context.Nastavniks.SingleOrDefault(n => n.username.Equals(userName));
                if (nastavnik == null)
                {
                    mlStatus.Visible = true;
                    DialogResult = DialogResult.None;
                } else
                {
                    string passwordDecrypt = "";
                    try
                    {
                        passwordDecrypt  = simpleAES.DecryptString(nastavnik.password);
                    } catch
                    {

                    }

                    if (!password.Equals(passwordDecrypt))
                    {
                        mlStatus.Visible = true;
                        DialogResult = DialogResult.None;
                    } else
                    {
                        try
                        {
                            DocumentumFactory.Login = nastavnik;
                        } catch
                        {

                        }
                        
                    }
                }
            }
        }
    }
}
