using System;
using System.Collections.Generic;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class InsertData
    {
        private static readonly Connect Conn = new Connect();

        /// <summary>
        /// Thêm 1 người dùng mới
        /// </summary>
        /// <returns></returns>
        public static bool ThemTaiKhoan(Taikhoan item)
        {
            try
            {
                Conn.ExcuteQuerySql("INSERT INTO TAIKHOAN(TaiKhoan,MatKhau,HoTen,Quyen) values(N'" +
                                    item.TaiKhoan + "',N'" + item.MatKhau + "',N'" + item.HoTen + "',N'" +
                                    item.Quyen + "')");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm người dùng mới mới
        /// </summary>
        /// <returns></returns>
        public static bool ThemTaiKhoan(IList<Taikhoan> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemTaiKhoan(item);
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
        /// Thêm 1 khoa quản lý
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool ThemKhoa(Khoa item)
        {
            try
            {
                Conn.ExcuteQuerySql("INSERT INTO KHOA(MaKhoa,TenKhoa) values(N'" + item.MaKhoa + "',N'" + item.TenKhoa + "')");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm nhiều khoa quản lý
        /// </summary>
        /// <returns></returns>
        public static bool ThemKhoa(IList<Khoa> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemKhoa(item);
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
        /// Thêm 1 lớp quản lý
        /// </summary>
        /// <returns></returns>
        public static bool ThemLop(Lop item)
        {
            try
            {
                Conn.ExcuteQuerySql("INSERT INTO LOP(MaLop,IdKhoa,GhiChu) values(N'" +
                            item.MaLop + "'," + item.IdKhoa + ",N'" + item.GhiChu + "')");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm nhiều lớp quản lý
        /// </summary>
        /// <returns></returns>
        public static bool ThemLop(IList<Lop> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemLop(item);
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
        /// Thêm mới 1 sinh viên
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool ThemSinhVien(SinhVien item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into SINHVIEN(MaSV,HoSV,TenSV,NgaySinh,IdLop) values(" +
                                    item.MaSV + ",N'" + item.HoSV + "',N'" + item.TenSV + "','" +
                                    item.NgaySinh + "'," + item.IdLop + ")");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm mới nhiều sinh viên
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool ThemSinhVien(IList<SinhVien> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemSinhVien(item);
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
        /// Thêm 1 kỳ thi
        /// </summary>
        /// <returns></returns>
        public static bool ThemKyThi(Kythi item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into KYTHI(MaKT,TenKT,NgayThi,TGLamBai,TGBatDau,TGKetThuc) values(N'" +
                            item.MaKT + "',N'" + item.TenKT + "','" + item.NgayThi + "'," +
                            item.TGLamBai + ",N'" + item.TGBatDau + "',N'" + item.TGKetThuc + "')");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm nhiều kỳ thi
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool ThemKythi(IList<Kythi> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemKyThi(item);
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
        /// Thêm 1 phòng thi
        /// </summary>
        /// <returns></returns>
        public static bool ThemPhongThi(PhongThi item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into PHONGTHI(TenPhong,SucChua,GhiChu) values(N'" +
                            item.TenPhong + "'," + item.SucChua + ",N'" + item.GhiChu + "')");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm nhiều phòng thi
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool ThemPhongThi(IList<PhongThi> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemPhongThi(item);
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
        /// Thêm 1 đáp án cho mã đề
        /// </summary>
        /// <returns></returns>
        public static bool ThemDapAn(DapAn item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into DapAn(IdKyThi,MaMon,MaDe,CauHoi,Dapan,ThangDiem) values(" +
                            item.IdKyThi + ",'" + item.MaMon + "','" + item.MaDe + "'," +
                            item.CauHoi + ",'" + item.Dapan + "'," + item.ThangDiem + ")");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm nhiều đáp án cho mã đề
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool ThemDapAn(IList<DapAn> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemDapAn(item);
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
        /// Thêm 1 bài làm của sinh viên
        /// </summary>
        /// <returns></returns>
        public static bool ThemBaiLam(BaiLam item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into BAILAM(IdKyThi,MaSV,MaDe,KetQua) values(" +
                item.IdKyThi + "," + item.MaSV + ",N'" + item.MaDe + "',N'" +
                item.KetQua + "')");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm 1 bài làm của sinh viên
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool ThemBaiLam(IList<BaiLam> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemBaiLam(item);
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
        /// Thêm bảng thống kê
        /// </summary>
        /// <returns></returns>
        public static bool ThemThongKe(ThongKe item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into DIEMTHI(MaSV,NamHoc,HocKy,Diem) values(" +
                item.MaSV + ",'" + item.NamHoc + "','" + item.HocKy + "'," +item.Diem + ")");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        public static bool ThemThongKe(IList<ThongKe> list)
        {
            try
            {
                foreach (var item in list)
                {
                    ThemThongKe(item);
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
        /// xếp phòng cho 1 sinh viên
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool XepPhong(XepPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into XepPhong(IdSV,IdPhong,IdKyThi) values(" + item.IdSV + "," + item.IdPhong +
                                    "," + item.IdKyThi + ")");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm vào bảng xếp phòng
        /// </summary>
        /// <param name="list"></param>
        public static bool XepPhong(IList<XepPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    XepPhong(item);
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
        /// Lưu 1 phòng được sử dụng trong kỳ thi
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool KtPhong(KTPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into KT_PHONG(IdKyThi,IdPhong,SiSo) values(" + item.IdKyThi + "," + item.IdPhong +
                                    "," + item.SiSo + ")");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Thêm vào bảng xếp phòng lưu nhiều phòng được sử dụng trong kỳ thi
        /// </summary>
        /// <param name="list"></param>
        public static bool KtPhong(IList<KTPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    KtPhong(item);
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
        /// lưu 1 sv được chọn tham gia thi
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool Chonsinhvien(XepPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("insert into XepPhong(IdSV,IdKyThi) values(" + item.IdSV + "," + item.IdKyThi + ")");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// lưu nhiều sv được chọn tham gia thi
        /// </summary>
        /// <param name="list"></param>
        public static bool Chonsinhvien(IList<XepPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    Chonsinhvien(item);
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
        /// lấy ra bản ghi lớp vừa được thêm khi chưa có lớp
        /// </summary>
        /// <param name="ma">mã lớp</param>
        /// <param name="id">id khoa</param>
        /// <returns></returns>
        public static Lop ThemLop(string ma, int id)
        {
            try
            {
                var sql1 = "insert into Lop(MaLop,idKhoa) values(N'" + ma + "'," + id + ")";
                var sql2 = "select * from Lop where MaLop = N'" + ma + "'";
                Conn.ExcuteQuerySql(sql1);
                var tb = Conn.GetTable(sql2);
                var newLop = new Lop
                {
                    MaLop = tb.Rows[0]["MaLop"].ToString(),
                    ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                    IdKhoa = int.Parse(tb.Rows[0]["IdKhoa"].ToString())
                };
                return newLop;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// lấy ra bản ghi khoa vừa được thêm vào bảng khoa
        /// </summary>
        /// <param name="ma">mã khoa</param>
        /// <returns></returns>
        public static Khoa ThemKhoa(string ma)
        {
            try
            {
                var sql1 = "insert into Khoa(TenKhoa) values(N'" + ma + "')";
                var sql2 = "select * from Khoa where TenKhoa = N'" + ma + "'";
                Conn.ExcuteQuerySql(sql1);
                var tb = Conn.GetTable(sql2);
                var newKhoa = new Khoa
                {
                    ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                    MaKhoa = tb.Rows[0]["MaKhoa"].ToString(),
                    TenKhoa = tb.Rows[0]["TenKhoa"].ToString()
                };
                return newKhoa;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
    }
}
