namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    partial class ucNguoiDungTimDuong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbLuotVe = new System.Windows.Forms.RadioButton();
            this.rdbLuotDi = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDiemDen = new System.Windows.Forms.ComboBox();
            this.cbDiemDi = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbXeTuyen4 = new System.Windows.Forms.ComboBox();
            this.cbXeTuyen3 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbXeTuyen2 = new System.Windows.Forms.ComboBox();
            this.cbXeTuyen1 = new System.Windows.Forms.ComboBox();
            this.lblQuangDuong = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.lbTuyenXe = new System.Windows.Forms.ListBox();
            this.lbTram = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBox1.Controls.Add(this.rdbLuotVe);
            this.groupBox1.Controls.Add(this.rdbLuotDi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbDiemDen);
            this.groupBox1.Controls.Add(this.cbDiemDi);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(85, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 179);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm đường";
            // 
            // rdbLuotVe
            // 
            this.rdbLuotVe.AutoSize = true;
            this.rdbLuotVe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLuotVe.Location = new System.Drawing.Point(218, 141);
            this.rdbLuotVe.Name = "rdbLuotVe";
            this.rdbLuotVe.Size = new System.Drawing.Size(61, 17);
            this.rdbLuotVe.TabIndex = 5;
            this.rdbLuotVe.TabStop = true;
            this.rdbLuotVe.Text = "Lượt về";
            this.rdbLuotVe.UseVisualStyleBackColor = true;
            this.rdbLuotVe.CheckedChanged += new System.EventHandler(this.timDuongcbSelectedIndexChange);
            // 
            // rdbLuotDi
            // 
            this.rdbLuotDi.AutoSize = true;
            this.rdbLuotDi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLuotDi.Location = new System.Drawing.Point(98, 141);
            this.rdbLuotDi.Name = "rdbLuotDi";
            this.rdbLuotDi.Size = new System.Drawing.Size(58, 17);
            this.rdbLuotDi.TabIndex = 4;
            this.rdbLuotDi.TabStop = true;
            this.rdbLuotDi.Text = "Lượt đi";
            this.rdbLuotDi.UseVisualStyleBackColor = true;
            this.rdbLuotDi.CheckedChanged += new System.EventHandler(this.timDuongcbSelectedIndexChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Điểm đến:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Điểm đi:";
            // 
            // cbDiemDen
            // 
            this.cbDiemDen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDiemDen.FormattingEnabled = true;
            this.cbDiemDen.Location = new System.Drawing.Point(98, 86);
            this.cbDiemDen.Name = "cbDiemDen";
            this.cbDiemDen.Size = new System.Drawing.Size(181, 21);
            this.cbDiemDen.TabIndex = 1;
            this.cbDiemDen.SelectedIndexChanged += new System.EventHandler(this.timDuongcbSelectedIndexChange);
            // 
            // cbDiemDi
            // 
            this.cbDiemDi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDiemDi.FormattingEnabled = true;
            this.cbDiemDi.Location = new System.Drawing.Point(98, 39);
            this.cbDiemDi.Name = "cbDiemDi";
            this.cbDiemDi.Size = new System.Drawing.Size(181, 21);
            this.cbDiemDi.TabIndex = 0;
            this.cbDiemDi.SelectedIndexChanged += new System.EventHandler(this.timDuongcbSelectedIndexChange);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBox2.Controls.Add(this.lbTram);
            this.groupBox2.Controls.Add(this.lbTuyenXe);
            this.groupBox2.Controls.Add(this.cbXeTuyen4);
            this.groupBox2.Controls.Add(this.cbXeTuyen3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbXeTuyen2);
            this.groupBox2.Controls.Add(this.cbXeTuyen1);
            this.groupBox2.Controls.Add(this.lblQuangDuong);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(469, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 222);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Xác nhận thông tin chuyến đi";
            // 
            // cbXeTuyen4
            // 
            this.cbXeTuyen4.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbXeTuyen4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXeTuyen4.FormattingEnabled = true;
            this.cbXeTuyen4.Location = new System.Drawing.Point(259, 115);
            this.cbXeTuyen4.Name = "cbXeTuyen4";
            this.cbXeTuyen4.Size = new System.Drawing.Size(45, 21);
            this.cbXeTuyen4.TabIndex = 43;
            // 
            // cbXeTuyen3
            // 
            this.cbXeTuyen3.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbXeTuyen3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXeTuyen3.FormattingEnabled = true;
            this.cbXeTuyen3.Location = new System.Drawing.Point(202, 115);
            this.cbXeTuyen3.Name = "cbXeTuyen3";
            this.cbXeTuyen3.Size = new System.Drawing.Size(45, 21);
            this.cbXeTuyen3.TabIndex = 42;
            this.cbXeTuyen3.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Chọn mã xe:";
            // 
            // cbXeTuyen2
            // 
            this.cbXeTuyen2.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbXeTuyen2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXeTuyen2.FormattingEnabled = true;
            this.cbXeTuyen2.Location = new System.Drawing.Point(145, 115);
            this.cbXeTuyen2.Name = "cbXeTuyen2";
            this.cbXeTuyen2.Size = new System.Drawing.Size(45, 21);
            this.cbXeTuyen2.TabIndex = 41;
            this.cbXeTuyen2.Visible = false;
            // 
            // cbXeTuyen1
            // 
            this.cbXeTuyen1.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbXeTuyen1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXeTuyen1.FormattingEnabled = true;
            this.cbXeTuyen1.Location = new System.Drawing.Point(90, 115);
            this.cbXeTuyen1.Name = "cbXeTuyen1";
            this.cbXeTuyen1.Size = new System.Drawing.Size(45, 21);
            this.cbXeTuyen1.TabIndex = 12;
            this.cbXeTuyen1.Visible = false;
            // 
            // lblQuangDuong
            // 
            this.lblQuangDuong.AutoSize = true;
            this.lblQuangDuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuangDuong.Location = new System.Drawing.Point(141, 19);
            this.lblQuangDuong.Name = "lblQuangDuong";
            this.lblQuangDuong.Size = new System.Drawing.Size(0, 13);
            this.lblQuangDuong.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Tuyến xe bus dự kiến:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Các trạm phải qua";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quãng đường dự kiến:";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnThanhToan.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnThanhToan.Location = new System.Drawing.Point(85, 271);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(88, 23);
            this.btnThanhToan.TabIndex = 62;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // lbTuyenXe
            // 
            this.lbTuyenXe.BackColor = System.Drawing.Color.LavenderBlush;
            this.lbTuyenXe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTuyenXe.FormattingEnabled = true;
            this.lbTuyenXe.Location = new System.Drawing.Point(127, 54);
            this.lbTuyenXe.Name = "lbTuyenXe";
            this.lbTuyenXe.Size = new System.Drawing.Size(177, 43);
            this.lbTuyenXe.TabIndex = 63;
            // 
            // lbTram
            // 
            this.lbTram.BackColor = System.Drawing.Color.LavenderBlush;
            this.lbTram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTram.FormattingEnabled = true;
            this.lbTram.Location = new System.Drawing.Point(127, 161);
            this.lbTram.Name = "lbTram";
            this.lbTram.Size = new System.Drawing.Size(177, 43);
            this.lbTram.TabIndex = 64;
            // 
            // ucNguoiDungTimDuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucNguoiDungTimDuong";
            this.Size = new System.Drawing.Size(871, 365);
            this.Load += new System.EventHandler(this.ucNguoiDungTimDuong_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbLuotVe;
        private System.Windows.Forms.RadioButton rdbLuotDi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDiemDen;
        private System.Windows.Forms.ComboBox cbDiemDi;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox cbXeTuyen4;
        public System.Windows.Forms.ComboBox cbXeTuyen3;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cbXeTuyen2;
        public System.Windows.Forms.ComboBox cbXeTuyen1;
        public System.Windows.Forms.Label lblQuangDuong;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.ListBox lbTuyenXe;
        private System.Windows.Forms.ListBox lbTram;
    }
}
