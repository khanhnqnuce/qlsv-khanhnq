using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public class HtmlSettingDA : BaseDA
    {
        #region Constructer
        public HtmlSettingDA()
        {
        }

        public HtmlSettingDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public HtmlSettingDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

     
        public List<HtmlSetting> GetListAll()
        {
            var query = from c in FDIDB.HtmlSettings
                        select c;
            return query.ToList();
        }

        public HtmlSetting GetByKey(string key)
        {
            var query = from c in FDIDB.HtmlSettings
                        where c.Key.ToLower().Equals(key.ToLower()) 
                        select c;
            return query.FirstOrDefault();
        }

        public HtmlSetting GetByKeyAndUrl(string key,string url)
        {
            var query = from c in FDIDB.HtmlSettings
                        where c.Key.ToLower().Equals(key.ToLower()) && c.url.ToLower().Equals(url.ToLower())
                        select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<HtmlSettingItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.HtmlSettings
                        select new HtmlSettingItem()
                        {
                            ID = o.ID,
                            Key = o.Key,
                            Value = o.Value,
                            Url = o.url??""
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlSettingItem GetSystemConfigItemById(int id)
        {
            var query = from o in FDIDB.HtmlSettings
                        where o.ID == id
                        orderby o.ID descending
                        select new HtmlSettingItem()
                        {
                            ID = o.ID,
                            Key = o.Key,
                            Value = o.Value
                        };
            return query.FirstOrDefault();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public HtmlSetting GetById(int id)
        {
            var query = from c in FDIDB.HtmlSettings where c.ID == id select c;
            return query.FirstOrDefault();
        }

        #endregion

        public void Add(HtmlSetting htmlSetting)
        {
            FDIDB.HtmlSettings.Add(htmlSetting);
        }
        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }

    }
}
