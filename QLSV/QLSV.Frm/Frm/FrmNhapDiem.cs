using System;
using System.Windows.Forms;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmNhapDiem : Form
    {
        public FrmNhapDiem()
        {
            InitializeComponent();
        }

        private void txtNhapdiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnnhapdiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNhapdiem.Text))
            {
                errorNhapdiem.SetError(txtNhapdiem,@"Nhập vào thang điểm");
                return;
            }
            Close();
        }

        private void txtNhapdiem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                       Close();
                        break;
                    case Keys.Escape:
                        txtNhapdiem.Clear();
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
