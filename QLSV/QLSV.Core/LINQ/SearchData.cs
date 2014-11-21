using System;
using System.Data;
using QLSV.Core.DataConnection;
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
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh, s.IdLop," +
                        "MaLop,l.IdKhoa,TenKhoa FROM SinhVien s, Lop l, Khoa k " +
                        "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and l.ID = " + id + "ORDER BY TenSinhVien";
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
    }
}
