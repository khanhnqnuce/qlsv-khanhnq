namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_207_ChamDiemThi
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters1 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters2 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters3 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters4 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_Sua = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Luulai = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Huy = new System.Windows.Forms.ToolStripMenuItem();
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.rptdapandethi = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.rptdanhsachsinhvien = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportManager2 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.fileReportSlot1 = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.excelExportFilter2 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.fileReportSlot2 = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.pnl_from = new System.Windows.Forms.Panel();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtmade = new System.Windows.Forms.ToolStripTextBox();
            this.btnTimkiem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btntimkiemsinhvien = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnrefresh = new System.Windows.Forms.ToolStripButton();
            this.pdfExportFilter2 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.menu_ug.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pnl_from.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_ug
            // 
            this.menu_ug.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Sua,
            this.menuStrip_Luulai,
            this.menuStrip_Huy});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(128, 70);
            // 
            // menuStrip_Sua
            // 
            this.menuStrip_Sua.Name = "menuStrip_Sua";
            this.menuStrip_Sua.Size = new System.Drawing.Size(127, 22);
            this.menuStrip_Sua.Text = "Chỉnh sửa";
            // 
            // menuStrip_Luulai
            // 
            this.menuStrip_Luulai.Name = "menuStrip_Luulai";
            this.menuStrip_Luulai.Size = new System.Drawing.Size(127, 22);
            this.menuStrip_Luulai.Text = "Lưu lại";
            // 
            // menuStrip_Huy
            // 
            this.menuStrip_Huy.Name = "menuStrip_Huy";
            this.menuStrip_Huy.Size = new System.Drawing.Size(127, 22);
            this.menuStrip_Huy.Text = "Hủy";
            // 
            // excelExportFilter1
            // 
            this.excelExportFilter1.ExportInLargePage = true;
            this.excelExportFilter1.ExportInOnePage = true;
            this.excelExportFilter1.ExportWithoutPageDelimeters = true;
            this.excelExportFilter1.ExtraParameters = extraParameters1;
            // 
            // rptdapandethi
            // 
            this.rptdapandethi.FilePath = "";
            this.rptdapandethi.ReportName = "";
            this.rptdapandethi.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdapandethi});
            // 
            // pdfExportFilter1
            // 
            this.pdfExportFilter1.ChangePermissionsPassword = null;
            this.pdfExportFilter1.Compress = true;
            this.pdfExportFilter1.ExtraParameters = extraParameters2;
            this.pdfExportFilter1.UserPassword = null;
            // 
            // rptdanhsachsinhvien
            // 
            this.rptdanhsachsinhvien.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachsinhvien.rst";
            this.rptdanhsachsinhvien.ReportName = "";
            this.rptdanhsachsinhvien.ReportScriptType = null;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItem1.Text = "Chỉnh sửa";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItem2.Text = "Lưu lại";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItem3.Text = "Hủy";
            // 
            // reportManager2
            // 
            this.reportManager2.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager2.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.fileReportSlot1});
            // 
            // fileReportSlot1
            // 
            this.fileReportSlot1.FilePath = "";
            this.fileReportSlot1.ReportName = "";
            this.fileReportSlot1.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // excelExportFilter2
            // 
            this.excelExportFilter2.ExportInLargePage = true;
            this.excelExportFilter2.ExportInOnePage = true;
            this.excelExportFilter2.ExportWithoutPageDelimeters = true;
            this.excelExportFilter2.ExtraParameters = extraParameters3;
            // 
            // fileReportSlot2
            // 
            this.fileReportSlot2.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachsinhvien.rst";
            this.fileReportSlot2.ReportName = "";
            this.fileReportSlot2.ReportScriptType = null;
            // 
            // pnl_from
            // 
            this.pnl_from.Controls.Add(this.dgv_DanhSach);
            this.pnl_from.Controls.Add(this.toolStrip1);
            this.pnl_from.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_from.Location = new System.Drawing.Point(0, 0);
            this.pnl_from.Margin = new System.Windows.Forms.Padding(5);
            this.pnl_from.Name = "pnl_from";
            this.pnl_from.Size = new System.Drawing.Size(626, 451);
            this.pnl_from.TabIndex = 32;
            this.pnl_from.Visible = false;
            // 
            // dgv_DanhSach
            // 
            this.dgv_DanhSach.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.dgv_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.dgv_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dgv_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dgv_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_DanhSach.Location = new System.Drawing.Point(0, 25);
            this.dgv_DanhSach.Margin = new System.Windows.Forms.Padding(5);
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(626, 426);
            this.dgv_DanhSach.TabIndex = 33;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.dgv_DanhSach_InitializeLayout);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtmade,
            this.btnTimkiem,
            this.toolStripSeparator1,
            this.btntimkiemsinhvien,
            this.toolStripSeparator2,
            this.btnrefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(626, 25);
            this.toolStrip1.TabIndex = 32;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(78, 22);
            this.toolStripLabel1.Text = "Nhập mã đề :";
            // 
            // txtmade
            // 
            this.txtmade.Name = "txtmade";
            this.txtmade.Size = new System.Drawing.Size(150, 25);
            this.txtmade.ToolTipText = "Nhập mã đề cần tìm kiếm";
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.Image = global::QLSV.Frm.Properties.Resources.find_icon;
            this.btnTimkiem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(77, 22);
            this.btnTimkiem.Text = "Tìm kiếm";
            this.btnTimkiem.ToolTipText = "Enter";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btntimkiemsinhvien
            // 
            this.btntimkiemsinhvien.Image = global::QLSV.Frm.Properties.Resources.find_icon;
            this.btntimkiemsinhvien.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btntimkiemsinhvien.Name = "btntimkiemsinhvien";
            this.btntimkiemsinhvien.Size = new System.Drawing.Size(127, 22);
            this.btntimkiemsinhvien.Text = "Tìm kiếm sinh viên";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnrefresh
            // 
            this.btnrefresh.Image = global::QLSV.Frm.Properties.Resources.refresh1_icon;
            this.btnrefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(70, 22);
            this.btnrefresh.Text = "Quay lại";
            this.btnrefresh.ToolTipText = "Tải lại dữ liệu";
            // 
            // pdfExportFilter2
            // 
            this.pdfExportFilter2.ChangePermissionsPassword = null;
            this.pdfExportFilter2.Compress = true;
            this.pdfExportFilter2.ExtraParameters = extraParameters4;
            this.pdfExportFilter2.UserPassword = null;
            // 
            // Frm_207_ChamDiemThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_from);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_207_ChamDiemThi";
            this.Size = new System.Drawing.Size(626, 451);
            this.Load += new System.EventHandler(this.FrmDanhSachBaiLam_Load);
            this.menu_ug.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnl_from.ResumeLayout(false);
            this.pnl_from.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Sua;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Luulai;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Huy;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdapandethi;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachsinhvien;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager2;
        private PerpetuumSoft.Reporting.Components.FileReportSlot fileReportSlot1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter2;
        private PerpetuumSoft.Reporting.Components.FileReportSlot fileReportSlot2;
        private System.Windows.Forms.Panel pnl_from;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtmade;
        private System.Windows.Forms.ToolStripButton btnTimkiem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btntimkiemsinhvien;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnrefresh;
        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
    }
}
