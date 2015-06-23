using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using NPOI.SS.Formula.Functions;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public sealed partial class Frm_101_Danhmuckhoa : FunctionControlHasGrid
    {
        private readonly DataTable _dt;

        public Frm_101_Danhmuckhoa()
        {
            InitializeComponent();
            _dt = GetTable();
        }

        #region Exit

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("TenKhoa", typeof(string));
            return table;
        }

        private void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(15);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void LoadFormDetail()
        {
            LoadGrid();
            if (dgv_DanhSach.Rows.Count == 0)
            {
                InsertRow();
            }
            IdDelete.Clear();
        }

        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "TenKhoa");
        }

        protected override void DeleteRow()
        {
            try
            {
                bool check;
                if (dgv_DanhSach.Selected.Rows.Count > 0)
                {
                    foreach (var row in dgv_DanhSach.Selected.Rows)
                    {
                        var id = row.Cells["ID"].Text;
                        if (!string.IsNullOrEmpty(id))
                        {
                            IdDelete.Add(int.Parse(id));
                        }
                    }
                    check = true;
                }
                else if (dgv_DanhSach.ActiveRow != null)
                {
                    var id = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                    if (string.IsNullOrEmpty(id)) return;
                    check = false;
                    var idStr = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                    if (!string.IsNullOrEmpty(idStr))
                        IdDelete.Add(int.Parse(idStr));
                }
                else
                {
                    MessageBox.Show(@"Chọn Khoa để xóa");
                    return;
                }
                var b = DeleteData.KtraXoaThongTin(2, IdDelete);
                if (!b)
                {
                    MessageBox.Show(@"không thể xóa Khoa. Cần xóa lớp trước khi xóa Khoa",
                        FormResource.MsgCaption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    IdDelete.Clear();
                    return;
                }
                if (IdDelete.Count > 0 && DialogResult.Yes ==
                        MessageBox.Show(@"Bạn có chắc chắn muốn xóa không ?",
                            FormResource.MsgCaption,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                {
                    DeleteData.XoaKhoa(IdDelete);
                    Stt();
                    if (check)
                    {
                        DeleteAndUpdate = true;
                        dgv_DanhSach.DeleteSelectedRows(false);
                    }
                    else
                    {
                        DeleteAndUpdate = true;
                        dgv_DanhSach.ActiveRow.Delete(false);
                    }
                    MessageBox.Show(@"Xóa dữ liệu thành công", FormResource.MsgCaption);
                }
                IdDelete.Clear();
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
                var tbKhoa = _save.tbKhoa();
                if (ValidateData())
                {
                    MessageBox.Show(@"Vui lòng nhập đầy đủ thông tin", @"Lỗi");
                }
                else
                {
                    foreach (var row in dgv_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                    {
                        tbKhoa.Rows.Add(null, row.Cells["TenKhoa"].Text);
                    }
                    if (tbKhoa.Rows.Count > 0) _save.Bulk_Insert("KHOA",tbKhoa);
                    if (_dt.Rows.Count > 0)
                    {
                        _save.Bulk_Update("sp_UpdateKhoas", "@tbl", _dt);
                        _dt.Rows.Clear();
                    }
                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadFormDetail();
                }
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

        private bool ValidateData()
        {
            var inputTypes = new List<InputType>
            {
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.ChuoiRong
                
            };
            return ValidateHighlight.UltraGrid(dgv_DanhSach, inputTypes);
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("KHOA");
                LoadFormDetail();
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

        private void Stt()
        {
            for (var i = 0; i < dgv_DanhSach.Rows.Count; i++)
            {
                dgv_DanhSach.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        #endregion

        #region Event_uG

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
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
                _dt.Rows.Add(id, dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Text);
                //foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                //{
                //    item.TenKhoa = dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Text;
                //    return;
                //}
                //var hs = new Khoa
                //{
                //    ID = int.Parse(id),
                //    TenKhoa = dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Text,
                //};
                //_listUpdate.Add(hs);
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
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MaxWidth = 70;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["TenKhoa"].Header.Caption = FormResource.txtTenkhoa;

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

        #endregion

        private void FrmDanhmuckhoa_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void dgv_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !DeleteAndUpdate;
            DeleteAndUpdate = false;
        }
    }
}
