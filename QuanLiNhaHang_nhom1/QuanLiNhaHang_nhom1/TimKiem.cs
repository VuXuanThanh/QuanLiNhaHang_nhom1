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
    public partial class TimKiem : Form
    {
        public TimKiem()
        {
            InitializeComponent();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string str = txtChuoiTim.Text;
            try
            {

                string queryAddress = "https://www.google.com/maps/search/" + str;


                webBrowser1.Navigate(queryAddress);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TimKiem_Load(object sender, EventArgs e)
        {

        }
    }
}
