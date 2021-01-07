using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiNhaHang_nhom1
{
    // 7.10pm
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
          
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuQLNCC_Click(object sender, EventArgs e)
        {
            frmDMNhaCC frmNCC = new frmDMNhaCC();
            frmNCC.ShowDialog();
        }

        private void mnuQLNV_Click(object sender, EventArgs e)
        {
            frmNhanVien frmNV = new frmNhanVien();
            frmNV.ShowDialog();
        }

        private void mnuQLKH_Click(object sender, EventArgs e)
        {
            frmKhachHang frmKH = new frmKhachHang();
            frmKH.ShowDialog();
        }

     
        private void mnuNguyenLieu_Click(object sender, EventArgs e)
        {
            frmDMNguyenLieu frmNguyenLieu = new frmDMNguyenLieu();
            frmNguyenLieu.ShowDialog();
        }

        private void mnuOrder_Click(object sender, EventArgs e)
        {
            frmGoiMonMain frmGM = new frmGoiMonMain();
            frmGM.ShowDialog();
        }

        private void mnuDanhMuc_Click(object sender, EventArgs e)
        {
            frmDanhMuc frmDM = new frmDanhMuc();
            frmDM.ShowDialog();
        }

        private void mnuThanhToanHoaDon_Click(object sender, EventArgs e)
        {
            frmThanhToanHoaDon frmTTHD = new frmThanhToanHoaDon();
            frmTTHD.ShowDialog();
        }

        private void mnuThongKe_Click(object sender, EventArgs e)
        {
            //frmBaoCaoThongKe frmBCTK = new frmBaoCaoThongKe();
            //frmBCTK.ShowDialog();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLiHoaDon frmQLHD = new frmQuanLiHoaDon();
            frmQLHD.ShowDialog();
        }

        private void ThongKeHoaDonmnu_Click(object sender, EventArgs e)
        {
            frmThongKeHoaDon frmTKHD = new frmThongKeHoaDon();
            frmTKHD.ShowDialog();
        }

        private void mnuTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem TK = new TimKiem();
            TK.ShowDialog();
        }
    }
}
