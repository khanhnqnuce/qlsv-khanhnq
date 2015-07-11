using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.EndUser.Implementation
{
    public class HtmlSettingImplement : InitDB, IReposityHtmlSetting
    {
        #region get data

        //public List<HtmlSettingItem> GetPageBySPQuery(IQueryable<HtmlSettingItem> query, int currentPage, int rowPerPage)
        //{
        //    #region lấy về bảng tạm & danh sách ID

        //    var ltsHtmlSettingItem = query.ToList(); //Lấy về tất cả
        //    var ltsIdSelect = new List<int>();
        //    if (ltsHtmlSettingItem.Any())
        //    {
        //        TongSoBanGhiSauKhiQuery = ltsHtmlSettingItem.Count(); // Tổng số lấy về        
        //        int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
        //        int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
        //        intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

        //        for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
        //        {
        //            ltsIdSelect.Add(Convert.ToInt32(ltsHtmlSettingItem[rowIndex].ID));
        //        }

        //    }
        //    else
        //        TongSoBanGhiSauKhiQuery = 0;
        //    #endregion
        //    //Query listItem theo listID
        //    var iquery = from c in ltsHtmlSettingItem
        //                 where ltsIdSelect.Contains(c.ID)
        //                 select c;

        //    return iquery.ToList();
        //}
        //public List<HtmlSettingItem> GetPageBySPQuery2(List<HtmlSettingItem> query, int currentPage, int rowPerPage)
        //{
        //    #region lấy về bảng tạm & danh sách ID

        //    var ltsHtmlSettingItem = query; //Lấy về tất cả
        //    var ltsIdSelect = new List<int>();
        //    if (ltsHtmlSettingItem.Any())
        //    {
        //        TongSoBanGhiSauKhiQuery = ltsHtmlSettingItem.Count(); // Tổng số lấy về        
        //        int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
        //        int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
        //        intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

        //        for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
        //        {
        //            ltsIdSelect.Add(Convert.ToInt32(ltsHtmlSettingItem[rowIndex].ID));
        //        }

        //    }
        //    else
        //        TongSoBanGhiSauKhiQuery = 0;
        //    #endregion
        //    //Query listItem theo listID
        //    var iquery = from c in ltsHtmlSettingItem
        //                 where ltsIdSelect.Contains(c.ID)

        //                 select c;

        //    return iquery.ToList();
        //}

        //public List<HtmlSettingItem> HtmlSettingPage(int currentPage, int rowPerPage, string type, List<HtmlSettingItem> listHtmlSettingItem)
        //{
        //    listHtmlSettingItem = GetPageBySPQuery2(listHtmlSettingItem, currentPage, rowPerPage);
        //    return listHtmlSettingItem;
        //}

        public List<HtmlSettingItem> GetList()
        {
            var query = (from c in Instance.HtmlSettings
                         select new HtmlSettingItem
                         {
                             ID = c.ID,
                             Key = c.Key,
                             Value = c.Value,
                             Url = c.url ?? ""
                         }).ToList();
            return query;
        }

        //public List<HtmlSettingItem> GetListHots()
        //{
        //    var duoi = WebConfig.Urltail;
        //    var query = (from c in Instance.HtmlSetting_HtmlSetting
        //                 where c.IsDeleted == false && c.IsShow == true && c.IsHot == true
        //                 orderby c.DateCreated descending
        //                 select new HtmlSettingItem
        //                 {

        //                     Title = c.Title,
        //                     TitleAscii = c.TitleAscii + duoi,
        //                     Description = c.Description,
        //                     CateAscii = c.HtmlSetting_Category.FirstOrDefault().NameAscii
        //                     //PictureUrl = c.Gallery_Picture.Folder+c.Gallery_Picture.Url
        //                 }).ToList();
        //    return query;
        //}

        //public List<HtmlSettingItem> GetListById(int id)
        //{
        //    var duoi = WebConfig.Urltail;
        //    var query = (from c in Instance.HtmlSetting_HtmlSetting
        //                 where c.IsDeleted == false && c.IsShow == true
        //                 orderby c.DateCreated descending
        //                 select new HtmlSettingItem
        //                 {

        //                     Title = c.Title,
        //                     TitleAscii = c.TitleAscii + duoi,
        //                     Description = c.Description,
        //                     CateAscii = c.HtmlSetting_Category.FirstOrDefault().NameAscii
        //                     //PictureUrl = c.Gallery_Picture.Folder+c.Gallery_Picture.Url
        //                 }).ToList();
        //    return query;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="nameAscii"> </param>
        ///// <returns></returns>
        ////}
        public List<HtmlSettingItem> GetHtmlSettingByKey(string key)
        {
            var query = from c in Instance.HtmlSettings
                        where c.Key.Equals(key)
                        select new HtmlSettingItem
                        {
                            ID = c.ID,
                            Key = c.Key,
                            Value = c.Value,
                            Url = c.url ?? ""
                        };
            return query.ToList();
        }
        #endregion
    }
}
