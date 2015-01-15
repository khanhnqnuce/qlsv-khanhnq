using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptDanhSachDiemThi : Form
    {
        public bool Update;
        public FrmRptDanhSachDiemThi()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Update = true;
            Close();
        }
    }
}
