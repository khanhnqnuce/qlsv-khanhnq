namespace QLSV.Frm.Frm
{
    partial class FrmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDangNhap));
            this.ultraGroupBox_Logo = new Infragistics.Win.Misc.UltraGroupBox();
            this.pictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.ultraGroupBox_login = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.errormatkhau = new System.Windows.Forms.ErrorProvider(this.components);
            this.errortaikhoan = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox_Logo)).BeginInit();
            this.ultraGroupBox_Logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox_login)).BeginInit();
            this.ultraGroupBox_login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errormatkhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errortaikhoan)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox_Logo
            // 
            this.ultraGroupBox_Logo.Controls.Add(this.pictureBox_Logo);
            this.ultraGroupBox_Logo.Dock = System.Windows.Forms.DockStyle.Left;
            this.ultraGroupBox_Logo.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox_Logo.Name = "ultraGroupBox_Logo";
            this.ultraGroupBox_Logo.Size = new System.Drawing.Size(232, 235);
            this.ultraGroupBox_Logo.TabIndex = 19;
            // 
            // pictureBox_Logo
            // 
            this.pictureBox_Logo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Logo.Image = global::QLSV.Frm.Properties.Resources.logo1;
            this.pictureBox_Logo.Location = new System.Drawing.Point(3, 0);
            this.pictureBox_Logo.Name = "pictureBox_Logo";
            this.pictureBox_Logo.Size = new System.Drawing.Size(226, 232);
            this.pictureBox_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Logo.TabIndex = 19;
            this.pictureBox_Logo.TabStop = false;
            // 
            // ultraGroupBox_login
            // 
            this.ultraGroupBox_login.Controls.Add(this.txtTaiKhoan);
            this.ultraGroupBox_login.Controls.Add(this.txtMatKhau);
            this.ultraGroupBox_login.Controls.Add(this.lblMatKhau);
            this.ultraGroupBox_login.Controls.Add(this.lblTaiKhoan);
            this.ultraGroupBox_login.Controls.Add(this.btnDangNhap);
            this.ultraGroupBox_login.Controls.Add(this.btnThoat);
            this.ultraGroupBox_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox_login.Location = new System.Drawing.Point(232, 0);
            this.ultraGroupBox_login.Name = "ultraGroupBox_login";
            this.ultraGroupBox_login.Size = new System.Drawing.Size(340, 235);
            this.ultraGroupBox_login.TabIndex = 20;
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaiKhoan.Location = new System.Drawing.Point(101, 48);
            this.txtTaiKhoan.MaxLength = 30;
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(206, 25);
            this.txtTaiKhoan.TabIndex = 10;
            this.txtTaiKhoan.Text = "admin";
            this.txtTaiKhoan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaiKhoan_KeyDown);
            this.txtTaiKhoan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDangNhap_KeyUp);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhau.Location = new System.Drawing.Point(101, 103);
            this.txtMatKhau.MaxLength = 30;
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(206, 25);
            this.txtMatKhau.TabIndex = 11;
            this.txtMatKhau.Text = "123456";
            this.txtMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaiKhoan_KeyDown);
            this.txtMatKhau.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDangNhap_KeyUp);
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.Location = new System.Drawing.Point(18, 107);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(68, 17);
            this.lblMatKhau.TabIndex = 13;
            this.lblMatKhau.Text = "Mật Khẩu";
            this.lblMatKhau.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaiKhoan.Location = new System.Drawing.Point(16, 52);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(70, 17);
            this.lblTaiKhoan.TabIndex = 12;
            this.lblTaiKhoan.Text = "Tài Khoản";
            this.lblTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDangNhap.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.Location = new System.Drawing.Point(101, 160);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(87, 27);
            this.btnDangNhap.TabIndex = 14;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(220, 160);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 27);
            this.btnThoat.TabIndex = 15;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // errormatkhau
            // 
            this.errormatkhau.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errormatkhau.ContainerControl = this;
            this.errormatkhau.Icon = ((System.Drawing.Icon)(resources.GetObject("errormatkhau.Icon")));
            // 
            // errortaikhoan
            // 
            this.errortaikhoan.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errortaikhoan.ContainerControl = this;
            this.errortaikhoan.Icon = ((System.Drawing.Icon)(resources.GetObject("errortaikhoan.Icon")));
            // 
            // FrmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 235);
            this.ControlBox = false;
            this.Controls.Add(this.ultraGroupBox_login);
            this.Controls.Add(this.ultraGroupBox_Logo);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmDangNhap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox_Logo)).EndInit();
            this.ultraGroupBox_Logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox_login)).EndInit();
            this.ultraGroupBox_login.ResumeLayout(false);
            this.ultraGroupBox_login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errormatkhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errortaikhoan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox_Logo;
        private System.Windows.Forms.PictureBox pictureBox_Logo;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox_login;
        public System.Windows.Forms.TextBox txtTaiKhoan;
        public System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ErrorProvider errormatkhau;
        private System.Windows.Forms.ErrorProvider errortaikhoan;
    }
}