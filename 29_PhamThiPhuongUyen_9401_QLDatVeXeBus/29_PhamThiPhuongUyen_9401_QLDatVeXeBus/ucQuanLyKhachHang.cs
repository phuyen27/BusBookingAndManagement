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
    public partial class ucQuanLyKhachHang : UserControl
    {
        public ucQuanLyKhachHang()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvKhachHang);
        }

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();

        int TongSoKhach = 0;
        int HienHuu = 0;

        string queryKH = "select MAKH, HOKH , TENKH, NAMSINHKH" +
                ",SDTKH , DIACHIKH , GIOITINHKH  from KHACH_HANG";

        private DataTable thongTinKhach()
        {
            return thuVien.loadThongTin(queryKH);
        }

        private void LoadDataKH()
        {
            bdSourceKH.DataSource = thongTinKhach();

            dgvKhachHang.DataSource = bdSourceKH;
            chinhDoRong();
            TongSoKhach = bdSourceKH.Count;
            lblTong.Text = "Tổng số khách: " + TongSoKhach.ToString();
        }


        private void ucQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            cbSapXep.Items.Add("Sắp xếp theo mã khách hàng");
            cbSapXep.Items.Add("Sắp xếp theo họ khách hàng");
            cbSapXep.Items.Add("Sắp xếp theo tên khách hàng");
            cbSapXep.Items.Add("Sắp xếp theo số tuổi của khách hàng");

            LoadDataKH();
        }

        private void chinhDoRong()
        {
            DataGridView chinhDoRong = dgvKhachHang;
            {
                chinhDoRong.Columns[0].Width = 100;
                chinhDoRong.Columns[1].Width = 90;
                chinhDoRong.Columns[2].Width = 100;
                chinhDoRong.Columns[3].Width = 100;
                chinhDoRong.Columns[4].Width = 100;
                chinhDoRong.Columns[5].Width = 145;
                chinhDoRong.Columns[6].Width = 80;

                chinhDoRong.Columns[0].HeaderText = "Mã khách";
                chinhDoRong.Columns[1].HeaderText = "Họ khách";
                chinhDoRong.Columns[2].HeaderText = "Tên khách";
                chinhDoRong.Columns[3].HeaderText = "Ngày sinh";
                chinhDoRong.Columns[4].HeaderText = "Số điện thoại";
                chinhDoRong.Columns[5].HeaderText = "Địa chỉ";
                chinhDoRong.Columns[6].HeaderText = "Giới tính";
            }
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentCell != null)
            {
                txtMaKH.Text = dgvKhachHang.CurrentRow.Cells[0].Value?.ToString() ?? "";
                txtHoKH.Text = dgvKhachHang.CurrentRow.Cells[1].Value?.ToString() ?? "";
                txtTenKH.Text = dgvKhachHang.CurrentRow.Cells[2].Value?.ToString() ?? "";

                if (dgvKhachHang.CurrentRow.Cells[3].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(dgvKhachHang.CurrentRow.Cells[3].Value);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }

                txtSDT.Text = dgvKhachHang.CurrentRow.Cells[4].Value?.ToString() ?? "";
                txtDC.Text = dgvKhachHang.CurrentRow.Cells[5].Value?.ToString() ?? "";
                txtGioiTinh.Text = dgvKhachHang.CurrentRow.Cells[6].Value?.ToString() ?? "";
                lblHH.Text = "Khách hàng số: " + (dgvKhachHang.CurrentRow.Index + 1).ToString();
            }
            else
            {
                txtMaKH.Text = "";
                txtHoKH.Text = "";
                txtTenKH.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                txtSDT.Text = "";
                txtDC.Text = "";
                txtGioiTinh.Text = "";
            }
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maKH = txtMaKH.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("KHACH_HANG", "MAKH", maKH); // Gọi hàm dùng chung để xóa
                LoadDataKH(); // Load lại dữ liệu sau khi xóa
            }

        }


        private void pbThem_Click(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length != 10) MessageBox.Show("Số điện thoại phải là 10 chữ số!");
            if (txtMaKH.Text == "" || txtHoKH.Text == "" || txtTenKH.Text == ""
                || txtSDT.Text == "" || txtGioiTinh.Text == "") MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            else
            {
                try
                {
                    int currentcell = dgvKhachHang.CurrentCell.RowIndex;


                    DateTime ngaySinh = dtpNgaySinh.Value;


                    Random random = new Random();
                    int randomNumber = random.Next(100000, 1000000);
                    string matKhau = classThuVienHam.RemoveDiacritics(txtTenKH.Text) + randomNumber.ToString();

                    string ngaySinhStr = ngaySinh.ToString("yyyy-MM-dd"); // Định dạng này phù hợp với SQL Server

                    string query = "INSERT INTO KHACH_HANG (MAKH, HOKH, TENKH, NAMSINHKH, SDTKH, DIACHIKH, PASS_TK_KH, GIOITINHKH) " +
                                   "VALUES ('" + txtMaKH.Text + "', N'" + txtHoKH.Text + "', N'" + txtTenKH.Text + "', " +
                                   "'" + ngaySinhStr + "', '" + txtSDT.Text + "'" +
                                   ", N'" + txtDC.Text + "', '" + matKhau + "', N'" + txtGioiTinh.Text + "')";

                    ketNoi.ExcuteNonQuery(query); // Thực hiện truy vấn với chuỗi SQL

                    MessageBox.Show("Thêm thông tin khách hàng thành công!");

                    LoadDataKH();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length != 10) MessageBox.Show("Số điện thoại phải là 10 chữ số!");
            if (txtMaKH.Text == "" || txtHoKH.Text == "" || txtTenKH.Text == ""
                || txtSDT.Text == "" || txtGioiTinh.Text == "") MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            try
            {
                int currentcell = dgvKhachHang.CurrentCell.RowIndex;

                DateTime ngaySinh = dtpNgaySinh.Value;
                string ngaySinhStr = ngaySinh.ToString("yyyy-MM-dd");
                string query = "UPDATE KHACH_HANG SET " +
                               "HOKH = N'" + txtHoKH.Text + "', " +
                               "TENKH = N'" + txtTenKH.Text + "', " +
                               "NAMSINHKH = '" + ngaySinhStr + "', " +
                               "SDTKH = '" + txtSDT.Text + "', " +
                               "DIACHIKH = N'" + txtDC.Text + "', " +
                               "GIOITINHKH = N'" + txtGioiTinh.Text + "' " +
                               "WHERE MAKH = '" + txtMaKH.Text + "'";

                ketNoi.ExcuteNonQuery(query);
                MessageBox.Show("Sửa thông tin khách hàng thành công!");
                LoadDataKH();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }


        private void pbLoad_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadDataKH();
        }

        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = queryKH;

            // Kiểm tra xem tìm kiếm theo mã hay họ tên
            if (rdbTimTheoMa.Checked)
            {
                // Tìm kiếm theo mã khách hàng
                timKiem += " WHERE MAKH = '" + txtTimKiem.Text + "'";
            }
            else
            {
                // Tìm kiếm theo họ và tên (có thể tìm theo một phần của họ hoặc tên)
                string searchTerm = "%" + txtTimKiem.Text + "%"; // Chuỗi tìm kiếm có dấu % để tìm kiếm chuỗi con
                timKiem += " WHERE HOKH LIKE @searchTerm OR TENKH LIKE @searchTerm";  // Tìm cả họ hoặc tên

                // Tạo đối tượng SqlDataAdapter để thêm tham số tìm kiếm
                SqlDataAdapter da = new SqlDataAdapter(timKiem, ketNoi.getConnect());
                da.SelectCommand.Parameters.AddWithValue("@searchTerm", searchTerm);  // Truyền tham số tìm kiếm với dấu %

                try
                {
                    // Gán DataSource cho DataGridView
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKhachHang.DataSource = dt;  // Hiển thị dữ liệu trong DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select MAKH, HOKH, TENKH, NAMSINHKH,SDTKH, " +
                "DIACHIKH, GIOITINHKH from KHACH_HANG"; // Câu lệnh SQL cơ bản

            // Xác định tiêu chí sắp xếp dựa trên lựa chọn trong ComboBox
            switch (cbSapXep.SelectedItem.ToString())
            {
                case "Sắp xếp theo mã khách hàng":
                    query += " ORDER BY MAKH";
                    break;
                case "Sắp xếp theo họ khách hàng":
                    query += " ORDER BY HOKH";
                    break;
                case "Sắp xếp theo tên khách hàng":
                    query += " ORDER BY TENKH";
                    break;
                case "Sắp xếp theo số tuổi của khách hàng":
                    query += " ORDER BY DATEDIFF(YEAR, NAMSINHKH, GETDATE())"; // Tính số tuổi dựa vào năm sinh
                    break;
                default:
                    break;
            }

            // Xác định hướng sắp xếp dựa vào RadioButton
            if (rdbCaoDenThap.Checked)
            {
                query += " DESC"; // Sắp xếp từ cao đến thấp
            }
            else if (rdbThapDenCao.Checked)
            {
                query += " ASC"; // Sắp xếp từ thấp đến cao
            }

            // Thực hiện truy vấn và cập nhật DataGridView
            SqlDataAdapter da = new SqlDataAdapter(query, ketNoi.getConnect());
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Cập nhật dữ liệu vào BindingSource
            bdSourceKH.DataSource = dt;
            dgvKhachHang.DataSource = bdSourceKH;
        }

        private void pbThemKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtHoKH.Text = "";
            txtTenKH.Text = "";
            txtSDT.Text = "";
            txtDC.Text = "";
            txtGioiTinh.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
        }

        private void pbDau_Click(object sender, EventArgs e)
        {
            bdSourceKH.Position = 0;

            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
            pbTruoc.Enabled = false;
            pbDau.Enabled = false;
        }

        private void pbTruoc_Click(object sender, EventArgs e)
        {
            bdSourceKH.Position -= 1;

            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
            pbTruoc.Enabled = false;
            pbDau.Enabled = false;
        }

        private void pbKe_Click(object sender, EventArgs e)
        {
            bdSourceKH.Position += 1;

            pbTruoc.Enabled = true;
            pbDau.Enabled = true;

        }

        private void pbCuoi_Click(object sender, EventArgs e)
        {
            bdSourceKH.Position = bdSourceKH.Count - 1;
            pbKe.Enabled = false;
            pbCuoi.Enabled = false;
            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }

    }
}
