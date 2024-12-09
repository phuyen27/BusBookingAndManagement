using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    public partial class FrmQuanLyDanhGiaKH : Form
    {
        public FrmQuanLyDanhGiaKH()
        {
            InitializeComponent();
        }

        classKetNoi ketNoi = new classKetNoi();
        public string thang = "";

        private void FrmQuanLyDanhGiaKH_Load(object sender, EventArgs e)
        {

            this.rpvDanhGia.RefreshReport();

            string query;
            if (thang == "")
            {
                query = "XemDanhGia";

            }
            else query = "XemDanhGia " + thang;

            // Gọi phương thức filldb của DataProvider để lấy dữ liệu
            DataTable dt = ketNoi.filldb(query);

            // Kiểm tra xem DataTable có dữ liệu hay không
            if (dt != null && dt.Rows.Count > 0)
            {
                // Tạo ReportDataSource và gán dữ liệu vào ReportViewer
                ReportDataSource rds = new ReportDataSource("DataSetDanhGia", dt);
                rpvDanhGia.LocalReport.DataSources.Clear();  // Xóa DataSource cũ (nếu có)
                rpvDanhGia.LocalReport.DataSources.Add(rds);  // Thêm DataSource mới
                this.rpvDanhGia.RefreshReport();  // Làm mới báo cáo
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }
    }
}
