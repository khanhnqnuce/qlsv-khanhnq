using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using NPOI.SS.Formula.Functions;
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
        private readonly BackgroundWorker _bgwInsert;
        private bool _update;
        private string _namhoc;
        private string _hocky;

        public Frm_209_GopKeQuaThi()
        {
            InitializeComponent();
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("MaSV");
            table.Columns.Add("HoSV");
            table.Columns.Add("TenSV");
            table.Columns.Add("NgaySinh");
            table.Columns.Add("MaLop");
            table.Columns.Add("TongDiem");
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var tb1 = Statistic.GopKetQua(_list);
                var tb2 = Statistic.GopKetQua1(_list);
                if (tb2.Rows.Count > 0 && _list.Count > 1)
                {
                    IList<Sinhvien> list2 = new List<Sinhvien>();
                    foreach (DataRow row in tb2.Rows)
                    {
                        var a = int.Parse(row["MaSV"].ToString());
                        foreach (var sv in list2.Where(sv => sv.MaSV == a))
                        {
                            for (var i = 0; i < _list.Count; i++)
                            {
                                var ten = "Diem" + (i + 1);
                                var diem = row[ten].ToString();
                                if (!string.IsNullOrEmpty(diem))
                                    sv.Diemthi[i] = int.Parse(row[ten].ToString());
                            }
                            goto b;
                        }
                        var sv1 = new Sinhvien(_list.Count)
                        {
                            MaSV = a,
                            HoSV = row["HoSV"].ToString(),
                            TenSV = row["TenSV"].ToString(),
                            MaLop = row["MaLop"].ToString(),
                            NgaySinh = row["NgaySinh"].ToString(),
                        };
                        for (var i = 0; i < _list.Count; i++)
                        {
                            var ten = "Diem" + (i + 1);
                            var diem = row[ten].ToString();
                            if (!string.IsNullOrEmpty(diem))
                                sv1.Diemthi[i] = int.Parse(row[ten].ToString());
                            else
                                sv1.Diemthi[i] = 0;
                        }
                        list2.Add(sv1);
                        b:
                        ;
                    }

                    foreach (var item in list2)
                    {
                        tb1.Rows.Add(item.MaSV, item.HoSV, item.TenSV, item.NgaySinh, item.MaLop);
                        for (var i = 0; i < _list.Count; i++)
                        {
                            var ten = "Diem" + (i + 1);
                            tb1.Rows[tb1.Rows.Count - 1][ten] = item.Diemthi[i];
                        }
                        tb1.Rows[tb1.Rows.Count - 1]["TongDiem"] = item.Tinhtong();
                    }
                }
                Invoke((Action)(()=>dgv_DanhSach.DataSource = tb1));
                Invoke((Action)(() => pnl_from.Visible = true));
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
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
                var frm = new FrmGopKetQua { Check = false };
                frm.ShowDialog();
                _list = frm.LstIdKyThi;
                if (_list.Count>0)
                {
                    var thread = new Thread(LoadGrid) {IsBackground = true};
                    thread.Start();
                    OnShowDialog("Loading...");
                }
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
                foreach (var row in dgv_DanhSach.Rows)
                {
                    var hs = new ThongKe
                    {
                        MaSV = int.Parse(row.Cells["MaSV"].Text),
                        Diem = int.Parse(row.Cells["TongDiem"].Text),
                        NamHoc = _namhoc,
                        HocKy = _hocky
                    };
                    _listThongke.Add(hs);
                }
                if (_listThongke.Count > 0)
                {
                    InsertData.ThemThongKe(_listThongke);
                    MessageBox.Show(@"Thêm thành công");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Ghi()
        {
            var frm = new FrmGopKQ { Update = false };
            frm.ShowDialog();
            if(!frm.Update) return;
            _namhoc = frm.txtNamHoc.Text;
            _hocky = frm.cbohocky.SelectedValue.ToString();
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
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

        #region BackgroundWorker

        private void bgwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SaveDetail();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void bgwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        #endregion

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
            LoadFormDetail();
        }
    }

    public class Sinhvien
    {
        public int MaSV { get; set; }
        public string HoSV { get; set; }
        public string TenSV { get; set; }
        public string NgaySinh { get; set; }
        public string MaLop { get; set; }
        public int[] Diemthi { get; set; }
        public int TongDiem { get; set; }

        public Sinhvien(int count)
        {
            Diemthi = new int[count];
            TongDiem = 0;
        }

        public int Tinhtong()
        {
            foreach (var t in Diemthi)
            {
                TongDiem = TongDiem + t;
            }
            return TongDiem;
        }
    }
}
