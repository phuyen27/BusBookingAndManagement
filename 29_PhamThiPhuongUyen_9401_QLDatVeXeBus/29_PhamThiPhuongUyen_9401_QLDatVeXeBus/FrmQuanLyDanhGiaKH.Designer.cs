namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    partial class FrmQuanLyDanhGiaKH
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
            this.rpvDanhGia = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvDanhGia
            // 
            this.rpvDanhGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvDanhGia.LocalReport.ReportEmbeddedResource = "_29_PhamThiPhuongUyen_9401_QLDatVeXeBus.rpQuanLyInDanhGia.rdlc";
            this.rpvDanhGia.Location = new System.Drawing.Point(0, 0);
            this.rpvDanhGia.Name = "rpvDanhGia";
            this.rpvDanhGia.ServerReport.BearerToken = null;
            this.rpvDanhGia.Size = new System.Drawing.Size(777, 450);
            this.rpvDanhGia.TabIndex = 0;
            // 
            // FrmQuanLyDanhGiaKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 450);
            this.Controls.Add(this.rpvDanhGia);
            this.Name = "FrmQuanLyDanhGiaKH";
            this.Text = "FrmQuanLyDanhGiaKH";
            this.Load += new System.EventHandler(this.FrmQuanLyDanhGiaKH_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvDanhGia;
    }
}