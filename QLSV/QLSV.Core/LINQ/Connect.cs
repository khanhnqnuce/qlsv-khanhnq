using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class Connect
    {
        private string _conString;
        private static string _sv;
        public string Database;
        private static string _user;
        private static string _pass;
        // Phương thức kết nối sql
        public SqlConnection GetConnect()
        {
            try
            {
                var xmlread = new XmlDocument();
                xmlread.Load("Connection.xml");
                var xmlelement = xmlread.DocumentElement;
                if (xmlelement != null)
                {
                    var serverNode = xmlelement.SelectSingleNode("server");
                    if (serverNode != null)
                        _sv = serverNode.InnerText;

                    var databaseNode = xmlelement.SelectSingleNode("database");
                    if (databaseNode != null)
                        Database = databaseNode.InnerText;

                    var usernameNode = xmlelement.SelectSingleNode("username");
                    if (usernameNode != null)
                        _user = usernameNode.InnerText;

                    var passwordNode = xmlelement.SelectSingleNode("password");
                    if (passwordNode != null)
                        _pass = passwordNode.InnerText;
                }
                if (_user == "") // ko dung user - pass
                    _conString = @"Data Source = " + _sv + ";Initial Catalog = " + Database + ";Integrated Security=SSPI";
                else
                    _conString = @"Data Source=" + _sv + ";Initial Catalog=" + Database + ";User Id=" + _user + ";Password=" +
                                 _pass + "";
                //var conString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\QLSV_TEST.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
                return new SqlConnection(_conString);
                
                // return new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\QLKhachSan.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
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
                Log2File.LogExceptionToFile(ex);
                Console.WriteLine(ex.Message);
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
                Log2File.LogExceptionToFile(ex);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
