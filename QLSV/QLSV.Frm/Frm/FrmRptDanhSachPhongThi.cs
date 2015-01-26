using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptDanhSachPhongThi : Form
    {
        public bool bUpdate;
        public FrmRptDanhSachPhongThi()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bUpdate = true;
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
