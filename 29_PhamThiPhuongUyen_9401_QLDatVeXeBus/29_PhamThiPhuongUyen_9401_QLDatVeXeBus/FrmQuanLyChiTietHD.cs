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
    public partial class FrmQuanLyChiTietHD : Form
    {
        public FrmQuanLyChiTietHD()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvHoaDon);
        }

        classKetNoi ketNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();


        private void FrmQLHoaDonCT_Load(object sender, EventArgs e)
        {
            loadThongTinHoaDonCT();
            string queryNV = "SELECT MANV FROM NHAN_VIEN WHERE MaPhong = 'TX'";
            thuVien.loadMaCanTim(cbMaNV, queryNV, "MANV");

            string queryMX = "SELECT MAXE FROM XE_BUS";
            thuVien.loadMaCanTim(cbMaXe, queryMX, "MAXE");

            string queryKH = "SELECT MAKH FROM KHACH_HANG";
            thuVien.loadMaCanTim(cbMaKhach, queryKH, "MAKH");
        }

        private void ChinhDoRong()
        {
            DataGridView chinhDoRong = dgvHoaDon;
            {
                chinhDoRong.Columns[0].Width = 110;
                chinhDoRong.Columns[1].Width = 120;
                chinhDoRong.Columns[2].Width = 120;
                chinhDoRong.Columns[3].Width = 120;
                chinhDoRong.Columns[4].Width = 150;

                chinhDoRong.Columns[0].HeaderText = "Mã CTHD";
                chinhDoRong.Columns[1].HeaderText = "Mã HD";
                chinhDoRong.Columns[2].HeaderText = "Mã xe";
                chinhDoRong.Columns[3].HeaderText = "Mã nhân viên";
                chinhDoRong.Columns[4].HeaderText = "Thời gian tạo";

            }
        }

        private DataTable thongTinHoaDonCT(string maHD)
        {
            string query = "SELECT * FROM HOA_DON_CHITIET WHERE MAHD = @maHD";
            SqlCommand command = new SqlCommand(query, ketNoi.getConnect());
            command.Parameters.AddWithValue("@maHD", maHD);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        private void UpdateTextBoxes()
        {
            if (bdSource.Count > 0)
            {
                // Chuyển đổi Current thành DataRowView
                DataRowView currentRow = (DataRowView)bdSource.Current;
                txtMaHD.Text = currentRow["MAHD"].ToString();
                cbMaKhach.Text = currentRow["MAKH"].ToString();
                txtSoTien.Text = currentRow["SOTIEN"].ToString();
                txtQuangDuong.Text = currentRow["QUANGDUONG"].ToString();

                DataTable table = currentRow.DataView.Table;
                if (table.Columns.Contains("THOIGIANTHANHTOAN") && currentRow["THOIGIANTHANHTOAN"] != DBNull.Value)
                {
                    dtpTGTT.Value = Convert.ToDateTime(currentRow["THOIGIANTHANHTOAN"]);
                }

            }

        }

        private void dgvHoaDonCT_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvHoaDon.CurrentCell != null)
            {
                txtMaCT.Text = dgvHoaDon.CurrentRow.Cells[0].Value?.ToString() ?? "";
                txtMaHoaDon.Text = dgvHoaDon.CurrentRow.Cells[1].Value?.ToString() ?? "";
                cbMaXe.Text = dgvHoaDon.CurrentRow.Cells[2].Value?.ToString() ?? "";
                if (dgvHoaDon.CurrentRow.Cells[4].Value != DBNull.Value)
                {
                    dtpTGTao.Value = Convert.ToDateTime(dgvHoaDon.CurrentRow.Cells[4].Value);
                }
                else
                {
                    dtpTGTao.Value = DateTime.Now;
                }
                cbMaNV.Text = dgvHoaDon.CurrentRow.Cells[3].Value?.ToString() ?? "";


            }
            else
            {
                txtMaCT.Text = "";
                txtMaHoaDon.Text = txtMaHD.Text;
                cbMaXe.Text = "";
                cbMaNV.Text = "";
                dtpTGTao.Value = DateTime.Now;
            }
        }

        private void pbDau_Click(object sender, EventArgs e)
        {
            bdSourceCT.Position = 0;


            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
            pbTruoc.Enabled = false;
            pbDau.Enabled = false;
        }

        private void pbTruoc_Click(object sender, EventArgs e)
        {
            bdSourceCT.Position -= 1;

            pbKe.Enabled = true;
            pbCuoi.Enabled = true;
        }

        private void pbKe_Click(object sender, EventArgs e)
        {
            bdSourceCT.Position += 1;

            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }

        private void pbCuoi_Click(object sender, EventArgs e)
        {
            bdSourceCT.Position = bdSource.Count - 1;

            pbKe.Enabled = false;
            pbCuoi.Enabled = false;
            pbTruoc.Enabled = true;
            pbDau.Enabled = true;
        }

        private void loadThongTinHoaDonCT()
        {
            string query = "SELECT * FROM HOA_DON";
            bdSource.DataSource = thuVien.loadThongTin(query);

            if (bdSource.Count > 0)
            {
                UpdateTextBoxes();
            }

            bdSourceCT.DataSource = thongTinHoaDonCT(txtMaHD.Text);
            dgvHoaDon.DataSource = bdSourceCT;

            txtTong.Text = bdSource.Count.ToString();
            ChinhDoRong();
        }

        private void pbXoaCT_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maHD = txtMaCT.Text.Trim(); // Lấy giá trị từ TextBox
                classThuVienHam.XoaDuLieu("HOA_DON_CHITIET", "MACTHD", maHD); // Gọi hàm dùng chung để xóa
            }
            loadThongTinHoaDonCT();
        }

        private void themVaSuaCTHD(string query, bool isEdit)
        {
            try
            {

                ketNoi.ExcuteNonQuery(query);
                if (isEdit)
                    MessageBox.Show("Sửa thông tin thành công!");
                else MessageBox.Show("Thêm thông tin thành công!");
                loadThongTinHoaDonCT();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void pbThemCT_Click(object sender, EventArgs e)
        {
            string tgTao = dtpTGTao.Value.ToString("yyyy-MM-dd"); // Đảm bảo sử dụng định dạng yyyy-MM-dd
            txtMaHoaDon.Text = txtMaHD.Text;
            string query = "INSERT INTO HOA_DON_CHITIET (MACTHD,MAHD, MAXE, MANV, THOIGIANTAO) VALUES (" +
                "'" + txtMaCT.Text + "', " +
                "'" + txtMaHoaDon.Text + "', " + // MAHD
                "'" + cbMaXe.Text + "', " +     // MAXE
                "'" + cbMaNV.Text + "', " +     // MANV
                "'" + tgTao + "')";
            themVaSuaCTHD(query, false);

        }

        private void pbSuaCT_Click(object sender, EventArgs e)
        {
            string tgTao = dtpTGTao.Value.ToString("yyyy-MM-dd"); // Đảm bảo sử dụng định dạng yyyy-MM-dd
            txtMaHoaDon.Text = txtMaHD.Text;
            string query = "UPDATE HOA_DON_CHITIET SET " +
               "MAHD = '" + txtMaHoaDon.Text + "', " + // Cập nhật MAXE
               "MAXE = '" + cbMaXe.Text + "', " + // Cập nhật MAXE
               "MANV = '" + cbMaNV.Text + "', " + // Cập nhật MANV
               "THOIGIANTAO = '" + tgTao + "' " +  // Cập nhật THOIGIANTAO
               "WHERE MACTHD = '" + txtMaCT.Text + "'"; // Điều kiện MAHD
            themVaSuaCTHD(query, true);

        }

        private void btnDau_Click(object sender, EventArgs e)
        {
            bdSource.Position = 0;
            txtHH.Text = (bdSource.Position + 1).ToString();


            btnKe.Enabled = true;
            btnCuoi.Enabled = true;
            btnTruoc.Enabled = false;
            btnDau.Enabled = false;
            UpdateTextBoxes();
            bdSourceCT.DataSource = thongTinHoaDonCT(txtMaHD.Text);
            dgvHoaDon.DataSource = bdSourceCT;

        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            bdSource.Position -= 1;
            txtHH.Text = (bdSource.Position + 1).ToString();

            btnKe.Enabled = true;
            btnCuoi.Enabled = true;
            UpdateTextBoxes();
            bdSourceCT.DataSource = thongTinHoaDonCT(txtMaHD.Text);
            dgvHoaDon.DataSource = bdSourceCT;
        }

        private void btnKe_Click(object sender, EventArgs e)
        {
            bdSource.Position += 1;
            txtHH.Text = (bdSource.Position + 1).ToString();

            btnTruoc.Enabled = true;
            btnDau.Enabled = true;
            UpdateTextBoxes();
            bdSourceCT.DataSource = thongTinHoaDonCT(txtMaHD.Text);
            dgvHoaDon.DataSource = bdSourceCT;
        }

        private void btnCuoi_Click(object sender, EventArgs e)
        {
            bdSource.Position = bdSource.Count - 1;
            txtHH.Text = (bdSource.Position + 1).ToString();

            btnKe.Enabled = false;
            btnCuoi.Enabled = false;
            btnTruoc.Enabled = true;
            btnDau.Enabled = true;
            UpdateTextBoxes();
            bdSourceCT.DataSource = thongTinHoaDonCT(txtMaHD.Text);
            dgvHoaDon.DataSource = bdSourceCT;
        }

        private void pbLoadThongTin_Click(object sender, EventArgs e)
        {
            loadThongTinHoaDonCT();
        }

        private void pbThemHD_Click(object sender, EventArgs e)
        {
            txtMaCT.Text = "";
            txtMaHoaDon.Text = txtMaHoaDon.Text;
            cbMaKhach.Text = "";
            cbMaXe.Text = "";
            dtpTGTao.Value = DateTime.Now;
            txtMaCT.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thoát? Tiến trình sẽ bị dừng lại", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
