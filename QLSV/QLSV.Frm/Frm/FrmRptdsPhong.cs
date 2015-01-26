using System;
using System.Windows.Forms;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptdsPhong : Form
    {
        public bool update;
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

        private void InPhong()
        {
            try
            {
                if (string.IsNullOrEmpty(txttuphong.Text))
                {
                    txttuphong.Focus();
                }
                else if (string.IsNullOrEmpty(txtdenphong.Text))
                {
                    txtdenphong.Focus();
                }
                else
                {
                    update = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            InPhong();
        }

        private void txtdenphong_KeyUp(object sender, KeyEventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Enter):
                    InPhong();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
