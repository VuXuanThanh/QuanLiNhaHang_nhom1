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
    public partial class frmThongKeHoaDon : Form
    {
        public frmThongKeHoaDon()
        {
            InitializeComponent();
        }

        private void frmThongKeHoaDon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLiNhaHang_nhom1DataSet1.sp_ThongKeHoaDonTheoNgay' table. You can move, or remove it, as needed.
            this.sp_ThongKeHoaDonTheoNgayTableAdapter.Fill(this.QuanLiNhaHang_nhom1DataSet1.sp_ThongKeHoaDonTheoNgay,dtpNgayBatDau.Value.Date,dtpNgayKetThuc.Value.Date);

            this.reportViewer1.RefreshReport();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKeHoaDon_Load(sender,e);
        }
    }
}
