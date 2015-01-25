namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_106_QuanLyKyThi
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
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_themdong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_xoadong = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uG_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu_ug.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).BeginInit();
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
            // panel2
            // 
            this.panel2.Controls.Add(this.uG_DanhSach);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 285);
            this.panel2.TabIndex = 4;
            // 
            // uG_DanhSach
            // 
            this.uG_DanhSach.ContextMenuStrip = this.menu_ug;
            this.uG_DanhSach.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.uG_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.uG_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.uG_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uG_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uG_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uG_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.uG_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.uG_DanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.uG_DanhSach.Name = "uG_DanhSach";
            this.uG_DanhSach.Size = new System.Drawing.Size(321, 285);
            this.uG_DanhSach.TabIndex = 25;
            this.uG_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.uG_DanhSach.AfterExitEditMode += new System.EventHandler(this.uG_DanhSach_AfterExitEditMode);
            this.uG_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.uG_DanhSach_BeforeRowsDeleted);
            this.uG_DanhSach.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.uG_DanhSach_ClickCell);
            // 
            // Frm_106_QuanLyKyThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_106_QuanLyKyThi";
            this.Size = new System.Drawing.Size(321, 285);
            this.Load += new System.EventHandler(this.FrmQuanLyKyThi_Load);
            this.menu_ug.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.UltraWinGrid.UltraGrid uG_DanhSach;
        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_themdong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_xoadong;
    }
}