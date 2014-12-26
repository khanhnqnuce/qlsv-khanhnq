using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmCheckXepPhong : Form
    {
        private bool m_bChon;

        public FrmCheckXepPhong()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmChonindssv_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bChon) return;
            rdoall.Checked = false;
            rdoone.Checked = false;
        }

        private void FrmCheckXepPhong_Load(object sender, EventArgs e)
        {
            
        }
    }
}
