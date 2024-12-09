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
    public partial class FrmQuanLy : Form
    {
        public FrmQuanLy()
        {
            InitializeComponent();
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void gunaBtnQLKH_Click(object sender, EventArgs e)
        {
            ucQuanLyKhachHang ucKH = new ucQuanLyKhachHang();

            addUserControl(ucKH);
        }

        private void gunaBtnQLNV_Click(object sender, EventArgs e)
        {
            ucQuanLyNhanVien ucNV = new ucQuanLyNhanVien();

            addUserControl(ucNV);
        }

        private void gunaBtnQLX_TX_Click(object sender, EventArgs e)
        {
            ucQuanLyXe_TuyenXe ucTX = new ucQuanLyXe_TuyenXe();

            addUserControl(ucTX);
        }

        private void gunaBtnQLTram_Click(object sender, EventArgs e)
        {
            ucQuanLyTramXe ucTram = new ucQuanLyTramXe();

            addUserControl(ucTram);
        }

        private void gunaBtnQLPB_Click(object sender, EventArgs e)
        {
            ucQuanLyPhongBan ucPB = new ucQuanLyPhongBan();

            addUserControl(ucPB);
        }

        private void gunaBtnQLHD_Click(object sender, EventArgs e)
        {
            ucQuanLyHoaDon ucHD = new ucQuanLyHoaDon();

            addUserControl(ucHD);
        }

        private void gunaBtnQLDG_Click(object sender, EventArgs e)
        {
            ucQuanLyDanhGia ucDG = new ucQuanLyDanhGia();

            addUserControl(ucDG);
        }

        private void gunaBtnQLBC_Click(object sender, EventArgs e)
        {
            ucQuanLyBaoCao ucBC = new ucQuanLyBaoCao();

            addUserControl(ucBC);
        }

        private void pbHome_Click(object sender, EventArgs e)
        {
            ucQuanLyHome ucQuanLyHome = new ucQuanLyHome();

            addUserControl(ucQuanLyHome);
        }

        private void FrmQuanLy_Load(object sender, EventArgs e)
        {
            ucQuanLyHome ucQuanLyHome = new ucQuanLyHome();

            addUserControl(ucQuanLyHome);

        }
    }
}
