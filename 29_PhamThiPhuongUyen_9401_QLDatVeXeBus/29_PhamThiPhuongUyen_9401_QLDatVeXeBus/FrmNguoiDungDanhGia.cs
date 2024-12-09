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
    public partial class FrmNguoiDungDanhGia : Form
    {
        public FrmNguoiDungDanhGia()
        {
            InitializeComponent();
        }

        classKetNoi KetNoi = new classKetNoi();

        private string GetMaNVFromXeBus(string maXe)
        {
            string query = "SELECT MANV FROM XE_BUS WHERE MAXE = @maXe";

            using (SqlConnection sqlConn = KetNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                cmd.Parameters.AddWithValue("@maXe", maXe);
                sqlConn.Open();

                // Sử dụng ExecuteScalar để lấy mã xe, trả về null nếu không tìm thấy
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {

            // Khởi tạo chuỗi nhanXet ban đầu là chuỗi rỗng
            string nhanXet = "";

            // Kiểm tra từng checkbox và thêm nội dung nếu được chọn
            if (checkBox1.Checked)
            {
                nhanXet += checkBox1.Text + ",";
            }
            if (checkBox2.Checked)
            {
                nhanXet += checkBox2.Text + ",";
            }
            if (checkBox3.Checked)
            {
                nhanXet += checkBox3.Text + ",";
            }
            if (checkBox4.Checked)
            {
                nhanXet += checkBox4.Text + ",";
            }

            // Xóa dấu phẩy cuối cùng nếu có
            if (!string.IsNullOrEmpty(nhanXet))
            {
                nhanXet = nhanXet.TrimEnd(',');
            }

            // Nếu txtDanhGia có giá trị, thêm vào chuỗi nhanXet
            if (!string.IsNullOrEmpty(txtDanhGia.Text))
            {
                if (!string.IsNullOrEmpty(nhanXet))
                {
                    nhanXet += ", " + txtDanhGia.Text;
                }
                else
                {
                    nhanXet = txtDanhGia.Text;
                }
            }

            // Lấy mã xe từ ComboBox
            string maXe = cbMaXeBus.SelectedItem.ToString();

            // Lấy mã nhân viên dựa trên mã xe
            string maNV = GetMaNVFromXeBus(maXe);

            // Lấy thời gian hiện tại
            DateTime ngayDanhGia = DateTime.Now;

            // Gọi hàm insertDanhGia để lưu vào cơ sở dữ liệu
            insertDanhGia(classKetNoi.maNguoiDung, maNV, ngayDanhGia, nhanXet);
            MessageBox.Show("Gửi đánh giá thành công!");

            if (cbMaXeBus.Items.Count == 1)
            {
                this.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có muốn tiếp tục đánh giá?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    cbMaXeBus.Items.Remove(cbMaXeBus.SelectedItem.ToString());

                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }

            }
        }


        public void insertDanhGia(string maKH, string maNV, DateTime ngayDanhGia, string nhanXet)
        {
            string query = @"INSERT INTO DANH_GIA (MAKH, MANV, NGAY_DANH_GIA, NHAN_XET)
                     VALUES (@maKH, @maNV, @ngayDanhGia, @nhanXet)";
            using (SqlConnection sqlConn = KetNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                cmd.Parameters.AddWithValue("@maKH", maKH);
                cmd.Parameters.AddWithValue("@maNV", maNV);
                cmd.Parameters.AddWithValue("@ngayDanhGia", ngayDanhGia);
                cmd.Parameters.AddWithValue("@nhanXet", nhanXet);

                sqlConn.Open();
                cmd.ExecuteNonQuery();
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

        private void FrmNguoiDungDanhGia_Load(object sender, EventArgs e)
        {
            lblNameUser.Text = classKetNoi.userNameClass;
        }
    }
}
