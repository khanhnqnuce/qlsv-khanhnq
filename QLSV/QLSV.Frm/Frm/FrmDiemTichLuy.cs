using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmDiemTichLuy : Form
    {
        public bool Update;
        public FrmDiemTichLuy()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Update = true;
            Close();
        }
    }
}
