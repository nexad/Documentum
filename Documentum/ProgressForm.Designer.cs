namespace Documentum
{
    partial class ProgressForm
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
            this.mpbProgress = new MetroFramework.Controls.MetroProgressBar();
            this.mlStatus = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // mpbProgress
            // 
            this.mpbProgress.Location = new System.Drawing.Point(43, 121);
            this.mpbProgress.Name = "mpbProgress";
            this.mpbProgress.Size = new System.Drawing.Size(367, 23);
            this.mpbProgress.TabIndex = 0;
            // 
            // mlStatus
            // 
            this.mlStatus.AutoSize = true;
            this.mlStatus.Location = new System.Drawing.Point(43, 76);
            this.mlStatus.Name = "mlStatus";
            this.mlStatus.Size = new System.Drawing.Size(46, 19);
            this.mlStatus.TabIndex = 1;
            this.mlStatus.Text = "Status:";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 260);
            this.Controls.Add(this.mlStatus);
            this.Controls.Add(this.mpbProgress);
            this.Name = "ProgressForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroProgressBar mpbProgress;
        private MetroFramework.Controls.MetroLabel mlStatus;
    }
}