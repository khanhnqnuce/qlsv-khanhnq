using System.Data;
using QLSV.Frm.Base;

namespace QLSV.Frm.FrmUserControl
{
    public partial class InportDapAn : FunctionControlHasGrid
    {
        public InportDapAn()
        {
            InitializeComponent();
        }

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("IdKyThi", typeof(string));
            table.Columns.Add("MaMon", typeof(string));
            table.Columns.Add("MaDe", typeof(string));
            table.Columns.Add("CauHoi", typeof(string));
            table.Columns.Add("DapAn", typeof(string));
            return table;
        }
    }
}
