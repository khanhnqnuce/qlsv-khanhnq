using System;
using System.Data;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class SqlBulkCopy
    {
        readonly Connect _connect = new Connect();
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
        public void InsertTable(string tablename, DataTable table)
        {
            using (var connection = _connect.GetConnect())
            {
                connection.Open();
                using (var bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo." + tablename;
                    bulkCopy.WriteToServer(table);
                }
            }
        }

    }
}
