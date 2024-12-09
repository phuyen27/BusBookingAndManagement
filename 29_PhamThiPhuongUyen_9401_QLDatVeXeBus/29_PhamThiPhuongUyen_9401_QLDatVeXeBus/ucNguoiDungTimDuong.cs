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
    public partial class ucNguoiDungTimDuong : UserControl
    {
        public ucNguoiDungTimDuong()
        {
            InitializeComponent();
            LoadDiemDiDiemDen();
        }

        private void ucNguoiDungTimDuong_Load(object sender, EventArgs e)
        {

        }

        classKetNoi ketNoi = new classKetNoi();

        // Hàm để load dữ liệu DIACHITRAM vào cbDiemDi và cbDiemDen
        private void LoadDiemDiDiemDen()
        {
            string query = "SELECT DIACHITRAM FROM TRAM";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                sqlConn.Open();

                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string diaChiTram = reader["DIACHITRAM"].ToString();
                        cbDiemDi.Items.Add(diaChiTram);  // Thêm vào ComboBox cbDiemDi
                        cbDiemDen.Items.Add(diaChiTram); // Thêm vào ComboBox cbDiemDen
                    }

                    reader.Close();
                }
            }
        }


        private void timChuyenDi()
        {
            if (cbDiemDi.SelectedItem == null || cbDiemDen.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn cả điểm đi và điểm đến.");
                return;
            }

            string diemDi = cbDiemDi.SelectedItem.ToString();
            string diemDen = cbDiemDen.SelectedItem.ToString();

            string matramDi = GetMaTramFromDiaChi(diemDi);
            string matramDen = GetMaTramFromDiaChi(diemDen);

            if (matramDi == null || matramDen == null)
            {
                MessageBox.Show("Không tìm thấy trạm cho địa chỉ đã chọn.");
                return;
            }

            float quangDuong;

            if (rdbLuotDi.Checked) // Lượt đi
            {
                quangDuong = tinhTongQuangDuong(matramDi, matramDen);
            }
            else if (rdbLuotVe.Checked) // Lượt về
            {
                quangDuong = tinhTongQuangDuong(matramDen, matramDi);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hướng đi.");
                return;
            }

            if (quangDuong >= 0)
            {
                lblQuangDuong.Text = quangDuong.ToString();

                // Lấy danh sách trạm giữa 2 mã trạm
                List<string> tramList;
                List<string> tuyenXe;
                List<string> xeBusList;

                if (rdbLuotDi.Checked)
                {
                    tramList = GetTramBetween(matramDi, matramDen);
                    tuyenXe = GetTuyenXe(matramDi, matramDen);
                    xeBusList = GetXeFromTuyen(matramDi, matramDen);

                }
                else  // rdbLuotVe.Checked
                {
                    tramList = GetTramBetween(matramDen, matramDi);
                    tramList.Reverse();
                    tuyenXe = GetTuyenXe(matramDen, matramDi);
                    tuyenXe.Reverse();
                    xeBusList = GetXeFromTuyen(matramDen, matramDi);
                    xeBusList.Reverse();

                }

                lbTram.Items.Clear();
                lbTuyenXe.Items.Clear();
                lbTuyenXe.Items.AddRange(tuyenXe.ToArray());
                lbTram.Items.AddRange(tramList.ToArray());

                // Xóa các items cũ trong ComboBox
                cbXeTuyen1.Items.Clear();
                cbXeTuyen2.Items.Clear();
                cbXeTuyen3.Items.Clear();

                // Hiển thị mã xe dựa trên số lượng tuyến xe
                switch (tuyenXe.Count)
                {
                    case 1:
                        // Hiển thị 1 ComboBox
                        cbXeTuyen1.Visible = true;
                        cbXeTuyen2.Visible = false;
                        cbXeTuyen3.Visible = false;
                        cbXeTuyen4.Visible = false;

                        for (int i = 0; i < 2; i++)
                        {
                            cbXeTuyen1.Items.Add(xeBusList[i]);
                        }
                        break;
                    case 2:
                        // Hiển thị 2 ComboBox
                        cbXeTuyen1.Visible = true;
                        cbXeTuyen2.Visible = true;
                        cbXeTuyen3.Visible = false;
                        cbXeTuyen4.Visible = false;

                        for (int i = 0; i < 2; i++)
                        {
                            cbXeTuyen1.Items.Add(xeBusList[i]);
                        }
                        for (int i = 2; i < 4; i++)
                        {
                            cbXeTuyen2.Items.Add(xeBusList[i]);
                        }
                        break;

                    case 3:
                        // Hiển thị 3 ComboBox
                        cbXeTuyen1.Visible = true;
                        cbXeTuyen2.Visible = true;
                        cbXeTuyen3.Visible = true;
                        cbXeTuyen4.Visible = false;


                        for (int i = 0; i < 2; i++)
                        {
                            cbXeTuyen1.Items.Add(xeBusList[i]);
                        }

                        for (int i = 2; i < 4; i++)
                        {
                            cbXeTuyen2.Items.Add(xeBusList[i]);
                        }
                        for (int i = 4; i < 6; i++)
                        {
                            cbXeTuyen3.Items.Add(xeBusList[i]);
                        }
                        break;

                    case 4:
                        // Hiển thị 3 ComboBox
                        cbXeTuyen1.Visible = true;
                        cbXeTuyen2.Visible = true;
                        cbXeTuyen3.Visible = true;
                        cbXeTuyen4.Visible = true;

                        for (int i = 0; i < 2; i++)
                        {
                            cbXeTuyen1.Items.Add(xeBusList[i]);
                        }

                        for (int i = 2; i < 4; i++)
                        {
                            cbXeTuyen2.Items.Add(xeBusList[i]);
                        }
                        for (int i = 4; i < 6; i++)
                        {
                            cbXeTuyen3.Items.Add(xeBusList[i]);
                        }
                        for (int i = 6; i < 8; i++)
                        {
                            cbXeTuyen4.Items.Add(xeBusList[i]);
                        }
                        break;

                    default:
                        MessageBox.Show("Không có tuyến xe nào phù hợp.");
                        cbXeTuyen1.Visible = false;
                        cbXeTuyen2.Visible = false;
                        cbXeTuyen3.Visible = false;
                        cbXeTuyen4.Visible = false;
                        break;
                }

            }
            else
            {
                MessageBox.Show("Không tìm thấy đường đi giữa hai điểm.");
            }
        }

        private List<string> GetXeFromTuyen(string maTramDi, string maTramDen)
        {
            List<string> xeList = new List<string>();

            // Truy vấn lấy MAXE từ các bảng liên quan
            string query = @"
            SELECT DISTINCT XB.MAXE
            FROM TRAM T
            INNER JOIN QUANG_DUONG QD ON T.MATRAM = QD.DIEMDI
            INNER JOIN TUYEN_XE TX ON QD.MATUYEN = TX.MATUYEN
            INNER JOIN XE_BUS XB ON TX.MATUYEN = XB.MATUYEN
            WHERE T.MATRAM BETWEEN @matramDi AND @matramDen;
            ";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@matramDi", maTramDi); // Thay thế với mã trạm thực tế
                    cmd.Parameters.AddWithValue("@matramDen", maTramDen); // Thay thế với mã trạm thực tế

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string maxe = reader["MAXE"].ToString();
                        xeList.Add(maxe);
                    }
                    reader.Close();
                }
            }

            return xeList;
        }

        //lấy mã trạm từ địa chỉ trạm
        private string GetMaTramFromDiaChi(string diaChi)
        {
            string matram = null;
            string query = "SELECT MATRAM FROM TRAM WHERE DIACHITRAM = @diaChi";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                sqlConn.Open();

                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        matram = result.ToString();
                    }
                }
            }

            return matram;
        }

        private List<string> GetTuyenXe(string matramDi, string matramDen)
        {
            List<string> tuyenXe = new List<string>();

            string query = @"SELECT DISTINCT TX.MATUYEN 
                            FROM TRAM T 
                            INNER JOIN QUANG_DUONG QD ON T.MATRAM = QD.DIEMDI 
                            INNER JOIN TUYEN_XE TX ON QD.MATUYEN = TX.MATUYEN 
                            WHERE T.MATRAM BETWEEN @matramDi AND @matramDen
                            ";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@matramDi", matramDi);
                    cmd.Parameters.AddWithValue("@matramDen", matramDen);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string matuyen = reader["MATUYEN"].ToString();
                        tuyenXe.Add(matuyen);
                    }
                    reader.Close();
                }
            }

            return tuyenXe;
        }

        private float tinhTongQuangDuong(string matramDi, string matramDen)
        {
            float totalDistance = 0;

            // Sử dụng Queue để duyệt qua các trạm
            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();
            queue.Enqueue(matramDi);

            while (queue.Count > 0)
            {
                string currentTram = queue.Dequeue();
                if (currentTram == matramDen)
                {
                    return totalDistance; // Trả về tổng quãng đường nếu đã đến trạm đích
                }

                visited.Add(currentTram);

                string query = "SELECT DIEMDEN, QUANGDUONG FROM QUANG_DUONG WHERE DIEMDI = @currentTram";
                using (SqlConnection sqlConn = ketNoi.getConnect())
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@currentTram", currentTram);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string nextTram = reader["DIEMDEN"].ToString();
                            float distance = Convert.ToSingle(reader["QUANGDUONG"]);

                            if (!visited.Contains(nextTram))
                            {
                                totalDistance += distance;
                                queue.Enqueue(nextTram);
                            }
                        }
                        reader.Close();
                    }
                }
            }

            return -1; // Trả về -1 nếu không tìm thấy đường đến trạm đích
        }

        private List<string> GetTramBetween(string matramDi, string matramDen)
        {
            List<string> tramList = new List<string>();

            string query = "SELECT TENTRAM FROM TRAM WHERE MATRAM BETWEEN @matramDi AND @matramDen ORDER BY MATRAM";

            using (SqlConnection sqlConn = ketNoi.getConnect())
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@matramDi", matramDi);
                    cmd.Parameters.AddWithValue("@matramDen", matramDen);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tentram = reader["TENTRAM"].ToString();
                        tramList.Add(tentram);
                    }
                    reader.Close();
                }
            }

            return tramList;
        }

        private void timDuongcbSelectedIndexChange(object sender, EventArgs e)
        {
            if (cbDiemDi.SelectedItem != null
                && cbDiemDen.SelectedItem != null
                && (rdbLuotDi.Checked || rdbLuotVe.Checked)) timChuyenDi();

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {

            // Tạo mảng chứa các ComboBox
            ComboBox[] comboBoxes = { cbXeTuyen1, cbXeTuyen2, cbXeTuyen3, cbXeTuyen4 };

            // Kiểm tra từng ComboBox trong mảng
            for (int i = 0; i < lbTuyenXe.Items.Count; i++)
            {
                if (comboBoxes[i].SelectedIndex == -1)
                {
                    MessageBox.Show($"Vui lòng chọn mã xe bus!");
                    return;
                }
            }

            // Nếu tất cả các mục đã được chọn, thực hiện tiếp các công việc
            FrmNguoiDungThanhToan frmThanhToan = new FrmNguoiDungThanhToan();



            frmThanhToan.lblTenKH.Text = classKetNoi.userNameClass;
            frmThanhToan.lblQuangDuong.Text = lblQuangDuong.Text;
            frmThanhToan.lblThoiGian.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            frmThanhToan.lblSoTien.Text = (7 * lbTuyenXe.Items.Count).ToString() + "000";

            // Thêm các mã xe đã chọn vào ListBox trong form thanh toán
            for (int i = 0; i < lbTuyenXe.Items.Count; i++)
            {
                frmThanhToan.lbMaXeBus.Items.Add(comboBoxes[i].SelectedItem.ToString());
            }
            MessageBox.Show("Đang tạo mẫu thanh toán");

            // Hiển thị form thanh toán dưới dạng dialog
            frmThanhToan.Show();
        }


    }
}
