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
    public partial class frmTimKiemNCC : Form
    {
        public string maNCC;
        public frmTimKiemNCC()
        {
            InitializeComponent();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCCTim.Text))
            {
                MessageBox.Show("Chưa nhập mã nhà cung cấp để tìm");
                return;
            }
            maNCC = txtMaNCCTim.Text;
            this.Close();
        }
    }
}
