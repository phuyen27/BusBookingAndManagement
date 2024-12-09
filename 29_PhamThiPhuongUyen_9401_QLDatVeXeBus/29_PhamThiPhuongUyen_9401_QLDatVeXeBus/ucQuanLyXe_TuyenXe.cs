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

    public partial class ucQuanLyXe_TuyenXe : UserControl
    {
        public ucQuanLyXe_TuyenXe()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvTuyenXe);
            thuVien.DesignDataGridView2(dgvXeBus);
        }

        classThuVienHam thuVien = new classThuVienHam();
        classKetNoi ketNoi = new classKetNoi();

        int tongSoTuyenXe = 0;
        int tongSoXe = 0;


        private void ucQuanLyXe_TuyenXe_Load(object sender, EventArgs e)
        {
            loadTuyenXe();

            cbSapXep.Items.Add("Theo mã tuyến");
            cbSapXep.Items.Add("Theo số lượng xe");
            cbSapXep.Items.Add("Theo thời gian hoạt động");
            cbSapXep.Items.Add("Theo thời gian kết thúc");

            string queryNV = "SELECT MANV FROM NHAN_VIEN WHERE MaPhong = 'TX'";
            thuVien.loadMaCanTim(cbMaNV, queryNV, "MANV");

        }

        string queryTuyenXe = "SELECT * FROM TUYEN_XE";

        private void chinhDoRong()
        {
            DataGridView chinhDoRong = dgvTuyenXe;
            {
                chinhDoRong.Columns[0].Width = 50;
                chinhDoRong.Columns[1].Width = 80;
                chinhDoRong.Columns[2].Width = 83;
                chinhDoRong.Columns[3].Width = 84;

                chinhDoRong.Columns[0].HeaderText = "Mã tuyến";
                chinhDoRong.Columns[1].HeaderText = "Số lượng xe";
                chinhDoRong.Columns[2].HeaderText = "TG hoạt động";
                chinhDoRong.Columns[3].HeaderText = "TG kết thúc";

            }


        }

        private DataTable thongTinTuyenXe()
        {
            DataTable data = thuVien.loadThongTin(queryTuyenXe);
            return data;
        }

        private void loadTuyenXe()
        {
            bdSource.DataSource = thongTinTuyenXe();

            dgvTuyenXe.DataSource = bdSource;
            tongSoTuyenXe = bdSource.Count;
            chinhDoRong();
        }

        //hàm tìm thông tin xe cùng tuyến với nhau
        private DataTable thongTinXeBus(string maTuyen)
        {
            string query = "SELECT * FROM XE_BUS WHERE MATUYEN = @maTuyen";
            SqlCommand command = new SqlCommand(query, ketNoi.getConnect());
            command.Parameters.AddWithValue("@maTuyen", maTuyen);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        private void dgvTuyenXe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTuyenXe.CurrentRow != null)
            {
                string maTuyen = dgvTuyenXe.CurrentRow.Cells["MATUYEN"].Value.ToString();

                // Gán lại nguồn dữ liệu cho bdSourceCT
                bdSourceXe.DataSource = thongTinXeBus(maTuyen);
                dgvXeBus.DataSource = bdSourceXe;

                // Đăng ký lại sự kiện PositionChanged mỗi khi thay đổi nguồn dữ liệu
                bdSourceXe.PositionChanged += (s, ev) => capNhatTrangThaiNutsXe();

                DataGridView chinhDoRong = dgvXeBus;
                {
                    chinhDoRong.Columns[0].Width = 50;
                    chinhDoRong.Columns[1].Width = 50;
                    chinhDoRong.Columns[2].Width = 60;
                    chinhDoRong.Columns[3].Width = 84;
                    chinhDoRong.Columns[3].Width = 56;

                    chinhDoRong.Columns[0].HeaderText = "Mã xe";
                    chinhDoRong.Columns[1].HeaderText = "Mã tuyến";
                    chinhDoRong.Columns[2].HeaderText = "Mã NV";
                    chinhDoRong.Columns[3].HeaderText = "Biển số xe";
                    chinhDoRong.Columns[4].HeaderText = "Số khách";
                }

                // Cập nhật trạng thái các nút điều hướng
                capNhatTrangThaiNutsTX();
                capNhatTrangThaiNutsXe();
            }

            if (dgvTuyenXe.CurrentRow != null)
            {
                string maTuyen = dgvTuyenXe.CurrentRow.Cells["MATUYEN"].Value.ToString();
                bdSourceXe.DataSource = thongTinXeBus(maTuyen);
                dgvXeBus.DataSource = bdSourceXe;
            }

            if (dgvTuyenXe.CurrentCell != null)
            {
                txtMaTuyen.Text = dgvTuyenXe.CurrentRow.Cells[0].Value?.ToString() ?? "";
                txtSoLuongXe.Text = dgvTuyenXe.CurrentRow.Cells[1].Value?.ToString() ?? "";
                // Kiểm tra và chuyển đổi giá trị của Cells[2]
                if (dgvTuyenXe.CurrentRow.Cells[2].Value != DBNull.Value)
                {
                    var value = dgvTuyenXe.CurrentRow.Cells[2].Value;
                    if (value is TimeSpan) // Nếu là TimeSpan, chuyển thành thời gian hiện tại + TimeSpan
                    {
                        dtpTGHD.Value = DateTime.Today.Add((TimeSpan)value);
                    }
                    else if (value is DateTime) // Nếu là DateTime, chuyển trực tiếp
                    {
                        dtpTGHD.Value = (DateTime)value;
                    }
                }
                else
                {
                    dtpTGHD.Value = DateTime.Now;
                }

                // Kiểm tra và chuyển đổi giá trị của Cells[3]
                if (dgvTuyenXe.CurrentRow.Cells[3].Value != DBNull.Value)
                {
                    var value = dgvTuyenXe.CurrentRow.Cells[3].Value;
                    if (value is TimeSpan) // Nếu là TimeSpan, chuyển thành thời gian hiện tại + TimeSpan
                    {
                        dtpTGKT.Value = DateTime.Today.Add((TimeSpan)value);
                    }
                    else if (value is DateTime) // Nếu là DateTime, chuyển trực tiếp
                    {
                        dtpTGKT.Value = (DateTime)value;
                    }
                }
                else
                {
                    dtpTGKT.Value = DateTime.Now;
                }

            }
            else
            {
                txtMaTuyen.Text = "";
                txtSoLuongXe.Text = "";
                dtpTGHD.Value = DateTime.Now;
                dtpTGKT.Value = DateTime.Now;
            }
        }

        private void dgvXeBus_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvXeBus.CurrentCell != null)
            {
                txtMaXe.Text = dgvXeBus.CurrentRow.Cells[0].Value?.ToString() ?? "";
                txtMaTuyenXe.Text = dgvXeBus.CurrentRow.Cells[1].Value?.ToString() ?? "";
                cbMaNV.Text = dgvXeBus.CurrentRow.Cells[2].Value?.ToString() ?? "";
                txtBienSoXe.Text = dgvXeBus.CurrentRow.Cells[3].Value?.ToString() ?? "";
                txtSoLuongKhach.Text = dgvXeBus.CurrentRow.Cells[4].Value?.ToString() ?? "";
            }
            else
            {
                txtMaXe.Text = "";
                txtMaTuyenXe.Text = "";
                cbMaNV.Text = "";
                txtBienSoXe.Text = "";
                txtSoLuongKhach.Text = "";
            }
        }

        private void capNhatTrangThaiNutsTX()
        {
            pbDau.Enabled = bdSource.Position > 0;
            pbTruoc.Enabled = bdSource.Position > 0;
            pbKe.Enabled = bdSource.Position < bdSource.Count - 1;
            pbCuoi.Enabled = bdSource.Position < bdSource.Count - 1;

            // Hiển thị vị trí hiện tại (vị trí + 1) lên lblHHHD
            lblHHHD.Text = "Tuyến số: " + (bdSource.Position + 1).ToString();

            // Hiển thị tổng số hàng lên lblTongHD
            lblTongHD.Text = "Tổng số tuyến: " + bdSource.Count.ToString();
        }

        private void pbDau_Click(object sender, EventArgs e)
        {
            bdSource.Position = 0; // Di chuyển đến bản ghi đầu tiên
            capNhatTrangThaiNutsTX();
        }

        private void pbTruoc_Click(object sender, EventArgs e)
        {
            if (bdSource.Position > 0)
            {
                bdSource.Position -= 1; // Di chuyển đến bản ghi trước
                capNhatTrangThaiNutsTX();
            }
        }

        private void pbKe_Click(object sender, EventArgs e)
        {
            if (bdSource.Position < bdSource.Count - 1)
            {
                bdSource.Position += 1; // Di chuyển đến bản ghi tiếp theo
                capNhatTrangThaiNutsTX();
            }
        }

        private void pbCuoi_Click(object sender, EventArgs e)
        {
            bdSource.Position = bdSource.Count - 1; // Di chuyển đến bản ghi cuối cùng
            capNhatTrangThaiNutsTX();
        }

        private void capNhatTrangThaiNutsXe()
        {
            pbDauCT.Enabled = bdSourceXe.Position > 0;
            pbTruocCT.Enabled = bdSourceXe.Position > 0;
            pbKeCT.Enabled = bdSourceXe.Position < bdSourceXe.Count - 1;
            pbCuoiCT.Enabled = bdSourceXe.Position < bdSourceXe.Count - 1;

            lblHH.Text = "Xe số: " + (bdSourceXe.Position + 1).ToString();

            lblTong.Text = "Tổng số xe: " + bdSourceXe.Count.ToString();
        }


        private void pbDauCT_Click(object sender, EventArgs e)
        {
            bdSourceXe.Position = 0;
            capNhatTrangThaiNutsXe();
        }

        private void pbTruocCT_Click(object sender, EventArgs e)
        {
            if (bdSourceXe.Position > 0)
            {
                bdSourceXe.Position -= 1;
                capNhatTrangThaiNutsXe();
            }
        }

        private void pbKeCT_Click(object sender, EventArgs e)
        {
            if (bdSourceXe.Position < bdSourceXe.Count - 1)
            {
                bdSourceXe.Position += 1;
                capNhatTrangThaiNutsXe();
            }
        }

        private void pbCuoiCT_Click(object sender, EventArgs e)
        {
            bdSourceXe.Position = bdSourceXe.Count - 1;
            capNhatTrangThaiNutsXe();
        }

        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = queryTuyenXe +
                " WHERE ";

            // Xây dựng câu truy vấn và xử lý tham số
            if (rdbTheoSLX.Checked)
            {
                timKiem += "SOLUONGXE = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }
            else
            {
                timKiem += "MATUYEN = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }

            try
            {
                // Gán DataSource cho DataGridView
                dgvTuyenXe.DataSource = thuVien.loadThongTin(timKiem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void pbSapXep_Click(object sender, EventArgs e)
        {
            SapXep();
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
            if (sapXep == "Theo mã tuyến") column = "MATUYEN";
            else if (sapXep == "Theo số lượng xe") column = "SOLUONGXE";
            else if (sapXep == "Theo thời gian hoạt động") column = "THOIGIANHOATDONG";
            else if (sapXep == "Theo thời gian kết thúc") column = "THOIGIANKETHUC";
            else
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp hợp lệ!");
                return;
            }

            // Câu lệnh SQL để sắp xếp dữ liệu
            string query = queryTuyenXe + " ORDER BY " + column + kieuSapXep;

            try
            {
                // Gán DataSource cho DataGridView
                dgvTuyenXe.DataSource = thuVien.loadThongTin(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void pbThemGiaTri_Click(object sender, EventArgs e)
        {
            txtMaTuyen.Text = "";
            txtBienSoXe.Text = "";
            dtpTGHD.Value = DateTime.Now;
            dtpTGKT.Value = DateTime.Now;
            txtMaXe.Text = "";
            txtMaTuyenXe.Text = dgvTuyenXe.CurrentRow.Cells[0].Value.ToString();
            txtSoLuongKhach.Text = "";
            txtSoLuongXe.Text = "";
            cbMaNV.Text = "";
        }

        private void pbLoadThongTin_Click(object sender, EventArgs e)
        {
            loadTuyenXe();
        }


        private void hamSuaVaThem(string query, bool isEdit)
        {
            try
            {
                ketNoi.ExcuteNonQuery(query);

                if (!isEdit) MessageBox.Show("Thêm thông tin thành công!");
                else MessageBox.Show("Sửa thông tin thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            loadTuyenXe();
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO TUYEN_XE VALUES (" +
                   "'" + txtMaTuyen.Text + "', " +
                   "'" + int.Parse(txtSoLuongXe.Text) + "', " +
                   "'" + dtpTGHD.Value.ToString("HH:mm:ss") + "', " +  // Chuyển DateTime thành Time
                   "'" + dtpTGKT.Value.ToString("HH:mm:ss") + "')";

            hamSuaVaThem(query, false);
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            string query = "UPDATE TUYEN_XE SET " +
                   "SOLUONGXE = '" + int.Parse(txtSoLuongXe.Text) + "', " +
                   "THOIGIANHOATDONG='" + dtpTGHD.Value.ToString("HH:mm:ss") + "', " +  // Chuyển DateTime thành Time
                   "THOIGIANKETHUC='" + dtpTGKT.Value.ToString("HH:mm:ss") + "'WHERE MATUYEN ='" + txtMaTuyen.Text + "'";
            hamSuaVaThem(query, true);
        }

        private void pbXoaCT_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?\n" +
                "", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maXe = txtMaXe.Text.Trim();
                classThuVienHam.XoaDuLieu("XE_BUS", "MAXE", maXe);
                loadTuyenXe();
            }
        }

        private void pbSuaCT_Click(object sender, EventArgs e)
        {
            string query = "UPDATE XE_BUS SET " +
                           "MATUYEN = '" + txtMaTuyenXe.Text + "', " +
                           "MANV='" + cbMaNV.Text + "', " +
                           "SOLUONGKHACH='" + txtSoLuongKhach.Text + "', " +
                           "BIENSOXE='" + txtBienSoXe.Text + "'WHERE MAXE ='" + txtMaXe.Text + "'";
            hamSuaVaThem(query, true);

        }

        private void pbThemCT_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO XE_BUS VALUES (" +
                  "'" + txtMaXe.Text + "', " +
                  "'" + txtMaTuyenXe.Text + "', " +
                  "'" + cbMaNV.Text + "', " +
                  "'" + txtBienSoXe.Text + "', " +
                  "'" + txtSoLuongKhach.Text + "')";

            hamSuaVaThem(query, false);
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa? nếu xóa tuyến, " +
                "toàn bộ xe thuộc tuyến cũng bị xóa", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maKH = txtMaTuyen.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("TUYEN_XE", "MATUYEN", maKH); // Gọi hàm dùng chung để xóa
                loadTuyenXe(); // Load lại dữ liệu sau khi xóa
            }
        }
    }
}
