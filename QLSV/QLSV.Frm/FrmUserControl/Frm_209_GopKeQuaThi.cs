using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
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
        private IList<int> _list; 
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
                _list = frm.LstIdKyThi;
                var tb1 = Statistic.GopKetQua(frm.LstIdKyThi);
                var tb2 = Statistic.GopKetQua1(frm.LstIdKyThi);
                tb1.Merge(tb2);
                dgv_DanhSach.DataSource = tb1;
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
            reportManager1.DataSources.Add("danhsach",dgv_DanhSach.DataSource);
            rptgopdiem.FilePath = Application.StartupPath + @"\Reports\gopdiem.rst";
            rptgopdiem.Prepare();
            var previewForm = new PreviewForm(rptgopdiem)
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false
            };
            previewForm.Show();
        }
        

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã SV";
                band.Columns["HoSV"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSV"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["MaLop"].Header.Caption = @"Lớp";
                band.Columns["TongDiem"].Header.Caption = @"Tổng điểm";

                #endregion

                band.Columns["MaSV"].Width = 110;
                band.Columns["HoSV"].Width = 150;
                band.Columns["TenSV"].Width = 110;
                band.Columns["NgaySinh"].Width = 110;
                band.Columns["MaLop"].Width = 110;
                band.Columns["TongDiem"].Width = 110;

                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TongDiem"].CellAppearance.TextHAlign = HAlign.Center;
                
                for (var i = 0; i < _list.Count; i++)
                {
                    var ten = "Diem" + (i + 1);
                    band.Columns[ten].Header.Caption = @"Điểm môn "+(i+1);
                    band.Columns[ten].Width = 110;
                    band.Columns[ten].CellAppearance.TextHAlign = HAlign.Center;
                }

                foreach (var coloum in band.Columns)
                {
                    coloum.CellActivation = Activation.NoEdit;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Frm_209_GopKeQuaThi_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
