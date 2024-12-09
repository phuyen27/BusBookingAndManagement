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
    public partial class FrmNguoiDungInHoaDon : Form
    {
        public FrmNguoiDungInHoaDon()
        {
            InitializeComponent();
        }

        classKetNoi ketNoi = new classKetNoi();

        private void FrmNguoiDungInHoaDon_Load(object sender, EventArgs e)
        {

            this.rpvInHoaDon.RefreshReport();

            // Truy vấn lấy dữ liệu từ bảng HOA_DON
            string query = "XemHD_MAKH " + classKetNoi.maNguoiDung;

            // Gọi phương thức filldb của DataProvider để lấy dữ liệu
            DataTable dt = ketNoi.filldb(query);

            // Kiểm tra xem DataTable có dữ liệu hay không
            if (dt != null && dt.Rows.Count > 0)
            {
                // Tạo ReportDataSource và gán dữ liệu vào ReportViewer
                ReportDataSource rds = new ReportDataSource("DataSetNguoiDungHD", dt);
                rpvInHoaDon.LocalReport.DataSources.Clear();  // Xóa DataSource cũ (nếu có)
                rpvInHoaDon.LocalReport.DataSources.Add(rds);  // Thêm DataSource mới
                this.rpvInHoaDon.RefreshReport();  // Làm mới báo cáo
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.rpvInHoaDon.RefreshReport();
        }
    }
}
