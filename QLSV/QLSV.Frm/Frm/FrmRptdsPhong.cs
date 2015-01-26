using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptdsPhong : Form
    {
        public FrmRptdsPhong()
        {
            InitializeComponent();
        }

        private void txtdenphong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttuphong.Text))
            {
                txttuphong.Focus();
            }
            else if(string.IsNullOrEmpty(txtdenphong.Text))
            {
                txtdenphong.Focus();
            }
            else
            {
                Close();
            }
        }
    }
}
