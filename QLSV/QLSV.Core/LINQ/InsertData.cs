using System;
using System.Collections.Generic;
using System.Linq;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class InsertData
    {
        private static readonly Connect Conn = new Connect();

        public static void ThemSinhVien(IList<SinhVien> list)
        {
            try
            {
                int a = list.Count;
                foreach (var sql in list.Select(item =>
                                "insert into SinhVien(MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,IdLop) values(N'" +
                                item.MaSinhVien + "',N'" + item.HoSinhVien + "',N'" + item.TenSinhVien + "','" +
                                item.NgaySinh + "'," + item.IdLop + ")"))
                {
                    Conn.ExcuteQuerySql(sql);
                }

            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Thêm vào bảng xếp phòng
        /// </summary>
        /// <param name="list"></param>
        public static void XepPhong(IList<XepPhong> list)
        {
            try
            {
                foreach (
                    var sql in
                        list.Select(
                            item => "insert into XepPhong(IdSV,IdPhong,IdKyThi) values(" + item.IdSV + "," + item.IdPhong + ","+ item.IdKyThi +")")
                    )
                {
                    Conn.ExcuteQuerySql(sql);
                }

            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void XepPhong1(XepPhong hs)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into XepPhong(IdSV,IdPhong,IdKyThi) values(" + hs.IdSV + "," + hs.IdPhong +
                                    "," + hs.IdKyThi + ")");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// lấy ra bản ghi lớp vừa được thêm khi chưa có lớp
        /// </summary>
        /// <param name="ma">mã lớp</param>
        /// <param name="id">id khoa</param>
        /// <returns></returns>
        public static Lop ThemLop(string ma, int id)
        {
            try
            {
                var sql1 = "insert into Lop(MaLop,idKhoa) values(N'" + ma + "'," + id + ")";
                var sql2 = "select * from Lop where MaLop = N'" + ma + "'";
                Conn.ExcuteQuerySql(sql1);
                var tb = Conn.getTable(sql2);
                var newLop = new Lop
                {
                    MaLop = tb.Rows[0]["MaLop"].ToString(),
                    ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                    IdKhoa = int.Parse(tb.Rows[0]["IdKhoa"].ToString())
                };
                return newLop;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// lấy ra bản ghi khoa vừa được thêm vào bảng khoa
        /// </summary>
        /// <param name="ma">mã khoa</param>
        /// <returns></returns>
        public static Khoa ThemKhoa(string ma)
        {
            try
            {
                var sql1 = "insert into Khoa(TenKhoa) values(N'" + ma + "')";
                var sql2 = "select * from Khoa where TenKhoa = N'" + ma + "'";
                Conn.ExcuteQuerySql(sql1);
                var tb = Conn.getTable(sql2);
                var newKhoa = new Khoa
                {
                    ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                    MaKhoa = tb.Rows[0]["MaKhoa"].ToString(),
                    TenKhoa = tb.Rows[0]["TenKhoa"].ToString()
                };
                return newKhoa;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
    }
}
