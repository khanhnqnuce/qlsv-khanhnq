using System;
using System.Data;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_208_ThongKeDiem : FunctionControlHasGrid
    {
        public Frm_208_ThongKeDiem()
        {
            InitializeComponent();
        }

        #region

        protected override DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MaSinhVien", typeof(string));
            table.Columns.Add("HoSinhVien", typeof(string));
            table.Columns.Add("TenSinhVien", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("DiemThi", typeof(int));
            return table;
        }

        protected override void LoadGrid()
        {
            try
            {
                lbthongke.Text = "";
                dgv_DanhSach.DataSource = SearchData.Thongkediem(6);
                pnl_from.Visible = true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        private void Frm_208_ThongKeDiem_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cbothongke_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbthongke.Text = "";
            dgv_DanhSach.DataSource = SearchData.Thongkediem(cbothongke.SelectedIndex);
            lbthongke.Text = @"Có "+ dgv_DanhSach.Rows.Count+ @" Sinh viên có điểm " + cbothongke.Text;
        }
    }
}
