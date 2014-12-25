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
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_207_ChamDiemThi : FunctionControlHasGrid
    {
        private readonly IList<BaiLam> _listUpdate = new List<BaiLam>();
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;
        private readonly BackgroundWorker _bgwInsert;

        public Frm_207_ChamDiemThi()
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;

            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
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

        protected override void LoadGrid()
        {
            try
            {
                var tbbailam = LoadData.Load(12);
                dgv_DanhSach.DataSource = tbbailam;
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
                LoadGrid();
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

        protected override void XoaDetail()
        {
            try
            {
                //DeleteData.Xoa("BaiLam");
                LoadFormDetail();
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

        #endregion

        #region Event uG

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                band.Columns["ID"].Hidden = true;
                band.Columns["IdKyThi"].Hidden = true;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSinhVien"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaDe"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["DiemThi"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.ActivateOnly;
                //band.Columns["KetQua"].CellAppearanc

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].Width = 50;
                band.Columns["MaSinhVien"].Width = 150;
                band.Columns["MaDe"].Width = 150;
                band.Columns["KetQua"].Width = 650;
                band.Columns["DiemThi"].Width = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = @"Mã sinh viên";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["KetQua"].Header.Caption = @"Bài làm sinh viên";
                band.Columns["DiemThi"].Header.Caption = @"Điểm thi";

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            _threads[0] = new Thread(Chamthi) {IsBackground = true};
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

        private readonly Thread[] _threads = new Thread[2];
        readonly FrmLoadding _frm = new FrmLoadding();

        private void btnchamthi_Click(object sender, EventArgs e)
        {
            _threads[0] = new Thread(Chamthi) {IsBackground = true};
            _threads[0].Start();

            Loadding();
        }

        private void Chamthi()
        {
            var tbbailam = LoadData.Load(12);
            foreach (DataRow dataRow in tbbailam.Rows)
            {
                var diem = 0;
                var listbailam = dataRow["KetQua"].ToString();
                var tbdapan = SearchData.Timkiemmade2(dataRow["MaDe"].ToString());
                for (var i = 0; i < tbdapan.Rows.Count; i++)
                {
                    var a = listbailam[i].ToString();
                    var b = tbdapan.Rows[i]["Dapan"].ToString();
                    var c = tbdapan.Rows[i]["ThangDiem"].ToString();
                    if (a == b)
                    {
                        diem = diem + int.Parse(c);
                    }
                }
                var hs = new BaiLam
                {
                    MaSV = int.Parse(dataRow["MaSV"].ToString()),
                    DiemThi = diem
                };
                _listUpdate.Add(hs);
                dataRow["DiemThi"] = diem.ToString();
            }
            Invoke((Action) (() => dgv_DanhSach.DataSource = tbbailam));
            Invoke((Action)(() => pnl_from.Visible = true));
            lock (LockTotal)
            {
                OnCloseDialog();
            }
        }

        private void Loadding()
        {
            _frm.Update("Đang chấm thi...");
            _frm.ShowDialog();
        }
    }
}
