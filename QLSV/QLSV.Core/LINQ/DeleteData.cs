using System;
using System.Collections.Generic;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class DeleteData
    {
        private static readonly Connect Conn = new Connect();

        public static void Xoa(IList<int> list, string table)
        {
            try
            {
                foreach (var i in list)
                {
                    Conn.ExcuteQuerySql("DELETE FROM "+table+" WHERE IdSV = " + i + "");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void XoaXepPhong(IList<XepPhong> list)
        {
            try
            {
                foreach (var i in list)
                {
                    Conn.ExcuteQuerySql("DELETE FROM XepPhong WHERE IdSV = " + i.IdSV + " and IdKyThi = "+i.IdKyThi+" and IdPhong = "+i.IdPhong+"");
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void Xoa(string table)
        {
            try
            {
                Conn.ExcuteQuerySql("DELETE FROM " + table);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
