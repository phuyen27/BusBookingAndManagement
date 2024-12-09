using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _29_PhamThiPhuongUyen_9401_QLDatVeXeBus
{
    public partial class ucQuanLyBaoCao : UserControl
    {
        public ucQuanLyBaoCao()
        {
            InitializeComponent();
        }

        private void pbBaoCaoHD_Click(object sender, EventArgs e)
        {
            CheckDanhGiaThang();
        }

        classKetNoi classKetNoi = new classKetNoi();

        private void CheckDanhGiaThang()
        {
            if(gunaCkThongKeTheoThang.Checked)
            {
                int thang = int.Parse(gunacbThang.SelectedItem.ToString());
                

                    FrmQuanLyInHoaDon inHD = new FrmQuanLyInHoaDon();
                    inHD.thang = gunacbThang.SelectedItem.ToString();
                    inHD.Show();
            }

            else
            {
                FrmQuanLyInHoaDon inHD = new FrmQuanLyInHoaDon();
                inHD.Show();
            }

        }

        private void pbBaoCaoKH_Click(object sender, EventArgs e)
        {
            FrmQuanLyInLuongNV inLuong = new FrmQuanLyInLuongNV();

            inLuong.Show();
        }

        private void pbBaoCaoDG_Click(object sender, EventArgs e)
        {
            if (gunaCkThongKeTheoThang.Checked)
            {
                int thang = int.Parse(gunacbThang.SelectedItem.ToString());


                FrmQuanLyDanhGiaKH inDG = new FrmQuanLyDanhGiaKH();
                inDG.thang = gunacbThang.SelectedItem.ToString();
                inDG.Show();
            }

            else
            {
                FrmQuanLyDanhGiaKH inDG = new FrmQuanLyDanhGiaKH();
                inDG.Show();
            }
        }
    }
}
