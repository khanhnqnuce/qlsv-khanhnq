using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_107_Chonphongthi : FunctionControlHasGrid
    {
        private IList<XepPhong> _listXepPhong = new List<XepPhong>();
        private IList<KTPhong> _listKtPhong = new List<KTPhong>();
        private int _idkythi;

        public Frm_107_Chonphongthi(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
        }

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof (int));
            table.Columns.Add("STT", typeof (int));
            table.Columns.Add("TenPhong", typeof (string));
            table.Columns.Add("SucChua", typeof (int));
            table.Columns.Add("SiSo", typeof (string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                _listXepPhong.Clear();
                _listKtPhong.Clear();
                dgv_DanhSach.DataSource = LoadData.Load(11,_idkythi);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            var frm = new FrmChonPhongThi(_idkythi);
            frm.ShowDialog();
            LoadGrid();
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(dgv_DanhSach, "ID", "TenPhong");
                Stt();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Stt()
        {
            for (var i = 0; i < dgv_DanhSach.Rows.Count; i++)
            {
                dgv_DanhSach.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("KT_PHONG",_idkythi);
                UpdateData.UpdateXepPhongNull(_idkythi);
                LoadGrid();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                foreach (var i in IdDelete)
                {
                    var xp = new XepPhong
                    {
                        IdKyThi = _idkythi,
                        IdPhong = i
                    };

                    var ktp = new KTPhong
                    {
                        IdKyThi = _idkythi,
                        IdPhong = i
                    };
                    _listXepPhong.Add(xp);
                    _listKtPhong.Add(ktp);
                }
                DeleteData.XoaKtPhong(_listKtPhong);
                UpdateData.UpdateXepPhongNull(_listXepPhong);
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Frm_Chonphongthi_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void menuStrip_chonphong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Override.CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["TenPhong"].CellActivation = Activation.NoEdit;
                band.Columns["SucChua"].CellActivation = Activation.NoEdit;
                band.Columns["SiSo"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MinWidth = 60;
                band.Columns["TenPhong"].MinWidth = 140;
                band.Columns["SucChua"].MinWidth = 140;
                band.Columns["SiSo"].MinWidth = 140;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["TenPhong"].MaxWidth = 150;
                band.Columns["SucChua"].MaxWidth = 150;
                band.Columns["SiSo"].MaxWidth = 150;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["SucChua"].FormatNumberic();

                #region Caption

                band.Columns["TenPhong"].Header.Caption = @"Tên phòng";
                band.Columns["SucChua"].Header.Caption = @"Sức chứa";
                band.Columns["SiSo"].Header.Caption = @"Sĩ số";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
