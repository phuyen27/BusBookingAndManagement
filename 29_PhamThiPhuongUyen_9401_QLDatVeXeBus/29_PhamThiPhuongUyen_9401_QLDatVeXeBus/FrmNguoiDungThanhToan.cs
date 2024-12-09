using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    public partial class FrmNguoiDungThanhToan : Form
    {
        public FrmNguoiDungThanhToan()
        {
            InitializeComponent();
        }

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string query = "SELECT MAX(MAHD) FROM HOA_DON WHERE MAHD LIKE 'HD%'";

            string maHD = thuVien.TaoMaMoi(query, "HD");
            MessageBox.Show("Mã hóa đơn: " + maHD);

            FrmNguoiDungVeXe frmVeXeOnl = new FrmNguoiDungVeXe();
            FrmNguoiDungVeXe.maHD = maHD;

            //Gán các dữ liệu từ form Thanh toán sang form vé xe
            frmVeXeOnl.lblMaHD.Text = maHD;
            frmVeXeOnl.lblSoTien.Text = lblSoTien.Text;
            frmVeXeOnl.lblThoiGian.Text = lblThoiGian.Text;
            frmVeXeOnl.lblQuangDuong.Text = lblQuangDuong.Text;
            frmVeXeOnl.lbMaXeBus.DataSource = lbMaXeBus.Items;

            //Thêm dữ liệu vào bảng hóa đơn và bảng chi tiết hóa đơn
            themDuLieuHDVaCTHD(maHD);

            //Chuyển qua giao diện vé xe
            frmVeXeOnl.Show();
            this.Close();
        }

        public void themDuLieuHDVaCTHD(string maHD)
        {
            //Chuyển đổi dữ liệu thời gian thanh toán về kiểu datetime
            string thoiGianText = lblThoiGian.Text;
            DateTime thoiGian = DateTime.ParseExact(thoiGianText, "yyyy-MM-dd HH:mm:ss.fff", null);
            string query = "SELECT MAX(MACTHD) FROM HOA_DON_CHITIET WHERE MACTHD LIKE 'CT%'";

            //
            InsertHoaDon(maHD, classKetNoi.maNguoiDung, decimal.Parse(lblSoTien.Text), thoiGian, float.Parse(lblQuangDuong.Text));

            Random random = new Random(); // Khởi tạo Random chỉ một lần
            for (int i = 0; i < lbMaXeBus.Items.Count; i++)
            {
                string maHDCT = thuVien.TaoMaMoi(query, "CT"); // Tạo mã chi tiết hóa đơn ngẫu nhiên
                string maXe = lbMaXeBus.Items[i].ToString();
                string maNV = GetMaNVFromXeBus(maXe);
                InsertHoaDonChiTiet(maHDCT, maHD, maXe, maNV); // Thêm chi tiết hóa đơn
            }
        }

        private string GetMaNVFromXeBus(string maXe)
        {
            string query = "SELECT MANV FROM XE_BUS WHERE MAXE = @maXe";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                cmd.Parameters.AddWithValue("@maXe", maXe);
                sqlConn.Open();

                // Sử dụng ExecuteScalar để lấy mã xe, trả về null nếu không tìm thấy
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        public void InsertHoaDon(string maHD, string maKH, decimal soTien, DateTime thoiGianThanhToan, float quangDuong)
        {
            string query = @"INSERT INTO HOA_DON (MAHD, MAKH, SOTIEN, THOIGIANTHANHTOAN, QUANGDUONG)
                     VALUES (@maHD, @maKH, @soTien, @thoiGianThanhToan, @quangDuong);";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maHD", maHD);
                cmd.Parameters.AddWithValue("@maKH", maKH);
                cmd.Parameters.AddWithValue("@soTien", soTien);
                cmd.Parameters.AddWithValue("@thoiGianThanhToan", thoiGianThanhToan);
                cmd.Parameters.AddWithValue("@quangDuong", quangDuong);

                sqlConn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void InsertHoaDonChiTiet(string maCTHD, string maHD, string maXe, string maNV)
        {
            string query = $@"INSERT INTO HOA_DON_CHITIET (MACTHD, MAHD, MAXE, MANV)
                      VALUES ('{maCTHD}', '{maHD}', '{maXe}', '{maNV}')";
            using (SqlConnection sqlConn = ketNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thoát? Tiến trình sẽ bị dừng lại", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
               this.Close();
            }
        }

        private void FrmNguoiDungThanhToan_Load(object sender, EventArgs e)
        {
            lblNameUser.Text = classKetNoi.userNameClass;
        }
    }
}
