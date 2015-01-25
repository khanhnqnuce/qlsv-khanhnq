using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_202_DanhSachDapAn : FunctionControlHasGrid
    {
        private readonly IList<DapAn> _listUpdate = new List<DapAn>();
        private int _idKyThi;
        public Frm_202_DanhSachDapAn(int idkythi)
        {
            InitializeComponent();
            _idKyThi = idkythi;
        }

        #region Exit

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("MaMon", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("CauHoi", typeof(string));
            table.Columns.Add("Dapan", typeof(string));
            table.Columns.Add("IdKyThi", typeof(string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(7,_idKyThi);
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
                Invoke((Action)(LoadGrid));
                Invoke((Action)(() => IdDelete.Clear()));
                Invoke((Action)(() => _listUpdate.Clear()));
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

        protected override void SaveDetail()
        {
            try
            {
                UpdateData.UpdateDapAn(_listUpdate);
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
                DeleteData.Xoa("DAPAN",_idKyThi);
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Huy()
        {
            try
            {
                var thread = new Thread(LoadFormDetail) { IsBackground = true };
                thread.Start();
                OnShowDialog("Loading...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void RptDapAn()
        {
            var tb = LoadData.Load(7, _idKyThi);
            if(tb.Rows.Count == 0) return;
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdapandethi.FilePath = Application.StartupPath + @"\Reports\dapandethi.rst";
            rptdapandethi.GetReportParameter += GetParameter;
            rptdapandethi.Prepare();
            var previewForm = new PreviewForm(rptdapandethi)
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false
            };
            previewForm.Show();
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
                    e.Parameters["NgayThi"].Value = row["NgayThi"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            RptDapAn();
        }

        private void Timkiemmde()
        {
            try
            {
                dgv_DanhSach.DataSource = SearchData.Timkiemmade1(_idKyThi, txtmade.Text);
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

                band.Columns["ThangDiem"].Hidden = true;

                band.Override.CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["MaMon"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["CauHoi"].CellActivation = Activation.NoEdit;
                band.Columns["ThangDiem"].CellActivation = Activation.NoEdit;
                
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaMon"].MinWidth = 140;
                band.Columns["MaDe"].MinWidth = 140;
                band.Columns["CauHoi"].MinWidth = 140;
                band.Columns["Dapan"].MinWidth = 140;
                band.Columns["ThangDiem"].MinWidth = 140;
                band.Columns["MaMon"].MaxWidth = 150;
                band.Columns["MaDe"].MaxWidth = 150;
                band.Columns["CauHoi"].MaxWidth = 150;
                band.Columns["Dapan"].MaxWidth = 150;
                band.Columns["ThangDiem"].MaxWidth = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaMon"].Header.Caption = @"Mã môn thi";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["CauHoi"].Header.Caption = @"Câu hỏi";
                band.Columns["Dapan"].Header.Caption = @"Đáp án";
                band.Columns["ThangDiem"].Header.Caption = @"Thang điểm";

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void dgv_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                var hs = new DapAn
                {
                    IdKyThi = _idKyThi,
                    MaMon = dgv_DanhSach.ActiveRow.Cells["MaMon"].Text,
                    MaDe = dgv_DanhSach.ActiveRow.Cells["MaDe"].Text,
                    CauHoi = int.Parse(dgv_DanhSach.ActiveRow.Cells["CauHoi"].Text),
                    Dapan = dgv_DanhSach.ActiveRow.Cells["Dapan"].Text,
                };
                _listUpdate.Add(hs);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion
       
        private void FrmDapAnCacMaDe_Load(object sender, EventArgs e)
        {
            Huy();
        }

        private void txtmade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
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
            Timkiemmde();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
        
    }
}
