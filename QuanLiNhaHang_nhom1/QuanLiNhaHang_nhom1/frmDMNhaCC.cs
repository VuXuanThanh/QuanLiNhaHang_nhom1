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
    public partial class frmDMNhaCC : Form
    {
        public static string str;
        public frmDMNhaCC()
        {
            InitializeComponent();
        }
        private void frmDMNhaCC_Load(object sender, EventArgs e)
        {
            DataTable tblNhaCC = new DataTable();
            tblNhaCC = BLL.showNCC();
            dgvNhaCC.DataSource = tblNhaCC;
        }
        private void dgvNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtMaNCC.Text = dgvNhaCC.Rows[d].Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNhaCC.Rows[d].Cells[1].Value.ToString();
            txtDienThoai.Text = dgvNhaCC.Rows[d].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvNhaCC.Rows[d].Cells[2].Value.ToString();
        }
        private void ResetValue()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Clear();
            txtDienThoai.Clear();
        }
        private void btnNhap_Click(object sender, EventArgs e)
        {
            txtTenNCC.Focus();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnThem.Enabled = true;
            btnNhap.Enabled = true;
            ResetValue();
           
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("vui lòng nhập đầy đủ các trường còn lại");
            }
            BLL.insertNCC(txtTenNCC.Text, txtDiaChi.Text, txtDienThoai.Text);
            frmDMNhaCC_Load(sender, e);
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnNhap.Enabled = true;
            txtMaNCC.Enabled = false;
           
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "") 
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("vui lòng nhập đầy đủ các trường còn lại");
            }
            BLL.updateNCC(txtMaNCC.Text, txtTenNCC.Text, txtDiaChi.Text, txtDienThoai.Text);
            frmDMNhaCC_Load(sender, e);
            ResetValue();
            btnBoQua.Enabled = false;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult dialog = MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                BLL.deleteNCC(txtMaNCC.Text);
                frmDMNhaCC_Load(sender, e);
                ResetValue();
            }
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnNhap.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            frmDMNhaCC_Load(sender,e);
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            frmTimKiemNCC frmTK = new frmTimKiemNCC();
            frmTK.ShowDialog();
            str = frmTK.maNCC;
            DataTable tblNhaCC = new DataTable();
            tblNhaCC = BLL.show1NCC(str);
            dgvNhaCC.DataSource = tblNhaCC;
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn muốn đóng tác vụ này?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
