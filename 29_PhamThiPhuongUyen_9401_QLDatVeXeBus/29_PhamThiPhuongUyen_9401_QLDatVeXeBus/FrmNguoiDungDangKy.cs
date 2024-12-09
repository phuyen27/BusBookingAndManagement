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
    public partial class FrmNguoiDungDangKy : Form
    {
        public FrmNguoiDungDangKy()
        {
            InitializeComponent();
        }

        private void FrmDangKy_Load(object sender, EventArgs e)
        {
            txtHo.Focus();
        }

        classKetNoi KetNoi = new classKetNoi();
        classThuVienHam thuVien = new classThuVienHam();
        private void changeColor(object sender, EventArgs e)
        {
            if (cbDoiMau.Checked)
            {
                this.BackColor = Color.Bisque;
                gbDangKy.BackColor = Color.SeaShell;
                btnDangKy.BackColor = Color.Crimson;
                btnThoat.BackColor = Color.Crimson;
            }
            else
            {
                this.BackColor = Color.Honeydew;
                gbDangKy.BackColor = Color.DarkSeaGreen;
                btnDangKy.BackColor = Color.DarkOliveGreen;
                btnThoat.BackColor = Color.DarkOliveGreen;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            //gọi hàm kiểm tra
            string validationMessage = classThuVienHam.ValidateUserInfo(txtHo.Text, txtTen.Text, txtSDT.Text, txtMatKhau.Text, rbNam.Checked, rbNu.Checked, rbKhac.Checked);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                MessageBox.Show(validationMessage);
                return; // Dừng lại nếu có lỗi
            }
            string query = "SELECT MAX(MAKH) FROM KHACH_HANG WHERE MAKH LIKE 'KH%'";
            string maKH = thuVien.TaoMaMoi(query, "KH");
            string gioiTinh;
            if (rbKhac.Checked) gioiTinh = "Khác";
            else if (rbNam.Checked) gioiTinh = "Nam";
            else gioiTinh = "Nữ";

            insertKhachHang(maKH, txtHo.Text, txtTen.Text, dtNgaySinh.Value, txtSDT.Text, txtDiaChi.Text, txtMatKhau.Text, gioiTinh);
            MessageBox.Show("Đăng ký tài khoản thành công!");

            // thêm thông tin người dùng vào class
            classKetNoi.userNameClass = txtHo.Text + " " + txtTen.Text;
            classKetNoi.sdtClass = txtSDT.Text;
            classKetNoi.maNguoiDung = maKH;

            this.Close();
        }

        private void insertKhachHang(string maKhachHang, string hoKh, string tenKh, DateTime ngaySinh, string sdt, string diaChi, string passTk, string gioiTinh)
        {
            string query = "INSERT INTO KHACH_HANG (MAKH, HOKH, TENKH, NAMSINHKH, SDTKH, DIACHIKH, PASS_TK_KH, GIOITINHKH) " +
                           "VALUES (@maKH, @hoKh, @tenKh, @ngaySinh, @sdt, @diaChi, @passTk, @gioiTinh)";

            using (SqlConnection sqlConn = KetNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                // Thêm các tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maKH", maKhachHang);
                cmd.Parameters.AddWithValue("@hoKh", hoKh);
                cmd.Parameters.AddWithValue("@tenKh", tenKh);
                cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@diaChi", diaChi);
                cmd.Parameters.AddWithValue("@passTk", passTk);
                cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);

                sqlConn.Open();
                cmd.ExecuteNonQuery(); // Thực hiện thêm khách hàng
            }
        }


        private void AskPermissionBeforeQuite(object sender, FormClosingEventArgs e)
        {
            DialogResult YesOrNO = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Đăng ký tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sender as Button != btnThoat && YesOrNO == DialogResult.No) e.Cancel = true;
            if (sender as Button == btnThoat && YesOrNO == DialogResult.Yes) Environment.Exit(0);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHo.Text) ||
                !string.IsNullOrEmpty(txtTen.Text) ||
                !string.IsNullOrEmpty(txtSDT.Text) ||
                !string.IsNullOrEmpty(txtMatKhau.Text) ||
                rbNam.Checked || rbNu.Checked || rbKhac.Checked)
                AskPermissionBeforeQuite(sender, e as FormClosingEventArgs);
            else this.Close();
        }


    }
}
