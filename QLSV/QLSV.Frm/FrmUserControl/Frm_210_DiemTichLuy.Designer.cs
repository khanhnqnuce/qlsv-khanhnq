namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_210_DiemTichLuy
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
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdsdiemtheokhoa = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.rptdsdiemtheolop = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.rptbangdiem = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cbothongke = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtKhoa = new System.Windows.Forms.ToolStripTextBox();
            this.btntimkiem = new System.Windows.Forms.ToolStripButton();
            this.lbsiso = new System.Windows.Forms.ToolStripLabel();
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
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdsdiemtheokhoa,
            this.rptdsdiemtheolop,
            this.rptbangdiem});
            // 
            // rptdsdiemtheokhoa
            // 
            this.rptdsdiemtheokhoa.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachduthi.rst";
            this.rptdsdiemtheokhoa.ReportName = "";
            this.rptdsdiemtheokhoa.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // rptdsdiemtheolop
            // 
            this.rptdsdiemtheolop.FilePath = "";
            this.rptdsdiemtheolop.ReportName = "";
            this.rptdsdiemtheolop.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // rptbangdiem
            // 
            this.rptbangdiem.FilePath = "";
            this.rptbangdiem.ReportName = "";
            this.rptbangdiem.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
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
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_DanhSach);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1091, 443);
            this.panel4.TabIndex = 36;
            // 
            // dgv_DanhSach
            // 
            this.dgv_DanhSach.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.dgv_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.dgv_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.dgv_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dgv_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dgv_DanhSach.DisplayLayout.UseFixedHeaders = true;
            this.dgv_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.dgv_DanhSach.Margin = new System.Windows.Forms.Padding(6);
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(1091, 443);
            this.dgv_DanhSach.TabIndex = 34;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.dgv_DanhSach_InitializeLayout);
            this.dgv_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.dgv_DanhSach_BeforeRowsDeleted);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1091, 23);
            this.panel3.TabIndex = 35;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.cbothongke);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(849, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(181, 23);
            this.panel9.TabIndex = 34;
            // 
            // cbothongke
            // 
            this.cbothongke.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbothongke.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbothongke.FormattingEnabled = true;
            this.cbothongke.Items.AddRange(new object[] {
            "- Tất cả - ",
            "x < 200",
            "200 <= x < 250",
            "x < 250",
            "250 <= x < 300",
            "x < 300",
            "300 <= x < 375",
            "x < 375",
            "375 <= x < 450",
            "x < 450",
            "x >= 450"});
            this.cbothongke.Location = new System.Drawing.Point(0, 0);
            this.cbothongke.Name = "cbothongke";
            this.cbothongke.Size = new System.Drawing.Size(181, 23);
            this.cbothongke.TabIndex = 0;
            this.cbothongke.SelectedIndexChanged += new System.EventHandler(this.cbothongke_SelectedIndexChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.toolStrip4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(750, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(99, 23);
            this.panel8.TabIndex = 33;
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.toolStripLabel4});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(99, 25);
            this.toolStrip4.TabIndex = 31;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel4.Text = "Thống kê điểm";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.toolStrip3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(531, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(219, 23);
            this.panel7.TabIndex = 4;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.txtKhoa,
            this.btntimkiem,
            this.lbsiso});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(219, 25);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel3.Text = "Chọn khóa: K";
            // 
            // txtKhoa
            // 
            this.txtKhoa.Name = "txtKhoa";
            this.txtKhoa.Size = new System.Drawing.Size(50, 25);
            this.txtKhoa.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtkhoa_KeyDown);
            this.txtKhoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkhoa_KeyPress);
            this.txtKhoa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtkhoa_KeyUp);
            // 
            // btntimkiem
            // 
            this.btntimkiem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
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
            // panel6
            // 
            this.panel6.Controls.Add(this.cbolop);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(404, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(127, 23);
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
            this.cbolop.Size = new System.Drawing.Size(127, 23);
            this.cbolop.TabIndex = 0;
            this.cbolop.ValueMember = "ID";
            this.cbolop.SelectedValueChanged += new System.EventHandler(this.cbolop_SelectedValueChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.toolStrip2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(332, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(72, 23);
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
            this.toolStrip2.Size = new System.Drawing.Size(72, 25);
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
            this.toolStripLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "Chọn lớp";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbokhoa);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(83, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 23);
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
            this.cbokhoa.Size = new System.Drawing.Size(249, 23);
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
            this.panel1.Size = new System.Drawing.Size(83, 23);
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
            this.toolStrip1.Size = new System.Drawing.Size(83, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Chọn khoa";
            // 
            // Frm_210_DiemTichLuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_210_DiemTichLuy";
            this.Size = new System.Drawing.Size(1091, 466);
            this.Load += new System.EventHandler(this.Frm_210_DiemTichLuy_Load);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
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
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdsdiemtheokhoa;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdsdiemtheolop;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptbangdiem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txtKhoa;
        private System.Windows.Forms.ToolStripButton btntimkiem;
        private System.Windows.Forms.ToolStripLabel lbsiso;
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox cbothongke;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
