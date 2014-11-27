using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
    public partial class FrmSvDaXepPhong : FunctionControlHasGrid
    {
        private IList<PhongThi> _lisPhong = new List<PhongThi>();
        private IList<XepPhong> m_IdDelete = new List<XepPhong>();

        public FrmSvDaXepPhong()
        {
            InitializeComponent();
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof (int));
            table.Columns.Add("STT", typeof (string));
            table.Columns.Add("MaSinhVien", typeof (string));
            table.Columns.Add("HoSinhVien", typeof (string));
            table.Columns.Add("TenSinhVien", typeof (string));
            table.Columns.Add("NgaySinh", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            table.Columns.Add("IdPhong", typeof (int));
            table.Columns.Add("PhongThi", typeof (string));
            table.Columns.Add("MaKhoa", typeof (string));
            table.Columns.Add("TenKhoa", typeof (string));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                var table = LoadData.Load(7);
                dgv_DanhSach.DataSource = table;
                pnl_form.Visible = true;
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
                Invoke((Action) LoadGrid);
                lock (LockTotal)
                {
                    OnCloseDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("XepPhong");
                UpdateData.ResetPhongThi();
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Contains(FormResource.msgLostConnect) ? FormResource.txtLoiDB : ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void DeleteRow()
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
                            var id = row.Cells["ID"].Value.ToString();
                            if (!string.IsNullOrEmpty(id))
                            {
                                var hs = new XepPhong
                                {
                                    IdSV = int.Parse(id),
                                    IdKyThi = int.Parse(row.Cells["IdKyThi"].Text),
                                    IdPhong = int.Parse(row.Cells["IdPhong"].Text),
                                };
                                m_IdDelete.Add(hs);
                            }
                            foreach (var p in _lisPhong.Where(p => p.TenPhong == row.Cells["PhongThi"].Text))
                            {
                                p.SoLuong = p.SoLuong + 1;
                            }
                            var phong = new PhongThi
                            {
                                TenPhong = row.Cells["PhongThi"].Text,
                                SoLuong = 1
                            };
                            _lisPhong.Add(phong);
                        }
                        dgv_DanhSach.DeleteSelectedRows(false);
                    }
                }
                else if (dgv_DanhSach.ActiveRow != null)
                {
                    if (DialogResult.Yes ==
                        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        var idStr = dgv_DanhSach.ActiveRow.Cells["ID"].Value.ToString();
                        if (!string.IsNullOrEmpty(idStr))
                        {
                            var hs = new XepPhong
                            {
                                IdSV = int.Parse(dgv_DanhSach.ActiveRow.Cells["ID"].Text),
                                IdKyThi = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdKyThi"].Text),
                                IdPhong = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdPhong"].Text),
                            };
                            m_IdDelete.Add(hs);
                        }
                        foreach (
                            var p in _lisPhong.Where(p => p.TenPhong == dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text))
                        {
                            p.SoLuong = p.SoLuong + 1;
                        }
                        var phong = new PhongThi
                        {
                            TenPhong = dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text,
                            SoLuong = 1
                        };
                        _lisPhong.Add(phong);
                        dgv_DanhSach.ActiveRow.Delete(false);
                    }
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

        protected override void SaveDetail()
        {
            try
            {
                DeleteData.XoaXepPhong(m_IdDelete);
                UpdateData.UpdateGiamPhongThi(_lisPhong);
                _lisPhong.Clear();
                m_IdDelete.Clear();
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

        private void Sua()
        {
            try
            {
                var frm = new FrmXepPhong
                {
                    txtmasinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["MaSinhVien"].Text},
                    txthotendem = {Text = dgv_DanhSach.ActiveRow.Cells["HoSinhVien"].Text},
                    txttensinhvien = {Text = dgv_DanhSach.ActiveRow.Cells["TenSinhVien"].Text},
                    txtNgaySinh = {Text = dgv_DanhSach.ActiveRow.Cells["NgaySinh"].Text},
                    cbolop = {Text = dgv_DanhSach.ActiveRow.Cells["MaLop"].Text},
                    cboPhongthi = {Text = dgv_DanhSach.ActiveRow.Cells["PhongThi"].Text},
                    gb_iIdsinhvien = int.Parse(dgv_DanhSach.ActiveRow.Cells["ID"].Text),
                    gb_iIdKythi = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdKyThi"].Text),
                    gb_iIdPhong = int.Parse(dgv_DanhSach.ActiveRow.Cells["IdPhong"].Text),
                    gb_bUpdate = true
                };
                frm.ShowDialog();
                if (frm.gb_bUpdate)
                {
                    dgv_DanhSach.ActiveRow.Cells["IdPhong"].Value = frm.gb_iIdPhong;
                    dgv_DanhSach.ActiveRow.Cells["PhongThi"].Value = frm.cboPhongthi.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void InDanhSach()
        {
            var frm = new FrmChonindssv();
            frm.ShowDialog();
            if (frm.rdoPhongthi.Checked)
                RptPhongthi();
            else if (frm.rdokhoa.Checked)
                RptKhoa();
            else if (frm.rdoLop.Checked)
                RptLop();
        }

        #region Xuất báo cáo

        private void RptPhongthi()
        {
            var listphongthi = QlsvSevice.Load<PhongThi>();

            var tb = (DataTable) dgv_DanhSach.DataSource;
            foreach (var phong in listphongthi)
            {
                var stt = 1;
                var lopnew = phong;
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["PhongThi"].ToString() == lopnew.TenPhong))
                {
                    row["STT"] = stt++;
                }
            }

            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachduthi.FilePath = Application.StartupPath + @"\Reports\danhsachduthi.rst";
            using (var previewForm = new PreviewForm(rptdanhsachduthi))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachduthi.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void RptKhoa()
        {
            var listkhoa = QlsvSevice.Load<Khoa>();
            var tb = ((DataTable) dgv_DanhSach.DataSource);
            foreach (var khoa in listkhoa)
            {
                var stt = 1;
                var khoa1 = khoa;
                foreach (
                    var row in tb.Rows.Cast<DataRow>().Where(row => row["MaKhoa"].ToString() == khoa1.ID.ToString()))
                {
                    row["STT"] = stt++;
                }
            }

            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachkhoa.FilePath = Application.StartupPath + @"\Reports\danhsachduthikhoa.rst";
            using (var previewForm = new PreviewForm(rptdanhsachkhoa))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachkhoa.Prepare();
                previewForm.ShowDialog();
            }
        }

        private void RptLop()
        {
            var listlop = QlsvSevice.Load<Lop>();
            var tb = ((DataTable) dgv_DanhSach.DataSource);
            foreach (var lop in listlop)
            {
                var stt = 1;
                var lopnew = lop;
                foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row["MaLop"].ToString() == lopnew.MaLop))
                {
                    row["STT"] = stt++;
                }
            }
            reportManager1.DataSources.Clear();
            reportManager1.DataSources.Add("danhsach", tb);
            rptdanhsachlop.FilePath = Application.StartupPath + @"\Reports\danhsachduthilop.rst";
            using (var previewForm = new PreviewForm(rptdanhsachlop))
            {
                previewForm.WindowState = FormWindowState.Maximized;
                rptdanhsachlop.Prepare();
                previewForm.ShowDialog();
            }
        }

        #endregion

        private void FrmSinhVienPhongThi_Load(object sender, EventArgs e)
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

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];

                #region Caption

                band.Columns["MaSinhVien"].Header.Caption = @"Mã Sinh Viên";
                band.Columns["HoSinhVien"].Header.Caption = @"Họ tên đệm";
                band.Columns["TenSinhVien"].Header.Caption = @"Tên Sinh Viên";
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["MaLop"].Header.Caption = @"Lớp";
                band.Columns["PhongThi"].Header.Caption = @"Phòng Thi";

                #endregion

                band.Columns["ID"].Hidden = true;
                band.Columns["MaKhoa"].Hidden = true;
                band.Columns["TenKhoa"].Hidden = true;
                band.Columns["IdPhong"].Hidden = true;
                band.Columns["IdKyThi"].Hidden = true;

                band.Columns["STT"].Width = 50;
                band.Columns["HoSinhVien"].Width = 170;
                band.Columns["TenSinhVien"].Width = 150;
                band.Columns["NgaySinh"].Width = 150;
                band.Columns["MaLop"].Width = 150;
                band.Columns["PhongThi"].Width = 150;

                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["MaSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["HoSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["TenSinhVien"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;
                band.Columns["PhongThi"].CellActivation = Activation.NoEdit;

                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["TenSinhVien"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["PhongThi"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["NgaySinh"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 12;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        private void dgv_DanhSach_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            Sua();
        }

        private void menuStrip_Themdong_Click(object sender, EventArgs e)
        {
            Sua();
        }
    }
}
