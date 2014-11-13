namespace QLSV.Frm.FrmUserControl
{
    partial class FrmSapxepphongthi
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
            this.uG_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
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
            this.uG_DanhSach.Size = new System.Drawing.Size(1035, 565);
            this.uG_DanhSach.TabIndex = 26;
            this.uG_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.uG_DanhSach.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.uG_DanhSach_DoubleClickCell);
            // 
            // pnl_form
            // 
            this.pnl_form.Controls.Add(this.uG_DanhSach);
            this.pnl_form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_form.Location = new System.Drawing.Point(0, 0);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(1035, 565);
            this.pnl_form.TabIndex = 27;
            this.pnl_form.Visible = false;
            // 
            // FrmSapxepphongthi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_form);
            this.Name = "FrmSapxepphongthi";
            this.Size = new System.Drawing.Size(1035, 565);
            this.Load += new System.EventHandler(this.Sapxepphongthi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).EndInit();
            this.pnl_form.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid uG_DanhSach;
        private System.Windows.Forms.Panel pnl_form;


    }
}
