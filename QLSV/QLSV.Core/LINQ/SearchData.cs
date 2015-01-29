using System;
using System.Data;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public static class SearchData
    {
        private static readonly Connect Conn = new Connect();

        #region Quản lý điểm

        public static DataTable Quanlydiem(int i,int idkhoa = 0 ,int idlop = 0 ,int idnamhoc = 0, string idhocky = null)
        {
            var table = new DataTable();
            try
            {
                string str;
                switch (i)
                {
                    case 1:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh, d.IdNamHoc,n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID where l.ID = " + idlop + " and d.IdNamHoc = " + idnamhoc + " ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 2:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " join KHOA k on l.IdKhoa = k.ID where k.ID = " + idkhoa + " and d.IdNamHoc = " + idnamhoc + " ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 3:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID where d.IdNamHoc = " + idnamhoc + " ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 4:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID where l.ID = " + idlop + " and d.HocKy = '"+idhocky+"' ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 5:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID " +
                            " join KHOA k on l.IdKhoa = k.ID where k.ID = " + idkhoa + " and d.HocKy = '" + idhocky + "' ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 6:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID where d.HocKy = '" + idhocky + "' ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 7:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " where l.ID = " + idlop + "" +
                            " and d.IdNamHoc = " + idnamhoc + "" +
                            " and  d.HocKy = '" + idhocky + "'" +
                            " ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 8:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " join KHOA k on l.IdKhoa = k.ID" +
                            " where k.ID = " + idkhoa + "" +
                            " and d.IdNamHoc = " + idnamhoc + "" +
                            " and  d.HocKy = '" + idhocky + "'" +
                            " ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                    case 9:
                        str =
                            " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                            " DIEMTHI d" +
                            " join NAMHOC n on d.IdNamHoc = n.ID" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " join KHOA k on l.IdKhoa = k.ID" +
                            " where d.IdNamHoc = " + idnamhoc +
                            " and  d.HocKy = '" + idhocky + "'" +
                            " ORDER BY d.MaSV";
                        table = Conn.GetTable(str);
                        break;
                }

            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return table;
        }

        #endregion

        /// <summary>
        /// Tìm kiếm sv theo khoa mục quản lý sinh viên
        /// </summary>
        public static DataTable Timkiemtheokhoa(int id)
        {
            try
            {
                var str = "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                          "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                          "FROM SINHVIEN s,LOP l, KHOA k " +
                          "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and k.ID = " + id + "ORDER BY TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Tìm kiếm sv theo khoa mục chọn sinh viên
        /// </summary>
        public static DataTable Timkiemtheokhoa(int id, int idkythi)
        {
            try
            {
                var str = "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, 'false' as [Chon] " +
                          "FROM SINHVIEN s,LOP l , KHOA k WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " +
                          idkythi + " ) " +
                          "and  s.IdLop = l.ID and l.IdKhoa = k.ID and K.ID = " + id + " ORDER BY s.TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// tìm kiếm sv theo khoa mục quản lý điểm
        /// </summary>
        public static DataTable Timkiemtheokhoa1(int id)
        {
            try
            {
                var str =
                    " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                    " DIEMTHI d" +
                    " join NAMHOC n on d.IdNamHoc = n.ID" +
                    " join SINHVIEN s on d.MaSV = s.MaSV" +
                    " join LOP l on s.IdLop = l.ID" +
                    " join KHOA k on l.IdKhoa = k.ID where k.ID = " + id + " ORDER BY d.MaSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// tìm kiếm sv theo khoa mục điểm tích lũy
        /// </summary>
        public static DataTable Timkiemtheokhoa2(int id)
        {
            try
            {
                var str =
                    "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                    " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                    " join SINHVIEN s on d.MaSV = s.MaSV" +
                    " join LOP l on s.IdLop = l.ID" +
                    " join KHOA k on l.IdKhoa = k.ID where k.ID= " + id + " order by s.TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// tìm kiếm sv theo khoa mục ds phòng thi
        /// </summary>
        public static DataTable Timkiemtheokhoa3(int id, int idkythi)
        {
            try
            {
                var str =
                    "SELECT ROW_NUMBER() OVER(ORDER BY p.TenPhong,s.TenSV) as [STT], " +
                        "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, p.TenPhong, k.TenKhoa, l.IdKhoa, x.IdPhong " +
                        "FROM SINHVIEN s " +
                        "join XEPPHONG x on s.MaSV = x.IdSV " +
                        "join PHONGTHI p on x.IdPhong = p.ID " +
                        "join LOP l on s.IdLop = l.ID " +
                        "join KHOA k on l.IdKhoa = k.ID " +
                        "WHERE x.IdKyThi = " + idkythi + " and k.ID = " + id + " order by p.TenPhong, s.TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
        
        /// <summary>
        /// tất cả sinh viên mục chọn sinh viên
        /// </summary>
        public static DataTable Tatcacackhoa(int idkythi)
        {
            try
            {
                var str = "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, 'false' as [Chon] " +
                          "FROM SINHVIEN s,LOP l , KHOA k " +
                          "WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " + idkythi + " ) " +
                          "and  s.IdLop = l.ID and l.IdKhoa = k.ID ORDER BY s.TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
        
        /// <summary>
        /// Tìm kiếm sv theo niên khóa mục quản lý sinh viên
        /// </summary>
        /// <returns>trả về bảng sinh  viên</returns>
        public static DataTable Timkiemnienkhoa(int i,string id, int idkythi,int idkhoa,int idlop)
        {
            try
            {
                string str = null;
                switch (i)
                {
                    case 1:
                        str =
                    "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop,'false' as [Chon] " +
                    "FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE x.IdSV = s.MaSV and x.IdKyThi = " + idkythi + ") " +
                    "and s.IdLop = l.ID  and l.ID = "+idlop+" and s.MaSV like '%" + id + "' ORDER BY TenSV";
                        break;
                    case 2:
                        str =
                     "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop,'false' as [Chon] " +
                     "FROM SINHVIEN s,LOP l, KHOA k WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE x.IdSV = s.MaSV and x.IdKyThi = " + idkythi + ") " +
                     "and s.IdLop = l.ID and l.IdKhoa = k.ID  and k.ID = " + idkhoa + " and s.MaSV like '%" + id + "' ORDER BY TenSV";
                        break;
                    case 3:
                        str =
                    "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop,'false' as [Chon] " +
                    "FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE x.IdSV = s.MaSV and x.IdKyThi = " + idkythi + ") " +
                    "and s.IdLop = l.ID  and s.MaSV like '%" + id + "' ORDER BY TenSV";
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

        public static DataTable Timkiemnienkhoa3(int i, string id, int idkhoa, int idlop)
        {
            try
            {
                string str = null;
                switch (i)
                {
                    case 1:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY l.MaLop, s.TenSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                          "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                          "FROM SINHVIEN s,LOP l, KHOA k " +
                          "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and l.ID = "+idlop+" and s.MaSV like '%" + id + "' ORDER BY l.MaLop, s.TenSV";
                        break;
                    case 2:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY l.MaLop, s.TenSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                          "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                          "FROM SINHVIEN s,LOP l, KHOA k " +
                          "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and k.ID = "+idkhoa+" and s.MaSV like '%" + id + "' ORDER BY l.MaLop, s.TenSV";
                        break;
                    case 3:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY l.MaLop, s.TenSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                          "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                          "FROM SINHVIEN s,LOP l, KHOA k " +
                          "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and s.MaSV like '%" + id + "' ORDER BY l.MaLop, s.TenSV";
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
        /// Tìm kiếm sv theo niên khóa mục điểm tích lũy
        /// </summary>
        public static DataTable Timkiemnienkhoa2(int i,string id, int idkhoa,int idlop)
        {
            try
            {
                string str = null;
                switch (i)
                {
                    case 1:
                        str =
                            "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                            " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " join KHOA k on l.IdKhoa = k.ID where l.ID = "+idlop+" and s.MaSV like '%" + id + "' order by s.TenSV";
                        break;
                    case 2:
                        str =
                            "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                            " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " join KHOA k on l.IdKhoa = k.ID where k.ID = "+idkhoa+" and s.MaSV like '%" + id + "' order by s.TenSV";
                        break;
                    case 3:
                        str =
                            "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                            " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                            " join SINHVIEN s on d.MaSV = s.MaSV" +
                            " join LOP l on s.IdLop = l.ID" +
                            " join KHOA k on l.IdKhoa = k.ID where s.MaSV like '%" + id + "' order by s.TenSV";
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
        /// Tìm kiếm sv theo lớp mục quản lý sv
        /// </summary>
        /// <returns>trả về bảng thông tin sinh  viên</returns>
        public static DataTable Timkiemtheolop(int id)
        {
            try
            {
                var str = "SELECT ROW_NUMBER() OVER(ORDER BY l.MaLop,s.TenSV DESC) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                          "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                          "FROM SINHVIEN s,LOP l, KHOA k " +
                          "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID and l.ID = " + id + "ORDER BY l.MaLop,s.TenSV DESC";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// tìm kiếm sv theo lớp mục chọn sinh viên
        /// </summary>
        public static DataTable Timkiemtheolop(int id, int idkythi)
        {
            try
            {
                var str =
                    "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, 'false' as [Chon] " +
                    "FROM SINHVIEN s,LOP l WHERE not exists (SELECT x.IdSV FROM XEPPHONG x WHERE  x.IdSV = s.MaSV and x.IdKyThi = " + idkythi + " ) " +
                    "and  s.IdLop = l.ID and l.ID = " + id + " ORDER BY s.TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
        
        /// <summary>
        /// tìm kiếm sv theo lớp mục quản lý điểm
        /// </summary>
        public static DataTable Timkiemtheolop1(int id)
        {
            try
            {
                var str =
                    " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,l.MaLop,s.NgaySinh,d.IdNamHoc, n.NamHoc, d.HocKy,d.Diem from" +
                    " DIEMTHI d" +
                    " join NAMHOC n on d.IdNamHoc = n.ID" +
                    " join SINHVIEN s on d.MaSV = s.MaSV" +
                    " join LOP l on s.IdLop = l.ID where l.ID = " + id + " ORDER BY d.MaSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// tìm kiếm sv theo lớp mục điểm tích lũy
        /// </summary>
        public static DataTable Timkiemtheolop2(int id)
        {
            try
            {
                var str =
                    "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                    " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                    " join SINHVIEN s on d.MaSV = s.MaSV" +
                    " join LOP l on s.IdLop = l.ID" +
                    " join KHOA k on l.IdKhoa = k.ID where l.ID = " + id + " order by s.TenSV";
                return Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }

        }

        /// <summary>
        /// tìm kiếm sv theo lớp mục ds phòng thi
        /// </summary>
        public static DataTable Timkiemtheolop3(int id, int idkythi)
        {
            try
            {
                var str =
                    "SELECT ROW_NUMBER() OVER(ORDER BY p.TenPhong,s.TenSV) as [STT], " +
                        "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, p.TenPhong, k.TenKhoa, l.IdKhoa, x.IdPhong " +
                        "FROM SINHVIEN s " +
                        "join XEPPHONG x on s.MaSV = x.IdSV " +
                        "join PHONGTHI p on x.IdPhong = p.ID " +
                        "join LOP l on s.IdLop = l.ID " +
                        "join KHOA k on l.IdKhoa = k.ID " +
                        "WHERE x.IdKyThi = " + idkythi + " and l.ID = " + id + " order by p.TenPhong, s.TenSV";
                return Conn.GetTable(str);
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
        public static DataTable LoadCboLop(int id)
        {
            try
            {
                var str = "SELECT ROW_NUMBER() OVER(ORDER BY  L.MaLop) as [STT], L.*, k.TenKhoa FROM LOP L" +
                          " join KHOA k on L.IdKhoa = k.id where IdKhoa = " + id + " order by L.MaLop";
                   // "SELECT * FROM LOP WHERE IdKhoa = " + id + "";
                return Conn.GetTable(str);
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
                var str = "SELECT ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT], b.IdKyThi, b.MaSV, b.MaDe, b.KetQua FROM BAILAM b WHERE b.IdKyThi = " + idkythi + " and MaDe = N'" + made + "'";
                return Conn.GetTable(str);
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
                    case 1:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "WHERE b.DiemThi < 200 and b.IdKyThi = "+idkythi+" ";
                        break;
                    case 2:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi >= 200 and b.DiemThi < 250 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 3:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi >= 250 and b.DiemThi < 300 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 4:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi >= 300 and b.DiemThi < 375 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 5:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "and b.DiemThi >= 375 and b.DiemThi < 450 and b.IdKyThi = " + idkythi + "";
                        break;
                    case 6:
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

        public static DataTable Thongkediem(int index)
        {
            try
            {
                string str = null;
                switch (index)
                {
                    case 1:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem < 200 order by s.TenSV";
                        break;
                    case 2:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem >= 200 and d.Diem < 250 order by s.TenSV";
                        break;
                    case 3:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem < 250 order by s.TenSV";
                        break;
                    case 4:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem >= 250 and d.Diem < 300 order by s.TenSV";
                        break;
                    case 5:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem < 300 order by s.TenSV";
                        break;
                    case 6:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem >= 300 and d.Diem < 375 order by s.TenSV";
                        break;
                    case 7:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem < 375 order by s.TenSV";
                        break;
                    case 8:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem >= 375 and d.Diem < 450 order by s.TenSV";
                        break;
                    case 9:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem < 450 order by s.TenSV";
                        break;
                    case 10:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem >= 450 order by s.TenSV";
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
        
        public static DataTable Thongkediem(int index, string s)
        {
            try
            {
                string str = null;
                switch (index)
                {
                    case 1:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem < 200 order by s.TenSV";
                        break;
                    case 2:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem >= 200 and d.Diem < 250 order by s.TenSV";
                        break;
                    case 3:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem < 250 order by s.TenSV";
                        break;
                    case 4:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem >= 250 and d.Diem < 300 order by s.TenSV";
                        break;
                    case 5:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem < 300 order by s.TenSV";
                        break;
                    case 6:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem >= 300 and d.Diem < 375 order by s.TenSV";
                        break;
                    case 7:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem < 375 order by s.TenSV";
                        break;
                    case 8:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem >= 375 and d.Diem < 450 order by s.TenSV";
                        break;
                    case 9:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where s.MaSV like '%" + s + "' and d.Diem < 450 order by s.TenSV";
                        break;
                    case 10:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID" +
                              " where d.Diem >= 450 order by s.TenSV";
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
                return Conn.GetTable("SELECT p.ID, p.TenPhong, p.SucChua, 'false' as [Chon] FROM PHONGTHI p WHERE not exists (SELECT p.ID  FROM KT_PHONG k WHERE k.IdPhong = p.ID and k.IdKyThi = " + id + ")");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        public static DataTable KtraChamThi(int idkythi)
        {
            try
            {
                return Conn.GetTable("select ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT], b.* from BAILAM b where b.IdKyThi = " + idkythi + " and b.DiemThi is not null");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
    }
}
