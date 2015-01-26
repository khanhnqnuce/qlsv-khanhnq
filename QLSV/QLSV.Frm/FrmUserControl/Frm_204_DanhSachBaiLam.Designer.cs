namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_204_DanhSachBaiLam
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
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_Suamasv = new System.Windows.Forms.ToolStripMenuItem();
            this.rptdanhsachsinhvien = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.rptdsbailam = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtmade = new System.Windows.Forms.ToolStripTextBox();
            this.btnTimkiem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btntimkiemsinhvien = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnrefresh = new System.Windows.Forms.ToolStripButton();
            this.pnl_from = new System.Windows.Forms.Panel();
            this.MenuStrip_suamade = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.menu_ug.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnl_from.SuspendLayout();
            this.SuspendLayout();
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
            this.dgv_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.dgv_DanhSach.Margin = new System.Windows.Forms.Padding(5);
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(654, 473);
            this.dgv_DanhSach.TabIndex = 25;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.dgv_DanhSach_InitializeLayout);
            this.dgv_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.dgv_DanhSach_BeforeRowsDeleted);
            this.dgv_DanhSach.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.dgv_DanhSach_DoubleClickCell);
            // 
            // menu_ug
            // 
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Suamasv,
            this.MenuStrip_suamade});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(175, 70);
            // 
            // menuStrip_Suamasv
            // 
            this.menuStrip_Suamasv.Name = "menuStrip_Suamasv";
            this.menuStrip_Suamasv.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Suamasv.Text = "Sửa mã sv";
            this.menuStrip_Suamasv.Click += new System.EventHandler(this.menuStrip_Suamasv_Click);
            // 
            // rptdanhsachsinhvien
            // 
            this.rptdanhsachsinhvien.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachsinhvien.rst";
            this.rptdanhsachsinhvien.ReportName = "";
            this.rptdanhsachsinhvien.ReportScriptType = null;
            // 
            // pdfExportFilter1
            // 
            this.pdfExportFilter1.ChangePermissionsPassword = null;
            this.pdfExportFilter1.Compress = true;
            this.pdfExportFilter1.ExtraParameters = extraParameters1;
            this.pdfExportFilter1.UserPassword = null;
            // 
            // excelExportFilter1
            // 
            this.excelExportFilter1.ExportInLargePage = true;
            this.excelExportFilter1.ExportInOnePage = true;
            this.excelExportFilter1.ExportWithoutPageDelimeters = true;
            this.excelExportFilter1.ExtraParameters = extraParameters2;
            // 
            // rptdsbailam
            // 
            this.rptdsbailam.FilePath = "";
            this.rptdsbailam.ReportName = "";
            this.rptdsbailam.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdsbailam});
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
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
            this.toolStrip1.Size = new System.Drawing.Size(654, 25);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(76, 22);
            this.toolStripLabel1.Text = "Nhập mã đề :";
            // 
            // txtmade
            // 
            this.txtmade.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtmade.Name = "txtmade";
            this.txtmade.Size = new System.Drawing.Size(100, 25);
            this.txtmade.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtmade.ToolTipText = "Nhập mã đề cần tìm kiếm";
            this.txtmade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtmade_KeyDown);
            this.txtmade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtmade_KeyUp);
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.Image = global::QLSV.Frm.Properties.Resources.find_icon;
            this.btnTimkiem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(74, 22);
            this.btnTimkiem.Text = "Tìm kiếm";
            this.btnTimkiem.ToolTipText = "Enter";
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click);
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
            this.btntimkiemsinhvien.Size = new System.Drawing.Size(126, 22);
            this.btntimkiemsinhvien.Text = "Tìm kiếm sinh viên";
            this.btntimkiemsinhvien.Click += new System.EventHandler(this.btntimkiemsinhvien_Click);
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
            this.btnrefresh.Size = new System.Drawing.Size(71, 22);
            this.btnrefresh.Text = "Quay lại";
            this.btnrefresh.ToolTipText = "Tải lại dữ liệu";
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // pnl_from
            // 
            this.pnl_from.Controls.Add(this.dgv_DanhSach);
            this.pnl_from.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_from.Location = new System.Drawing.Point(0, 25);
            this.pnl_from.Margin = new System.Windows.Forms.Padding(5);
            this.pnl_from.Name = "pnl_from";
            this.pnl_from.Size = new System.Drawing.Size(654, 473);
            this.pnl_from.TabIndex = 30;
            this.pnl_from.Visible = false;
            // 
            // MenuStrip_suamade
            // 
            this.MenuStrip_suamade.Name = "MenuStrip_suamade";
            this.MenuStrip_suamade.Size = new System.Drawing.Size(174, 22);
            this.MenuStrip_suamade.Text = "Sửa mã đề, bài làm";
            this.MenuStrip_suamade.Click += new System.EventHandler(this.MenuStrip_suamade_Click);
            // 
            // Frm_204_DanhSachBaiLam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_from);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_204_DanhSachBaiLam";
            this.Size = new System.Drawing.Size(654, 498);
            this.Load += new System.EventHandler(this.FrmDanhSachBaiLam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.menu_ug.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnl_from.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachsinhvien;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdsbailam;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel pnl_from;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtmade;
        private System.Windows.Forms.ToolStripButton btnTimkiem;
        private System.Windows.Forms.ToolStripButton btnrefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btntimkiemsinhvien;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Suamasv;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_suamade;
    }
}
