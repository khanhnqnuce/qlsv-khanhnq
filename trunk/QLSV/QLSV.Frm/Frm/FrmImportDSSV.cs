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
    public partial class FrmImportDSSV : Form
    {
        private DataTable _tbError;
        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private readonly BackgroundWorker _bgwInsert;
        private readonly int _idkythi;

        public FrmImportDSSV(DataTable table)
        {
            InitializeComponent();
            dgv_DanhSach.DataSource = table;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.Columns["STT"].CellActivation = Activation.NoEdit;
            band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
            band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
            band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            #region Size
            band.Columns["STT"].MinWidth = 50;
            band.Columns["STT"].MaxWidth = 50;
            band.Columns["MaSV"].MinWidth = 100;
            band.Columns["MaSV"].MaxWidth = 120;
            band.Columns["HoSV"].MinWidth = 130;
            band.Columns["HoSV"].MaxWidth = 150;
            band.Columns["TenSV"].MinWidth = 90;
            band.Columns["TenSV"].MaxWidth = 100;
            band.Columns["NgaySinh"].MinWidth = 100;
            band.Columns["NgaySinh"].MaxWidth = 100;
            band.Columns["MaLop"].MinWidth = 100;
            band.Columns["MaLop"].MaxWidth = 110;
            //band.Columns["TenKhoa"].MinWidth = 270;
            //band.Columns["TenKhoa"].MaxWidth = 290;
            #endregion
            band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

            #region Caption
            band.Groups.Clear();
            var columns = band.Columns;
            band.ColHeadersVisible = false;
            var group5 = band.Groups.Add("STT");
            var group0 = band.Groups.Add("Mã SV");
            var group1 = band.Groups.Add("Họ và tên");
            var group2 = band.Groups.Add("Ngày sinh");
            var group3 = band.Groups.Add("Lớp");
            //var group4 = band.Groups.Add("Khoa");
            columns["STT"].Group = group5;
            columns["MaSV"].Group = group0;
            columns["HoSV"].Group = group1;
            columns["TenSV"].Group = group1;
            columns["NgaySinh"].Group = group2;
            columns["MaLop"].Group = group3;
            //columns["TenKhoa"].Group = group4;

            #endregion

            columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
            columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
            columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;
            columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
        }

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(string));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("HoSV", typeof(string));
            table.Columns.Add("TenSV", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));

            return table;
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        private void SaveDetail()
        {
            try
            {
                _tbError = GetTable();
                var i = 1;
                var tbLop = LoadData.Load(16);
                var danhsach = (DataTable)dgv_DanhSach.DataSource;
                foreach (DataRow row in danhsach.Rows)
                {
                    var b = false;
                    var malop = row["MaLop"].ToString();
                    foreach (var dataRow in tbLop.Rows.Cast<DataRow>().Where(dataRow => dataRow["MaLop"].ToString().Equals(malop)))
                    {
                        var hs = new SinhVien
                        {
                            MaSV = int.Parse(row["MaSV"].ToString()),
                            HoSV = row["HoSV"].ToString(),
                            TenSV = row["TenSV"].ToString(),
                            NgaySinh = row["NgaySinh"].ToString(),
                            IdLop = int.Parse(dataRow["ID"].ToString()),
                        };
                        b = true;
                        _listAdd.Add(hs);
                    }
                    if (!b)
                    {
                        _tbError.Rows.Add(i++,
                            row["MaSV"].ToString(),
                            row["HoSV"].ToString(),
                            row["TenSV"].ToString(),
                            row["NgaySinh"].ToString(),
                            row["MaLop"].ToString());
                    }
                }
                if (_listAdd.Count <= 0) return;
                InsertData.ThemSinhVien(_listAdd);
                if (_tbError.Rows.Count > 0) return;
                MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            ShowLoading("Đang lưu dữ liệu");
            if (_tbError.Rows.Count > 0)
            {
                var text = @"Còn " + _tbError.Rows.Count + @" sinh viên chưa được lưu vào CSDL.";
                var frm = new FrmMsgImportSv(text, _tbError, 1);
                frm.ShowDialog();
            }
            Close();
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
                        //_loading.Dispose();
                        _loading = null;
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
