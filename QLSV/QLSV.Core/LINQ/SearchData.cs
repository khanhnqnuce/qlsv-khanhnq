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
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, '' as [PhongThi] FROM SINHVIEN s,LOP l, KHOA k WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " +
                        idkythi + " )and s.IdLop = l.ID and l.IdKhoa = k.ID  and k.ID = " + id + " ORDER BY TenSV";
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
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, '' as [PhongThi] FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE x.IdSV = s.MaSV and x.IdKyThi = " +
                        idkythi + ") and s.IdLop = l.ID  and s.MaSV like '%" + id + "' ORDER BY TenSV";
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
                        "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, '' as [PhongThi] FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " +
                        idkythi + " )and  s.IdLop = l.ID and l.ID = " + id + " ORDER BY TenSV";
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
        public static DataTable Timkiemmade(string made)
        {
            try
            {
                try
                {
                    var str = "SELECT ROW_NUMBER() OVER(ORDER BY ID) as [STT], ID, MaSinhVien, MaDe, KetQua, IdKyThi" +
                              " FROM BaiLam WHERE MaDe = N'" + made + "'";
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
                    var str =
                        "SELECT ROW_NUMBER() OVER(ORDER BY d.ID) as [STT], d.ID, MaMon, MaDe, CauHoi, Dapan, IdKyThi, TenKyThi, NgayThi, ThangDiem FROM DapAn d, Kythi k WHERE d.IdKyThi = k.ID and MaDe = N'" +
                        made + "'";
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
                var str = "SELECT * FROM DapAn WHERE MaDe = N'" + made + "'";
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
        /// <returns></returns>
        public static DataTable Thongkediem(int index)
        {
            try
            {
                string str = null;
                switch (index)
                {
                    case 0:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop " +
                              "and b.DiemThi < 200";
                        break;
                    case 1:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop " +
                              "and b.DiemThi > 200 and b.DiemThi < 249";
                        break;
                    case 2:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop " +
                              "and b.DiemThi > 250 and b.DiemThi < 300";
                        break;
                    case 3:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop " +
                              "and b.DiemThi > 300 and b.DiemThi < 374";
                        break;
                    case 4:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop " +
                              "and b.DiemThi > 375 and b.DiemThi < 450";
                        break;
                    case 5:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop " +
                              "and b.DiemThi > 450";
                        break; 
                    default:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.ID,s.MaSinhVien,s.HoSinhVien,s.TenSinhVien,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM SinhVien s, BaiLam b, Lop l WHERE s.MaSinhVien = b.MaSinhVien and l.ID = s.IdLop";
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
                    return Conn.GetTable("SELECT p.ID, 'false' as [Chon], p.TenPhong, p.SucChua FROM PHONGTHI p WHERE not exists (SELECT p.ID  FROM KT_PHONG k WHERE k.IdPhong = p.ID and k.IdKyThi = "+id+")");
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
