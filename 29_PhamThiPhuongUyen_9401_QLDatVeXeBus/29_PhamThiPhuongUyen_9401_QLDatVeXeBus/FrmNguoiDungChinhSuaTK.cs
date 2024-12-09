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
    public partial class FrmNguoiDungChinhSuaTK : Form
    {
        public FrmNguoiDungChinhSuaTK()
        {
            InitializeComponent();
        }

        classKetNoi ketNoi = new classKetNoi();

        private void changeColor(object sender, EventArgs e)
        {
            if (cbDoiMau.Checked)
            {
                this.BackColor = Color.Bisque;
                gbChinhSuaTK.BackColor = Color.SeaShell;
                btnLuu.BackColor = Color.Crimson;
                btnThoat.BackColor = Color.Crimson;
            }
            else
            {
                this.BackColor = Color.Honeydew;
                gbChinhSuaTK.BackColor = Color.DarkSeaGreen;
                btnLuu.BackColor = Color.DarkOliveGreen;
                btnThoat.BackColor = Color.DarkOliveGreen;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }

        }



        private void updateThongTinCaNhan(string maNguoiDung, string newHo, string newTen, DateTime newNgaySinh, string newDiaChi, string newSDT, string newGioiTinh)
        {
            string query = "";

            // Xác định bảng cần cập nhật dựa trên mã người dùng
            if (maNguoiDung.StartsWith("KH"))
            {
                query = "UPDATE KHACH_HANG SET HOKH = @hoKh,TENKH = @tenKh, NAMSINHKH = @ngaySinh, DIACHIKH = @diaChi, SDTKH = @sdt WHERE MAKH = @maKH";
            }
            else if (maNguoiDung.StartsWith("NV") || maNguoiDung.StartsWith("DH") || maNguoiDung.StartsWith("TX"))
            {
                query = "UPDATE NHAN_VIEN SET HONV = @hoNV,TENNV = @tenNV, NGAYSINHNV = @ngaySinh, DIACHINV = @diaChi, SDTNV = @sdt WHERE MANV = @maNV";
            }
            else
            {
                // Trường hợp mã không hợp lệ
                MessageBox.Show("Mã người dùng không hợp lệ.");
                return;
            }

            using (SqlConnection sqlConn = ketNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                // Thêm các tham số vào câu lệnh SQL
                if (maNguoiDung.StartsWith("KH"))
                {
                    cmd.Parameters.AddWithValue("@hoKh", newHo);
                    cmd.Parameters.AddWithValue("@tenKh", newTen);
                    cmd.Parameters.AddWithValue("@ngaySinh", newNgaySinh);
                    cmd.Parameters.AddWithValue("@diaChi", newDiaChi);
                    cmd.Parameters.AddWithValue("@sdt", newSDT);
                    cmd.Parameters.AddWithValue("@maKH", maNguoiDung);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@hoNV", newHo);
                    cmd.Parameters.AddWithValue("@tenNV", newTen);
                    cmd.Parameters.AddWithValue("@ngaySinh", newNgaySinh);
                    cmd.Parameters.AddWithValue("@diaChi", newDiaChi);
                    cmd.Parameters.AddWithValue("@sdt", newSDT);
                    cmd.Parameters.AddWithValue("@maNV", maNguoiDung);
                }

                sqlConn.Open();
                cmd.ExecuteNonQuery(); // Thực hiện cập nhật
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtHo.Text == "" || txtSDT.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }

            if (txtSDT.Text.Length != 10) MessageBox.Show("Số điện thoại không hợp lệ!");

            //kiểm tra mật khẩu (lớn hoặc bằng 8)
            if (txtMatKhau.Text.Length < 8) MessageBox.Show("Mật khẩu phải lớn hơn hoặc bằng 8!");

            if (!rbNam.Checked && !rbNu.Checked && !rbKhac.Checked) MessageBox.Show("Vui lòng chọn giới tính!");
            else
            {
                string gioiTinh;
                if (rbKhac.Checked) gioiTinh = "Khác";
                if (rbNam.Checked) gioiTinh = "Nam";
                else gioiTinh = "Nữ";

                updateThongTinCaNhan(classKetNoi.maNguoiDung, txtHo.Text, txtTen.Text, dtNgaySinh.Value, txtDiaChi.Text, txtSDT.Text, gioiTinh);

                //cập nhật lại thông tin khách hàng
                classKetNoi.userNameClass = txtHo.Text + " " + txtTen.Text;
                classKetNoi.sdtClass = txtSDT.Text;

                MessageBox.Show("Thay đổi thông tin thành công!");
            }
        }

        private void FrmNguoiDungChinhSuaTK_Load(object sender, EventArgs e)
        {
        }
    }
}
