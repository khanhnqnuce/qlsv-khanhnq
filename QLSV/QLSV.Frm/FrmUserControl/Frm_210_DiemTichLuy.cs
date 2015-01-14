using System;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_210_DiemTichLuy : FunctionControlHasGrid
    {
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;

        public Frm_210_DiemTichLuy()
        {
            InitializeComponent();
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
        }

        public void InDanhSach()
        {
            var frm = new FrmDiemTichLuy()
            {
                Update = false
            };
            frm.ShowDialog();
            if (frm.rdokhoa.Checked&& frm.Update)
            {
                RptKhoa();
            }
            else if (frm.rdoLop.Checked && frm.Update)
            {
                RptLop();
            }else if (frm.rdobangdiem.Checked && frm.Update)
            {
                Rptbangdiem();
            }
        }

        private void Rptbangdiem()
        {
            try
            {
                if(dgv_DanhSach.ActiveRow == null) return;
                var masv = int.Parse(dgv_DanhSach.ActiveRow.Cells["MaSV"].Text);
                var tb = LoadData.LoadBangdiem(masv);
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach", tb);
                rptbangdiem.FilePath = Application.StartupPath + @"\Reports\bangdiem.rst";
                rptbangdiem.Prepare();
                rptbangdiem.GetReportParameter += GetParameter;
                var previewForm = new PreviewForm(rptbangdiem)
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void GetParameter(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                e.Parameters["MaSV"].Value = dgv_DanhSach.ActiveRow.Cells["MaSV"].Text;
                e.Parameters["diemtichluy"].Value = dgv_DanhSach.ActiveRow.Cells["Diem"].Text;
                e.Parameters["TenSV"].Value = dgv_DanhSach.ActiveRow.Cells["HoSV"].Text + " " + dgv_DanhSach.ActiveRow.Cells["TenSV"].Text;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void RptKhoa()
        {
            try
            {
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
                rptdsdiemtheokhoa.FilePath = Application.StartupPath + @"\Reports\dsdiemkhoa.rst";
                rptdsdiemtheokhoa.Prepare();
                var previewForm = new PreviewForm(rptdsdiemtheokhoa)
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void RptLop()
        {
            try
            {
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
                rptdsdiemtheolop.FilePath = Application.StartupPath + @"\Reports\dsdiemlop.rst";
                rptdsdiemtheolop.Prepare();
                var previewForm = new PreviewForm(rptdsdiemtheolop)
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false
                };
                previewForm.Show();
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
                    row.Activate();
                    _newRow = row;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Frm_210_DiemTichLuy_Load(object sender, EventArgs e)
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(210);
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

                #region Caption
                var columns = band.Columns;
                band.ColHeadersVisible = false;
                var group6 = band.Groups.Add("STT");
                var group0 = band.Groups.Add("Mã SV");
                var group1 = band.Groups.Add("Họ và tên");
                var group2 = band.Groups.Add("Ngày sinh");
                var group3 = band.Groups.Add("Lớp");
                var group4 = band.Groups.Add("Khoa");
                var group5 = band.Groups.Add("Điểm");
                columns["STT"].Group = group6;
                columns["MaSV"].Group = group0;
                columns["HoSV"].Group = group1;
                columns["TenSV"].Group = group1;
                columns["NgaySinh"].Group = group2;
                columns["MaLop"].Group = group3;
                columns["TenKhoa"].Group = group4;
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
                band.Columns["MaLop"].MinWidth = 100;
                band.Columns["MaLop"].MaxWidth = 110;
                band.Columns["TenKhoa"].MinWidth = 270;
                band.Columns["TenKhoa"].MaxWidth = 290;
                band.Columns["Diem"].MinWidth = 100;
                band.Columns["Diem"].MaxWidth = 120;

                #endregion

                    columns["IdKhoa"].Hidden = true;
                 columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                 columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                 columns["Diem"].CellAppearance.TextHAlign = HAlign.Center;
                 columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;

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
