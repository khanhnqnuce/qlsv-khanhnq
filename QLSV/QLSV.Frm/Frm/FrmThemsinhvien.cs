using System;
using System.Linq;
using System.Windows.Forms;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmThemsinhvien : Form
    {
        public FrmThemsinhvien()
        {
            InitializeComponent();
        }

        private void FrmThemsinhvien_Load(object sender, EventArgs e)
        {
            cbokhoa.DataSource = QlsvSevice.Load<Khoa>();
            cbokhoa.ValueMember = "ID";
            cbokhoa.DisplayMember = "TenKhoa";
            cbokhoa.Rows.Band.Columns["ID"].Hidden = true;
            cbokhoa.Rows.Band.Columns["MaKhoa"].Hidden = true;
            cbokhoa.Rows.Band.Columns["TenKhoa"].Width = 250;
            cbokhoa.Rows.Band.ColHeadersVisible = false;

            cbolop.DataSource = QlsvSevice.Load<Lop>();
            cbolop.ValueMember = "ID";
            cbolop.DisplayMember = "MaLop";
            cbolop.Rows.Band.Columns["ID"].Hidden = true;
            cbolop.Rows.Band.Columns["IdKhoa"].Hidden = true;
            cbolop.Rows.Band.Columns["Khoa"].Hidden = true;
            cbolop.Rows.Band.Columns["GhiChu"].Hidden = true;
            cbolop.Rows.Band.ColHeadersVisible = false;
            cbolop.DropDownWidth = 0;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            txtmasinhvien.Clear();
            txttensinhvien.Clear();
            txthotendem.Clear();
            cbongaysinh.Value = null;
            cbokhoa.Value = null;
            cbolop.Value = null;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Checkghi())
                {
                    var listLop = QlsvSevice.Load<Lop>();
                    var listKhoa = QlsvSevice.Load<Khoa>();
                    foreach (var lop in listLop.Where(lop => lop.ID.ToString() == cbolop.Value.ToString()))
                    {
                        var hs = new SinhVien
                        {
                            MaSinhVien = txtmasinhvien.Text,
                            HoSinhVien = txthotendem.Text,
                            TenSinhVien = txttensinhvien.Text,
                            NgaySinh = cbongaysinh.Text,
                            IdLop = lop.ID,
                        };
                        QlsvSevice.Them(hs);
                        MessageBox.Show(@"Ghi thành công");
                        return;
                    }
                    foreach (var khoa in listKhoa.Where(khoa => khoa.ID.ToString() == cbokhoa.Value.ToString()))
                    {
                        var newLop1 = SinhVienSql.ThemLop(cbolop.Text, khoa.ID);
                        var hs = new SinhVien
                        {
                            MaSinhVien = txtmasinhvien.Text,
                            HoSinhVien = txthotendem.Text,
                            TenSinhVien = txttensinhvien.Text,
                            NgaySinh = cbongaysinh.Text,
                            IdLop = newLop1.ID
                        };
                        QlsvSevice.Them(hs);
                        MessageBox.Show(@"Ghi thành công");
                        return;
                    }
                    var newkhoa = SinhVienSql.ThemKhoa(cbokhoa.Text);
                    var newLop3 = SinhVienSql.ThemLop(cbolop.Text, newkhoa.ID);
                    var hs1 = new SinhVien
                    {
                        MaSinhVien = txtmasinhvien.Text,
                        HoSinhVien = txthotendem.Text,
                        TenSinhVien = txttensinhvien.Text,
                        NgaySinh = cbongaysinh.Text,
                        IdLop = newLop3.ID
                    };
                    QlsvSevice.Them(hs1);
                    MessageBox.Show(@"Ghi thành công");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private bool Checkghi()
        {
            try
            {
                var b = true;
                if (string.IsNullOrEmpty(txtmasinhvien.Text))
                {
                    b = false;
                    errormasinhvien.SetError(txtmasinhvien, "Không được để trống");
                }
                else
                {
                    errormasinhvien.Dispose();
                }
                if (string.IsNullOrEmpty(txthotendem.Text))
                {
                    b = false;
                    errorhodem.SetError(txthotendem, "Không được để trống");
                }
                else
                {
                    errorhodem.Dispose();
                }
                if (string.IsNullOrEmpty(txttensinhvien.Text))
                {
                    b = false;
                    errortensinhvien.SetError(txttensinhvien, "Không được để trống");
                }
                else
                {
                    errortensinhvien.Dispose();
                }
                if (string.IsNullOrEmpty(cbongaysinh.Text))
                {
                    b = false;
                    errormasinhvien.SetError(cbongaysinh, "Không được để trống");
                }
                else
                {
                    errorngaysinh.Dispose();
                }
                if (string.IsNullOrEmpty(cbokhoa.Text))
                {
                    b = false;
                    errorkhoa.SetError(cbokhoa, "Không được để trống");
                }
                else
                {
                    errorkhoa.Dispose();
                }
                if (string.IsNullOrEmpty(cbolop.Text))
                {
                    b = false;
                    errorlop.SetError(cbolop, "Không được để trống");
                }
                else
                {
                    errorlop.Dispose();
                }
                return b;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }
    }
}
