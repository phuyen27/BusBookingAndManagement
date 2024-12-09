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
    public partial class ucNguoiDungTaiKhoan : UserControl
    {
        public ucNguoiDungTaiKhoan()
        {
            InitializeComponent();
        }

        classKetNoi KetNoi = new classKetNoi();


        private void loadThongTin()
        {
            lblTenKH.Text = classKetNoi.userNameClass;
            lblSDT.Text = classKetNoi.sdtClass;
            var thongTin = timThongTin(classKetNoi.maNguoiDung);
            lblNamSinh.Text = thongTin.namSinh;
            lblGioiTinh.Text = thongTin.gioiTinh;
            lblDiaChi.Text = thongTin.diaChi;

        }

        private (string namSinh, string gioiTinh, string diaChi) timThongTin(string maNguoiDung)
        {
            string query = "";
            string tableName = "";
            string gioiTinh = "";
            string diaChi = "";
            string namSinh = "";

            // Kiểm tra mã người dùng để xác định bảng cần truy vấn
            if (maNguoiDung.StartsWith("KH"))
            {
                tableName = "KHACH_HANG";
                query = $"SELECT NAMSINHKH, GIOITINHKH, DIACHIKH FROM {tableName} WHERE MAKH = @maNguoiDung";
            }
            else if (maNguoiDung.StartsWith("NV") || maNguoiDung.StartsWith("TX") || maNguoiDung.StartsWith("DH"))
            {
                tableName = "NHAN_VIEN";
                query = $"SELECT NGAYSINHNV, GIOITINHNV, DIACHINV FROM {tableName} WHERE MANV = @maNguoiDung";
            }
            else
            {
                // Trường hợp mã không hợp lệ
                return (null, null, null);
            }

            using (SqlConnection sqlConn = KetNoi.getConnect())
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                cmd.Parameters.AddWithValue("@maNguoiDung", maNguoiDung);
                sqlConn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Lấy dữ liệu từ reader
                        if (maNguoiDung.StartsWith("KH"))
                        {
                            namSinh = reader["NAMSINHKH"]?.ToString();
                            gioiTinh = reader["GIOITINHKH"]?.ToString();
                            diaChi = reader["DIACHIKH"]?.ToString();
                        }
                        else if (maNguoiDung.StartsWith("NV") || maNguoiDung.StartsWith("TX") || maNguoiDung.StartsWith("DH"))
                        {
                            namSinh = reader["NGAYSINHNV"]?.ToString();
                            gioiTinh = reader["GIOITINHNV"]?.ToString();
                            diaChi = reader["DIACHINV"]?.ToString();
                        }
                    }
                }
            }

            return (namSinh, gioiTinh, diaChi);
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            FrmNguoiDungChinhSuaTK frmChinhSuaTK = Application.OpenForms["FrmNguoiDungChinhSuaTK"] as FrmNguoiDungChinhSuaTK;

            if (frmChinhSuaTK == null)
            {
                frmChinhSuaTK = new FrmNguoiDungChinhSuaTK();
            }

            frmChinhSuaTK.Show();
        }

        private void ucNguoiDungTaiKhoan_Load(object sender, EventArgs e)
        {
            loadThongTin();
        }
    }
}
