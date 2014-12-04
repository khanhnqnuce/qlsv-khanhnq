using System;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonKyThi : Form
    {
        private bool b = false;
        public FrmChonKyThi()
        {
            InitializeComponent();
        }

        private void FrmChonKyThi_Load(object sender, EventArgs e)
        {
            cboKythi.DataSource = LoadData.Load(10);
            cboKythi.DisplayMember = "MaKyThi";
            cboKythi.ValueMember = "ID";
            cboKythi.Rows.Band.Columns["ID"].Hidden = true;
            cboKythi.Rows.Band.Columns["ThoiGianLamBai"].Hidden = true;
            cboKythi.Rows.Band.Columns["ThoiGianBatDau"].Hidden = true;
            cboKythi.Rows.Band.Columns["ThoiGianKetThuc"].Hidden = true;
            cboKythi.DisplayLayout.Bands[0].Columns["NgayThi"].Header.Caption = @"ngày thi";
            cboKythi.DisplayLayout.Bands[0].Columns["MaKyThi"].Header.Caption = @"Mã kỳ thi";
            cboKythi.DisplayLayout.Bands[0].Columns["TenKyThi"].Header.Caption = @"Tên kỳ thi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboKythi.Text))
            {
                errorkythi.SetError(cboKythi, "Không được để trống");
                return;
            }
            b = true;
            Close();
        }

        private void FrmChonKyThi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!b) cboKythi.Value = null;
        }
    }
}
