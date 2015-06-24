using System.Data;
using System.Data.SqlClient;

namespace QLSV.Core.LINQ
{
    public class SqlBulkCopy
    {
        readonly Connect _connect = new Connect();
        public DataTable tbKhoa()
        {
            var newProducts = new DataTable("KHOA");
            newProducts.Columns.Add("ID", typeof(int));
            newProducts.Columns.Add("TenKhoa", typeof(string));
            return newProducts;
        }

        public DataTable tbXepPhong()
        {
            var newProducts = new DataTable("XEPPHONG");
            newProducts.Columns.Add("IdSV", typeof(int));
            newProducts.Columns.Add("IdKyThi", typeof(int));
            newProducts.Columns.Add("IdPhong", typeof(int));
            return newProducts;
        }
        
        public DataTable tbKTPhong()
        {
            var newProducts = new DataTable("KT_PHONG");
            newProducts.Columns.Add("IdPhong", typeof(int));
            newProducts.Columns.Add("IdKyThi", typeof(int));
            newProducts.Columns.Add("SiSo", typeof(int));
            return newProducts;
        }
        public DataTable tbBAILAM()
        {
            var newProducts = new DataTable("BAILAM");
            newProducts.Columns.Add("IdKyThi", typeof(int));
            newProducts.Columns.Add("MaSV", typeof(int));
            newProducts.Columns.Add("MaDe", typeof(string));
            newProducts.Columns.Add("KetQua", typeof(string));
            newProducts.Columns.Add("DiemThi", typeof(double));
            newProducts.Columns.Add("MaHoiDong", typeof(string));
            newProducts.Columns.Add("MaLoCham", typeof(string));
            newProducts.Columns.Add("TenFile", typeof(string));
            return newProducts;
        }
        public DataTable tbDAPAN()
        {
            var newProducts = new DataTable("DAPAN");
            newProducts.Columns.Add("IdKyThi", typeof(int));
            newProducts.Columns.Add("MaMon", typeof(string));
            newProducts.Columns.Add("MaDe", typeof(string));
            newProducts.Columns.Add("CauHoi", typeof(int));
            newProducts.Columns.Add("DapAn", typeof(string));
            newProducts.Columns.Add("ThangDiem", typeof(double));
            return newProducts;
        }

        /// <summary>
        /// Insert một dữ liệu lớn đổ 1 table vào csdl
        /// </summary>
        /// <param name="tablename">Tên bảng cần insert</param>
        /// <param name="table">Bảng dữ liệu cần insert</param>
        public void Bulk_Insert(string tablename, DataTable table)
        {
            using (var connection = _connect.GetConnect())
            {
                connection.Open();
                using (var bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo." + tablename;
                    bulkCopy.WriteToServer(table);
                }
                connection.Close();
            }
        }
        /// <summary>
        /// Insert or Update số lượng bản ghi lớn
        /// </summary>
        /// <param name="storename">Tên store</param>
        /// <param name="tbType">Kiểu parameter</param>
        /// <param name="table">Bảng dữ liệu cần insert</param>
        public void sp_InsertUpdate(string storename, string tbType, DataTable table)
        {
            using (var con = _connect.GetConnect())
            {
                using (var cmd = new SqlCommand(storename))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue(tbType, table);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        
        /// <summary>
        /// //Truyền vào 1 datatable gồm nhiều dữ liệu kiểm tra xem đã tồn tại chưa
        /// </summary>
        /// <param name="storename">Tên store</param>
        /// <param name="tbType">Tên biến Parameter</param>
        /// <param name="table">Bảng dữ liệu cần kiểm tra</param>
        /// <returns>Trả về dữ liệu chưa tồn tịa</returns>
        public DataTable sp_checkData(string storename, string tbType, DataTable table)
        {
            var dt = new DataTable();
            using (var con = _connect.GetConnect())
            {
                using (var cmd = new SqlCommand(storename))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue(tbType, table);
                    con.Open();
                    var adt = new SqlDataAdapter {SelectCommand = cmd};
                    adt.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
    }
}
