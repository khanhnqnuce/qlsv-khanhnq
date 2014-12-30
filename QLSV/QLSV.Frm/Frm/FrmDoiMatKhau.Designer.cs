namespace QLSV.Frm.Frm
{
    partial class FrmDoiMatKhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDoiMatKhau));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMK1 = new System.Windows.Forms.TextBox();
            this.txtMK2 = new System.Windows.Forms.TextBox();
            this.txtMK3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.errorcu = new System.Windows.Forms.ErrorProvider(this.components);
            this.errormoi = new System.Windows.Forms.ErrorProvider(this.components);
            this.errornhaplai = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorcu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errormoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errornhaplai)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu cũ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(20, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu mới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(20, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nhập lại MK";
            // 
            // txtMK1
            // 
            this.txtMK1.Location = new System.Drawing.Point(130, 14);
            this.txtMK1.Name = "txtMK1";
            this.txtMK1.PasswordChar = '*';
            this.txtMK1.Size = new System.Drawing.Size(192, 22);
            this.txtMK1.TabIndex = 3;
            // 
            // txtMK2
            // 
            this.txtMK2.Location = new System.Drawing.Point(130, 46);
            this.txtMK2.Name = "txtMK2";
            this.txtMK2.PasswordChar = '*';
            this.txtMK2.Size = new System.Drawing.Size(192, 22);
            this.txtMK2.TabIndex = 4;
            // 
            // txtMK3
            // 
            this.txtMK3.Location = new System.Drawing.Point(130, 78);
            this.txtMK3.Name = "txtMK3";
            this.txtMK3.PasswordChar = '*';
            this.txtMK3.Size = new System.Drawing.Size(192, 22);
            this.txtMK3.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Đồng ý";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(230, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Đóng";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorcu
            // 
            this.errorcu.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorcu.ContainerControl = this;
            this.errorcu.Icon = ((System.Drawing.Icon)(resources.GetObject("errorcu.Icon")));
            // 
            // errormoi
            // 
            this.errormoi.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errormoi.ContainerControl = this;
            this.errormoi.Icon = ((System.Drawing.Icon)(resources.GetObject("errormoi.Icon")));
            // 
            // errornhaplai
            // 
            this.errornhaplai.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errornhaplai.ContainerControl = this;
            this.errornhaplai.Icon = ((System.Drawing.Icon)(resources.GetObject("errornhaplai.Icon")));
            // 
            // FrmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 149);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMK3);
            this.Controls.Add(this.txtMK2);
            this.Controls.Add(this.txtMK1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDoiMatKhau";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu";
            ((System.ComponentModel.ISupportInitialize)(this.errorcu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errormoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errornhaplai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMK1;
        private System.Windows.Forms.TextBox txtMK2;
        public System.Windows.Forms.TextBox txtMK3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ErrorProvider errorcu;
        public System.Windows.Forms.ErrorProvider errormoi;
        public System.Windows.Forms.ErrorProvider errornhaplai;
    }
}