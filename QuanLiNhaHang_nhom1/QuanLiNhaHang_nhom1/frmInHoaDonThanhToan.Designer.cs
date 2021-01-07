
namespace QuanLiNhaHang_nhom1
{
    partial class frmInHoaDonThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sp_getInforOfFoodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QuanLiNhaHang_nhom1DataSet = new QuanLiNhaHang_nhom1.QuanLiNhaHang_nhom1DataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.sp_getInforOfFoodTableAdapter = new QuanLiNhaHang_nhom1.QuanLiNhaHang_nhom1DataSetTableAdapters.sp_getInforOfFoodTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sp_getInforOfFoodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuanLiNhaHang_nhom1DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // sp_getInforOfFoodBindingSource
            // 
            this.sp_getInforOfFoodBindingSource.DataMember = "sp_getInforOfFood";
            this.sp_getInforOfFoodBindingSource.DataSource = this.QuanLiNhaHang_nhom1DataSet;
            // 
            // QuanLiNhaHang_nhom1DataSet
            // 
            this.QuanLiNhaHang_nhom1DataSet.DataSetName = "QuanLiNhaHang_nhom1DataSet";
            this.QuanLiNhaHang_nhom1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.sp_getInforOfFoodBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLiNhaHang_nhom1.InHoaDonThanhToan.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(929, 602);
            this.reportViewer1.TabIndex = 0;
            // 
            // sp_getInforOfFoodTableAdapter
            // 
            this.sp_getInforOfFoodTableAdapter.ClearBeforeFill = true;
            // 
            // frmInHoaDonThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 602);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmInHoaDonThanhToan";
            this.Text = "frmInHoaDonThanhToan";
            this.Load += new System.EventHandler(this.frmInHoaDonThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sp_getInforOfFoodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuanLiNhaHang_nhom1DataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sp_getInforOfFoodBindingSource;
        private QuanLiNhaHang_nhom1DataSet QuanLiNhaHang_nhom1DataSet;
        private QuanLiNhaHang_nhom1DataSetTableAdapters.sp_getInforOfFoodTableAdapter sp_getInforOfFoodTableAdapter;
    }
}