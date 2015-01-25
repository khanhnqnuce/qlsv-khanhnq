using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using Color = System.Drawing.Color;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_204_DanhSachBaiLam : FunctionControlHasGrid
    {
        private readonly IList<BaiLam> _listUpdate = new List<BaiLam>();
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;
        private readonly int _idKyThi;

        public Frm_204_DanhSachBaiLam(int idkythi)
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;

            _idKyThi = idkythi;
        }

        #region Exit

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof (int));
            table.Columns.Add("MaSV", typeof (string));
            table.Columns.Add("MaDe", typeof (string));
            table.Columns.Add("KetQua", typeof (string));
            table.Columns.Add("IdKyThi", typeof (string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(6,_idKyThi);
                pnl_from.Visible = true;
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
                Invoke((Action) (LoadGrid));
                Invoke((Action) (() => IdDelete.Clear()));
                Invoke((Action) (() => _listUpdate.Clear()));
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
            InsertRow(dgv_DanhSach, "STT", "MaSV");
        }

        protected override void DeleteRow()
        {

            DeleteRowGrid(dgv_DanhSach, "MaSV", "MaSV");
        }

        protected override void SaveDetail()
        {
            try
            {
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
                DeleteData.Xoa("BAILAM",_idKyThi);
                LoadFormDetail();
                MessageBox.Show(@"Xoá dữ liệu thành công.", @"Thông báo");

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

        private void Huy()
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

        private void SuaMaSinhVien()
        {
            var masv = int.Parse(dgv_DanhSach.ActiveRow.Cells["MaSV"].Text);
            var made = dgv_DanhSach.ActiveRow.Cells["MaDe"].Text;
            var frm = new FrmSuaMaSinhVien(masv, _idKyThi, made) {Update = false};
            frm.ShowDialog();
            if (frm.Update) 
                dgv_DanhSach.ActiveRow.Cells["MaSV"].Value = frm.txtmasinhvien.Text;
        }

        private void Timkiemmde()
        {
            try
            {
                dgv_DanhSach.DataSource = SearchData.Timkiemmade(_idKyThi,txtmade.Text);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            Rptdsbailam();
        }

        private void Rptdsbailam()
        {
            try
            {
                if(dgv_DanhSach.Rows.Count == 0) return;
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
                rptdsbailam.FilePath = Application.StartupPath + @"\Reports\dsbailam.rst";
                rptdsbailam.Prepare();
                rptdsbailam.GetReportParameter += GetParameter;
                var previewForm = new PreviewForm(rptdsbailam)
                {
                    WindowState = FormWindowState.Maximized
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void GetParameter(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                var tb = LoadData.Load(3, _idKyThi);
                foreach (DataRow row in tb.Rows)
                {
                    e.Parameters["TenKT"].Value = row["TenKT"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        #region Event uG

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["IdKyThi"].Hidden = true;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaDe"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.ActivateOnly;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].MinWidth = 60;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaSV"].MinWidth = 140;
                band.Columns["MaSV"].MaxWidth = 150;
                band.Columns["MaDe"].MinWidth = 140;
                band.Columns["MaDe"].MaxWidth = 150;
                band.Columns["KetQua"].MinWidth = 640;
                band.Columns["KetQua"].MaxWidth = 650;

                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã sinh viên";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["KetQua"].Header.Caption = @"Đáp án bài làm";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void dgv_DanhSach_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            SuaMaSinhVien();
        }

        #endregion

        private void FrmDanhSachBaiLam_Load(object sender, EventArgs e)
        {
            Huy();
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

        private void txtmade_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (string.IsNullOrEmpty(txtmade.Text)) return;
                        Timkiemmde();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtmade.Text))return;
            Timkiemmde();
        }

        private void btntimkiemsinhvien_Click(object sender, EventArgs e)
        {
            _frmTimkiem.ShowDialog();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        private void txtmade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
    }
}
