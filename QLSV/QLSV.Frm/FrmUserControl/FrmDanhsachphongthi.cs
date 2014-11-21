using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmDanhsachphongthi : FunctionControlHasGrid
    {
        private readonly List<PhongThi> _listAdd = new List<PhongThi>();
        private readonly List<PhongThi> _listUpdate = new List<PhongThi>();

        public FrmDanhsachphongthi()
        {
            InitializeComponent();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("TenPhong", typeof(string));
            table.Columns.Add("SucChua", typeof(int));
            table.Columns.Add("SoLuong", typeof(int));
            table.Columns.Add("GhiChu", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
                var table = GetTable();
                var danhsach = QlsvSevice.Load<PhongThi>();
                var stt = 1;
                foreach (var hs in danhsach)
                {
                    table.Rows.Add(hs.ID, stt, hs.TenPhong, hs.SucChua, hs.SoLuong, hs.GhiChu);
                    stt++;
                }
                uG_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void LoadFormDetail()
        {
            try
            {
                LoadGrid();
                if (uG_DanhSach.Rows.Count == 0)
                {
                    InsertRow();
                }
                _listUpdate.Clear();
                _listAdd.Clear();
                IdDelete.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "TenPhong");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "TenPhong");
                Stt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                foreach (var row in uG_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                {
                    var hs = new PhongThi
                    {
                        TenPhong = row.Cells["TenPhong"].Text,
                        SucChua = int.Parse(row.Cells["SucChua"].Text),
                        GhiChu = row.Cells["GhiChu"].Text
                    };
                    _listAdd.Add(hs);
                }
                QlsvSevice.ThemAll(_listAdd);
                QlsvSevice.SuaAll(_listUpdate);
                QlsvSevice.Xoa(IdDelete, "PhongThi");
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();

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

        protected override void XoaDetail()
        {
            try
            {
                QlsvSevice.Xoa("PhongThi");
                LoadFormDetail();
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

        public void Rptdanhsach()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", uG_DanhSach.DataSource);
            rptdanhsachphongthi.FilePath = Application.StartupPath + @"\Reports\danhsachphongthi.rst";
            using (var previewForm = new PreviewForm(rptdanhsachphongthi))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachphongthi.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void Stt()
        {
            for (var i = 0; i < uG_DanhSach.Rows.Count; i++)
            {
                uG_DanhSach.Rows[i].Cells[1].Value = i + 1;
            }
        }

        #endregion

        #region Event_uG

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                if (b)
                {
                    b = false;
                    return;
                }
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                if (string.IsNullOrEmpty(id)) return;
                foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                {
                    item.TenPhong = uG_DanhSach.ActiveRow.Cells["TenPhong"].Text;
                    item.SucChua = int.Parse(uG_DanhSach.ActiveRow.Cells["SucChua"].Text);
                    item.GhiChu = uG_DanhSach.ActiveRow.Cells["GhiChu"].Text;
                    return;
                }
                var hs = new PhongThi
                {
                    ID = int.Parse(id),
                    TenPhong = uG_DanhSach.ActiveRow.Cells["TenPhong"].Text,
                    SucChua = int.Parse(uG_DanhSach.ActiveRow.Cells["SucChua"].Text),
                    GhiChu = uG_DanhSach.ActiveRow.Cells["GhiChu"].Text,
                };
                _listUpdate.Add(hs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["SoLuong"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["TenPhong"].Width = 150;
                band.Columns["SucChua"].Width = 150;
                band.Columns["SoLuong"].Width = 150;
                band.Columns["GhiChu"].Width = 250;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["SucChua"].FormatNumberic();

                #region Caption

                band.Columns["TenPhong"].Header.Caption = @"Tên phòng";
                band.Columns["SucChua"].Header.Caption = @"Sức chứa";
                band.Columns["SoLuong"].Header.Caption = @"Số Lượng";
                band.Columns["GhiChu"].Header.Caption = @"Ghi chú";

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        uG_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        uG_DanhSach.PerformAction(UltraGridAction.AboveCell, false, false);
                        e.Handled = true;
                        uG_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Down:
                        uG_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        uG_DanhSach.PerformAction(UltraGridAction.BelowCell, false, false);
                        e.Handled = true;
                        uG_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Right:
                        uG_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        uG_DanhSach.PerformAction(UltraGridAction.NextCellByTab, false, false);
                        e.Handled = true;
                        uG_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Left:
                        uG_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        uG_DanhSach.PerformAction(UltraGridAction.PrevCellByTab, false, false);
                        e.Handled = true;
                        uG_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region MenuStrip

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            SaveDetail();
        }

        #endregion

        private void FrmDanhsachphongthi_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

    }
}
