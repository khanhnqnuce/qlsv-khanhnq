using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_106_QuanLyKyThi : FunctionControlHasGrid
    {
        private readonly IList<Kythi> _listAdd = new List<Kythi>();
        private readonly IList<Kythi> _listUpdate = new List<Kythi>();

        public Frm_106_QuanLyKyThi()
        {
            InitializeComponent();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof (int));
            table.Columns.Add("STT", typeof (int));
            table.Columns.Add("MaKT", typeof (string));
            table.Columns.Add("TenKT", typeof (string));
            table.Columns.Add("NgayThi", typeof (DateTime));
            table.Columns.Add("TGLamBai", typeof(int));
            table.Columns.Add("TGBatDau", typeof(string));
            table.Columns.Add("TGKetThuc", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
               
                uG_DanhSach.DataSource = LoadData.Load(10);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void LoadFormDetail()
        {
            LoadGrid();
            if (uG_DanhSach.Rows.Count == 0)
            {
                InsertRow();
            }
            _listUpdate.Clear();
            _listAdd.Clear();
            IdDelete.Clear();
        }

        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "MaKT");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaKT");
                Stt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                foreach (var row in uG_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                {
                    var hs = new Kythi
                    {
                        MaKT = row.Cells["MaKT"].Text,
                        TenKT = row.Cells["TenKT"].Text,
                        NgayThi = row.Cells["NgayThi"].Text,
                        TGLamBai = int.Parse(row.Cells["TGLamBai"].Text),
                        TGBatDau = row.Cells["TGBatDau"].Text,
                        TGKetThuc = row.Cells["TGKetThuc"].Text,
                    };
                    _listAdd.Add(hs);
                }
                UpdateData.UpdateKyThi(_listUpdate);
                DeleteData.Xoa(IdDelete,"KYTHI");
                InsertData.ThemKythi(_listAdd);
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void XoaDetail()
        {
            try
            {
                QlsvSevice.Xoa("Kythi");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Stt()
        {
            for (var i = 0; i < uG_DanhSach.Rows.Count; i++)
            {
                uG_DanhSach.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        #endregion

        #region Event_uG

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                if (b)
                {
                    b = false;
                    return;
                }
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                    {
                        item.MaKT = uG_DanhSach.ActiveRow.Cells["MaKT"].Text;
                        item.TenKT = uG_DanhSach.ActiveRow.Cells["TenKT"].Text;
                        item.NgayThi = uG_DanhSach.ActiveRow.Cells["NgayThi"].Text;
                        item.TGLamBai = int.Parse(uG_DanhSach.ActiveRow.Cells["TGLamBai"].Text);
                        item.TGBatDau = uG_DanhSach.ActiveRow.Cells["TGBatDau"].Text;
                        item.TGKetThuc = uG_DanhSach.ActiveRow.Cells["TGKetThuc"].Text;
                        return;
                    }
                    var hs = new Kythi
                    {
                        ID = int.Parse(id),
                        MaKT = uG_DanhSach.ActiveRow.Cells["MaKT"].Text,
                        TenKT = uG_DanhSach.ActiveRow.Cells["TenKT"].Text,
                        NgayThi = uG_DanhSach.ActiveRow.Cells["NgayThi"].Text,
                        TGLamBai = int.Parse(uG_DanhSach.ActiveRow.Cells["TGLamBai"].Text),
                        TGBatDau = uG_DanhSach.ActiveRow.Cells["TGBatDau"].Text,
                        TGKetThuc = uG_DanhSach.ActiveRow.Cells["TGKetThuc"].Text,
                    };
                    _listUpdate.Add(hs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TGLamBai"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TGBatDau"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TGKetThuc"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["TenKT"].Width = 400;
                band.Columns["NgayThi"].MaskInput = FormResource.txtddmmyyyy;
                band.Columns["NgayThi"].Style = ColumnStyle.Date;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["TGLamBai"].FormatNumberic();
                band.Columns["TGBatDau"].FormatTime();
                band.Columns["TGKetThuc"].FormatTime();

                #region Caption

                band.Columns["MaKT"].Header.Caption = FormResource.txtMakythi;
                band.Columns["TenKT"].Header.Caption = FormResource.txtTenkythi;
                band.Columns["NgayThi"].Header.Caption = FormResource.txtNgaythi;
                band.Columns["TGLamBai"].Header.Caption = FormResource.txtThoigianlambai;
                band.Columns["TGBatDau"].Header.Caption = FormResource.txtThoigianbatdau;
                band.Columns["TGKetThuc"].Header.Caption = FormResource.txtThoigianketthuc;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.DisplayPromptMsg = false;
        }

        #endregion

        #region MenuStrip

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            SaveDetail();
        }

        #endregion

        private void FrmQuanLyKyThi_Load(object sender, EventArgs e)
        {
            LoadForm();
        }
    }
}
