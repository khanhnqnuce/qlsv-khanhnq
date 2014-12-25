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
                            "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT],s.MaSV,s.HoSV,s.TenSV,s.NgaySinh," +
                            "s.IdLop,l.MaLop,l.IdKhoa,k.TenKhoa " +
                            "FROM SINHVIEN s,LOP l, KHOA k " +
                            "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID ORDER BY TenSV";
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
                    case 5:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.*,l.IdKhoa " +
                              "FROM SINHVIEN s,XEPPHONG x,PHONGTHI p,LOP l " +
                              "WHERE s.ID = x.IdSV and x.IdPhong = p.ID and s.IdLop = l.ID";
                        break;
                    case 6:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID, s.MaSinhVien, s.HoSinhVien, " +
                              "s.TenSinhVien, s.NgaySinh, l.MaLop FROM SinhVien s,Lop l WHERE not exists " +
                              "(SELECT x.IdSV FROM XepPhong x " +
                              "WHERE s.ID = x.IdSV) and s.IdLop = l.ID";
                        break;
                    case 7:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID, s.MaSinhVien, s.HoSinhVien, " +
                              "s.TenSinhVien, s.NgaySinh,l.MaLop,p.ID as [IdPhong], p.TenPhong as [PhongThi], " +
                              "k.ID as [MaKhoa], k.TenKhoa, kt.ID as [IdKyThi]" +
                              "FROM Khoa k, Lop l, SinhVien s, XepPhong x, PhongThi p,Kythi kt " +
                              "WHERE k.ID = l.IdKhoa and l.ID = s.IdLop and s.ID = x.IdSV and x.IdPhong = p.ID and x.IdKyThi = kt.ID;";
                        break;
                    case 8:
                        str = "select p.TenPhong, p.SucChua, k.SiSo, k.IdPhong from PHONGTHI p left join KT_PHONG k on p.ID = k.IdPhong";
                        break;
                    case 9:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY p.ID) as [STT], p.* FROM PHONGTHI p";
                        break;
                    case 10:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY K.ID) as [STT], K.* FROM KYTHI K";
                        break;
                    case 11:
                        str =
                            "SELECT ROW_NUMBER() OVER(ORDER BY d.ID) as [STT], d.ID, MaMon, MaDe, CauHoi, Dapan, ThangDiem FROM DAPAN d, KYTHI k WHERE d.IdKyThi = k.ID";
                        break;
                    case 12:
                        str = "SELECT ROW_NUMBER() OVER(ORDER BY b.MaSV) as [STT], b.* FROM BAILAM b";
                        break;
                    case 13:
                        str =
                            "select * from BAILAM b where not exists (select * from SinhVien s where b.MaSinhVien = s.MaSinhVien)";
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
                        str = "SELECT ID, MaKT, TenKT  FROM KYTHI";
                        break;
                    case 19:
                        str = "SELECT ID, 'false' as [Chon], TenPhong, SucChua  FROM PHONGTHI";
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
        /// 1. sinh viên dự thi
        /// 5. si số phòng đã được xếp sinh viên
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
                            "SELECT ROW_NUMBER() OVER(ORDER BY s.MaSV) as [STT], " +
                            "s.MaSV, s.HoSV, s.TenSV, s.NgaySinh, l.MaLop, p.TenPhong, k.TenKhoa, l.IdKhoa, x.IdPhong " +
                            "FROM SINHVIEN s " +
                            "join XEPPHONG x on s.MaSV = x.IdSV " +
                            "join PHONGTHI p on x.IdPhong = p.ID " +
                            "join LOP l on s.IdLop = l.ID " +
                            "join KHOA k on l.IdKhoa = k.ID " +
                            "WHERE x.IdKyThi = "+idKythi+"";
                        break;
                    case 2:
                        str = "SELECT p.TenPhong FROM PHONGTHI p  join XEPPHONG x on p.ID = x.IdPhong where x.IdKyThi = "+idKythi+" ORDER BY p.TenPhong";
                        break;
                    case 3:
                        str = "SELECT TenKT, NgayThi FROM KYTHI WHERE ID = " + idKythi + "";
                        break;
                    case 4:
                        str = "SELECT l.MaLop FROM LOP l join SINHVIEN s on l.ID = s.IdLop join XEPPHONG x " +
                              "on s.MaSV = x.IdSV where x.IdKyThi = "+idKythi+" GROUP BY l.MaLop";
                        break;
                    case 5:
                        str = "SELECT p.TenPhong, p.SucChua, k.SiSo, k.IdPhong FROM KT_PHONG k join PHONGTHI p on k.IdPhong = p.ID WHERE k.IdKyThi = "+idKythi+"";
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

        public static Taikhoan KiemTraTaiKhoan(string user, string pass)
        {
            var tk = new Taikhoan();
            try
            {
                var str1 = "SELECT * FROM Taikhoan WHERE TaiKhoan = N'" + user + "' and MatKhau = N'" + pass + "'";
                var tb = Conn.GetTable(str1);
                if (tb != null && tb.Rows.Count > 0)
                {
                    tk.ID = int.Parse(tb.Rows[0]["ID"].ToString());
                    tk.TaiKhoan = tb.Rows[0]["TaiKhoan"].ToString();
                    tk.MatKhau = tb.Rows[0]["MatKhau"].ToString();
                    tk.HoTen = tb.Rows[0]["HoTen"].ToString();
                    tk.Quyen = tb.Rows[0]["Quyen"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return tk;
        }
    }
}
