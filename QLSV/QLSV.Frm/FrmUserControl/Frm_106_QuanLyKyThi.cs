using System;
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
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_106_QuanLyKyThi : FunctionControlHasGrid
    {
        private readonly IList<Kythi> _listAdd = new List<Kythi>();
        private readonly IList<Kythi> _listUpdate = new List<Kythi>();
        public delegate void CustomHandler3(object sender);
        public event CustomHandler3 updatekythi = null;
        private string _quyen;
        public Frm_106_QuanLyKyThi(string quyen)
        {
            InitializeComponent();
            _quyen = quyen;
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
            uG_DanhSach.Rows[uG_DanhSach.Rows.Count - 1].Cells["TT"].Value = "Hiển thị";
            uG_DanhSach.Rows[uG_DanhSach.Rows.Count - 1].Cells["TrangThai"].Value = true;
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "TenKT");
                Stt();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override bool ValidateData()
        {
            var inputTypes = new List<InputType>
            {
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.ChuoiRong,
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                
            };
            return ValidateHighlight.UltraGrid(uG_DanhSach, inputTypes);
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
                    if (_listUpdate.Count <= 0 && IdDelete.Count <= 0 && _listAdd.Count <= 0) return;
                    if (_listUpdate.Count > 0) UpdateData.UpdateKyThi(_listUpdate);
                    if (IdDelete.Count > 0) DeleteData.Xoa(IdDelete, "KYTHI");
                    if (_listAdd.Count > 0) InsertData.ThemKythi(_listAdd);
                    updatekythi(null);
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

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("KYTHI");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
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
                if (B)
                {
                    B = false;
                    return;
                }
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                    {
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
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Columns["TrangThai"].Hidden = true;
                if (!_quyen.Equals("quantri")) band.Columns["TT"].Hidden = true;
                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TGLamBai"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TGBatDau"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TGKetThuc"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["TT"].CellActivation = Activation.NoEdit;
                band.Columns["TrangThai"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["TT"].Style = ColumnStyle.URL;
                //band.Columns["TT"].Style = ColumnStyle.Button;
                band.Columns["STT"].MinWidth = 60;
                band.Columns["MaKT"].MinWidth = 100;
                band.Columns["TenKT"].MinWidth = 270;
                band.Columns["NgayThi"].MinWidth = 140;
                band.Columns["TGLamBai"].MinWidth = 100;
                band.Columns["TGBatDau"].MinWidth = 100;
                band.Columns["TGKetThuc"].MinWidth = 100;
                band.Columns["TT"].MinWidth = 100;
                band.Columns["STT"].MaxWidth = 70;
                band.Columns["MaKT"].MaxWidth = 110;
                band.Columns["TenKT"].MaxWidth = 300;
                band.Columns["NgayThi"].MaxWidth = 150;
                band.Columns["TGLamBai"].MaxWidth = 110;
                band.Columns["TGBatDau"].MaxWidth = 110;
                band.Columns["TGKetThuc"].MaxWidth = 110;
                band.Columns["TT"].MaxWidth = 110;

                //band.Columns["NgayThi"].MaskInput = FormResource.txtddmmyyyy;
                //band.Columns["NgayThi"].Style = ColumnStyle.Date;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                //band.Columns["TGLamBai"].FormatNumberic();
                //band.Columns["TGBatDau"].FormatTime();
                //band.Columns["TGKetThuc"].FormatTime();

                #region Caption

                band.Columns["MaKT"].Header.Caption = FormResource.txtMakythi;
                band.Columns["TenKT"].Header.Caption = FormResource.txtTenkythi;
                band.Columns["NgayThi"].Header.Caption = FormResource.txtNgaythi;
                band.Columns["TGLamBai"].Header.Caption = FormResource.txtThoigianlambai;
                band.Columns["TGBatDau"].Header.Caption = FormResource.txtThoigianbatdau;
                band.Columns["TGKetThuc"].Header.Caption = FormResource.txtThoigianketthuc;
                band.Columns["TT"].Header.Caption = @"Trạng thái";

                #endregion
            }
            catch (Exception ex)
            {
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

        private void uG_DanhSach_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key != "TT") return;
                if (string.IsNullOrEmpty(e.Cell.Row.Cells["ID"].Text)) return;
                if (bool.Parse(e.Cell.Row.Cells["TrangThai"].Text))
                {
                    e.Cell.Value = "Ẩn";
                    e.Cell.Row.Cells["TrangThai"].Value = false;
                    var hs = new Kythi
                    {
                        ID = int.Parse(e.Cell.Row.Cells["ID"].Text),
                        TrangThai = false
                    };
                    UpdateData.UpdateTrangThaiKt(hs);
                }
                else
                {
                    e.Cell.Value = "Hiển thị";
                    e.Cell.Row.Cells["TrangThai"].Value = true;
                    var hs = new Kythi
                    {
                        ID = int.Parse(e.Cell.Row.Cells["ID"].Text),
                        TrangThai = true
                    };
                    UpdateData.UpdateTrangThaiKt(hs);
                }
                updatekythi(sender);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
