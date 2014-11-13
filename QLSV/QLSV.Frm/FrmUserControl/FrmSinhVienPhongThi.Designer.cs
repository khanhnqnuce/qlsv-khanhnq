namespace QLSV.Frm.FrmUserControl
{
    partial class FrmSinhVienPhongThi
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
            this.uG_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdanhsachduthi = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.rptdanhsachkhoa = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.rptdanhsachlop = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.pnl_form = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).BeginInit();
            this.pnl_form.SuspendLayout();
            this.SuspendLayout();
            // 
            // uG_DanhSach
            // 
            this.uG_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.uG_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.uG_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uG_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uG_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uG_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.uG_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.uG_DanhSach.Name = "uG_DanhSach";
            this.uG_DanhSach.Size = new System.Drawing.Size(997, 589);
            this.uG_DanhSach.TabIndex = 27;
            this.uG_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
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
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdanhsachduthi,
            this.rptdanhsachkhoa,
            this.rptdanhsachlop});
            // 
            // rptdanhsachduthi
            // 
            this.rptdanhsachduthi.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachduthi.rst";
            this.rptdanhsachduthi.ReportName = "";
            this.rptdanhsachduthi.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // rptdanhsachkhoa
            // 
            this.rptdanhsachkhoa.FilePath = "";
            this.rptdanhsachkhoa.ReportName = "";
            this.rptdanhsachkhoa.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // rptdanhsachlop
            // 
            this.rptdanhsachlop.FilePath = "";
            this.rptdanhsachlop.ReportName = "";
            this.rptdanhsachlop.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // pnl_form
            // 
            this.pnl_form.Controls.Add(this.uG_DanhSach);
            this.pnl_form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_form.Location = new System.Drawing.Point(0, 0);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(997, 589);
            this.pnl_form.TabIndex = 28;
            this.pnl_form.Visible = false;
            // 
            // FrmSinhVienPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_form);
            this.Name = "FrmSinhVienPhongThi";
            this.Size = new System.Drawing.Size(997, 589);
            this.Load += new System.EventHandler(this.FrmSinhVienPhongThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).EndInit();
            this.pnl_form.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid uG_DanhSach;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachduthi;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachkhoa;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachlop;
        private System.Windows.Forms.Panel pnl_form;
    }
}
