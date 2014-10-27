using System;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.Service
{
    public partial class QlsvSevice
    {
        public static Lop LoadLop(string ma)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    var a = mySession.CreateQuery("FROM Lop as l where l.MaLop = :ma")
                        .SetParameter("ma",ma).List<Lop>();
                    return a.Count>0 ? a[0] : null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
        public static Khoa LoadKhoa(string ma)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    var a = mySession.CreateQuery("FROM Khoa as k where k.TenKhoa = :ma")
                        .SetParameter("ma", ma).List<Khoa>();
                    return a.Count > 0 ? a[0] : null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }
        public static SinhVien LoadSinhVien(string ma)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    var a = mySession.CreateQuery("FROM SinhVien as sv where sv.MaSinhVien = :ma")
                        .SetParameter("ma", ma).List<SinhVien>();
                    return a.Count > 0 ? a[0] : null;
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
