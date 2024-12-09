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
    public partial class ucQuanLyDanhGia : UserControl
    {
        public ucQuanLyDanhGia()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvDanhGia);
        }

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();

        public void loadThongTin()
        {
            bdSource.DataSource = thongTin();

            dgvDanhGia.DataSource = bdSource;
            loadChinhDoRong();

            txtTimKiem.Text = "";
            cbSapXep.Text = "";

            rdbCaoDenThap.Checked = false;
            rdbThapDenCao.Checked = false;
            rdbTimNV.Checked = false;
            rdbTimTheoMaKH.Checked = false;
        }

        public DataTable thongTin()
        {
            string query = "Select * FROM DANH_GIA";

            DataTable data = thuVien.loadThongTin(query);
            return data;
        }

        private void ucQuanLyDanhGia_Load(object sender, EventArgs e)
        {
            loadThongTin();

            cbSapXep.Items.Add("Sắp xếp theo mã khách hàng");
            cbSapXep.Items.Add("Sắp xếp theo mã nhân viên");
            cbSapXep.Items.Add("Sắp xếp theo ngày đánh giá");


            string queryKH = "SELECT MAKH FROM KHACH_HANG";
            thuVien.loadMaCanTim(cbMaKhach, queryKH, "MAKH");

            string queryNV = "SELECT MANV FROM NHAN_VIEN WHERE MAPHONG = 'TX'";
            thuVien.loadMaCanTim(cbMaNV, queryNV, "MANV");
        }

        private void SapXep()
        {
            if (cbSapXep.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp!");
                return;
            }

            string sapXep = cbSapXep.SelectedItem.ToString(); // Lấy giá trị được chọn trong ComboBox

            string column = ""; // Khởi tạo biến để lưu cột cần sắp xếp
            string kieuSapXep = rdbCaoDenThap.Checked ? " DESC" : " ASC";

            // Xác định cột cần sắp xếp dựa vào giá trị của ComboBox
            if (sapXep == "Sắp xếp theo mã khách hàng") column = "MAKH";
            else if (sapXep == "Sắp xếp theo mã nhân viên") column = "MANV";
            else if (sapXep == "Sắp xếp theo ngày đánh giá") column = "NGAY_DANH_GIA";

            else
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp hợp lệ!");
                return;
            }

            // Câu lệnh SQL để sắp xếp dữ liệu
            string query = "SELECT * FROM DANH_GIA ORDER BY " + column + kieuSapXep;

            try
            {
                // Gán DataSource cho DataGridView
                dgvDanhGia.DataSource = thuVien.loadThongTin(query);
                loadChinhDoRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void loadChinhDoRong()
        {
            DataGridView chinhDoRong = dgvDanhGia;
            {
                for (int i = 0; i < chinhDoRong.Rows.Count; i++)
                {
                    chinhDoRong.Rows[i].Height = 40;

                }

                chinhDoRong.Columns[2].Width = 170;
                chinhDoRong.Columns[0].Width = 100;
                chinhDoRong.Columns[1].Width = 100;
                chinhDoRong.Columns[3].Width = 250;

                chinhDoRong.Columns[0].HeaderText = "Mã khách hàng";
                chinhDoRong.Columns[1].HeaderText = "Mã nhân viên";
                chinhDoRong.Columns[2].HeaderText = "Ngày đánh giá";
                chinhDoRong.Columns[3].HeaderText = "Đánh giá";
            }
        }

        private void dgvTram_SelectionChange(object sender, EventArgs e)
        {
            if (dgvDanhGia.CurrentRow != null)
            {
                cbMaKhach.Text = dgvDanhGia.CurrentRow.Cells[0].Value?.ToString() ?? "";
                cbMaNV.Text = dgvDanhGia.CurrentRow.Cells[1].Value?.ToString() ?? "";
                if (dgvDanhGia.CurrentRow.Cells[2].Value != DBNull.Value)
                {
                    dtpNgayDanhGia.Value = Convert.ToDateTime(dgvDanhGia.CurrentRow.Cells[2].Value);
                }
                else
                {
                    dtpNgayDanhGia.Value = DateTime.Now;
                }
                txtDanhGia.Text = dgvDanhGia.CurrentRow.Cells[3].Value?.ToString() ?? "";
            }
            else
            {
                cbMaNV.Text = "";
                cbMaKhach.Text = "";
                dtpNgayDanhGia.Value = DateTime.Now;
                txtDanhGia.Text = "";
            }
        }

        private void pbDau_Click(object sender, EventArgs e)
        {
            bdSource.Position = 0; // Di chuyển đến bản ghi đầu tiên
            pbDau.Enabled = false; // Vô hiệu nút "Đầu"
            pbTruoc.Enabled = false; // Vô hiệu nút "Trước"
            pbKe.Enabled = true; // Kích hoạt nút "Kế"
            pbCuoi.Enabled = true; // Kích hoạt nút "Cuối"
        }

        private void pbTruoc_Click(object sender, EventArgs e)
        {
            if (bdSource.Position > 0)
            {
                bdSource.Position -= 1; // Di chuyển đến bản ghi trước
            }

            // Cập nhật trạng thái của các nút
            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
            pbDau.Enabled = bdSource.Position == 0; // Nếu ở đầu, vô hiệu nút "Đầu"
            pbTruoc.Enabled = bdSource.Position > 0; // Nếu không ở đầu, kích hoạt nút "Trước"
        }

        private void pbKe_Click(object sender, EventArgs e)
        {
            if (bdSource.Position < bdSource.Count - 1)
            {
                bdSource.Position += 1; // Di chuyển đến bản ghi tiếp theo
            }

            // Cập nhật trạng thái của các nút
            pbDau.Enabled = true;
            pbTruoc.Enabled = true;
            pbKe.Enabled = bdSource.Position < bdSource.Count - 1; // Nếu chưa ở cuối, kích hoạt nút "Kế"
            pbCuoi.Enabled = bdSource.Position < bdSource.Count - 1; // Nếu chưa ở cuối, kích hoạt nút "Cuối"
        }

        private void pbCuoi_Click(object sender, EventArgs e)
        {
            bdSource.Position = bdSource.Count - 1; // Di chuyển đến bản ghi cuối cùng
            pbCuoi.Enabled = false; // Vô hiệu nút "Cuối"
            pbKe.Enabled = false; // Vô hiệu nút "Kế"
            pbDau.Enabled = true; // Kích hoạt nút "Đầu"
            pbTruoc.Enabled = true; // Kích hoạt nút "Trước"
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string ngayDanhGia = (dtpNgayDanhGia.Value).ToString("yyyy-MM-dd");

                    // Sửa lại câu lệnh SQL: bỏ dấu phẩy thừa và sử dụng AND đúng cách
                    string query = "DELETE FROM DANH_GIA WHERE " +
                                   "MAKH = '" + cbMaKhach.Text + "' " +  // Bỏ dấu phẩy
                                   "AND MANV = '" + cbMaNV.Text + "' " +
                                   "AND NGAY_DANH_GIA = '" + ngayDanhGia + "'"; // Thêm điều kiện cho NGAY_DANH_GIA

                    ketNoi.ExcuteNonQuery(query);

                    MessageBox.Show("Xóa thông tin đánh giá thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                loadThongTin();
            }
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            // Sửa thông tin vào cơ sở dữ liệu
            string ngayDanhGia = (dtpNgayDanhGia.Value).ToString("yyyy-MM-dd");

            // Cập nhật lại câu lệnh WHERE để có đủ 3 khóa chính: MAKH, MANV, NGAY_DANH_GIA
            string query = $"UPDATE DANH_GIA SET  NHAN_XET = N'{txtDanhGia.Text}'" +
                           $" WHERE MAKH = '{cbMaKhach.SelectedItem.ToString()}'" +
                           $" AND MANV = '{cbMaNV.SelectedItem.ToString()}'" +
                           $" AND NGAY_DANH_GIA = '{ngayDanhGia}'";  // Sử dụng đầy đủ cả 3 khóa chính

            ketNoi.ExcuteQuery(query);

            MessageBox.Show("Sửa thành công");

            // Nạp lại dữ liệu vào DataGridView
            loadThongTin();
        }

        private void pbThemPB_Click(object sender, EventArgs e)
        {
            cbMaNV.Text = "";
            cbMaKhach.Text = "";
            dtpNgayDanhGia.Value = DateTime.Now;
            txtDanhGia.Text = "";
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            try
            {
                string ngayDanhGia = (dtpNgayDanhGia.Value).ToString("yyyy-MM-dd");


                string query = "INSERT INTO DANH_GIA  VALUES (" +
                               "'" + cbMaKhach.Text + "', " + // Thêm dấu phẩy ở đây
                               "'" + cbMaNV.Text + "', " + // Thêm dấu phẩy ở đây
                               "'" + ngayDanhGia + "', " +
                               "N'" + txtDanhGia.Text + "')";

                ketNoi.ExcuteNonQuery(query);

                MessageBox.Show("Thêm thông tin đánh giá thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            loadThongTin();
        }

        private void pbLoadThongTin_Click(object sender, EventArgs e)
        {
            loadThongTin();
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            SapXep();
        }

        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = "SELECT * from DANH_GIA WHERE ";

            // Xây dựng câu truy vấn và xử lý tham số
            if (rdbTimTheoMaKH.Checked)
            {
                timKiem += "MAKH = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }
            else
            {
                timKiem += "MANV = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }

            try
            {
                dgvDanhGia.DataSource = thuVien.loadThongTin(timKiem);
                loadChinhDoRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


    }
}
