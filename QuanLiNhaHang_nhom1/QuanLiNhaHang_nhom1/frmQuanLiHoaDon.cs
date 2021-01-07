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
    public partial class frmQuanLiHoaDon : Form
    {
        public frmQuanLiHoaDon()
        {
            InitializeComponent();
        }
        void hienThi()
        {
            DataTable table = new DataTable();
            table = BLL.showHoaDon();
            dgvHoaDon.DataSource = table;
        }
        private void frmQuanLiHoaDon_Load(object sender, EventArgs e)
        {
            hienThi();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int d = e.RowIndex;
                txtMaHD.Text = dgvHoaDon.Rows[d].Cells[0].Value?.ToString();
                dtpNgayXuat.Value = DateTime.Parse(dgvHoaDon.Rows[d].Cells[1].Value?.ToString());
                txtMaNV.Text = dgvHoaDon.Rows[d].Cells[2].Value?.ToString();
                txtMaKH.Text = dgvHoaDon.Rows[d].Cells[3].Value?.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Mã hóa đơn trống");
                return;
            }
            if(MessageBox.Show("Xác nhận xóa?", "???", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BLL.deleteChiTietHoaDon(txtMaHD.Text);
                BLL.deleteHoaDon(txtMaHD.Text);
                hienThi();
            }
          
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
