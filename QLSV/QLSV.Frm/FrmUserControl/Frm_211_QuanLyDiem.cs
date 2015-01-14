using System;
using System.Data;
using System.Drawing;
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
        public Frm_211_QuanLyDiem()
        {
            InitializeComponent();
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
        }
    }
}
