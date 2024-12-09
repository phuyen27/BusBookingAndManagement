using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    internal class classKetNoi
    {
        private string connectionString = "Data Source=UYENBABY2K4\\SQLEXPRESS;Initial Catalog=QLXB;Integrated Security=True";

        //Tạo các biến để lưu trữ tên, mật khẩu, số điện thoại người dùng
        private static string _userNameClass;
        private static string _passwordClass;
        private static string _sdtClass;
        private static string _maNguoiDung;

        //Lưu họ tên người dùng
        public static string userNameClass
        {
            get { return _userNameClass; }
            set { _userNameClass = value; }
        }

        //Lưu số điện thoại người dùng
        public static string sdtClass
        {
            get { return _sdtClass; }
            set { _sdtClass = value; }
        }

        //Lưu mã người dùng
        public static string maNguoiDung
        {
            get { return _maNguoiDung; }
            set { _maNguoiDung = value; }
        }

        public SqlConnection getConnect()
        {
            return new SqlConnection(connectionString);
        }


        public void ExcuteNonQuery(string commandText)
        {
            using (SqlConnection sqlConn = getConnect())
            {
                using (SqlCommand cmd = new SqlCommand(commandText, sqlConn))
                {
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable filldb(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = getConnect())
                {
                    // Sử dụng SqlDataAdapter để thực thi truy vấn và điền dữ liệu vào DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    conn.Open();  // Mở kết nối đến cơ sở dữ liệu
                    adapter.Fill(dt);  // Điền dữ liệu vào DataTable
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
            return dt;
        }

        public DataTable ExcuteQuery(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection ketnoi = new SqlConnection(connectionString))
            {
                ketnoi.Open();

                SqlCommand thucthi = new SqlCommand(query, ketnoi);

                SqlDataAdapter laydulieu = new SqlDataAdapter(thucthi);
                laydulieu.Fill(dt);
                ketnoi.Close();
            }
            return dt;
        }
    }
}
