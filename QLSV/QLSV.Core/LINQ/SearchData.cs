using System;
using System.Data;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class SearchData
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
                    var str =
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh," +
                            "s.IdLop,MaLop,l.IdKhoa,TenKhoa " +
                            "FROM SinhVien s,Lop l, Khoa k " +
                            "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID  and k.ID = " + id + " ORDER BY TenSinhVien";
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
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh, s.IdLop," +
                        "MaLop,l.IdKhoa,TenKhoa FROM SinhVien s, Lop l, Khoa k " +
                        "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and l.ID = " + id + "ORDER BY TenSinhVien";
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
        public static DataTable Timkiemmade(string made)
        {
            try
            {
                try
                {
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY ID) as [STT], ID, MaSinhVien, MaDe, KetQua, IdKyThi" +
                                 " FROM BaiLam WHERE MaDe = N'"+made+"'";
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
        public static DataTable Timkiemmade1(string made)
        {
            try
            {
                try
                {
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY d.ID) as [STT], d.ID, MaMon, MaDe, CauHoi, Dapan, IdKyThi, TenKyThi, NgayThi, ThangDiem FROM DapAn d, Kythi k WHERE d.IdKyThi = k.ID and MaDe = N'"+made+"'";
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
        public static DataTable Timkiemmade2(string made)
        {
            try
            {
                try
                {
                    var str = "SELECT * FROM DapAn WHERE MaDe = N'" + made + "'";
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
        }
}
