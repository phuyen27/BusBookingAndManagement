using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    public partial class FrmNguoiDungVeXe : Form
    {
        classKetNoi ketNoi = new classKetNoi();
        public FrmNguoiDungVeXe()
        {
            InitializeComponent();
        }

        private static string _maHD;
        public static string maHD
        {
            get { return _maHD; }
            set { _maHD = value; }
        }

        classThuVienHam thuVien = new classThuVienHam();

        

        private void FrmVeXeOnl_Load(object sender, EventArgs e)
        {
            lblNameUser.Text = classKetNoi.userNameClass;
        }

        private void btnDanhGia_Click(object sender, EventArgs e)

        {
            FrmNguoiDungDanhGia frmDanhGia = Application.OpenForms["FrmNguoiDungDanhGia"] as FrmNguoiDungDanhGia;
            if (frmDanhGia == null)
            {
                // Nếu chưa tồn tại, tạo mới
                frmDanhGia = new FrmNguoiDungDanhGia();
            }

            // Hiển thị form nếu chưa hiển thị

            //gán mã xe bus khách hàng đi từ form Vé xe qua form đánh giá
            foreach (var item in lbMaXeBus.Items)
            {
                frmDanhGia.cbMaXeBus.Items.Add(item);
            }

            frmDanhGia.Show();
            this.Close();
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
