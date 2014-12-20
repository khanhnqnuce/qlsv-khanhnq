using System;
using System.Windows.Forms;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmXepPhong : Form
    {
        public int IdPhong;
        public int IdKythi;
        public bool bUpdate;
        private bool _bCheckUpdate;

        public FrmXepPhong()
        {
            InitializeComponent();
        }

        private void FrmXepPhong_Load(object sender, EventArgs e)
        {
            if (bUpdate)
            {
                cboPhongthi.DisplayMember = "TenPhong";
                cboPhongthi.ValueMember = "IdPhong";
                cboPhongthi.DataSource = LoadData.Load(5,IdKythi);
                cboPhongthi.Rows.Band.Columns["IdPhong"].Hidden = true;
                cboPhongthi.DisplayLayout.Bands[0].Columns["TenPhong"].Header.Caption = @"Phòng thi";
                cboPhongthi.DisplayLayout.Bands[0].Columns["SucChua"].Header.Caption = @"Sức chứa";
                cboPhongthi.DisplayLayout.Bands[0].Columns["SiSo"].Header.Caption = @"Sĩ số";
                return;
            }
            cboPhongthi.DataSource = LoadData.Load(8);
            cboPhongthi.DisplayMember = "TenPhong";
            cboPhongthi.ValueMember = "ID";
            cboPhongthi.Rows.Band.Columns["ID"].Hidden = true;
            cboPhongthi.Rows.Band.Columns["GhiChu"].Hidden = true;
            cboPhongthi.DisplayLayout.Bands[0].Columns["TenPhong"].Header.Caption = @"Phòng thi";
            cboPhongthi.DisplayLayout.Bands[0].Columns["SucChua"].Header.Caption = @"Sức chứa";
            cboPhongthi.DisplayLayout.Bands[0].Columns["SoLuong"].Header.Caption = @"Sĩ số";
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

        private void FrmXepPhong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_bCheckUpdate) return;
            bUpdate = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboPhongthi.Text))
            {
                errorPhongthi.SetError(cboPhongthi, "Chọn Phòng thi");
                return;
            }
            if (bUpdate)
            {
                var hs = new XepPhong
                {
                    IdSV = int.Parse(txtmasinhvien.Text),
                    IdPhong = int.Parse(cboPhongthi.Value.ToString()),
                    IdKyThi = IdKythi
                };
                UpdateData.UpdateXepPhong(hs);
                UpdateData.UpdateKtPhong(hs.IdPhong, IdPhong, IdKythi);
                bUpdate = false;
                Close();
            }
            else
            {
                
                Close();
            }
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
