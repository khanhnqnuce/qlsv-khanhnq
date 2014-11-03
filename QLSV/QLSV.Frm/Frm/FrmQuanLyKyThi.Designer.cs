namespace QLSV.Frm.Frm
{
    partial class FrmQuanLyKyThi
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
            this.menuStrip_luulai = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Huy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_dong = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uG_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnGhi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lbXoadong = new System.Windows.Forms.Label();
            this.lbInsert = new System.Windows.Forms.Label();
            this.menu_ug.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_ug
            // 
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_themdong,
            this.menuStrip_xoadong,
            this.menuStrip_luulai,
            this.menuStrip_Huy,
            this.menuStrip_dong});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(137, 114);
            // 
            // menuStrip_themdong
            // 
            this.menuStrip_themdong.Name = "menuStrip_themdong";
            this.menuStrip_themdong.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_themdong.Text = "Thêm dòng";
            this.menuStrip_themdong.Click += new System.EventHandler(this.menuStrip_themdong_Click);
            // 
            // menuStrip_xoadong
            // 
            this.menuStrip_xoadong.Name = "menuStrip_xoadong";
            this.menuStrip_xoadong.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_xoadong.Text = "Xóa dòng";
            this.menuStrip_xoadong.Click += new System.EventHandler(this.menuStrip_xoadong_Click);
            // 
            // menuStrip_luulai
            // 
            this.menuStrip_luulai.Name = "menuStrip_luulai";
            this.menuStrip_luulai.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_luulai.Text = "Lưu lại";
            this.menuStrip_luulai.Click += new System.EventHandler(this.menuStrip_luulai_Click);
            // 
            // menuStrip_Huy
            // 
            this.menuStrip_Huy.Name = "menuStrip_Huy";
            this.menuStrip_Huy.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_Huy.Text = "Hủy";
            this.menuStrip_Huy.Click += new System.EventHandler(this.menuStripHuy_Click);
            // 
            // menuStrip_dong
            // 
            this.menuStrip_dong.Name = "menuStrip_dong";
            this.menuStrip_dong.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_dong.Text = "Đóng";
            this.menuStrip_dong.Click += new System.EventHandler(this.menuStrip_dong_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uG_DanhSach);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 452);
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
            this.uG_DanhSach.Size = new System.Drawing.Size(1026, 452);
            this.uG_DanhSach.TabIndex = 25;
            this.uG_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.uG_DanhSach.AfterExitEditMode += new System.EventHandler(this.uG_DanhSach_AfterExitEditMode);
            this.uG_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.uG_DanhSach_BeforeRowsDeleted);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.btnXoa);
            this.panel1.Controls.Add(this.btnGhi);
            this.panel1.Controls.Add(this.btnDong);
            this.panel1.Controls.Add(this.lbXoadong);
            this.panel1.Controls.Add(this.lbInsert);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 452);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 64);
            this.panel1.TabIndex = 2;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHuy.Location = new System.Drawing.Point(779, 16);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 30);
            this.btnHuy.TabIndex = 24;
            this.btnHuy.Text = "Hủy (F12)";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXoa.Location = new System.Drawing.Point(547, 16);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 30);
            this.btnXoa.TabIndex = 20;
            this.btnXoa.Text = "Xóa dòng (F11)";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnGhi
            // 
            this.btnGhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGhi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGhi.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnGhi.Location = new System.Drawing.Point(663, 16);
            this.btnGhi.Margin = new System.Windows.Forms.Padding(4);
            this.btnGhi.Name = "btnGhi";
            this.btnGhi.Size = new System.Drawing.Size(100, 30);
            this.btnGhi.TabIndex = 21;
            this.btnGhi.Text = "Lưu (F5)";
            this.btnGhi.UseVisualStyleBackColor = true;
            this.btnGhi.Click += new System.EventHandler(this.btnGhi_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDong.Location = new System.Drawing.Point(895, 16);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(108, 30);
            this.btnDong.TabIndex = 22;
            this.btnDong.Text = "Đóng (Esc)";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // lbXoadong
            // 
            this.lbXoadong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbXoadong.AutoSize = true;
            this.lbXoadong.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbXoadong.Location = new System.Drawing.Point(12, 37);
            this.lbXoadong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbXoadong.Name = "lbXoadong";
            this.lbXoadong.Size = new System.Drawing.Size(118, 15);
            this.lbXoadong.TabIndex = 19;
            this.lbXoadong.Text = "Nhấn F11: Xóa tất cả";
            // 
            // lbInsert
            // 
            this.lbInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbInsert.AutoSize = true;
            this.lbInsert.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbInsert.Location = new System.Drawing.Point(12, 11);
            this.lbInsert.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInsert.Name = "lbInsert";
            this.lbInsert.Size = new System.Drawing.Size(137, 15);
            this.lbInsert.TabIndex = 18;
            this.lbInsert.Text = "Nhấn Insert: Thêm dòng";
            // 
            // FrmQuanLyKyThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmQuanLyKyThi";
            this.Size = new System.Drawing.Size(1026, 516);
            this.Load += new System.EventHandler(this.FrmQuanLyKyThi_Load);
            this.menu_ug.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnGhi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbXoadong;
        private System.Windows.Forms.Label lbInsert;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.UltraWinGrid.UltraGrid uG_DanhSach;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_themdong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_xoadong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_luulai;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Huy;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_dong;
    }
}