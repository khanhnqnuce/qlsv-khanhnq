using System;
using System.Data;
using System.Data.SqlClient;

namespace QLSV.Core.LINQ
{
    public class Connect
    {
        public static SqlConnection GetConnect()
        {
            try
            {
                const string conString = @"Data Source = QUANGKHANH-PC\SQLEXPRESS;Initial Catalog = ToiecTestManager;Integrated Security=SSPI";
                return new SqlConnection(conString);
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public DataTable GetTable(String sql)
        {
            var dt = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(sql)) return dt;
                var connect = GetConnect();
                var ad = new SqlDataAdapter(sql, connect);
                ad.Fill(dt);

            }
                catch (Exception ex)
            {
               
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
            catch (Exception ex)
            {
                
            }
        }

        private void Storedprocedure(string storename)
        {
            var connect = GetConnect();
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = storename
            };

            cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar).Value = "785255";
            cmd.Parameters.Add("@TenSV", SqlDbType.NVarChar).Value = "Khánh";

            cmd.Connection = connect;
            try
            {
                connect.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                
            }
            finally
            {
                connect.Close();
            }
        } 
    }
}
