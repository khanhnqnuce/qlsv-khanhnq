using System;
using System.Windows.Forms;
using QLSV.Core.LINQ;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmDangNhap : Form
    {
        public delegate void CustomHandler(object sender, bool checkState, Taikhoan hs);

        public event CustomHandler CheckDangNhap;

        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtDangNhap_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Dangnhap();
                        break;
                    case Keys.Escape:
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void txtTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaiKhoan.Text))
            {
                errormatkhau.Dispose();
                errortaikhoan.SetError(txtTaiKhoan,"Không được để trống");
                txtTaiKhoan.Focus();
            }else if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                errortaikhoan.Dispose();
                errormatkhau.SetError(txtMatKhau,"Không được để trống");
                txtMatKhau.Focus();
            }
            else
            {
                Dangnhap();
            }
        }

        private void Dangnhap()
        {
            try
            {
                var tb = LoadData.KiemTraTaiKhoan(txtTaiKhoan.Text, MaHoaMd5.Md5(txtMatKhau.Text));
                if (tb.Rows.Count>0)
                {
                    var taikhoan = new Taikhoan
                    {
                        ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                        TaiKhoan = tb.Rows[0]["TaiKhoan"].ToString(),
                        MatKhau = tb.Rows[0]["MatKhau"].ToString(),
                        HoTen = tb.Rows[0]["HoTen"].ToString(),
                        Quyen = tb.Rows[0]["Quyen"].ToString()
                    };
                    CheckDangNhap(this, true, taikhoan);
                }
                else
                {
                    errormatkhau.SetError(txtMatKhau, "Sai tài khoản hoặc mật khẩu");
                    txtMatKhau.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void HighlightTextBoxSet()
        {
            var tooltip = new ToolTip
            {
                IsBalloon = true,
                InitialDelay = 0,
                ShowAlways = true,
                ToolTipIcon = ToolTipIcon.Error,
                ToolTipTitle = "Lỗi"
            };
            tooltip.SetToolTip(txtMatKhau, "Sai mật khẩu hoặc tài khoản");
        }
    }
}
