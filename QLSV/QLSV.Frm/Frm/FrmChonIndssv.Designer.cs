namespace QLSV.Frm.Frm
{
    partial class FrmChonIndssv
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
            this.rdoLop = new System.Windows.Forms.RadioButton();
            this.rdokhoa = new System.Windows.Forms.RadioButton();
            this.rdoPhongthi = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoLop
            // 
            this.rdoLop.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdoLop.Location = new System.Drawing.Point(36, 93);
            this.rdoLop.Name = "rdoLop";
            this.rdoLop.Size = new System.Drawing.Size(95, 19);
            this.rdoLop.TabIndex = 19;
            this.rdoLop.Text = "Theo Lớp";
            this.rdoLop.UseVisualStyleBackColor = true;
            // 
            // rdokhoa
            // 
            this.rdokhoa.AutoSize = true;
            this.rdokhoa.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdokhoa.Location = new System.Drawing.Point(36, 58);
            this.rdokhoa.Name = "rdokhoa";
            this.rdokhoa.Size = new System.Drawing.Size(92, 21);
            this.rdokhoa.TabIndex = 18;
            this.rdokhoa.Text = "Theo Khoa";
            this.rdokhoa.UseVisualStyleBackColor = true;
            // 
            // rdoPhongthi
            // 
            this.rdoPhongthi.AutoSize = true;
            this.rdoPhongthi.Checked = true;
            this.rdoPhongthi.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdoPhongthi.Location = new System.Drawing.Point(36, 23);
            this.rdoPhongthi.Name = "rdoPhongthi";
            this.rdoPhongthi.Size = new System.Drawing.Size(113, 21);
            this.rdoPhongthi.TabIndex = 17;
            this.rdoPhongthi.TabStop = true;
            this.rdoPhongthi.Text = "Theo phòng thi";
            this.rdoPhongthi.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.rdoLop);
            this.panel1.Controls.Add(this.rdokhoa);
            this.panel1.Controls.Add(this.rdoPhongthi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 181);
            this.panel1.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(55, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmChonIndssv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 181);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChonIndssv";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RadioButton rdoLop;
        public System.Windows.Forms.RadioButton rdokhoa;
        public System.Windows.Forms.RadioButton rdoPhongthi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;

    }
}