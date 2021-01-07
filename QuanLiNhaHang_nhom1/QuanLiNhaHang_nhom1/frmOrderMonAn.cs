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
    public partial class frmOrderMonAn : Form
    {
        public string strNhan;
        public frmOrderMonAn()
        {
            InitializeComponent();
        }
        private DataTable table = new DataTable();
        private void frmOrderMonAn_Load(object sender, EventArgs e)
        {
            txtMaKH.Text = strNhan;
            DataTable table1 = new DataTable();
            table1 = BLL.fillComboDanhMuc();
            cbxMaDanhMuc.DataSource = table1;
            cbxMaDanhMuc.DisplayMember = "TenDanhMuc";
            cbxMaDanhMuc.ValueMember = "TenDanhMuc";

            table.Columns.Add("Mã món", typeof(string));
            table.Columns.Add("Tên món", typeof(string));
            table.Columns.Add("Số lượng", typeof(int));
            table.Columns.Add("Đơn giá", typeof(float));
            table.Columns.Add("Giảm giá", typeof(float));
            table.Columns.Add("Thành tiền", typeof(float));
            dgvDSMonDaChon.DataSource = table;

            dgvDSMonDaChon.AllowUserToAddRows = false;
            dgvDSMonDaChon.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void cbxMaDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = BLL.showMonAnTheoTenDanhMuc(cbxMaDanhMuc.Text);
            dgvDSMonAn.DataSource = table;
        }

        private void dgvDSMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int d = e.RowIndex;
                txtMaMon.Text = dgvDSMonAn.Rows[d].Cells[0].Value.ToString();
                txtTenMon.Text = dgvDSMonAn.Rows[d].Cells[1].Value.ToString();
            }
            catch(Exception)
            {
                MessageBox.Show("Vui lòng chọn 1 bản ghi trong bảng");
            }
        }

        private void txtMaMon_TextChanged(object sender, EventArgs e)
        {
            if (txtMaMon.Text != "")
            {
                txtTenMon.Text = BLL.showTenTheoMaMon(txtMaMon.Text);
                txtDonGia.Text = BLL.showDonGiaTheoMaMon(txtMaMon.Text);
                string anhMonAn = BLL.showAnhTheoMaMon(txtMaMon.Text);
                Image image = Image.FromFile(@"F:\BTL lập trình windows nhóm 1\BTL-C-\QuanLiNhaHang_nhom1\QuanLiNhaHang_nhom1\Images\Dishes\" + anhMonAn);
                picAnhMonAn.Image = image;
                picAnhMonAn.Width = image.Width;
                picAnhMonAn.Height = image.Height; 
            }
          
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtMaMon.Text == "")
            {
                return;
            }
            double tt, sl, dg, gg;
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = double.Parse(txtDonGia.Text);
            sl =double.Parse(numSoLuong.Value.ToString());
            if (cbxGiamGia.Text == "")
                gg = 0;
            else
                gg = double.Parse(cbxGiamGia.Text);
            tt = dg * sl - dg * sl * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cbxGiamGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = double.Parse(txtDonGia.Text);
            sl = double.Parse(numSoLuong.Value.ToString());
            if (cbxGiamGia.Text == "")
                gg = 0;
            else
                gg = double.Parse(cbxGiamGia.Text);
            tt = dg * sl - dg * sl * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }
        private bool checkDuplicateKey(string ma)
        {
            for(int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["Mã món"].ToString() == ma)
                    return false;
            }
            return true;
        }
        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtDonGia.Text == "")
            {
                return;
            }
            dg = double.Parse(txtDonGia.Text);
            sl = double.Parse(numSoLuong.Value.ToString());
            if (cbxGiamGia.Text == "")
                gg = 0;
            else
                gg = double.Parse(cbxGiamGia.Text);
            tt = dg * sl - dg * sl * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                float gg;
                if (cbxGiamGia.Text == "")
                    gg = 0;
                else
                    gg = float.Parse(cbxGiamGia.Text);
                if (checkDuplicateKey(txtMaMon.Text))
                {
                    table.Rows.Add(txtMaMon.Text, txtTenMon.Text, int.Parse(numSoLuong.Value.ToString()), float.Parse(txtDonGia.Text), gg, float.Parse(txtThanhTien.Text));
                }
                else
                {
                    MessageBox.Show("Món ăn này đã được chọn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                resetValue();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteRow(string ma)
        {
            List<DataRow> toDelete = new List<DataRow>();

            foreach (DataRow dr in table.Rows)
            {
                if (dr["Mã món"].ToString() == ma)
                {
                    toDelete.Add(dr);
                }
            }

            foreach (DataRow dr in toDelete)
            {
                table.Rows.Remove(dr);
            }
        }
        private void resetValue()
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            txtDonGia.Text = "";
            cbxGiamGia.Text = "";
            numSoLuong.Value = 1;
            txtThanhTien.Text = "";
            picAnhMonAn.Image = null;
        }
       
        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            if (dgvDSMonDaChon.Rows.Count>0)
            {
                BLL.insertGoiMon(txtMaKH.Text,false,DateTime.Parse(dtpThoiGian.Text));
                foreach (DataRow row in table.Rows)
                {
                    string duDoan = BLL.IDENT_CURRENT();
                    string IDGoiMon = "GM" + duDoan;

                    BLL.insertChiTietGoiMon(IDGoiMon, row["Mã món"].ToString(), int.Parse(row["Số lượng"].ToString()), float.Parse(row["Giảm giá"].ToString()));
                }
                MessageBox.Show("Đã order thành công món ăn");
                dgvDSMonDaChon.DataSource = null;
            }
            else
            {
                MessageBox.Show("Chưa chọn món ăn nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvDSMonDaChon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDSMonDaChon.Rows.Count > -1)
                {
                    int d = e.RowIndex;
                    txtMaMon.Text = dgvDSMonDaChon.Rows[d].Cells[0].Value.ToString();
                    txtTenMon.Text = dgvDSMonDaChon.Rows[d].Cells[1].Value.ToString();
                    numSoLuong.Value = decimal.Parse(dgvDSMonDaChon.Rows[d].Cells[2].Value.ToString());
                    txtDonGia.Text = dgvDSMonDaChon.Rows[d].Cells[3].Value.ToString();
                    cbxGiamGia.Text = dgvDSMonDaChon.Rows[d].Cells[4].Value.ToString();
                    txtThanhTien.Text = dgvDSMonDaChon.Rows[d].Cells[5].Value.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn 1 bản ghi trong bảng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaMon.Text == "")
            {
                MessageBox.Show("Chưa chọn món để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            deleteRow(txtMaMon.Text);
            resetValue();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
