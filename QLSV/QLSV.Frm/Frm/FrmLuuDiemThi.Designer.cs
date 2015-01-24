namespace QLSV.Frm.Frm
{
    partial class FrmLuuDiemThi
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
            this.btnOk = new System.Windows.Forms.Button();
            this.rdokythi = new System.Windows.Forms.RadioButton();
            this.rdonamhoc = new System.Windows.Forms.RadioButton();
            this.rdolucahai = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(90, 138);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rdokythi
            // 
            this.rdokythi.AutoSize = true;
            this.rdokythi.Checked = true;
            this.rdokythi.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdokythi.Location = new System.Drawing.Point(15, 25);
            this.rdokythi.Name = "rdokythi";
            this.rdokythi.Size = new System.Drawing.Size(119, 21);
            this.rdokythi.TabIndex = 21;
            this.rdokythi.Text = "Lưu điểm kỳ thi";
            this.rdokythi.UseVisualStyleBackColor = true;
            // 
            // rdonamhoc
            // 
            this.rdonamhoc.AutoSize = true;
            this.rdonamhoc.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdonamhoc.Location = new System.Drawing.Point(15, 62);
            this.rdonamhoc.Name = "rdonamhoc";
            this.rdonamhoc.Size = new System.Drawing.Size(224, 21);
            this.rdonamhoc.TabIndex = 21;
            this.rdonamhoc.Text = "Lưu điểm thi vào học kỳ-năm học";
            this.rdonamhoc.UseVisualStyleBackColor = true;
            // 
            // rdolucahai
            // 
            this.rdolucahai.AutoSize = true;
            this.rdolucahai.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdolucahai.Location = new System.Drawing.Point(15, 99);
            this.rdolucahai.Name = "rdolucahai";
            this.rdolucahai.Size = new System.Drawing.Size(135, 21);
            this.rdolucahai.TabIndex = 21;
            this.rdolucahai.Text = "Cả 2 lựa chọn trên";
            this.rdolucahai.UseVisualStyleBackColor = true;
            // 
            // FrmLuuDiemThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 187);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rdolucahai);
            this.Controls.Add(this.rdonamhoc);
            this.Controls.Add(this.rdokythi);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLuuDiemThi";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lưu điểm thi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.RadioButton rdokythi;
        public System.Windows.Forms.RadioButton rdonamhoc;
        public System.Windows.Forms.RadioButton rdolucahai;
    }
}