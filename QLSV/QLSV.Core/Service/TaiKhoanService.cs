using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using QLSV.Core.Domain;
using QLSV.Core.Utils;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.Service
{
    public partial class QuanlysinhvienSevice
    {

        public static IList<Taikhoan> KiemTraTaiKhoan(string id, string pass)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    var a = mySession.CreateQuery("FROM Taikhoan tk where tk.TaiKhoan = :id and tk.MatKhau = :pass")
                        .SetParameter("id", id)
                        .SetParameter("pass", pass)
                        .List<Taikhoan>();
                    var list = a;
                    return list;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        public static void SuaTaiKhoan(IEnumerable<Taikhoan> list)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    foreach (var a in list.Select(item => mySession.CreateQuery(
                        "Update Taikhoan set HoTen = :b, Quyen = :c where ID = :id")
                        .SetParameter("b", item.HoTen)
                        .SetParameter("c", item.Quyen)
                        .SetParameter("id", item.ID)
                        .ExecuteUpdate())){}
                    mySession.Flush();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void SuaMatKhau(IEnumerable<Taikhoan> list)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    foreach (var a in list.Select(item => mySession.CreateQuery(
                        "Update Taikhoan set MatKhau = :a where ID = :id")
                        .SetParameter("a", item.MatKhau)
                        .SetParameter("id", item.ID)
                        .ExecuteUpdate())){}
                    mySession.Flush();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void XoaTaiKhoan()
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    mySession.CreateQuery("DELETE FROM Taikhoan tk WHERE tk.Quyen <> :a")
                        .SetParameter("a", "quantri")
                        .ExecuteUpdate();
                    mySession.Flush();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
