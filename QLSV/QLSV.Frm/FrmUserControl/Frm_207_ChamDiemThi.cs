using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_207_ChamDiemThi : FunctionControlHasGrid
    {
        private readonly IList<BaiLam> _listUpdate = new List<BaiLam>();
        private readonly int _idkythi;
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;
        private readonly BackgroundWorker _bgwInsert;
        private readonly Thread[] _threads = new Thread[2];

        public Frm_207_ChamDiemThi(int idkythi)
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;

            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;

            _idkythi = idkythi;
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSinhVien", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("KetQua", typeof(string));
            table.Columns.Add("IdKyThi", typeof(string));
            return table;
        }

        protected override void SaveDetail()
        {
            try
            {
                UpdateData.UpdateDiemThi(_listUpdate);
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Ghi()
        {
            if (dgv_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
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
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Chamthi()
        {
            var tbbailam = LoadData.Load(16, _idkythi);
            foreach (DataRow dataRow in tbbailam.Rows)
            {
                var diem = 0;
                var listbailam = dataRow["KetQua"].ToString();
                var tbdapan = SearchData.Timkiemmade2(dataRow["MaDe"].ToString(), _idkythi);
                for (var i = 0; i < tbdapan.Rows.Count; i++)
                {
                    var a = listbailam[i].ToString();
                    var s = tbdapan.Rows[i]["Dapan"].ToString();
                    var c = tbdapan.Rows[i]["ThangDiem"].ToString();
                    if (a == s)
                    {
                        diem = diem + int.Parse(c);
                    }
                }
                var hs = new BaiLam
                {
                    IdKyThi = _idkythi,
                    MaSV = int.Parse(dataRow["MaSV"].ToString()),
                    DiemThi = diem
                };
                _listUpdate.Add(hs);
                dataRow["DiemThi"] = diem.ToString();
            }
            Invoke((Action)(() => dgv_DanhSach.DataSource = tbbailam));
            Invoke((Action)(() => pnl_from.Visible = true));
            lock (LockTotal)
            {
                OnCloseDialog();
            }
        }

        public void InDanhSach()
        {
            RptLop();
        }

        private void RptLop()
        {
            var tblop = LoadData.Load(4, _idkythi);
            var tb = LoadData.Load(10,_idkythi);
            foreach (DataRow rowl in tblop.Rows)
            {
                var stt = 1;
                var malop = rowl["MaLop"].ToString();
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["MaLop"].ToString().Equals(malop)))
                {
                    row["STT"] = stt++;
                }
            }
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdiemthi.FilePath = Application.StartupPath + @"\Reports\diemthi.rst";
            using (var previewForm = new PreviewForm(rptdiemthi))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdiemthi.GetReportParameter += GetParameter;
                rptdiemthi.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void GetParameter(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                var tb = LoadData.Load(3, _idkythi);
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
                band.Columns["DiemThi"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.ActivateOnly;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].Width = 50;
                band.Columns["MaSV"].Width = 150;
                band.Columns["MaDe"].Width = 150;
                band.Columns["KetQua"].Width = 650;
                band.Columns["DiemThi"].Width = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã sinh viên";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["KetQua"].Header.Caption = @"Bài làm sinh viên";
                band.Columns["DiemThi"].Header.Caption = @"Điểm thi";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        #region MenuStrip

        #endregion

        #region BackgroundWorker

        private void bgwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SaveDetail();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void bgwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        #endregion

        private void FrmDanhSachBaiLam_Load(object sender, EventArgs e)
        {
            _threads[0] = new Thread(Chamthi) { IsBackground = true };
            _threads[0].Start();

            OnShowDialog("Đang chấm thi...");
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
