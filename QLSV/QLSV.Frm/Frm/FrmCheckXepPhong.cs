using System;
using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmCheckXepPhong : Form
    {
        public bool update;

        public FrmCheckXepPhong()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            update = true;
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
