namespace QLSV.Frm.Frm
{
    partial class FrmDiemTichLuy
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.rdoLop = new System.Windows.Forms.RadioButton();
            this.rdokhoa = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.rdoLop);
            this.panel1.Controls.Add(this.rdokhoa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 127);
            this.panel1.TabIndex = 21;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(57, 89);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rdoLop
            // 
            this.rdoLop.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdoLop.Location = new System.Drawing.Point(47, 50);
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
            this.rdokhoa.Location = new System.Drawing.Point(48, 15);
            this.rdokhoa.Name = "rdokhoa";
            this.rdokhoa.Size = new System.Drawing.Size(92, 21);
            this.rdokhoa.TabIndex = 18;
            this.rdokhoa.Text = "Theo Khoa";
            this.rdokhoa.UseVisualStyleBackColor = true;
            // 
            // FrmDiemTichLuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 127);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDiemTichLuy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.RadioButton rdoLop;
        public System.Windows.Forms.RadioButton rdokhoa;
    }
}