using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonSv : Form
    {
        private readonly IList<XepPhong> _listXepPhong = new List<XepPhong>();
        private readonly int _idkythi;
        private FrmLoadding _loading = new FrmLoadding();
        private readonly BackgroundWorker _bgwInsert;

        public FrmChonSv(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("Chon", typeof(bool));
            table.Columns.Add("MaSV", typeof(string));
            table.Columns.Add("HoSV", typeof(string));
            table.Columns.Add("TenSV", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            return table;
        }

        /// <summary>
        /// Tìm theo khoa , lớp
        /// </summary>
        private void Timkiemtheolop()
        {
            try
            {
                var indexkhoa = cbokhoa.SelectedValue;
                var indexlop = cbolop.SelectedValue;
                if (indexlop == null)
                {
                    if (indexkhoa == null) return;
                    if (IsNumber(indexkhoa.ToString()))
                    {
                        dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa((int) indexkhoa, _idkythi);
                    }
                }
                else
                {
                    if (IsNumber(indexlop.ToString()))
                    {
                        dgv_DanhSach.DataSource = SearchData.Timkiemtheolop((int) indexlop, _idkythi);
                    }
                }


            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Tìm theo niên khóa
        /// </summary>
        private void Timkiemtheokhoa()
        {
            try
            {
                if (string.IsNullOrEmpty(txtkhoa.Text)) return;
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa(txtkhoa.Text, _idkythi);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private static bool IsNumber(string pText)
        {
            var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void ChonSinhVien()
        {
            try
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    if (!bool.Parse(row.Cells["Chon"].Text)) continue;
                    var masv = int.Parse(row.Cells["MaSV"].Text);
                    var hspp = new XepPhong
                    {
                        IdKyThi = _idkythi,
                        IdSV = masv
                    };
                    _listXepPhong.Add(hspp);
                }
                InsertData.Chonsinhvien(_listXepPhong);
                Invoke((Action)(()=>MessageBox.Show(@"Lưu lại thành công", @"Thông báo")));
                Invoke((Action)(Close));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Luu()
        {
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
        }

        private void FrmChonSv_Load(object sender, EventArgs e)
        {
            try
            {
                cbokhoa.DataSource = LoadData.Load(3);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = cbokhoa.SelectedValue;
                if (obj == null) return;
                cbolop.DataSource = SearchData.Timkiem(int.Parse(obj.ToString()));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnTimtheokhoa_Click(object sender, EventArgs e)
        {
            Timkiemtheokhoa();
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                #region Caption
                band.Groups.Clear();
                var columns = band.Columns;
                band.ColHeadersVisible = false;
                var group0 = band.Groups.Add("Mã SV");
                var group1 = band.Groups.Add("Họ và tên");
                var group2 = band.Groups.Add("Ngày sinh");
                var group3 = band.Groups.Add("Lớp");
                var group5 = band.Groups.Add("Chọn");
                columns["MaSV"].Group = group0;
                columns["HoSV"].Group = group1;
                columns["TenSV"].Group = group1;
                columns["NgaySinh"].Group = group2;
                columns["MaLop"].Group = group3;
                columns["Chon"].Group = group5;
                #endregion

                band.Columns["Chon"].Style = ColumnStyle.CheckBox;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["HoSV"].CellActivation = Activation.NoEdit;
                band.Columns["TenSV"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;

                band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["HoSV"].MinWidth = 160;
                band.Columns["MaSV"].MinWidth = 140;
                band.Columns["TenSV"].MinWidth = 140;
                band.Columns["NgaySinh"].MinWidth = 140;
                band.Columns["MaLop"].MinWidth = 140;
                band.Columns["HoSV"].MaxWidth = 170;
                band.Columns["MaSV"].MaxWidth = 150;
                band.Columns["TenSV"].MaxWidth = 150;
                band.Columns["NgaySinh"].MaxWidth = 150;
                band.Columns["MaLop"].MaxWidth = 150;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void ckbChon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbChon.Checked)
                {
                    foreach (var row in dgv_DanhSach.Rows)
                    {
                        row.Cells["Chon"].Value = "true";
                        row.Appearance.BackColor = Color.LightCyan;
                    }
                }
                else
                {
                    foreach (var row in dgv_DanhSach.Rows)
                    {
                        row.Cells["Chon"].Value = "false";
                        row.Appearance.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            Luu();
        }

        private void txtkhoa_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Timkiemtheokhoa();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbolop_SelectedValueChanged(object sender, EventArgs e)
        {
            Timkiemtheolop();
        }

        private void txtkhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtkhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.F5):
                    Luu();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region BackgroundWorker

        private void bgwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            ChonSinhVien();
        }

        private void bgwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        private void OnShowDialog(string msg)
        {
            try
            {
                _loading = new FrmLoadding();
                _loading.Update(msg);
                _loading.ShowDialog();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void OnCloseDialog()
        {
            try
            {
                if (_loading != null)
                {
                    _loading.Invoke((Action) (() =>
                    {
                        _loading.Close();
                        _loading = null;
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        private void dgv_DanhSach_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key != "Chon") return;
                var b = bool.Parse(e.Cell.Row.Cells["Chon"].Text);
                e.Cell.Row.Appearance.BackColor = b ? Color.LightCyan : Color.White;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
