using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_QuanLyNguoiDung : FunctionControlHasGrid
    {
        private readonly IList<Taikhoan> _listAdd = new List<Taikhoan>();
        private readonly IList<Taikhoan> _listUpdate = new List<Taikhoan>();
        private readonly IList<Taikhoan> _listUpdatepass = new List<Taikhoan>();

        public Frm_QuanLyNguoiDung()
        {
            InitializeComponent();
        }

        #region Exit

        protected virtual DataTable GetTable()
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

        protected virtual void LoadGrid()
        {
            try
            {
                var a = LoadData.Load(14);
                dgv_DanhSach.DataSource = LoadData.Load(14);
                foreach (var row in dgv_DanhSach.Rows)
                {
                    row.Cells["TaiKhoan"].Activation = Activation.NoEdit;
                    row.Cells["MatKhau"].Hidden = true;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void LoadFormDetail()
        {
            LoadGrid();
            if (dgv_DanhSach.Rows.Count == 0)
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
            ucQuyen.Rows.Band.Columns["Quyen"].Width = 250;
            return ucQuyen;
        }

        private void FocusCellPass()
        {
            try
            {
                dgv_DanhSach.ActiveRow.Cells["MatKhau"].Value = DBNull.Value;
                dgv_DanhSach.ActiveRow.Cells["MatKhau"].Hidden = false;
                dgv_DanhSach.ActiveRow.Cells["MatKhau"].Activate();
                dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "TaiKhoan");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(dgv_DanhSach, "ID", "TaiKhoan");
                if (IdDelete.Count > 0)
                {
                    DeleteData.XoaTaiKhoan(IdDelete);
                    Stt();
                    MessageBox.Show(@"Xóa dữ liệu thành công.");
                }
                
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected virtual bool ValidateData()
        {
            var inputTypes = new List<InputType>
            {
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                
            };
            return ValidateHighlight.UltraGrid(dgv_DanhSach, inputTypes);
        }

        protected override void SaveDetail()
        {
            try
            {
                if (ValidateData())
                {
                    MessageBox.Show(@"Vui lòng nhập đầy đủ thông tin", @"Lỗi");
                }
                else
                {
                    foreach (var row in dgv_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
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
                    if (_listAdd.Count <= 0 && _listUpdate.Count <= 0 && _listUpdatepass.Count <= 0) return;
                    if (_listUpdate.Count > 0) UpdateData.UpdateTaiKhoan(_listUpdate);
                    if (_listUpdatepass.Count > 0) UpdateData.UpdateMatKhau(_listUpdatepass);
                    if (_listAdd.Count > 0) InsertData.ThemTaiKhoan(_listAdd);

                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadFormDetail();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.XoaTaiKhoan();
                LoadFormDetail();
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

        #endregion

        #region Event uG

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                //band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["TaiKhoan"].Header.Caption = FormResource.txtTenTaiKhoan;
                band.Columns["HoTen"].Header.Caption = FormResource.txtTenNguoiDung;
                band.Columns["MatKhau"].Header.Caption = FormResource.txtMatKhau;
                band.Columns["Quyen"].Header.Caption = FormResource.txtQuyen;

                #endregion

                band.Columns["STT"].MaxWidth = 70;
                //band.Columns["MatKhau"].Width = 150;
                //band.Columns["HoTen"].Width = 200;
                //band.Columns["TaiKhoan"].Width = 150;

                band.Columns["ID"].Hidden = true;
                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["Quyen"].EditorComponent = CboQuyen();
                band.Columns["Quyen"].Style = ColumnStyle.DropDownList;
            
                dgv_DanhSach.DisplayLayout.UseFixedHeaders = true;
                band.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;
                band.Columns["STT"].Header.Fixed = true;
                band.Columns["TaiKhoan"].Header.Fixed = true;
                band.Columns["MatKhau"].Header.Fixed = true; 

            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }

        private void uG_DanhSach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.AboveCell, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Down:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.BelowCell, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Right:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.NextCellByTab, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Left:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.PrevCellByTab, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                if (DeleteAndUpdate)
                {
                    DeleteAndUpdate = false;
                    return;
                }
                var indexcell = dgv_DanhSach.ActiveCell.Column.Index;
                var id = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                var ht = dgv_DanhSach.ActiveRow.Cells["HoTen"].Text;
                var qu = dgv_DanhSach.ActiveRow.Cells["Quyen"].Text;
                if (indexcell == 3)
                {
                    var mk = dgv_DanhSach.ActiveRow.Cells["MatKhau"].Text;
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
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {

            try
            {
                if (dgv_DanhSach.ActiveCell != null) return;
                FocusCellPass();
            }
            catch (Exception ex)
            {
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
            LoadFormDetail();
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
