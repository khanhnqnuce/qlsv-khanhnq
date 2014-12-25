﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_107_ChonSinhVienThi : FunctionControlHasGrid
    {
        #region Create

        private readonly IList<SinhVien> _listAdd = new List<SinhVien>();
        private readonly IList<SinhVien> _listUpdate = new List<SinhVien>();
        private int _idkythi;
        private UltraGridRow _newRow;

        #endregion

        public Frm_107_ChonSinhVienThi(int id)
        {
            InitializeComponent();
            _idkythi = id;
        }

        #region Exit

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("MaSV", typeof (string));
            table.Columns.Add("HoSV", typeof (string));
            table.Columns.Add("TenSV", typeof (string));
            table.Columns.Add("NgaySinh", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("PhongThi", typeof(string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                dgv_DanhSach.DataSource = GetTable();
                pnl_from.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void LoadFormDetail()
        {
            try
            {
                Invoke((Action)(LoadGrid));
                Invoke((Action)(()=>_listAdd.Clear()));
                Invoke((Action)(()=>_listUpdate.Clear()));
                Invoke((Action)(()=>IdDelete.Clear()));
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void InsertRow()
        {
            //InsertRow(dgv_DanhSach, "STT", "MaSV");
        }

        protected override void DeleteRow()
        {

            DeleteRowGrid(dgv_DanhSach, "MaSV", "MaSV");
        }

        protected override void SaveDetail()
        {
            try
            {
                DeleteData.XoaSV(IdDelete);
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
                //DeleteData.Xoa("SINHVIEN");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

       private void Sua()
        {
            try
            {
                var txtphongthi = dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text;
                if (!string.IsNullOrEmpty(txtphongthi))
                {
                    MessageBox.Show(@"Sinh viên đã được xếp phòng");
                    return;
                }

                var frm = new FrmXepPhong
                {
                    txtmasinhvien = { Text = dgv_DanhSach.ActiveRow.Cells["MaSV"].Text },
                    txthotendem = { Text = dgv_DanhSach.ActiveRow.Cells["HoSV"].Text },
                    txttensinhvien = { Text = dgv_DanhSach.ActiveRow.Cells["TenSV"].Text },
                    txtNgaySinh = { Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text },
                    cbolop = { Text = dgv_DanhSach.ActiveRow.Cells["MaLop"].Text },
                    IdKythi = _idkythi
                };
                frm.ShowDialog();
                dgv_DanhSach.ActiveRow.Cells["PhongThi"].Value = frm.cboPhongthi.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Huy()
        {
            try
            {
                var thread = new Thread(LoadFormDetail) {IsBackground = true};
                thread.Start();
                OnShowDialog("Loading...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
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
                        dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa((int)indexkhoa,_idkythi);
                }
                else
                {
                    if (IsNumber(indexlop.ToString()))
                        dgv_DanhSach.DataSource = SearchData.Timkiemtheolop((int)indexlop, _idkythi);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
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
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa(txtkhoa.Text,_idkythi);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private static bool IsNumber(string pText)
        {
            var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        #endregion

        #region Event uG

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 11;

                #region Caption

                band.Columns["MaSV"].Header.Caption = @"Mã SV";
                band.Columns["HoSV"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSV"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["PhongThi"].Header.Caption = @"Phòng Thi";
                band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;

                #endregion
                
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
                band.Columns["PhongThi"].Width = 150;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;
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

        private void menuStrip_Sua_Click(object sender, EventArgs e)
        {
           Sua();
        }

        #endregion

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                cbokhoa.DataSource = LoadData.Load(3);
                Huy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.S):
                    
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            var obj = cbokhoa.SelectedValue;
            if(obj == null) return;
            //cbolop.ValueMember = "ID";
            //cbolop.DisplayMember = "MaLop";
            cbolop.DataSource = SearchData.Timkiem(int.Parse(obj.ToString()));
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            Timkiemtheolop();
        }

        private void btnTimtheokhoa_Click(object sender, EventArgs e)
        {
            Timkiemtheokhoa();
        }

        private void txtkhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private void txtkhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        private void btnxepphong_Click(object sender, EventArgs e)
        {
            var count = dgv_DanhSach.Rows.Count;
            if (count <= 0)
            {
                MessageBox.Show(@"Chưa có sinh viên để xếp phòng!");
                return;
            }
            var frm = new FrmChonPhongThi(_idkythi,count,(DataTable) dgv_DanhSach.DataSource);
            frm.ShowDialog();

            dgv_DanhSach.DataSource = frm.TbTable;
        }
    }
}
