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

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_208_ThongKeDiem : FunctionControlHasGrid
    {
        private readonly int _idkythi;

        public Frm_208_ThongKeDiem(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
        }

        #region

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSinhVien", typeof(string));
            table.Columns.Add("HoSinhVien", typeof(string));
            table.Columns.Add("TenSinhVien", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("DiemThi", typeof(int));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                lbthongke.Text = "";
                dgv_DanhSach.DataSource = SearchData.Thongkediem(6,_idkythi);
                pnl_from.Visible = true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            RptLop();
        }

        private void RptLop()
        {
            
            reportManager1.DataSources.Clear();
            rptthongke.FilePath = Application.StartupPath + @"\Reports\thongke.rst";
            using (var previewForm = new PreviewForm(rptthongke))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptthongke.GetReportParameter += GetParameter;
                rptthongke.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void GetParameter(object sender,
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

            band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            band.Override.HeaderAppearance.FontData.SizeInPoints = 11;

            #region Caption

            band.Columns["MaSV"].Header.Caption = FormResource.txtMasinhvien;
            band.Columns["HoSV"].Header.Caption = FormResource.txtHosinhvien;
            band.Columns["TenSV"].Header.Caption = FormResource.txtTensinhvien;
            band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
            band.Columns["DiemThi"].Header.Caption = @"Điểm thi";
            band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;

            #endregion

            band.Columns["STT"].CellActivation = Activation.NoEdit;
            band.Columns["MaSV"].CellActivation = Activation.NoEdit;
            band.Columns["HoSV"].CellActivation = Activation.NoEdit;
            band.Columns["TenSV"].CellActivation = Activation.NoEdit;
            band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
            band.Columns["MaLop"].CellActivation = Activation.NoEdit;
            band.Columns["DiemThi"].CellActivation = Activation.NoEdit;

            band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;

            band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
            band.Columns["STT"].Width = 50;
            band.Columns["HoSV"].Width = 170;
            band.Columns["TenSV"].Width = 150;
            band.Columns["NgaySinh"].Width = 150;
            band.Columns["MaLop"].Width = 150;
            band.Columns["DiemThi"].Width = 150;
        }
    }
}
