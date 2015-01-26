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
using NPOI.HSSF.Record.Chart;
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
        private readonly IList<DiemThi> _listThongke = new List<DiemThi>();
        private readonly int _idkythi;
        private readonly FrmTimkiem _frmTimkiem;
        private DataTable _tbError = new DataTable();
        private UltraGridRow _newRow;
        private readonly BackgroundWorker _bgwInsert;
        private readonly BackgroundWorker _bgwInsert1;
        private readonly BackgroundWorker _bgwInsert2;
        private int _idnamhoc;
        private string _hocky;

        public Frm_207_ChamDiemThi(int idkythi)
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;

            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
            
            _bgwInsert1 = new BackgroundWorker();
            _bgwInsert1.DoWork += bgwInsert_DoWork1;
            _bgwInsert1.RunWorkerCompleted += bgwInsert_RunWorkerCompleted1;
            
            _bgwInsert2 = new BackgroundWorker();
            _bgwInsert2.DoWork += bgwInsert_DoWork2;
            _bgwInsert2.RunWorkerCompleted += bgwInsert_RunWorkerCompleted2;

            _idkythi = idkythi;
        }

        #region Exit

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("KetQua", typeof(string));
            return table;
        }

        /// <summary>
        /// chấm tthi
        /// </summary>
        protected virtual void LoadGrid()
        {
            var stt = 1;
            _tbError = GetTable();
            var tbbailam = LoadData.Load(207, _idkythi);
            var tabledapan = LoadData.Load(7, _idkythi);
            if (tabledapan.Rows.Count==0)
            {
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
                Invoke(
                        (Action)(() => MessageBox.Show(@"Chưa Import đáp án của mã đề", @"Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)));
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
                return;
            }
            if (tbbailam.Rows.Count>0)
            {
                foreach (DataRow dataRow in tbbailam.Rows)
                {
                    double diem = 0;
                    var listbailam = dataRow["KetQua"].ToString();
                    var tbdapan = SearchData.Timkiemmade2(dataRow["MaDe"].ToString(), _idkythi);
                    if (listbailam.Length != tbdapan.Rows.Count)
                    {
                        _tbError.Rows.Add(stt++,
                            dataRow["MaSV"].ToString(),
                            dataRow["MaDe"].ToString(),
                            dataRow["KetQua"].ToString()
                            );
                        continue;
                    }
                    for (var i = 0; i < tbdapan.Rows.Count; i++)
                    {
                        var a = listbailam[i].ToString();
                        var s = tbdapan.Rows[i]["Dapan"].ToString();
                        var c = tbdapan.Rows[i]["ThangDiem"].ToString();
                        if (a == s)
                        {
                            diem = diem + double.Parse(c);
                        }
                    }
                    var d = Math.Round(diem, 1);
                    var hs = new BaiLam
                    {
                        IdKyThi = _idkythi,
                        MaSV = int.Parse(dataRow["MaSV"].ToString()),
                        DiemThi = d
                    };
                    _listUpdate.Add(hs);
                    dataRow["DiemThi"] = d;
                }
                Invoke((Action)(() => dgv_DanhSach.DataSource = tbbailam));
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
            }
            else
            {
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
                Invoke(
                        (Action)(() => MessageBox.Show(@"Chưa Import bài làm của sinh viên", @"Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)));
            }
        }

        protected override void LoadFormDetail()
        {
            try
            {
                var tb = SearchData.KtraChamThi(_idkythi);
                if (tb.Rows.Count > 0)
                {
                    if (DialogResult.No ==
                        MessageBox.Show(@"Bài thi của sv đã được chấm bạn có muốn chấm lại không.",
                            FormResource.MsgCaption,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        dgv_DanhSach.DataSource = tb;
                        pnl_from.Visible = true;
                        return;
                    }
                }
                var thread = new Thread(LoadGrid) {IsBackground = true};
                thread.Start();
                OnShowDialog("Loading...");
                if (_tbError.Rows.Count > 0)
                {
                    var text = @"Còn " + _tbError.Rows.Count + @" bài thi chưa được chấm";
                    var frm = new FrmMsgImportSv(text, _tbError, 2);
                    frm.ShowDialog();
                }
                pnl_from.Visible = true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                if (_listUpdate.Count > 0)
                {
                    UpdateData.UpdateDiemThi(_listUpdate);
                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void SaveDetail1()
        {
            try
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    var hs = new DiemThi
                    {
                        MaSV = int.Parse(row.Cells["MaSV"].Text),
                        Diem = double.Parse(row.Cells["DiemThi"].Text),
                        IdNamHoc = _idnamhoc,
                        HocKy = _hocky
                    };
                    _listThongke.Add(hs);
                }
                if (_listThongke.Count > 0 || _listUpdate.Count > 0)
                {
                    UpdateData.UpdateDiemThi(_listUpdate);
                    InsertData.ThemThongKe(_listThongke);
                    MessageBox.Show(@"Lưu lại thành công");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void SaveDetail2()
        {
            try
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    var hs = new DiemThi
                    {
                        MaSV = int.Parse(row.Cells["MaSV"].Text),
                        Diem = double.Parse(row.Cells["DiemThi"].Text),
                        IdNamHoc = _idnamhoc,
                        HocKy = _hocky
                    };
                    _listThongke.Add(hs);
                }
                if (_listThongke.Count > 0)
                {
                    InsertData.ThemThongKe(_listThongke);
                    MessageBox.Show(@"Lưu lại thành công");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Ghi()
        {
            if (_tbError.Rows.Count > 0)
            {
                if (DialogResult.No ==
                    MessageBox.Show(@"Một số bài thi chưa được chấm. Bạn có muốn lưu lại không ?",
                    FormResource.MsgCaption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    return;
                }
            }

            var frm = new FrmLuuDiemThi { bUpdate = false };
            frm.ShowDialog();
            if (frm.bUpdate && frm.rdokythi.Checked)
            {
                if (dgv_DanhSach.Rows.Count <= 0) return;
                _bgwInsert.RunWorkerAsync();
                OnShowDialog("Đang lưu dữ liệu");
            }
            else if (frm.bUpdate && frm.rdonamhoc.Checked)
            {
                var frm1 = new FrmGopKQ { Update = false };
                frm1.ShowDialog();
                if (!frm1.Update) return;
                _idnamhoc = int.Parse(frm1.cboNamHoc.SelectedValue.ToString());
                _hocky = frm1.cbohocky.SelectedValue.ToString();
                _bgwInsert1.RunWorkerAsync();
                OnShowDialog("Đang lưu dữ liệu");
            }
            else if (frm.bUpdate && frm.rdolucahai.Checked)
            {
                var frm2 = new FrmGopKQ { Update = false };
                frm2.ShowDialog();
                if (!frm2.Update) return;
                _idnamhoc = int.Parse(frm2.cboNamHoc.SelectedValue.ToString());
                _hocky = frm2.cbohocky.SelectedValue.ToString();
                _bgwInsert2.RunWorkerAsync();
                OnShowDialog("Đang lưu dữ liệu");

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
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            RptLop();
        }

        private void RptLop()
        {
            var tblop = LoadData.Load(4, _idkythi);
            var tb = LoadData.Load(10, _idkythi);
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
            rptdiemthi.Prepare();
            rptdiemthi.GetReportParameter += GetParameter;
            var previewForm = new PreviewForm(rptdiemthi)
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
                var tb = LoadData.Load(3, _idkythi);
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
                band.Columns["MaHoiDong"].Hidden = true;
                band.Columns["MaLoCham"].Hidden = true;
                band.Columns["TenFile"].Hidden = true;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaDe"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["DiemThi"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["DiemThi"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.ActivateOnly;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                
                band.Columns["STT"].MinWidth = 50;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaSV"].MinWidth = 140;
                band.Columns["MaSV"].MaxWidth = 150;
                band.Columns["MaDe"].MinWidth = 140;
                band.Columns["MaDe"].MaxWidth = 150;
                band.Columns["KetQua"].MinWidth = 640;
                band.Columns["KetQua"].MaxWidth = 650;
                band.Columns["DiemThi"].MinWidth = 140;
                band.Columns["DiemThi"].MaxWidth = 150;

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

        private void bgwInsert_DoWork1(object sender, DoWorkEventArgs e)
        {
            try
            {
                SaveDetail1();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void bgwInsert_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        private void bgwInsert_DoWork2(object sender, DoWorkEventArgs e)
        {
            try
            {
                SaveDetail2();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void bgwInsert_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        #endregion

        private void FrmDanhSachBaiLam_Load(object sender, EventArgs e)
        {
            LoadFormDetail();
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

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
    }
}
