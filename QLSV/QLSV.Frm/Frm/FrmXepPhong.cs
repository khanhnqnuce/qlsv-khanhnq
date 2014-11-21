using System;
using System.Windows.Forms;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmXepPhong : Form
    {
        public int gb_iIdsinhvien = 0;
        public int gb_iIdPhong = 0;
        public int gb_iIdKythi = 0;
        public bool gb_bUpdate = false;

        public FrmXepPhong()
        {
            InitializeComponent();
        }

        private void FrmXepPhong_Load(object sender, EventArgs e)
        {
            if (gb_bUpdate)
            {
                cboPhongthi.DataSource = LoadData.Load(9);
                cboPhongthi.DisplayMember = "TenPhong";
                cboPhongthi.ValueMember = "ID";
                cboPhongthi.Rows.Band.Columns["ID"].Hidden = true;
                cboPhongthi.Rows.Band.Columns["GhiChu"].Hidden = true;
                cboPhongthi.DisplayLayout.Bands[0].Columns["TenPhong"].Header.Caption = @"Phòng thi";
                cboPhongthi.DisplayLayout.Bands[0].Columns["SucChua"].Header.Caption = @"Sức chứa";
                cboPhongthi.DisplayLayout.Bands[0].Columns["SoLuong"].Header.Caption = @"Sĩ số";
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

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboPhongthi.Text))
            {
                errorPhongthi.SetError(cboPhongthi, "Chọn Phòng thi");
                return;
            }
            if (gb_bUpdate)
            {
                var hs = new XepPhong
                {
                    IdSV = gb_iIdsinhvien,
                    IdPhong = int.Parse(cboPhongthi.Value.ToString()),
                    IdKyThi = gb_iIdKythi
                };
                UpdateData.XepPhong(hs);
                UpdateData.UpdateGiamPhongThi(gb_iIdPhong);
                UpdateData.UpdatePhongThi(hs.IdPhong);
                Close();
                return;
            }
            var hs1 = new XepPhong
            {
                IdSV = gb_iIdsinhvien,
                IdPhong = int.Parse(cboPhongthi.Value.ToString()),
                IdKyThi = gb_iIdKythi

            };
            InsertData.XepPhong1(hs1);
            UpdateData.UpdatePhongThi(hs1.IdPhong);
            gb_iIdsinhvien = 0;
            Close();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
