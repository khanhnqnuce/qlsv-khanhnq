using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Frm_209_GopKeQuaThi : FunctionControlHasGrid
    {
        private readonly IList<ThongKe> _listThongke = new List<ThongKe>();
        public Frm_209_GopKeQuaThi()
        {
            InitializeComponent();
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("HoSV", typeof(string));
            table.Columns.Add("TenSV", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("Diem1", typeof(int));
            table.Columns.Add("Diem2", typeof(int));
            table.Columns.Add("TongDiem", typeof(int));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var frm = new FrmGopKetQua {Check = false};
                frm.ShowDialog();
                var tb = LoadData.GopKetQua(frm.LstIdKyThi[0],frm.LstIdKyThi[1]);
                var tb1 = LoadData.GopKetQua1(frm.LstIdKyThi[0], frm.LstIdKyThi[1]);
                var tb2 = LoadData.GopKetQua2(frm.LstIdKyThi[0], frm.LstIdKyThi[1]);
                if (tb1.Rows.Count > 0) tb.Merge(tb1);
                if (tb2.Rows.Count > 0) tb.Merge(tb2);
                dgv_DanhSach.DataSource = tb;
                pnl_from.Visible = true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void LoadFormDetail()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                var frm = new FrmGopKQ();
                frm.ShowDialog();
                foreach (var row in dgv_DanhSach.Rows)
                {
                    var hs = new ThongKe
                    {
                        MaSV = int.Parse(row.Cells["MaSV"].Text),
                        Diem = int.Parse(row.Cells["TongDiem"].Text),
                        NamHoc = frm.txtNamHoc.Text,
                        HocKy = (string)frm.cbohocky.SelectedValue
                    };
                    _listThongke.Add(hs);
                }
                InsertData.ThemThongKe(_listThongke);
                MessageBox.Show(@"Thêm thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            RptLop();
        }

        private void RptLop()
        {

            //reportManager1.DataSources.Clear();
            //rptthongke.FilePath = Application.StartupPath + @"\Reports\thongke.rst";
            //rptthongke.GetReportParameter += GetParameter;
            //rptthongke.Prepare();
            //var previewForm = new PreviewForm(rptthongke)
            //{
            //    WindowState = FormWindowState.Maximized
            //};
            //previewForm.ShowInTaskbar = false;
            //previewForm.Show();
        }

        private void GetParameter(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                //double bosung = SearchData.Thongkediem(0, _idkythi).Rows.Count;
                //double toiec1 = SearchData.Thongkediem(1, _idkythi).Rows.Count;
                //double toiec2 = SearchData.Thongkediem(2, _idkythi).Rows.Count;
                //double toiec3 = SearchData.Thongkediem(3, _idkythi).Rows.Count;
                //double toiec4 = SearchData.Thongkediem(4, _idkythi).Rows.Count;
                //double miengiam = SearchData.Thongkediem(5, _idkythi).Rows.Count;
                //var tong = bosung + toiec1 + toiec2 + toiec3 + toiec4 + miengiam;

                //e.Parameters["bosung"].Value = bosung.ToString();
                //e.Parameters["toiec1"].Value = toiec1.ToString();
                //e.Parameters["toiec2"].Value = toiec2.ToString();
                //e.Parameters["toiec3"].Value = toiec3.ToString();
                //e.Parameters["toiec4"].Value = toiec4.ToString();
                //e.Parameters["miengiam"].Value = miengiam.ToString();
                //e.Parameters["TLbosung"].Value = Math.Round(bosung / tong * 100, 1).ToString();
                //e.Parameters["TLtoiec1"].Value = Math.Round(toiec1 / tong * 100, 1).ToString();
                //e.Parameters["TLtoiec2"].Value = Math.Round(toiec2 / tong * 100, 1).ToString();
                //e.Parameters["TLtoiec3"].Value = Math.Round(toiec3 / tong * 100, 1).ToString();
                //e.Parameters["TLtoiec4"].Value = Math.Round(toiec4 / tong * 100, 1).ToString();
                //e.Parameters["TLmiengiam"].Value = Math.Round(miengiam / tong * 100, 1).ToString();
                //e.Parameters["tong"].Value = tong.ToString();
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
                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã SV";
                band.Columns["HoSV"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSV"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["MaLop"].Header.Caption = @"Lớp";
                band.Columns["Diem1"].Header.Caption = @"Điểm thi 1";
                band.Columns["Diem2"].Header.Caption = @"Điểm thi 2";
                band.Columns["TongDiem"].Header.Caption = @"Tổng điểm";

                #endregion

                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["HoSV"].CellActivation = Activation.NoEdit;
                band.Columns["TenSV"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["Diem1"].CellActivation = Activation.NoEdit;
                band.Columns["Diem2"].CellActivation = Activation.NoEdit;
                band.Columns["TongDiem"].CellActivation = Activation.NoEdit;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Frm_209_GopKeQuaThi_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
