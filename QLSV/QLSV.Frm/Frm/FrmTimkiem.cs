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
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
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
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void FrmTimkiem_Load(object sender, EventArgs e)
        {
            txtmasinhvien.Clear();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            Timkiemsinhvien(sender, txtmasinhvien.Text);
        }
    }
}
