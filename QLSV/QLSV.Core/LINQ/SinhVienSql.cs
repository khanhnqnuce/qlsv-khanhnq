using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class SinhVienSql
    {
        private static readonly Connect Conn = new Connect();

        #region Load dữ liệu từ CSDL

        /// <summary>
        /// Lấy ra thông tin của tất cả sinh viên
        /// </summary>
        /// <returns>trả về 1 bảng</returns>
        public static DataTable LoadSinhVien()
        {
            try
            {
                const string str = "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa from SinhVien,Lop,Khoa where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID ORDER BY TenSinhVien";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy ra danh sách mã sinh biên
        /// </summary>
        /// <returns></returns>
        public static DataTable LoadMaSinhVien()
        {
            try
            {
                const string str = "select MaSinhVien from SinhVien ORDER BY TenSinhVien";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy ra thông tin các khoa
        /// </summary>
        /// <returns>trả về table khoa</returns>
        public static DataTable LoadKhoa()
        {
            try
            {
                const string str = "select * from Khoa";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy ra thông tin các khoa
        /// </summary>
        /// <returns>trả về table khoa</returns>
        public static DataTable LoadLop()
        {
            try
            {
                const string str = "select * from Lop";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// lấy ra danh sách lớp thuộc khoa
        /// </summary>
        /// <returns>trả về bảng thông tin lop</returns>
        public static DataTable LoadLopTheoKhoa(int id)
        {
            try
            {
                try
                {
                    var str = "select * from Lop where IdKhoa = " + id;
                    return Conn.getTable(str);
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Load tổng sức chứa phòng thì
        /// </summary>
        /// <param name="ma"></param>
        /// <returns>tổng</returns>
        public static int Loadsoluong(string ma)
        {
            try
            {
                const string sql = "SELECT SUM([SucChua])[soluong] FROM PhongThi";
                var tb = Conn.getTable(sql);
                return int.Parse(tb.Rows[0]["soluong"].ToString());
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return 0;
            }
        }

        public static DataTable LoadSvChuaXepphong()
        {
            try
            {
                const string str = "select Row_Number() OVER (ORDER BY s.ID) AS [STT], s.* from SinhVien s where not exists (select x.IdSV from XepPhong x where s.ID = x.IdSV)";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        #endregion

        #region Tìm kiếm

        /// <summary>
        /// Tìm kiếm sinh viên theo khoa
        /// </summary>
        /// <returns>trả về bảng sinh  viên</returns>
        public static DataTable Timkiemtheokhoa(int id)
        {
            try
            {
                try
                {
                    var str =
                        "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa from SinhVien,Lop,Khoa where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID and Khoa.ID = " +
                        id + " ORDER BY TenSinhVien";
                    return Conn.getTable(str);
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo Lop
        /// </summary>
        /// <returns>trả về bảng thông tin sinh  viên</returns>
        public static DataTable Timkiemtheolop(int id)
        {
            try
            {
                try
                {
                    var str =
                        "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa from SinhVien,Lop,Khoa where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID and Lop.ID = " +
                        id + "ORDER BY TenSinhVien";
                    return Conn.getTable(str);
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        #endregion

        #region Thêm và Sửa dữ liệu

        /// <summary>
        /// Thêm mới sinh viên
        /// </summary>
        /// <param name="list">danh sách sinh viên</param>
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
                            item => "insert into XepPhong(IdSV,IdPhong) values(" + item.IdSV + "," + item.IdPhong + ")")
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

        public static void UpdatePhongThi(IList<PhongThi> list)
        {
            try
            {
                foreach (
                    var sql in
                        list.Select(
                            item => "UPDATE PhongThi SET SoLuong = " + item.SoLuong + " WHERE ID = " + item.ID + ""))
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

        public static void SuaSinhVien(SinhVien hs)
        {
            try
            {
                var sql = "UPDATE SinhVien SET HoSinhVien = N'" + hs.HoSinhVien + "', TenSinhVien = N'" + hs.TenSinhVien +
                          "', NgaySinh = N'" + hs.NgaySinh + "', IdLop = " + hs.IdLop + " WHERE MaSinhVien = N'" +
                          hs.MaSinhVien + "'";
                Conn.ExcuteQuerySql(sql);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion
    }
}
