using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_105_QuanLySinhVien : FunctionControlHasGrid
    {
        #region Create

        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private readonly IList<SinhVien> _listUpdate = new List<SinhVien>();
        private readonly FrmTimkiem _frmTimkiem;
        private FrmThemsinhvien _frmThemsinhvien;
        private UltraGridRow _newRow;

        #endregion

        public Frm_105_QuanLySinhVien()
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
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
                dgv_DanhSach.DataSource = LoadData.Load(1);
                pnl_from.Visible = true;
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
                Invoke((Action)(LoadGrid));
                Invoke((Action)(()=>cbokhoa.Value = null));
                Invoke((Action)(()=>cbolop.Value = null));
                Invoke((Action)(()=>_listAdd.Clear()));
                Invoke((Action)(()=>_listUpdate.Clear()));
                Invoke((Action)(()=>IdDelete.Clear()));
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "MaSinhVien");
        }

        protected override void DeleteRow()
        {

            DeleteRowGrid(dgv_DanhSach, "ID", "MaSinhVien");
        }

        protected override void SaveDetail()
        {
            try
            {
                QlsvSevice.Xoa(IdDelete, "SinhVien");
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("SinhVien");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Themmoi()
        {
            _frmThemsinhvien = new FrmThemsinhvien();
            _frmThemsinhvien.Themmoisinhvien += Themmoisinhvien;
            _frmThemsinhvien.ShowDialog();
        }

        private void Sua()
        {
            try
            {
                if(string.IsNullOrEmpty(dgv_DanhSach.ActiveRow.Cells["ID"].Text)) return;
                var frm = new FrmThemsinhvien
                {
                    Id = int.Parse(dgv_DanhSach.ActiveRow.Cells["ID"].Text),
                    txtmasinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["MaSinhVien"].Text, ReadOnly = true},
                    txthotendem = {Text = dgv_DanhSach.ActiveRow.Cells["HoSinhVien"].Text},
                    txttensinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["TenSinhVien"].Text},
                    cbongaysinh = {Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text},
                };
                frm.cbolop.Value = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdLop"].Text);
                frm.cbokhoa.Value = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdKhoa"].Text);
                frm.ShowDialog();
                if (frm.Id == 0)
                {
                    dgv_DanhSach.ActiveRow.Cells["HoSinhVien"].Value = frm.txthotendem.Text;
                    dgv_DanhSach.ActiveRow.Cells["TenSinhVien"].Value = frm.txttensinhvien.Text;
                    dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Value = frm.cbongaysinh.Text;
                    dgv_DanhSach.ActiveRow.Cells["MaLop"].Value = frm.cbolop.Text;
                    dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Value = frm.cbokhoa.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Huy()
        {
            try
            {
                var thread = new Thread(LoadFormDetail) {IsBackground = true};
                thread.Start();
                OnShowDialog("Loading...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Timkiemsinhvien(object sender, string masinhvien)
        {
            try
            {
                if (_newRow != null) _newRow.Selected = false;
                foreach (
                    var row in dgv_DanhSach.Rows.Where(row => row.Cells["MaSinhVien"].Value.ToString() == masinhvien))
                {
                    dgv_DanhSach.ActiveRowScrollRegion.ScrollPosition = row.Index;
                    row.Selected = true;
                    _newRow = row;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                var row = dgv_DanhSach.DisplayLayout.Bands[0].AddNew();
                var stt = dgv_DanhSach.Rows.Count;
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
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Rptdanhsach()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\danhsachsinhvien.rst";
            using (var previewForm = new PreviewForm(rptdanhsachsinhvien))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachsinhvien.Prepare();
                previewForm.ShowDialog();
            }
        }
        
        #endregion

        #region Event uG

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = FormResource.txtMasinhvien;
                band.Columns["HoSinhVien"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSinhVien"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["TenKhoa"].Header.Caption = FormResource.txtKhoaquanly;
                band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;

                #endregion
                
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["HoSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["TenSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["TenKhoa"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSinhVien"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["ID"].Hidden = true;
                band.Columns["IdLop"].Hidden = true;
                band.Columns["IdKhoa"].Hidden = true;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["HoSinhVien"].Width = 170;
                band.Columns["TenSinhVien"].Width = 150;
                band.Columns["NgaySinh"].Width = 150;
                band.Columns["MaLop"].Width = 150;
                band.Columns["TenKhoa"].Width = 350;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;
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
                MessageBox.Show(ex.Message);
            }
        }

        private void uG_DanhSach_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            Sua();
        }

        #endregion

        #region MenuStrip

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            Themmoi();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void menuStrip_dong_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void menuStrip_Sua_Click(object sender, EventArgs e)
        {
           Sua();
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
                var a = SearchData.Timkiemtheokhoa(int.Parse(cbokhoa.Value.ToString()));
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa(int.Parse(cbokhoa.Value.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                dgv_DanhSach.DataSource = SearchData.Timkiemtheolop(int.Parse(cbolop.Value.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                cbokhoa.DataSource = LoadData.Load(3);
                cbokhoa.ValueMember = "ID";
                cbokhoa.DisplayMember = "TenKhoa";
                cbokhoa.Rows.Band.Columns["ID"].Hidden = true;
                cbokhoa.Rows.Band.Columns["MaKhoa"].Hidden = true;
                cbokhoa.Rows.Band.Columns["TenKhoa"].Width = 250;
                cbokhoa.Rows.Band.ColHeadersVisible = false;

                cbolop.DataSource = LoadData.Load(4);
                cbolop.ValueMember = "ID";
                cbolop.DisplayMember = "MaLop";
                cbolop.Rows.Band.Columns["ID"].Hidden = true;
                cbolop.Rows.Band.Columns["IdKhoa"].Hidden = true;
                cbolop.Rows.Band.Columns["GhiChu"].Hidden = true;
                cbolop.Rows.Band.ColHeadersVisible = false;
                cbolop.DropDownWidth = 0;

                Huy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.S):
                    _frmTimkiem.ShowDialog();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

