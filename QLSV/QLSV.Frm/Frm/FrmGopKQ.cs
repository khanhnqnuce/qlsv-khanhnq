using System;
using System.Windows.Forms;
using System.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmGopKQ : Form
    {
        public FrmGopKQ()
        {
            InitializeComponent();
        }

        private void FrmGopKQ_Load(object sender, EventArgs e)
        {
            var table = new DataTable();
            table.Columns.Add("ma", typeof (string));
            table.Columns.Add("ten", typeof (string));
            table.Rows.Add("HK1", "Học kỳ 1");
            table.Rows.Add("HK2", "Học kỳ 2");
            cbohocky.DisplayMember = "ten";
            cbohocky.ValueMember = "ma";
            cbohocky.DataSource = table;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamHoc.Text))
            {
                errorNH.SetError(txtNamHoc, "Nhập năm học");
                return;
            }
            else if(string.IsNullOrEmpty(cbohocky.Text))
            {
                errorHK.SetError(cbohocky,"Chọn học kỳ");
            }

            Close();
        }
    }
}
