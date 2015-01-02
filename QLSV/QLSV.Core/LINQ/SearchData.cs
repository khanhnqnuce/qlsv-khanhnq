using System;
using System.Data;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public static class SearchData
    {
        private static readonly Connect Conn = new Connect();

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
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                              "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                              "FROM SINHVIEN s,LOP l, KHOA k " +
                              "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID  and k.ID = " + id + " ORDER BY TenSV";
                    return Conn.GetTable(str);
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

        public static DataTable Timkiemtheokhoa(int id, int idkythi)
        {
            try
            {
                try
                {
                    var str =
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop " +
                        "FROM SINHVIEN s,LOP l , KHOA k" +
                        "WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " +idkythi + " ) " +
                        "and s.IdLop = l.ID and l.IdKhoa = k.ID  and k.ID = " + id + " ORDER BY TenSV";
                    return Conn.GetTable(str);
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
        /// Tìm kiếm sinh viên theo niên khóa
        /// </summary>
        /// <returns>trả về bảng sinh  viên</returns>
        public static DataTable Timkiemtheokhoa(string id, int idkythi)
        {
            try
            {
                try
                {
                    var str =
                        "SELECT s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop,'false' as [Chon] " +
                        "FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE x.IdSV = s.MaSV and x.IdKyThi = " + idkythi + ") " +
                        "and s.IdLop = l.ID  and s.MaSV like '%" + id + "' ORDER BY TenSV";
                    return Conn.GetTable(str);
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

        public static DataTable Timkiemtheokhoa(string id)
        {
            try
            {
                try
                {
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                              "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                              "FROM SINHVIEN s,LOP l, KHOA k " +
                              "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and s.MaSV like '%" + id + "' ORDER BY TenSV";
                    return Conn.GetTable(str);
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
        /// Tìm kiếm sinh viên theo lớp
        /// </summary>
        /// <returns>trả về bảng thông tin sinh  viên</returns>
        public static DataTable Timkiemtheolop(int id)
        {
            try
            {
                try
                {
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                              "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                              "FROM SINHVIEN s,LOP l, KHOA k " +
                              "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and l.ID = " + id + "ORDER BY TenSV";
                    return Conn.GetTable(str);
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

        public static DataTable Timkiemtheolop(int id, int idkythi)
        {
            try
            {
                try
                {
                    var str =
                        "SELECT 'false' as [Chon],s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop " +
                        "FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " +idkythi + " ) " +
                        "and  s.IdLop = l.ID and l.ID = " + id + " ORDER BY TenSV";
                    return Conn.GetTable(str);
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
        /// Lấy ra lớp quản lý theo mã khoa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable Timkiem(int id)
        {
            try
            {
                try
                {
                    var str = "SELECT * FROM LOP WHERE IdKhoa = " + id + "";
                    return Conn.GetTable(str);
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
        /// Tìm kiếm bài làm của sinh viên theo mã đề
        /// </summary>
        /// <returns>trả về bảng thông tin bài làm</returns>
        public static DataTable Timkiemmade(int idkythi, string made)
        {
            try
            {
                try
                {
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT], b.IdKyThi, b.MaSV, b.MaDe, b.KetQua FROM BAILAM b WHERE b.IdKyThi = " + idkythi + " and MaDe = N'" + made + "'";
                    return Conn.GetTable(str);
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
        /// Tìm kiếm bài làm của sinh viên theo mã đề
        /// </summary>
        /// <returns>trả về bảng thông tin bài làm</returns>
        public static DataTable Timkiemmade1(int idKythi, string made)
        {
            try
            {
                try
                {
                    var str =
                        "SELECT MaMon, MaDe, CauHoi, Dapan, ThangDiem FROM DAPAN d, KYTHI k WHERE d.IdKyThi = k.ID and d.IdKyThi = " + idKythi + " and MaDe = '" + made + "'";
                    return Conn.GetTable(str);
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
        /// Lấy đáp án theo ma đe
        /// </summary>
        /// <returns>trả về bảng thông tin bài làm</returns>
        public static DataTable Timkiemmade2(string made, int idkythi)
        {
            try
            {
                var str = "SELECT * FROM DapAn WHERE MaDe = N'" + made + "' and IdKyThi = " + idkythi + " order by CauHoi";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Thống kê sinh viên theo điểm
        /// </summary>
        /// <param name="index"></param>
        /// <param name="idkythi"></param>
        /// <returns></returns>
        public static DataTable Thongkediem(int index, int idkythi)
        {
            try
            {
                string str = null;
                switch (index)
                {
                    case 0:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "WHERE b.DiemThi < 200 and b.IdKyThi = "+idkythi+" ";
                        break;
                    case 1:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi > 200 and b.DiemThi < 249 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 2:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi > 250 and b.DiemThi < 300 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 3:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi > 300 and b.DiemThi < 374 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 4:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi > 375 and b.DiemThi < 450 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 5:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi >= 450 and b.IdKyThi = " + idkythi + "";
                        break;
                }
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// lấy ra phòng thi chưa dùng tròng kỳ thi
        /// </summary>
        /// <returns></returns>
        public static DataTable LoadPhong(int id)
        {
            try
            {
                try
                {
                    return Conn.GetTable("SELECT p.ID, p.TenPhong, p.SucChua, 'false' as [Chon] FROM PHONGTHI p WHERE not exists (SELECT p.ID  FROM KT_PHONG k WHERE k.IdPhong = p.ID and k.IdKyThi = " + id + ")");
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
    }
}
