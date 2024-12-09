namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    partial class FrmQuanLyInHoaDon
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
            this.rpvInHoaDon = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvInHoaDon
            // 
            this.rpvInHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvInHoaDon.LocalReport.ReportEmbeddedResource = "_29_PhamThiPhuongUyen_9401_QLDatVeXeBus.rpQuanLyInHoaDon.rdlc";
            this.rpvInHoaDon.Location = new System.Drawing.Point(0, 0);
            this.rpvInHoaDon.Name = "rpvInHoaDon";
            this.rpvInHoaDon.ServerReport.BearerToken = null;
            this.rpvInHoaDon.Size = new System.Drawing.Size(696, 450);
            this.rpvInHoaDon.TabIndex = 0;
            // 
            // FrmQuanLyInHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 450);
            this.Controls.Add(this.rpvInHoaDon);
            this.Name = "FrmQuanLyInHoaDon";
            this.Text = "FrmQuanLyInHoaDon";
            this.Load += new System.EventHandler(this.FrmQuanLyInHoaDon_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvInHoaDon;
    }
}