namespace Documentum
{
    partial class FormMain
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
            this.metroPanelMain = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.mbMain = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanelMain
            // 
            this.metroPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanelMain.HorizontalScrollbarBarColor = true;
            this.metroPanelMain.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanelMain.HorizontalScrollbarSize = 10;
            this.metroPanelMain.Location = new System.Drawing.Point(23, 125);
            this.metroPanelMain.Name = "metroPanelMain";
            this.metroPanelMain.Size = new System.Drawing.Size(622, 335);
            this.metroPanelMain.TabIndex = 2;
            this.metroPanelMain.VerticalScrollbarBarColor = true;
            this.metroPanelMain.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanelMain.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroButton2);
            this.metroPanel1.Controls.Add(this.mbMain);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 63);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(622, 56);
            this.metroPanel1.TabIndex = 3;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // mbMain
            // 
            this.mbMain.BackColor = System.Drawing.Color.SkyBlue;
            this.mbMain.Location = new System.Drawing.Point(3, 3);
            this.mbMain.Name = "mbMain";
            this.mbMain.Size = new System.Drawing.Size(88, 35);
            this.mbMain.Style = MetroFramework.MetroColorStyle.Orange;
            this.mbMain.TabIndex = 2;
            this.mbMain.Text = "Početna";
            this.mbMain.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mbMain.UseSelectable = true;
            this.mbMain.UseStyleColors = true;
            this.mbMain.Click += new System.EventHandler(this.mbMain_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.SkyBlue;
            this.metroButton2.Location = new System.Drawing.Point(97, 3);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(88, 35);
            this.metroButton2.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = "Plan i program";
            this.metroButton2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton2.UseSelectable = true;
            this.metroButton2.UseStyleColors = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 480);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroPanelMain);
            this.Name = "FormMain";
            this.Text = "Documentum";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel metroPanelMain;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton mbMain;
    }
}

