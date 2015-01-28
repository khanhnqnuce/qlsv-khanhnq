namespace QLSV.Frm.Frm
{
    partial class FrmSuaLop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSuaLop));
            this.cbokhoa = new System.Windows.Forms.ComboBox();
            this.txtlop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnluulai = new System.Windows.Forms.Button();
            this.errorlop = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorkhoa = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorlop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorkhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // cbokhoa
            // 
            this.cbokhoa.DisplayMember = "TenKhoa";
            this.cbokhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbokhoa.FormattingEnabled = true;
            this.cbokhoa.Location = new System.Drawing.Point(106, 24);
            this.cbokhoa.Name = "cbokhoa";
            this.cbokhoa.Size = new System.Drawing.Size(218, 23);
            this.cbokhoa.TabIndex = 1;
            this.cbokhoa.ValueMember = "ID";
            // 
            // txtlop
            // 
            this.txtlop.Location = new System.Drawing.Point(106, 74);
            this.txtlop.Name = "txtlop";
            this.txtlop.Size = new System.Drawing.Size(218, 22);
            this.txtlop.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chọn khoa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nhập tên lớp:";
            // 
            // btnluulai
            // 
            this.btnluulai.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnluulai.Location = new System.Drawing.Point(135, 127);
            this.btnluulai.Name = "btnluulai";
            this.btnluulai.Size = new System.Drawing.Size(75, 23);
            this.btnluulai.TabIndex = 4;
            this.btnluulai.Text = "Lưu lại";
            this.btnluulai.UseVisualStyleBackColor = true;
            this.btnluulai.Click += new System.EventHandler(this.btnluulai_Click);
            // 
            // errorlop
            // 
            this.errorlop.ContainerControl = this;
            this.errorlop.Icon = ((System.Drawing.Icon)(resources.GetObject("errorlop.Icon")));
            // 
            // errorkhoa
            // 
            this.errorkhoa.ContainerControl = this;
            this.errorkhoa.Icon = ((System.Drawing.Icon)(resources.GetObject("errorkhoa.Icon")));
            // 
            // FrmSuaLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 174);
            this.Controls.Add(this.btnluulai);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtlop);
            this.Controls.Add(this.cbokhoa);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSuaLop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa thông tin lớp";
            this.Load += new System.EventHandler(this.FrmSuaLop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorlop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorkhoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbokhoa;
        public System.Windows.Forms.TextBox txtlop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnluulai;
        private System.Windows.Forms.ErrorProvider errorlop;
        private System.Windows.Forms.ErrorProvider errorkhoa;
    }
}