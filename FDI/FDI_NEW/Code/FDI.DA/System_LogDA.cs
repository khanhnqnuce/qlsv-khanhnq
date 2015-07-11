using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA
{
    public partial class System_LogDA : BaseDA
    {
        #region Constructer
        public System_LogDA()
        {
        }

        public System_LogDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_LogDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion
        public List<SystemLogItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Shop_Log_System
                        orderby c.ID
                        select new SystemLogItem
                                   {
                                       ID = c.ID,
                                       UserID = c.UserID
                                   };
            return query.ToList();
        }
        public List<SystemLogItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Log_System
                        orderby c.ID
                        where c.UserID.ToString().StartsWith(keyword)
                        select new SystemLogItem
                                   {
                                       ID = c.ID,
                                       UserID = c.UserID
                                   };
            return query.Take(showLimit).ToList();
        }
        public List<SystemLogItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Shop_Log_System
                        orderby o.ID descending
                        select new SystemLogItem
                                   {
                                       ID = o.ID,
                                       UserID = o.UserID,
                                       AddressIP = o.AddressIP,
                                       ActionType = o.ActionType,
                                       ActionModule = o.ActionModule,
                                       ActionSubModule = o.ActionSubModule,
                                       UrlLink = o.UrlLink,
                                       DateCreated = o.DateCreated,
                                       UserName = o.UserName,
                                       ActionTypeName = o.ActionTypeName
                                   };

            try
            {
                if (!string.IsNullOrEmpty(httpRequest.QueryString["searchname"]))
                {
                    var uid = Guid.Parse(httpRequest["searchname"]);
                    query = query.Where(c => c.UserID == uid);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.LogError(GetType(), ex);
            }
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }
        public List<SystemLogItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Shop_Log_System
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID ascending
                        select new SystemLogItem
                                   {
                                       ID = o.ID,
                                       UserID = o.UserID,
                                       AddressIP = o.AddressIP,
                                       ActionType = o.ActionType,
                                       ActionModule = o.ActionModule,
                                       ActionSubModule = o.ActionSubModule,
                                       UrlLink = o.UrlLink,
                                       DateCreated = o.DateCreated,
                                       UserName = o.UserName,
                                       ActionTypeName = o.ActionTypeName
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }
        #region Check Exits,Update, Delete
        public Shop_Log_System GetById(int logId)
        {
            var query = from c in FDIDB.Shop_Log_System where c.ID == logId select c;
            return query.FirstOrDefault();
        }
        public List<Shop_Log_System> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Log_System where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }
        public bool CheckExits(Shop_Log_System log)
        {
            var query = from c in FDIDB.Shop_Log_System where ((c.UserID == log.UserID) && (c.ID != log.ID)) select c;
            return query.Any();
        }

        public void Add(Shop_Log_System log)
        {
            FDIDB.Shop_Log_System.Add(log);
        }
        public void Delete(Shop_Log_System log)
        {
            FDIDB.Shop_Log_System.Remove(log);
        }
        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
