using System;
using System.Windows.Forms;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptBangDiem : Form
    {
        public FrmRptBangDiem()
        {
            InitializeComponent();
        }

        public bool bUpdate { get; set; }
        public string Masv { get; set; }

        private void txtmasinhvien_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(txtmasinhvien.Text))
                        {
                            errormasinhvien.SetError(txtmasinhvien, "Mời nhập vào mã sinh viên.");
                            return;
                        }
                        bUpdate = true;
                        Masv = txtmasinhvien.Text;
                        Close();
                        break;
                    case Keys.Escape:
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmasinhvien.Text))
            {
                errormasinhvien.SetError(txtmasinhvien, "Mời nhập vào mã sinh viên.");
                return;
            }
            bUpdate = true;
            Masv = txtmasinhvien.Text;
            Close();
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
