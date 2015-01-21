using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_203_InportBaiLam : FunctionControlHasGrid
    {

        private readonly IList<BaiLam> _listAdd = new List<BaiLam>();
        private int _idKythi;

        private readonly BackgroundWorker _bgwInsert;

        public Frm_203_InportBaiLam(int idkythi)
        {
            InitializeComponent();
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;

            _idKythi = idkythi;
        }

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("KetQua", typeof(string));
            return table;
        }

        protected override void LoadFormDetail()
        {
            try
            {
                var table = GetTable();
                dgv_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Hàm lấy dữ liệu từ file excel
        /// </summary>
        public void Napdulieu()
        {
            try
            {
                var tb = GetTable();
                var stt = 1;
                var dialog = new OpenFileDialog
                {
                    Filter = @"Tập tin (.txt)|*.txt",
                    Multiselect = false,
                    Title = @"Chọn tập tin"
                };
                var dlr = dialog.ShowDialog();
                if (dlr != DialogResult.OK) return;
                var fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                var sr = new StreamReader(fs);
                var str = sr.ReadLine();
                while (str != null)
                {
                    var chuoi = str.Replace("\"", "");
                    var bailam = chuoi.Split(',');
                    tb.Rows.Add(null, stt++, bailam[0], bailam[1],bailam[2]);
                    str = sr.ReadLine();
                }
                sr.Close();
                fs.Close();

                dgv_DanhSach.DataSource = tb;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Thêm 1 dòng trên UltraGrid
        /// </summary>
        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "MaSV");
        }

        /// <summary>
        /// Xóa 1 dồng trên UltraGrid
        /// </summary>
        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(dgv_DanhSach, "ID", "MaSV");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        protected override void SaveDetail()
        {
            try
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    var hs = new BaiLam
                    {
                        MaSV = int.Parse(row.Cells["MaSV"].Text),
                        MaDe = row.Cells["MaDe"].Text,
                        KetQua = row.Cells["KetQua"].Text,
                        IdKyThi = _idKythi
                    };
                    _listAdd.Add(hs);
                }

                InsertData.ThemBaiLam(_listAdd);
                MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Ghi()
        {
            if (dgv_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
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

        private void FrmInportBaiLam_Load(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        /// <summary>
        /// Hàm khởi tạo của UltraGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                band.Columns["ID"].Hidden = true;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaDe"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["MaDe"].CellActivation = Activation.NoEdit;
                band.Columns["KetQua"].CellActivation = Activation.NoEdit;
                band.Columns["ID"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].MinWidth = 60;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaSV"].MinWidth = 140;
                band.Columns["MaSV"].MaxWidth = 150;
                band.Columns["MaDe"].MinWidth = 140;
                band.Columns["MaDe"].MaxWidth = 150;
                band.Columns["KetQua"].MinWidth = 640;
                band.Columns["KetQua"].MaxWidth = 650;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã sinh viên";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["KetQua"].Header.Caption = @"Bài làm sinh viên";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #region Menu Strip

        private void menuStrip_Inport_Click(object sender, EventArgs e)
        {
            Napdulieu();
        }

        private void menuStrip_Themmoi_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_Xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        #endregion
    }
}
