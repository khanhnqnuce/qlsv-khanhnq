using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;
using Color = System.Drawing.Color;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmSapxepphongthi : FunctionControlHasGrid
    {
        private readonly BackgroundWorker _bgwInsert = null;

        public delegate void CustomHandler1(object sender);

        public event CustomHandler1 CloseDialog = null;

        public delegate void CustomHandler(object sender);

        public event CustomHandler ShowDialog = null;

        public FrmSapxepphongthi()
        {
            InitializeComponent();
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            //table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("MaSinhVien", typeof (string));
            table.Columns.Add("HoSinhVien", typeof (string));
            table.Columns.Add("TenSinhVien", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("PhongThi", typeof (string));
            table.Columns.Add("MaKhoa", typeof (int));

            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var listsinhvien = QlsvSevice.LoadSinhVien();
                var listphongthi = QlsvSevice.Load<PhongThi>();
                if (listsinhvien == null || listsinhvien.Count == 0) return;
                var truoc = 0;
                var sau = 0;
                var tg = 0;
                var table = GetTable();
                var stt = 1;
                foreach (var phongThi in listphongthi)
                {
                    truoc = truoc + tg;
                    sau = sau + phongThi.SucChua;
                    tg = phongThi.SucChua;
                    if (sau > listsinhvien.Count)
                    {
                        for (var i = truoc; i < listsinhvien.Count; i++)
                        {
                            table.Rows.Add(stt++, listsinhvien[i].MaSinhVien, listsinhvien[i].HoSinhVien,
                                listsinhvien[i].TenSinhVien, listsinhvien[i].Lop.MaLop, phongThi.TenPhong,
                                listsinhvien[i].Lop.IdKhoa);
                        }
                        break;
                    }
                    for (var i = truoc; i < sau; i++)
                    {
                        table.Rows.Add(stt++, listsinhvien[i].MaSinhVien, listsinhvien[i].HoSinhVien,
                                listsinhvien[i].TenSinhVien, listsinhvien[i].Lop.MaLop, phongThi.TenPhong,
                                listsinhvien[i].Lop.IdKhoa);
                    }
                }
                if (sau < listsinhvien.Count)
                    MessageBox.Show(
                        @"Còn " + (listsinhvien.Count - sau - 1) + @" Sinh viên chưa được sắp xếp vào phòng thi",
                        @"Thông báo");
                else
                    MessageBox.Show(@"Đã sắp xếp " + listsinhvien.Count + @" Sinh viên vào phòng thi", @"Thông báo");
                uG_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        public void LoadForm()
        {
            _bgwInsert.RunWorkerAsync();
            ShowDialog(null);
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
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", uG_DanhSach.DataSource);
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
            var tb = (DataTable) uG_DanhSach.DataSource;
            tb.Columns.Add("TenKhoa", typeof (string));
            foreach (DataRow row in tb.Rows)
            {
                var row1 = row;
                foreach (var khoa in listkhoa.Where(item=>item.ID == int.Parse(row1["MaKhoa"].ToString())))
                {
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
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", uG_DanhSach.DataSource);
            rptdanhsachlop.FilePath = Application.StartupPath + @"\Reports\danhsachduthilop.rst";
            using (var previewForm = new PreviewForm(rptdanhsachlop))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachlop.Prepare();
                previewForm.ShowDialog();
            }
        }

        #region BackgroundWorker

        private void bgwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke((Action) (LoadGrid));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void bgwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseDialog(null);
        }

        #endregion

        private void Sapxepphongthi_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = FormResource.txtMasinhvien;
                band.Columns["HoSinhVien"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSinhVien"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;
                band.Columns["PhongThi"].Header.Caption = @"Phòng thi";

                #endregion

                #region NoExit

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["HoSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["TenSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["PhongThi"].CellActivation = Activation.NoEdit;

                #endregion

                band.Columns["MaKhoa"].Hidden = true;
                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSinhVien"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["PhongThi"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["HoSinhVien"].Width = 170;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}