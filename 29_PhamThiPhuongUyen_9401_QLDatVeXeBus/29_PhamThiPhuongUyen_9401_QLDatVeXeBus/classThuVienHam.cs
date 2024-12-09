using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    internal class classThuVienHam
    {
        classKetNoi ketNoi = new classKetNoi();

        //hàm để xóa dữ liệu 
        public static void XoaDuLieu(string tableName, string columnName, string id)
        {
            classKetNoi ketNoiInstance = new classKetNoi(); // Tạo một đối tượng classKetNoi
            using (SqlConnection sqlConn = ketNoiInstance.getConnect())
            {
                try
                {
                    sqlConn.Open();
                    // Tạo câu lệnh xóa với tham số là bảng và cột cần xóa
                    string query = $"DELETE FROM {tableName} WHERE {columnName} = @id";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@id", id); // Tránh lỗi SQL Injection bằng cách sử dụng parameter
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu cần xóa.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message); // Hiển thị thông báo lỗi
                }
            }
        }

        public void checkForm()
        {
            var openForms = Application.OpenForms;

            // Kiểm tra xem chỉ còn form chính đang mở hay không
            if (openForms.Count == 1 && openForms[0] is FrmDangNhap)
            {
                FrmDangNhap mainForm = (FrmDangNhap)openForms[0];

                // Nếu form chính đang bị ẩn, show nó
                if (!mainForm.Visible)
                {
                    mainForm.Show();
                }
            }
        }

        //hàm đăng nhập
        public static SqlDataReader ExecuteLoginQuery(SqlConnection conn, string sql, string sdt, string mk)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@mk", mk);
                return cmd.ExecuteReader();
            }
        }



        //hàm kiểm tra đăng ký - chỉnh sửa tài khoản
        public static string ValidateUserInfo(string ho, string ten, string sdt, string matKhau, bool rbNam, bool rbNu, bool rbKhac)
        {
            if (string.IsNullOrEmpty(ho) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(matKhau))
            {
                return "Vui lòng nhập đầy đủ thông tin!";
            }

            if (sdt.Length != 10)
            {
                return "Số điện thoại không hợp lệ!";
            }

            if (matKhau.Length < 8)
            {
                return "Mật khẩu phải lớn hơn hoặc bằng 8!";
            }

            if (!rbNam && !rbNu && !rbKhac)
            {
                return "Vui lòng chọn giới tính!";
            }

            return ""; // Trả về chuỗi rỗng nếu tất cả hợp lệ
        }


        //hàm tìm mã lớn nhất trong bảng
        public string timMaLonNhat(string query)
        {
            string last = "";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                SqlCommand command = new SqlCommand(query, sqlConn);
                sqlConn.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    last = result.ToString();
                }
            }

            return last;
        }


        public string TaoMaMoi(string query, string user)
        {
            string maMax = timMaLonNhat(query);

            string soMa = maMax.Substring(2);
            int maTiepTheo = int.Parse(soMa) + 1;
            if (maTiepTheo <= 9)
            {
                return user + "00" + maTiepTheo.ToString();
            }
            else if (maTiepTheo <= 99)
            {
                return user + "0" + maTiepTheo.ToString();

            }
            else return user + maTiepTheo.ToString();
        }

        public DataTable loadThongTin(string query)
        {
            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(query, ketNoi.getConnect());

            adapter.Fill(data);
            return data;
        }

        public void DesignDataGridView(DataGridView dgv)
        {
            // Màu nền cho toàn bộ DataGridView
            dgv.BackgroundColor = Color.White;

            // Chỉnh màu cho các hàng xen kẽ nhau (Alternate row)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(204, 255, 204); // Xanh lá cây nhạt

            // Màu cho các hàng bình thường
            dgv.RowsDefaultCellStyle.BackColor = Color.White;

            // Màu chữ cho các hàng
            dgv.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Màu nền cho header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 153, 76); // Xanh lá cây đậm

            // Màu chữ cho header
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Chỉnh kiểu chữ cho header
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Chỉnh viền các ô
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.Gray;

            // Màu cho các ô đang chọn (Selection)
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(102, 255, 178); // Xanh lá cây sáng khi chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }


        public void DesignDataGridView2(DataGridView dgv)
        {
            // Màu nền cho toàn bộ DataGridView
            dgv.BackgroundColor = Color.White;

            // Chỉnh màu cho các hàng xen kẽ nhau (Alternate row)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 229); // Hồng pastel nhạt

            // Màu cho các hàng bình thường
            dgv.RowsDefaultCellStyle.BackColor = Color.White;

            // Màu chữ cho các hàng
            dgv.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Màu nền cho header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 153, 204); // Hồng pastel đậm

            // Màu chữ cho header
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Chỉnh kiểu chữ cho header
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Chỉnh viền các ô
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.Gray;

            // Màu cho các ô đang chọn (Selection)
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 182, 193); // Hồng pastel khi chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

       

        public void btnDangXuat_Click(object sender, EventArgs e, Form form)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FrmDangNhap frmdangNhap = Application.OpenForms["FrmDangNhap"] as FrmDangNhap;

                if (frmdangNhap == null)
                {
                    frmdangNhap = new FrmDangNhap();
                }

                frmdangNhap.Show();
                form.Close();
            }
        }

        //public void veTrangChu(object sender, EventArgs e, Form form)
        //{
        //    DialogResult result = MessageBox.Show("Bạn chắc chắn muốn về trang chủ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    if (result == DialogResult.Yes)
        //    {
        //        FrmTrangChu frmTrangChu = Application.OpenForms["FrmTrangChu"] as FrmTrangChu;

        //        if (frmTrangChu == null)
        //        {
        //            frmTrangChu = new FrmTrangChu();
        //        }

        //        frmTrangChu.Show();
        //        form.Close();
        //    }
        //}

        public void loadMaCanTim(ComboBox cb, string query, string ma)
        {
            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                try
                {
                    sqlConn.Open();

                    SqlCommand cmd = new SqlCommand(query, sqlConn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cb.Items.Clear();

                        while (reader.Read())
                        {
                            cb.Items.Add(reader[ma].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        //hàm tạo mật khẩu ngẫu nhiên cho người dùng
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
