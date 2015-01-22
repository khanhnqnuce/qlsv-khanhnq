namespace QLSV.Frm.Frm
{
    partial class FrmRptkiemtraloilogic
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
            this.button1 = new System.Windows.Forms.Button();
            this.rdoone = new System.Windows.Forms.RadioButton();
            this.rdoall = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 27);
            this.button1.TabIndex = 26;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rdoone
            // 
            this.rdoone.AutoSize = true;
            this.rdoone.Checked = true;
            this.rdoone.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdoone.Location = new System.Drawing.Point(31, 20);
            this.rdoone.Name = "rdoone";
            this.rdoone.Size = new System.Drawing.Size(179, 21);
            this.rdoone.TabIndex = 24;
            this.rdoone.Text = "KT logic kỳ thi đang xử lý";
            this.rdoone.UseVisualStyleBackColor = true;
            // 
            // rdoall
            // 
            this.rdoall.AutoSize = true;
            this.rdoall.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdoall.Location = new System.Drawing.Point(31, 62);
            this.rdoall.Name = "rdoall";
            this.rdoall.Size = new System.Drawing.Size(148, 21);
            this.rdoall.TabIndex = 24;
            this.rdoall.Text = "KT logic nhiều kỳ thi";
            this.rdoall.UseVisualStyleBackColor = true;
            // 
            // FrmRptkiemtraloilogic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 152);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rdoall);
            this.Controls.Add(this.rdoone);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRptkiemtraloilogic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra lỗi logic";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.RadioButton rdoone;
        public System.Windows.Forms.RadioButton rdoall;
    }
}