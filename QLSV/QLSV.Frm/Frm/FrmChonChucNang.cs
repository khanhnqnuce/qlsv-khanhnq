using System;
using System.Windows.Forms;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonChucNang : Form
    {
        public FrmChonChucNang()
        {
            InitializeComponent();
        }

        public string StrChucNang = "";

        private void txtMaChucNang_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void txtMaChucNang_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        StrChucNang = txtMaChucNang.Text;
                        Dispose();
                        break;
                    case Keys.Escape:
                        Dispose();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void txtMaChucNang_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    e.SuppressKeyPress = true;
        }
    }
}
