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
        public string Encrypt(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");

            //encrypt data
            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string cipher)
        {
            if (cipher == null) throw new ArgumentNullException("cipher");

            //parse base64 string
            byte[] data = Convert.FromBase64String(cipher);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void mbLogin_Click(object sender, EventArgs e)
        {
            string userName = mtbUserName.Text;
            string password = mtbPassword.Text;

            

            using (var context = new documentumEntities())
            {
                Nastavnik nastavnik = context.Nastavniks.SingleOrDefault(n => n.username.Equals(userName));
                if (nastavnik == null)
                {
                    mlStatus.Visible = true;
                    DialogResult = DialogResult.None;
                } else
                {
                    if (!password.Equals(Decrypt(nastavnik.password)))
                    {
                        mlStatus.Visible = true;
                        DialogResult = DialogResult.None;
                    } else
                    {
                        DocumentumFactory.login = nastavnik;
                    }
                }
            }
        }
    }
}
