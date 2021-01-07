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
    public partial class frmTimKiemNV : Form
    {
       public string getStr;
        public frmTimKiemNV()
        {
            InitializeComponent();
        }

        private void frmTimKiemNV_Load(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNVTim.Text))
            {
                MessageBox.Show("Chưa nhập mã nhân viên tìm kiếm");
                return;
            }
            getStr = txtMaNVTim.Text;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
