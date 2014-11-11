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
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).BeginInit();
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
            this.uG_DanhSach.Size = new System.Drawing.Size(892, 565);
            this.uG_DanhSach.TabIndex = 26;
            this.uG_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            // 
            // FrmSapxepphongthi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uG_DanhSach);
            this.Name = "FrmSapxepphongthi";
            this.Size = new System.Drawing.Size(892, 565);
            this.Load += new System.EventHandler(this.Sapxepphongthi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uG_DanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid uG_DanhSach;


    }
}
