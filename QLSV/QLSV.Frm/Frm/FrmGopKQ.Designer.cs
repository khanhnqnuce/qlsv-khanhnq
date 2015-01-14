namespace QLSV.Frm.Frm
{
    partial class FrmGopKQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGopKQ));
            this.cbohocky = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnluu = new System.Windows.Forms.Button();
            this.errorNH = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorHK = new System.Windows.Forms.ErrorProvider(this.components);
            this.cboNamHoc = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorNH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorHK)).BeginInit();
            this.SuspendLayout();
            // 
            // cbohocky
            // 
            this.cbohocky.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbohocky.FormattingEnabled = true;
            this.cbohocky.Location = new System.Drawing.Point(82, 65);
            this.cbohocky.Name = "cbohocky";
            this.cbohocky.Size = new System.Drawing.Size(140, 23);
            this.cbohocky.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Năm học :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Học kỳ :";
            // 
            // btnluu
            // 
            this.btnluu.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnluu.Location = new System.Drawing.Point(78, 106);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(87, 27);
            this.btnluu.TabIndex = 4;
            this.btnluu.Text = "OK";
            this.btnluu.UseVisualStyleBackColor = true;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // errorNH
            // 
            this.errorNH.ContainerControl = this;
            this.errorNH.Icon = ((System.Drawing.Icon)(resources.GetObject("errorNH.Icon")));
            // 
            // errorHK
            // 
            this.errorHK.ContainerControl = this;
            this.errorHK.Icon = ((System.Drawing.Icon)(resources.GetObject("errorHK.Icon")));
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.DisplayMember = "NamHoc";
            this.cboNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamHoc.FormattingEnabled = true;
            this.cboNamHoc.Location = new System.Drawing.Point(82, 26);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Size = new System.Drawing.Size(140, 23);
            this.cboNamHoc.TabIndex = 5;
            this.cboNamHoc.ValueMember = "ID";
            // 
            // FrmGopKQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 157);
            this.Controls.Add(this.cboNamHoc);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbohocky);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGopKQ";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmGopKQ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorNH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorHK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbohocky;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.ErrorProvider errorNH;
        private System.Windows.Forms.ErrorProvider errorHK;
        public System.Windows.Forms.ComboBox cboNamHoc;
    }
}