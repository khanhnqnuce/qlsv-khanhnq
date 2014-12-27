using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    public partial class Frm_ChonSinhVien : FunctionControlHasGrid
    {
        private readonly IList<KTPhong> _listKtPhong = new List<KTPhong>();
        private readonly IList<XepPhong> _listXepPhong = new List<XepPhong>();
        private readonly int _idkythi;

        public Frm_ChonSinhVien(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(string));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("HoSV", typeof(string));
            table.Columns.Add("TenSV", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = LoadData.Load(12,_idkythi);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            var frm = new FrmChonSv(_idkythi);
            frm.ShowDialog();
            LoadGrid();
        }

        protected override void DeleteRow()
        {
            try
            {
                try
                {
                    if (dgv_DanhSach.Selected.Rows.Count > 0)
                    {
                        if (DialogResult.Yes ==
                            MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question))
                        {
                            foreach (var row in dgv_DanhSach.Selected.Rows)
                            {
                                var masv = row.Cells["MaSV"].Text;
                                var idPhong = row.Cells["IdPhong"].Text;
                                if (!string.IsNullOrEmpty(idPhong))
                                {
                                    var ktp = new KTPhong
                                    {
                                        IdKyThi = _idkythi,
                                        IdPhong = int.Parse(idPhong)
                                    };
                                    _listKtPhong.Add(ktp);
                                }
                                var xp = new XepPhong
                                {
                                    IdKyThi = _idkythi,
                                    IdSV = int.Parse(masv)
                                };
                                _listXepPhong.Add(xp);
                            }
                            dgv_DanhSach.DeleteSelectedRows(false);
                        }
                    }
                    else if (dgv_DanhSach.ActiveRow != null)
                    {
                        var index = dgv_DanhSach.ActiveRow.Index;
                        if (DialogResult.Yes ==
                            MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question))
                        {
                            var masv = dgv_DanhSach.ActiveRow.Cells["MaSV"].Text;
                            var idPhong = dgv_DanhSach.ActiveRow.Cells["IdPhong"].Text;
                            if (!string.IsNullOrEmpty(idPhong))
                            {
                                var ktp = new KTPhong
                                {
                                    IdKyThi = _idkythi,
                                    IdPhong = int.Parse(idPhong)
                                };
                                _listKtPhong.Add(ktp);
                            }
                            var xp = new XepPhong
                            {
                                IdKyThi = _idkythi,
                                IdSV = int.Parse(masv)
                            };
                            _listXepPhong.Add(xp);
                            dgv_DanhSach.ActiveRow.Delete(false);
                            if (index > 0)
                                dgv_DanhSach.Rows[index - 1].Cells[2].Activate();
                            else
                                dgv_DanhSach.Rows[index].Cells[2].Activate();
                            
                            dgv_DanhSach.PerformAction(UltraGridAction.EnterEditMode);
                        }
                    }
                    Stt();
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
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
                DeleteData.Xoa("XEPPHONG", _idkythi);
                UpdateData.UpdateKtPhongNull(_idkythi);
                LoadGrid();
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
                if (_listKtPhong.Count > 0) UpdateData.UpdateGiamSiSo(_listKtPhong);
                if (_listXepPhong.Count > 0) DeleteData.XoaXepPhong(_listXepPhong);
                LoadGrid();
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

        private void Frm_ChonSinhVien_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
            band.Override.HeaderAppearance.FontData.SizeInPoints = 11;

            #region Caption

            band.Columns["MaSV"].Header.Caption = @"Mã SV";
            band.Columns["HoSV"].Header.Caption = FormResource.txtHosinhvien;
            band.Columns["TenSV"].Header.Caption = FormResource.txtTensinhvien;
            band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
            band.Columns["MaLop"].Header.Caption = @"Lớp";

            #endregion

            band.Columns["IdPhong"].Hidden = true;
            band.Columns["STT"].CellActivation = Activation.NoEdit;
            band.Columns["MaSV"].CellActivation = Activation.NoEdit;
            band.Columns["HoSV"].CellActivation = Activation.NoEdit;
            band.Columns["TenSV"].CellActivation = Activation.NoEdit;
            band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
            band.Columns["MaLop"].CellActivation = Activation.NoEdit;

            band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;

            band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
            band.Columns["STT"].Width = 50;
            band.Columns["HoSV"].Width = 170;
            band.Columns["TenSV"].Width = 150;
            band.Columns["NgaySinh"].Width = 150;
            band.Columns["MaLop"].Width = 150;
            band.Override.HeaderClickAction = HeaderClickAction.SortSingle;
        }
    }
}
