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
    public partial class Frm_110_DaXepPhong : FunctionControlHasGrid
    {
        private IList<XepPhong> _listxepphong = new List<XepPhong>();
        private IList<KTPhong> _listktphong = new List<KTPhong>();
        private readonly int _idkythi;
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;

        public Frm_110_DaXepPhong(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
        }

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("MaSV", typeof (string));
            table.Columns.Add("HoSV", typeof (string));
            table.Columns.Add("TenSV", typeof (string));
            table.Columns.Add("NgaySinh", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("TenPhong", typeof(string));
            table.Columns.Add("TenKhoa", typeof(string));
            table.Columns.Add("IdKhoa", typeof(string));
            table.Columns.Add("IdPhong", typeof(string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                var table = LoadData.Load(1,_idkythi);
                dgv_DanhSach.DataSource = table;
                pnl_form.Visible = true;
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
                Invoke((Action) LoadGrid);
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

        protected override void XoaDetail()
        {
            try
            {
                UpdateData.UpdateKtPhong(_idkythi);
                UpdateData.UpdateXepPhongNull(_idkythi);
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                 Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void DeleteRow()
        {
            try
            {
                if (dgv_DanhSach.Selected.Rows.Count > 0)
                {
                    if (DialogResult.Yes ==
                        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        foreach (var row in dgv_DanhSach.Selected.Rows)
                        {
                            var xp = new XepPhong
                            {
                                IdKyThi = _idkythi,
                                IdSV = int.Parse(row.Cells["MaSV"].Text)
                            };
                            var kt = new KTPhong
                            {
                                IdPhong = int.Parse(row.Cells["IdPhong"].Text),
                                IdKyThi = _idkythi
                            };
                            _listxepphong.Add(xp);
                            _listktphong.Add(kt);
                        }
                        DeleteAndUpdate = true;
                        dgv_DanhSach.DeleteSelectedRows(false);
                    }
                }
                else if (dgv_DanhSach.ActiveRow != null)
                {
                    if (DialogResult.Yes ==
                        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        var xp = new XepPhong
                        {
                            IdKyThi = _idkythi,
                            IdSV = int.Parse(dgv_DanhSach.ActiveRow.Cells["MaSV"].Text)
                        };
                        var kt = new KTPhong
                        {
                            IdPhong = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdPhong"].Text),
                            IdKyThi = _idkythi
                        };
                        _listxepphong.Add(xp);
                        _listktphong.Add(kt);
                        DeleteAndUpdate = true;
                        dgv_DanhSach.ActiveRow.Delete(false);
                    }
                }
                if (_listxepphong.Count <= 0 && _listktphong.Count <= 0) return;
                if (_listktphong.Count > 0) UpdateData.UpdateGiamSiSo(_listktphong);
                if (_listktphong.Count > 0) UpdateData.UpdateXP_Null(_listxepphong);
                MessageBox.Show(@"Xóa dữ liệu thành công", FormResource.MsgCaption);
                LoadGrid();
                _listktphong.Clear();
                _listxepphong.Clear();
                IdDelete.Clear();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Sua()
        {
            try
            {
                var frm = new FrmXepPhong
                {
                    txtmasinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["MaSV"].Text},
                    txthotendem = {Text = dgv_DanhSach.ActiveRow.Cells["HoSV"].Text},
                    txttensinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["TenSV"].Text},
                    txtNgaySinh = {Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text},
                    cbolop = {Text = dgv_DanhSach.ActiveRow.Cells["MaLop"].Text},
                    cboPhongthi = { Text = dgv_DanhSach.ActiveRow.Cells["TenPhong"].Text },
                    IdKythi = _idkythi,
                    IdPhong = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdPhong"].Text),
                    bUpdate = true
                };
                frm.ShowDialog();
                if (frm.bUpdate) return;
                dgv_DanhSach.ActiveRow.Cells["IdPhong"].Value = frm.cboPhongthi.Value.ToString();
                dgv_DanhSach.ActiveRow.Cells["TenPhong"].Value = frm.cboPhongthi.Text;
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

        public void InDanhSach()
        {
            var frm = new FrmRptDanhSachPhongThi {bUpdate = false};
            frm.ShowDialog();
            if (frm.rdonopbai.Checked && frm.bUpdate)
                RptNopbai();
            if (frm.rdoPhongthi.Checked && frm.bUpdate)
                RptPhongthi();
            else if (frm.rdokhoa.Checked && frm.bUpdate)
                RptKhoa();
            else if (frm.rdoLop.Checked && frm.bUpdate)
                RptLop();
        }

        #region Xuất báo cáo

        private void RptNopbai()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdanhsachduthi.FilePath = Application.StartupPath + @"\Reports\danhsachduthiphong.rst";
            rptdanhsachduthi.Prepare();
            rptdanhsachduthi.GetReportParameter += GetParameter;
            var previewForm = new PreviewForm(rptdanhsachduthi)
            {
                WindowState = FormWindowState.Maximized
            };
            previewForm.Show();
        }
        
        private void RptPhongthi()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdanhsachduthi.FilePath = Application.StartupPath + @"\Reports\danhsachnopbai.rst";
            rptdanhsachduthi.GetReportParameter += GetParameter;
            rptdanhsachduthi.Prepare();
            var previewForm = new PreviewForm(rptdanhsachduthi)
            {
                WindowState = FormWindowState.Maximized
            };
            previewForm.Show();
        }

        private void RptKhoa()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdanhsachkhoa.FilePath = Application.StartupPath + @"\Reports\danhsachduthikhoa.rst";
            rptdanhsachkhoa.Prepare();
            var previewForm = new PreviewForm(rptdanhsachkhoa)
            {
                WindowState = FormWindowState.Maximized
            };
            previewForm.Show();
        }

        private void RptLop()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdanhsachlop.FilePath = Application.StartupPath + @"\Reports\danhsachduthilop.rst";
            rptdanhsachlop.Prepare();
            var previewForm = new PreviewForm(rptdanhsachlop)
            {
                WindowState = FormWindowState.Maximized
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
                    e.Parameters["GhiChu"].Value = "Giờ thi: "+ row["TGBatDau"] + " - " + row["TGKetThuc"];
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        private void FrmSinhVienPhongThi_Load(object sender, EventArgs e)
        {
            try
            {
                var thread = new Thread(LoadFormDetail) {IsBackground = true};
                thread.Start();
                OnShowDialog("Loading...");
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

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

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
                columns["MaSV"].Group = group0;
                columns["HoSV"].Group = group1;
                columns["TenSV"].Group = group1;
                columns["NgaySinh"].Group = group2;
                columns["MaLop"].Group = group3;
                columns["TenPhong"].Group = group4;

                #endregion

                band.Columns["IdKhoa"].Hidden = true;
                band.Columns["TenKhoa"].Hidden = true;
                band.Columns["IdPhong"].Hidden = true;

                band.Columns["STT"].MinWidth = 60;
                band.Columns["MaSV"].MinWidth = 110;
                band.Columns["HoSV"].MinWidth = 170;
                band.Columns["TenSV"].MinWidth = 120;
                band.Columns["NgaySinh"].MinWidth = 140;
                band.Columns["MaLop"].MinWidth = 140;
                band.Columns["TenPhong"].MinWidth = 140;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaSV"].MaxWidth = 120;
                band.Columns["HoSV"].MaxWidth = 180;
                band.Columns["TenSV"].MaxWidth = 130;
                band.Columns["NgaySinh"].MaxWidth = 150;
                band.Columns["MaLop"].MaxWidth = 150;
                band.Columns["TenPhong"].MaxWidth = 150;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["HoSV"].CellActivation = Activation.NoEdit;
                band.Columns["TenSV"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["TenPhong"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenPhong"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void dgv_DanhSach_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            Sua();
        }

        private void menuStrip_Themdong_Click(object sender, EventArgs e)
        {
            Sua();
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
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa3(int.Parse(cbokhoa.SelectedValue.ToString()),_idkythi);
                return;
            }
            dgv_DanhSach.DataSource = SearchData.Timkiemtheolop3(int.Parse(obj.ToString()),_idkythi);
        }

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
    }
}
