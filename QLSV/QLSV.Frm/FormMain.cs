﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using Infragistics.Win;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;
using Infragistics.Win.UltraWinExplorerBar;
using QLSV.Frm.Frm;
using Infragistics.Win.UltraWinTabControl;
using QLSV.Frm.FrmUserControl;

namespace QLSV.Frm
{
    public partial class FormMain : Form
    {
        private static FrmDangNhap _frmDangNhap;
        private static FrmQuanLyNguoiDung _frmQuanLyNguoiDung;
        private static FrmDanhmuckhoa _frmDanhmuckhoa;
        private static FrmDanhmuclop _frmDanhmuclop;
        private static FrmInportSinhVien _frmInportSinhVien;
        private static FrmSinhVien _frmQuanlySinhVien;
        private static FrmQuanLyKyThi _frmQuanLyKyThi;
        private static FrmDanhsachphongthi _frmDanhsachphongthi;
        private static FrmSapxepphongthi _frmSapxepphongthi;
        private bool _dangnhap = false;

        public FormMain()
        {
            InitializeComponent();
            _frmDangNhap = new FrmDangNhap();
            _frmDangNhap.CheckDangNhap += CheckDangNhap;
            _frmQuanLyNguoiDung = new FrmQuanLyNguoiDung();
            _frmDanhmuckhoa = new FrmDanhmuckhoa();
            _frmDanhmuclop = new FrmDanhmuclop();

            _frmInportSinhVien = new FrmInportSinhVien();
            _frmInportSinhVien.ShowDialog += ShowLoading;
            _frmInportSinhVien.CloseDialog += KillLoading;

            _frmQuanlySinhVien = new FrmSinhVien();
            _frmQuanLyKyThi = new FrmQuanLyKyThi();
            _frmDanhsachphongthi = new FrmDanhsachphongthi();

            _frmSapxepphongthi = new FrmSapxepphongthi();
            _frmSapxepphongthi.ShowDialog += ShowLoading;
            _frmSapxepphongthi.CloseDialog += KillLoading;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDefaul(null);
            _frmDangNhap.ShowDialog();
        }

        private static void ShowControl(Control frm, Control panel)
        {
            try
            {
                panel.Controls.Clear();
                frm.Dock = DockStyle.Fill;
                panel.Controls.Add(frm);
                panel.Controls[frm.Name].Focus();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void ChonChucNang(string strChucNang)
        {
            try
            {
                switch (strChucNang)
                {
                    case "login":
                        _frmDangNhap.ShowDialog();
                        break;
                    case "logout":
                        _dangnhap = false;
                        LoadDefaul(null);
                        _frmDangNhap.txtMatKhau.Clear();
                        lbusername.Text = "";
                        _frmDangNhap.ShowDialog();
                        break;
                    case "doimatkhau":
                        break;
                    case "thoat":
                        if (DialogResult.Yes ==
                            MessageBox.Show(FormResource.msgThoat, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question))
                        {
                            Application.Exit();
                        }
                        break;
                    case "101":
                        if (Tabquanlynguoidung.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = Tabquanlynguoidung.Tab;
                            return;
                        }
                        Tabquanlynguoidung.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlynguoidung.Tab;
                        ShowControl(_frmQuanLyNguoiDung, pn_quanlynguoidung);
                        break;
                    case "102":
                        if (Tabdanhmuckhoa.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = Tabdanhmuckhoa.Tab;
                            return;
                        }
                        Tabdanhmuckhoa.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhmuckhoa.Tab;
                        ShowControl(_frmDanhmuckhoa, pn_danhmuckhoa);
                        break;
                    case "103":
                        if (Tabdanhmuclop.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = Tabdanhmuclop.Tab;
                            return;
                        }
                        Tabdanhmuclop.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhmuclop.Tab;
                        ShowControl(_frmDanhmuclop, pn_danhmuclop);
                        break;
                    case "104":
                        if (TabInportsinhvien.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = TabInportsinhvien.Tab;
                            return;
                        }
                        TabInportsinhvien.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabInportsinhvien.Tab;
                        ShowControl(_frmInportSinhVien, pn_inportsinhvien);
                        break;
                    case "105":
                        if (Tabquanlysinhvien.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = Tabquanlysinhvien.Tab;
                            return;
                        }
                        Tabquanlysinhvien.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlysinhvien.Tab;
                        ShowControl(_frmQuanlySinhVien, pn_quanlysinhvien);
                        break;
                    case "106":
                        if (Tabquanlykythi.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = Tabquanlykythi.Tab;
                            return;
                        }
                        Tabquanlykythi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlykythi.Tab;
                        ShowControl(_frmQuanLyKyThi, pn_quanlykythi);
                        break;
                    case "107":
                        if (Tabdanhsachphongthi.Tab.Visible)
                        {
                            TabPageControl.SelectedTab = Tabdanhsachphongthi.Tab;
                            return;
                        }
                        Tabdanhsachphongthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhsachphongthi.Tab;
                        ShowControl(_frmDanhsachphongthi, pn_danhsachphong);
                        break;
                    case "108":
                        if (TabSapxepphongthi.Tab.Visible)
                        {
                            _frmSapxepphongthi.LoadForm();
                            TabPageControl.SelectedTab = TabSapxepphongthi.Tab;
                            return;
                        }
                        TabSapxepphongthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabSapxepphongthi.Tab;
                        ShowControl(_frmSapxepphongthi, pn_sapxepphongthi);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void LoadDefaul(string quyen)
        {
            switch (quyen)
            {
                case "quantri":
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.True;
                    break;
                case "nguoidung":
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.True;
                    break;
                default:
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.False;
                    Tabquanlynguoidung.Tab.Visible = false;
                    Tabdanhmuckhoa.Tab.Visible = false;
                    Tabdanhmuclop.Tab.Visible = false;
                    TabInportsinhvien.Tab.Visible = false;
                    Tabquanlysinhvien.Tab.Visible = false;
                    Tabquanlykythi.Tab.Visible = false;
                    Tabdanhsachphongthi.Tab.Visible = false;
                    TabSapxepphongthi.Tab.Visible = false;
                    pn_Button.Visible = false;
                    break;
            }

        }

        private void CheckDangNhap(object sender, bool checkState, Taikhoan hs)
        {
            if (hs == null)
            {
                LoadDefaul(null);
                return;
            }
            LoadDefaul(hs.Quyen);
            _dangnhap = true;
            lbusername.Text = hs.HoTen;
            _frmDangNhap.Close();
        }

        private void MenuBar_ItemClick(object sender, ItemEventArgs e)
        {
            try
            {
                ChonChucNang(e.Item.Key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void TabPageControl_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            try
            {
                if (!_dangnhap) return;
                var b = true;
                pn_Button.Visible = true;
                if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
                {
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                    b = false;
                }
                else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
                {
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                    b = false;
                }
                if(b == false) return;
                pn_Button.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        #region Exit

        private void Dong_Esc()
        {
            if (Tabquanlynguoidung.Tab.Active)
            {
                Tabquanlynguoidung.Tab.Visible = false;
            }
            else if (Tabdanhmuckhoa.Tab.Active)
            {
                Tabdanhmuckhoa.Tab.Visible = false;
            }
            else if (Tabdanhmuclop.Tab.Active)
            {
                Tabdanhmuclop.Tab.Visible = false;
            }
            else if (TabInportsinhvien.Tab.Active)
            {
                TabInportsinhvien.Tab.Visible = false;
            }
            else if (Tabquanlysinhvien.Tab.Active)
            {
                Tabquanlysinhvien.Tab.Visible = false;
            }
            else if (Tabdanhsachphongthi.Tab.Active)
            {
                Tabdanhsachphongthi.Tab.Visible = false;
            }
            else if (Tabquanlykythi.Tab.Active)
            {
                Tabquanlykythi.Tab.Visible = false;
            }
            else if (TabSapxepphongthi.Tab.Active)
            {
                TabSapxepphongthi.Tab.Visible = false;
            }
        }

        private void Huy_F12()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.LoadForm();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.LoadForm();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.LoadForm();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.LoadForm();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Huy();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.LoadForm();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.LoadForm();
            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {
            }
        }

        private void Luu_F5()
        {
            btnLuu.Focus();
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.Save();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.Save();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.Save();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.Ghi();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Save();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Save();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.Save();
            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {

            }
        }

        private void Xoa_F3()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.Xoa();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.Xoa();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.Xoa();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Xoa();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Xoa();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.Xoa();
            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {

            }
        }

        private void Xoadong_F11()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.uG_DeleteRow();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.uG_DeleteRow();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.uG_DeleteRow();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.uG_DeleteRow();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.uG_DeleteRow();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.uG_DeleteRow();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.uG_DeleteRow();
            }
        }

        private void Themmoi_Insert()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.uG_InsertRow();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.uG_InsertRow();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.uG_InsertRow();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.uG_InsertRow();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Themmoi();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.uG_InsertRow();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.uG_InsertRow();
            }
        }

        private void In_F10()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {

            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {

            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {

            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {

            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Rptdanhsach();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Rptdanhsach();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {

            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {
                _frmSapxepphongthi.InDanhSach();
            }
        }

        private void NapDuLieu_F8()
        {
            if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.Napdulieu();
            }
        }

        #endregion

        #region Button

        private void btnDong_Click(object sender, EventArgs e)
        {
            Dong_Esc();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy_F12();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Luu_F5();
        }

        private void btnXoadong_Click(object sender, EventArgs e)
        {
            Xoadong_F11();
        }

        private void btnthemmoi_Click(object sender, EventArgs e)
        {
            Themmoi_Insert();
        }

        private void btnInds_Click(object sender, EventArgs e)
        {
            In_F10();
        }

        private void btnNapDuLieu_Click(object sender, EventArgs e)
        {
            NapDuLieu_F8();
        }

        #endregion

        #region Loadding

        private FrmLoadding _loading;

        private void ShowLoading(object sender)
        {
            _loading = new FrmLoadding();
            _loading.ShowDialog();
        }

        private void KillLoading(object sender)
        {
            try
            {
                if (_loading != null)
                {
                    _loading.Invoke((Action)(() =>
                    { if (_loading != null) _loading.Close(); }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.F):
                    if (!_dangnhap) break;
                    var frmChon = new FrmChonChucNang();
                    frmChon.ShowDialog();
                    if (frmChon.StrChucNang != "" && frmChon.StrChucNang != "login")
                    {
                        ChonChucNang(frmChon.StrChucNang);
                    }
                    break;
                case (Keys.Escape):
                    Dong_Esc();
                    break;
                case (Keys.F3):
                    Xoa_F3();
                    break;
                case (Keys.F5):
                    Luu_F5();
                    break;
                case (Keys.F8):
                    NapDuLieu_F8();
                    break;
                case (Keys.F10):
                    In_F10();
                    break;
                case (Keys.F11):
                    Xoadong_F11();
                    break;
                case (Keys.F12):
                    Huy_F12();
                    break;
                case (Keys.Insert):
                    Themmoi_Insert();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (DateTime.Now.Hour > 12)
        //    {
        //        lbtime.Text = DateTime.Now.ToString();
        //    }
        //    else
        //    {
        //        lbtime.Text = DateTime.Now.ToString();
        //    }
        //}

    }
}
