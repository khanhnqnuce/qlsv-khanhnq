namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_103_TuDienPhongThi
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
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters1 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters2 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_themdong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_xoadong = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdanhsachphongthi = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtKhoa = new System.Windows.Forms.ToolStripTextBox();
            this.btntimkiem = new System.Windows.Forms.ToolStripButton();
            this.lbsiso = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menu_ug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_ug
            // 
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_themdong,
            this.menuStrip_xoadong});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(153, 70);
            // 
            // menuStrip_themdong
            // 
            this.menuStrip_themdong.Name = "menuStrip_themdong";
            this.menuStrip_themdong.Size = new System.Drawing.Size(152, 22);
            this.menuStrip_themdong.Text = "Thêm dòng";
            this.menuStrip_themdong.Click += new System.EventHandler(this.menuStrip_themdong_Click);
            // 
            // menuStrip_xoadong
            // 
            this.menuStrip_xoadong.Name = "menuStrip_xoadong";
            this.menuStrip_xoadong.Size = new System.Drawing.Size(152, 22);
            this.menuStrip_xoadong.Text = "Xóa dòng";
            this.menuStrip_xoadong.Click += new System.EventHandler(this.menuStrip_xoadong_Click);
            // 
            // dgv_DanhSach
            // 
            this.dgv_DanhSach.ContextMenuStrip = this.menu_ug;
            this.dgv_DanhSach.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.dgv_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.dgv_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.dgv_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dgv_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dgv_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.dgv_DanhSach.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(598, 432);
            this.dgv_DanhSach.TabIndex = 25;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.dgv_DanhSach.AfterExitEditMode += new System.EventHandler(this.uG_DanhSach_AfterExitEditMode);
            this.dgv_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.dgv_DanhSach_BeforeRowsDeleted);
            this.dgv_DanhSach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uG_DanhSach_KeyDown);
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdanhsachphongthi});
            // 
            // rptdanhsachphongthi
            // 
            this.rptdanhsachphongthi.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachphongthi.rst";
            this.rptdanhsachphongthi.ReportName = "";
            this.rptdanhsachphongthi.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // excelExportFilter1
            // 
            this.excelExportFilter1.ExportInLargePage = true;
            this.excelExportFilter1.ExportInOnePage = true;
            this.excelExportFilter1.ExportWithoutPageDelimeters = true;
            this.excelExportFilter1.ExtraParameters = extraParameters1;
            // 
            // pdfExportFilter1
            // 
            this.pdfExportFilter1.ChangePermissionsPassword = null;
            this.pdfExportFilter1.Compress = true;
            this.pdfExportFilter1.ExtraParameters = extraParameters2;
            this.pdfExportFilter1.UserPassword = null;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(598, 25);
            this.panel3.TabIndex = 28;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.txtKhoa,
            this.btntimkiem,
            this.lbsiso});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(598, 25);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel3.Text = "Nhập tên phòng:";
            // 
            // txtKhoa
            // 
            this.txtKhoa.Name = "txtKhoa";
            this.txtKhoa.Size = new System.Drawing.Size(100, 25);
            this.txtKhoa.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKhoa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKhoa_KeyUp);
            // 
            // btntimkiem
            // 
            this.btntimkiem.Image = global::QLSV.Frm.Properties.Resources.find_icon;
            this.btntimkiem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btntimkiem.Name = "btntimkiem";
            this.btntimkiem.Size = new System.Drawing.Size(74, 22);
            this.btntimkiem.Text = "Tìm kiếm";
            this.btntimkiem.Click += new System.EventHandler(this.btntimkiem_Click);
            // 
            // lbsiso
            // 
            this.lbsiso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbsiso.Name = "lbsiso";
            this.lbsiso.Size = new System.Drawing.Size(0, 22);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_DanhSach);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(598, 432);
            this.panel2.TabIndex = 29;
            // 
            // Frm_103_TuDienPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_103_TuDienPhongThi";
            this.Size = new System.Drawing.Size(598, 457);
            this.Load += new System.EventHandler(this.FrmDanhsachphongthi_Load);
            this.menu_ug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_themdong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_xoadong;
        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachphongthi;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txtKhoa;
        private System.Windows.Forms.ToolStripButton btntimkiem;
        private System.Windows.Forms.ToolStripLabel lbsiso;
        private System.Windows.Forms.Panel panel2;
    }
}