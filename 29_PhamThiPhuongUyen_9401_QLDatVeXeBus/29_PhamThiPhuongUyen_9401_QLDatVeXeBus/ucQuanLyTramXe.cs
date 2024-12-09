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
    public partial class ucQuanLyTramXe : UserControl
    {
        public ucQuanLyTramXe()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvTram);
        }

        classThuVienHam thuVien = new classThuVienHam();
        classKetNoi ketNoi = new classKetNoi();

        private void ucQuanLyTramXe_Load(object sender, EventArgs e)
        {
            loadThongTin();

            cbSapXep.Items.Add("Sắp xếp theo mã trạm");
            cbSapXep.Items.Add("Sắp xếp theo tên trạm");
        }

        public void loadThongTin()
        {
            bdSource.DataSource = thongTinPhong();

            dgvTram.DataSource = bdSource;
            txtTong.Text = bdSource.Count.ToString();
            loadChinhDoRong();
        }

        public DataTable thongTinPhong()
        {
            string query = "Select * FROM TRAM";

            DataTable data = thuVien.loadThongTin(query);
            return data;
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
            if (sapXep == "Sắp xếp theo mã trạm") column = "MATRAM";
            else if (sapXep == "Sắp xếp theo tên trạm") column = "TENTRAM";
            else
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp hợp lệ!");
                return;
            }

            // Câu lệnh SQL để sắp xếp dữ liệu
            string query = "SELECT * FROM TRAM ORDER BY " + column + kieuSapXep;

            try
            {
                // Gán DataSource cho DataGridView
                dgvTram.DataSource = thuVien.loadThongTin(query);
                loadChinhDoRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void loadChinhDoRong()
        {
            DataGridView chinhDoRong = dgvTram;
            {
                for (int i = 0; i < chinhDoRong.Rows.Count; i++)
                {
                    chinhDoRong.Rows[i].Height = 40;

                }

                chinhDoRong.Columns[2].Width = 250;
                chinhDoRong.Columns[0].Width = 120;
                chinhDoRong.Columns[1].Width = 250;

                chinhDoRong.Columns[2].HeaderText = "Địa chỉ trạm";
                chinhDoRong.Columns[0].HeaderText = "Mã trạm";
                chinhDoRong.Columns[1].HeaderText = "Tên trạm";
            }
        }

        private void dgvTram_SelectionChange(object sender, EventArgs e)
        {
            if (dgvTram.CurrentRow != null)
            {
                txtMaTram.Text = dgvTram.CurrentRow.Cells[0].Value?.ToString() ?? "";
                txtTenTram.Text = dgvTram.CurrentRow.Cells[1].Value?.ToString() ?? "";
                txtDChi.Text = dgvTram.CurrentRow.Cells[2].Value?.ToString() ?? "";
                txtHH.Text = (dgvTram.CurrentRow.Index + 1).ToString();
            }
            else
            {
                txtMaTram.Text = "";
                txtTenTram.Text = "";
                txtDChi.Text = "";
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
                string maPhong = txtMaTram.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("TRAM", "MATRAM", maPhong); // Gọi hàm dùng chung để xóa
            }
            loadThongTin();
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            // Sửa thông tin vào cơ sở dữ liệu
            string maTram = txtMaTram.Text;
            string tenTram = txtTenTram.Text;
            string DC = txtDChi.Text;

            string query = $"UPDATE TRAM SET TENTRAM = N'{tenTram}', DIACHITRAM = N'{DC}'" +
                $" WHERE MATRAM = '{maTram}'";
            ketNoi.ExcuteQuery(query);

            MessageBox.Show("Sửa thành công");
            // Nạp lại dữ liệu vào DataGridView
            loadThongTin();

        }

        private void pbThemPB_Click(object sender, EventArgs e)
        {
            txtMaTram.Text = "";
            txtTenTram.Text = "";
            txtDChi.Text = "";
            txtMaTram.Focus();
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            try
            {

                string query = "INSERT INTO TRAM  VALUES (" +
                               "'" + txtMaTram.Text + "', " + // Thêm dấu phẩy ở đây
                               "N'" + txtTenTram.Text + "', " + // Thêm dấu phẩy ở đây
                               "N'" + txtDChi.Text + "')";

                ketNoi.ExcuteNonQuery(query);

                MessageBox.Show("Thêm thông tin trạm thành công!");
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
            string timKiem = "SELECT * from TRAM WHERE ";

            // Xây dựng câu truy vấn và xử lý tham số
            if (rdbTimTheoMa.Checked)
            {
                timKiem += "MATRAM = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }
            else
            {
                timKiem += "TENTRAM = N'" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }

            try
            {
                dgvTram.DataSource = thuVien.loadThongTin(timKiem);
                loadChinhDoRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
