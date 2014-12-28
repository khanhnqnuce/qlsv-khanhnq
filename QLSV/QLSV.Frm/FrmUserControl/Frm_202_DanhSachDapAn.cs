using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("MaMon", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("CauHoi", typeof(string));
            table.Columns.Add("Dapan", typeof(string));
            table.Columns.Add("IdKyThi", typeof(string));
            return table;
        }

        protected override void LoadGrid()
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

        //protected override void InsertRow()
        //{
        //    InsertRow(dgv_DanhSach, "STT", "MaMon");
        //}

        //protected override void DeleteRow()
        //{

        //    DeleteRowGrid(dgv_DanhSach, "ID", "MaMon");
        //}

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
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", LoadData.Load(7,_idKyThi));
            rptdapandethi.FilePath = Application.StartupPath + @"\Reports\dapandethi.rst";
            using (var previewForm = new PreviewForm(rptdapandethi))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdapandethi.GetReportParameter += GetParameter;
                rptdapandethi.Prepare();
                previewForm.ShowDialog();
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
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaMon"].Width = 150;
                band.Columns["MaDe"].Width = 150;
                band.Columns["CauHoi"].Width = 150;
                band.Columns["Dapan"].Width = 150;
                band.Columns["ThangDiem"].Width = 150;
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
                    CauHoi = dgv_DanhSach.ActiveRow.Cells["CauHoi"].Text,
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

        #region MenuStrip
        
        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void menuStrip_Luulai_Click(object sender, EventArgs e)
        {
            SaveDetail();
        }

        private void menuStrip_In_Click(object sender, EventArgs e)
        {
            RptDapAn();
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
        
    }
}
