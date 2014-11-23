using System;
using System.Data;
using System.Data.SqlClient;

namespace QLSV.Core.LINQ
{
    class Connect
    {
        private String _conString;
        
        // Phương thức kết nối sql
        private SqlConnection GetConnect(){
            _conString = @"Data Source=QUANGKHANH-PC;Initial Catalog=QLSV;Integrated Security=True";
            return new SqlConnection(_conString);
        }
        
        public DataTable GetTable(String sql)
        {
            var dt = new DataTable();
            try
            {

                var connect = GetConnect();
                var ad = new SqlDataAdapter(sql, connect);
                ad.Fill(dt);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dt;
        }

        // Phương thức thực hiện thêm sửa ...
        public void ExcuteQuerySql(string sql)
        {
            try
            {
                var connect = GetConnect();
                connect.Open();
                var cmd = new SqlCommand(sql, connect);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connect.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
