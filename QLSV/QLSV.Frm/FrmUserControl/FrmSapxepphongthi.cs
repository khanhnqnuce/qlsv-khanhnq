using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;
using QLSV.Frm.Frm;
using Color = System.Drawing.Color;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmSapxepphongthi : FunctionControlHasGrid
    {
        private IList<XepPhong> _listAdd = new List<XepPhong>();
        private IList<PhongThi> _listAdd1 = new List<PhongThi>();
        private bool check = true;
        
        private readonly BackgroundWorker _bgwInsert = null;
        private readonly BackgroundWorker _bgwLoad = null;
        public delegate void CustomHandler1(object sender);
        public event CustomHandler1 CloseDialog = null;
        public delegate void CustomHandler(object sender);
        public event CustomHandler ShowDialog = null;

        public FrmSapxepphongthi()
        {
            InitializeComponent();
            _bgwLoad = new BackgroundWorker();
            _bgwLoad.DoWork += BgwLoadDoWork;
            _bgwLoad.RunWorkerCompleted += BgwLoadRunWorkerCompleted;

            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += BgwInsertDoWork;
            _bgwInsert.RunWorkerCompleted += BgwLoadRunWorkerCompleted;
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSinhVien", typeof (string));
            table.Columns.Add("HoSinhVien", typeof (string));
            table.Columns.Add("TenSinhVien", typeof (string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("PhongThi", typeof (string));

            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listAdd1.Clear();
                var table = GetTable();
                var listSinhvien = QlsvSevice.LoadSvChuaXepPhong();
                var listPhongthi = QlsvSevice.Load<PhongThi>();
                var t = 0;
                var s = 0;
                var tg = 0;
                var stt = 1;
                if (listSinhvien == null || listSinhvien.Count == 0)
                {
                    MessageBox.Show(@"Tất cả sinh viên đã được xếp phòng");
                    return;
                }
                var frm = new FrmCheckXepPhong();
                frm.ShowDialog();
                if (frm.rdoall.Checked)
                {
                    check = false;
                    foreach (var pt in listPhongthi.Where(pt=>pt.SoLuong<pt.SucChua))
                    {
                        var phong = new PhongThi
                        {
                            ID = pt.ID,
                            SoLuong = pt.SoLuong
                        };
                        t = t + tg;
                        tg = pt.SucChua - pt.SoLuong;
                        s = s + tg;
                        if (s<listSinhvien.Count)
                        {
                            for (var i = t; i < s; i++)
                            {
                                table.Rows.Add(listSinhvien[i].ID,
                                    stt++,
                                    listSinhvien[i].MaSinhVien,
                                    listSinhvien[i].HoSinhVien,
                                    listSinhvien[i].TenSinhVien,
                                    listSinhvien[i].NgaySinh,
                                    listSinhvien[i].Lop.MaLop,
                                    pt.TenPhong);
                                var hs = new XepPhong
                                {
                                    IdSV = listSinhvien[i].ID,
                                    IdPhong = pt.ID
                                };
                                phong.SoLuong = phong.SoLuong + 1;
                                _listAdd.Add(hs);
                            }
                            _listAdd1.Add(phong);
                        }
                        else
                        {
                            for (var i = t; i < listSinhvien.Count; i++)
                            {
                                table.Rows.Add(listSinhvien[i].ID,
                                    stt++,
                                    listSinhvien[i].MaSinhVien,
                                    listSinhvien[i].HoSinhVien,
                                    listSinhvien[i].TenSinhVien,
                                    listSinhvien[i].NgaySinh,
                                    listSinhvien[i].Lop.MaLop,
                                    pt.TenPhong);
                                var hs = new XepPhong
                                {
                                    IdSV = listSinhvien[i].ID,
                                    IdPhong = pt.ID
                                };
                                phong.SoLuong = phong.SoLuong + 1;
                                _listAdd.Add(hs);
                            }
                            _listAdd1.Add(phong);
                            break;
                        }

                    }
                    if (s < listSinhvien.Count)
                        MessageBox.Show(@"Không đủ phòng thi để xếp sinh viên");
                    else
                        uG_DanhSach.DataSource = table;
                    
                }else if (frm.rdoone.Checked)
                {
                    foreach (var sv in listSinhvien)
                    {
                        table.Rows.Add(sv.ID,
                            stt++,
                            sv.MaSinhVien,
                            sv.HoSinhVien,
                            sv.TenSinhVien,
                            sv.NgaySinh,
                            sv.Lop.MaLop);
                    }
                    uG_DanhSach.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        private void LoadForm()
        {
            _bgwLoad.RunWorkerAsync();
            ShowDialog(null);
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        protected override void SaveDetail()
        {
            try
            {
                SinhVienSql.XepPhong(_listAdd);
                SinhVienSql.UpdatePhongThi(_listAdd1);
                _listAdd.Clear();
                _listAdd1.Clear();
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

        private void BgwLoadDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke((Action) (LoadGrid));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void BgwInsertDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke((Action)(SaveDetail));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void BgwLoadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseDialog(null);
        }

        #endregion

        private void Sapxepphongthi_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = FormResource.txtMasinhvien;
                band.Columns["HoSinhVien"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSinhVien"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["MaLop"].Header.Caption = FormResource.txtMalop;
                band.Columns["PhongThi"].Header.Caption = @"Phòng thi";

                #endregion

                #region NoExit

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["HoSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["TenSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["PhongThi"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;

                #endregion
                
                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSinhVien"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["PhongThi"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].Width = 50;
                band.Columns["HoSinhVien"].Width = 170;
                band.Override.HeaderClickAction = HeaderClickAction.SortSingle;
                band.Columns["ID"].Hidden = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                if (!check) return;
                var frm = new FrmXepPhong();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }
    }
}