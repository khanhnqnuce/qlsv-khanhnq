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
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.rptdiemthi = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.rptdanhsachsinhvien = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.fileReportSlot2 = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.pnl_from = new System.Windows.Forms.Panel();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.pdfExportFilter2 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.pnl_from.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // excelExportFilter1
            // 
            this.excelExportFilter1.ExportInLargePage = true;
            this.excelExportFilter1.ExportInOnePage = true;
            this.excelExportFilter1.ExportWithoutPageDelimeters = true;
            this.excelExportFilter1.ExtraParameters = extraParameters1;
            // 
            // rptdiemthi
            // 
            this.rptdiemthi.FilePath = "";
            this.rptdiemthi.ReportName = "";
            this.rptdiemthi.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdiemthi});
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
            // fileReportSlot2
            // 
            this.fileReportSlot2.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachsinhvien.rst";
            this.fileReportSlot2.ReportName = "";
            this.fileReportSlot2.ReportScriptType = null;
            // 
            // pnl_from
            // 
            this.pnl_from.Controls.Add(this.dgv_DanhSach);
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
            this.dgv_DanhSach.Size = new System.Drawing.Size(626, 451);
            this.dgv_DanhSach.TabIndex = 33;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.dgv_DanhSach_InitializeLayout);
            this.dgv_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.dgv_DanhSach_BeforeRowsDeleted);
            // 
            // pdfExportFilter2
            // 
            this.pdfExportFilter2.ChangePermissionsPassword = null;
            this.pdfExportFilter2.Compress = true;
            this.pdfExportFilter2.ExtraParameters = extraParameters3;
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
            this.pnl_from.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdiemthi;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachsinhvien;
        private PerpetuumSoft.Reporting.Components.FileReportSlot fileReportSlot2;
        private System.Windows.Forms.Panel pnl_from;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter2;
        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
    }
}
