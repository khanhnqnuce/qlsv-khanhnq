using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_211_QuanLyDiem : FunctionControlHasGrid
    {
        private readonly FrmTimkiem _frmTimkiem;

        private int _idkhoa, _idlop, _idnamhoc;
        private DataTable _tb1;
        private string _idhocky;

        public Frm_211_QuanLyDiem()
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSV", typeof(int));
            table.Columns.Add("HoSV", typeof(string));
            table.Columns.Add("TenSV", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("NamHoc", typeof(string));
            table.Columns.Add("HocKy", typeof(string));
            table.Columns.Add("Diem", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(211);
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
                var frm = new FrmGopKQ { Update = false };
                frm.ShowDialog();
                if (!frm.Update) return;
                var nh = int.Parse(frm.cboNamHoc.SelectedValue.ToString());
                var hk = frm.cbohocky.SelectedValue.ToString();
                if(MessageBox.Show(@"Bạn có chắc chắn muốn xóa không?",
                    @"Thông báo",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) != DialogResult.OK) return;
                DeleteData.XoaDiemThi(nh,hk);
                MessageBox.Show(@"Xóa dữ liệu thành công", @"Thông báo");
                LoadGrid();
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
                 foreach (var row in dgv_DanhSach.Selected.Rows)
                 {
                     row.Selected = false;
                 }
                 foreach (
                     var row in dgv_DanhSach.Rows.Where(row => row.Cells["MaSV"].Value.ToString() == masinhvien))
                 {
                     row.Selected = true;
                     if (row.Index > 4)
                         dgv_DanhSach.ActiveRowScrollRegion.ScrollPosition = row.Index - 3;
                     else
                         dgv_DanhSach.ActiveRowScrollRegion.ScrollPosition = row.Index;
                 }
             }
             catch (Exception ex)
             {
                 Log2File.LogExceptionToFile(ex);
             }
         }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Groups.Clear();
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;

                #region Caption
                var columns = band.Columns;
                band.ColHeadersVisible = false;
                var group6 = band.Groups.Add("STT");
                var group0 = band.Groups.Add("Mã SV");
                var group1 = band.Groups.Add("Họ và tên");
                var group2 = band.Groups.Add("Ngày sinh");
                var group3 = band.Groups.Add("Năm học");
                var group4 = band.Groups.Add("Học kỳ");
                var group5 = band.Groups.Add("Điểm");
                columns["STT"].Group = group6;
                columns["MaSV"].Group = group0;
                columns["HoSV"].Group = group1;
                columns["TenSV"].Group = group1;
                columns["NgaySinh"].Group = group2;
                columns["NamHoc"].Group = group3;
                columns["HocKy"].Group = group4;
                columns["Diem"].Group = group5;

                #endregion

                #region Size

                band.Columns["STT"].MinWidth = 60;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaSV"].MinWidth = 100;
                band.Columns["MaSV"].MaxWidth = 120;
                band.Columns["HoSV"].MinWidth = 130;
                band.Columns["HoSV"].MaxWidth = 150;
                band.Columns["TenSV"].MinWidth = 90;
                band.Columns["TenSV"].MaxWidth = 100;
                band.Columns["NgaySinh"].MinWidth = 100;
                band.Columns["NgaySinh"].MaxWidth = 100;
                band.Columns["NamHoc"].MinWidth = 140;
                band.Columns["NamHoc"].MaxWidth = 150;
                band.Columns["HocKy"].MinWidth = 100;
                band.Columns["HocKy"].MaxWidth = 110;
                band.Columns["Diem"].MinWidth = 100;
                band.Columns["Diem"].MaxWidth = 120;

                #endregion

                columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                columns["NamHoc"].CellAppearance.TextHAlign = HAlign.Center;
                columns["Diem"].CellAppearance.TextHAlign = HAlign.Center;
                columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                columns["HocKy"].CellAppearance.TextHAlign = HAlign.Center;

                foreach (var column in band.Columns)
                {
                    column.CellActivation = Activation.NoEdit;
                }

                dgv_DanhSach.DisplayLayout.UseFixedHeaders = true;
                dgv_DanhSach.DisplayLayout.FixedHeaderOffImage = Properties.Resources.trang;
                dgv_DanhSach.DisplayLayout.FixedHeaderOnImage = Properties.Resources.trang;
                group0.Header.Fixed = true;
                group1.Header.Fixed = true;
                group2.Header.Fixed = true;
                group3.Header.Fixed = true;
                group6.Header.Fixed = true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Frm_211_QuanLyDiem_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadGrid();

                //---Lop--
                _tb1 = new DataTable();
                _tb1.Columns.Add("ID", typeof(int));
                _tb1.Columns.Add("MaLop", typeof(string));
                _tb1.Rows.Add("0", "- Chọn lớp -");
               
                //-- Khoa --

                var table = LoadData.Load(3);
                var tb = new DataTable();
                tb.Columns.Add("ID", typeof(int));
                tb.Columns.Add("TenKhoa", typeof(string));
                tb.Rows.Add("0", "- Tất cả các khoa -");
                tb.Merge(table);
                cbokhoa.DataSource = tb;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _idkhoa = int.Parse(cbokhoa.SelectedValue.ToString());
                if (_idkhoa == 0)
                {
                    cbolop.DataSource = _tb1;
                }
                else
                {
                    var table = SearchData.LoadCboLop(_idkhoa);
                    var tb = new DataTable();
                    tb.Columns.Add("ID", typeof (int));
                    tb.Columns.Add("MaLop", typeof (string));
                    tb.Rows.Add("0", "- Tất cả các lớp -");
                    tb.Merge(table);
                    cbolop.DataSource = tb;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbolop_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _idlop = int.Parse(cbolop.SelectedValue.ToString());
                var table2 = LoadData.Load(209);
                var tb2 = new DataTable();
                tb2.Columns.Add("ID", typeof(int));
                tb2.Columns.Add("NamHoc", typeof(string));
                tb2.Rows.Add("0", "- Tất cả các năm -");
                tb2.Merge(table2);
                cboNamhoc.DataSource = tb2;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cboNamhoc_SelectedValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                _idnamhoc = int.Parse(cboNamhoc.SelectedValue.ToString());
                var tb3 = new DataTable();
                tb3.Columns.Add("MaHK", typeof(string));
                tb3.Columns.Add("TenHK", typeof(string));
                tb3.Rows.Add("0", "- Tất cả học kỳ -");
                tb3.Rows.Add("HK0", "Học kỳ 0");
                tb3.Rows.Add("HK1", "Học kỳ 1");
                tb3.Rows.Add("HK2", "Học kỳ 2");
                tb3.Rows.Add("HK3", "Học kỳ 3");
                cboHocky.DataSource = tb3;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cboHocky_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _idhocky = cboHocky.SelectedValue.ToString();
                if (_idhocky == "0")
                {
                    if (_idnamhoc == 0)
                    {
                        if (_idlop != 0)
                            dgv_DanhSach.DataSource = SearchData.Timkiemtheolop1(_idlop);
                        else if (_idkhoa != 0)
                            dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa1(_idkhoa);
                        else
                            LoadGrid();
                    }
                    else if (_idlop != 0)
                        dgv_DanhSach.DataSource = SearchData.Quanlydiem(1, _idkhoa, _idlop, _idnamhoc, _idhocky);
                    else if (_idkhoa != 0)
                        dgv_DanhSach.DataSource = SearchData.Quanlydiem(2, _idkhoa, _idlop, _idnamhoc, _idhocky);
                    else
                        dgv_DanhSach.DataSource = SearchData.Quanlydiem(3, _idkhoa, _idlop, _idnamhoc, _idhocky);
                }
                else if (_idnamhoc != 0)
                {
                    if (_idlop != 0)
                        dgv_DanhSach.DataSource = SearchData.Quanlydiem(7, _idkhoa, _idlop, _idnamhoc, _idhocky);
                    else if (_idkhoa != 0)
                        dgv_DanhSach.DataSource = SearchData.Quanlydiem(8, _idkhoa, _idlop, _idnamhoc, _idhocky); 
                    else
                        dgv_DanhSach.DataSource = SearchData.Quanlydiem(9, _idkhoa, _idlop, _idnamhoc, _idhocky);
                }
                else if (_idlop != 0)
                {
                    dgv_DanhSach.DataSource = SearchData.Quanlydiem(4, _idkhoa, _idlop, _idnamhoc, _idhocky);
                }
                else if (_idkhoa != 0)
                {
                    dgv_DanhSach.DataSource = SearchData.Quanlydiem(5, _idkhoa, _idlop, _idnamhoc, _idhocky);
                }
                else
                {
                    dgv_DanhSach.DataSource = SearchData.Quanlydiem(6, _idkhoa, _idlop, _idnamhoc, _idhocky);
                }
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
                    _frmTimkiem.ShowDialog();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
    }
}
