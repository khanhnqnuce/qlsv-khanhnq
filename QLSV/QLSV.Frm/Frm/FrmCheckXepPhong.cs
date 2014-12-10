using System;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmCheckXepPhong : Form
    {
        private bool m_bChon;
        public int gb_iIdKythi;
        public FrmCheckXepPhong()
        {
            InitializeComponent();
            //MaximizeBox = false;
            //MinimizeBox = false;
            //ShowIcon = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboKythi.Text))
            {
                errorkythi.SetError(cboKythi,"Chọn kỳ thi");
                return;
            }
            m_bChon = true;
            gb_iIdKythi = int.Parse(cboKythi.Value.ToString());
            Close();
        }

        private void FrmChonindssv_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bChon) return;
            rdoall.Checked = false;
            rdoone.Checked = false;
        }

        private void FrmCheckXepPhong_Load(object sender, EventArgs e)
        {
            cboKythi.DataSource = LoadData.Load(10);
            cboKythi.DisplayMember = "TenKyThi";
            cboKythi.ValueMember = "ID";
            cboKythi.Rows.Band.Columns["ID"].Hidden = true;
            cboKythi.Rows.Band.Columns["ThoiGianLamBai"].Hidden = true;
            cboKythi.Rows.Band.Columns["ThoiGianBatDau"].Hidden = true;
            cboKythi.Rows.Band.Columns["ThoiGianKetThuc"].Hidden = true;
            cboKythi.Rows.Band.Columns["NgayThi"].Hidden = true;
            cboKythi.DisplayLayout.Bands[0].Columns["MaKyThi"].Header.Caption = @"Mã kỳ thi";
            cboKythi.DisplayLayout.Bands[0].Columns["TenKyThi"].Header.Caption = @"Tên kỳ thi";
        }
    }
}
