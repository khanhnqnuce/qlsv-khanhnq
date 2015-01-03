namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_201_InportDapAn
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
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_Inport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Themmoi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Xoadong = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.menu_ug.SuspendLayout();
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
            this.dgv_DanhSach.Size = new System.Drawing.Size(447, 290);
            this.dgv_DanhSach.TabIndex = 27;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.dgv_DanhSach_InitializeLayout);
            // 
            // menu_ug
            // 
            this.menu_ug.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Inport,
            this.menuStrip_Themmoi,
            this.menuStrip_Xoadong});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(150, 70);
            // 
            // menuStrip_Inport
            // 
            this.menuStrip_Inport.Name = "menuStrip_Inport";
            this.menuStrip_Inport.Size = new System.Drawing.Size(149, 22);
            this.menuStrip_Inport.Text = "Import đáp án";
            this.menuStrip_Inport.Click += new System.EventHandler(this.menuStrip_Inport_Click);
            // 
            // menuStrip_Themmoi
            // 
            this.menuStrip_Themmoi.Name = "menuStrip_Themmoi";
            this.menuStrip_Themmoi.Size = new System.Drawing.Size(149, 22);
            this.menuStrip_Themmoi.Text = "Thêm mới";
            this.menuStrip_Themmoi.Click += new System.EventHandler(this.menuStrip_Themmoi_Click);
            // 
            // menuStrip_Xoadong
            // 
            this.menuStrip_Xoadong.Name = "menuStrip_Xoadong";
            this.menuStrip_Xoadong.Size = new System.Drawing.Size(149, 22);
            this.menuStrip_Xoadong.Text = "Xóa dòng";
            this.menuStrip_Xoadong.Click += new System.EventHandler(this.menuStrip_Xoadong_Click);
            // 
            // Frm_201_InportDapAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_DanhSach);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_201_InportDapAn";
            this.Size = new System.Drawing.Size(447, 290);
            this.Load += new System.EventHandler(this.FrmInportDapAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.menu_ug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Themmoi;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Xoadong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Inport;
    }
}
