using System;
using System.Data;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;
namespace QLSV.Core.LINQ
{
    public static class LoadData
    {
        private static readonly Connect Conn = new Connect();

        /// <summary>
        /// Load dữ liệu
        /// </summary>
        /// <param name="chon"></param>
        /// <returns>trả về 1 table</returns>
        /// 1:Trả về bảng thông tin sinh viên gồm lớp khoa
        /// 2:Trả về bảng bao gồm mã sinh viên
        /// 3:Trả về bảng khoa
        /// 4:Trả về bảng lớp
        /// 5:Trả về bảng sinh viên đã được xếp phòng
        /// 6:Trả về bảng sinh viên chưa được xếp phòng
        /// 7:Trả về bảng sinh viên đã được xếp phòng
        /// 8:Trả về bảng phòng thi còn xếp được sinh viên(số lượng , sức chứa)
        /// 9:Trả về bảng phòng thi
        /// 10:Trả về bảng kỳ thi
        /// 11:Trả về bảng đáp án các mã đề
        /// 12:Trả về danh sách bài làm của sinh viên
        /// 13:Kiểm tra những sinh viên có trong danh sách bài thi những không có trong danh sách dự thi 
        /// 14: trả về bảng TAIKHOAN
        /// 15: Khoa
        /// 16: Lớp
        /// 17: Sinh viên
        /// 18: kỳ thi
        /// 19: phòng thi
        /// 20: lấy ra kỳ thi
        /// 21: lấy ra điểm thi cao nhất
        /// 22: Năm học
        public static DataTable Load(int chon)
        {
            var table = new DataTable();
            string str = null;
            try
            {
                switch (chon)
                {
                   case 1:
                        str =
                            "SELECT ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                            "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                            "FROM SINHVIEN s,LOP l, KHOA k " +
                            "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID ORDER BY s.TenSV";
                        break;
                    case 2:
                        str = "SELECT MaSV FROM SINHVIEN ORDER BY TenSV";
                        break;
                    case 3:
                        str = "SELECT * FROM KHOA";
                        break;
                    case 4:
                        str = "SELECT * FROM LOP";
                        break;
                    case 8:
                        str = "select p.TenPhong, p.SucChua, k.SiSo, k.IdPhong from PHONGTHI p left join KT_PHONG k on p.ID = k.IdPhong";
                        break;
                    case 9:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY p.ID) as [STT], p.* FROM PHONGTHI p";
                        break;
                    case 10:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY K.ID) as [STT], K.*, [TT] = case K.TrangThai when 1 then N'Hiển thị' when 0 then N'Ẩn' end  FROM KYTHI K";
                        break;
                    case 14:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY T.ID) as [STT], T.* FROM TAIKHOAN T";
                        break;
                    case 15:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY K.ID) as [STT], K.* FROM KHOA K";
                        break;
                    case 16:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY L.ID) as [STT], L.ID, L.MaLop,L.IdKhoa, L.GhiChu FROM LOP L";
                        break;
                    case 17:
                        str = "SELECT * FROM SINHVIEN";
                        break;
                    case 18:
                        str = "SELECT ID, MaKT, TenKT  FROM KYTHI where TrangThai = 1";
                        break;
                    case 20:
                        str = "SELECT ID, TenKT, 'false' as [Chon] FROM KYTHI where TrangThai = 1 order by ID desc";
                        break;
                    case 210:
                        str = "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, l.IdKhoa,k.TenKhoa ,d.Diem from" +
                              " (select MaSV, Max(Diem) as [Diem] from DIEMTHI group by MaSV) d" +
                              " join SINHVIEN s on d.MaSV = s.MaSV" +
                              " join LOP l on s.IdLop = l.ID" +
                              " join KHOA k on l.IdKhoa = k.ID order by s.TenSV";
                        break;
                    case 111:
                        str = "select ROW_NUMBER() OVER(ORDER BY N.ID) as [STT], N.* from NAMHOC N order by N.ID";
                        break;
                    case 209:
                        str = "select N.* from NAMHOC N order by N.ID DESC";
                        break;
                    case 211:
                        str = " select ROW_NUMBER() OVER(ORDER BY d.MaSV) as [STT], d.MaSV, s.HoSV, s.TenSV,s.NgaySinh, n.NamHoc, d.HocKy,d.Diem from" +
                              " DIEMTHI d"+
                              " join NAMHOC n on d.IdNamHoc = n.ID"+
                              " join SINHVIEN s on d.MaSV = s.MaSV ORDER BY d.MaSV";
                        break;
                }
                table = Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return table;
        }

        /// <summary>
        /// Load dữ liệu theo kỳ thi
        /// </summary>
        /// <param name="chon"></param>
        /// <param name="idKythi"></param>
        /// <returns>trả về 1 table</returns>
        /// 1. Sinh viên đã được xếp phòng
        /// 2.3.4. RptPhongthi - 109_SinhVienDuThi
        /// 5. Sĩ số phòng đã được xếp sinh viên
        /// 6: Trả về danh sách bài làm của sinh viên
        /// 7: Đáp án
        /// 8: Kiểm tra lỗi logic
        /// 9: Nhập thang điểm
        /// 10: In điểm thi
        /// 11. Phòng thi đã được chọn
        /// 12. Sinh viên đã được chọn để thi
        /// 13: Sinh viên chưa được xếp phòng trong bảng KT_PHONG với IdPhong là null
        /// 14: Phong thi có SiSo nhỏ hơn SucChua
        /// 15: Bảng BAILAM với sinh viên đã được chấm thì DiemThi not null
        public static DataTable Load(int chon, int idKythi)
        {
            var table = new DataTable();
            string str = null;
            try
            {
                switch (chon)
                {
                    case 1:
                        str =
                            "SELECT ROW_NUMBER() OVER(ORDER BY p.TenPhong,s.TenSV) as [STT], " +
                            "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, p.TenPhong, k.TenKhoa, l.IdKhoa, x.IdPhong " +
                            "FROM SINHVIEN s " +
                            "join XEPPHONG x on s.MaSV = x.IdSV " +
                            "join PHONGTHI p on x.IdPhong = p.ID " +
                            "join LOP l on s.IdLop = l.ID " +
                            "join KHOA k on l.IdKhoa = k.ID " +
                            "WHERE x.IdKyThi = " + idKythi + " order by p.TenPhong, s.TenSV";
                        break;
                    case 2:
                        str = "SELECT p.TenPhong " +
                              "FROM PHONGTHI p  join XEPPHONG x on p.ID = x.IdPhong " +
                              "where x.IdKyThi = "+idKythi+" ORDER BY p.TenPhong";
                        break;
                    case 3:
                        str = "SELECT TenKT, NgayThi FROM KYTHI WHERE ID = " + idKythi + "";
                        break;
                    case 4:
                        str = "SELECT l.MaLop " +
                              "FROM LOP l join SINHVIEN s on l.ID = s.IdLop " +
                              "join XEPPHONG x on s.MaSV = x.IdSV " +
                              "where x.IdKyThi = "+idKythi+" GROUP BY l.MaLop";
                        break;
                    case 5:
                        str = "SELECT p.TenPhong, p.SucChua, k.SiSo, k.IdPhong " +
                              "FROM KT_PHONG k join PHONGTHI p on k.IdPhong = p.ID " +
                              "WHERE k.IdKyThi = "+idKythi+"";
                        break;
                    case 6:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT]," +
                              " b.IdKyThi, b.MaSV, b.MaDe, b.KetQua " +
                              "FROM BAILAM b " +
                              "WHERE b.IdKyThi = "+idKythi+"";
                        break;
                    case 7:
                        str = "SELECT MaMon, MaDe, CauHoi, Dapan, ThangDiem " +
                              "FROM DAPAN d, KYTHI k " +
                              "WHERE d.IdKyThi = k.ID and d.IdKyThi = " + idKythi + "";
                        break;
                    case 8:
                        str = "select b.MaSV" +
                              " FROM BAILAM b" +
                              " where b.IdKyThi = "+idKythi+"" +
                              " and not exists (select * from XEPPHONG x where b.MaSV = x.IdSV and x.IdKyThi = "+idKythi+") ORDER BY b.MaSV";
                        break;
                    case 9:
                        str =
                            "SELECT MaMon, MaDe, CauHoi, Dapan, ThangDiem " +
                            "FROM DAPAN d, KYTHI k " +
                            "WHERE d.IdKyThi = k.ID and d.IdKyThi = " + idKythi + "";
                        break;
                    case 10:
                        str =
                            "SELECT ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT], b.MaSV, s.HoSV, s.TenSV,s.NgaySinh,l.MaLop, b.DiemThi " +
                            "FROM BAILAM b join SINHVIEN s on b.MaSV = s.MaSV " +
                            "join LOP l on s.IdLop = l.ID " +
                            "WHERE b.IdKyThi = "+idKythi+" ";
                        break;
                    case 11:
                        str =
                            "select ROW_NUMBER() OVER(ORDER BY p.ID) as [STT], " +
                            "p.ID, p.TenPhong,p.SucChua, kt.SiSo " +
                            "From KT_PHONG kt join PHONGTHI p on kt.IdPhong = p.ID " +
                            "where kt.IdKyThi = " + idKythi + "";
                        break;
                    case 12:
                        str =
                            "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                            "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, x.IdPhong " +
                            "FROM SINHVIEN s join XEPPHONG x on s.MaSV = x.IdSV " +
                            "join LOP l on s.IdLop = l.ID " +
                            "WHERE x.IdKyThi = " + idKythi + "";
                        break;
                    case 13:
                        str =
                            "SELECT ROW_NUMBER() OVER(ORDER BY x.IdSV) as [STT],x.IdSV, s.HoSV, s.TenSV,s.NgaySinh, l.MaLop, '' as [PhongThi] " +
                            "FROM XEPPHONG x join SINHVIEN s on x.IdSV = s.MaSV " +
                            "join LOP l on s.IdLop = l.ID " +
                            "where x.IdPhong is null and x.IdKyThi = "+idKythi+"";
                        break;
                    case 14:
                        str =
                            "SELECT k.IdPhong, p.SucChua, k.SiSo, p.TenPhong " +
                            "FROM KT_PHONG k join PHONGTHI p on k.IdPhong = p.ID " +
                            "WHERE k.SiSo < p.SucChua and k.IdKyThi = " + idKythi + "";
                        break;
                    case 15:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                              "s.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,b.DiemThi " +
                              "FROM BAILAM b join SINHVIEN s  on b.MaSV = s.MaSV " +
                              "join LOP l on s.IdLop = l.ID " +
                              "WHERE b.IdKyThi = " + idKythi + " and b.DiemThi is not null";
                        break;
                    case 16:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT], b.* "+
                              "FROM BAILAM b " +
                              "WHERE b.IdKyThi = " + idKythi + "";
                        break;
                    case 209:
                        str =
                            "select ROW_NUMBER() OVER(ORDER BY s.TenSV) as [STT], b.MaSV, s.HoSV,s.TenSV, s.NgaySinh, l.MaLop,b.MaDe, b.DiemThi " +
                            "from BAILAM b join SINHVIEN s on b.MaSV = s.MaSV " +
                            "join LOP l on s.IdLop = l.ID " +
                            "where b.DiemThi is not null and IdKyThi = " + idKythi + " order by TenSV";
                        break;
                }
                table =  Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return table;
        }

        public static DataTable LoadBangdiem(int masv)
        {
            var tb = new DataTable();
            try
            {
                var str = "select ROW_NUMBER() OVER(order by n.NamHoc, d.HocKy) as [STT], n.NamHoc, d.HocKy, d.Diem from DIEMTHI d join NAMHOC n on d.IdNamHoc = n.ID where MaSV = " + masv + " order by n.NamHoc, d.HocKy";
                tb = Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return tb;
        }

        public static DataTable KiemTraTaiKhoan(string user, string pass)
        {
            var tb = new DataTable();
            try
            {
                var str1 = "SELECT * FROM Taikhoan WHERE TaiKhoan = N'" + user + "' and MatKhau = N'" + pass + "'";
                tb = Conn.GetTable(str1);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return tb;
        }
    }
}
