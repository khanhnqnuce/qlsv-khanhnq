using System;
using System.Windows.Forms;
using System.Data;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmGopKQ : Form
    {
        public bool Update;
        public FrmGopKQ()
        {
            InitializeComponent();
        }

        private void FrmGopKQ_Load(object sender, EventArgs e)
        {
            var table = new DataTable();
            table.Columns.Add("ma", typeof (string));
            table.Columns.Add("ten", typeof (string));
            table.Rows.Add("HK0", "Học kỳ 0");
            table.Rows.Add("HK1", "Học kỳ 1");
            table.Rows.Add("HK2", "Học kỳ 2");
            table.Rows.Add("HK3", "Học kỳ 3");
            cbohocky.DisplayMember = "ten";
            cbohocky.ValueMember = "ma";
            cbohocky.DataSource = table;

            cboNamHoc.DataSource = LoadData.Load(209);
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboNamHoc.Text))
            {
                errorNH.SetError(cboNamHoc, "Chọn năm học");
            }else if(string.IsNullOrEmpty(cbohocky.Text))
            {
                errorHK.SetError(cbohocky,"Chọn học kỳ");
            }
            else
            {
                Update = true;
                Close();
            }
        }
    }
}
