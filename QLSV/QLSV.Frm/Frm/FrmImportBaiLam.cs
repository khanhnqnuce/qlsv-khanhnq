using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmImportBaiLam : Form
    {
        private readonly DataTable _tableBaiLam;
        private readonly BackgroundWorker _bgwInsert;

        public FrmImportBaiLam(DataTable table)
        {
            InitializeComponent();
            _tableBaiLam = table;
            dgv_DanhSach.DataSource = table;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["IdKyThi"].Hidden = true;
                band.Columns["DiemThi"].Hidden = true;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaDe"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaHoiDong"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLoCham"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenFile"].CellAppearance.TextHAlign = HAlign.Center;

               
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.NoEdit;

                
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaSV"].MinWidth = 120;
                band.Columns["MaSV"].MaxWidth = 130;
                band.Columns["MaDe"].MinWidth = 100;
                band.Columns["MaDe"].MaxWidth = 110;
                band.Columns["KetQua"].MinWidth = 640;
                band.Columns["KetQua"].MaxWidth = 650;
                band.Columns["MaHoiDong"].MinWidth = 100;
                band.Columns["MaLoCham"].MinWidth = 100;
                band.Columns["TenFile"].MinWidth = 100;
                band.Columns["MaHoiDong"].MaxWidth = 110;
                band.Columns["MaLoCham"].MaxWidth = 110;
                band.Columns["TenFile"].MaxWidth = 110;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã sinh viên";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["KetQua"].Header.Caption = @"Bài làm sinh viên";
                band.Columns["MaHoiDong"].Header.Caption = @"Hội đồng";
                band.Columns["MaLoCham"].Header.Caption = @"Lô chấm";
                band.Columns["TenFile"].Header.Caption = @"Tên file";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        private void SaveDetail()
        {
            try
            {
                var save = new SqlBulkCopy();
                if (_tableBaiLam.Rows.Count <= 0) return;
                save.Bulk_Insert("BAILAM", _tableBaiLam);
                MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Thao tác thất bại", FormResource.MsgCaption);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            ShowLoading("Đang lưu dữ liệu");
        }

        private FrmLoadding _loading;
        private void ShowLoading(string msg)
        {
            _loading = new FrmLoadding();
            _loading.Update(msg);
            _loading.ShowDialog();
        }
        private void KillLoading()
        {
            try
            {
                if (_loading != null)
                {
                    _loading.Invoke((Action)(() =>
                    {
                        _loading.Close();
                        _loading = null;
                        Close();
                    }));
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

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
            KillLoading();
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
