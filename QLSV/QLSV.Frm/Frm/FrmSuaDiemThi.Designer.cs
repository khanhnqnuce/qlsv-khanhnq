namespace QLSV.Frm.Frm
{
    partial class FrmSuaDiemThi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSuaDiemThi));
            this.txtmade = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtmasv = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.txtchuoi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnluu = new System.Windows.Forms.Button();
            this.btnhuy = new System.Windows.Forms.Button();
            this.errormade = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorchuoi = new System.Windows.Forms.ErrorProvider(this.components);
            this.errordiem = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtmade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmasv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errormade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorchuoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errordiem)).BeginInit();
            this.SuspendLayout();
            // 
            // txtmade
            // 
            this.txtmade.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtmade.Location = new System.Drawing.Point(128, 65);
            this.txtmade.Name = "txtmade";
            this.txtmade.Size = new System.Drawing.Size(241, 26);
            this.txtmade.TabIndex = 11;
            // 
            // txtmasv
            // 
            this.txtmasv.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtmasv.Location = new System.Drawing.Point(128, 26);
            this.txtmasv.Name = "txtmasv";
            this.txtmasv.ReadOnly = true;
            this.txtmasv.Size = new System.Drawing.Size(241, 26);
            this.txtmasv.TabIndex = 10;
            // 
            // ultraLabel2
            // 
            appearance1.TextVAlignAsString = "Middle";
            this.ultraLabel2.Appearance = appearance1;
            this.ultraLabel2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ultraLabel2.Location = new System.Drawing.Point(32, 67);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(49, 23);
            this.ultraLabel2.TabIndex = 9;
            this.ultraLabel2.Text = "Mã đề:";
            // 
            // ultraLabel1
            // 
            appearance2.TextHAlignAsString = "Left";
            appearance2.TextVAlignAsString = "Middle";
            this.ultraLabel1.Appearance = appearance2;
            this.ultraLabel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ultraLabel1.Location = new System.Drawing.Point(32, 28);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(96, 23);
            this.ultraLabel1.TabIndex = 8;
            this.ultraLabel1.Text = "Mã sinh viên:";
            // 
            // txtchuoi
            // 
            this.txtchuoi.Location = new System.Drawing.Point(128, 106);
            this.txtchuoi.Multiline = true;
            this.txtchuoi.Name = "txtchuoi";
            this.txtchuoi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtchuoi.Size = new System.Drawing.Size(241, 77);
            this.txtchuoi.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Chuỗi bài làm:";
            // 
            // btnluu
            // 
            this.btnluu.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnluu.Location = new System.Drawing.Point(128, 205);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(75, 23);
            this.btnluu.TabIndex = 14;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = true;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnhuy.Location = new System.Drawing.Point(212, 205);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(75, 23);
            this.btnhuy.TabIndex = 18;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.UseVisualStyleBackColor = true;
            // 
            // errormade
            // 
            this.errormade.ContainerControl = this;
            this.errormade.Icon = ((System.Drawing.Icon)(resources.GetObject("errormade.Icon")));
            // 
            // errorchuoi
            // 
            this.errorchuoi.ContainerControl = this;
            this.errorchuoi.Icon = ((System.Drawing.Icon)(resources.GetObject("errorchuoi.Icon")));
            // 
            // errordiem
            // 
            this.errordiem.ContainerControl = this;
            this.errordiem.Icon = ((System.Drawing.Icon)(resources.GetObject("errordiem.Icon")));
            // 
            // FrmSuaDiemThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 266);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.txtchuoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtmade);
            this.Controls.Add(this.txtmasv);
            this.Controls.Add(this.ultraLabel2);
            this.Controls.Add(this.ultraLabel1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSuaDiemThi";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa mã đề, chuỗi bài làm";
            ((System.ComponentModel.ISupportInitialize)(this.txtmade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmasv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errormade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorchuoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errordiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Infragistics.Win.UltraWinEditors.UltraTextEditor txtmade;
        public Infragistics.Win.UltraWinEditors.UltraTextEditor txtmasv;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        public System.Windows.Forms.TextBox txtchuoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.Button btnhuy;
        private System.Windows.Forms.ErrorProvider errormade;
        private System.Windows.Forms.ErrorProvider errorchuoi;
        private System.Windows.Forms.ErrorProvider errordiem;
    }
}