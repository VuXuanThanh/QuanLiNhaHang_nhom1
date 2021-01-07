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
    public partial class frmGoiMonMain : Form
    {
        public frmGoiMonMain()
        {
            InitializeComponent();
        }
        private void FillCombobox()
        {
            DataTable tbl = new DataTable();
            tbl = BLL.fillComboKH();
            cbxMaKH.DataSource = tbl;
            cbxMaKH.ValueMember = "MaKH";
            cbxMaKH.DisplayMember = "MaKH";
        }

        private void frmGoiMonMain_Load(object sender, EventArgs e)
        {
            FillCombobox();
            cbxMaKH.Text = "";
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (cbxMaKH.Text == "")
            {
                MessageBox.Show("Chưa chọn mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmOrderMonAn frmGm = new frmOrderMonAn();
            frmGm.strNhan = cbxMaKH.Text;
            frmGm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận thoát tác vụ này?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cbxMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxMaKH.Text!="")
                txtTenKH.Text = BLL.showTenTheoMaKH(cbxMaKH.Text);
        }
    }
}
