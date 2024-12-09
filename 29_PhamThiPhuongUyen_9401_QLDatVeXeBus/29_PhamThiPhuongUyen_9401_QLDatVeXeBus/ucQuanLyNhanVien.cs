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
    public partial class ucQuanLyNhanVien : UserControl
    {

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();

        public ucQuanLyNhanVien()
        {
            InitializeComponent();
            thuVien.DesignDataGridView2(dgvNhanVien);
        }

        private void ucQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadDataNV();

            string queryMP = "SELECT MAPHONG FROM PHONG_BAN";
            thuVien.loadMaCanTim(cbMaPhong, queryMP, "MAPHONG");


            cbSapXep.Items.Add("Sắp xếp theo mã nhân viên");
            cbSapXep.Items.Add("Sắp xếp theo mã phòng ban");
            cbSapXep.Items.Add("Sắp xếp theo họ nhân viên");
            cbSapXep.Items.Add("Sắp xếp theo tên nhân viên");
            cbSapXep.Items.Add("Sắp xếp theo số tuổi nhân viên");
            cbSapXep.Items.Add("Sắp xếp theo thời gian vào làm");
        }

        string queryNV = "select MANV ,MAPHONG , HONV , TENNV , NGAYSINHNV " +
               ",NGAYVAOLAM  ,CCCD , SDTNV , DIACHINV ," +
               " GIOITINHNV  from NHAN_VIEN";
        private DataTable thongTinNV()
        {
            return thuVien.loadThongTin(queryNV);
        }

        private void loadDataNV()
        {
            bdSourceNV.DataSource = thongTinNV();

            dgvNhanVien.DataSource = bdSourceNV;
            txtTong.Text = bdSourceNV.Count.ToString();
            chinhDoRong();
        }

        private void chinhDoRong()
        {
            DataGridView chinhDoRong = dgvNhanVien;
            {
                chinhDoRong.Columns[0].Width = 50;
                chinhDoRong.Columns[1].Width = 45;
                chinhDoRong.Columns[2].Width = 90;
                chinhDoRong.Columns[3].Width = 50;
                chinhDoRong.Columns[4].Width = 70;
                chinhDoRong.Columns[5].Width = 70;
                chinhDoRong.Columns[6].Width = 90;
                chinhDoRong.Columns[7].Width = 80;
                chinhDoRong.Columns[8].Width = 140;
                chinhDoRong.Columns[9].Width = 50;

                chinhDoRong.Columns[0].HeaderText = "Mã NV";
                chinhDoRong.Columns[1].HeaderText = "Mã phòng";
                chinhDoRong.Columns[2].HeaderText = "Họ NV";
                chinhDoRong.Columns[3].HeaderText = "Tên NV";
                chinhDoRong.Columns[4].HeaderText = "Ngày sinh";
                chinhDoRong.Columns[5].HeaderText = "Ngày vào làm";
                chinhDoRong.Columns[6].HeaderText = "CCCD";
                chinhDoRong.Columns[7].HeaderText = "SĐT";
                chinhDoRong.Columns[8].HeaderText = "Địa chỉ";
                chinhDoRong.Columns[9].HeaderText = "Giới tính";
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu hàng hiện tại là hàng dữ liệu (không phải là hàng tiêu đề)
            if (dgvNhanVien.CurrentRow != null && dgvNhanVien.CurrentRow.Index != -1 && dgvNhanVien.CurrentRow.Index < dgvNhanVien.Rows.Count - 1)
            {
                // Kiểm tra nếu ô được chọn không có giá trị null
                txtMaNV.Text = dgvNhanVien.CurrentRow.Cells[0].Value?.ToString() ?? "";
                cbMaPhong.Text = dgvNhanVien.CurrentRow.Cells[1].Value?.ToString() ?? "";
                txtHoNV.Text = dgvNhanVien.CurrentRow.Cells[2].Value?.ToString() ?? "";
                txtTenNV.Text = dgvNhanVien.CurrentRow.Cells[3].Value?.ToString() ?? "";

                if (dgvNhanVien.CurrentRow.Cells[4].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(dgvNhanVien.CurrentRow.Cells[4].Value);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }
                if (dgvNhanVien.CurrentRow.Cells[5].Value != DBNull.Value)
                {
                    dtpNgayVaoLam.Value = Convert.ToDateTime(dgvNhanVien.CurrentRow.Cells[5].Value);
                }
                else
                {
                    dtpNgayVaoLam.Value = DateTime.Now;
                }

                txtCCCD.Text = dgvNhanVien.CurrentRow.Cells[6].Value?.ToString() ?? "";
                txtSDT.Text = dgvNhanVien.CurrentRow.Cells[7].Value?.ToString() ?? "";
                txtDC.Text = dgvNhanVien.CurrentRow.Cells[8].Value?.ToString() ?? "";
                txtGioiTinh.Text = dgvNhanVien.CurrentRow.Cells[9].Value?.ToString() ?? "";
                txtHH.Text = (dgvNhanVien.CurrentRow.Index + 1).ToString();

            }
            else
            {
                // Nếu người dùng chọn hàng tiêu đề hoặc hàng không hợp lệ
                txtMaNV.Text = "";
                cbMaPhong.Text = "";
                txtHoNV.Text = "";
                txtTenNV.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                dtpNgayVaoLam.Value = DateTime.Now;
                txtCCCD.Text = "";
                txtDC.Text = "";
                txtSDT.Text = "";
                txtGioiTinh.Text = "";
            }
        }

        private void hamSuaVaThem(string query, bool isEdit)
        {
            if (txtSDT.Text.Length != 10) MessageBox.Show("Số điện thoại phải là 10 chữ số!");
            if (txtMaNV.Text == "" || txtHoNV.Text == "" || txtTenNV.Text == ""
                || txtSDT.Text == "" || txtGioiTinh.Text == "") MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            else
            {
                try
                {
                    // Câu lệnh SQL                   
                    ketNoi.ExcuteNonQuery(query); // Thực hiện truy vấn với chuỗi SQL

                    if (isEdit) MessageBox.Show("Sửa thông tin nhân viên thành công!");
                    else MessageBox.Show("Thêm thông tin nhân viên thành công!");

                    loadDataNV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void pbLuu_Click(object sender, EventArgs e)
        {

            DateTime ngaySinh = dtpNgaySinh.Value;
            DateTime ngayVaoLam = dtpNgayVaoLam.Value;
            Random random = new Random();

            int currentcell = dgvNhanVien.CurrentCell.RowIndex;
            int randomNumber = random.Next(100000, 1000000);
            //tạo mật khẩu ngẫu nhiên
            string matKhau = classThuVienHam.RemoveDiacritics(txtTenNV.Text) + randomNumber.ToString();

            // Chuyển đổi ngày sinh sang định dạng chuỗi SQL
            string ngaySinhStr = ngaySinh.ToString("yyyy-MM-dd"); // Định dạng này phù hợp với SQL Server
            string ngayVaoLamStr = ngayVaoLam.ToString("yyyy-MM-dd");

            string query = "INSERT INTO NHAN_VIEN (MANV, MAPHONG, HONV, TENNV, NGAYSINHNV, CCCD, SDTNV, DIACHINV, PASS_TK_NV, GIOITINHNV,NGAYVAOLAM) " +
             "VALUES ('" + txtMaNV.Text + "', '" + cbMaPhong.Text + "', N'" + txtHoNV.Text + "'," +
            " N'" + txtTenNV.Text + "', '" + ngaySinhStr + "', '" + txtCCCD.Text + "', '" + txtSDT.Text +
            "', N'" + txtDC.Text + "', '" + matKhau + "', N'" + txtGioiTinh.Text + "','" + ngayVaoLamStr + "')";

            hamSuaVaThem(query, false);
        }

        private void pbSua_Click(object sender, EventArgs e)
        {

            string ngaySinhStr = (dtpNgaySinh.Value).ToString("yyyy-MM-dd");
            string ngayVaoLamStr = (dtpNgayVaoLam.Value).ToString("yyyy-MM-dd");

            string query = "UPDATE NHAN_VIEN SET " +
                               "MAPHONG = N'" + cbMaPhong.Text + "', " +
                               "HONV = N'" + txtHoNV.Text + "', " +
                               "TENNV = N'" + txtTenNV.Text + "', " +
                               "NGAYSINHNV = '" + ngaySinhStr + "', " +
                               "SDTNV = '" + txtSDT.Text + "', " +
                               "DIACHINV = N'" + txtDC.Text + "', " +
                               "GIOITINHNV = N'" + txtGioiTinh.Text + "', " +
                               "NGAYVAOLAM = '" + ngayVaoLamStr + "'," +
                               "CCCD = '" + txtCCCD.Text + "'" +
                               "WHERE MANV = '" + txtMaNV.Text + "'";

            hamSuaVaThem(query, true);
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            cbMaPhong.Text = "";
            txtHoNV.Text = "";
            txtTenNV.Text = "";
            txtCCCD.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            dtpNgayVaoLam.Value = DateTime.Now;
            txtDC.Text = "";
            txtGioiTinh.Text = "";
            txtSDT.Text = "";
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maKH = txtMaNV.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("NHAN_VIEN", "MANV", maKH); // Gọi hàm dùng chung để xóa
                loadDataNV(); // Load lại dữ liệu sau khi xóa
            }

        }

        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = queryNV;

            // Kiểm tra loại tìm kiếm
            if (rdbLocTheoMa.Checked)
            {
                timKiem += " WHERE MANV LIKE @searchTerm"; // Đảm bảo "WHERE" chỉ xuất hiện một lần
            }
            else if (rdbLocTen.Checked)
            {
                timKiem += " WHERE HONV LIKE @searchTerm OR TENNV LIKE @searchTerm"; // Cũng kiểm tra "WHERE"
            }
            else
            {
                timKiem += " WHERE MAPHONG LIKE @searchTerm"; // Đảm bảo "WHERE" chỉ xuất hiện một lần
            }

            // Khởi tạo SqlDataAdapter và thiết lập tham số
            SqlDataAdapter da = new SqlDataAdapter(timKiem, ketNoi.getConnect());

            // Thêm tham số tìm kiếm với LIKE
            da.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + txtTimKiem.Text + "%");

            // Tạo DataTable và đổ dữ liệu vào
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Gán DataTable vào DataGridView
            dgvNhanVien.DataSource = dt;
        }

        private void pbLoad_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            loadDataNV();
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = queryNV;
            string orderBy = "";

            switch (cbSapXep.SelectedIndex)
            {
                case 0:
                    orderBy = " ORDER BY MANV"; // Sắp xếp theo mã nhân viên
                    break;
                case 1:
                    orderBy = " ORDER BY MAPHONG"; // Sắp xếp theo mã phòng ban
                    break;
                case 2:
                    orderBy = " ORDER BY HONV"; // Sắp xếp theo họ nhân viên
                    break;
                case 3:
                    orderBy = " ORDER BY TENNV"; // Sắp xếp theo tên nhân viên
                    break;
                case 4:
                    orderBy = " ORDER BY DATEDIFF(YEAR, NGAYSINHNV, GETDATE())"; // Sắp xếp theo số tuổi
                    break;
                case 5:
                    orderBy = " ORDER BY DATEDIFF(YEAR, NGAYVAOLAM, GETDATE())"; // Sắp xếp theo thời gian vào làm
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query += orderBy; // Ghép câu truy vấn với mệnh đề ORDER BY
            }

            if (rdbCaoDenThap.Checked)
            {
                query += " DESC"; // Sắp xếp từ cao đến thấp
            }
            else if (rdbThapDenCao.Checked)
            {
                query += " ASC"; // Sắp xếp từ thấp đến cao
            }

            // Gọi hàm để thực thi truy vấn và hiển thị dữ liệu lên giao diện
            SqlDataAdapter da = new SqlDataAdapter(query, ketNoi.getConnect());
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Cập nhật dữ liệu vào BindingSource
            bdSourceNV.DataSource = dt;
            dgvNhanVien.DataSource = bdSourceNV;
        }

        private void pbDau_Click(object sender, EventArgs e)
        {
            bdSourceNV.Position = 0;

            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
            pbTruoc.Enabled = false;
            pbDau.Enabled = false;
        }

        private void pbTruoc_Click(object sender, EventArgs e)
        {
            bdSourceNV.Position -= 1;
            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
        }

        private void pbKe_Click(object sender, EventArgs e)
        {
            bdSourceNV.Position += 1;

            pbTruoc.Enabled = true;
            pbDau.Enabled = true;

        }

        private void pbCuoi_Click(object sender, EventArgs e)
        {
            bdSourceNV.Position = bdSourceNV.Count - 1;
            pbKe.Enabled = false;
            pbCuoi.Enabled = false;
            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }
    }
}
