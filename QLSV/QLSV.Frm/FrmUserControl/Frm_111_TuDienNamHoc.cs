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
using QLSV.Data.Utils.Data;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_111_TuDienNamHoc : FunctionControlHasGrid
    {
        private readonly List<NamHoc> _listAdd = new List<NamHoc>();
        private readonly List<NamHoc> _listUpdate = new List<NamHoc>();

        public Frm_111_TuDienNamHoc()
        {
            InitializeComponent();
        }

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NamHoc", typeof(string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(111);
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
                LoadGrid();
                if (dgv_DanhSach.Rows.Count == 0)
                {
                    InsertRow();
                }
                _listUpdate.Clear();
                _listAdd.Clear();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "NamHoc");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(dgv_DanhSach, "ID", "NamHoc");
                if (IdDelete.Count > 0)
                {
                    DeleteData.XoaNamHoc(IdDelete);
                    Stt();
                    MessageBox.Show(@"Xóa dữ liệu thành công.", @"Thông báo");
                }
                IdDelete.Clear();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Stt()
        {
            for (var i = 0; i < dgv_DanhSach.Rows.Count; i++)
            {
                dgv_DanhSach.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                if (ValidateData())
                {
                    MessageBox.Show(@"Vui lòng nhập đầy đủ thông tin", @"Lỗi");
                }
                else
                {
                    foreach (var row in dgv_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                    {
                        var hs = new NamHoc
                        {
                            namhoc = row.Cells["NamHoc"].Text
                        };
                        _listAdd.Add(hs);
                    }
                    if (_listUpdate.Count <= 0 && IdDelete.Count <= 0 && _listAdd.Count <= 0) return;
                    if (_listUpdate.Count > 0) UpdateData.UpdateNamHoc(_listUpdate);
                    if (_listAdd.Count > 0) InsertData.ThemNamHoc(_listAdd);
                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadFormDetail();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected virtual bool ValidateData()
        {
            var inputTypes = new List<InputType>
            {
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.ChuoiRong,
            };
            return ValidateHighlight.UltraGrid(dgv_DanhSach, inputTypes);
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("NAMHOC");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.Columns["ID"].Hidden = true;
            band.Columns["STT"].MinWidth = 60;
            band.Columns["STT"].MaxWidth = 70;
            band.Columns["NamHoc"].MinWidth = 190;
            band.Columns["NamHoc"].MaxWidth = 200;
            band.Override.CellAppearance.TextHAlign = HAlign.Center;
            band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
            band.Columns["STT"].CellActivation = Activation.NoEdit;
            band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
            band.Columns["NamHoc"].Header.Caption = @"Năm học";
        }

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void Frm_111_TuDienNamHoc_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dgv_DanhSach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.AboveCell, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Down:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.BelowCell, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Right:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.NextCellByTab, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                    case Keys.Left:
                        dgv_DanhSach.PerformAction(UltraGridAction.ExitEditMode, false, false);
                        dgv_DanhSach.PerformAction(UltraGridAction.PrevCellByTab, false, false);
                        e.Handled = true;
                        dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void dgv_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                if (DeleteAndUpdate)
                {
                    DeleteAndUpdate = false;
                    return;
                }
                var id = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                if (string.IsNullOrEmpty(id)) return;
                foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                {
                    item.namhoc = dgv_DanhSach.ActiveRow.Cells["NamHoc"].Text;
                    return;
                }
                var hs = new NamHoc
                {
                    ID = int.Parse(id),
                    namhoc = dgv_DanhSach.ActiveRow.Cells["NamHoc"].Text
                };
                _listUpdate.Add(hs);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }


    }
}
