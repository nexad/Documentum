namespace Documentum
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mtbUserName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mtbPassword = new MetroFramework.Controls.MetroTextBox();
            this.mbLogin = new MetroFramework.Controls.MetroButton();
            this.mbCancel = new MetroFramework.Controls.MetroButton();
            this.mlStatus = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // mtbUserName
            // 
            // 
            // 
            // 
            this.mtbUserName.CustomButton.Image = null;
            this.mtbUserName.CustomButton.Location = new System.Drawing.Point(243, 1);
            this.mtbUserName.CustomButton.Name = "";
            this.mtbUserName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtbUserName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtbUserName.CustomButton.TabIndex = 1;
            this.mtbUserName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtbUserName.CustomButton.UseSelectable = true;
            this.mtbUserName.CustomButton.Visible = false;
            this.mtbUserName.Lines = new string[0];
            this.mtbUserName.Location = new System.Drawing.Point(133, 106);
            this.mtbUserName.MaxLength = 32767;
            this.mtbUserName.Name = "mtbUserName";
            this.mtbUserName.PasswordChar = '\0';
            this.mtbUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtbUserName.SelectedText = "";
            this.mtbUserName.SelectionLength = 0;
            this.mtbUserName.SelectionStart = 0;
            this.mtbUserName.ShortcutsEnabled = true;
            this.mtbUserName.Size = new System.Drawing.Size(265, 23);
            this.mtbUserName.TabIndex = 0;
            this.mtbUserName.UseSelectable = true;
            this.mtbUserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtbUserName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(44, 110);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(53, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Korisnik";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(44, 153);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(35, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Šifra";
            // 
            // mtbPassword
            // 
            // 
            // 
            // 
            this.mtbPassword.CustomButton.Image = null;
            this.mtbPassword.CustomButton.Location = new System.Drawing.Point(243, 1);
            this.mtbPassword.CustomButton.Name = "";
            this.mtbPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtbPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtbPassword.CustomButton.TabIndex = 1;
            this.mtbPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtbPassword.CustomButton.UseSelectable = true;
            this.mtbPassword.CustomButton.Visible = false;
            this.mtbPassword.Lines = new string[0];
            this.mtbPassword.Location = new System.Drawing.Point(133, 149);
            this.mtbPassword.MaxLength = 32767;
            this.mtbPassword.Name = "mtbPassword";
            this.mtbPassword.PasswordChar = '*';
            this.mtbPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtbPassword.SelectedText = "";
            this.mtbPassword.SelectionLength = 0;
            this.mtbPassword.SelectionStart = 0;
            this.mtbPassword.ShortcutsEnabled = true;
            this.mtbPassword.Size = new System.Drawing.Size(265, 23);
            this.mtbPassword.TabIndex = 3;
            this.mtbPassword.UseSelectable = true;
            this.mtbPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtbPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mbLogin
            // 
            this.mbLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbLogin.Location = new System.Drawing.Point(323, 193);
            this.mbLogin.Name = "mbLogin";
            this.mbLogin.Size = new System.Drawing.Size(75, 23);
            this.mbLogin.TabIndex = 4;
            this.mbLogin.Text = "Prihvati";
            this.mbLogin.UseSelectable = true;
            this.mbLogin.Click += new System.EventHandler(this.mbLogin_Click);
            // 
            // mbCancel
            // 
            this.mbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbCancel.Location = new System.Drawing.Point(133, 193);
            this.mbCancel.Name = "mbCancel";
            this.mbCancel.Size = new System.Drawing.Size(75, 23);
            this.mbCancel.TabIndex = 5;
            this.mbCancel.Text = "Otkaži";
            this.mbCancel.UseSelectable = true;
            // 
            // mlStatus
            // 
            this.mlStatus.AutoSize = true;
            this.mlStatus.ForeColor = System.Drawing.Color.Red;
            this.mlStatus.Location = new System.Drawing.Point(133, 69);
            this.mlStatus.Name = "mlStatus";
            this.mlStatus.Size = new System.Drawing.Size(155, 19);
            this.mlStatus.TabIndex = 6;
            this.mlStatus.Text = "Pogrešan korisnik ili šifra!";
            this.mlStatus.UseCustomForeColor = true;
            this.mlStatus.Visible = false;
            // 
            // FormLogin
            // 
            this.AcceptButton = this.mbLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 278);
            this.Controls.Add(this.mlStatus);
            this.Controls.Add(this.mbCancel);
            this.Controls.Add(this.mbLogin);
            this.Controls.Add(this.mtbPassword);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mtbUserName);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox mtbUserName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox mtbPassword;
        private MetroFramework.Controls.MetroButton mbLogin;
        private MetroFramework.Controls.MetroButton mbCancel;
        private MetroFramework.Controls.MetroLabel mlStatus;
    }
}