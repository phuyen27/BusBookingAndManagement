namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    partial class FrmQuanLyInLuongNV
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
            this.rpvLuongNV = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvLuongNV
            // 
            this.rpvLuongNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvLuongNV.LocalReport.ReportEmbeddedResource = "_29_PhamThiPhuongUyen_9401_QLDatVeXeBus.rpQuanLyInLuongNV.rdlc";
            this.rpvLuongNV.Location = new System.Drawing.Point(0, 0);
            this.rpvLuongNV.Name = "rpvLuongNV";
            this.rpvLuongNV.ServerReport.BearerToken = null;
            this.rpvLuongNV.Size = new System.Drawing.Size(696, 450);
            this.rpvLuongNV.TabIndex = 0;
            // 
            // FrmQuanLyInLuongNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 450);
            this.Controls.Add(this.rpvLuongNV);
            this.Name = "FrmQuanLyInLuongNV";
            this.Text = "FrmQuanLyInLuongNV";
            this.Load += new System.EventHandler(this.FrmQuanLyInLuongNV_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvLuongNV;
    }
}