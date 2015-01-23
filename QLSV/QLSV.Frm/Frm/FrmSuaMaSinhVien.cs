using System;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSuaMaSinhVien : Form
    {
        private readonly int _masv;
        private readonly int _idkythi;
        private readonly string _made;
        public bool Update;

        public FrmSuaMaSinhVien(int masv, int idkythi, string made)
        {
            InitializeComponent();
            _masv = masv;
            _idkythi = idkythi;
            _made = made;
        }

        private void Sua()
        {
            if (txtmasinhvien.Text=="")
            {
                errorMaSinhVien.SetError(txtmasinhvien, "Nhập mã sinh viên");
            }
            else
            {
                UpdateData.UpdateMaSinhVien(int.Parse(txtmasinhvien.Text), _masv,_idkythi,_made);
                Close();
                MessageBox.Show(@"Lưu lại thành công");
                Update = true;
            }
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

        private void btnluu_Click(object sender, EventArgs e)
        {
            Sua();
        }
    }
}
