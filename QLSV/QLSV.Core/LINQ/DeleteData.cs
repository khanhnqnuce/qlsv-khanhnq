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
        /// Xóa 1 bản ghi trong bảng theo ID
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
        /// xóa nhiều bản ghi trong bảng theo ID
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
        /// Xóa bản SINHVIEN theo mã sinh viên
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
        /// Xóa bản SINHVIEN theo mã sinh viên
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
        /// xóa bảng XEPPHONG theo kỳ thi và mã sinh viên
        /// </summary>
        public static void XoaXepPhong(XepPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM XEPPHONG WHERE IdKyThi = " + item.IdKyThi + " and IdSV = " + item.IdSV + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa bảng XEPPHONG theo kỳ thi và mã phòng
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
        /// xóa bảng KT_PHONG theo kỳ thi và mã phòng
        /// </summary>
        public static void XoaKtPhong(KTPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM KT_PHONG WHERE IdKyThi = " + item.IdKyThi + " and IdPhong = " + item.IdPhong + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa bảng KT_PHONG theo kỳ thi và mã phòng
        /// </summary>
        /// <param name="list"></param>
        public static void XoaKtPhong(IList<KTPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    XoaKtPhong(item);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// xóa 1 bảng theo kỳ thi
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
        /// Xóa toàn bộ bảng
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

        /// <summary>
        /// Xóa toàn bộ bảng tai khoản
        /// </summary>
        public static void XoaTaiKhoan()
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM TAIKHOAN WHERE Quyen <> N'quantri'");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
