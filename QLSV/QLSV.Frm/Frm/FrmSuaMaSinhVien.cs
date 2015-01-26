using System;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSuaMaSinhVien : Form
    {
        private readonly int _masv;
        private readonly int _idkythi;
        private readonly string _made;
        public bool update;
        private UltraGrid _ultra;

        public FrmSuaMaSinhVien(int masv, int idkythi, string made,UltraGrid ultra)
        {
            InitializeComponent();
            _ultra = ultra;
            _masv = masv;
            _idkythi = idkythi;
            _made = made;
        }

        private void Sua()
        {
            if (txtmasinhvien.Text=="")
            {
                errorMaSinhVien.SetError(txtmasinhvien, "Nhập mã sinh viên");
            }
            else
            {
                foreach (var row in _ultra.Rows.Where(row => row.Cells["MaSV"].Text == txtmasinhvien.Text))
                {
                    MessageBox.Show(@"Mã sinh viên đã bị trùng", @"Thông báo");
                    return;
                }
                UpdateData.UpdateMaSinhVien(int.Parse(txtmasinhvien.Text), _masv,_idkythi,_made);
                Close();
                MessageBox.Show(@"Lưu lại thành công");
                update = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Enter):
                    Sua();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtmasinhvien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            Sua();
        }
    }
}
