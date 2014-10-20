using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using QLSV.Core.Domain;
using QLSV.Core.Utils;

namespace QLSV.Core.Service
{
    public partial class QuanlysinhvienSevice
    {
        static ISessionFactory _mySecsionFactory;

        private static ISession OpenSession()
        {
            if (_mySecsionFactory == null)
            {
                var configuration = new Configuration();
                configuration.AddAssembly(Assembly.GetCallingAssembly());

                _mySecsionFactory = configuration.BuildSessionFactory();
            }
            return _mySecsionFactory.OpenSession();
        }

        public static IEnumerable<T> Load<T>() where T : class
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    var criteria = mySession.CreateCriteria<T>();
                    var list = criteria.List<T>();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        public static void Them<T>(IEnumerable<T> list)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    foreach (var item in list)
                    {
                        mySession.Save(item);
                    }
                    mySession.Flush();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void Sua<T>(IEnumerable<T> list)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    foreach (var item in list)
                    {
                        mySession.Update(item);
                    }
                    mySession.Flush();
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
                using (var mySession = OpenSession())
                {
                    mySession.CreateQuery("DELETE FROM "+table)
                        .ExecuteUpdate();
                    mySession.Flush();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public static void Xoa(IEnumerable<int> list, string table)
        {
            try
            {
                using (var mySession = OpenSession())
                {
                    foreach (var item in list)
                    {
                        mySession.CreateQuery("DELETE FROM "+table+" tb WHERE tb.ID = :id")
                            .SetParameter("id", item)
                            .ExecuteUpdate();
                        mySession.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
