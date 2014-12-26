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
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_103_Danhmuclop : FunctionControlHasGrid
    {
        private readonly List<Lop> _listAdd = new List<Lop>();
        private readonly List<Lop> _listUpdate = new List<Lop>();

        public Frm_103_Danhmuclop()
        {
            InitializeComponent();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("IdKhoa", typeof(string));
            table.Columns.Add("GhiChu", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
                var a = LoadData.Load(16);
                uG_DanhSach.DataSource = LoadData.Load(16);
            }
            catch (Exception ex)
            {
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
            InsertRow(uG_DanhSach, "STT", "MaLop");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaLop");
                Stt();
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
                foreach (var row in uG_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                {
                    var hs = new Lop
                    {
                        MaLop = row.Cells["MaLop"].Text,
                        GhiChu = row.Cells["GhiChu"].Text,
                        IdKhoa = int.Parse(row.Cells["IdKhoa"].Value.ToString())
                    };
                    _listAdd.Add(hs);
                }
                if (_listUpdate.Count > 0) UpdateData.UpdateLop(_listUpdate);
                if (IdDelete.Count > 0) DeleteData.Xoa(IdDelete, "LOP");
                if (_listAdd.Count > 0) InsertData.ThemLop(_listAdd);
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadFormDetail();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("LOP");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                        item.MaLop = uG_DanhSach.ActiveRow.Cells["MaLop"].Text;
                        item.IdKhoa = int.Parse(uG_DanhSach.ActiveRow.Cells["IdKhoa"].Value.ToString());
                        item.GhiChu = uG_DanhSach.ActiveRow.Cells["GhiChu"].Text;
                        return;
                    }
                    var hs = new Lop
                    {
                        ID = int.Parse(id),
                        MaLop = uG_DanhSach.ActiveRow.Cells["MaLop"].Text,
                        IdKhoa = int.Parse(uG_DanhSach.ActiveRow.Cells["IdKhoa"].Value.ToString()),
                        GhiChu = uG_DanhSach.ActiveRow.Cells["GhiChu"].Text,
                    };
                    _listUpdate.Add(hs);
                }
            }
            catch (Exception ex)
            {
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
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["MaLop"].Width = 200;
                band.Columns["IdKhoa"].Width = 400;
                band.Columns["GhiChu"].Width = 300;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["IdKhoa"].Loadcbokhoa();
                band.Columns["IdKhoa"].Style = ColumnStyle.DropDownList;
                #region Caption

                band.Columns["MaLop"].Header.Caption = @"Mã lớp";
                band.Columns["IdKhoa"].Header.Caption = @"Tên khoa";
                band.Columns["GhiChu"].Header.Caption = @"Ghi chú";

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
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
        
        private void FrmDanhmuclop_Load(object sender, EventArgs e)
        {
            LoadForm();
        }
    }
}
