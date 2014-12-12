using System;
using System.Windows.Forms;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSuaMaSinhVien : Form
    {
        public int id;

        public FrmSuaMaSinhVien()
        {
            InitializeComponent();
        }

        private void Sua()
        {
            if (txtmasinhvien.Text=="")
            {
                errorMaSinhVien.SetError(txtmasinhvien, "Nhập mã sinh viên");
            }
            else
            {
                var hs = new BaiLam
                {
                    ID = id,
                    MaSinhVien = double.Parse(txtmasinhvien.Text)
                };
                UpdateData.UpdateMaSinhVien(hs);
                Close();
                MessageBox.Show(@"Lưu lại thành công");
                id = 0;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Sua();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Enter):
                    Sua();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
