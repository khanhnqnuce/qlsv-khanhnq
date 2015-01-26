using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptDiemTichLuy : Form
    {
        public bool Update;
        public FrmRptDiemTichLuy()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Update = true;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
