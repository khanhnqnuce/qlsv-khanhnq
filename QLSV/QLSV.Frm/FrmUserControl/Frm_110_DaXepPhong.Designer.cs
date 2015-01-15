namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_110_DaXepPhong
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
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters23 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters24 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_Themdong = new System.Windows.Forms.ToolStripMenuItem();
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdanhsachduthi = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.rptdanhsachkhoa = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.rptdanhsachlop = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbolop = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbokhoa = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.pnl_form = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.menu_ug.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnl_form.SuspendLayout();
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
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(947, 390);
            this.dgv_DanhSach.TabIndex = 27;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.dgv_DanhSach.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.dgv_DanhSach_DoubleClickCell);
            // 
            // menu_ug
            // 
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Themdong});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(128, 26);
            // 
            // menuStrip_Themdong
            // 
            this.menuStrip_Themdong.Name = "menuStrip_Themdong";
            this.menuStrip_Themdong.Size = new System.Drawing.Size(127, 22);
            this.menuStrip_Themdong.Text = "Chỉnh sửa";
            this.menuStrip_Themdong.Click += new System.EventHandler(this.menuStrip_Themdong_Click);
            // 
            // excelExportFilter1
            // 
            this.excelExportFilter1.ExportInLargePage = true;
            this.excelExportFilter1.ExportInOnePage = true;
            this.excelExportFilter1.ExportWithoutPageDelimeters = true;
            this.excelExportFilter1.ExtraParameters = extraParameters23;
            // 
            // pdfExportFilter1
            // 
            this.pdfExportFilter1.ChangePermissionsPassword = null;
            this.pdfExportFilter1.Compress = true;
            this.pdfExportFilter1.ExtraParameters = extraParameters24;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 24);
            this.panel3.TabIndex = 29;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cbolop);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(471, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(148, 24);
            this.panel6.TabIndex = 3;
            // 
            // cbolop
            // 
            this.cbolop.DisplayMember = "MaLop";
            this.cbolop.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbolop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbolop.FormattingEnabled = true;
            this.cbolop.Location = new System.Drawing.Point(0, 0);
            this.cbolop.Name = "cbolop";
            this.cbolop.Size = new System.Drawing.Size(148, 23);
            this.cbolop.TabIndex = 0;
            this.cbolop.ValueMember = "ID";
            this.cbolop.SelectedValueChanged += new System.EventHandler(this.cbolop_SelectedValueChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.toolStrip2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(387, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(84, 24);
            this.panel5.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripLabel2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(84, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "Chọn lớp";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbokhoa);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(97, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 24);
            this.panel2.TabIndex = 1;
            // 
            // cbokhoa
            // 
            this.cbokhoa.DisplayMember = "TenKhoa";
            this.cbokhoa.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbokhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbokhoa.FormattingEnabled = true;
            this.cbokhoa.Location = new System.Drawing.Point(0, 0);
            this.cbokhoa.Name = "cbokhoa";
            this.cbokhoa.Size = new System.Drawing.Size(290, 23);
            this.cbokhoa.TabIndex = 0;
            this.cbokhoa.ValueMember = "ID";
            this.cbokhoa.SelectedValueChanged += new System.EventHandler(this.cbokhoa_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(97, 24);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(97, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Chọn khoa";
            // 
            // pnl_form
            // 
            this.pnl_form.Controls.Add(this.dgv_DanhSach);
            this.pnl_form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_form.Location = new System.Drawing.Point(0, 24);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(947, 390);
            this.pnl_form.TabIndex = 30;
            this.pnl_form.Visible = false;
            // 
            // Frm_110_DaXepPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_form);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_110_DaXepPhong";
            this.Size = new System.Drawing.Size(947, 414);
            this.Load += new System.EventHandler(this.FrmSinhVienPhongThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.menu_ug.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnl_form.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachduthi;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachkhoa;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachlop;
        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Themdong;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cbolop;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbokhoa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel pnl_form;
    }
}
