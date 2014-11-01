using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;

namespace QLSV.Frm.Frm
{
    public partial class FrmSinhVien : FunctionControlHasGrid
    {
        #region Create

        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private readonly IList<SinhVien> _listUpdate = new List<SinhVien>();
        private readonly FrmTimkiem _frmTimkiem;
        private readonly FrmThemsinhvien _frmThemsinhvien;
        private IList<Lop> _listLop;
        private IList<Khoa> _listKhoa;
        private DataTable _danhsach = new DataTable();
        private UltraGridRow _newRow = null;

        #endregion

        public FrmSinhVien()
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
            _frmThemsinhvien = new FrmThemsinhvien();
            _frmThemsinhvien.Themmoisinhvien += Themmoisinhvien;
            _listLop = QlsvSevice.Load<Lop>();
            _listKhoa = QlsvSevice.Load<Khoa>();
            LoadForm();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof (int));
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("MaSinhVien", typeof (string));
            table.Columns.Add("HoSinhVien", typeof (string));
            table.Columns.Add("TenSinhVien", typeof (string));
            table.Columns.Add("NgaySinh", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("TenKhoa", typeof(string));

            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var table = GetTable();
                var stt = 1;
                table.Merge(SinhVienSql.LoadSinhVien());
                foreach (DataRow row in table.Rows)
                {
                    row["STT"] = stt++;
                }
                uG_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        private void LoadForm()
        {
            try
            {
                LoadGrid();
                _listAdd.Clear();
                _listUpdate.Clear();
                IdDelete.Clear();
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

        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "MaSinhVien");
        }

        protected override void DeleteRow()
        {

            DeleteRowGrid(uG_DanhSach, "ID", "MaSinhVien");
        }

        protected override void Save()
        {
            try
            {
                if (_danhsach == null || _danhsach.Rows.Count == 0) goto abc;
                foreach (DataRow row in _danhsach.Rows)
                {
                    var checkmalop = "";
                    var checkmakhoa = "";
                    var tenkhoa = row["TenKhoa"].ToString();
                    var malop = row["MaLop"].ToString();                    
                    // Kiểm tra lớp đã tồn tại chưa
                    foreach (var lop in _listLop.Where(lop => lop.MaLop == malop))
                    {
                        var hs = new SinhVien
                        {
                            MaSinhVien = row["MaSinhVien"].ToString(),
                            HoSinhVien = row["HoSinhVien"].ToString(),
                            TenSinhVien = row["TenSinhVien"].ToString(),
                            NgaySinh = row["NgaySinh"].ToString(),
                            IdLop = lop.ID,
                        };
                        checkmalop = malop;
                        _listAdd.Add(hs);
                    }
                    if (checkmalop != "") continue;
                    //Kiểm tra khoa đã tồn tại chưa
                    foreach (
                        var khoa in
                            _listKhoa.Where(khoa => khoa.TenKhoa.Equals(tenkhoa, StringComparison.OrdinalIgnoreCase))
                        )
                    {
                        var newLop1 = SinhVienSql.ThemLop(malop, khoa.ID);
                        checkmakhoa = newLop1.MaLop;
                        var hs = new SinhVien
                        {
                            MaSinhVien = row["MaSinhVien"].ToString(),
                            HoSinhVien = row["HoSinhVien"].ToString(),
                            TenSinhVien = row["TenSinhVien"].ToString(),
                            NgaySinh = row["NgaySinh"].ToString(),
                            IdLop = newLop1.ID,
                        };
                        _listAdd.Add(hs);
                        _listLop.Add(newLop1);
                    }
                    if (checkmakhoa != "") continue;

                    // Chưa có khoa lớp thì thêm mới
                    var newkhoa = SinhVienSql.ThemKhoa(tenkhoa);
                    var newLop3 = SinhVienSql.ThemLop(malop, newkhoa.ID);
                    var hs1 = new SinhVien
                    {
                        MaSinhVien = row["MaSinhVien"].ToString(),
                        HoSinhVien = row["HoSinhVien"].ToString(),
                        TenSinhVien = row["TenSinhVien"].ToString(),
                        NgaySinh = row["NgaySinh"].ToString(),
                        IdLop = newLop3.ID
                    };

                    _listAdd.Add(hs1);
                    _listLop.Add(newLop3);
                    _listKhoa.Add(newkhoa);
                }
                SinhVienSql.ThemSinhVien(_listAdd);
                _danhsach.Clear();
                abc:
                QlsvSevice.Sua(_listUpdate);
                QlsvSevice.Xoa(IdDelete, "SinhVien");

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

        protected override void Xoa()
        {
            try
            {
                if (DialogResult.Yes ==
                    MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                {
                    QlsvSevice.Xoa("SinhVien");
                    LoadForm();
                    _listLop = QlsvSevice.Load<Lop>();
                    _listKhoa = QlsvSevice.Load<Khoa>();
                    _danhsach.Clear();
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

        private void Huy()
        {
            try
            {
                LoadForm();
                cbokhoa.Value = null;
                cbolop.Value = null;
                _danhsach.Clear();
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

        //private void Napdulieu()
        //{
        //    try
        //    {
        //        if (_danhsach == null || _danhsach.Rows.Count == 0) _danhsach = GetTable();
        //        var stt = uG_DanhSach.Rows.Count;
        //        var frmNapDuLieu = new FrmNapDuLieu(stt);
        //        frmNapDuLieu.ShowDialog();
        //        var b = frmNapDuLieu.ResultValue;
        //        _danhsach.Merge(b);
        //        if (b == null || b.Rows.Count == 0) return;
        //        var table = (DataTable) uG_DanhSach.DataSource;

        //        table.Merge(b);
        //        uG_DanhSach.DataSource = table;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains(FormResource.msgLostConnect))
        //        {
        //            MessageBox.Show(FormResource.txtLoiDB);
        //        }
        //        else
        //            Log2File.LogExceptionToFile(ex);
        //    }
        //}

        private void Timkiemsinhvien(object sender, string masinhvien)
        {
            try
            {
                if (_newRow != null) _newRow.Selected = false;
                var tb = (DataTable) uG_DanhSach.DataSource;
                foreach (
                    var row in uG_DanhSach.Rows.Where(row => row.Cells["MaSinhVien"].Value.ToString() == masinhvien))
                {
                    uG_DanhSach.ActiveRowScrollRegion.ScrollPosition = row.Index;
                    row.Selected = true;
                    _newRow = row;
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

        private void Themmoisinhvien(object sender, SinhVien hs, string tenkhoa)
        {
            try

            {
                var listLop = QlsvSevice.Load<Lop>();
                var malop = "";
                foreach (var item in listLop.Where(item => item.ID == hs.IdLop))
                {
                    malop = item.MaLop;
                }
                var row = uG_DanhSach.DisplayLayout.Bands[0].AddNew();
                var stt = uG_DanhSach.Rows.Count;
                row.Cells["ID"].Value = hs.ID;
                row.Cells["STT"].Value = stt;
                row.Cells["MaSinhVien"].Value = hs.MaSinhVien;
                row.Cells["HoSinhVien"].Value = hs.HoSinhVien;
                row.Cells["TenSinhVien"].Value = hs.TenSinhVien;
                row.Cells["NgaySinh"].Value = hs.NgaySinh;
                row.Cells["MaLop"].Value = malop;
                row.Cells["TenKhoa"].Value = tenkhoa;
                row.Cells["MaSinhVien"].Activate();
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
        
        #endregion

        #region Event uG

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                //band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["HoSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["TenSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["TenKhoa"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MaxWidth = 70;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaSinhVien"].MaxWidth = 150;
                band.Columns["HoSinhVien"].MaxWidth = 200;
                band.Columns["TenSinhVien"].MaxWidth = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = FormResource.txtMasinhvien;
                band.Columns["HoSinhVien"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSinhVien"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["TenKhoa"].Header.Caption = FormResource.txtKhoaquanly;
                band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;

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
            //try
            //{
            //    var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
            //    if (string.IsNullOrEmpty(id)) return;
            //    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
            //    {
            //        item.MaSinhVien = uG_DanhSach.ActiveRow.Cells["MaSinhVien"].Text;
            //        item.HoSinhVien = uG_DanhSach.ActiveRow.Cells["HoSinhVien"].Text;
            //        item.TenSinhVien = uG_DanhSach.ActiveRow.Cells["TenSinhVien"].Text;
            //        item.NgaySinh = uG_DanhSach.ActiveRow.Cells["NgaySinh"].Text;
            //        item.IdLop = SinhVienSql.LoadLop(uG_DanhSach.ActiveRow.Cells["MaLop"].Text).ID;
            //        return;
            //    }
            //    var hs = new SinhVien
            //    {
            //        ID = int.Parse(id),
            //        MaSinhVien = uG_DanhSach.ActiveRow.Cells["MaSinhVien"].Text,
            //        HoSinhVien = uG_DanhSach.ActiveRow.Cells["HoSinhVien"].Text,
            //        TenSinhVien = uG_DanhSach.ActiveRow.Cells["TenSinhVien"].Text,
            //        NgaySinh = uG_DanhSach.ActiveRow.Cells["NgaySinh"].Text,
            //        IdLop = SinhVienSql.LoadLop(uG_DanhSach.ActiveRow.Cells["MaLop"].Text).ID
            //    };
            //    _listUpdate.Add(hs);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    Log2File.LogExceptionToFile(ex);
            //}
        }

        #endregion

        #region Button

        private void btnthemmoi_Click(object sender, EventArgs e)
        {
            _frmThemsinhvien.ShowDialog();
        }

        private void btnNapDuLieu_Click(object sender, EventArgs e)
        {
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnHuy.Focus();
            DeleteRow();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void btnInds_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Kiemtrafile())
            //    {
            //        MessageBox.Show(@"Vui lòng đóng file đang được mở.", @"Thông báo", MessageBoxButtons.OK,
            //            MessageBoxIcon.Information);
            //        return;
            //    }

            //    if (!Directory.Exists("Data"))
            //        Directory.CreateDirectory("Data");

            //    ultraGridExcelExporter.Export(uG_DanhSach, @"Data\DanhSachSinhVien.xls");

            //    var mydoc = new Process();
            //    if (File.Exists(Application.StartupPath + @"\Data\DanhSachSinhVien.xls"))
            //    {
            //        mydoc.StartInfo.FileName = Application.StartupPath + @"\Data\DanhSachSinhVien.xls";
            //        mydoc.Start();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    Log2File.LogExceptionToFile(ex);
            //}
        }

        private static bool Kiemtrafile()
        {
            try
            {
                if (!File.Exists(Application.StartupPath + @"\Data\DanhSachSinhVien.xls")) return false;
                var f = new FileStream(Application.StartupPath + @"\Data\DanhSachSinhVien.xls", FileMode.Open);
                f.Dispose();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        #endregion

        #region MenuStrip

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            _frmThemsinhvien.ShowDialog();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            btnHuy.Focus();
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void menuStrip_dong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void napDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmInportSinhVien();
            frm.ShowDialog();
            LoadForm();
        }

        #endregion

        #region Loadcombobox

        private void Timkiemtheokhoa()
        {
            try
            {
                if (string.IsNullOrEmpty(cbokhoa.Text)) return;
                var b = IsNumber(cbokhoa.Value.ToString());
                if (!b) return;
                var table = GetTable();
                table.Merge(SinhVienSql.Timkiemtheokhoa(int.Parse(cbokhoa.Value.ToString())));
                var stt = 1;
                foreach (DataRow row in table.Rows)
                {
                    row["STT"] = stt++;
                }
                uG_DanhSach.DataSource = table;
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

        private void Timkiemtheolop()
        {
            try
            {
                if (string.IsNullOrEmpty(cbolop.Text)) return;
                var b = IsNumber(cbolop.Value.ToString());
                if (!b) return;
                var table = GetTable();
                table.Merge(SinhVienSql.Timkiemtheolop(int.Parse(cbolop.Value.ToString())));
                var stt = 1;
                foreach (DataRow row in table.Rows)
                {
                    row["STT"] = stt++;
                }
                uG_DanhSach.DataSource = table;
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

        private void cbokhoa_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Timkiemtheokhoa();
                    break;
            }
        }

        private void cbolop_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Timkiemtheolop();
                    break;
            }
        }

        private void cbokhoa_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Timkiemtheokhoa();
                var rootBand = cbolop.DisplayLayout.Bands[0];
                rootBand.ColumnFilters.ClearAllFilters();
                if (string.IsNullOrEmpty(cbokhoa.Text)) return;
                rootBand.ColumnFilters["IdKhoa"].FilterConditions.Add(FilterComparisionOperator.Equals, cbokhoa.Value);
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

        private void cbolop_ValueChanged(object sender, EventArgs e)
        {
            Timkiemtheolop();
        }

        private bool IsNumber(string pText)
        {
            var regex = new Regex(@"[0-9]");
            return regex.IsMatch(pText);
        }

        #endregion

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                cbokhoa.DataSource = SinhVienSql.LoadKhoa();
                cbokhoa.ValueMember = "ID";
                cbokhoa.DisplayMember = "TenKhoa";
                cbokhoa.Rows.Band.Columns["ID"].Hidden = true;
                cbokhoa.Rows.Band.Columns["MaKhoa"].Hidden = true;
                cbokhoa.Rows.Band.Columns["TenKhoa"].Width = 250;
                cbokhoa.Rows.Band.ColHeadersVisible = false;

                cbolop.DataSource = SinhVienSql.LoadLop();
                cbolop.ValueMember = "ID";
                cbolop.DisplayMember = "MaLop";
                cbolop.Rows.Band.Columns["ID"].Hidden = true;
                cbolop.Rows.Band.Columns["IdKhoa"].Hidden = true;
                cbolop.Rows.Band.Columns["GhiChu"].Hidden = true;
                cbolop.Rows.Band.ColHeadersVisible = false;
                cbolop.DropDownWidth = 0;
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.F3):
                    Xoa();
                    break;
                case (Keys.F5):
                    break;
                case (Keys.F8):
                    break;
                case (Keys.F11):
                    btnHuy.Focus();
                    DeleteRow();
                    break;
                case (Keys.F12):
                    Huy();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
                case (Keys.Insert):
                    _frmThemsinhvien.ShowDialog();
                    break;
                case (Keys.Control | Keys.S):
                    _frmTimkiem.ShowDialog();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //private void btnpdf_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Kiemtrafile())
        //        {
        //            MessageBox.Show(@"Vui lòng đóng file đang được mở.", @"Thông báo", MessageBoxButtons.OK,
        //                MessageBoxIcon.Information);
        //            return;
        //        }

        //        if (!Directory.Exists("Data"))
        //            Directory.CreateDirectory("Data");

        //        ultraGridDocumentExporter1.Export(uG_DanhSach, Application.StartupPath + @"\Data\grid.pdf", GridExportFileFormat.PDF);

        //        var mydoc = new Process();
        //        if (File.Exists(Application.StartupPath + @"\Data\grid.pdf"))
        //        {
        //            mydoc.StartInfo.FileName = Application.StartupPath + @"\Data\grid.pdf";
        //            mydoc.Start();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        Log2File.LogExceptionToFile(ex);
        //    }
        //}

        private void uG_DanhSach_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            var index = e.Cell.Row.Index;
            var obj = new FrmThemsinhvien();
            obj.Id = int.Parse(e.Cell.Row.Cells["ID"].Value.ToString());
            obj.txtmasinhvien.Text = e.Cell.Row.Cells["MaSinhVien"].Value.ToString();
            obj.txtmasinhvien.ReadOnly = true;
            obj.txthotendem.Text = e.Cell.Row.Cells["HoSinhVien"].Value.ToString();
            obj.txttensinhvien.Text = e.Cell.Row.Cells["TenSinhVien"].Value.ToString();
            obj.cbongaysinh.Text = e.Cell.Row.Cells["NgaySinh"].Value.ToString();
            obj.cbolop.Text = e.Cell.Row.Cells["MaLop"].Value.ToString();
            obj.cbokhoa.Text = e.Cell.Row.Cells["TenKhoa"].Value.ToString();
            obj.ShowDialog();
        }
    }
}

