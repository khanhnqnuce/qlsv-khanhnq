using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmSinhVienPhongThi : FunctionControlHasGrid
    {
        public FrmSinhVienPhongThi()
        {
            InitializeComponent();
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(string));
            table.Columns.Add("MaSinhVien", typeof(string));
            table.Columns.Add("HoSinhVien", typeof(string));
            table.Columns.Add("TenSinhVien", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("PhongThi", typeof(string));
            table.Columns.Add("MaKhoa", typeof(string));
            table.Columns.Add("TenKhoa", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var table = LoadData.Load(7);
                uG_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        public void InDanhSach()
        {
            var frm = new FrmChonindssv();
            frm.ShowDialog();
            if (frm.rdoPhongthi.Checked)
                RptPhongthi();
            else if (frm.rdokhoa.Checked)
                RptKhoa();
            else if (frm.rdoLop.Checked)
                RptLop();
        }

        private void RptPhongthi()
        {
            var listphongthi = QlsvSevice.Load<PhongThi>();
            var tb = GetTable();
            tb.Merge((DataTable)uG_DanhSach.DataSource);
            foreach (var phong in listphongthi)
            {
                var stt = 1;
                var lopnew = phong;
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["PhongThi"].ToString() == lopnew.TenPhong))
                {
                    row["STT"] = stt++;
                }
            }

            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachduthi.FilePath = Application.StartupPath + @"\Reports\danhsachduthi.rst";
            using (var previewForm = new PreviewForm(rptdanhsachduthi))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachduthi.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void RptKhoa()
        {
            var listkhoa = QlsvSevice.Load<Khoa>();
            var tb = GetTable();
            tb.Merge((DataTable)uG_DanhSach.DataSource);
            tb.Columns.Add("TenKhoa", typeof(string));
            foreach (var khoa in listkhoa)
            {
                var stt = 1;
                var khoa1 = khoa;
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["MaKhoa"].ToString() == khoa1.ID.ToString()))
                {
                    row["STT"] = stt++;
                    row["TenKhoa"] = khoa.TenKhoa;
                }
            }

            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachkhoa.FilePath = Application.StartupPath + @"\Reports\danhsachduthikhoa.rst";
            using (var previewForm = new PreviewForm(rptdanhsachkhoa))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachkhoa.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void RptLop()
        {
            var listlop = QlsvSevice.Load<Lop>();
            var tb = GetTable();
            tb.Merge((DataTable)uG_DanhSach.DataSource);
            foreach (var lop in listlop)
            {
                var stt = 1;
                var lopnew = lop;
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["MaLop"].ToString() == lopnew.MaLop))
                {
                    row["STT"] = stt++;
                }
            }
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachlop.FilePath = Application.StartupPath + @"\Reports\danhsachduthilop.rst";
            using (var previewForm = new PreviewForm(rptdanhsachlop))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachlop.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void FrmSinhVienPhongThi_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void uG_DanhSach_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = @"Mã Sinh Viên";
                band.Columns["HoSinhVien"].Header.Caption = @"Họ tên đệm";
                band.Columns["TenSinhVien"].Header.Caption = @"Tên Sinh Viên";
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["MaLop"].Header.Caption = @"Lớp";
                band.Columns["PhongThi"].Header.Caption = @"Phòng Thi";

                #endregion

                band.Columns["ID"].Hidden = true;
                band.Columns["MaKhoa"].Hidden = true;
                band.Columns["TenKhoa"].Hidden = true;

                band.Columns["STT"].Width = 50;
                band.Columns["HoSinhVien"].Width = 170;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["HoSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["TenSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["PhongThi"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSinhVien"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["PhongThi"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }
    }
}
