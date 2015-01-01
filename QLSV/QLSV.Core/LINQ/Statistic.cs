using System;
using System.Collections.Generic;
using System.Data;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public static class Statistic
    {
        private static readonly Connect Conn = new Connect();

        /// <summary>
        /// Gộp kết quả của các kỳ thi
        /// </summary>
        /// <returns></returns>
        public static DataTable GopKetQua(IList<int> list )
        {
            var tb = new DataTable();
            try
            {
                var str = new string[100];
                var strselect = "select a0.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop,";

                for (var i = 0; i < list.Count; i++)
                {
                    var chuoi = " a" + i + ".DiemThi as [Diem" + (i + 1) + "],";
                    strselect = strselect + chuoi;
                    str[i] = "(select MaSV, DiemThi from BAILAM where DiemThi is not null and IdKyThi = " + list[i] + " ) a"+i;
                }

                var strtong = "(a0.DiemThi";
                for (var i = 1; i < list.Count; i++)
                {
                    strtong = strtong + "+ a" + i + ".DiemThi";

                }
                strselect = strselect + strtong + ") as [TongDiem] ";

                var strjointable = str[0];
                for (var i = 1; i < list.Count; i++)
                {
                    strjointable = strjointable + " join " + str[i] + " on a" + (i - 1) + ".MaSV = a" + i + ".MaSV ";
                }

                const string strjoin = " join SINHVIEN s on a0.MaSV = s.MaSV join LOP l on s.IdLop = l.ID order by s.TenSV";
                var strend = strselect + " FROM " + strjointable + strjoin;
                tb = Conn.GetTable(strend);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return tb;
        }

        public static DataTable GopKetQua1(int id1, int id2)
        {
            var table = new DataTable();
            try
            {
                var str =
                    "select bl.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop, bl.DiemThi as [Diem1], 0 as [Diem2], bl.DiemThi as[TongDiem]" +
                    " from BAILAM bl" +
                    " join SINHVIEN s on bl.MaSV = s.MaSV" +
                    " join LOP l on s.IdLop = l.ID" +
                    " where IdKyThi = " + id1 + " and DiemThi is not null and not exists(" +
                    " select * from (" +
                    " select a.MaSV from (" +
                    " select  MaSV from BAILAM where DiemThi is not null and IdKyThi = " + id1 + ") a join (" +
                    " select MaSV from BAILAM where DiemThi is not null and IdKyThi = " + id2 +
                    " ) b on a.MaSV = b.MaSV) c" +
                    " where bl.MaSV = c.MaSV)";
                table = Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return table;
        }

        public static DataTable GopKetQua2(int id1, int id2)
        {
            var table = new DataTable();
            try
            {
                var str =
                    "select bl.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop, bl.DiemThi as [Diem1], 0 as [Diem2], bl.DiemThi as[TongDiem]" +
                    " from BAILAM bl" +
                    " join SINHVIEN s on bl.MaSV = s.MaSV" +
                    " join LOP l on s.IdLop = l.ID" +
                    " where IdKyThi = " + id2 + " and DiemThi is not null and not exists(" +
                    " select * from (" +
                    " select a.MaSV from (" +
                    " select  MaSV from BAILAM where DiemThi is not null and IdKyThi = " + id1 + ") a join (" +
                    " select MaSV from BAILAM where DiemThi is not null and IdKyThi = " + id2 +
                    " ) b on a.MaSV = b.MaSV) c" +
                    " where bl.MaSV = c.MaSV)";
                table = Conn.GetTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return table;
        }
    }
}
