namespace QLSV.Frm.Frm
{
    partial class FrmRptdsPhong
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txttuphong = new System.Windows.Forms.TextBox();
            this.txtdenphong = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ phòng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "đến phòng";
            // 
            // txttuphong
            // 
            this.txttuphong.Location = new System.Drawing.Point(91, 15);
            this.txttuphong.Name = "txttuphong";
            this.txttuphong.Size = new System.Drawing.Size(57, 22);
            this.txttuphong.TabIndex = 1;
            this.txttuphong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txttuphong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdenphong_KeyPress);
            // 
            // txtdenphong
            // 
            this.txtdenphong.Location = new System.Drawing.Point(213, 15);
            this.txtdenphong.Name = "txtdenphong";
            this.txtdenphong.Size = new System.Drawing.Size(57, 22);
            this.txtdenphong.TabIndex = 1;
            this.txtdenphong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtdenphong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdenphong_KeyPress);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOk.Location = new System.Drawing.Point(113, 49);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmRptdsPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 86);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtdenphong);
            this.Controls.Add(this.txttuphong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRptdsPhong";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập số phòng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txttuphong;
        public System.Windows.Forms.TextBox txtdenphong;
        private System.Windows.Forms.Button btnOk;
    }
}