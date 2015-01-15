using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptDanhSachSinhVien : Form
    {
        public bool bUpdate;
        public FrmRptDanhSachSinhVien()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            bUpdate = true;
            Close();
        }
    }
}
