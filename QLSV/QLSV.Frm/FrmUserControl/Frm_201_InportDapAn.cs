using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Frm_201_InportDapAn : FunctionControlHasGrid
    {
        private readonly IList<DapAn> _listAdd = new List<DapAn>(); 
        private readonly BackgroundWorker _bgwInsert;

        private int _idKythi;

        public Frm_201_InportDapAn(int idkythi)
        {
            InitializeComponent();
            _idKythi = idkythi;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("MaMon", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("CauHoi", typeof(string));
            table.Columns.Add("Dapan", typeof(string));
            table.Columns.Add("IdKyThi", typeof(string));
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
                MessageBox.Show(ex.Message);
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
                var frmNapDuLieu = new FrmNapDuLieu(GetTable(), 1)
                {
                    ViTriHeader = 1
                };
                frmNapDuLieu.ShowDialog();
                var resultValue = frmNapDuLieu.ResultValue;
                if (resultValue == null || resultValue.Rows.Count == 0) return;
                var table = (DataTable)dgv_DanhSach.DataSource;

                table.Merge(resultValue);
                dgv_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Thêm 1 dòng trên UltraGrid
        /// </summary>
        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, null, "MaMon");
        }

        /// <summary>
        /// Xóa 1 dồng trên UltraGrid
        /// </summary>
        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(dgv_DanhSach, "IdKyThi", "MaMon");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    var hs = new DapAn
                    {
                        IdKyThi = _idKythi,
                        MaMon = row.Cells["MaMon"].Text,
                        MaDe = row.Cells["MaDe"].Text,
                        CauHoi = int.Parse(row.Cells["CauHoi"].Text),
                        Dapan = row.Cells["Dapan"].Text,
                        ThangDiem = 0
                    };

                    _listAdd.Add(hs);

                }
                if (_listAdd.Count <= 0) return;
                InsertData.ThemDapAn(_listAdd);
                MessageBox.Show(@"Đã lưu vào CSDL", FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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

        private void FrmInportDapAn_Load(object sender, EventArgs e)
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

                band.Columns["IdKyThi"].Hidden = true;

                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["MaMon"].MinWidth = 140;
                band.Columns["MaDe"].MinWidth = 140;
                band.Columns["CauHoi"].MinWidth = 140;
                band.Columns["Dapan"].MinWidth = 140;
                band.Columns["MaMon"].MaxWidth = 150;
                band.Columns["MaDe"].MaxWidth = 150;
                band.Columns["CauHoi"].MaxWidth = 150;
                band.Columns["Dapan"].MaxWidth = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaMon"].Header.Caption = @"Mã môn thi";
                band.Columns["MaDe"].Header.Caption = @"Mã đề thi";
                band.Columns["CauHoi"].Header.Caption = @"Câu hỏi";
                band.Columns["Dapan"].Header.Caption = @"Đáp án";

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

        private void menuStrip_Luulai_Click(object sender, EventArgs e)
        {
            Ghi();
        }

        #endregion

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
    }
}
