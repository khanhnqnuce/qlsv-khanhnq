using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
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
    public partial class FrmDapAnCacMaDe : FunctionControlHasGrid
    {
        public FrmDapAnCacMaDe()
        {
            InitializeComponent();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaMon", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("CauHoi", typeof(string));
            table.Columns.Add("Dapan", typeof(string));
            table.Columns.Add("IdKyThi", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(11);
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
                Invoke((Action)(LoadGrid));
                Invoke((Action)(() => IdDelete.Clear()));
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "MaMon");
        }

        protected override void DeleteRow()
        {

            DeleteRowGrid(dgv_DanhSach, "ID", "MaMon");
        }

        protected override void SaveDetail()
        {
            try
            {
                QlsvSevice.Xoa(IdDelete, "DapAn");
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("DapAn");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Huy()
        {
            try
            {
                var thread = new Thread(LoadFormDetail) { IsBackground = true };
                thread.Start();
                OnShowDialog("Loading...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Rptdanhsach()
        {
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", dgv_DanhSach.DataSource);
            rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\danhsachsinhvien.rst";
            using (var previewForm = new PreviewForm(rptdanhsachsinhvien))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachsinhvien.Prepare();
                previewForm.ShowDialog();
            }
        }

        #endregion

        #region Event uG

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                band.Columns["ID"].Hidden = true;
                band.Columns["IdKyThi"].Hidden = true;

                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].Width = 50;
                band.Columns["MaMon"].Width = 150;
                band.Columns["MaDe"].Width = 150;
                band.Columns["CauHoi"].Width = 150;
                band.Columns["Dapan"].Width = 150;
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
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_KeyDown(object sender, KeyEventArgs e)
        {

        }

        #endregion

        #region MenuStrip


        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void menuStrip_dong_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void menuStrip_Sua_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

        private void FrmDapAnCacMaDe_Load(object sender, EventArgs e)
        {
            Huy();
        }
        
    }
}
