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

namespace QLSV.Frm.Frm
{
    public partial class FrmDanhmuckhoa : FunctionControlHasGrid
    {
        private readonly List<Khoa> _listAdd = new List<Khoa>();
        private readonly List<Khoa> _listUpdate = new List<Khoa>();

        public FrmDanhmuckhoa()
        {
            InitializeComponent();
            LoadForm();
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaKhoa", typeof(string));
            table.Columns.Add("TenKhoa", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
                var table = GetTable();
                var danhsach = QuanlysinhvienSevice.Load<Khoa>();
                var stt = 1;
                foreach (var hs in danhsach)
                {
                    table.Rows.Add(hs.ID, stt, hs.MaKhoa, hs.TenKhoa);
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

        private void LoadForm()
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
            InsertRow(uG_DanhSach, "STT", "MaKhoa");
        }

        protected override void DeleteRow()
        {
            try
            {
                DeleteRowGrid(uG_DanhSach, "ID", "MaKhoa");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void Save()
        {
            try
            {
                btnGhi.Focus();
                foreach (var row in uG_DanhSach.Rows)
                {
                    var id = row.Cells["ID"].Value.ToString();
                    var hs = new Khoa
                    {
                        MaKhoa = row.Cells["MaKhoa"].Text,
                        TenKhoa = row.Cells["TenKhoa"].Text
                    };
                    if (string.IsNullOrEmpty(id))
                    {
                        _listAdd.Add(hs);
                    }
                }
                QuanlysinhvienSevice.ThemAll(_listAdd);
                QuanlysinhvienSevice.Sua(_listUpdate);
                QuanlysinhvienSevice.Xoa(IdDelete, "Khoa");
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

        protected override void Xoa()
        {
            try
            {
                if (DialogResult.Yes ==
                    MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                {
                    QuanlysinhvienSevice.Xoa("Khoa");
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

        #endregion

        #region Button

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        #region Event_uG

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                btnGhi.Focus();
                var id = uG_DanhSach.ActiveRow.Cells["ID"].Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                    {
                        item.MaKhoa = uG_DanhSach.ActiveRow.Cells["MaKhoa"].Text;
                        item.TenKhoa = uG_DanhSach.ActiveRow.Cells["TenKhoa"].Text;
                        return;
                    }
                    var hs = new Khoa
                    {
                        ID = int.Parse(id),
                        MaKhoa = uG_DanhSach.ActiveRow.Cells["MaKhoa"].Text,
                        TenKhoa = uG_DanhSach.ActiveRow.Cells["TenKhoa"].Text,
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
                band.Override.CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MaxWidth = 100;
                band.Columns["MaKhoa"].MaxWidth = 300;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["MaKhoa"].Header.Caption = FormResource.txtMakhoa;
                band.Columns["TenKhoa"].Header.Caption = FormResource.txtTenkhoa;

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

        private void menuStrip_dong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.F3):
                    Xoa();
                    break;
                case (Keys.F5):
                    Save();
                    break;
                case (Keys.F11):
                    DeleteRow();
                    break;
                case (Keys.F12):
                    LoadForm();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
                case (Keys.Insert):
                    InsertRow();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
