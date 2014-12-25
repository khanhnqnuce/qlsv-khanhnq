using System;
using System.Collections.Generic;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class DeleteData
    {
        private static readonly Connect Conn = new Connect();

        public static void XoaTaiKhoan(IList<int> list)
        {
            try
            {
                
                    foreach (var item in list)
                    {
                        Conn.ExcuteQuerySql("DELETE FROM TAIKHOAN WHERE ID = " + item + " AND Quyen <> 'quantri' ");
                    }
                
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa 1 bản ghi
        /// </summary>
        /// <param name="item"></param>
        /// <param name="table"></param>
        public static void Xoa(int item, string table)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM " + table + " WHERE ID = " + item + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
        
        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="list"></param>
        /// <param name="table"></param>
        public static void Xoa(IList<int> list, string table)
        {
            try
            {
                foreach (var item in list)
                {
                    Xoa(item,table);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa 1 sih viên
        /// </summary>
        /// <param name="item"></param>
        public static void XoaSV(int item)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM SINHVIEN WHERE MaSV = " + item + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa nhiều sinh viên
        /// </summary>
        /// <param name="list"></param>
        public static void XoaSV(IList<int> list)
        {
            try
            {
                foreach (var item in list)
                {
                    XoaSV(item);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa xếp phòng cho 1 sinh viên
        /// </summary>
        /// <param name="list"></param>
        public static void XoaXepPhong(XepPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM XepPhong WHERE IdSV = " + item.IdSV + " and IdKyThi = " + item.IdKyThi + " and IdPhong = " + item.IdPhong + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xếp phòng cho nhiều sinh viên
        /// </summary>
        /// <param name="list"></param>
        public static void XoaXepPhong(IList<XepPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    XoaXepPhong(item);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa theo kỳ thi
        /// </summary>
        /// <param name="table"></param>
        public static void Xoa(string table, int idkythi)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM " + table + " WHERE IdKyThi = " + idkythi + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa theo kỳ thi
        /// </summary>
        /// <param name="table"></param>
        public static void Xoa(string table)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM " + table);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
