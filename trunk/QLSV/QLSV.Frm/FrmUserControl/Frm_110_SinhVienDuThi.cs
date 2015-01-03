﻿using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;
using Color = System.Drawing.Color;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_110_SinhVienDuThi : FunctionControlHasGrid
    {
        private readonly int _idkythi;
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;

        public Frm_110_SinhVienDuThi(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
        }

        protected override DataTable GetTable()
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

        protected override void LoadGrid()
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
                //if (dgv_DanhSach.Selected.Rows.Count > 0)
                //{
                //    if (DialogResult.Yes ==
                //        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                //            MessageBoxIcon.Question))
                //    {
                //        foreach (var row in dgv_DanhSach.Selected.Rows)
                //        {
                //            var id = row.Cells["ID"].Value.ToString();
                //            if (!string.IsNullOrEmpty(id))
                //            {
                //                var hs = new XepPhong
                //                {
                //                    IdSV = int.Parse(id),
                //                    IdKyThi = int.Parse(row.Cells["IdKyThi"].Text),
                //                    IdPhong = int.Parse(row.Cells["IdPhong"].Text),
                //                };
                //                m_IdDelete.Add(hs);
                //            }
                //            foreach (var p in _lisPhong.Where(p => p.TenPhong == row.Cells["PhongThi"].Text))
                //            {
                //                p.SoLuong = p.SoLuong + 1;
                //            }
                //            var phong = new PhongThi
                //            {
                //                TenPhong = row.Cells["PhongThi"].Text,
                //                SoLuong = 1
                //            };
                //            _lisPhong.Add(phong);
                //        }
                //        dgv_DanhSach.DeleteSelectedRows(false);
                //    }
                //}
                //else if (dgv_DanhSach.ActiveRow != null)
                //{
                //    if (DialogResult.Yes ==
                //        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                //            MessageBoxIcon.Question))
                //    {
                //        var idStr = dgv_DanhSach.ActiveRow.Cells["ID"].Value.ToString();
                //        if (!string.IsNullOrEmpty(idStr))
                //        {
                //            var hs = new XepPhong
                //            {
                //                IdSV = int.Parse(dgv_DanhSach.ActiveRow.Cells["ID"].Text),
                //                IdKyThi = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdKyThi"].Text),
                //                IdPhong = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdPhong"].Text),
                //            };
                //            m_IdDelete.Add(hs);
                //        }
                //        foreach (
                //            var p in _lisPhong.Where(p => p.TenPhong == dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text))
                //        {
                //            p.SoLuong = p.SoLuong + 1;
                //        }
                //        var phong = new PhongThi
                //        {
                //            TenPhong = dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text,
                //            SoLuong = 1
                //        };
                //        _lisPhong.Add(phong);
                //        dgv_DanhSach.ActiveRow.Delete(false);
                //    }
                //}
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
                
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();
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
            var frm = new FrmChonIndssv {Update = false};
            frm.ShowDialog();
            if (frm.rdoPhongthi.Checked && frm.Update)
                RptPhongthi();
            else if (frm.rdokhoa.Checked && frm.Update)
                RptKhoa();
            else if (frm.rdoLop.Checked && frm.Update)
                RptLop();
        }

        #region Xuất báo cáo

        private void RptPhongthi()
        {
            var tbPhong = LoadData.Load(2,_idkythi);

            var tb = (DataTable) dgv_DanhSach.DataSource;
            foreach (DataRow rowp in tbPhong.Rows)
            {
                var stt = 1;
                var phong = rowp["TenPhong"].ToString();
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["TenPhong"].ToString().Equals(phong)))
                {
                    row["STT"] = stt++;
                }
            }

            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachduthi.FilePath = Application.StartupPath + @"\Reports\danhsachduthiphong.rst";
            rptdanhsachduthi.GetReportParameter += GetParameter;
            rptdanhsachduthi.Prepare();
            var previewForm = new PreviewForm(rptdanhsachduthi)
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false
            };
            previewForm.Show();
        }

        private void RptKhoa()
        {
            var tbkhoa = LoadData.Load(15);
            var tb = ((DataTable) dgv_DanhSach.DataSource);
            foreach (DataRow rowl in tbkhoa.Rows)
            {
                var stt = 1;
                var id = rowl["ID"].ToString();
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["IdKhoa"].ToString().Equals(id)))
                {
                    row["STT"] = stt++;
                }
            }
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachkhoa.FilePath = Application.StartupPath + @"\Reports\danhsachduthikhoa.rst";
            rptdanhsachkhoa.GetReportParameter += GetParameter;
            rptdanhsachkhoa.Prepare();
            var previewForm = new PreviewForm(rptdanhsachkhoa)
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false
            };
            previewForm.Show();
        }

        private void RptLop()
        {
            var tblop = LoadData.Load(4,_idkythi);
            var tb = ((DataTable) dgv_DanhSach.DataSource);
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
            rptdanhsachlop.FilePath = Application.StartupPath + @"\Reports\danhsachduthilop.rst";
            rptdanhsachlop.GetReportParameter += GetParameter;
            rptdanhsachlop.Prepare();
            var previewForm = new PreviewForm(rptdanhsachlop)
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
                    e.Parameters["NgayThi"].Value = row["NgayThi"].ToString();
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
    }
}
