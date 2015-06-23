﻿using System.Data;
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

        public DataTable tbSinhVien()
        {
            var newProducts = new DataTable("SINHVIEN");
            newProducts.Columns.Add("MaSV", typeof(int));
            newProducts.Columns.Add("HoSV", typeof(string));
            newProducts.Columns.Add("TenSV", typeof(string));
            newProducts.Columns.Add("NgaySinh", typeof(string));
            newProducts.Columns.Add("IdLop", typeof(int));
            return newProducts;
        }
        public DataTable tbXepPhong()
        {
            var newProducts = new DataTable("XEPPHONG");


            newProducts.Columns.Add("IdSV", typeof(int));
            newProducts.Columns.Add("IdPhong", typeof(int));
            newProducts.Columns.Add("IdKyThi", typeof(int));
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

        // Phương thức đổ 1 bảng vào CSDL ...
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
        //update nhiều bản ghi
        public void Bulk_Update(string storename, string tbType, DataTable table)
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
        //Truyền vào 1 datatable gồm nhiều dữ liệu kiểm tra xem đã toàn tại chưa
        public DataTable Bulk_checkData(string storename, string tbType, DataTable table)
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
