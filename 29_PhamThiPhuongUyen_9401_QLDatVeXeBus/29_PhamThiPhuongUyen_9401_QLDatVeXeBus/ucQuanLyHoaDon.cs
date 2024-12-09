using Microsoft.Reporting.Map.WebForms.BingMaps;
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
    public partial class ucQuanLyHoaDon : UserControl
    {
        public ucQuanLyHoaDon()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvHoaDon);
        }

        private void ucQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            loadHoaDon();

            cbSapXepHD.Items.Add("Theo mã HD");
            cbSapXepHD.Items.Add("Theo mã KH");
            cbSapXepHD.Items.Add("Theo thời gian thanh toán");
            cbSapXepHD.Items.Add("Theo quãng đường");
            cbSapXepHD.Items.Add("Theo số tiền");

            string queryKH = "SELECT MAKH FROM KHACH_HANG";
            thuVien.loadMaCanTim(cbMaKhach, queryKH, "MAKH");

            loadChinhDoRong();

        }

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();


        private DataTable thongTinHoaDon()
        {
            string query = "SELECT * FROM HOA_DON";
            DataTable data = thuVien.loadThongTin(query);
            return data;
        }


        private void loadHoaDon()
        {
            bdSource.DataSource = thongTinHoaDon();
            dgvHoaDon.DataSource = bdSource;
            txtTong.Text = bdSource.Count.ToString();
            txtTimKiem.Text = "";
            cbSapXepHD.Text = "";
            rdbCaoDenThap.Checked = false;
            rdbThapDenCao.Checked = false;
            rdbTheoMaHD.Checked = false;
            rdbTheoMaKH.Checked = false;
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvHoaDon.CurrentCell != null)
            {
                txtMaHD.Text = dgvHoaDon.CurrentRow.Cells[0].Value?.ToString() ?? "";
                cbMaKhach.Text = dgvHoaDon.CurrentRow.Cells[1].Value?.ToString() ?? "";
                txtSoTien.Text = dgvHoaDon.CurrentRow.Cells[2].Value?.ToString() ?? "";
                if (dgvHoaDon.CurrentRow.Cells[3].Value != DBNull.Value)
                {
                    dtpTGTT.Value = Convert.ToDateTime(dgvHoaDon.CurrentRow.Cells[3].Value);
                }
                else
                {
                    dtpTGTT.Value = DateTime.Now;
                }
                txtQuangDuong.Text = dgvHoaDon.CurrentRow.Cells[4].Value?.ToString() ?? "";
                txtHH.Text = (dgvHoaDon.CurrentRow.Index + 1).ToString();
            }
            else
            {
                txtMaHD.Text = "";
                cbMaKhach.Text = "";
                txtSoTien.Text = "";
                txtQuangDuong.Text = "";
                dtpTGTT.Value = DateTime.Now;
            }
        }

        private void loadChinhDoRong()
        {
            DataGridView chinhDoRong = dgvHoaDon;
            {
                chinhDoRong.Columns[0].Width = 90;
                chinhDoRong.Columns[1].Width = 90;
                chinhDoRong.Columns[2].Width = 97;
                chinhDoRong.Columns[3].Width = 180;
                chinhDoRong.Columns[4].Width = 130;

                chinhDoRong.Columns[0].HeaderText = "Mã HD";
                chinhDoRong.Columns[1].HeaderText = "Mã KH";
                chinhDoRong.Columns[2].HeaderText = "Số tiền";
                chinhDoRong.Columns[3].HeaderText = "TG Thanh toán";
                chinhDoRong.Columns[4].HeaderText = "Quãng đường";
            }
        }

        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = "SELECT * FROM HOA_DON" +
                " WHERE ";

            // Xây dựng câu truy vấn và xử lý tham số
            if (rdbTheoMaKH.Checked)
            {
                timKiem += "MAKH = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }
            else
            {
                timKiem += "MAHD = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }

            try
            {
                // Gán DataSource cho DataGridView
                dgvHoaDon.DataSource = thuVien.loadThongTin(timKiem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void SapXep()
        {
            if (cbSapXepHD.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp!");
                return;
            }
            string sapXep = cbSapXepHD.SelectedItem.ToString(); // Lấy giá trị được chọn trong ComboBox

            string column = ""; // Khởi tạo biến để lưu cột cần sắp xếp
            string kieuSapXep = rdbCaoDenThap.Checked ? " DESC" : " ASC";

            // Xác định cột cần sắp xếp dựa vào giá trị của ComboBox
            if (sapXep == "Theo mã HD") column = "MAHD";
            else if (sapXep == "Theo mã KH") column = "MAKH";
            else if (sapXep == "Theo thời gian thanh toán") column = "THOIGIANTHANHTOAN";
            else if (sapXep == "Theo quãng đường") column = "QUANGDUONG";
            else if (sapXep == "Theo số tiền") column = "SOTIEN";
            else
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp hợp lệ!");
                return;
            }

            // Câu lệnh SQL để sắp xếp dữ liệu
            string query = "SELECT * FROM HOA_DON" +
                " ORDER BY " + column + kieuSapXep;

            try
            {
                // Gán DataSource cho DataGridView
                dgvHoaDon.DataSource = thuVien.loadThongTin(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void pbSapXepHD_Click(object sender, EventArgs e)
        {
            SapXep();
        }

        private void pbLoadThongTin_Click(object sender, EventArgs e)
        {
            loadHoaDon();
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?Nếu xóa toàn bộ CTHD của HD cũng sẽ bị xóa", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maHD = txtMaHD.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("HOA_DON", "MAHD", maHD); // Gọi hàm dùng chung để xóa
            }
            loadHoaDon();
        }

        private void pbSua_Click(object sender, EventArgs e)
        {

            string query = "UPDATE HOA_DON SET " +
                           "MAKH = @MAKH, " +
                           "SOTIEN = @SOTIEN, " +
                           "THOIGIANTHANHTOAN = @THOIGIANTHANHTOAN, " +
                           "QUANGDUONG = @QUANGDUONG " +
                           "WHERE MAHD = @MAHD";
            thucHienThemHoacSua(query, true);

        }

        private void pbLuu_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO HOA_DON (MAKH, SOTIEN, THOIGIANTHANHTOAN, QUANGDUONG, MAHD) " +
                               "VALUES (@MAKH, @SOTIEN, @THOIGIANTHANHTOAN, @QUANGDUONG, @MAHD)";
            thucHienThemHoacSua(query, false);
        }

        private void thucHienThemHoacSua(string query, bool isEdit)
        {
            string ngayTTS = dtpTGTT.Value.ToString("yyyy-MM-dd"); // Đảm bảo sử dụng định dạng yyyy-MM-dd
            decimal soTien;

            // Kiểm tra giá trị tiền
            if (!decimal.TryParse(txtSoTien.Text, out soTien))
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return; // Dừng lại nếu giá trị không hợp lệ
            }

            decimal quangDuong;
            // Kiểm tra giá trị quãng đường
            if (!decimal.TryParse(txtQuangDuong.Text, out quangDuong))
            {
                MessageBox.Show("Quãng đường không hợp lệ.");
                return; // Dừng lại nếu giá trị không hợp lệ
            }

            // Làm tròn quãng đường với 2 chữ số sau dấu phẩy
            quangDuong = Math.Round(quangDuong, 2);

            // Câu truy vấn SQL (nếu là sửa thì là UPDATE, nếu là thêm thì là INSERT INTO)

            // Khởi tạo kết nối
            using (SqlConnection conn = ketNoi.getConnect()) // Giả định ketNoi.getConnect() trả về SqlConnection
            {
                conn.Open();

                // Khởi tạo SqlCommand với kết nối
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@MAKH", cbMaKhach.Text);
                    cmd.Parameters.AddWithValue("@SOTIEN", soTien);
                    cmd.Parameters.AddWithValue("@THOIGIANTHANHTOAN", ngayTTS);
                    cmd.Parameters.AddWithValue("@QUANGDUONG", quangDuong);
                    cmd.Parameters.AddWithValue("@MAHD", txtMaHD.Text);

                    try
                    {
                        // Thực thi câu lệnh
                        cmd.ExecuteNonQuery();

                        if (isEdit)
                            MessageBox.Show("Sửa thành công");
                        else
                            MessageBox.Show("Thêm thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                    }
                }

                // Đóng kết nối (tự động đóng khi ra khỏi khối using)
            }

            // Nạp lại dữ liệu vào DataGridView
            loadHoaDon();
        }


        private void pbThem_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = "";
            cbMaKhach.Text = "";
            txtQuangDuong.Text = "";
            txtSoTien.Text = "";
            dtpTGTT.Value = DateTime.Now;
        }


        private void pbDau_Click(object sender, EventArgs e)
        {
            bdSource.Position = 0;

            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
            pbTruoc.Enabled = false;
            pbDau.Enabled = false;
        }

        private void pbTruoc_Click(object sender, EventArgs e)
        {
            bdSource.Position -= 1;
            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
        }

        private void pbKe_Click(object sender, EventArgs e)
        {
            bdSource.Position += 1;

            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }

        private void pbCuoi_Click(object sender, EventArgs e)
        {
            bdSource.Position = bdSource.Count - 1;
            pbKe.Enabled = false;
            pbCuoi.Enabled = false;
            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }

        private void btnXemThongTinCT_Click(object sender, EventArgs e)
        {
            FrmQuanLyChiTietHD frCTHD = new FrmQuanLyChiTietHD();
            frCTHD.Show();
        }
    }
}
