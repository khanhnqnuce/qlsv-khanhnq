using System;
using System.Windows.Forms;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmTimkiem : Form
    {
        public delegate void CustomHandler(object sender, string masinhvien);
        public event CustomHandler Timkiemsinhvien;

        public FrmTimkiem()
        {
            InitializeComponent();
        }

        private void txtMaChucNang_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Timkiemsinhvien(sender,txtmasinhvien.Text);
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
    }
}
