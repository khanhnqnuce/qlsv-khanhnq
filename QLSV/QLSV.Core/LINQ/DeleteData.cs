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
        private static void Xoa(int item, string table)
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
        /// kiểm tra nếu sinh viên đã được chấm thi thì k được xóa
        /// </summary>
        private static bool KtraXoaThongTin(int i, int id)
        {
            try
            {
                switch (i)
                {
                    case 1:
                        var tb1 = Conn.GetTable("select d.MaSV from DIEMTHI d" +
                                                " join SINHVIEN s on d.MaSV = s.MaSV" +
                                                " join LOP l on s.IdLop = l.ID" +
                                                " where l.ID = "+id+"");
                        return tb1.Rows.Count <= 0;
                    case 2:
                        var tb2 = Conn.GetTable("select d.MaSV from DIEMTHI d" +
                                                " join SINHVIEN s on d.MaSV = s.MaSV" +
                                                " join LOP l on s.IdLop = l.ID" +
                                                " join KHOA k on l.IdKhoa = k.ID" +
                                                " where k.ID = " + id + "");
                        return tb2.Rows.Count <= 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }
        
        public static bool KtraXoaThongTin(int i, IList<int> list)
        {
            try
            {
                foreach (var id in list)
                {
                    if(!KtraXoaThongTin(i, id))
                        return false;
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
        /// xóa lớp và sinh viên thuộc lớp
        /// </summary>
        private static void XoaLop(int idlop)
        {
            try
            {
                Conn.ExcuteQuerySql("delete from XEPPHONG where IdSV not in (select MaSV from SINHVIEN where IdLop != "+idlop+")");
                Conn.ExcuteQuerySql("DELETE FROM SINHVIEN WHERE IdLop = " + idlop + "");
                Conn.ExcuteQuerySql("DELETE FROM LOP WHERE ID = " + idlop + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void XoaLop(IList<int> list)
        {
            try
            {
                foreach (var idlop in list)
                {
                    XoaLop(idlop);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
        
        /// <summary>
        /// xóa khoa và các lớp thuộc khoa
        /// </summary>
        private static void XoaKhoa(int idkhoa)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM LOP WHERE IdKhoa = " + idkhoa + "");
                Conn.ExcuteQuerySql("DELETE FROM KHOA WHERE ID = " + idkhoa + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void XoaKhoa(IList<int> list)
        {
            try
            {
                foreach (var idkhoa in list)
                {
                    XoaKhoa(idkhoa);
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
        private static void XoaSv(int item)
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

        public static void XoaSv(IList<int> list)
        {
            try
            {
                foreach (var item in list)
                {
                    XoaSv(item);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
            
        /// <summary>
        /// Xóa năm học
        /// </summary>
        /// <param name="item"></param>
        private static void XoaNamHoc(int item)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM NAMHOC WHERE ID = " + item + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void XoaNamHoc(IList<int> list)
        {
            try
            {
                foreach (var item in list)
                {
                    XoaNamHoc(item);
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
        private static void XoaXepPhong(XepPhong item)
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
        private static void XoaKtPhong(KTPhong item)
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
        /// Xóa bảng điểm thi theo học kỳ và năm học
        /// </summary>
        public static void XoaDiemThi(int nh, string hk)
        {
            try
            {
                Conn.ExcuteQuerySql("delete DIEMTHI where IdNamHoc = " + nh + " and HocKy = '" + hk + "'");
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
