using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmThemsinhvien : Form
    {
        public delegate void CustomHandler(object sender, SinhVien hs,string malop, string tenkhoa);

        public event CustomHandler Themmoisinhvien;

        public bool CheckUpdate;

        public FrmThemsinhvien()
        {
            InitializeComponent();
            LoadForm();
        }

        private void LoadForm()
        {
            cbokhoa.DataSource = LoadData.Load(15);
        }

        private void ClearAll()
        {
            txtmasinhvien.Clear();
            txttensinhvien.Clear();
            txthotendem.Clear();
            cbongaysinh.Value = null;
            cbokhoa.Value = null;
            cbolop.Value = null;
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

        private void cbokhoa_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = cbokhoa.Value;
                if (obj == null) return;
                cbolop.DataSource = SearchData.LoadCboLop(int.Parse(obj.ToString()));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbokhoa_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            cbokhoa.Rows.Band.Columns["STT"].Hidden = true;
            cbokhoa.Rows.Band.Columns["ID"].Hidden = true;
            cbokhoa.Rows.Band.Columns["MaKhoa"].Hidden = true;
            cbokhoa.Rows.Band.Columns["TenKhoa"].Width = 250;
            cbokhoa.Rows.Band.ColHeadersVisible = false;
        }

        private void cbolop_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.Columns["ID"].Hidden = true;
            band.Columns["IdKhoa"].Hidden = true;
            band.Columns["GhiChu"].Hidden = true;
            band.Columns["MaLop"].Width = 200;
            band.ColHeadersVisible = false;
            band.ColHeadersVisible = false;
        }

        private void FrmThemsinhvien_Load(object sender, EventArgs e)
        {
            if (CheckUpdate)
                Text = @"Chỉnh sửa thông tin";
            else
                Text = @"Thêm mới sinh viên";
        }

        private void btnluu_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Checkghi()) return;
                if (!CheckUpdate)
                {
                    var tbLop = LoadData.Load(15);
                    var tbKhoa = LoadData.Load(16);
                    var tbSv = LoadData.Load(17);
                    foreach (
                        var dataRow in
                            tbSv.Rows.Cast<DataRow>()
                                .Where(dataRow => dataRow["MaSV"].ToString().Equals(txtmasinhvien.Text)))
                    {
                        MessageBox.Show(@"Sinh viên đã có trong CSDL");
                        return;
                    }

                    foreach (
                        var dataRow in
                            tbLop.Rows.Cast<DataRow>().Where(dataRow => dataRow["ID"].ToString().Equals(cbolop.Value.ToString())))
                    {
                        var hs = new SinhVien
                        {
                            MaSV = int.Parse(txtmasinhvien.Text),
                            HoSV = txthotendem.Text,
                            TenSV = txttensinhvien.Text,
                            NgaySinh = cbongaysinh.Text,
                            IdLop = int.Parse(dataRow["ID"].ToString()),
                        };
                        InsertData.ThemSinhVien(hs);
                        Themmoisinhvien(sender, hs, cbolop.Text, cbokhoa.Text);
                        MessageBox.Show(@"Đã Thêm mới một sinh viên");
                        ClearAll();
                        return;
                    }

                    foreach (
                        var dataRow in
                            tbKhoa.Rows.Cast<DataRow>()
                                .Where(dataRow => dataRow["ID"].ToString().Equals(cbokhoa.Value.ToString())))
                    {
                        var newLop1 = InsertData.ThemLop(cbolop.Text, int.Parse(dataRow["ID"].ToString()));
                        var hs = new SinhVien
                        {
                            MaSV = int.Parse(txtmasinhvien.Text),
                            HoSV = txthotendem.Text,
                            TenSV = txttensinhvien.Text,
                            NgaySinh = cbongaysinh.Text,
                            IdLop = newLop1.ID
                        };
                        InsertData.ThemSinhVien(hs);
                        Themmoisinhvien(sender, hs, cbolop.Text, cbokhoa.Text);
                        MessageBox.Show(@"Đã Thêm mới một sinh viên");
                        ClearAll();
                        return;
                    }
                    var newkhoa = InsertData.ThemKhoa(cbokhoa.Text);
                    var newLop3 = InsertData.ThemLop(cbolop.Text, newkhoa.ID);
                    var hs1 = new SinhVien
                    {
                        MaSV = int.Parse(txtmasinhvien.Text),
                        HoSV = txthotendem.Text,
                        TenSV = txttensinhvien.Text,
                        NgaySinh = cbongaysinh.Text,
                        IdLop = newLop3.ID
                    };
                    InsertData.ThemSinhVien(hs1);
                    Themmoisinhvien(sender, hs1, cbolop.Text, cbokhoa.Text);
                    MessageBox.Show(@"Đã Thêm mới một sinh viên");
                    ClearAll();
                }
                else
                {
                    var hs1 = new SinhVien
                    {
                        MaSV = int.Parse(txtmasinhvien.Text),
                        HoSV = txthotendem.Text,
                        TenSV = txttensinhvien.Text,
                        NgaySinh = cbongaysinh.Text,
                        IdLop = int.Parse(cbolop.Value.ToString())
                    };
                    UpdateData.UpdateSv(hs1);
                    MessageBox.Show(@"Chỉnh sửa thành công", @"Thông báo");
                    CheckUpdate = false;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
