using System;
using System.Data;
using System.Data.SqlClient;

namespace QLSV.Core.DataConnection
{
    class Connect
    {
        SqlConnection conn;
        public String conString;
        
        // Phương thức kết nối sql
        public SqlConnection getConnect(){
            
            //conString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\quanlybanhang.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            // return new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\QLKhachSan.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");

            conString = @"Data Source=QUANGKHANH-PC;Initial Catalog=QLSV;Integrated Security=True";
            return new SqlConnection(conString);
            //return new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=quanlybanhang;Integrated Security=True");
        }
        
        public DataTable getTable(String sql)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection conn = getConnect();
                SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
                ad.Fill(dt);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dt;
        }

        // Phương thức thực hiện them sua ...
        public void ExcuteQuerySql(string sql)
        {
            try
            {
                SqlConnection conn = getConnect();
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
