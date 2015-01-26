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
