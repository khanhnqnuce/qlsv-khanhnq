﻿using System;
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
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmDanhSachBaiLam : FunctionControlHasGrid
    {
        private readonly IList<BaiLam> _listUpdate = new List<BaiLam>();

        public FrmDanhSachBaiLam()
        {
            InitializeComponent();
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
                dgv_DanhSach.DataSource = LoadData.Load(12);
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
                UpdateData.UpdateBaiLam(_listUpdate);
                QlsvSevice.Xoa(IdDelete, "BaiLam");
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
                DeleteData.Xoa("BaiLam");
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
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdapandethi.FilePath = Application.StartupPath + @"\Reports\dapandethi.rst";
            using (var previewForm = new PreviewForm(rptdapandethi))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdapandethi.Prepare();
                previewForm.ShowDialog();
            }
        }

        public void InDanhSach()
        {
            RptDapAn();
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

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].Width = 50;
                band.Columns["MaSinhVien"].Width = 150;
                band.Columns["MaDe"].Width = 150;
                band.Columns["KetQua"].Width = 650;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = @"Mã sinh viên";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["KetQua"].Header.Caption = @"Bài làm sinh viên";

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
                if (b)
                {
                    b = false;
                    return;
                }
                var id = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                if (string.IsNullOrEmpty(id)) return;
                foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                {
                    item.MaSinhVien = dgv_DanhSach.ActiveRow.Cells["MaSinhVien"].Text;
                    item.MaDe = dgv_DanhSach.ActiveRow.Cells["MaDe"].Text;
                    item.KetQua = dgv_DanhSach.ActiveRow.Cells["KetQua"].Text;
                    return;
                }
                var hs = new BaiLam
                {
                    ID = int.Parse(id),
                    MaSinhVien = dgv_DanhSach.ActiveRow.Cells["MaSinhVien"].Text,
                    MaDe = dgv_DanhSach.ActiveRow.Cells["MaDe"].Text,
                    KetQua = dgv_DanhSach.ActiveRow.Cells["KetQua"].Text
                };
                _listUpdate.Add(hs);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_KeyDown(object sender, KeyEventArgs e)
        {

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

        private void FrmDanhSachBaiLam_Load(object sender, EventArgs e)
        {
            Huy();
        }
    }
}