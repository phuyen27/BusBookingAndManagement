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
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void changeColorr(object sender, EventArgs e)
        {
            if (cbDoiMau.Checked)
            {
                this.BackColor = Color.Bisque;
                grDangNhap.BackColor = Color.SeaShell;
                btnDangKy.BackColor = Color.Crimson;
                btnDangNhap.BackColor = Color.Crimson;
                btnThoat.BackColor = Color.Crimson;
                pictureBox4.BackColor = Color.SeaShell;
                pictureBox2.BackColor = Color.SeaShell;
            }
            else
            {
                this.BackColor = Color.Honeydew;
                grDangNhap.BackColor = Color.DarkSeaGreen;
                btnDangKy.BackColor = Color.DarkOliveGreen;
                btnDangNhap.BackColor = Color.DarkOliveGreen;
                btnThoat.BackColor = Color.DarkOliveGreen;
                pictureBox4.BackColor = Color.DarkSeaGreen;
                pictureBox2.BackColor = Color.DarkSeaGreen;
            }
        }

        private void DangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSDT.Text) ||
                string.IsNullOrEmpty(label.Text))
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            //kiểm tra số điện thoại hợp lệ (bằng 10)
            if (txtSDT.Text.Length != 10) MessageBox.Show("Số điện thoại không hợp lệ!");

            //kiểm tra mật khẩu (lớn hoặc bằng 8)
            if (txtMatKhau.Text.Length < 8) MessageBox.Show("Mật khẩu phải lớn hơn hoặc bằng 8!");

            else
            {
                // Kết nối cơ sở dữ liệu thông qua classKetNoi
                using (SqlConnection sqlConn = new classKetNoi().getConnect())
                {
                    try
                    {
                        sqlConn.Open();
                        string sql;
                        string userName = ""; // Biến để lưu tên người dùng


                        if (rdbKhachHang.Checked)
                        {
                            sql = "SELECT * FROM KHACH_HANG WHERE SDTKH = '" + txtSDT.Text + "' AND PASS_TK_KH = '" + txtMatKhau.Text + "'";
                        }
                        else 
                        {
                            sql = "SELECT * FROM NHAN_VIEN WHERE MAPHONG = 'DH' AND SDTNV = '" + txtSDT.Text + "'" +
                                " AND PASS_TK_NV = '" + txtMatKhau.Text + "'";

                        }

                        using (var reader = classThuVienHam.ExecuteLoginQuery(sqlConn, sql, txtSDT.Text, txtSDT.Text))
                        {
                            if (reader.Read())
                            {
                                // Lấy tên người dùng từ cột tương ứng
                                userName = (rdbKhachHang.Checked ? reader["HOKH"].ToString() : reader["HONV"].ToString()) + " " + (rdbKhachHang.Checked ? reader["TENKH"].ToString() : reader["TENNV"].ToString());

                                classKetNoi.maNguoiDung = (rdbKhachHang.Checked ? reader["MAKH"].ToString() : reader["MANV"].ToString());
                                classKetNoi.sdtClass = txtSDT.Text;
                                classKetNoi.userNameClass = userName;
                                MessageBox.Show("Chúc mừng bạn đăng nhập thành công!");

                                if (rdbKhachHang.Checked)
                                {
                                    FrmNguoiDung frm = Application.OpenForms["FrmNguoiDung"] as FrmNguoiDung;

                                    if (frm == null) frm = new FrmNguoiDung();
                                    frm.Show();

                                }

                                else 
                                {
                                    FrmQuanLy frm = Application.OpenForms["FrmQuanLy"] as FrmQuanLy;

                                    if (frm == null) frm = new FrmQuanLy();
                                    frm.Show();

                                }
                            }

                            else MessageBox.Show("Đăng nhập thất bại!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                    finally
                    {
                        sqlConn.Close();
                    }
                }
            }
        }


        private void cbHienMatKhau_CheckedChange(object sender, EventArgs e)
        {
            if (cbHienMatKhau.Checked)
            {
                // Hiển thị mật khẩu
                txtMatKhau.PasswordChar = '\0'; // '\0' là ký tự rỗng, nghĩa là không có ký tự đặc biệt nào được dùng để che mật khẩu
            }
            else
            {
                // Ẩn mật khẩu
                txtMatKhau.PasswordChar = '*'; // Hoặc có thể dùng bất kỳ ký tự nào bạn muốn để che mật khẩu
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            FrmNguoiDungDangKy frmDK = Application.OpenForms["FrmNguoiDungDangKy"] as FrmNguoiDungDangKy;

            if (frmDK == null)
            {
                // Nếu chưa tồn tại, tạo mới
                frmDK = new FrmNguoiDungDangKy();
            }

            // Hiển thị form nếu chưa hiển thị

            frmDK.Show();
        }
    }
}
