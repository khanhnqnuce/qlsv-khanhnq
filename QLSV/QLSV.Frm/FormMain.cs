using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Infragistics.Win;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using Infragistics.Win.UltraWinExplorerBar;
using QLSV.Data.Utils.Data;
using QLSV.Frm.Frm;
using Infragistics.Win.UltraWinTabControl;
using QLSV.Frm.FrmUserControl;
using Infragistics.Win.UltraWinGrid;

namespace QLSV.Frm
{
    public partial class FormMain : Form
    {
        private static FrmDangNhap _frmDangNhap;
        private static Frm_QuanLyNguoiDung _frmQuanLyNguoiDung;
        private static Frm_101_Danhmuckhoa _frmDanhmuckhoa;
        private static Frm_102_Danhmuclop _frmDanhmuclop;
        private static Frm_103_TuDienPhongThi _frmDanhsachphongthi;
        private static Frm_104_QuanLySinhVien _frmQuanlySinhVien;
        private static Frm_105_InportSinhVien _frmInportSinhVien;
        private static Frm_106_QuanLyKyThi _frmQuanLyKyThi;
        private static Frm_107_Chonphongthi _frmChonphongthi;
        private static Frm_108_ChonSinhVien _frmChonSinhVien;
        private static Frm_109_SapXepPhongThi _frmSapXepPhongThi;
        private static Frm_110_SinhVienDuThi _frmSinhVienDuThi;
        private static Frm_201_InportDapAn _frmInportDapAn;
        private static Frm_203_InportBaiLam _frmInportBaiLam;
        private static Frm_202_DanhSachDapAn _frmDapAnCacMaDe;
        private static Frm_204_DanhSachBaiLam _frmDanhSachBaiLam;
        private static Frm_206_NhapThangDiem _frmNhapThangDiem;
        private static Frm_207_ChamDiemThi _frmChamDiemThi;
        private static Frm_208_ThongKeDiem _frmThongKeDiem;
        private static Frm_209_GopKeQuaThi _frmGopKeQua;
        private bool _dangnhap;
        private int _idkythi ;
        private string _taikhoan;
        private string _matkhau;

        public FormMain()
        {
            InitializeComponent();
            _frmDangNhap = new FrmDangNhap();
            _frmDangNhap.CheckDangNhap += CheckDangNhap;
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
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void LoadDefaul(string quyen)
        {
            switch (quyen)
            {
                case "quantri":
                    //MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["QLND"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["209"].Settings.Enabled = DefaultableBoolean.True;
                    cboChonkythi.Enabled = true;
                    break;
                case "nguoidung":
                    //MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["QLND"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["209"].Settings.Enabled = DefaultableBoolean.True;
                    Tabquanlynguoidung.Tab.Visible = false;
                    cboChonkythi.Enabled = true;
                    break;
                default:
                    TabPageControl.Visible = false;
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["QLND"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["109"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["110"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["201"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["202"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["203"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["204"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["205"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["206"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["207"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["208"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["209"].Settings.Enabled = DefaultableBoolean.False;
                    Tabquanlynguoidung.Tab.Visible = false;
                    Tabdanhmuckhoa.Tab.Visible = false;
                    Tabdanhmuclop.Tab.Visible = false;
                    TabInportsinhvien.Tab.Visible = false;
                    Tabquanlysinhvien.Tab.Visible = false;
                    Tabquanlykythi.Tab.Visible = false;
                    Tabdanhsachphongthi.Tab.Visible = false;
                    TabSapxepphongthi.Tab.Visible = false;
                    Tabdaxepphong.Tab.Visible = false;
                    TabInportdapdan.Tab.Visible = false;
                    TabDapanmade.Tab.Visible = false;
                    TabInportbailam.Tab.Visible = false;
                    TabDanhsachbailam.Tab.Visible = false;
                    TabNhapthangdiem.Tab.Visible = false;
                    Tabchamdiemthi.Tab.Visible = false;
                    Tabthongkediem.Tab.Visible = false;
                    TabChonPhongThi.Tab.Visible = false;
                    Tabchonsinhvien.Tab.Visible = false;
                    Tabgopketqua.Tab.Visible = false;
                    cboChonkythi.Enabled = false;
                    //cboChonkythi.Value = null;
                    break;
            }

        }

        private void ChonChucNang(string strChucNang)
        {
            try
            {
                SelectTabControl();
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
                        var frmdmk = new FrmDoiMatKhau(_taikhoan,_matkhau) {CheckUpdate = false};
                        frmdmk.ShowDialog();
                        if (frmdmk.CheckUpdate) _matkhau = MaHoaMd5.Md5(frmdmk.txtMK3.Text);
                        break;
                    case "thoat":
                        Close();
                        break;
                    case "QLND":
                        _frmQuanLyNguoiDung = new Frm_QuanLyNguoiDung();
                        Tabquanlynguoidung.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlynguoidung.Tab;
                        ShowControl(_frmQuanLyNguoiDung, pn_quanlynguoidung);
                        break;
                    case "101":
                        _frmDanhmuckhoa = new Frm_101_Danhmuckhoa();
                        Tabdanhmuckhoa.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhmuckhoa.Tab;
                        ShowControl(_frmDanhmuckhoa, pn_danhmuckhoa);
                        break;
                    case "102":
                        _frmDanhmuclop = new Frm_102_Danhmuclop();
                        Tabdanhmuclop.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhmuclop.Tab;
                        ShowControl(_frmDanhmuclop, pn_danhmuclop);
                        break;
                    case "103":
                        _frmDanhsachphongthi = new Frm_103_TuDienPhongThi();
                        Tabdanhsachphongthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhsachphongthi.Tab;
                        ShowControl(_frmDanhsachphongthi, pn_danhsachphong);
                        break;
                    case "104":
                        _frmQuanlySinhVien = new Frm_104_QuanLySinhVien();
                        _frmQuanlySinhVien.ShowDialog += ShowLoading;
                        _frmQuanlySinhVien.CloseDialog += KillLoading;
                        _frmQuanlySinhVien.UpdateDialog += UpdateLoading;
                        Tabquanlysinhvien.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlysinhvien.Tab;
                        ShowControl(_frmQuanlySinhVien, pn_quanlysinhvien);
                        break;
                    case "105":
                        _frmInportSinhVien = new Frm_105_InportSinhVien();
                        _frmInportSinhVien.ShowDialog += ShowLoading;
                        _frmInportSinhVien.CloseDialog += KillLoading;
                        _frmInportSinhVien.UpdateDialog += UpdateLoading;
                        TabInportsinhvien.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabInportsinhvien.Tab;
                        ShowControl(_frmInportSinhVien, pn_inportsinhvien);
                        break;
                    case "106":
                        _frmQuanLyKyThi = new Frm_106_QuanLyKyThi();
                        Tabquanlykythi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlykythi.Tab;
                        ShowControl(_frmQuanLyKyThi, pn_quanlykythi);
                        break;
                    case "107":
                        _frmChonphongthi = new Frm_107_Chonphongthi(_idkythi);
                        _frmChonphongthi.ShowDialog += ShowLoading;
                        _frmChonphongthi.CloseDialog += KillLoading;
                        _frmChonphongthi.UpdateDialog += UpdateLoading;
                        TabChonPhongThi.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabChonPhongThi.Tab;
                        ShowControl(_frmChonphongthi, pnl_chonphongthi);
                        break;
                    case "108":
                        _frmChonSinhVien = new Frm_108_ChonSinhVien(_idkythi);
                        _frmChonSinhVien.ShowDialog += ShowLoading;
                        _frmChonSinhVien.CloseDialog += KillLoading;
                        _frmChonSinhVien.UpdateDialog += UpdateLoading;
                        Tabchonsinhvien.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabchonsinhvien.Tab;
                        ShowControl(_frmChonSinhVien, pnl_chonsinhvien);
                        break;
                    case "109":
                        _frmSapXepPhongThi = new Frm_109_SapXepPhongThi(_idkythi);
                        _frmSapXepPhongThi.ShowDialog += ShowLoading;
                        _frmSapXepPhongThi.CloseDialog += KillLoading;
                        _frmSapXepPhongThi.UpdateDialog += UpdateLoading;
                        TabSapxepphongthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabSapxepphongthi.Tab;
                        ShowControl(_frmSapXepPhongThi, pnl_sapxepphongthi);
                        break;
                    case "110":
                        _frmSinhVienDuThi = new Frm_110_SinhVienDuThi(_idkythi);
                        _frmSinhVienDuThi.ShowDialog += ShowLoading;
                        _frmSinhVienDuThi.CloseDialog += KillLoading;
                        _frmSinhVienDuThi.UpdateDialog += UpdateLoading;
                        Tabdaxepphong.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdaxepphong.Tab;
                        ShowControl(_frmSinhVienDuThi, pnl_daxepphong);
                        break;
                    case "201":
                        _frmInportDapAn = new Frm_201_InportDapAn(_idkythi);
                        _frmInportDapAn.ShowDialog += ShowLoading;
                        _frmInportDapAn.CloseDialog += KillLoading;
                        _frmInportDapAn.UpdateDialog += UpdateLoading;
                        TabInportdapdan.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabInportdapdan.Tab;
                        ShowControl(_frmInportDapAn, pnl_Inportdapan);
                        break;
                    case "202":
                        _frmDapAnCacMaDe = new Frm_202_DanhSachDapAn(_idkythi);
                        _frmDapAnCacMaDe.ShowDialog += ShowLoading;
                        _frmDapAnCacMaDe.CloseDialog += KillLoading;
                        _frmDapAnCacMaDe.UpdateDialog += UpdateLoading;
                        TabDapanmade.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabDapanmade.Tab;
                        ShowControl(_frmDapAnCacMaDe, pnl_Dapanmade);
                        break;
                    case "203":
                        _frmInportBaiLam = new Frm_203_InportBaiLam(_idkythi);
                        _frmInportBaiLam.ShowDialog += ShowLoading;
                        _frmInportBaiLam.CloseDialog += KillLoading;
                        _frmInportBaiLam.UpdateDialog += UpdateLoading;
                        TabInportbailam.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabInportbailam.Tab;
                        ShowControl(_frmInportBaiLam, pnl_Inportbailam);
                        break;
                    case "204":
                        _frmDanhSachBaiLam = new Frm_204_DanhSachBaiLam(_idkythi);
                        _frmDanhSachBaiLam.ShowDialog += ShowLoading;
                        _frmDanhSachBaiLam.CloseDialog += KillLoading;
                        _frmDanhSachBaiLam.UpdateDialog += UpdateLoading;
                        TabDanhsachbailam.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabDanhsachbailam.Tab;
                        ShowControl(_frmDanhSachBaiLam, pnl_danhsachbailam);
                        break;
                    case "205":
                        var frm = new FrmKiemTraLoiLogic(_idkythi);
                        frm.Indanhsach();
                        break;
                    case "206":
                        _frmNhapThangDiem = new Frm_206_NhapThangDiem(_idkythi);
                        _frmNhapThangDiem.ShowDialog += ShowLoading;
                        _frmNhapThangDiem.CloseDialog += KillLoading;
                        _frmNhapThangDiem.UpdateDialog += UpdateLoading;
                        TabNhapthangdiem.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabNhapthangdiem.Tab;
                        ShowControl(_frmNhapThangDiem, pnl_nhapthangdiem);
                        break;
                    case "207":
                        _frmChamDiemThi = new Frm_207_ChamDiemThi(_idkythi);
                        _frmChamDiemThi.ShowDialog += ShowLoading;
                        _frmChamDiemThi.CloseDialog += KillLoading;
                        _frmChamDiemThi.UpdateDialog += UpdateLoading;
                        Tabchamdiemthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabchamdiemthi.Tab;
                        ShowControl(_frmChamDiemThi, pnl_chamdiemthi);
                        break;
                    case "208":
                        _frmThongKeDiem = new Frm_208_ThongKeDiem(_idkythi);
                        _frmThongKeDiem.ShowDialog += ShowLoading;
                        _frmThongKeDiem.CloseDialog += KillLoading;
                        _frmThongKeDiem.UpdateDialog += UpdateLoading;
                        Tabthongkediem.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabthongkediem.Tab;
                        ShowControl(_frmThongKeDiem, pnl_thongkediem);
                        break;
                    case "209":
                        _frmGopKeQua = new Frm_209_GopKeQuaThi();
                        _frmGopKeQua.ShowDialog += ShowLoading;
                        _frmGopKeQua.CloseDialog += KillLoading;
                        _frmGopKeQua.UpdateDialog += UpdateLoading;
                        Tabgopketqua.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabgopketqua.Tab;
                        ShowControl(_frmGopKeQua, pnl_gopketqua);
                        break;
                    case "gioithieu":
                        //TabPageControl.Visible = false;
                        var frmGt = new FrmGioiThieu();
                        frmGt.ShowDialog();
                        break;
                    case "huongdan":
                        //TabPageControl.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void SelectTabControl()
        {
            try
            {
                if (!_dangnhap) return;
                TabPageControl.Visible = true;
                var bCheck = false;
                if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabDanhsachbailam.Tab.Visible && TabDanhsachbailam.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabNhapthangdiem.Tab.Visible && TabNhapthangdiem.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabchamdiemthi.Tab.Visible && Tabchamdiemthi.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (Tabthongkediem.Tab.Visible && Tabthongkediem.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabChonPhongThi.Tab.Visible && TabChonPhongThi.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (Tabchonsinhvien.Tab.Visible && Tabchonsinhvien.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (Tabgopketqua.Tab.Visible && Tabgopketqua.Tab.Active)
                {
                    bCheck = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                if (!bCheck)
                {
                    TabPageControl.Visible = false;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void CheckDangNhap(object sender, bool checkState, Taikhoan hs)
        {
            LoadDefaul(hs.Quyen);
            _dangnhap = true;
            lbusername.Text = hs.HoTen;
            _taikhoan = hs.TaiKhoan;
            _matkhau = hs.MatKhau;
            _frmDangNhap.Close();

            cboChonkythi.DataSource = LoadData.Load(18);
        }

        private void MenuBar_ItemClick(object sender, ItemEventArgs e)
        {
            try
            {
                ChonChucNang(e.Item.Key);
            }
            catch (Exception ex)
            {
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
            else if (TabChonPhongThi.Tab.Active)
            {
                TabChonPhongThi.Tab.Visible = false;
            }
            else if (Tabchonsinhvien.Tab.Active)
            {
                Tabchonsinhvien.Tab.Visible = false;
            }
            else if (TabSapxepphongthi.Tab.Active)
            {
                TabSapxepphongthi.Tab.Visible = false;
            }else if (Tabdaxepphong.Tab.Active)
            {
                Tabdaxepphong.Tab.Visible = false;
            }
            else if (TabInportdapdan.Tab.Active)
            {
                TabInportdapdan.Tab.Visible = false;
            }
            else if (TabDapanmade.Tab.Active)
            {
                TabDapanmade.Tab.Visible = false;
            }
            else if (TabInportbailam.Tab.Active)
            {
                TabInportbailam.Tab.Visible = false;
            }
            else if (TabDanhsachbailam.Tab.Active)
            {
                TabDanhsachbailam.Tab.Visible = false;
            }
            else if (TabNhapthangdiem.Tab.Active)
            {
                TabNhapthangdiem.Tab.Visible = false;
            }
            else if (Tabchamdiemthi.Tab.Active)
            {
                Tabchamdiemthi.Tab.Visible = false;
            }
            else if (Tabthongkediem.Tab.Active)
            {
                Tabthongkediem.Tab.Visible = false;
            }
            else if (Tabgopketqua.Tab.Active)
            {
                Tabgopketqua.Tab.Visible = false;
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
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.Huy();
            }
            else if (TabDanhsachbailam.Tab.Active)
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
                ChonChucNang("104");
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
                cboChonkythi.DataSource = LoadData.Load(18);
                SelectCbo();
            }
            else if (TabChonPhongThi.Tab.Visible && TabChonPhongThi.Tab.Active)
            {
                _frmChonphongthi.Save();
            }
            else if (Tabchonsinhvien.Tab.Visible && Tabchonsinhvien.Tab.Active)
            {
                _frmChonSinhVien.Save();
            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {
                _frmSapXepPhongThi.Ghi();
            }
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
                _frmSinhVienDuThi.Save();
            }
            else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.Ghi();
                ChonChucNang("202");
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.Save();
            }
            else if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
            {
                _frmInportBaiLam.Ghi();
                ChonChucNang("204");
            }
            else if (TabNhapthangdiem.Tab.Visible  && TabNhapthangdiem.Tab.Active)
            {
                _frmNhapThangDiem.Ghi();
            }
            else if (Tabchamdiemthi.Tab.Visible && Tabchamdiemthi.Tab.Active)
            {
                _frmChamDiemThi.Ghi();
            }
            else if (Tabgopketqua.Tab.Visible && Tabgopketqua.Tab.Active)
            {
                _frmGopKeQua.Save();
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
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
                _frmSinhVienDuThi.Xoa();
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.Xoa();
            }
            else if (TabDanhsachbailam.Tab.Visible && TabDanhsachbailam.Tab.Active)
            {
                _frmDanhSachBaiLam.Xoa();
            }
            else if (TabChonPhongThi.Tab.Visible && TabChonPhongThi.Tab.Active)
            {
                _frmChonphongthi.Xoa();
            }
            else if (Tabchonsinhvien.Tab.Visible && Tabchonsinhvien.Tab.Active)
            {
                _frmChonSinhVien.Xoa();
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
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
                _frmSinhVienDuThi.uG_DeleteRow();
            }
            else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.uG_DeleteRow();
            }
            else if (TabChonPhongThi.Tab.Visible && TabChonPhongThi.Tab.Active)
            {
                _frmChonphongthi.uG_DeleteRow();
            }
            else if (Tabchonsinhvien.Tab.Visible && Tabchonsinhvien.Tab.Active)
            {
                _frmChonSinhVien.uG_DeleteRow();
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
            else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.uG_InsertRow();
            }
            else if (TabChonPhongThi.Tab.Visible && TabChonPhongThi.Tab.Active)
            {
                _frmChonphongthi.uG_InsertRow();
            }
            else if (Tabchonsinhvien.Tab.Visible && Tabchonsinhvien.Tab.Active)
            {
               _frmChonSinhVien.uG_InsertRow();
            }
        }

        private void In_F10()
        {
            if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Rptdanhsach();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Rptdanhsach();
            }
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
               _frmSinhVienDuThi.InDanhSach();
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.InDanhSach();
            }
            else if (Tabchamdiemthi.Tab.Active)
            {
                _frmChamDiemThi.InDanhSach();
            }
            else if (Tabthongkediem.Tab.Active)
            {
                _frmThongKeDiem.InDanhSach();
            }
            else if (Tabgopketqua.Tab.Visible && Tabgopketqua.Tab.Active)
            {
                _frmGopKeQua.InDanhSach();
            }
        }

        private void NapDuLieu_F8()
        {
            if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.Napdulieu();
            }
            if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.Napdulieu();
            }
            if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
            {
               _frmInportBaiLam.Napdulieu();
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

        #endregion

        #region Loadding

        private FrmLoadding _loading;

        private void ShowLoading(object sender, string msg)
        {
            _loading = new FrmLoadding();
            _loading.Update(msg);
            _loading.ShowDialog();
        }

        private void KillLoading(object sender)
        {
            try
            {
                if (_loading != null)
                {
                    _loading.Invoke((Action)(() =>
                    {
                        _loading.Close();
                        //_loading.Dispose();
                        _loading = null;
                    }));
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void UpdateLoading(object sender, string strInfo)
        {
            if (_loading != null)
            {
                _loading.Invoke((Action)(() => _loading.Update(strInfo)));
            }
        }

        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDefaul(null);
            _frmDangNhap.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var a = MessageBox.Show(@"Bạn có muốn thoát chương trình không?",
                    @"Thông báo",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) != DialogResult.OK;
                e.Cancel = a;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cboChonkythi_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            cboChonkythi.Rows.Band.Columns["ID"].Hidden = true;
            cboChonkythi.Rows.Band.Columns["MaKT"].Width = 70;
            cboChonkythi.Rows.Band.Columns["TenKT"].Width = 250;
            cboChonkythi.Rows.Band.ColHeadersVisible = false;
        }

        private void CloseTab()
        {

            TabSapxepphongthi.Tab.Visible = false;
            Tabdaxepphong.Tab.Visible = false;
            TabInportdapdan.Tab.Visible = false;
            TabDapanmade.Tab.Visible = false;
            TabInportbailam.Tab.Visible = false;
            TabDanhsachbailam.Tab.Visible = false;
            TabNhapthangdiem.Tab.Visible = false;
            Tabchamdiemthi.Tab.Visible = false;
            Tabthongkediem.Tab.Visible = false;
            TabChonPhongThi.Tab.Visible = false;
            Tabchonsinhvien.Tab.Visible = false;

            MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["109"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["110"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["201"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["202"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["203"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["204"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["205"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["206"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["207"].Settings.Enabled = DefaultableBoolean.False;
            MenuBar.Groups["chuongtrinh"].Items["208"].Settings.Enabled = DefaultableBoolean.False;
            
        }

        private void SelectCbo()
        {
            lbXoa.Focus();
            var index = cboChonkythi.Value;
            if (index == null)
            {
                CloseTab();
                return;
            }
            if (!IsNumber(index.ToString())) return;
            _idkythi = (int)index;
            CloseTab();

            #region menu
            
            MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["109"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["110"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["201"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["202"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["203"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["204"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["205"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["206"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["207"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["208"].Settings.Enabled = DefaultableBoolean.True;

            #endregion
        }

        private void cboChonkythi_ValueChanged(object sender, EventArgs e)
        {
            SelectCbo();
        }

        private static bool IsNumber(string pText)
        {
            var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void TabPageControl_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            SelectTabControl();
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
