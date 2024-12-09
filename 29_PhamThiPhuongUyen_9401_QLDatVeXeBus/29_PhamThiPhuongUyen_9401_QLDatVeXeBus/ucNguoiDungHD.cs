using Microsoft.Reporting.Map.WebForms.BingMaps;
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
    public partial class ucNguoiDungHD : UserControl
    {
        public ucNguoiDungHD()
        {
            InitializeComponent();
            thuVien.DesignDataGridView(dgvHoaDon);
        }

        classThuVienHam thuVien = new classThuVienHam();

        private DataTable thongTinHoaDon()
        {
            string query = "SELECT MAHD, SOTIEN, THOIGIANTHANHTOAN, QUANGDUONG FROM HOA_DON " +
                           "WHERE MAKH = '" + classKetNoi.maNguoiDung + "'";
            DataTable data = thuVien.loadThongTin(query);
            return data;
        }

        private void chinhDoRong()
        {
            DataGridView chinhDoRong = dgvHoaDon;
            {
                chinhDoRong.Columns[0].Width = 97;
                chinhDoRong.Columns[1].Width = 104;
                chinhDoRong.Columns[2].Width = 190;
                chinhDoRong.Columns[3].Width = 180;

                chinhDoRong.Columns[0].HeaderText = "Mã HD";
                chinhDoRong.Columns[1].HeaderText = "Số tiền";
                chinhDoRong.Columns[2].HeaderText = "TG Thanh toán";
                chinhDoRong.Columns[3].HeaderText = "Quãng đường";
            }
        }

        private void loadHD()
        {
            bdSource.DataSource = thongTinHoaDon();
            dgvHoaDon.DataSource = bdSource;
            chinhDoRong();
        }

        private void ucNguoiDungHD_Load(object sender, EventArgs e)
        {
            loadHD();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            FrmNguoiDungInHoaDon frmIn = Application.OpenForms["FrmNguoiDungInHoaDon"] as FrmNguoiDungInHoaDon;

            if (frmIn == null)
            {
                frmIn = new FrmNguoiDungInHoaDon();
            }
            frmIn.Show();
        }
    }
}
