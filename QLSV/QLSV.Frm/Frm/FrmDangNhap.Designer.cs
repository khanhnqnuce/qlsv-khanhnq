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
            this.ultraGroupBox_login = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.errormatkhau = new System.Windows.Forms.ErrorProvider(this.components);
            this.errortaikhoan = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox_login)).BeginInit();
            this.ultraGroupBox_login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errormatkhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errortaikhoan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGroupBox_login
            // 
            this.ultraGroupBox_login.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.Rectangular3D;
            this.ultraGroupBox_login.Controls.Add(this.txtTaiKhoan);
            this.ultraGroupBox_login.Controls.Add(this.txtMatKhau);
            this.ultraGroupBox_login.Controls.Add(this.lblMatKhau);
            this.ultraGroupBox_login.Controls.Add(this.lblTaiKhoan);
            this.ultraGroupBox_login.Controls.Add(this.btnDangNhap);
            this.ultraGroupBox_login.Controls.Add(this.btnThoat);
            this.ultraGroupBox_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox_login.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox_login.Name = "ultraGroupBox_login";
            this.ultraGroupBox_login.Size = new System.Drawing.Size(325, 211);
            this.ultraGroupBox_login.TabIndex = 20;
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaiKhoan.Location = new System.Drawing.Point(107, 45);
            this.txtTaiKhoan.MaxLength = 30;
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(173, 25);
            this.txtTaiKhoan.TabIndex = 10;
            this.txtTaiKhoan.Text = "admin";
            this.txtTaiKhoan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaiKhoan_KeyDown);
            this.txtTaiKhoan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDangNhap_KeyUp);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhau.Location = new System.Drawing.Point(107, 93);
            this.txtMatKhau.MaxLength = 30;
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(173, 25);
            this.txtMatKhau.TabIndex = 11;
            this.txtMatKhau.Text = "123456";
            this.txtMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaiKhoan_KeyDown);
            this.txtMatKhau.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDangNhap_KeyUp);
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.Location = new System.Drawing.Point(24, 97);
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
            this.lblTaiKhoan.Location = new System.Drawing.Point(22, 49);
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
            this.btnDangNhap.Location = new System.Drawing.Point(107, 139);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(85, 27);
            this.btnDangNhap.TabIndex = 14;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(195, 139);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(85, 27);
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
            // pictureBox_Logo
            // 
            this.pictureBox_Logo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox_Logo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Logo.Image")));
            this.pictureBox_Logo.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Logo.Name = "pictureBox_Logo";
            this.pictureBox_Logo.Size = new System.Drawing.Size(205, 211);
            this.pictureBox_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Logo.TabIndex = 21;
            this.pictureBox_Logo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraGroupBox_login);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(205, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 211);
            this.panel1.TabIndex = 22;
            // 
            // FrmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(530, 211);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox_Logo);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmDangNhap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox_login)).EndInit();
            this.ultraGroupBox_login.ResumeLayout(false);
            this.ultraGroupBox_login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errormatkhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errortaikhoan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox_login;
        public System.Windows.Forms.TextBox txtTaiKhoan;
        public System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.Button btnThoat;
        public System.Windows.Forms.ErrorProvider errormatkhau;
        public System.Windows.Forms.ErrorProvider errortaikhoan;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.PictureBox pictureBox_Logo;
        private System.Windows.Forms.Panel panel1;
    }
}