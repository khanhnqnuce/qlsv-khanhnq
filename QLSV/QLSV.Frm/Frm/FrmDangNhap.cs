using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QLSV.Core.Service;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmDangNhap : Form
    {
        private readonly QuanlysinhvienSevice _taikhoanSrv;
        private IList<Taikhoan> _listCheck = new List<Taikhoan>();

        public delegate void CustomHandler(object sender, bool checkState, Taikhoan hs);

        public event CustomHandler CheckDangNhap;

        public FrmDangNhap()
        {
            InitializeComponent();
            _taikhoanSrv = new QuanlysinhvienSevice();
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
                _listCheck = QuanlysinhvienSevice.KiemTraTaiKhoan(txtTaiKhoan.Text, MaHoaMd5.Md5(txtMatKhau.Text));
                if (_listCheck.Count > 0)
                {
                    CheckDangNhap(this, true, _listCheck[0]);
                }
                else
                {
                    CheckDangNhap(this, false, null);
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

        public void HighlightControlSet()
        {
            //errormatkhau.SetError(txtMatKhau, "Sai mật khẩu hoặc tài khoản");
            //errorProvider = new ErrorProvider();
            //errorProvider.Icon = FormResource.error;
            //errorProvider.SetError(txtMatKhau, "Sai mật khẩu hoặc tài khoản");
        }
    }
}
