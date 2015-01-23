namespace QLSV.Frm.Frm
{
    partial class FrmSuaMaSinhVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSuaMaSinhVien));
            this.txtmasinhvien = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.errorMaSinhVien = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnluu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtmasinhvien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMaSinhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // txtmasinhvien
            // 
            this.txtmasinhvien.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtmasinhvien.Location = new System.Drawing.Point(115, 19);
            this.txtmasinhvien.Name = "txtmasinhvien";
            this.txtmasinhvien.Size = new System.Drawing.Size(197, 26);
            this.txtmasinhvien.TabIndex = 34;
            this.txtmasinhvien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtmasinhvien_KeyPress);
            // 
            // ultraLabel1
            // 
            appearance1.TextHAlignAsString = "Left";
            appearance1.TextVAlignAsString = "Middle";
            this.ultraLabel1.Appearance = appearance1;
            this.ultraLabel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ultraLabel1.Location = new System.Drawing.Point(19, 21);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(90, 23);
            this.ultraLabel1.TabIndex = 33;
            this.ultraLabel1.Text = "Nhập mã sv:";
            // 
            // errorMaSinhVien
            // 
            this.errorMaSinhVien.ContainerControl = this;
            this.errorMaSinhVien.Icon = ((System.Drawing.Icon)(resources.GetObject("errorMaSinhVien.Icon")));
            // 
            // btnluu
            // 
            this.btnluu.Location = new System.Drawing.Point(129, 59);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(75, 23);
            this.btnluu.TabIndex = 35;
            this.btnluu.Text = "Lưu lại";
            this.btnluu.UseVisualStyleBackColor = true;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // FrmSuaMaSinhVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(331, 101);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.txtmasinhvien);
            this.Controls.Add(this.ultraLabel1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSuaMaSinhVien";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa mã sinh viên";
            ((System.ComponentModel.ISupportInitialize)(this.txtmasinhvien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMaSinhVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Infragistics.Win.UltraWinEditors.UltraTextEditor txtmasinhvien;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private System.Windows.Forms.ErrorProvider errorMaSinhVien;
        private System.Windows.Forms.Button btnluu;
    }
}