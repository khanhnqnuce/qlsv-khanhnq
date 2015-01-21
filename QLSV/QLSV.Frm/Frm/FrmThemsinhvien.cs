using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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

        private int _idkhoa, _idlop;

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

        private void FrmThemsinhvien_Load(object sender, EventArgs e)
        {
            cbokhoa.DataSource = LoadData.Load(3);
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
                    var hs = new SinhVien
                    {
                        MaSV = int.Parse(txtmasinhvien.Text),
                        HoSV = txthotendem.Text,
                        TenSV = txttensinhvien.Text,
                        NgaySinh = cbongaysinh.Text,
                        IdLop = _idlop,
                    };
                    InsertData.ThemSinhVien(hs);
                    Themmoisinhvien(sender, hs, cbolop.Text, cbokhoa.Text);
                    MessageBox.Show(@"Đã Thêm mới một sinh viên");
                }
                else
                {
                    var hs1 = new SinhVien
                    {
                        MaSV = int.Parse(txtmasinhvien.Text),
                        HoSV = txthotendem.Text,
                        TenSV = txttensinhvien.Text,
                        NgaySinh = cbongaysinh.Text,
                        IdLop = _idlop
                    };
                    if (UpdateData.UpdateSv(hs1))
                    {
                        MessageBox.Show(@"Chỉnh sửa thành công", @"Thông báo");
                        CheckUpdate = false;
                    }
                    else
                    {
                        MessageBox.Show(@"Sai thông tin lớp", @"Thông báo");
                    }
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

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cbolop.Refresh();
                _idkhoa = int.Parse(cbokhoa.SelectedValue.ToString());
                cbolop.DataSource = SearchData.LoadCboLop(_idkhoa);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbolop_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _idlop = int.Parse(cbolop.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
