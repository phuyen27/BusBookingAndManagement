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
    public partial class ucQuanLyPhongBan : UserControl
    {
        public ucQuanLyPhongBan()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvPhongBan);

        }

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();

       
        public void loadThongTinPhong()
        {
            bdSource.DataSource = thongTinPhong();

            dgvPhongBan.DataSource = bdSource;

            loadChinhDoRong();
        }

        public DataTable thongTinPhong()
        {
            string query = "Select MaPhong as N'Mã phòng', TenPhong as N'Tên phòng'," +
                "SoLuongNV as N'Số lượng NV'" +
            "from PHONG_BAN";

            DataTable data = thuVien.loadThongTin(query);
            return data;
        }


        private void ucQuanLyPhongBan_Load(object sender, EventArgs e)
        {
            loadHinhAnh_KiemTra(dgvPhongBan.DataSource as DataTable);

            loadThongTinPhong();
            cbSapXep.Items.Add("Sắp xếp theo mã phòng");
            cbSapXep.Items.Add("Sắp xếp theo tên phòng");
            cbSapXep.Items.Add("Sắp xếp theo số lượng nhân viên");

            loadChinhDoRong();

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
            if (sapXep == "Sắp xếp theo mã phòng") column = "MaPhong";
            else if (sapXep == "Sắp xếp theo tên phòng") column = "TenPhong";
            else if (sapXep == "Sắp xếp theo số lượng nhân viên") column = "SoLuongNV";
            else
            {
                MessageBox.Show("Vui lòng chọn kiểu sắp xếp hợp lệ!");
                return;
            }

            // Câu lệnh SQL để sắp xếp dữ liệu
            string query = "SELECT MaPhong as N'Mã phòng', TenPhong as N'Tên phòng'," +
                " SoLuongNV as N'Số lượng NV' " +
                           "FROM PHONG_BAN ORDER BY " + column + kieuSapXep;

            try
            {
                // Gán DataSource cho DataGridView
                dgvPhongBan.DataSource = thuVien.loadThongTin(query);
                loadChinhDoRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void loadHinhAnh_KiemTra(DataTable dt)
        {

            // Thêm cột "Hình ảnh"
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.HeaderText = "Hình ảnh";
            imgCol.Name = "colHinhAnh"; // Đặt tên cột để tham chiếu trong sự kiện CellPainting
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom; // Hiển thị hình ảnh theo kiểu Zoom
            dgvPhongBan.Columns.Add(imgCol);

            // Thêm cột "Kiểm tra" (checkbox)
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.HeaderText = "Kiểm tra số lượng NV";
            btnCol.Name = "KiemTra";
            btnCol.Text = "Check";
            btnCol.UseColumnTextForButtonValue = true; // Hiển thị văn bản trên button

            // Thêm cột button vào DataGridView
            dgvPhongBan.Columns.Add(btnCol);
        }


        
        public int tinhSoNV(string maPhong)
        {
            int employeeCount = 0;

            // Câu lệnh SQL để tính số lượng nhân viên theo mã phòng
            string query = "SELECT COUNT(MANV) FROM NHAN_VIEN WHERE MAPHONG = @maPhong GROUP BY MAPHONG;";

            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = ketNoi.getConnect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.AddWithValue("@maPhong", maPhong);

                    try
                    {
                        connection.Open();
                        // Thực thi truy vấn và lấy số lượng nhân viên
                        employeeCount = (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                    }
                }
            }

            return employeeCount;
        }

        private void dgvPhongBan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvPhongBan.Columns["colHinhAnh"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                object tenPhongBanObj = dgvPhongBan.Rows[e.RowIndex].Cells[2].Value;
                string tenPhongBan = tenPhongBanObj != null ? tenPhongBanObj.ToString() : string.Empty;

                Image img = null;

                switch (tenPhongBan)
                {
                    case "CS":
                        img = Properties.Resources.chamSocKhachHang;
                        break;
                    case "DH":
                        img = Properties.Resources.dieuHanh;
                        break;
                    case "GD":
                        img = Properties.Resources.giamDoc;
                        break;
                    case "TC":
                        img = Properties.Resources.taiChinh;
                        break;
                    case "TX":
                        img = Properties.Resources.taiXe;
                        break;
                    default:
                        img = null; // Nếu không có hình tương ứng, để null hoặc bạn có thể sử dụng hình mặc định
                        break;
                }

                if (img != null)
                {
                    // Tính toán kích thước mới của hình ảnh theo kiểu zoom
                    int imgWidth = Math.Min(img.Width, e.CellBounds.Width);
                    int imgHeight = Math.Min(img.Height, e.CellBounds.Height);
                    Rectangle imgRect = new Rectangle(
                        e.CellBounds.X + (e.CellBounds.Width - imgWidth) / 2,  // Căn giữa theo chiều ngang
                        e.CellBounds.Y + (e.CellBounds.Height - imgHeight) / 2, // Căn giữa theo chiều dọc
                        imgWidth,
                        imgHeight);

                    // Vẽ hình ảnh theo kích thước đã tính toán
                    e.Graphics.DrawImage(img, imgRect);
                }

                e.Handled = true; // Ngăn DataGridView vẽ đè lên
            }

        }


     
        private void DgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem chỉ số hàng có hợp lệ và cột nhấn có phải là "KiemTra"
            if (e.RowIndex >= 0 && e.RowIndex < dgvPhongBan.Rows.Count &&
                e.ColumnIndex >= 0 && e.ColumnIndex < dgvPhongBan.Columns.Count &&
                dgvPhongBan.Columns[e.ColumnIndex].Name == "KiemTra")
            {
                // Lấy mã phòng từ cột 2
                object maPhongObj = dgvPhongBan.Rows[e.RowIndex].Cells[2].Value;
                string maPhong = maPhongObj != null ? maPhongObj.ToString() : string.Empty;

                // Lấy số lượng nhân viên từ cột 4
                object soLuongNVObj = dgvPhongBan.Rows[e.RowIndex].Cells[4].Value;
                int soLuongNvTrongPhong = 0;

                // Kiểm tra giá trị số lượng nhân viên và chuyển đổi
                if (soLuongNVObj != null && int.TryParse(soLuongNVObj.ToString(), out soLuongNvTrongPhong))
                {
                    // Tính số lượng nhân viên thực tế trong phòng từ cơ sở dữ liệu
                    int soLuongNvThucTe = tinhSoNV(maPhong);

                    // So sánh số lượng nhân viên
                    if (soLuongNvThucTe < soLuongNvTrongPhong)
                    {
                        MessageBox.Show("Phòng ban hiện tại còn thiếu nhân viên!");
                    }
                    else
                    {
                        MessageBox.Show("Phòng ban đã đủ nhân viên!");
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng nhân viên không hợp lệ!");
                }
            }

        }

        private void loadChinhDoRong()
        {
            DataGridView chinhDoRong = dgvPhongBan;
            {
                for (int i = 0; i < chinhDoRong.Rows.Count; i++)
                {
                    chinhDoRong.Rows[i].Height = 60;

                }

                chinhDoRong.Columns[2].Width = 100;
                chinhDoRong.Columns[3].Width = 200;
                chinhDoRong.Columns[4].Width = 100;
                chinhDoRong.Columns[0].Width = 72;
                chinhDoRong.Columns[1].Width = 150;
            }
        }

        private void dgvPhong_SelectionChange(object sender, EventArgs e)
        {
            if (dgvPhongBan.CurrentRow != null)
            {
                txtMaPhong.Text = dgvPhongBan.CurrentRow.Cells[2].Value?.ToString() ?? "";
                txtTenPhong.Text = dgvPhongBan.CurrentRow.Cells[3].Value?.ToString() ?? "";
                txtSoLuongNV.Text = dgvPhongBan.CurrentRow.Cells[4].Value?.ToString() ?? "";
            }
            else
            {
                txtMaPhong.Text = "";
                txtTenPhong.Text = "";
                txtSoLuongNV.Text = "";
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
            bdSource.Position = bdSource.Count - 1;
            pbKe.Enabled = false;
            pbCuoi.Enabled = false;
            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maPhong = txtMaPhong.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("PHONG_BAN", "MaPhong", maPhong); // Gọi hàm dùng chung để xóa
            }
            loadThongTinPhong();
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            // Sửa thông tin vào cơ sở dữ liệu
            string maPhong = txtMaPhong.Text;
            string tenPhong = txtTenPhong.Text;
            int soLuongNV = int.Parse(txtSoLuongNV.Text);

            string query = $"UPDATE PHONG_BAN SET TenPhong = N'{tenPhong}', SoLuongNV = {soLuongNV} WHERE MaPhong = '{maPhong}'";
            ketNoi.ExcuteQuery(query);

            MessageBox.Show("Sửa thành công");
            // Nạp lại dữ liệu vào DataGridView
            loadThongTinPhong();

            // Đảm bảo rằng hàng vừa được sửa sẽ được chọn và hiển thị trên TextBox
            foreach (DataGridViewRow row in dgvPhongBan.Rows)
            {
                if (row.Cells[0].Value?.ToString() == maPhong)
                {
                    dgvPhongBan.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        private void pbThemPB_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtSoLuongNV.Text = "";
            txtMaPhong.Focus();
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            try
            {

                string query = "INSERT INTO PHONG_BAN  VALUES (" +
                               "'" + txtMaPhong.Text + "', " + // Thêm dấu phẩy ở đây
                               "N'" + txtTenPhong.Text + "', " + // Thêm dấu phẩy ở đây
                               "'" + txtSoLuongNV.Text + "')";

                ketNoi.ExcuteNonQuery(query);

                MessageBox.Show("Thêm thông tin  thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            loadThongTinPhong();
        }

        private void pbLoadThongTin_Click(object sender, EventArgs e)
        {
            loadThongTinPhong();
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            SapXep();
        }

        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = "SELECT MaPhong as N'Mã phòng', TenPhong as N'Tên phòng', SoLuongNV as N'Số lượng NV' FROM PHONG_BAN WHERE ";

            // Xây dựng câu truy vấn và xử lý tham số
            if (rdbTimTheoMa.Checked)
            {
                // Nếu tìm theo mã phòng
                timKiem += "MaPhong = '" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }
            else
            {
                // Nếu tìm theo tên phòng
                timKiem += "TenPhong = N'" + txtTimKiem.Text + "'"; // Chèn giá trị vào chuỗi truy vấn
            }

            try
            {
                // Gán DataSource cho DataGridView
                dgvPhongBan.DataSource = thuVien.loadThongTin(timKiem);
                loadChinhDoRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

    }
}
