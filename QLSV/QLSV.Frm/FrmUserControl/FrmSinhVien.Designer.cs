namespace QLSV.Frm.FrmUserControl
{
    partial class FrmSinhVien
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters1 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters2 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_Themdong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Inport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Huy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_dong = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_from = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbolop = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.cbokhoa = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.lbLop = new Infragistics.Win.Misc.UltraLabel();
            this.lbkhoa = new Infragistics.Win.Misc.UltraLabel();
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdanhsachsinhvien = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.menu_ug.SuspendLayout();
            this.pnl_from.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbolop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbokhoa)).BeginInit();
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
            this.menuStrip_Themdong.Click += new System.EventHandler(this.menuStrip_themdong_Click);
            // 
            // menuStrip_Inport
            // 
            this.menuStrip_Inport.Name = "menuStrip_Inport";
            this.menuStrip_Inport.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_Inport.Text = "Chỉnh sửa";
            this.menuStrip_Inport.Click += new System.EventHandler(this.menuStrip_Sua_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem2.Text = "Xóa dòng";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.menuStrip_xoadong_Click);
            // 
            // menuStrip_Huy
            // 
            this.menuStrip_Huy.Name = "menuStrip_Huy";
            this.menuStrip_Huy.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_Huy.Text = "Hủy";
            this.menuStrip_Huy.Click += new System.EventHandler(this.menuStripHuy_Click);
            // 
            // menuStrip_dong
            // 
            this.menuStrip_dong.Name = "menuStrip_dong";
            this.menuStrip_dong.Size = new System.Drawing.Size(129, 22);
            this.menuStrip_dong.Text = "Đóng";
            this.menuStrip_dong.Click += new System.EventHandler(this.menuStrip_dong_Click);
            // 
            // pnl_from
            // 
            this.pnl_from.Controls.Add(this.panel4);
            this.pnl_from.Controls.Add(this.panel3);
            this.pnl_from.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_from.Location = new System.Drawing.Point(0, 0);
            this.pnl_from.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_from.Name = "pnl_from";
            this.pnl_from.Size = new System.Drawing.Size(1215, 565);
            this.pnl_from.TabIndex = 6;
            this.pnl_from.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_DanhSach);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1215, 531);
            this.panel4.TabIndex = 27;
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
            this.dgv_DanhSach.Size = new System.Drawing.Size(1215, 531);
            this.dgv_DanhSach.TabIndex = 25;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.dgv_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.uG_DanhSach_BeforeRowsDeleted);
            this.dgv_DanhSach.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.uG_DanhSach_DoubleClickCell);
            this.dgv_DanhSach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uG_DanhSach_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbolop);
            this.panel3.Controls.Add(this.cbokhoa);
            this.panel3.Controls.Add(this.lbLop);
            this.panel3.Controls.Add(this.lbkhoa);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1215, 34);
            this.panel3.TabIndex = 26;
            // 
            // cbolop
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbolop.DisplayLayout.Appearance = appearance1;
            this.cbolop.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cbolop.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.cbolop.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbolop.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.cbolop.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbolop.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.cbolop.DisplayLayout.MaxColScrollRegions = 1;
            this.cbolop.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbolop.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cbolop.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.cbolop.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cbolop.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.cbolop.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cbolop.DisplayLayout.Override.CellAppearance = appearance8;
            this.cbolop.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cbolop.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cbolop.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.cbolop.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.cbolop.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cbolop.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.cbolop.DisplayLayout.Override.RowAppearance = appearance11;
            this.cbolop.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbolop.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.cbolop.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cbolop.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cbolop.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cbolop.Location = new System.Drawing.Point(704, 4);
            this.cbolop.Name = "cbolop";
            this.cbolop.Size = new System.Drawing.Size(189, 25);
            this.cbolop.TabIndex = 3;
            this.cbolop.ValueChanged += new System.EventHandler(this.cbolop_ValueChanged);
            this.cbolop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbolop_KeyUp);
            // 
            // cbokhoa
            // 
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbokhoa.DisplayLayout.Appearance = appearance13;
            this.cbokhoa.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cbokhoa.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.cbokhoa.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbokhoa.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.cbokhoa.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbokhoa.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.cbokhoa.DisplayLayout.MaxColScrollRegions = 1;
            this.cbokhoa.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbokhoa.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cbokhoa.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.cbokhoa.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cbokhoa.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.cbokhoa.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cbokhoa.DisplayLayout.Override.CellAppearance = appearance20;
            this.cbokhoa.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cbokhoa.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.cbokhoa.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.cbokhoa.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.cbokhoa.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cbokhoa.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.cbokhoa.DisplayLayout.Override.RowAppearance = appearance23;
            this.cbokhoa.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbokhoa.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.cbokhoa.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cbokhoa.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cbokhoa.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cbokhoa.Location = new System.Drawing.Point(407, 4);
            this.cbokhoa.Name = "cbokhoa";
            this.cbokhoa.Size = new System.Drawing.Size(189, 25);
            this.cbokhoa.TabIndex = 2;
            this.cbokhoa.ValueChanged += new System.EventHandler(this.cbokhoa_ValueChanged);
            this.cbokhoa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbokhoa_KeyUp);
            // 
            // lbLop
            // 
            appearance25.TextHAlignAsString = "Right";
            appearance25.TextVAlignAsString = "Middle";
            this.lbLop.Appearance = appearance25;
            this.lbLop.Location = new System.Drawing.Point(596, 5);
            this.lbLop.Name = "lbLop";
            this.lbLop.Size = new System.Drawing.Size(100, 23);
            this.lbLop.TabIndex = 1;
            this.lbLop.Text = "Chọn Lớp:";
            // 
            // lbkhoa
            // 
            appearance26.TextHAlignAsString = "Right";
            appearance26.TextVAlignAsString = "Middle";
            this.lbkhoa.Appearance = appearance26;
            this.lbkhoa.Location = new System.Drawing.Point(321, 5);
            this.lbkhoa.Name = "lbkhoa";
            this.lbkhoa.Size = new System.Drawing.Size(78, 23);
            this.lbkhoa.TabIndex = 0;
            this.lbkhoa.Text = "Chọn Khoa :";
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
            // FrmSinhVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnl_from);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmSinhVien";
            this.Size = new System.Drawing.Size(1215, 565);
            this.Load += new System.EventHandler(this.FrmSinhVien_Load);
            this.menu_ug.ResumeLayout(false);
            this.pnl_from.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbolop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbokhoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private System.Windows.Forms.Panel pnl_from;
        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Themdong;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Huy;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_dong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Inport;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private Infragistics.Win.UltraWinGrid.UltraCombo cbolop;
        private Infragistics.Win.UltraWinGrid.UltraCombo cbokhoa;
        private Infragistics.Win.Misc.UltraLabel lbLop;
        private Infragistics.Win.Misc.UltraLabel lbkhoa;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachsinhvien;
    }
}