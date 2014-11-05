using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;
using QLSV.Frm.Base;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.Frm
{
    public partial class FrmQuanLyNguoiDung : FunctionControlHasGrid
    {
        private readonly IList<Taikhoan> _listAdd = new List<Taikhoan>();
        private readonly IList<Taikhoan> _listUpdate = new List<Taikhoan>();
        private readonly IList<Taikhoan> _listUpdatepass = new List<Taikhoan>();

        public FrmQuanLyNguoiDung()
        {
            InitializeComponent();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("TaiKhoan", typeof(string));
            table.Columns.Add("MatKhau", typeof(string));
            table.Columns.Add("HoTen", typeof(string));
            table.Columns.Add("Quyen", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var table = GetTable();
                var danhsach = QlsvSevice.Load<Taikhoan>();
                var stt = 1;
                foreach (var hs in danhsach)
                {
                    table.Rows.Add(hs.ID, stt, hs.TaiKhoan, hs.MatKhau, hs.HoTen, hs.Quyen);
                    stt++;
                }
                uG_DanhSach.DataSource = table;
                foreach (var row in uG_DanhSach.Rows)
                {
                    row.Cells["TaiKhoan"].Activation = Activation.NoEdit;
                    row.Cells["MatKhau"].Hidden = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        public void LoadForm()
        {
            LoadGrid();
            if (uG_DanhSach.Rows.Count == 0)
            {
                InsertRow();
            }
            _listAdd.Clear();
            _listUpdate.Clear();
            _listUpdatepass.Clear();
            IdDelete.Clear();
        }

        private static UltraCombo CboQuyen()
        {
            var ucQuyen = new UltraCombo();
            var table = new DataTable();
            table.Columns.Add("Quyen");
            table.Rows.Add("quantri");
            table.Rows.Add("nguoidung");

            ucQuyen.DataSource = table;
            ucQuyen.DisplayMember = "Quyen";
            ucQuyen.ValueMember = "Quyen";

            ucQuyen.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
            ucQuyen.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            ucQuyen.DropDownWidth = 0;
            ucQuyen.Rows.Band.ColHeadersVisible = false;
            ucQuyen.Rows.Band.Columns["Quyen"].Width = 180;
            return ucQuyen;
        }

        private void FocusCellPass()
        {
            try
            {
                uG_DanhSach.ActiveRow.Cells["MatKhau"].Value = DBNull.Value;
                uG_DanhSach.ActiveRow.Cells["MatKhau"].Hidden = false;
                uG_DanhSach.ActiveRow.Cells["MatKhau"].Activate();
                uG_DanhSach.PerformAction(UltraGridAction.EnterEditMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "TaiKhoan");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "TaiKhoan");
                STT();
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
                    var hs = new Taikhoan
                    {
                        TaiKhoan = row.Cells["TaiKhoan"].Value.ToString(),
                        HoTen = row.Cells["HoTen"].Value.ToString(),
                        Quyen = row.Cells["Quyen"].Value.ToString(),
                        MatKhau = MaHoaMd5.Md5(row.Cells["MatKhau"].Value.ToString())
                    };
                    _listAdd.Add(hs);
                }
                QlsvSevice.ThemAll(_listAdd);
                QlsvSevice.SuaTaiKhoan(_listUpdate);
                QlsvSevice.SuaMatKhau(_listUpdatepass);
                QlsvSevice.Xoa(IdDelete,"Taikhoan");

                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadForm();
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
                if (DialogResult.Yes ==
                    MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                {
                    QlsvSevice.XoaTaiKhoan();
                    LoadForm();
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

        private void STT()
        {
            for (var i = 0; i < uG_DanhSach.Rows.Count; i++)
            {
                uG_DanhSach.Rows[i].Cells[1].Value = i + 1;
            }
        }

        #endregion

        #region Button

        private void btnGhi_Click(object sender, EventArgs e)
        {
            SaveDetail();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaDetail();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        #endregion

        #region Event uG

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["TaiKhoan"].MaxWidth = 250;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["Quyen"].MaxWidth = 200;
                band.Columns["Quyen"].EditorComponent = CboQuyen();
                band.Columns["Quyen"].Style = ColumnStyle.DropDownList;

                #region Caption

                band.Columns["TaiKhoan"].Header.Caption = FormResource.txtTenTaiKhoan;
                band.Columns["HoTen"].Header.Caption = FormResource.txtTenNguoiDung;
                band.Columns["MatKhau"].Header.Caption = FormResource.txtMatKhau;
                band.Columns["Quyen"].Header.Caption = FormResource.txtQuyen;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.DisplayPromptMsg = false;
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

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                var indexcell = uG_DanhSach.ActiveCell.Column.Index;
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                var ht = uG_DanhSach.ActiveRow.Cells["HoTen"].Text;
                var qu = uG_DanhSach.ActiveRow.Cells["Quyen"].Text;
                if (indexcell == 3)
                {
                    var mk = uG_DanhSach.ActiveRow.Cells["MatKhau"].Text;
                    if (!string.IsNullOrEmpty(id)
                        && !string.IsNullOrEmpty(mk))
                    {
                        foreach (var item in _listUpdatepass.Where(item => item.ID == int.Parse(id)))
                        {
                            item.MatKhau = MaHoaMd5.Md5(mk);
                            return;
                        }
                        var hs = new Taikhoan
                        {
                            ID = int.Parse(id),
                            MatKhau = MaHoaMd5.Md5(mk)
                        };
                        _listUpdatepass.Add(hs);
                    }
                }
                else if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                    {
                        item.HoTen =ht;
                        item.Quyen = qu;
                        return;
                    }
                    var hs = new Taikhoan
                    {
                        ID = int.Parse(id),
                        HoTen = ht,
                        Quyen = qu
                    };
                    _listUpdate.Add(hs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {

            try
            {
                if (uG_DanhSach.ActiveCell != null) return;
                FocusCellPass();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
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

        private void menuStrip_doipass_Click(object sender, EventArgs e)
        {
            FocusCellPass();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void menuStrip_dong_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            SaveDetail();
        }
        
        #endregion

        private void FrmQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

    }
}
