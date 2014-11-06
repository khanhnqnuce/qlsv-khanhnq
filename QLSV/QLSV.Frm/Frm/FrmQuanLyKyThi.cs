using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.Frm
{
    public partial class FrmQuanLyKyThi : FunctionControlHasGrid
    {
        private readonly List<Kythi> _listAdd = new List<Kythi>();
        private readonly List<Kythi> _listUpdate = new List<Kythi>();

        public FrmQuanLyKyThi()
        {
            InitializeComponent();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof (int));
            table.Columns.Add("STT", typeof (int));
            table.Columns.Add("MaKyThi", typeof (string));
            table.Columns.Add("TenKythi", typeof (string));
            table.Columns.Add("NgayThi", typeof (DateTime));
            table.Columns.Add("ThoiGianLamBai", typeof (int));
            table.Columns.Add("ThoiGianBatDau", typeof (string));
            table.Columns.Add("ThoiGianKetThuc", typeof (string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
                var table = GetTable();
                var danhsach = QlsvSevice.Load<Kythi>();
                var stt = 1;
                foreach (var hs in danhsach)
                {
                    table.Rows.Add(hs.ID, stt, hs.MaKyThi, hs.TenKyThi, hs.NgayThi, hs.ThoiGianLamBai, hs.ThoiGianBatDau,
                        hs.ThoiGianKetThuc);
                    stt++;
                }
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
            InsertRow(uG_DanhSach, "STT", "MaKyThi");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaKyThi");
                STT();
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
                        MaKyThi = row.Cells["MaKyThi"].Text,
                        TenKyThi = row.Cells["TenKyThi"].Text,
                        NgayThi = row.Cells["NgayThi"].Text,
                        ThoiGianLamBai = int.Parse(row.Cells["ThoiGianLamBai"].Text),
                        ThoiGianBatDau = row.Cells["ThoiGianBatDau"].Text,
                        ThoiGianKetThuc = row.Cells["ThoiGianKetThuc"].Text,
                    };
                    _listAdd.Add(hs);
                }
                QlsvSevice.ThemAll(_listAdd);
                QlsvSevice.SuaAll(_listUpdate);
                QlsvSevice.Xoa(IdDelete, "Kythi");
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadForm();

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
                if (DialogResult.Yes ==
                    MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                {
                    QlsvSevice.Xoa("Kythi");
                    LoadForm();
                }
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

        private void STT()
        {
            for (var i = 0; i < uG_DanhSach.Rows.Count; i++)
            {
                uG_DanhSach.Rows[i].Cells[1].Value = i + 1;
            }
        }

        #endregion

        #region Event_uG

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                    {
                        item.MaKyThi = uG_DanhSach.ActiveRow.Cells["MaKyThi"].Text;
                        item.TenKyThi = uG_DanhSach.ActiveRow.Cells["TenKyThi"].Text;
                        item.NgayThi = uG_DanhSach.ActiveRow.Cells["NgayThi"].Text;
                        item.ThoiGianLamBai = int.Parse(uG_DanhSach.ActiveRow.Cells["ThoiGianLamBai"].Text);
                        item.ThoiGianBatDau = uG_DanhSach.ActiveRow.Cells["ThoiGianBatDau"].Text;
                        item.ThoiGianKetThuc = uG_DanhSach.ActiveRow.Cells["ThoiGianKetThuc"].Text;
                        return;
                    }
                    var hs = new Kythi
                    {
                        ID = int.Parse(id),
                        MaKyThi = uG_DanhSach.ActiveRow.Cells["MaKyThi"].Text,
                        TenKyThi = uG_DanhSach.ActiveRow.Cells["TenKyThi"].Text,
                        NgayThi = uG_DanhSach.ActiveRow.Cells["NgayThi"].Text,
                        ThoiGianLamBai = int.Parse(uG_DanhSach.ActiveRow.Cells["ThoiGianLamBai"].Text),
                        ThoiGianBatDau = uG_DanhSach.ActiveRow.Cells["ThoiGianBatDau"].Text,
                        ThoiGianKetThuc = uG_DanhSach.ActiveRow.Cells["ThoiGianKetThuc"].Text,
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
                band.Columns["ThoiGianLamBai"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["ThoiGianBatDau"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["ThoiGianKetThuc"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["TenKythi"].Width = 400;
                band.Columns["NgayThi"].MaskInput = FormResource.txtddmmyyyy;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["ThoiGianLamBai"].FormatNumberic();
                band.Columns["ThoiGianBatDau"].FormatTime();
                band.Columns["ThoiGianKetThuc"].FormatTime();

                #region Caption

                band.Columns["MaKyThi"].Header.Caption = FormResource.txtMakythi;
                band.Columns["TenKythi"].Header.Caption = FormResource.txtTenkythi;
                band.Columns["NgayThi"].Header.Caption = FormResource.txtNgaythi;
                band.Columns["ThoiGianLamBai"].Header.Caption = FormResource.txtThoigianlambai;
                band.Columns["ThoiGianBatDau"].Header.Caption = FormResource.txtThoigianbatdau;
                band.Columns["ThoiGianKetThuc"].Header.Caption = FormResource.txtThoigianketthuc;

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
            LoadForm();
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
