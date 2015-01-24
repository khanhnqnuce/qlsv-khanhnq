using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmLuuDiemThi : Form
    {
        public bool bUpdate ;
        public FrmLuuDiemThi()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            bUpdate = true;
            Close();
        }
    }
}
