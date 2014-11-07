﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmInportSinhVien : FunctionControlHasGrid
    {
        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private IList<Lop> _listLop;
        private IList<Khoa> _listKhoa;

        private readonly BackgroundWorker _bgwInsert;
        public delegate void CustomHandler1(object sender);
        public event CustomHandler1 CloseDialog = null;
        public delegate void CustomHandler(object sender);
        public event CustomHandler ShowDialog = null;

        public FrmInportSinhVien()
        {
            InitializeComponent();
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(string));
            table.Columns.Add("MaSinhVien", typeof(string));
            table.Columns.Add("HoSinhVien", typeof(string));
            table.Columns.Add("TenSinhVien", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("TenKhoa", typeof(string));

            return table;
        }

        public void LoadForm()
        {
            try
            {
                _listLop = QlsvSevice.Load<Lop>();
                _listKhoa = QlsvSevice.Load<Khoa>();
                _listAdd.Clear();
                var table = GetTable();
                uG_DanhSach.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        public void Napdulieu()
        {
            try
            {
                var stt = uG_DanhSach.Rows.Count;
                var frmNapDuLieu = new FrmNapDuLieu(stt);
                frmNapDuLieu.ShowDialog();
                var b = frmNapDuLieu.ResultValue;
                if (b == null || b.Rows.Count == 0) return;
                var table = (DataTable)uG_DanhSach.DataSource;

                table.Merge(b);
                uG_DanhSach.DataSource = table;

                MessageBox.Show(@"Inport thành công " + b.Rows.Count + @" Sinh viên. Nhấn F5 để lưu lại");
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

        protected override void InsertRow()
        {
            InsertRow(uG_DanhSach, "STT", "MaSinhVien");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaSinhVien");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private new void Save()
        {
            try
            {
                var danhsach = (DataTable)uG_DanhSach.DataSource;
                foreach (DataRow row in danhsach.Rows)
                {
                    var checkmalop = "";
                    var checkmakhoa = "";
                    var tenkhoa = row["TenKhoa"].ToString();
                    var malop = row["MaLop"].ToString();
                    // Kiểm tra lớp đã tồn tại chưa
                    foreach (var lop in _listLop.Where(lop => lop.MaLop == malop))
                    {
                        var hs = new SinhVien
                        {
                            MaSinhVien = row["MaSinhVien"].ToString(),
                            HoSinhVien = row["HoSinhVien"].ToString(),
                            TenSinhVien = row["TenSinhVien"].ToString(),
                            NgaySinh = row["NgaySinh"].ToString(),
                            IdLop = lop.ID,
                        };
                        checkmalop = malop;
                        _listAdd.Add(hs);
                    }
                    if (checkmalop != "") continue;
                    //Kiểm tra khoa đã tồn tại chưa
                    foreach (
                        var khoa in
                            _listKhoa.Where(khoa => khoa.TenKhoa.Equals(tenkhoa, StringComparison.OrdinalIgnoreCase))
                        )
                    {
                        var newLop1 = SinhVienSql.ThemLop(malop, khoa.ID);
                        checkmakhoa = newLop1.MaLop;
                        var hs = new SinhVien
                        {
                            MaSinhVien = row["MaSinhVien"].ToString(),
                            HoSinhVien = row["HoSinhVien"].ToString(),
                            TenSinhVien = row["TenSinhVien"].ToString(),
                            NgaySinh = row["NgaySinh"].ToString(),
                            IdLop = newLop1.ID,
                        };
                        _listAdd.Add(hs);
                        _listLop.Add(newLop1);
                    }
                    if (checkmakhoa != "") continue;

                    // Chưa có khoa lớp thì thêm mới
                    var newkhoa = SinhVienSql.ThemKhoa(tenkhoa);
                    var newLop3 = SinhVienSql.ThemLop(malop, newkhoa.ID);
                    var hs1 = new SinhVien
                    {
                        MaSinhVien = row["MaSinhVien"].ToString(),
                        HoSinhVien = row["HoSinhVien"].ToString(),
                        TenSinhVien = row["TenSinhVien"].ToString(),
                        NgaySinh = row["NgaySinh"].ToString(),
                        IdLop = newLop3.ID
                    };

                    _listAdd.Add(hs1);
                    _listLop.Add(newLop3);
                    _listKhoa.Add(newkhoa);
                }
                SinhVienSql.ThemSinhVien(_listAdd);
                danhsach.Clear();
                LoadForm();
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
            if (uG_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            ShowDialog(null);
        }

        #region BackgroundWorker

        private void bgwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void bgwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseDialog(null);
        }

        #endregion

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Columns["STT"].Width = 50;
                band.Columns["MaSinhVien"].Width = 150;
                band.Columns["HoSinhVien"].Width = 200;
                band.Columns["TenSinhVien"].Width = 150;
                band.Columns["TenKhoa"].Width = 400;
                band.Columns["MaLop"].Width = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = FormResource.txtMasinhvien;
                band.Columns["HoSinhVien"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSinhVien"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["TenKhoa"].Header.Caption = FormResource.txtKhoaquanly;
                band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void FrmInportSinhVien_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void menuStrip_Huy_Click(object sender, EventArgs e)
        {
            LoadForm();
        }
    }
}
