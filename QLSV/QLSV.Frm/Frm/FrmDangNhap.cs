using System;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing;
using QLSV.Core.LINQ;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;
using System.Drawing;

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

        private void lbdangnhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaiKhoan.Text))
            {
                MessageBox.Show(@"Vui lòng nhập tên tài khoản",@"Thông báo",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
            }else if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show(@"Vui lòng nhập mật khâu", @"Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(FormResource.FrmDangNhap_Dangnhap_, @"Thông báo");
                    txtMatKhau.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            var a = "  anh nho em nhiều lắm ";
            var b = a.Trim();
            var c = a.Replace(" ", "");

            lbdangnhap.ForeColor = Color.FromArgb(255,255,255);
            lbdangnhap.BackColor = Color.FromArgb(0, 171, 228);
        }

        private void lbdangnhap_MouseEnter(object sender, EventArgs e)
        {
            lbdangnhap.BackColor = Color.FromArgb(0, 255, 230);
        }

        private void lbdangnhap_MouseLeave(object sender, EventArgs e)
        {
            lbdangnhap.BackColor = Color.FromArgb(0, 171, 228);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Enter):
                    Dangnhap();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new FrmCauHinh();
            frm.ShowDialog();
        }
    }
}
