using System;
using System.Collections.Generic;
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
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_104_QuanLySinhVien : FunctionControlHasGrid
    {
        #region Create

        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private readonly IList<SinhVien> _listUpdate = new List<SinhVien>();
        private readonly FrmTimkiem _frmTimkiem;
        private FrmThemsinhvien _frmThemsinhvien;
        private UltraGridRow _newRow;

        #endregion

        public Frm_104_QuanLySinhVien()
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("MaSV", typeof (string));
            table.Columns.Add("HoSV", typeof (string));
            table.Columns.Add("TenSV", typeof (string));
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
                lbsiso.Text = "";
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void LoadFormDetail()
        {
            try
            {
                Invoke((Action)(LoadGrid));
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
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            //InsertRow(dgv_DanhSach, "STT", "MaSV");
        }

        protected override void DeleteRow()
        {

            DeleteRowGrid(dgv_DanhSach, "MaSV", "MaSV");
        }

        protected override void SaveDetail()
        {
            try
            {
                if(IdDelete.Count<=0)return;
                DeleteData.XoaSV(IdDelete);
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();
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
                DeleteData.Xoa("SINHVIEN");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
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
                if (string.IsNullOrEmpty(dgv_DanhSach.ActiveRow.Cells["MaSV"].Text)) return;
                var frm = new FrmThemsinhvien
                {
                    txtmasinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["MaSV"].Text, ReadOnly = true},
                    txthotendem = {Text = dgv_DanhSach.ActiveRow.Cells["HoSV"].Text},
                    txttensinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["TenSV"].Text},
                    cbongaysinh = {Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text},
                };
                frm.CheckUpdate = true;
                frm.cbolop.Value = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdLop"].Text);
                frm.cbokhoa.Value = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdKhoa"].Text);
                frm.ShowDialog();
                if (frm.CheckUpdate) return;
                dgv_DanhSach.ActiveRow.Cells["HoSV"].Value = frm.txthotendem.Text;
                dgv_DanhSach.ActiveRow.Cells["TenSV"].Value = frm.txttensinhvien.Text;
                dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Value = frm.cbongaysinh.Text;
                dgv_DanhSach.ActiveRow.Cells["MaLop"].Value = frm.cbolop.Text;
                dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Value = frm.cbokhoa.Text;
            }
            catch (Exception ex)
            {
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
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Timkiemsinhvien(object sender, string masinhvien)
        {
            try
            {
                if (_newRow != null) _newRow.Selected = false;
                foreach (
                    var row in dgv_DanhSach.Rows.Where(row => row.Cells["MaSV"].Value.ToString() == masinhvien))
                {
                    dgv_DanhSach.ActiveRowScrollRegion.ScrollPosition = row.Index;
                    row.Selected = true;
                    _newRow = row;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Themmoisinhvien(object sender, SinhVien hs, string malop, string tenkhoa)
        {
            try

            {
                var row = dgv_DanhSach.DisplayLayout.Bands[0].AddNew();
                var stt = dgv_DanhSach.Rows.Count;
                row.Cells["STT"].Value = stt;
                row.Cells["MaSV"].Value = hs.MaSV;
                row.Cells["HoSV"].Value = hs.HoSV;
                row.Cells["TenSV"].Value = hs.TenSV;
                row.Cells["NgaySinh"].Value = hs.NgaySinh;
                row.Cells["MaLop"].Value = malop;
                row.Cells["TenKhoa"].Value = tenkhoa;
                row.Cells["MaSV"].Activate();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            try
            {
                var frm = new FrmRptDanhSachSinhVien {bUpdate = false};
                frm.ShowDialog();
                if (frm.rdokhoa.Checked && frm.bUpdate)
                    RptKhoa();
                else if(frm.rdoLop.Checked && frm.bUpdate)
                    RptLop();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void RptKhoa()
        {

            try
            {
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
                rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\dsSvKhoa.rst";
                rptdanhsachsinhvien.Prepare();
                var previewForm = new PreviewForm(rptdanhsachsinhvien)
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false,
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
        
        public void RptLop()
        {

            try
            {
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
                rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\dsSvLop.rst";
                rptdanhsachsinhvien.Prepare();
                var previewForm = new PreviewForm(rptdanhsachsinhvien)
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
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
                
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                #region Caption
                band.Groups.Clear();
                var columns = band.Columns;
                band.ColHeadersVisible = false;
                var group5 = band.Groups.Add("STT");
                var group0 = band.Groups.Add("Mã SV");
                var group1 = band.Groups.Add("Họ và tên");
                var group2 = band.Groups.Add("Ngày sinh");
                var group3 = band.Groups.Add("Lớp");
                var group4 = band.Groups.Add("Khoa");
                columns["STT"].Group = group5;
                columns["MaSV"].Group = group0;
                columns["HoSV"].Group = group1;
                columns["TenSV"].Group = group1;
                columns["NgaySinh"].Group = group2;
                columns["MaLop"].Group = group3;
                columns["TenKhoa"].Group = group4;

                #endregion

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["HoSV"].CellActivation = Activation.NoEdit;
                band.Columns["TenSV"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["TenKhoa"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["IdLop"].Hidden = true;
                band.Columns["IdKhoa"].Hidden = true;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                #region Size
                band.Columns["STT"].MinWidth = 50;
                band.Columns["STT"].MaxWidth = 50;
                band.Columns["MaSV"].MinWidth = 100;
                band.Columns["MaSV"].MaxWidth = 120;
                band.Columns["HoSV"].MinWidth = 130;
                band.Columns["HoSV"].MaxWidth = 150;
                band.Columns["TenSV"].MinWidth = 90;
                band.Columns["TenSV"].MaxWidth = 100;
                band.Columns["NgaySinh"].MinWidth = 100;
                band.Columns["NgaySinh"].MaxWidth = 100;
                band.Columns["MaLop"].MinWidth = 100;
                band.Columns["MaLop"].MaxWidth = 110;
                band.Columns["TenKhoa"].MinWidth = 270;
                band.Columns["TenKhoa"].MaxWidth = 290;
                #endregion                
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;
            }
            catch (Exception ex)
            {
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

        private void Timkiem()
        {
            try
            {
                var indexkhoa = cbokhoa.SelectedValue;
                var indexlop = cbolop.SelectedValue;
                lbsiso.Text = "";
                if (indexlop == null)
                {
                    if(indexkhoa == null) return;
                    if(IsNumber(indexkhoa.ToString()))
                    dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa((int)indexkhoa);
                    if (dgv_DanhSach.Rows.Count > 0)
                        lbsiso.Text = @" Sĩ số: " + dgv_DanhSach.Rows.Count;
                }
                else
                {
                    if (IsNumber(indexlop.ToString()))
                    dgv_DanhSach.DataSource = SearchData.Timkiemtheolop((int)indexlop);
                    if (dgv_DanhSach.Rows.Count > 0)
                        lbsiso.Text = @" Sĩ số: " + dgv_DanhSach.Rows.Count;
                }

                
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private static bool IsNumber(string pText)
        {
            var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        #endregion

        private void txtkhoa_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(txtKhoa.Text)) return;
                        dgv_DanhSach.DataSource = SearchData.Timkiemnienkhoa(txtKhoa.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void txtkhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtkhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                Huy();
                //--------Khoa------------
                var table = LoadData.Load(3);
                var tb = new DataTable();
                tb.Columns.Add("ID", typeof(string));
                tb.Columns.Add("TenKhoa", typeof(string));
                tb.Rows.Add("0", "- Tất cả các khoa -");
                foreach (DataRow row in table.Rows)
                {
                    tb.Rows.Add(row["ID"].ToString(), row["TenKhoa"].ToString());
                }
                cbokhoa.DataSource = tb;
                //------------Lớp-----------
                var tb1 = new DataTable();
                tb1.Columns.Add("ID", typeof(string));
                tb1.Columns.Add("MaLop", typeof(string));
                tb1.Rows.Add("0", "- Chọn lớp -");
                cbolop.DataSource = tb1;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = cbokhoa.SelectedValue;
                if (obj == null || obj.ToString().Equals("0"))
                {
                    var tb1 = new DataTable();
                    tb1.Columns.Add("ID", typeof(string));
                    tb1.Columns.Add("MaLop", typeof(string));
                    tb1.Rows.Add("0", "- Chọn lớp -");
                    cbolop.DataSource = tb1;
                    LoadGrid();
                    return;
                }
                //dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa(int.Parse(obj.ToString()));

                var table = SearchData.LoadCboLop(int.Parse(obj.ToString()));
                var tb = new DataTable();
                tb.Columns.Add("ID", typeof(string));
                tb.Columns.Add("MaLop", typeof(string));
                tb.Rows.Add("0", "- Tất cả các lớp -");
                foreach (DataRow row in table.Rows)
                {
                    tb.Rows.Add(row["ID"].ToString(), row["MaLop"].ToString());
                }
                cbolop.DataSource = tb;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbolop_SelectedValueChanged(object sender, EventArgs e)
        {
            var obj = cbolop.SelectedValue;
            if (obj == null || obj.ToString().Equals("0"))
            {
                if (cbokhoa.SelectedValue.ToString().Equals("0")) return;
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa(int.Parse(cbokhoa.SelectedValue.ToString()));
                return;
            }
            dgv_DanhSach.DataSource = SearchData.Timkiemtheolop(int.Parse(obj.ToString()));
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtKhoa.Text)) return;
            dgv_DanhSach.DataSource = SearchData.Timkiemnienkhoa(txtKhoa.Text);
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.S):
                    _frmTimkiem.ShowDialog();
                    break;
                case (Keys.Enter):
                    Timkiem();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
       
    }
}

