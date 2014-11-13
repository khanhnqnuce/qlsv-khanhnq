using System;
using System.Data;
using QLSV.Core.DataConnection;
using QLSV.Core.Utils.Core;
namespace QLSV.Core.LINQ
{
    public static class LoadData
    {
        private static readonly Connect Conn = new Connect();

        #region Khai báo chuỗi

        const string Str1 = "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa " +
                            "from SinhVien,Lop,Khoa " +
                            "where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID ORDER BY TenSinhVien";
        const string Str2 = "select MaSinhVien from SinhVien ORDER BY TenSinhVien";
        const string Str3 = "select * from Khoa";
        const string Str4 = "select * from Lop";
        const string Str5 = "select ROW_NUMBER() OVER(ORDER BY s.ID) as [STT], s.*,l.IdKhoa " +
                            "From SinhVien s,XepPhong x,PhongThi p,Lop l " +
                            "where s.ID = x.IdSV and x.IdPhong = p.ID and s.IdLop = l.ID";
        const string Str6 = "SELECT s.*,l.MaLop FROM SinhVien s,Lop l WHERE not exists " +
                            "(SELECT x.IdSV FROM XepPhong x " +
                            "WHERE s.ID = x.IdSV) and s.IdLop = l.ID";
        const string Str7 = "select ROW_NUMBER() OVER(ORDER BY s.ID) as [STT],s.ID, s.MaSinhVien, s.HoSinhVien, " +
                            "s.TenSinhVien, s.NgaySinh,l.MaLop, p.TenPhong as [PhongThi], k.ID as [MaKhoa], k.TenKhoa " +
                            "FROM Khoa k, Lop l, SinhVien s, XepPhong x, PhongThi p " +
                            "where k.ID = l.IdKhoa and l.ID = s.IdLop and s.ID = x.IdSV and x.IdPhong = p.ID;";

        private const string Str8 = "SELECT * From PhongThi where SoLuong < SucChua";

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
        /// 8:Trả về bảng phòng thi còn xếp được sinh viên
        public static DataTable Load(int chon)
        {
            try
            {
                var table = new DataTable();
                switch (chon)
                {
                    case 1:
                        table = Conn.getTable(Str1);
                        break;
                    case 2:
                        table = Conn.getTable(Str2);
                        break;
                    case 3:
                        table = Conn.getTable(Str3);
                        break;
                    case 4:
                        table = Conn.getTable(Str4);
                        break;
                    case 5:
                        table = Conn.getTable(Str5);
                        break;
                    case 6:
                        table = Conn.getTable(Str6);
                        break;
                    case 7:
                        table = Conn.getTable(Str7);
                        break;
                    case 8:
                        table = Conn.getTable(Str8);
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
    }
}
