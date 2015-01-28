using System;
using System.Data;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSuaLop : Form
    {
        public int Idlop;
        public bool update;
        public FrmSuaLop()
        {
            InitializeComponent();
        }

        private void btnluulai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbokhoa.Text))
            {
                errorkhoa.SetError(cbokhoa,"Chưa chọn khoa");
            }else if (string.IsNullOrEmpty(txtlop.Text))
            {
                errorkhoa.Dispose();
                errorlop.SetError(txtlop,"Chưa nhập tên lớp");
            }
            else
            {
                errorlop.Dispose();
                errorkhoa.Dispose();
                var idkhoa = int.Parse(cbokhoa.SelectedValue.ToString());
                UpdateData.UpdateLop(Idlop, idkhoa, txtlop.Text);
                update = true;
                Close();
            }

        }

        private void FrmSuaLop_Load(object sender, EventArgs e)
        {
            var table = LoadData.Load(3);
            var tb = new DataTable();
            tb.Columns.Add("ID", typeof(string));
            tb.Columns.Add("TenKhoa", typeof(string));
            tb.Rows.Add("0", "");
            foreach (DataRow row in table.Rows)
            {
                tb.Rows.Add(row["ID"].ToString(), row["TenKhoa"].ToString());
            }
            cbokhoa.DataSource = tb;
        }
    }
}
