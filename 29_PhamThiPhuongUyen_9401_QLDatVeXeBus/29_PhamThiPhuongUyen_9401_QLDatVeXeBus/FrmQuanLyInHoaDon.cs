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
    public partial class FrmQuanLyInHoaDon : Form
    {
        public FrmQuanLyInHoaDon()
        {
            InitializeComponent();
        }

        classKetNoi ketNoi = new classKetNoi();

        public string thang = "";

        private void FrmQuanLyInHoaDon_Load(object sender, EventArgs e)
        {

            this.rpvInHoaDon.RefreshReport();
            string query;
            if (thang == "")
            {
                query = "XemHoaDon ";

            }
            else query = "XemHoaDon " + thang;

            // Gọi phương thức filldb của DataProvider để lấy dữ liệu
            DataTable dt = ketNoi.filldb(query);

            // Kiểm tra xem DataTable có dữ liệu hay không
            if (dt != null && dt.Rows.Count > 0)
            {
                // Tạo ReportDataSource và gán dữ liệu vào ReportViewer
                ReportDataSource rds = new ReportDataSource("DataSetHoaDon", dt);
                rpvInHoaDon.LocalReport.DataSources.Clear();  // Xóa DataSource cũ (nếu có)
                rpvInHoaDon.LocalReport.DataSources.Add(rds);  // Thêm DataSource mới
                this.rpvInHoaDon.RefreshReport();  // Làm mới báo cáo
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }    
        }
    }
}
