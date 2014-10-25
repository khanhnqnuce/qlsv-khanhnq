using System;
using System.Data;
using System.Windows.Forms;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Frm;

namespace QLSV.Frm
{
    public partial class FrmMain : Form
    {

        private static FrmDangNhap _frmDangNhap;
        private static FrmQuanLyNguoiDung _frmQuanLyNguoiDung;
        private static FrmSinhVien _frmSinhVien;
        private static FrmDanhmuckhoa _frmDanhmuckhoa;
        private static FrmDanhmuclop _frmDanhmuclop;
        private static FrmDanhsachphongthi _frmDanhsachphongthi;

        public FrmMain()
        {
            InitializeComponent();
            _frmDangNhap = new FrmDangNhap();
            _frmDangNhap.CheckDangNhap += CheckDangNhap;
        }

        private void LoadDefaul(string quyen)
        {
            switch (quyen)
            {
                case "quantri":
                    MenuBar.Tools["dangnhap"].SharedProps.Enabled = false;
                    MenuBar.Tools["dangxuat"].SharedProps.Enabled = true;
                    MenuBar.Tools["doimatkhau"].SharedProps.Enabled = true;
                    MenuBar.Tools["101"].SharedProps.Enabled = true;
                    MenuBar.Tools["201"].SharedProps.Enabled = true;
                    MenuBar.Tools["103"].SharedProps.Enabled = true;
                    MenuBar.Tools["104"].SharedProps.Enabled = true;
                    MenuBar.Tools["105"].SharedProps.Enabled = true;
                    MenuBar.Tools["106"].SharedProps.Enabled = true;
                    panel_footer.Enabled = true;
                    _frmDangNhap.Close();
                    break;
                case "nguoidung":
                    MenuBar.Tools["dangnhap"].SharedProps.Enabled = false;
                    MenuBar.Tools["dangxuat"].SharedProps.Enabled = true;
                    MenuBar.Tools["doimatkhau"].SharedProps.Enabled = true;
                    MenuBar.Tools["101"].SharedProps.Enabled = false;
                    MenuBar.Tools["201"].SharedProps.Enabled = true;
                    MenuBar.Tools["103"].SharedProps.Enabled = true;
                    MenuBar.Tools["104"].SharedProps.Enabled = true;
                    MenuBar.Tools["105"].SharedProps.Enabled = true;
                    MenuBar.Tools["106"].SharedProps.Enabled = true;
                    panel_footer.Enabled = true;
                    _frmDangNhap.Close();
                    break;
                default:
                    MenuBar.Tools["dangnhap"].SharedProps.Enabled = true;
                    MenuBar.Tools["dangxuat"].SharedProps.Enabled = false;
                    MenuBar.Tools["doimatkhau"].SharedProps.Enabled = false;
                    MenuBar.Tools["101"].SharedProps.Enabled = false;
                    MenuBar.Tools["201"].SharedProps.Enabled = false;
                    MenuBar.Tools["103"].SharedProps.Enabled = false;
                    MenuBar.Tools["104"].SharedProps.Enabled = false;
                    MenuBar.Tools["105"].SharedProps.Enabled = false;
                    MenuBar.Tools["106"].SharedProps.Enabled = false;
                    panel_footer.Enabled = false;
                    break;
            }

        }

        private void ChonChucNang(string strChucNang)
        {
            try
            {
                switch (strChucNang)
                {
                    case "dangnhap":
                        _frmDangNhap.ShowDialog();
                        break;
                    case "dangxuat":
                        LoadDefaul(null);
                        _frmDangNhap.txtMatKhau.Clear();
                        lbusername.Text = "";
                        _frmDangNhap.ShowDialog();
                        break;
                    case "101":
                        _frmQuanLyNguoiDung = new FrmQuanLyNguoiDung();
                        _frmQuanLyNguoiDung.ShowDialog();
                        break;
                    case "201":
                        _frmSinhVien = new FrmSinhVien();
                        _frmSinhVien.ShowDialog();
                        break;
                    case "103":
                        var frmQuanLyKyThi = new FrmQuanLyKyThi();
                        frmQuanLyKyThi.ShowDialog();
                        break;
                    case "104":
                        _frmDanhmuckhoa = new FrmDanhmuckhoa();
                        _frmDanhmuckhoa.ShowDialog();
                        break;
                    case "105":
                        _frmDanhmuclop = new FrmDanhmuclop();
                        _frmDanhmuclop.ShowDialog();
                        break;
                    case "106":
                        _frmDanhsachphongthi = new FrmDanhsachphongthi();
                        _frmDanhsachphongthi.ShowDialog();
                        break;
                    case "thoat":
                        if (DialogResult.Yes ==
                            MessageBox.Show(FormResource.msgThoat, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question))
                        {
                            Application.Exit();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
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
            lbusername.Text = hs.HoTen;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.F):
                    var frmChon = new FrmChonChucNang();
                    frmChon.ShowDialog();
                    frmChon.BringToFront();
                    frmChon.Focus();
                    if (frmChon.StrChucNang != "")
                    {
                        ChonChucNang(frmChon.StrChucNang);
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Event_uG

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDefaul(null);
                _frmDangNhap.ShowDialog();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnkythi_Click(object sender, EventArgs e)
        {
            var frmQuanLyKyThi = new FrmQuanLyKyThi();
            frmQuanLyKyThi.ShowDialog();
        }

        private void cboChonKyThi_Click(object sender, EventArgs e)
        {
            new QuanlysinhvienSevice();

            var tb = new DataTable();
            tb.Columns.Add("Mã kỳ thi", typeof (string));
            tb.Columns.Add("Tên kỳ thi", typeof (string));
            foreach (var item in QuanlysinhvienSevice.Load<Kythi>())
            {
                tb.Rows.Add(item.MaKyThi, item.TenKyThi);
            }
            cboChonKyThi.DataSource = tb;
            cboChonKyThi.DisplayMember = "Tên kỳ thi";
            cboChonKyThi.ValueMember = "Mã kỳ thi";
        }

        private void MenuBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            try
            {
                ChonChucNang(e.Tool.Key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (DateTime.Now.Hour > 12)
        //    {
        //        lbdatetime.Text = DateTime.Now.ToString();
        //    }
        //    else
        //    {
        //        lbdatetime.Text = DateTime.Now.ToString();
        //    }
        //}
    }
}
