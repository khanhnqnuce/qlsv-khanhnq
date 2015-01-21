using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmThemLop : Form
    {
        private readonly List<Lop> _listAdd = new List<Lop>();
        private int _idkhoa;
        public FrmThemLop()
        {
            InitializeComponent();
        }

        private bool Checknull()
        {
            if (_idkhoa == 0)
            {
                errorkhoa.SetError(cbokhoa,"Chưa chọn khoa.");
                cbokhoa.Focus();
                return false;
            }
            errorkhoa.Dispose();
            if (string.IsNullOrEmpty(txtLop.Text))
            {
                errorlop.SetError(txtLop,"Chưa nhập vào danh sách lớp.");
                txtLop.Focus();
                return false;
            }
            errorlop.Dispose();
            return true;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                const string enter = "\n";
                if(!Checknull()) return;
                var strchuoi = txtLop.Text;
                var list = strchuoi.Split(char.Parse(enter));
                foreach (var str in list)
                {
                    var listlop = str.Trim().Split(',');
                    foreach (var s in listlop)
                    {
                        var dslop = s.Trim().ToUpper();
                        if(string.IsNullOrEmpty(dslop)) continue;
                        var hs = new Lop
                        {
                            MaLop = dslop,
                            IdKhoa = _idkhoa
                        };
                        _listAdd.Add(hs);
                    }
                }
                if (_listAdd.Count > 0)
                {
                    InsertData.ThemLop(_listAdd);
                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption);
                    txtLop.Clear();
                    cbokhoa.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void FrmThemLop_Load(object sender, EventArgs e)
        {
            try
            {
                var table = LoadData.Load(3);
                var tb = new DataTable();
                tb.Columns.Add("ID", typeof(string));
                tb.Columns.Add("TenKhoa", typeof(string));
                tb.Rows.Add("0", "- Chọn khoa -");
                foreach (DataRow row in table.Rows)
                {
                    tb.Rows.Add(row["ID"].ToString(), row["TenKhoa"].ToString());
                }
                cbokhoa.DataSource = tb;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
           
        }

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _idkhoa = int.Parse(cbokhoa.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
