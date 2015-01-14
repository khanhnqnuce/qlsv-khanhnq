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
        //private UltraGridRow _newRow;

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
            LoadGrid();
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
            cbolop.ValueMember = "ID";
            cbolop.DisplayMember = "MaLop";
            cbolop.DataSource = tb1;
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
                    cbolop.ValueMember = "ID";
                    cbolop.DisplayMember = "MaLop";
                    cbolop.DataSource = tb1;
                    LoadGrid();
                    return;
                }
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa1(int.Parse(obj.ToString()));

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
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa1(int.Parse(cbokhoa.SelectedValue.ToString()));
                return;
            }
            dgv_DanhSach.DataSource = SearchData.Timkiemtheolop1(int.Parse(obj.ToString()));
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
