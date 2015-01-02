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
        private static string Getstr(IList<int> list )
        {
            var tb = new DataTable();
            try
            {
                var str = new string[list.Count];
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
                return strend;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        private static string Getstr1(IList<int> list)
        {
            var tb = new DataTable();
            try
            {
                var str = new string[list.Count];
                const string strselect = "select a0.MaSV";

                for (var i = 0; i < list.Count; i++)
                {
                    str[i] = "(select MaSV, DiemThi from BAILAM where DiemThi is not null and IdKyThi = " + list[i] + " ) a" + i;
                }

                var strjointable = str[0];
                for (var i = 1; i < list.Count; i++)
                {
                    strjointable = strjointable + " join " + str[i] + " on a" + (i - 1) + ".MaSV = a" + i + ".MaSV ";
                }
                
                var strend = strselect + " FROM " + strjointable;
                return strend;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Gộp kết quả của các kỳ thi
        /// </summary>
        /// <returns></returns>
        public static DataTable GopKetQua(IList<int> list)
        {
            var tb = new DataTable();
            try
            {
                tb = Conn.GetTable(Getstr(list));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return tb;
        }

        public static DataTable GopKetQua1(IList<int> list )
        {
            var table = new DataTable();
            try
            {
                var tb= new DataTable[list.Count];
                var str = new string[list.Count];

                for (var i = 0; i < list.Count; i++)
                {
                    str[i] =
                        "select bl.MaSV,s.HoSV,s.TenSV,s.NgaySinh,l.MaLop, bl.DiemThi as [Diem"+(i+1)+"], bl.DiemThi as[TongDiem]" +
                        " from BAILAM bl" +
                        " join SINHVIEN s on bl.MaSV = s.MaSV" +
                        " join LOP l on s.IdLop = l.ID" +
                        " where IdKyThi = " + list[i] + " and DiemThi is not null and not exists( select c.MaSV From (" +
                        Getstr1(list) + " ) c where bl.MaSV = c.MaSV)";
                    tb[i] = Conn.GetTable(str[i]);
                    table.Merge(tb[i]);
                }
                
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
            return table;
        }


        /// <summary>
        /// Thống kê sinh viên theo điểm
        /// </summary>
        /// <returns></returns>
        private static string GetstrThongke(IList<int> list)
        {
            var tb = new DataTable();
            try
            {
                var str = new string[list.Count];
                var strselect = "select ";

                for (var i = 0; i < list.Count; i++)
                {
                    str[i] = "(select MaSV, DiemThi from BAILAM where DiemThi is not null and IdKyThi = " + list[i] + " ) a" + i;
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

                var strend = strselect + " FROM " + strjointable;
                return strend;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
        public static DataTable Thongkediem(int index, IList<int> list)
        {
            try
            {
                string str = null;
                switch (index)
                {
                    case 0:
                        str = "SELECT a.TongDiem FROM (" + GetstrThongke(list) + " ) a WHERE a.TongDiem < 200 ";
                        break;
                    case 1:
                        str =
                            "SELECT a.TongDiem FROM (" + GetstrThongke(list) + " ) a WHERE a.TongDiem > 200 and a.TongDiem < 249 ";
                        break;
                    case 2:
                        str = "SELECT a.TongDiem FROM (" + GetstrThongke(list) + " ) a WHERE a.TongDiem > 250 and a.TongDiem < 300 ";
                        break;
                    case 3:
                        str = "SELECT a.TongDiem FROM (" + GetstrThongke(list) + " ) a WHERE a.TongDiem > 300 and a.TongDiem < 374 ";
                        break;
                    case 4:
                        str = "SELECT a.TongDiem FROM (" + GetstrThongke(list) + " ) a WHERE a.TongDiem > 375 and a.TongDiem < 450 ";
                        break;
                    case 5:
                        str = "SELECT a.TongDiem FROM (" + GetstrThongke(list) + " ) a WHERE a.TongDiem >= 450 ";
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
    }
}
