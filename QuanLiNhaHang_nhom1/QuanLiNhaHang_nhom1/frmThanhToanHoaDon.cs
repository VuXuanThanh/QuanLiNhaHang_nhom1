using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;
namespace QuanLiNhaHang_nhom1
{
    public partial class frmThanhToanHoaDon : Form
    {
        public frmThanhToanHoaDon()
        {
            InitializeComponent();
        }
        public void fillIntoCombo()
        {
            DataTable tbl = new DataTable();
            tbl = BLL.fillComboNV();
            cbxMaNV.DataSource = tbl;
            cbxMaNV.ValueMember = "MaNV";
            cbxMaNV.DisplayMember = "MaNV";
            tbl = BLL.fillComboKH();
            cbxMaKH.DataSource = tbl;
            cbxMaKH.ValueMember = "MaKH";
            cbxMaKH.DisplayMember = "MaKH";
        }
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }
        //Hàm tạo khóa có dạng: TientoNgaythangnam_giophutgiay
        public string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        private void LoadDataGridView()// hiển thị món ăn lên datagridview với mã hóa đơn trên txtMaHD
        {
            DataTable table = new DataTable();
            table = BLL.showChiTietHoaDon(cbxMaKH.Text,DateTime.Parse(dtpNgayXuat.Text));
            dgvMonAnTheoHD.DataSource = table;
            dgvMonAnTheoHD.AllowUserToAddRows = false;
            dgvMonAnTheoHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void resetValue()//Xóa trắng các control
        {
            txtMaHD.Text = "";
            cbxMaNV.Text = "";
            cbxMaKH.Text = "";
            txtTenNV.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: ";
            DataTable DT = (DataTable)dgvMonAnTheoHD.DataSource;
            if (DT != null)
            {
                DT.Clear();
            }
          
        }
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            fillIntoCombo();
            resetValue();
            if (txtMaHD.Text != "")
            {
                LoadThongTinHoaDon();
            }
            txtTongTien.Text = "0";
            btnInHoaDon.Enabled = false;
            btnXuatFile.Enabled = false;
           
       
        }
        private void LoadThongTinHoaDon()
        {
            if (BLL.showNgayXuatTheoHoaDon(txtMaHD.Text) == "")
            {
                dtpNgayXuat.Value =DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            dtpNgayXuat.Value = DateTime.Parse(BLL.showNgayXuatTheoHoaDon(txtMaHD.Text));
            cbxMaNV.Text = BLL.showMaNVTheoHoaDon(txtMaHD.Text);
            
            txtTongTien.Text = BLL.showTongTienTheoHoaDon(txtMaHD.Text);
            lblBangChu.Text = "Bằng chữ " + ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
        }
        //123 => một trăm hai ba đồng
        //1,123,000=>một triệu một trăm hai ba nghìn đồng
        //1,123,345,000 => một tỉ một trăm hai ba triệu ba trăm bốn lăm ngàn đồng
       static string[] mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
        //Viết hàm chuyển số hàng chục, giá trị truyền vào là số cần chuyển và một biến đọc phần lẻ hay không ví dụ 101 => một trăm lẻ một
        private string DocHangChuc(double so, bool daydu)
        {
            string chuoi = "";
            //Hàm để lấy số hàng chục ví dụ 21/10 = 2
            Int64 chuc = Convert.ToInt64(Math.Floor((double)(so / 10)));
            //Lấy số hàng đơn vị bằng phép chia 21 % 10 = 1
            Int64 donvi = (Int64)so % 10;
            //Nếu số hàng chục tồn tại tức >=20
            if (chuc > 1)
            {
                chuoi = " " + mNumText[chuc] + " mươi";
                if (donvi == 1)
                {
                    chuoi += " mốt";
                }
            }
            else if (chuc == 1)
            {//Số hàng chục từ 10-19
                chuoi = " mười";
                if (donvi == 1)
                {
                    chuoi += " một";
                }
            }
            else if (daydu && donvi > 0)
            {//Nếu hàng đơn vị khác 0 và có các số hàng trăm ví dụ 101 => thì biến daydu = true => và sẽ đọc một trăm lẻ một
                chuoi = " lẻ";
            }
            if (donvi == 5 && chuc >= 1)
            {//Nếu đơn vị là số 5 và có hàng chục thì chuỗi sẽ là " lăm" chứ không phải là " năm"
                chuoi += " lăm";
            }
            else if (donvi > 1 || (donvi == 1 && chuc == 0))
            {
                chuoi += " " + mNumText[donvi];
            }
            return chuoi;
        }
        private string DocHangTram(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng trăm ví du 434 / 100 = 4 (hàm Floor sẽ làm tròn số nguyên bé nhất)
            Int64 tram = Convert.ToInt64(Math.Floor((double)so / 100));
            //Lấy phần còn lại của hàng trăm 434 % 100 = 34 (dư 34)
            so = so % 100;
            if (daydu || tram > 0)
            {
                chuoi = " " + mNumText[tram] + " trăm";
                chuoi += DocHangChuc(so, true);
            }
            else
            {
                chuoi = DocHangChuc(so, false);
            }
            return chuoi;
        }
        private string DocHangTrieu(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng triệu
            Int64 trieu = Convert.ToInt64(Math.Floor((double)so / 1000000));
            //Lấy phần dư sau số hàng triệu ví dụ 2,123,000 => so = 123,000
            so = so % 1000000;
            if (trieu > 0)
            {
                chuoi = DocHangTram(trieu, daydu) + " triệu";
                daydu = true;
            }
            //Lấy số hàng nghìn
            Int64 nghin = Convert.ToInt64(Math.Floor((double)so / 1000));
            //Lấy phần dư sau số hàng nghin 
            so = so % 1000;
            if (nghin > 0)
            {
                chuoi += DocHangTram(nghin, daydu) + " nghìn";
                daydu = true;
            }
            if (so > 0)
            {
                chuoi += DocHangTram(so, daydu);
            }
            return chuoi;
        }
        public string ChuyenSoSangChuoi(double so)
        {
            if (so == 0)
                return mNumText[0];
            string chuoi = "", hauto = "";
            Int64 ty;
            do
            {
                //Lấy số hàng tỷ
                ty = Convert.ToInt64(Math.Floor((double)so / 1000000000));
                //Lấy phần dư sau số hàng tỷ
                so = so % 1000000000;
                if (ty > 0)
                {
                    chuoi = DocHangTrieu(so, true) + hauto + chuoi;
                }
                else
                {
                    chuoi = DocHangTrieu(so, false) + hauto + chuoi;
                }
                hauto = " tỷ";
            } while (ty > 0);
            return chuoi + " đồng";
        }
        private void cbxMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMaNV.Text == "")
                txtTenNV.Text = "";    
            txtTenNV.Text = BLL.showTenTheoMa(cbxMaNV.Text);
        }
        private void cbxMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
            }
            txtTenKH.Text = BLL.showTenTheoMaKH(cbxMaKH.Text);
            txtDiaChi.Text = BLL.showDiaChiTheoMaKH(cbxMaKH.Text);
            txtDienThoai.Text = BLL.showDienThoaiTheoMaKH(cbxMaKH.Text);
            LoadDataGridView();
            //if (BLL.showTongTienTheoMaKH(cbxMaKH.Text, DateTime.Parse(dtpNgayXuat.Text)) == "")
            //{
            //    txtTongTien.Text = "0";
            //}
            //else
            //    txtTongTien.Text = BLL.showTongTienTheoMaKH(cbxMaKH.Text, DateTime.Parse(dtpNgayXuat.Text));
        }
        private void cbxMaMon_SelectedIndexChanged(object sender, EventArgs e)
        {
         /*   if (cbxMaMon.Text == "")
            {
                txtTenMon.Text = "";
                txtDonGia.Text = "";
            }
            txtTenMon.Text = BLL.showTenTheoMaMon(cbxMaMon.SelectedValue.ToString());
            txtDonGia.Text = BLL.showDonGiaTheoMaMon(cbxMaMon.SelectedValue.ToString());*/
        }
        private void cbxGiamGia_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* double sl, tt, dg, gg;
            if (txtSoLuong.Text == "")
            {
                sl = 0;
            }
            else sl = double.Parse(txtSoLuong.Text);
            if (cbxGiamGia.Text == "")
                gg = 0;
            else
                gg = double.Parse(cbxGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = double.Parse(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();*/
        }
        private void btnNhap_Click(object sender, EventArgs e)
        {
            resetValue();
            txtMaHD.Text = CreateKey("");
          // txtThanhTien.Text = "0";
            txtTongTien.Text = "0";
            frmThanhToanHoaDon.sum = 0;
        }
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
           /* double sl, tt, dg, gg;
            if (txtSoLuong.Text == "")
            {
                sl = 0;
            }
            else sl = double.Parse(txtSoLuong.Text);
            if (cbxGiamGia.Text == "")
                gg = 0;
            else
                gg = double.Parse(cbxGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = double.Parse(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();*/
        }
        static double sum = 0;
        private void btnThem_Click(object sender, EventArgs e)// thêm món ăn vào hóa đơn
        {
            //try
            //{
            if (!BLL.testHoaDon(txtMaHD.Text))
            {
                if (string.IsNullOrEmpty(txtMaHD.Text))
                {
                    MessageBox.Show("Chưa tạo hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(cbxMaNV.Text))
                {
                    MessageBox.Show("Chưa chọn mã nhân viên tạo hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(cbxMaKH.Text))
                {
                    MessageBox.Show("Chưa chọn mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // thêm mới vào bảng hóa đơn
                BLL.insertHD(txtMaHD.Text, dtpNgayXuat.Value.Date,cbxMaNV.Text, cbxMaKH.Text);
                // thêm mới bảng chi tiết hóa đơn
                if (dgvMonAnTheoHD.Rows.Count == 0)
                {
                    return;
                }
                foreach (DataGridViewRow row in dgvMonAnTheoHD.Rows)
                {
                    // currQty += row.Cells["qty"].Value;
                    BLL.insertChiTietHD(txtMaHD.Text, row.Cells[0].Value.ToString(), int.Parse(row.Cells[2].Value.ToString()), float.Parse(row.Cells[3].Value.ToString()), float.Parse(row.Cells[4].Value.ToString()), float.Parse(row.Cells[5].Value.ToString()));
                }
                BLL.updateGoiMon(cbxMaKH.Text);
                txtTongTien.Text = BLL.showTongTienTheoMaHD(txtMaHD.Text);
                MessageBox.Show("Đã thêm thành công hóa đơn", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnInHoaDon.Enabled = true;
                btnXuatFile.Enabled = true;
                BLL.updateDatBanKhiThanhToanHoaDon(cbxMaKH.Text, dtpNgayXuat.Value);
                

            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thoát ?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
             }
        }

        public static string maHD, tenKH, tenNV, diaChi, ngayXuat, tongTienBangChu, tongTienBangSo;
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text != "")
            {
                maHD = txtMaHD.Text;
            }
            tenKH = txtTenKH.Text;
            diaChi = txtDiaChi.Text;
            tenNV = txtTenNV.Text;
            ngayXuat = dtpNgayXuat.Value.ToString();
            tongTienBangSo = txtTongTien.Text;
            tongTienBangChu = lblBangChu.Text;
            frmInHoaDonThanhToan frmIHDTT = new frmInHoaDonThanhToan(maHD,tenKH,tenNV,ngayXuat,diaChi,tongTienBangSo, tongTienBangChu);
           
            frmIHDTT.ShowDialog();
        }
        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            resetValue();
        }

        

        public static DataTable table = new DataTable();
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //if (cbxChuoiTim.Text == "")
            //{
            //    MessageBox.Show("Hãy chọn một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cbxChuoiTim.Focus();
            //    return;
            //}
            //if (txtChuoiTim.Text == "")
            //{
            //    MessageBox.Show("Hãy chọn chuỗi tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtChuoiTim.Focus();
            //    return;
            //}
            //if (cbxChuoiTim.Text == "Mã hóa đơn")
            //{
            //    table = BLL.showThongTinHoaDonTimKiem(txtChuoiTim.Text, true);
            //}
            //else
            //{
            //    table = BLL.showThongTinHoaDonTimKiem(txtChuoiTim.Text, false);
            //}
            //frmTimKiemHoaDon frmTKHD = new frmTimKiemHoaDon();
            //frmTKHD.ShowDialog();
        }
        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            if (txtTongTien.Text == "")
            {
                lblBangChu.Text = "Bằng chữ: ";
            }
            lblBangChu.Text = "Bằng chữ: " + ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            resetValue();
            txtMaHD.Text = CreateKey("");
            btnInHoaDon.Enabled = false;
            btnXuatFile.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dgvMonAnTheoHD.Rows.Count.ToString());
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;
            int hang = 0, cot = 0;
            DataTable tblHoaDon, tblMonAn;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman";

            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Nhà hàng Phương Nam";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Bắc Từ Liêm - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (03)08424419";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN THANH TOÁN";
            tblHoaDon = BLL.showThongTinHoaDonDeXuatRaExel(txtMaHD.Text);

            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblHoaDon.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblHoaDon.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblHoaDon.Rows[0][5].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = "'"+tblHoaDon.Rows[0][4].ToString();
            exRange.Range["B10:B10"].Value = "Ngày xuất:";
            exRange.Range["C10:E10"].MergeCells = true;
            exRange.Range["C10:C10"].Value = dtpNgayXuat.Value.ToString("dd-MM-yyyy HH:mmm:ss");


            //Lấy thông tin các mặt hàng
            //select HOADON.MaHD, NgayXuat,sum(SoLuong*CHITIETHOADON.DonGia-SoLuong*CHITIETHOADON.DonGia*GiamGia/100) as TongTien, KHACHHANG.TenKH, KHACHHANG.DienThoai, KHACHHANG.DiaChi, NHANVIEN.TenNV

            exRange.Range["A11:G11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Mã món";
            exRange.Range["C11:C11"].Value = "Tên món";
            exRange.Range["C11:C11"].ColumnWidth = 27;
            exRange.Range["D11:D11"].Value = "Số lượng";
            exRange.Range["E11:E11"].Value = "Đơn giá";
            exRange.Range["F11:F11"].Value = "Tiền giảm giá";
            exRange.Range["F11:F11"].ColumnWidth = 25;
            exRange.Range["G11:G11"].Value = "Thành tiền";
            exRange.Range["G11:G11"].ColumnWidth = 25;
            tblMonAn = BLL.showMonAnDeXuatRaFileExel(txtMaHD.Text);
            for (hang = 0; hang < tblMonAn.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblMonAn.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblMonAn.Rows[hang][cot].ToString();
                    if (cot == 4) exSheet.Cells[cot + 2][hang + 12] = tblMonAn.Rows[hang][cot].ToString();
                }
            }
            DialogResult result = MessageBox.Show("Bạn muốn xuất hóa đơn ra exel ko?", "Thông báo?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            exRange = exSheet.Cells[cot][hang + 13];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";

            exRange = exSheet.Cells[cot + 1][hang + 13];
            exRange.Font.Bold = true;
            exRange.Value2 +=BLL.showTongTienTheoMaHD(txtMaHD.Text);

            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].Font.Size = 16;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + ChuyenSoSangChuoi(double.Parse(BLL.showTongTienTheoMaHD(txtMaHD.Text)));

            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblHoaDon.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên thu tiền";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblHoaDon.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }
        
    }
}
