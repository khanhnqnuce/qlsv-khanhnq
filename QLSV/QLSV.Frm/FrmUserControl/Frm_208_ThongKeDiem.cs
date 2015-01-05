using System;
using System.Data;
using System.Drawing;
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
    public partial class Frm_208_ThongKeDiem : FunctionControlHasGrid
    {
        private readonly int _idkythi;
        private readonly FrmTimkiem _frmTimkiem;
        private UltraGridRow _newRow;

        public Frm_208_ThongKeDiem(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
            _frmTimkiem = new FrmTimkiem();
            _frmTimkiem.Timkiemsinhvien += Timkiemsinhvien;
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("IdKyThi", typeof(string));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("KetQua", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("DiemThi", typeof(int));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                lbthongke.Text = "";
                var tb = LoadData.Load(15, _idkythi);
                if (tb.Rows.Count == 0)
                {
                    MessageBox.Show(@"Sinh viên chưa được chấm thi",@"Thông báo");
                }
                dgv_DanhSach.DataSource = tb;
                pnl_from.Visible = true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            var frm = new FrmCheckInDiem
            {
                Update = false
            };
            frm.ShowDialog();
            if(frm.rdodanhsach.Checked && frm.Update)
                Rptdanhsach();
            else if(frm.rdoThongke.Checked && frm.Update)
                Rptthongke();
        }

        private void Rptdanhsach()
        {
            var tblop = LoadData.Load(4, _idkythi);
            var tb = LoadData.Load(10, _idkythi);
            foreach (DataRow rowl in tblop.Rows)
            {
                var stt = 1;
                var malop = rowl["MaLop"].ToString();
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["MaLop"].ToString().Equals(malop)))
                {
                    row["STT"] = stt++;
                }
            }
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdiemthi.FilePath = Application.StartupPath + @"\Reports\diemthi.rst";
            rptdiemthi.GetReportParameter += GetParameter;
            rptdiemthi.Prepare();
            var previewForm = new PreviewForm(rptdiemthi)
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false
            };
            previewForm.Show();

        }

        private void GetParameter(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                var tb = LoadData.Load(3, _idkythi);
                foreach (DataRow row in tb.Rows)
                {
                    e.Parameters["TenKT"].Value = row["TenKT"].ToString();
                    e.Parameters["NgayThi"].Value = row["NgayThi"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }


        private void Rptthongke()
        {

            reportManager1.DataSources.Clear();
            rptthongke.FilePath = Application.StartupPath + @"\Reports\thongke.rst";
            rptthongke.GetReportParameter += GetParameter1;
            rptthongke.Prepare();
            var previewForm = new PreviewForm(rptthongke)
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false
            };
            previewForm.Show();
        }

        private void GetParameter1(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                double bosung = SearchData.Thongkediem(0, _idkythi).Rows.Count;
                double toiec1 = SearchData.Thongkediem(1, _idkythi).Rows.Count;
                double toiec2 = SearchData.Thongkediem(2, _idkythi).Rows.Count;
                double toiec3 = SearchData.Thongkediem(3, _idkythi).Rows.Count;
                double toiec4 = SearchData.Thongkediem(4, _idkythi).Rows.Count;
                double miengiam = SearchData.Thongkediem(5, _idkythi).Rows.Count;
                var tong = bosung + toiec1 + toiec2 + toiec3 + toiec4 + miengiam;

                e.Parameters["bosung"].Value = bosung.ToString();
                e.Parameters["toiec1"].Value = toiec1.ToString();
                e.Parameters["toiec2"].Value = toiec2.ToString();
                e.Parameters["toiec3"].Value = toiec3.ToString();
                e.Parameters["toiec4"].Value = toiec4.ToString();
                e.Parameters["miengiam"].Value = miengiam.ToString();
                e.Parameters["TLbosung"].Value = Math.Round(bosung / tong*100, 1).ToString();
                e.Parameters["TLtoiec1"].Value = Math.Round(toiec1 / tong * 100, 1).ToString();
                e.Parameters["TLtoiec2"].Value = Math.Round(toiec2 / tong * 100, 1).ToString();
                e.Parameters["TLtoiec3"].Value = Math.Round(toiec3 / tong * 100, 1).ToString();
                e.Parameters["TLtoiec4"].Value = Math.Round(toiec4 / tong * 100, 1).ToString();
                e.Parameters["TLmiengiam"].Value = Math.Round(miengiam / tong * 100, 1).ToString();
                e.Parameters["tong"].Value = tong.ToString();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

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
                    _newRow = row;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Frm_208_ThongKeDiem_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cbothongke_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbthongke.Text = "";
            dgv_DanhSach.DataSource = SearchData.Thongkediem(cbothongke.SelectedIndex,_idkythi);
            lbthongke.Text = @"Có "+ dgv_DanhSach.Rows.Count+ @" Sinh viên có điểm " + cbothongke.Text;
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.Groups.Clear();
            band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            band.Override.HeaderAppearance.FontData.SizeInPoints = 10;

            #region Caption
            var columns = band.Columns;
            band.ColHeadersVisible = false;
            var group0 = band.Groups.Add("STT");
            var group1 = band.Groups.Add("Mã SV");
            var group2 = band.Groups.Add("Họ và tên");
            var group3 = band.Groups.Add("Ngày sinh");
            var group4 = band.Groups.Add("Lớp");
            var group5 = band.Groups.Add("Điểm thi");
            columns["STT"].Group = group0;
            columns["MaSV"].Group = group1;
            columns["HoSV"].Group = group2;
            columns["TenSV"].Group = group2;
            columns["NgaySinh"].Group = group3;
            columns["MaLop"].Group = group4;
            columns["DiemThi"].Group = group5;

            #endregion

            band.Columns["STT"].CellActivation = Activation.NoEdit;
            band.Columns["MaSV"].CellActivation = Activation.NoEdit;
            band.Columns["HoSV"].CellActivation = Activation.NoEdit;
            band.Columns["TenSV"].CellActivation = Activation.NoEdit;
            band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
            band.Columns["MaLop"].CellActivation = Activation.NoEdit;
            band.Columns["DiemThi"].CellActivation = Activation.NoEdit;

            band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["DiemThi"].CellAppearance.TextHAlign = HAlign.Center;

            band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
            #region Size

            band.Columns["STT"].MinWidth = 60;
            band.Columns["STT"].MaxWidth = 80;
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
            band.Columns["DiemThi"].MinWidth = 100;
            band.Columns["DiemThi"].MaxWidth = 120;

            #endregion

            dgv_DanhSach.DisplayLayout.UseFixedHeaders = true;
            dgv_DanhSach.DisplayLayout.FixedHeaderOffImage = Properties.Resources.trang;
            dgv_DanhSach.DisplayLayout.FixedHeaderOnImage = Properties.Resources.trang;
            group0.Header.Fixed = true;
            group1.Header.Fixed = true;
            group2.Header.Fixed = true; 
            group3.Header.Fixed = true;
            group4.Header.Fixed = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadGrid();
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
