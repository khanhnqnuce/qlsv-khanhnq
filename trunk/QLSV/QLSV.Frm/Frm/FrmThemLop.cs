using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmThemLop : Form
    {
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

        private DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("IdKhoa", typeof(int));
            return table;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                var save = new SqlBulkCopy();
                var tbLop = GetTable();
                const string enter = "\n";
                if(!Checknull()) return;
                var strchuoi = txtLop.Text;
                var list = strchuoi.Split(char.Parse(enter));
                foreach (var dslop in list.Select(str => str.Trim().Split(',')).SelectMany(listlop => listlop.Select(s => s.Trim().ToUpper()).Where(dslop => !string.IsNullOrEmpty(dslop))))
                {
                    tbLop.Rows.Add(dslop, _idkhoa);
                }
                if (tbLop.Rows.Count <= 0) return;
                save.sp_InsertUpdate("sp_InsertLop", "@tbl",tbLop);
                MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption);
                txtLop.Clear();
                cbokhoa.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Thao tác thất bại", FormResource.MsgCaption);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
