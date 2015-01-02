using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmCheckInDiem : Form
    {
        public bool Update;
        public FrmCheckInDiem()
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
