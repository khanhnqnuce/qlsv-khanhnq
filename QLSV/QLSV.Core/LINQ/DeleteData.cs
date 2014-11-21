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
    }
}
