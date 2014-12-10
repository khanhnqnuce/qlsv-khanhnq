﻿using System;
using System.Data;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;
namespace QLSV.Core.LINQ
{
    public static class LoadData
    {
        private static readonly Connect Conn = new Connect();

        #region Khai báo chuỗi

        const string Str1 = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh," +
                            "s.IdLop,MaLop,l.IdKhoa,TenKhoa " +
                            "FROM SinhVien s,Lop l, Khoa k " +
                            "WHERE s.IdLop = l.ID and l.IdKhoa = k.ID ORDER BY TenSinhVien";
        const string Str2 = "SELECT MaSinhVien FROM SinhVien ORDER BY TenSinhVien";
        const string Str3 = "SELECT * FROM Khoa";
        const string Str4 = "SELECT * FROM Lop";
        const string Str5 = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.*,l.IdKhoa " +
                            "FROM SinhVien s,XepPhong x,PhongThi p,Lop l " +
                            "WHERE s.ID = x.IdSV and x.IdPhong = p.ID and s.IdLop = l.ID";
        const string Str6 = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID, s.MaSinhVien, s.HoSinhVien, " +
                            "s.TenSinhVien, s.NgaySinh, l.MaLop FROM SinhVien s,Lop l WHERE not exists " +
                            "(SELECT x.IdSV FROM XepPhong x " +
                            "WHERE s.ID = x.IdSV) and s.IdLop = l.ID";
        const string Str7 = "SELECT ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID, s.MaSinhVien, s.HoSinhVien, " +
                            "s.TenSinhVien, s.NgaySinh,l.MaLop,p.ID as [IdPhong], p.TenPhong as [PhongThi], " +
                            "k.ID as [MaKhoa], k.TenKhoa, kt.ID as [IdKyThi]" +
                            "FROM Khoa k, Lop l, SinhVien s, XepPhong x, PhongThi p,Kythi kt " +
                            "WHERE k.ID = l.IdKhoa and l.ID = s.IdLop and s.ID = x.IdSV and x.IdPhong = p.ID and x.IdKyThi = kt.ID;";
        private const string Str8 = "SELECT * FROM PhongThi WHERE SoLuong < SucChua";
        private const string Str9 = "SELECT * FROM PhongThi";
        private const string Str10 = "SELECT * FROM Kythi";
        private const string Str11 = "SELECT ROW_NUMBER() OVER(ORDER BY d.ID) as [STT], d.ID, MaMon, MaDe, CauHoi, Dapan, IdKyThi, TenKyThi, NgayThi FROM DapAn d, Kythi k WHERE d.IdKyThi = k.ID";
        private const string Str12 = "SELECT ROW_NUMBER() OVER(ORDER BY ID) as [STT], ID, MaSinhVien, MaDe, KetQua, IdKyThi" +
                                     " FROM BaiLam";

        #endregion

        /// <summary>
        /// 
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
        /// 12: Trả về danh sách bài làm của sinh viên
        public static DataTable Load(int chon)
        {
            try
            {
                var table = new DataTable();
                switch (chon)
                {
                    case 1:
                        table = Conn.GetTable(Str1);
                        break;
                    case 2:
                        table = Conn.GetTable(Str2);
                        break;
                    case 3:
                        table = Conn.GetTable(Str3);
                        break;
                    case 4:
                        table = Conn.GetTable(Str4);
                        break;
                    case 5:
                        table = Conn.GetTable(Str5);
                        break;
                    case 6:
                        table = Conn.GetTable(Str6);
                        break;
                    case 7:
                        table = Conn.GetTable(Str7);
                        break;
                    case 8:
                        table = Conn.GetTable(Str8);
                        break;
                    case 9:
                        table = Conn.GetTable(Str9);
                        break;
                    case 10:
                        table = Conn.GetTable(Str10);
                        break;
                    case 11:
                        table = Conn.GetTable(Str11);
                        break;
                    case 12:
                        table = Conn.GetTable(Str12);
                        break;
                }
                return table;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        public static Taikhoan KiemTraTaiKhoan(string user, string pass)
        {
            try
            {
                var str = "SELECT * FROM Taikhoan WHERE TaiKhoan = N'"+user+"' and MatKhau = N'"+pass+"'";
                var tb = Conn.GetTable(str);
                if (tb != null && tb.Rows.Count>0)
                {
                    var tk = new Taikhoan
                        {
                            ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                            TaiKhoan = tb.Rows[0]["TaiKhoan"].ToString(),
                            MatKhau = tb.Rows[0]["MatKhau"].ToString(),
                            HoTen = tb.Rows[0]["HoTen"].ToString(),
                            Quyen = tb.Rows[0]["Quyen"].ToString(),
                        };
                    return tk;
                }
                else
                {
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
