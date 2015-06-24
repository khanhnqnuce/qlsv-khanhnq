using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmImportDSSV : Form
    {
        private readonly DataTable _tbSinhVien;
        private readonly BackgroundWorker _bgwInsert;

        public FrmImportDSSV(DataTable table)
        {
            InitializeComponent();
            _tbSinhVien = table;
            dgv_DanhSach.DataSource = table;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            //band.Columns["STT"].CellActivation = Activation.NoEdit;
            //band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
            band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
            band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            #region Size
            //band.Columns["STT"].MinWidth = 50;
            //band.Columns["STT"].MaxWidth = 50;
            band.Columns["MaSV"].MinWidth = 100;
            band.Columns["MaSV"].MaxWidth = 120;
            band.Columns["HoSV"].MinWidth = 130;
            band.Columns["HoSV"].MaxWidth = 150;
            band.Columns["TenSV"].MinWidth = 90;
            band.Columns["TenSV"].MaxWidth = 100;
            band.Columns["NgaySinh"].MinWidth = 100;
            band.Columns["NgaySinh"].MaxWidth = 100;
            band.Columns["Lop"].MinWidth = 100;
            band.Columns["Lop"].MaxWidth = 110;
            //band.Columns["TenKhoa"].MinWidth = 270;
            //band.Columns["TenKhoa"].MaxWidth = 290;
            #endregion
            band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

            #region Caption
            band.Groups.Clear();
            var columns = band.Columns;
            band.ColHeadersVisible = false;
            //var group5 = band.Groups.Add("STT");
            var group0 = band.Groups.Add("Mã SV");
            var group1 = band.Groups.Add("Họ và tên");
            var group2 = band.Groups.Add("Ngày sinh");
            var group3 = band.Groups.Add("Lớp");
            //var group4 = band.Groups.Add("Khoa");
            //columns["STT"].Group = group5;
            columns["MaSV"].Group = group0;
            columns["HoSV"].Group = group1;
            columns["TenSV"].Group = group1;
            columns["NgaySinh"].Group = group2;
            columns["Lop"].Group = group3;
            //columns["TenKhoa"].Group = group4;

            #endregion

            //columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
            columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
            columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;
            columns["Lop"].CellAppearance.TextHAlign = HAlign.Center;
        }

        private static DataTable GetTable()
        {
            var table = new DataTable();
            //table.Columns.Add("STT", typeof(string));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("HoSV", typeof(string));
            table.Columns.Add("TenSV", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("Lop", typeof(string));

            return table;
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        private void SaveDetail()
        {
            var save = new SqlBulkCopy();
            save.sp_InsertUpdate("sp_InsertSV", "@tbl", _tbSinhVien);
            MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption);
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
