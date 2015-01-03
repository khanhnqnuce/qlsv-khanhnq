using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_109_SapXepPhongThi : FunctionControlHasGrid
    {
        #region Create

        private readonly IList<XepPhong> _listXepPhong = new List<XepPhong>();
        private readonly IList<KTPhong> _listPhanPhong = new List<KTPhong>();
        private readonly int _idkythi;
        private DataTable _tbSv = new DataTable();
        private DataTable _tbPhong = new DataTable();
        private UltraGridRow _newRow;
        private readonly BackgroundWorker _bgwInsert;

        #endregion

        public Frm_109_SapXepPhongThi(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("IdSV", typeof(string));
            table.Columns.Add("HoSV", typeof (string));
            table.Columns.Add("TenSV", typeof (string));
            table.Columns.Add("NgaySinh", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("PhongThi", typeof(string));
            return table;
        }

        private void Sua()
        {
            try
            {
                if (!string.IsNullOrEmpty(dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text))
                {
                    MessageBox.Show(@"Sinh viên đã được xếp phòng");
                    return;
                }
                var frm = new FrmXepPhong
                {
                    txtmasinhvien = { Text = dgv_DanhSach.ActiveRow.Cells["IdSV"].Text },
                    txthotendem = { Text = dgv_DanhSach.ActiveRow.Cells["HoSV"].Text },
                    txttensinhvien = { Text = dgv_DanhSach.ActiveRow.Cells["TenSV"].Text },
                    txtNgaySinh = { Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text },
                    cbolop = { Text = dgv_DanhSach.ActiveRow.Cells["MaLop"].Text },
                    IdKythi = _idkythi,
                    bUpdate= false
                };
                frm.ShowDialog();
                if (!frm.bUpdate) return;
                MessageBox.Show(@"Lưu lại thành công", @"Thông báo");
                dgv_DanhSach.ActiveRow.Cells["PhongThi"].Value = frm.cboPhongthi.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Xepphong()
        {
            var kt = 0;
            var tong = _tbSv.Rows.Count;

            foreach (DataRow row in _tbPhong.Rows)
            {
                var sc = int.Parse(row["SucChua"].ToString()) - int.Parse(row["SiSo"].ToString());
                var idphong = int.Parse(row["IdPhong"].ToString());
                var bd = kt;
                kt = kt + sc;
                if (kt < tong)
                {
                    for (var i = bd; i < kt; i++)
                    {
                        var hsxp = new XepPhong
                        {
                            IdKyThi = _idkythi,
                            IdPhong = idphong,
                            IdSV = int.Parse(_tbSv.Rows[i]["IdSV"].ToString())
                        };
                        _listXepPhong.Add(hsxp);
                        _tbSv.Rows[i]["PhongThi"] = row["TenPhong"].ToString();
                    }
                    var hspp = new KTPhong
                    {
                        IdKyThi = _idkythi,
                        IdPhong = idphong,
                        SiSo = int.Parse(row["SucChua"].ToString())
                    };
                    _listPhanPhong.Add(hspp);
                }
                else
                {
                    for (var i = bd; i < tong; i++)
                    {
                        var hsxp = new XepPhong
                        {
                            IdKyThi = _idkythi,
                            IdPhong = idphong,
                            IdSV = int.Parse(_tbSv.Rows[i]["IdSV"].ToString())
                        };
                        _listXepPhong.Add(hsxp);
                        _tbSv.Rows[i]["PhongThi"] = row["TenPhong"].ToString();
                    }
                    var hspp = new KTPhong
                    {
                        IdKyThi = _idkythi,
                        IdPhong = idphong,
                        SiSo = int.Parse(row["SiSo"].ToString()) + (tong - bd)
                    };
                    _listPhanPhong.Add(hspp);
                    break;
                }
            }
        }

        protected override void LoadGrid()
        {
            try
            {
                var frm = new FrmCheckXepPhong {update = false};
                frm.ShowDialog();
                if (frm.update && frm.rdoall.Checked)
                {
                    if (_tbSv.Rows.Count > 0)
                        Xepphong();
                    else
                        MessageBox.Show(@"Chưa chọn sinh viên hoặc sinh viên đã được xếp phòng");
                    dgv_DanhSach.DataSource = _tbSv;
                    pnl_from.Visible = true;
                }else if (frm.update && frm.rdoone.Checked)
                {
                    dgv_DanhSach.DataSource = _tbSv;
                    pnl_from.Visible = true;
                }
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

        private void Huy()
        {
            try
            {
                _tbSv = LoadData.Load(13, _idkythi);
                _tbPhong = LoadData.Load(14, _idkythi);
                if (_tbSv.Rows.Count == 0)
                {
                    MessageBox.Show(@"Chưa chọn sinh viên hoặc sinh viên đã được xếp phòng");
                }
                else if (_tbPhong.Rows.Count == 0)
                {
                    MessageBox.Show(@"Chưa chọn phòng thi");
                }
                else
                {
                    var thread = new Thread(LoadFormDetail) {IsBackground = true};
                    thread.Start();
                    OnShowDialog("Loading...");
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
                if (_listXepPhong.Count > 0) UpdateData.UpdateXepPhong(_listXepPhong);
                if (_listPhanPhong.Count > 0) UpdateData.UpdateKtPhong(_listPhanPhong);
                MessageBox.Show(@"Sinh viên đã được xếp phòng");
                _listPhanPhong.Clear();
                _listXepPhong.Clear();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Ghi()
        {
            if (_listXepPhong.Count >0 && _listPhanPhong.Count > 0){
                _bgwInsert.RunWorkerAsync();
                OnShowDialog("Đang lưu dữ liệu");
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
                var group4 = band.Groups.Add("Phòng Thi");
                columns["STT"].Group = group5;
                columns["IdSV"].Group = group0;
                columns["HoSV"].Group = group1;
                columns["TenSV"].Group = group1;
                columns["NgaySinh"].Group = group2;
                columns["MaLop"].Group = group3;
                columns["PhongThi"].Group = group4;

                #endregion
                
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["IdSV"].CellActivation = Activation.NoEdit;
                band.Columns["HoSV"].CellActivation = Activation.NoEdit;
                band.Columns["TenSV"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["IdSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MinWidth = 60;
                band.Columns["IdSV"].MinWidth = 110;
                band.Columns["HoSV"].MinWidth = 170;
                band.Columns["TenSV"].MinWidth = 120;
                band.Columns["NgaySinh"].MinWidth = 140;
                band.Columns["MaLop"].MinWidth = 140;
                band.Columns["PhongThi"].MinWidth = 140;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["IdSV"].MaxWidth = 120;
                band.Columns["HoSV"].MaxWidth = 180;
                band.Columns["TenSV"].MaxWidth = 130;
                band.Columns["NgaySinh"].MaxWidth = 150;
                band.Columns["MaLop"].MaxWidth = 150;
                band.Columns["PhongThi"].MaxWidth = 150;
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

        #endregion

        #region MenuStrip

        private void menuStrip_Sua_Click(object sender, EventArgs e)
        {
            Sua();
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

        #endregion

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                Huy();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.S):
                    
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgv_DanhSach_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            Sua();
        }
    }
}
