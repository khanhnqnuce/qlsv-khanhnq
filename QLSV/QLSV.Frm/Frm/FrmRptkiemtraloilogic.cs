using System.Windows.Forms;

namespace QLSV.Frm.Frm
{
    public partial class FrmRptkiemtraloilogic : Form
    {
        public bool Check;
        public FrmRptkiemtraloilogic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Check = true;
            Close();
        }
    }
}
