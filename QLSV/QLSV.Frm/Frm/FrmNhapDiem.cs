using System;
using System.Windows.Forms;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmNhapDiem : Form
    {
        private bool _bCheck = false;
        public FrmNhapDiem()
        {
            InitializeComponent();
        }

        private void btnnhapdiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNhapdiem.Text))
            {
                errorNhapdiem.SetError(txtNhapdiem,@"Nhập vào thang điểm");
                return;
            }
            _bCheck = true;
            Close();
        }

        private void txtNhapdiem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        _bCheck = true;
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

        private void FrmNhapDiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bCheck) return;
            txtNhapdiem.Clear();
        }
    }
}
