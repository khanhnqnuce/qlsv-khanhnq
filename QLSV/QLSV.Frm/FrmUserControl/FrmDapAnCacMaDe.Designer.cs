namespace QLSV.Frm.FrmUserControl
{
    partial class FrmDapAnCacMaDe
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
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_Themdong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Inport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Huy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_dong = new System.Windows.Forms.ToolStripMenuItem();
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdanhsachsinhvien = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnl_from = new System.Windows.Forms.Panel();
            this.menu_ug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnl_from.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_ug
            // 
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Themdong,
            this.menuStrip_Inport,
            this.toolStripMenuItem2,
            this.menuStrip_Huy,
            this.menuStrip_dong});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(130, 114);
            // 
            // menuStrip_Themdong
            // 
            this.menuStrip_Themdong.Name = "menuStrip_Themdong";
            this.menuStrip_Themdong.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_Themdong.Text = "Thêm mới";
            // 
            // menuStrip_Inport
            // 
            this.menuStrip_Inport.Name = "menuStrip_Inport";
            this.menuStrip_Inport.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_Inport.Text = "Chỉnh sửa";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem2.Text = "Xóa dòng";
            // 
            // menuStrip_Huy
            // 
            this.menuStrip_Huy.Name = "menuStrip_Huy";
            this.menuStrip_Huy.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_Huy.Text = "Hủy";
            // 
            // menuStrip_dong
            // 
            this.menuStrip_dong.Name = "menuStrip_dong";
            this.menuStrip_dong.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_dong.Text = "Đóng";
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdanhsachsinhvien});
            // 
            // rptdanhsachsinhvien
            // 
            this.rptdanhsachsinhvien.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachsinhvien.rst";
            this.rptdanhsachsinhvien.ReportName = "";
            this.rptdanhsachsinhvien.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
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
            // dgv_DanhSach
            // 
            this.dgv_DanhSach.ContextMenuStrip = this.menu_ug;
            this.dgv_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.dgv_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.dgv_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dgv_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dgv_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.dgv_DanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(752, 564);
            this.dgv_DanhSach.TabIndex = 25;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.dgv_DanhSach_InitializeLayout);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_DanhSach);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(752, 564);
            this.panel4.TabIndex = 27;
            // 
            // pnl_from
            // 
            this.pnl_from.Controls.Add(this.panel4);
            this.pnl_from.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_from.Location = new System.Drawing.Point(0, 0);
            this.pnl_from.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_from.Name = "pnl_from";
            this.pnl_from.Size = new System.Drawing.Size(752, 564);
            this.pnl_from.TabIndex = 7;
            this.pnl_from.Visible = false;
            // 
            // FrmDapAnCacMaDe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_from);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmDapAnCacMaDe";
            this.Size = new System.Drawing.Size(752, 564);
            this.Load += new System.EventHandler(this.FrmDapAnCacMaDe_Load);
            this.menu_ug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.panel4.ResumeLayout(false);
            this.pnl_from.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Themdong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Inport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Huy;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_dong;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachsinhvien;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnl_from;

    }
}
