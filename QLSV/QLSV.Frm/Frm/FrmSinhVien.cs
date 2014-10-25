using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.Frm
{
    public partial class FrmSinhVien : FunctionControlHasGrid
    {
        #region Create

        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private readonly IList<SinhVien> _listUpdate = new List<SinhVien>();

        #endregion

        public FrmSinhVien()
        {
            InitializeComponent();
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
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("MaKhoa", typeof (string));
            
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var listLop = QuanlysinhvienSevice.Load<Lop>();
                var listKhoa = QuanlysinhvienSevice.Load<Khoa>();
                var malop = "";
                var idKhoa = 0;
                var makhoa = "";
                var table = GetTable();
                var danhsach = QuanlysinhvienSevice.Load<SinhVien>();
                var stt = 1;
                foreach (var hs in danhsach)
                {
                    foreach (var item in listLop.Where(item => hs.Lop.ID == item.ID))
                    {
                        malop = item.MaLop;
                        idKhoa = item.IdKhoa;
                        goto add1;
                    }
                    add1:
                    foreach (var item in listKhoa.Where(item => item.ID == idKhoa))
                    {
                        makhoa = item.TenKhoa;
                        goto add2;
                    }
                    add2:
                    table.Rows.Add(hs.ID, stt, hs.MaSinhVien, hs.HoSinhVien, hs.TenSinhVien, hs.NgaySinh, malop,makhoa);
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

        private void LoadForm()
        {
            LoadGrid();
            if (uG_DanhSach.Rows.Count == 0)
            {
                InsertRow();
            }
            _listAdd.Clear();
            _listUpdate.Clear();
            IdDelete.Clear();
        }

        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "MaSinhVien");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaSinhVien");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void Save()
        {
            try
            {
                btnGhi.Focus();
                foreach (var row in uG_DanhSach.Rows)
                {
                    var id = row.Cells["ID"].Value.ToString();
                    if (!string.IsNullOrEmpty(id)) continue;
                    var hs = new SinhVien
                    {
                        MaSinhVien = row.Cells["MaSinhVien"].Text,
                        HoSinhVien = row.Cells["HoSinhVien"].Text,
                        TenSinhVien = row.Cells["TenSinhVien"].Text,
                        NgaySinh = row.Cells["NgaySinh"].Text,
                        IdLop = SinhVienSql.LoadLop(row.Cells["MaLop"].Text).ID,
                    };
                    _listAdd.Add(hs);
                }
                var a = DateTime.Now.Minute;
                var v = DateTime.Now.Second;
                SinhVienSql.ThemSinhVien(_listAdd);
                MessageBox.Show((DateTime.Now.Minute - a) + ":" + (DateTime.Now.Second - v));
                QuanlysinhvienSevice.Sua(_listUpdate);
                QuanlysinhvienSevice.Xoa(IdDelete, "SinhVien");

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
                    QuanlysinhvienSevice.Xoa("SinhVien");
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

        private void Napdulieu()
        {
            var stt = uG_DanhSach.Rows.Count;
            var frmNapDuLieu = new FrmNapDuLieu(stt);
            frmNapDuLieu.ShowDialog();
            var a = DateTime.Now.Minute;
            var v = DateTime.Now.Second;
            var b = frmNapDuLieu.ResultValue;
            var table = (DataTable)uG_DanhSach.DataSource;
            table.Merge(b);
            uG_DanhSach.DataSource = table;
            MessageBox.Show((DateTime.Now.Minute - a) + @":" + (DateTime.Now.Second - v));
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
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaSinhVien"].MaxWidth = 150;
                band.Columns["HoSinhVien"].MaxWidth = 200;
                band.Columns["TenSinhVien"].MaxWidth = 150;
                //band.Columns["NgaySinh"].MaskInput = @"dd/mm/yyyy";
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = FormResource.txtMasinhvien;
                band.Columns["HoSinhVien"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSinhVien"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["MaKhoa"].Header.Caption = FormResource.txtKhoaquanly;
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
            try
            {
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                    {
                        item.MaSinhVien = uG_DanhSach.ActiveRow.Cells["MaSinhVien"].Text;
                        item.HoSinhVien = uG_DanhSach.ActiveRow.Cells["HoSinhVien"].Text;
                        item.TenSinhVien = uG_DanhSach.ActiveRow.Cells["TenSinhVien"].Text;
                        item.NgaySinh = uG_DanhSach.ActiveRow.Cells["NgaySinh"].Text;
                        item.IdLop = SinhVienSql.LoadLop(uG_DanhSach.ActiveRow.Cells["MaLop"].Text).ID;
                        return;
                    }
                    var hs = new SinhVien
                    {
                        ID = int.Parse(id),
                        MaSinhVien = uG_DanhSach.ActiveRow.Cells["MaSinhVien"].Text,
                        HoSinhVien = uG_DanhSach.ActiveRow.Cells["HoSinhVien"].Text,
                        TenSinhVien = uG_DanhSach.ActiveRow.Cells["TenSinhVien"].Text,
                        NgaySinh = uG_DanhSach.ActiveRow.Cells["NgaySinh"].Text,
                        IdLop = SinhVienSql.LoadLop(uG_DanhSach.ActiveRow.Cells["MaLop"].Text).ID
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

        #endregion

        #region Button

        private void btnNapDuLieu_Click(object sender, EventArgs e)
        {
            Napdulieu();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnInds_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kiemtrafile())
                {
                    MessageBox.Show(@"Vui lòng đóng file đang được mở.", @"Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                if (!Directory.Exists("Data"))
                    Directory.CreateDirectory("Data");

                ultraGridExcelExporter.Export(uG_DanhSach, @"Data\DanhSachSinhVien.xls");

                var mydoc = new Process();
                if (File.Exists(Application.StartupPath + @"\Data\DanhSachSinhVien.xls"))
                {
                    mydoc.StartInfo.FileName = Application.StartupPath + @"\Data\DanhSachSinhVien.xls";
                    mydoc.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public bool Kiemtrafile()
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
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            btnGhi.Focus();
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void menuStrip_dong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void napDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Napdulieu();
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.F3):
                    Xoa();
                    break;
                case (Keys.F5):
                    Save();
                    break;
                case (Keys.F11):
                    btnGhi.Focus();
                    DeleteRow();
                    break;
                case (Keys.F12):
                    LoadForm();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
                case (Keys.Insert):
                    InsertRow();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        
    }

}

