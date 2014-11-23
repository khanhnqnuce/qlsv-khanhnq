using System;
using System.Collections.Generic;
using System.Linq;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class UpdateData
    {
        private static readonly Connect Conn = new Connect();

        public static void UpdatePhongThi(IList<PhongThi> list)
        {
            try
            {
                foreach (
                    var sql in
                        list.Select(
                            item => "UPDATE PhongThi SET SoLuong = " + item.SoLuong + " WHERE ID = " + item.ID + ""))
                {
                    Conn.ExcuteQuerySql(sql);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void UpdatePhongThi(int id)
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = ((select SoLuong from PhongThi where ID = " + id + ") + 1) where ID = " + id + "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void UpdateGiamPhongThi(IList<PhongThi> list)
        {
            try
            {
                foreach (var phong in list)
                {
                    Conn.ExcuteQuerySql("update PhongThi set SoLuong = ((select SoLuong from PhongThi where TenPhong = '" + phong.TenPhong + "') - " + phong.SoLuong + ") where TenPhong = '" + phong.TenPhong + "'");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void UpdateGiamPhongThi(int i)
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = ((select SoLuong from PhongThi where ID = " +
                                    i + ") - " + 1 + ") where ID = " + i +
                                    "");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void ResetPhongThi()
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = " + 0 + " ");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
        public static void XepPhong(XepPhong hs)
        {
            try
            {
                Conn.ExcuteQuerySql("update XepPhong set IdPhong = " + hs.IdPhong + " WHERE IdSV = "+hs.IdSV+" and IdKythi = "+hs.IdKyThi+"");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
