using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptDanhSachDiemThi : Form
    {
        public bool update;
        public FrmRptDanhSachDiemThi()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
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
