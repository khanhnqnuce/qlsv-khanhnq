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
using QLSV.Frm.Frm;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_102_Danhmuclop : FunctionControlHasGrid
    {
        private readonly List<Lop> _listAdd = new List<Lop>();
        private readonly List<Lop> _listUpdate = new List<Lop>();

        public Frm_102_Danhmuclop()
        {
            InitializeComponent();
        }

        #region Exit

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("IdKhoa", typeof(string));
            table.Columns.Add("TenKhoa", typeof(string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
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
            _listUpdate.Clear();
            _listAdd.Clear();
            IdDelete.Clear();
        }

        protected override void InsertRow()
        {
            var frm = new FrmThemLop();
            frm.ShowDialog();
            LoadGrid();
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaLop");
                if (IdDelete.Count <= 0) return;
                if (IdDelete.Count > 0) DeleteData.Xoa(IdDelete, "LOP");
                MessageBox.Show(@"Xóa dữ liệu thành công", FormResource.MsgCaption);
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
                if (ValidateData())
                {
                    MessageBox.Show(@"Vui lòng nhập đầy đủ thông tin", @"Lỗi");
                }
                else
                {
                    //foreach (var row in uG_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                    //{
                    //    var hs = new Lop
                    //    {
                    //        MaLop = row.Cells["MaLop"].Text,
                    //        IdKhoa = int.Parse(row.Cells["IdKhoa"].Value.ToString())
                    //    };
                    //    _listAdd.Add(hs);
                    //}
                    if (_listUpdate.Count <= 0 && IdDelete.Count <= 0) return;
                    if (_listUpdate.Count > 0) UpdateData.UpdateLop(_listUpdate);
                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption);
                    LoadFormDetail();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                InputType.ChuoiRong,
                InputType.KhongKiemTra
                
            };
            return ValidateHighlight.UltraGrid(uG_DanhSach, inputTypes);
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
            //try
            //{
            //    if (B)
            //    {
            //        B = false;
            //        return;
            //    }
            //    var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
            //    if (!string.IsNullOrEmpty(id))
            //    {
            //        foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
            //        {
            //            item.MaLop = uG_DanhSach.ActiveRow.Cells["MaLop"].Text;
            //            item.IdKhoa = int.Parse(uG_DanhSach.ActiveRow.Cells["IdKhoa"].Value.ToString());
            //            return;
            //        }
            //        var hs = new Lop
            //        {
            //            ID = int.Parse(id),
            //            MaLop = uG_DanhSach.ActiveRow.Cells["MaLop"].Text,
            //            IdKhoa = int.Parse(uG_DanhSach.ActiveRow.Cells["IdKhoa"].Value.ToString()),
                        
            //        };
            //        _listUpdate.Add(hs);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log2File.LogExceptionToFile(ex);
            //}
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Columns["IdKhoa"].Hidden = true;
                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["TenKhoa"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                #region MyRegion
                band.Columns["STT"].MinWidth = 50;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaLop"].MinWidth = 100;
                band.Columns["MaLop"].MaxWidth = 120;
                band.Columns["IdKhoa"].MinWidth = 250;
                band.Columns["IdKhoa"].MaxWidth = 300;
                #endregion
                
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                #region Caption

                band.Columns["MaLop"].Header.Caption = @"Mã lớp";
                band.Columns["TenKhoa"].Header.Caption = @"Tên khoa";

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

        private void uG_DanhSach_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.Cancel = !B;
            B = false;
        }
    }
}
