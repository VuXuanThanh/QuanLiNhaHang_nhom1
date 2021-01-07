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
    public partial class frmNhanVien : Form
    {
       public static string value;
        public frmNhanVien()
        {
            InitializeComponent();
        }
        private void hienThi()
        {
            DataTable table = new DataTable();
            table = BLL.showNV();
            dgvNhanVien.DataSource = table;
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
                if (row.Index % 2 == 0)
                    row.DefaultCellStyle.BackColor = Color.Beige;
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            txtMaNV.Enabled = false;
        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            hienThi();
        }
        private void resetValue()
        {
            txtMaNV.Enabled = false;
            txtMaNV.Text = "";
            txtTenNV.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtLuongThang.Clear();
            txtTenNV.Focus();
        }
        private void btnNhap_Click(object sender, EventArgs e)
        {
            resetValue();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string gt;
                if (txtTenNV.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNV.Focus();
                    return;
                }
                if (txtDiaChi.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiaChi.Focus();
                    return;
                }
                if (txtDienThoai.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDienThoai.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtLuongThang.Text))
                {
                    MessageBox.Show("Bạn phải nhập lương tháng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLuongThang.Focus();
                    return;
                }
                gt = chkNam.Checked ? "Nam" : "Nữ";
                BLL.insertNV(txtTenNV.Text, gt, dtpNgaySinh.Value.Date, txtDiaChi.Text, txtDienThoai.Text, float.Parse(txtLuongThang.Text), cbxChucVu.Text);
                hienThi();
                resetValue();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi nhập không đúng kiểu "+ex.Message);
            }
        }
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int d = e.RowIndex;
                txtMaNV.Text = dgvNhanVien.Rows[d].Cells[0].Value.ToString();
                txtTenNV.Text = dgvNhanVien.Rows[d].Cells[1].Value.ToString();
                if (dgvNhanVien.Rows[d].Cells[2].Value.ToString().Contains("Nam"))
                    chkNam.Checked = true;
                else chkNam.Checked = false;
                dtpNgaySinh.Value = Convert.ToDateTime(dgvNhanVien.Rows[d].Cells[3].Value);
                txtDiaChi.Text = dgvNhanVien.Rows[d].Cells[4].Value.ToString();
                txtDienThoai.Text = dgvNhanVien.Rows[d].Cells[5].Value.ToString();
                txtLuongThang.Text = dgvNhanVien.Rows[d].Cells[6].Value.ToString();
                cbxChucVu.Text = dgvNhanVien.Rows[d].Cells[7].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string gt;
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLuongThang.Text))
            {
                MessageBox.Show("Bạn phải nhập lương tháng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongThang.Focus();
                return;
            }
            gt = chkNam.Checked ? "Nam" : "Nữ";
            BLL.updateNV(txtMaNV.Text, txtTenNV.Text, gt, dtpNgaySinh.Value.Date, txtDiaChi.Text, txtDienThoai.Text, 
                float.Parse(txtLuongThang.Text), cbxChucVu.Text);
            hienThi();
            resetValue();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BLL.deleteNV(txtMaNV.Text);
                hienThi();
                resetValue();
                MessageBox.Show("Đã xóa thành công 1 bản ghi");
            }
          
        }
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            hienThi();
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            frmTimKiemNV frmTKNV = new frmTimKiemNV();
            frmTKNV.ShowDialog();
            value = frmTKNV.getStr;
            DataTable tblNV = new DataTable();
            tblNV = BLL.show1NV(value);
            dgvNhanVien.DataSource = tblNV;
            hienThi();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
