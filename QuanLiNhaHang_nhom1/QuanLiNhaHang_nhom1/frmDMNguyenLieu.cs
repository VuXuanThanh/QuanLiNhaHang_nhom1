using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiNhaHang_nhom1
{
    public partial class frmDMNguyenLieu : Form
    {
        public frmDMNguyenLieu()
        {
            InitializeComponent();
        }
        private void resetValue()
        {
            txtMaNL.Text = "";
            txtTenNL.Text = "";
            txtHanSuDung.Text = "";
            cbxDonViTinh.Text = "";
            txtSoLuongTonKho.Text = "";
            cbxDonViTinh.Text = "";
            cbxMaNCC.Text = "";
            txtTenNCC.Text = "";
        }
        private void fillComboboxNCC()
        {
            DataTable table = new DataTable();
            table = BLL.fillComboMaNCC();
            cbxMaNCC.DataSource = table;
            cbxMaNCC.ValueMember = "MaNCC";
            cbxMaNCC.DisplayMember = "MaNCC";
        }
        private void loadDataGridView()
        {
            DataTable table = new DataTable();
            table = BLL.showNguyenLieu_NCC();
            dgvNguyenLieu.DataSource = table;
            foreach (DataGridViewRow row in dgvNguyenLieu.Rows)
                if (row.Index % 2 == 0)
                    row.DefaultCellStyle.BackColor = Color.Beige;
                else
                    row.DefaultCellStyle.BackColor = Color.White;
        }
        private void frmDMNguyenLieu_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            resetValue();
            fillComboboxNCC();
            dgvNguyenLieu.AllowUserToAddRows = false;
            dgvNguyenLieu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            resetValue();
            txtTenNL.Focus();
            btnXoa.Enabled = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenNL.Text == "")
            {
                MessageBox.Show("Chưa nhập tên nguyên liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxDonViTinh.Text == "")
            {
                MessageBox.Show("Chưa chọn đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtSoLuongTonKho.Text == "")
            {
                MessageBox.Show("Chưa nhập số lượng tồn kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtHanSuDung.Text == "")
            {
                MessageBox.Show("Chưa nhập hạn sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (BLL.testTenNguyenLieu(txtTenNL.Text)=="0")
            {
                BLL.insertNguyenLieu(txtTenNL.Text, dtpNgayNhap.Value.Date, int.Parse(txtHanSuDung.Text), cbxDonViTinh.Text, int.Parse(txtSoLuongTonKho.Text));
            }
            string duDoan = BLL.IDENT_CURRENT_NGUYENLIEU();
            string MaNL = "NL" + duDoan;
            if (cbxMaNCC.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BLL.insertNguyenLieu_NCC(MaNL, cbxMaNCC.Text);
            loadDataGridView();
            MessageBox.Show("Đã insert thành công nguyên liệu '"+txtTenNL.Text+"' của NCC'"+txtTenNCC.Text+"'","Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
            cbxMaNCC.Text = "";
            txtTenNCC.Text = "";
            btnXoa.Enabled = true;
        }

        private void cbxMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenNCC.Text = BLL.showTenTheoMaNCC(cbxMaNCC.Text);
        }

        private void btnHienThiDSNL_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            DataTable table = new DataTable();
            if (cbxMaNCC.Text == "")
            {
                table = BLL.showDSNguyenLieu();
                dgvNguyenLieu.DataSource = table;
            }
            else
            {
                table = BLL.showDSNguyenLieuTheoNCC(cbxMaNCC.Text);
                dgvNguyenLieu.DataSource = table;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNL.Text == "")
            {
                MessageBox.Show("Chưa chọn nguyên liệu nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxMaNCC.Text == "")
            {
                MessageBox.Show("Chưa chọn nhà cung cấp nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BLL.deleteNguyenLieuTheoNCC(cbxMaNCC.Text, txtMaNL.Text);
            loadDataGridView();
        }

        private void dgvNguyenLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNguyenLieu.Columns.Count <= 4)
            {
                int d = e.RowIndex;
                txtMaNL.Text = dgvNguyenLieu.Rows[d].Cells[0].Value.ToString();
                txtTenNL.Text = dgvNguyenLieu.Rows[d].Cells[1].Value.ToString();
                cbxMaNCC.Text = dgvNguyenLieu.Rows[d].Cells[2].Value.ToString();
                txtTenNCC.Text = dgvNguyenLieu.Rows[d].Cells[3].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dgvNguyenLieu.Columns.Count.ToString());
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thoát ?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
