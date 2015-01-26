namespace QLSV.Frm.Frm
{
    partial class FrmNhapDiem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNhapDiem));
            this.txtNhapdiem = new System.Windows.Forms.TextBox();
            this.btnnhapdiem = new System.Windows.Forms.Button();
            this.errorNhapdiem = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorNhapdiem)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNhapdiem
            // 
            this.txtNhapdiem.Location = new System.Drawing.Point(28, 10);
            this.txtNhapdiem.MaxLength = 5;
            this.txtNhapdiem.Name = "txtNhapdiem";
            this.txtNhapdiem.Size = new System.Drawing.Size(118, 22);
            this.txtNhapdiem.TabIndex = 0;
            this.txtNhapdiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNhapdiem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNhapdiem_KeyUp);
            // 
            // btnnhapdiem
            // 
            this.btnnhapdiem.Location = new System.Drawing.Point(152, 10);
            this.btnnhapdiem.Name = "btnnhapdiem";
            this.btnnhapdiem.Size = new System.Drawing.Size(75, 23);
            this.btnnhapdiem.TabIndex = 1;
            this.btnnhapdiem.Text = "Nhập xong";
            this.btnnhapdiem.UseVisualStyleBackColor = true;
            this.btnnhapdiem.Click += new System.EventHandler(this.btnnhapdiem_Click);
            // 
            // errorNhapdiem
            // 
            this.errorNhapdiem.ContainerControl = this;
            this.errorNhapdiem.Icon = ((System.Drawing.Icon)(resources.GetObject("errorNhapdiem.Icon")));
            this.errorNhapdiem.RightToLeft = true;
            // 
            // FrmNhapDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 43);
            this.Controls.Add(this.btnnhapdiem);
            this.Controls.Add(this.txtNhapdiem);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNhapDiem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập thang điểm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNhapDiem_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errorNhapdiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtNhapdiem;
        private System.Windows.Forms.Button btnnhapdiem;
        private System.Windows.Forms.ErrorProvider errorNhapdiem;
    }
}