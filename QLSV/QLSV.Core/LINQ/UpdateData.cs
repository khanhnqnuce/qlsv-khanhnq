using System;
using System.Collections.Generic;
using System.Linq;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class UpdateData
    {
        private static readonly Connect Conn = new Connect();

        /// <summary>
        /// Update lại sô lượng sĩ số phòng thi khi đã xếp phòng cho sinh viên
        /// </summary>
        /// <param name="list">Danh sách phòng</param>
        /// <returns>true</returns>
        public static bool UpdatePhongThi(IList<PhongThi> list)
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
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa lại đáp án đúng của câu hỏi
        /// </summary>
        /// <param name="list">danh sách đối tượng đáp án được sửa</param>
        /// <returns>true</returns>
        public static bool UpdateDapAn(IList<DapAn> list)
        {
            try
            {
                foreach (
                    var sql in
                        list.Select(
                            item => "UPDATE DapAn SET MaMon = N'" + item.MaMon + "',MaDe = N'" + item.MaDe + "'," +
                                    "CauHoi = N'" + item.CauHoi + "',Dapan = N'" + item.Dapan + "' WHERE ID = " + item.ID + ""))
                {
                    Conn.ExcuteQuerySql(sql);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// tăng số lượng sinh viên lên 1 khi xếp 1 sinh viên vào phòng
        /// </summary>
        /// <param name="id">ID của phòng thi</param>
        /// <returns>true nếu thành công</returns>
        public static bool UpdatePhongThi(int id)
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = ((select SoLuong from PhongThi where ID = " + id + ") + 1) where ID = " + id + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// update lại số lượng sinh viên khi chuyển phòng thi
        /// </summary>
        /// <param name="list">danh sách phòng thì có sinh viên xếp sang phòng khác</param>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateGiamPhongThi(IList<PhongThi> list)
        {
            try
            {
                foreach (var phong in list)
                {
                    Conn.ExcuteQuerySql("update PhongThi set SoLuong = ((select SoLuong from PhongThi where TenPhong = '" + phong.TenPhong + "') - " + phong.SoLuong + ") where TenPhong = '" + phong.TenPhong + "'");
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// giảm số lượng sinh viên lên 1 khi xếp 1 sinh viên vào phòng
        /// </summary>
        /// <param name="id">ID của phòng thi</param>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateGiamPhongThi(int i)
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = ((select SoLuong from PhongThi where ID = " +
                                    i + ") - " + 1 + ") where ID = " + i +
                                    "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sau khi xóa tất cả sinh viên đã xếp phòng thì sĩ số phòng giảm về 0
        /// </summary>
        /// <returns>true</returns>
        public static bool ResetPhongThi()
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = " + 0 + " ");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Xếp phòng cho tường sinh viên
        /// </summary>
        /// <param name="hs">đối tượng sinh viên cần xếp</param>
        /// <returns>true</returns>
        public static bool XepPhong(XepPhong hs)
        {
            try
            {
                Conn.ExcuteQuerySql("update XepPhong set IdPhong = " + hs.IdPhong + " WHERE IdSV = "+hs.IdSV+" and IdKythi = "+hs.IdKyThi+"");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }
    }
}
