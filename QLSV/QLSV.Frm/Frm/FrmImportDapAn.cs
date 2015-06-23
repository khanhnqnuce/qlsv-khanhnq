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
    public partial class FrmImportDapAn : Form
    {
        private readonly BackgroundWorker _bgwInsert;
        private DataTable _tableDapDan;

        public FrmImportDapAn(DataTable table)
        {
            InitializeComponent();
            _tableDapDan = table;
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
                band.Columns["ThangDiem"].Hidden = true;

                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaMon"].MinWidth = 140;
                band.Columns["MaDe"].MinWidth = 140;
                band.Columns["CauHoi"].MinWidth = 140;
                band.Columns["Dapan"].MinWidth = 140;
                band.Columns["MaMon"].MaxWidth = 150;
                band.Columns["MaDe"].MaxWidth = 150;
                band.Columns["CauHoi"].MaxWidth = 150;
                band.Columns["Dapan"].MaxWidth = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaMon"].Header.Caption = @"Mã môn thi";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["CauHoi"].Header.Caption = @"Câu hỏi";
                band.Columns["Dapan"].Header.Caption = @"Đáp án";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("IdKyThi", typeof(int));
            table.Columns.Add("MaMon", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("CauHoi", typeof(int));
            table.Columns.Add("DapAn", typeof(string));
            table.Columns.Add("ThangDiem", typeof(double));
            return table;
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        private void SaveDetail()
        {
            try
            {
                var save = new SqlBulkCopy();

                if (_tableDapDan.Rows.Count <= 0) return;
                save.InsertTable("DAPAN", _tableDapDan);
                MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
