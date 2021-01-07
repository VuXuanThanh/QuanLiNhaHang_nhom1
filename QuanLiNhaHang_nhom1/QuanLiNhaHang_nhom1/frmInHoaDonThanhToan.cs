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
    public partial class frmInHoaDonThanhToan : Form
    {
        public string  maHD, tenKH, diaChi, ngayXuat,tenNV, tongTienBangChu, tongTienBangSo;
        public frmInHoaDonThanhToan(string _maHD, string _tenKH, string _tenNV, string _ngayXuat, string _diachi, string _tongTienBangSo,string _tongTienBangChu)
        {
            InitializeComponent();
            maHD = _maHD;
            tenKH = _tenKH;
            tenNV = _tenNV;
            ngayXuat = _ngayXuat;
            diaChi = _diachi;
            tongTienBangSo = _tongTienBangSo;
            tongTienBangChu = _tongTienBangChu;
        }

        private void frmInHoaDonThanhToan_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLiNhaHang_nhom1DataSet.sp_getInforOfFood' table. You can move, or remove it, as needed.
            this.sp_getInforOfFoodTableAdapter.Fill(this.QuanLiNhaHang_nhom1DataSet.sp_getInforOfFood,maHD);
            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
               new Microsoft.Reporting.WinForms.ReportParameter("maHD",maHD),
                new Microsoft.Reporting.WinForms.ReportParameter("tenKH",tenKH),
                new Microsoft.Reporting.WinForms.ReportParameter("tenNV",tenNV),
                new Microsoft.Reporting.WinForms.ReportParameter("ngayXuat",ngayXuat),
                new Microsoft.Reporting.WinForms.ReportParameter("diaChi",diaChi),
                new Microsoft.Reporting.WinForms.ReportParameter("tongTienBangSo",tongTienBangSo),
                new Microsoft.Reporting.WinForms.ReportParameter("tongTienBangChu",tongTienBangChu),


           };
            this.reportViewer1.LocalReport.SetParameters(param);
            this.reportViewer1.RefreshReport();
        }
    }
}
