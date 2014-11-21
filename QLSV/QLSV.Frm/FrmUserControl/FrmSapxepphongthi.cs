using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
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
        private IList<XepPhong> _lstAdd = new List<XepPhong>();
        private IList<PhongThi> _lstAdd1 = new List<PhongThi>();
        private bool _check = true;
        private int m_iIdKythi;
        private readonly BackgroundWorker _bgwInsert = null;

        public FrmSapxepphongthi()
        {
            InitializeComponent();

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
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("PhongThi", typeof (string));

            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                _lstAdd.Clear();
                _lstAdd1.Clear();
                var table = GetTable();
                var tbSinhVien = LoadData.Load(6);
                var listPhongthi = QlsvSevice.Load<PhongThi>();
                var t = 0;
                var s = 0;
                var tg = 0;
                var stt = 1;
                if (tbSinhVien == null || tbSinhVien.Rows.Count == 0)
                {
                    MessageBox.Show(FormResource.msgxepphong);
                    return;
                }
                var frm = new FrmCheckXepPhong();
                frm.ShowDialog();
                m_iIdKythi = frm.gb_iIdKythi;
                if (frm.rdoall.Checked)
                {
                    _check = false;
                    foreach (var pt in listPhongthi.Where(pt => pt.SoLuong < pt.SucChua))
                    {
                        var phong = new PhongThi
                        {
                            ID = pt.ID,
                            SoLuong = pt.SoLuong
                        };
                        t = t + tg;
                        tg = pt.SucChua - pt.SoLuong;
                        s = s + tg;
                        if (s < tbSinhVien.Rows.Count)
                        {
                            for (var i = t; i < s; i++)
                            {
                                table.Rows.Add(tbSinhVien.Rows[i]["ID"].ToString(),
                                    stt++,
                                    tbSinhVien.Rows[i]["MaSinhVien"].ToString(),
                                    tbSinhVien.Rows[i]["HoSinhVien"].ToString(),
                                    tbSinhVien.Rows[i]["TenSinhVien"].ToString(),
                                    tbSinhVien.Rows[i]["NgaySinh"].ToString(),
                                    tbSinhVien.Rows[i]["MaLop"].ToString(),
                                    pt.TenPhong);
                                var hs = new XepPhong
                                {
                                    IdSV = int.Parse(tbSinhVien.Rows[i]["ID"].ToString()),
                                    IdPhong = pt.ID,
                                    IdKyThi = m_iIdKythi
                                };
                                phong.SoLuong = phong.SoLuong + 1;
                                _lstAdd.Add(hs);
                            }
                            _lstAdd1.Add(phong);
                        }
                        else
                        {
                            for (var i = t; i < tbSinhVien.Rows.Count; i++)
                            {
                                table.Rows.Add(tbSinhVien.Rows[i]["ID"].ToString(),
                                    stt++,
                                    tbSinhVien.Rows[i]["MaSinhVien"].ToString(),
                                    tbSinhVien.Rows[i]["HoSinhVien"].ToString(),
                                    tbSinhVien.Rows[i]["TenSinhVien"].ToString(),
                                    tbSinhVien.Rows[i]["NgaySinh"].ToString(),
                                    tbSinhVien.Rows[i]["MaLop"].ToString(),
                                    pt.TenPhong);
                                var hs = new XepPhong
                                {
                                    IdSV = int.Parse(tbSinhVien.Rows[i]["ID"].ToString()),
                                    IdPhong = pt.ID,
                                    IdKyThi = m_iIdKythi
                                };
                                phong.SoLuong = phong.SoLuong + 1;
                                _lstAdd.Add(hs);
                            }
                            _lstAdd1.Add(phong);
                            break;
                        }

                    }
                    if (s < tbSinhVien.Rows.Count)
                        MessageBox.Show(@"Không đủ phòng thi để xếp sinh viên");
                    else
                        dgv_DanhSach.DataSource = table;

                }
                else if (frm.rdoone.Checked)
                {
                    foreach (DataRow sv in tbSinhVien.Rows)
                    {
                        table.Rows.Add(sv["ID"].ToString(),
                            stt++,
                            sv["MaSinhVien"].ToString(),
                            sv["HoSinhVien"].ToString(),
                            sv["TenSinhVien"].ToString(),
                            sv["NgaySinh"].ToString(),
                            sv["MaLop"].ToString());
                    }
                    dgv_DanhSach.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
            finally
            {
                pnl_form.Visible = true;
            }
        }

        protected override void LoadFormDetail()
        {
            Invoke((Action)(LoadGrid));
            lock (LockTotal)
            {
                OnCloseDialog();
            }
        }

        private void Sua()
        {
            try
            {
                if (!_check || dgv_DanhSach.ActiveRow.Cells["ID"].Text == "0")
                {
                    MessageBox.Show(@"Sinh viên đã được xếp phòng");
                    return;
                }
                var frm = new FrmXepPhong
                {
                    txtmasinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["MaSinhVien"].Text},
                    txthotendem = {Text = dgv_DanhSach.ActiveRow.Cells["HoSinhVien"].Text},
                    txttensinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["TenSinhVien"].Text},
                    txtNgaySinh = {Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text},
                    cbolop = {Text = dgv_DanhSach.ActiveRow.Cells["MaLop"].Text},
                    gb_iIdsinhvien = int.Parse(dgv_DanhSach.ActiveRow.Cells["ID"].Text),
                    gb_iIdKythi = m_iIdKythi
                };
                frm.ShowDialog();
                if(frm.gb_iIdsinhvien != 0) return;
                dgv_DanhSach.ActiveRow.Cells["ID"].Value = 0;
                dgv_DanhSach.ActiveRow.Cells["PhongThi"].Value = frm.cboPhongthi.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Lưu dữ liệu trên UltraGrid
        /// </summary>
        protected override void SaveDetail()
        {
            try
            {
                InsertData.XepPhong(_lstAdd);
                UpdateData.UpdatePhongThi(_lstAdd1);
                _lstAdd.Clear();
                _lstAdd1.Clear();
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
            if (dgv_DanhSach.Rows.Count <= 0) return;
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
        }

        #region BackgroundWorker
        

        private void BgwInsertDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SaveDetail();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void BgwLoadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        #endregion

        private void Sapxepphongthi_Load(object sender, EventArgs e)
        {
            var thread = new Thread(LoadFormDetail) { IsBackground = true };
            thread.Start();
            OnShowDialog("Loading...");
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
                band.Columns["TenSinhVien"].Width = 150;
                band.Columns["NgaySinh"].Width = 150;
                band.Columns["MaLop"].Width = 150;
                band.Columns["PhongThi"].Width = 150;
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
                Sua();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        private void menuStrip_Themdong_Click(object sender, EventArgs e)
        {
            Sua();
        }
    }
}