using System;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSuaMaSinhVien : Form
    {
        public int Masv = 0;

        public FrmSuaMaSinhVien(int id)
        {
            Masv = id;
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
                
                UpdateData.UpdateMaSinhVien(Masv,int.Parse(txtmasinhvien.Text));
                Close();
                MessageBox.Show(@"Lưu lại thành công");
                Masv = 0;
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

        private void txtmasinhvien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
