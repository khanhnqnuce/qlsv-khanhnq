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
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_105_InportSinhVien : FunctionControlHasGrid
    {
        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private DataTable _tbError;

        private readonly BackgroundWorker _bgwInsert;

        public Frm_105_InportSinhVien()
        {
            InitializeComponent();
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        /// <summary>
        /// khởi tạo table
        /// </summary>
        /// <returns>trả về 1 bảng sinh viên đển gán vào UltraGrid</returns>
        protected virtual DataTable GetTable()
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

        protected override void LoadFormDetail()
        {
            try
            {
                _listAdd.Clear();
                var table = GetTable();
                uG_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Hàm lấy dữ liệu từ file excel
        /// </summary>
        public void Napdulieu()
        {
            try
            {
                var stt = uG_DanhSach.Rows.Count;
                var frmNapDuLieu = new FrmNDLSinhVien(stt,GetTable());
                frmNapDuLieu.ShowDialog();
                var resultValue = frmNapDuLieu.ResultValue;
                if (resultValue == null || resultValue.Rows.Count == 0) return;
                var table = (DataTable)uG_DanhSach.DataSource;

                table.Merge(resultValue);
                uG_DanhSach.DataSource = table;

                MessageBox.Show(@"Import thành công " + resultValue.Rows.Count + @" Sinh viên. Nhấn F5 để lưu lại");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Thêm 1 dòng trên UltraGrid
        /// </summary>
        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "MaSV");
        }

        /// <summary>
        /// Xóa 1 dồng trên UltraGrid
        /// </summary>
        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "MaSV", "MaSV");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        protected override void SaveDetail()
        {
            try
            {
                _tbError = GetTable();
                var i = 1;
                var tbLop = LoadData.Load(16);
                var danhsach = (DataTable)uG_DanhSach.DataSource;
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
                    if(!b)
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
                if(_tbError.Rows.Count>0) return;
                MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Ghi()
        {
            if (uG_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
            if (_tbError.Rows.Count > 0)
            {
                var text = @"Còn " + _tbError.Rows.Count + @" sinh viên chưa được lưu vào CSDL";
                var frm = new FrmMsgImportSv(text,_tbError,1);
                frm.ShowDialog();
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
            OnCloseDialog();
        }

        #endregion

        #region MenuStrip của ultraGrid

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        #endregion

        private void FrmInportSinhVien_Load(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        /// <summary>
        /// Hàm khởi tạo của UltraGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
    }
}
