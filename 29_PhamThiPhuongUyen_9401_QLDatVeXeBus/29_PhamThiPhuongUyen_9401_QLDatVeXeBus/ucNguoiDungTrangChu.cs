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
    public partial class ucNguoiDungTrangChu : UserControl
    {
        public ucNguoiDungTrangChu()
        {
            InitializeComponent();
        }

        private void ucNguoiDungTrangChu_Load(object sender, EventArgs e)
        {

        }

        private void tmrChayChu_Tick(object sender, EventArgs e)
        {
            lblChayChu.Text = lblChayChu.Text.Substring(1, int.Parse(lblChayChu.Text.Length.ToString()) - 1)
                + lblChayChu.Text.Substring(0, 1);
        }
    }
}
