using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlBulkCopy
{
    public static class Connect
    {
        
        // Phương thức kết nối sql
        public static SqlConnection GetConnect()
        {
            try
            {
                return new SqlConnection( @"Data Source=QUANGKHANH-PC\SQLEXPRESS; Integrated Security=true;Initial Catalog=ToeicTestManager;");
                //---Win xp
                //const string conString = @"Data Source=.\SQLEXPRESS; AttachDbFilename=DataDirectory|\QLSV_TEST.mdf; Integrated Security=True; Connect Timeout=30; User Instance=True;";
                //---Win 7
                //const string conString = @"Server=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\QLSV_TEST.mdf; Database=QLSV_TEST;Trusted_Connection=Yes;";
                //return new SqlConnection(conString);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public static DataTable GetTable(String sql)
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
        public static void ExcuteQuerySql(string sql)
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

        // Phương thức đổ 1 bảng vào CSDL ...
        public static bool InsertTable(string tablename, DataTable table)
        {
            using (var connection = GetConnect())
            {
                connection.Open();
                using (var bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo." + tablename;

                    try
                    {
                        bulkCopy.WriteToServer(table);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        
                        return false;
                    }
                }
            }
        }

        private static void Storedprocedure(string storename)
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
            catch (Exception ex)
            {
                
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
