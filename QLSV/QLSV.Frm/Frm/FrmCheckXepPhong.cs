using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmCheckXepPhong : Form
    {
        private bool _ok;
        public FrmCheckXepPhong()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _ok = true;
            Close();
        }

        private void FrmChonindssv_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ok) return;
            rdoall.Checked = false;
            rdoone.Checked = false;
        }
    }
}
