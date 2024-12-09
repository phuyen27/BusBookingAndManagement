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
    public partial class FrmNguoiDung : Form
    {
        public FrmNguoiDung()
        {
            InitializeComponent();
        }

        private void addUserControl (UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void gunaBtnTimDuong_Click(object sender, EventArgs e)
        {
            ucNguoiDungTimDuong ucTimDuong = new ucNguoiDungTimDuong();

            addUserControl(ucTimDuong);
        }

        private void gunaBtnHD_Click(object sender, EventArgs e)
        {
            ucNguoiDungHD ucHD = new ucNguoiDungHD();

            addUserControl(ucHD);
        }

        private void gunaBtnTK_Click(object sender, EventArgs e)
        {
            ucNguoiDungTaiKhoan ucTK = new ucNguoiDungTaiKhoan();

            addUserControl(ucTK);
        }

        private void gunaBtnTrangChu_Click(object sender, EventArgs e)
        {
            ucNguoiDungTrangChu ucTrangChu  = new ucNguoiDungTrangChu();

            addUserControl(ucTrangChu);
        }

        private void FrmNguoiDung_Load(object sender, EventArgs e)
        {
            ucNguoiDungTrangChu ucTrangChu = new ucNguoiDungTrangChu();

            addUserControl(ucTrangChu);

            lblUserName.Text = classKetNoi.userNameClass;
        }
    }
}
