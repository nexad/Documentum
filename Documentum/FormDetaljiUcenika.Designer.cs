namespace Documentum
{
    partial class FormDetaljiUcenika
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mbPregled = new MetroFramework.Controls.MetroButton();
            this.metroGridDokumenta = new MetroFramework.Controls.MetroGrid();
            this.mlImePrezime = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel4 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.mcbPredmet = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.mbSave = new MetroFramework.Controls.MetroButton();
            this.mtbOcenaOpis = new MetroFramework.Controls.MetroTextBox();
            this.mtbOcena = new MetroFramework.Controls.MetroTextBox();
            this.metroGridOcene = new MetroFramework.Controls.MetroGrid();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.mbSaveBookmark = new MetroFramework.Controls.MetroButton();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.mtbBookmarkValue = new MetroFramework.Controls.MetroTextBox();
            this.metroGridUcenikBookmarks = new MetroFramework.Controls.MetroGrid();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridDokumenta)).BeginInit();
            this.metroPanel2.SuspendLayout();
            this.metroPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridOcene)).BeginInit();
            this.metroPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridUcenikBookmarks)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(14, 21);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(50, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Učenik:";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroLabel2);
            this.metroPanel1.Controls.Add(this.mbPregled);
            this.metroPanel1.Controls.Add(this.metroGridDokumenta);
            this.metroPanel1.Controls.Add(this.mlImePrezime);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1137, 207);
            this.metroPanel1.TabIndex = 2;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(563, 21);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(76, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Dokumenta";
            // 
            // mbPregled
            // 
            this.mbPregled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mbPregled.Location = new System.Drawing.Point(1015, 21);
            this.mbPregled.Name = "mbPregled";
            this.mbPregled.Size = new System.Drawing.Size(75, 23);
            this.mbPregled.Style = MetroFramework.MetroColorStyle.Orange;
            this.mbPregled.TabIndex = 4;
            this.mbPregled.Text = "Pregled";
            this.mbPregled.UseSelectable = true;
            this.mbPregled.UseStyleColors = true;
            this.mbPregled.Click += new System.EventHandler(this.MbPregled_Click);
            // 
            // metroGridDokumenta
            // 
            this.metroGridDokumenta.AllowUserToResizeRows = false;
            this.metroGridDokumenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroGridDokumenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.metroGridDokumenta.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.metroGridDokumenta.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridDokumenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGridDokumenta.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGridDokumenta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridDokumenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGridDokumenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGridDokumenta.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGridDokumenta.EnableHeadersVisualStyles = false;
            this.metroGridDokumenta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGridDokumenta.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridDokumenta.Location = new System.Drawing.Point(645, 21);
            this.metroGridDokumenta.Name = "metroGridDokumenta";
            this.metroGridDokumenta.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridDokumenta.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroGridDokumenta.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGridDokumenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGridDokumenta.Size = new System.Drawing.Size(364, 180);
            this.metroGridDokumenta.TabIndex = 3;
            this.metroGridDokumenta.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGridDokumenta_RowEnter);
            // 
            // mlImePrezime
            // 
            this.mlImePrezime.AutoSize = true;
            this.mlImePrezime.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlImePrezime.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlImePrezime.Location = new System.Drawing.Point(14, 64);
            this.mlImePrezime.Name = "mlImePrezime";
            this.mlImePrezime.Size = new System.Drawing.Size(127, 25);
            this.mlImePrezime.TabIndex = 2;
            this.mlImePrezime.Text = "Ime i prezime";
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.metroPanel4);
            this.metroPanel2.Controls.Add(this.metroGridOcene);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(20, 267);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(602, 357);
            this.metroPanel2.TabIndex = 3;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroPanel4
            // 
            this.metroPanel4.Controls.Add(this.metroLabel5);
            this.metroPanel4.Controls.Add(this.mcbPredmet);
            this.metroPanel4.Controls.Add(this.metroLabel4);
            this.metroPanel4.Controls.Add(this.metroLabel3);
            this.metroPanel4.Controls.Add(this.mbSave);
            this.metroPanel4.Controls.Add(this.mtbOcenaOpis);
            this.metroPanel4.Controls.Add(this.mtbOcena);
            this.metroPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel4.HorizontalScrollbarBarColor = true;
            this.metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel4.HorizontalScrollbarSize = 10;
            this.metroPanel4.Location = new System.Drawing.Point(0, 0);
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.Size = new System.Drawing.Size(600, 158);
            this.metroPanel4.TabIndex = 6;
            this.metroPanel4.VerticalScrollbarBarColor = true;
            this.metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel4.VerticalScrollbarSize = 10;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(6, 23);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(60, 19);
            this.metroLabel5.TabIndex = 8;
            this.metroLabel5.Text = "Predmet";
            // 
            // mcbPredmet
            // 
            this.mcbPredmet.FormattingEnabled = true;
            this.mcbPredmet.ItemHeight = 23;
            this.mcbPredmet.Location = new System.Drawing.Point(133, 13);
            this.mcbPredmet.Name = "mcbPredmet";
            this.mcbPredmet.Size = new System.Drawing.Size(216, 29);
            this.mcbPredmet.TabIndex = 7;
            this.mcbPredmet.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(6, 81);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(75, 19);
            this.metroLabel4.TabIndex = 6;
            this.metroLabel4.Text = "Ocena opis";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(6, 52);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(47, 19);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Ocena";
            // 
            // mbSave
            // 
            this.mbSave.Location = new System.Drawing.Point(133, 123);
            this.mbSave.Name = "mbSave";
            this.mbSave.Size = new System.Drawing.Size(75, 23);
            this.mbSave.Style = MetroFramework.MetroColorStyle.Orange;
            this.mbSave.TabIndex = 4;
            this.mbSave.Text = "Snimi";
            this.mbSave.UseSelectable = true;
            this.mbSave.UseStyleColors = true;
            // 
            // mtbOcenaOpis
            // 
            // 
            // 
            // 
            this.mtbOcenaOpis.CustomButton.Image = null;
            this.mtbOcenaOpis.CustomButton.Location = new System.Drawing.Point(53, 1);
            this.mtbOcenaOpis.CustomButton.Name = "";
            this.mtbOcenaOpis.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtbOcenaOpis.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtbOcenaOpis.CustomButton.TabIndex = 1;
            this.mtbOcenaOpis.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtbOcenaOpis.CustomButton.UseSelectable = true;
            this.mtbOcenaOpis.CustomButton.Visible = false;
            this.mtbOcenaOpis.Lines = new string[0];
            this.mtbOcenaOpis.Location = new System.Drawing.Point(133, 77);
            this.mtbOcenaOpis.MaxLength = 32767;
            this.mtbOcenaOpis.Name = "mtbOcenaOpis";
            this.mtbOcenaOpis.PasswordChar = '\0';
            this.mtbOcenaOpis.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtbOcenaOpis.SelectedText = "";
            this.mtbOcenaOpis.SelectionLength = 0;
            this.mtbOcenaOpis.SelectionStart = 0;
            this.mtbOcenaOpis.ShortcutsEnabled = true;
            this.mtbOcenaOpis.Size = new System.Drawing.Size(75, 23);
            this.mtbOcenaOpis.TabIndex = 3;
            this.mtbOcenaOpis.UseSelectable = true;
            this.mtbOcenaOpis.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtbOcenaOpis.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mtbOcena
            // 
            // 
            // 
            // 
            this.mtbOcena.CustomButton.Image = null;
            this.mtbOcena.CustomButton.Location = new System.Drawing.Point(53, 1);
            this.mtbOcena.CustomButton.Name = "";
            this.mtbOcena.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtbOcena.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtbOcena.CustomButton.TabIndex = 1;
            this.mtbOcena.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtbOcena.CustomButton.UseSelectable = true;
            this.mtbOcena.CustomButton.Visible = false;
            this.mtbOcena.Lines = new string[0];
            this.mtbOcena.Location = new System.Drawing.Point(133, 48);
            this.mtbOcena.MaxLength = 32767;
            this.mtbOcena.Name = "mtbOcena";
            this.mtbOcena.PasswordChar = '\0';
            this.mtbOcena.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtbOcena.SelectedText = "";
            this.mtbOcena.SelectionLength = 0;
            this.mtbOcena.SelectionStart = 0;
            this.mtbOcena.ShortcutsEnabled = true;
            this.mtbOcena.Size = new System.Drawing.Size(75, 23);
            this.mtbOcena.TabIndex = 2;
            this.mtbOcena.UseSelectable = true;
            this.mtbOcena.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtbOcena.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroGridOcene
            // 
            this.metroGridOcene.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.metroGridOcene.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.metroGridOcene.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.metroGridOcene.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.metroGridOcene.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.metroGridOcene.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridOcene.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGridOcene.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGridOcene.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridOcene.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.metroGridOcene.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGridOcene.DefaultCellStyle = dataGridViewCellStyle6;
            this.metroGridOcene.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.metroGridOcene.EnableHeadersVisualStyles = false;
            this.metroGridOcene.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGridOcene.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridOcene.Location = new System.Drawing.Point(-1, 164);
            this.metroGridOcene.Name = "metroGridOcene";
            this.metroGridOcene.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridOcene.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.metroGridOcene.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGridOcene.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGridOcene.Size = new System.Drawing.Size(602, 191);
            this.metroGridOcene.TabIndex = 2;
            this.metroGridOcene.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGridOcene_RowEnter);
            // 
            // metroPanel3
            // 
            this.metroPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel3.Controls.Add(this.mbSaveBookmark);
            this.metroPanel3.Controls.Add(this.metroLabel6);
            this.metroPanel3.Controls.Add(this.mtbBookmarkValue);
            this.metroPanel3.Controls.Add(this.metroGridUcenikBookmarks);
            this.metroPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(622, 267);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(535, 357);
            this.metroPanel3.TabIndex = 4;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // mbSaveBookmark
            // 
            this.mbSaveBookmark.Location = new System.Drawing.Point(156, 124);
            this.mbSaveBookmark.Name = "mbSaveBookmark";
            this.mbSaveBookmark.Size = new System.Drawing.Size(111, 23);
            this.mbSaveBookmark.Style = MetroFramework.MetroColorStyle.Orange;
            this.mbSaveBookmark.TabIndex = 5;
            this.mbSaveBookmark.Text = "Snimi bookmark";
            this.mbSaveBookmark.UseSelectable = true;
            this.mbSaveBookmark.UseStyleColors = true;
            this.mbSaveBookmark.Click += new System.EventHandler(this.mbSaveBookmark_Click);
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(6, 24);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(123, 19);
            this.metroLabel6.TabIndex = 4;
            this.metroLabel6.Text = "Bookmark vrednost";
            // 
            // mtbBookmarkValue
            // 
            // 
            // 
            // 
            this.mtbBookmarkValue.CustomButton.Image = null;
            this.mtbBookmarkValue.CustomButton.Location = new System.Drawing.Point(316, 1);
            this.mtbBookmarkValue.CustomButton.Name = "";
            this.mtbBookmarkValue.CustomButton.Size = new System.Drawing.Size(85, 85);
            this.mtbBookmarkValue.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtbBookmarkValue.CustomButton.TabIndex = 1;
            this.mtbBookmarkValue.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtbBookmarkValue.CustomButton.UseSelectable = true;
            this.mtbBookmarkValue.CustomButton.Visible = false;
            this.mtbBookmarkValue.Lines = new string[0];
            this.mtbBookmarkValue.Location = new System.Drawing.Point(156, 14);
            this.mtbBookmarkValue.MaxLength = 32767;
            this.mtbBookmarkValue.Multiline = true;
            this.mtbBookmarkValue.Name = "mtbBookmarkValue";
            this.mtbBookmarkValue.PasswordChar = '\0';
            this.mtbBookmarkValue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtbBookmarkValue.SelectedText = "";
            this.mtbBookmarkValue.SelectionLength = 0;
            this.mtbBookmarkValue.SelectionStart = 0;
            this.mtbBookmarkValue.ShortcutsEnabled = true;
            this.mtbBookmarkValue.Size = new System.Drawing.Size(402, 87);
            this.mtbBookmarkValue.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtbBookmarkValue.TabIndex = 3;
            this.mtbBookmarkValue.UseSelectable = true;
            this.mtbBookmarkValue.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtbBookmarkValue.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroGridUcenikBookmarks
            // 
            this.metroGridUcenikBookmarks.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.metroGridUcenikBookmarks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.metroGridUcenikBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.metroGridUcenikBookmarks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.metroGridUcenikBookmarks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.metroGridUcenikBookmarks.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridUcenikBookmarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGridUcenikBookmarks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGridUcenikBookmarks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridUcenikBookmarks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.metroGridUcenikBookmarks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGridUcenikBookmarks.DefaultCellStyle = dataGridViewCellStyle10;
            this.metroGridUcenikBookmarks.EnableHeadersVisualStyles = false;
            this.metroGridUcenikBookmarks.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGridUcenikBookmarks.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridUcenikBookmarks.Location = new System.Drawing.Point(6, 164);
            this.metroGridUcenikBookmarks.Name = "metroGridUcenikBookmarks";
            this.metroGridUcenikBookmarks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridUcenikBookmarks.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.metroGridUcenikBookmarks.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGridUcenikBookmarks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGridUcenikBookmarks.Size = new System.Drawing.Size(524, 144);
            this.metroGridUcenikBookmarks.TabIndex = 2;
            this.metroGridUcenikBookmarks.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGridUcenikBookmarks_RowEnter);
            // 
            // FormDetaljiUcenika
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 644);
            this.Controls.Add(this.metroPanel3);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Name = "FormDetaljiUcenika";
            this.Text = "Detalji Ucenika";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormDetaljiUcenika_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridDokumenta)).EndInit();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel4.ResumeLayout(false);
            this.metroPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridOcene)).EndInit();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridUcenikBookmarks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroGrid metroGridOcene;
        private MetroFramework.Controls.MetroLabel mlImePrezime;
        private MetroFramework.Controls.MetroGrid metroGridDokumenta;
        private MetroFramework.Controls.MetroButton mbPregled;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroGrid metroGridUcenikBookmarks;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroTextBox mtbBookmarkValue;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox mcbPredmet;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton mbSave;
        private MetroFramework.Controls.MetroTextBox mtbOcenaOpis;
        private MetroFramework.Controls.MetroTextBox mtbOcena;
        private MetroFramework.Controls.MetroButton mbSaveBookmark;
    }
}