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
    public class NewsImplement : InitDB, IReposityNews
    {
        #region get data

        public List<NewsItem> GetPageBySPQuery(IQueryable<NewsItem> query, int currentPage, int rowPerPage)
        {
            #region lấy về bảng tạm & danh sách ID

            var ltsNewsItem = query.ToList(); //Lấy về tất cả
            var ltsIdSelect = new List<int>();
            if (ltsNewsItem.Any())
            {
                TongSoBanGhiSauKhiQuery = ltsNewsItem.Count(); // Tổng số lấy về        
                int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
                int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
                intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

                for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
                {
                    ltsIdSelect.Add(Convert.ToInt32(ltsNewsItem[rowIndex].ID));
                }

            }
            else
                TongSoBanGhiSauKhiQuery = 0;
            #endregion
            //Query listItem theo listID
            var iquery = from c in ltsNewsItem
                         where ltsIdSelect.Contains(c.ID)
                         select c;

            return iquery.ToList();
        }
        public List<NewsItem> GetPageBySPQuery2(List<NewsItem> query, int currentPage, int rowPerPage)
        {
            #region lấy về bảng tạm & danh sách ID

            var ltsNewsItem = query; //Lấy về tất cả
            var ltsIdSelect = new List<int>();
            if (ltsNewsItem.Any())
            {
                TongSoBanGhiSauKhiQuery = ltsNewsItem.Count(); // Tổng số lấy về        
                int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
                int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
                intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

                for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
                {
                    ltsIdSelect.Add(Convert.ToInt32(ltsNewsItem[rowIndex].ID));
                }

            }
            else
                TongSoBanGhiSauKhiQuery = 0;
            #endregion
            //Query listItem theo listID
            var iquery = from c in ltsNewsItem
                         where ltsIdSelect.Contains(c.ID)
                         select c;

            return iquery.ToList();
        }

        public List<NewsItem> NewsPage(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem)
        {
            listNewsItem = GetPageBySPQuery2(listNewsItem, currentPage, rowPerPage);
            return listNewsItem;
        }

        public List<NewsItem> GetList()
        {
            var query = (from c in Instance.News_News
                         where c.IsDeleted == false && c.IsShow == true
                         orderby c.DateCreated descending
                         select new NewsItem
                         {
                             ID = c.ID,
                             Title = c.Title,
                             TitleAscii = c.TitleAscii,
                             Description = c.Description,
                             CateAscii = c.News_Category.FirstOrDefault().NameAscii,
                             IsHot = c.IsHot,
                             PictureUrl = c.Gallery_Picture.Folder + c.Gallery_Picture.Url,
                             DateCreated = c.DateCreated,
                             UserNameCreate = c.aspnet_Users.UserName,
                             SeoDescription = c.SEODescription,
                             SeoKeyword = c.SEOKeyword,
                             SeoTitle = c.SEOTitle,
                             NewTag = c.System_Tag.Select(m => new TagItem()
                             {
                                 Name = m.NameAscii
                             })
                         }).ToList();
            return query;
        }

        public List<NewsItem> GetListHots()
        {
            var query = (from c in Instance.News_News
                         where c.IsDeleted == false && c.IsShow == true && c.IsHot == true
                         orderby c.DateCreated descending
                         select new NewsItem
                         {

                             Title = c.Title,
                             TitleAscii = c.TitleAscii,
                             Description = c.Description,
                             CateAscii = c.News_Category.FirstOrDefault().NameAscii,
                             PictureUrl = c.Gallery_Picture.Folder + c.Gallery_Picture.Url
                         }).ToList();
            return query;
        }

        public List<NewsItem> GetListById(int id)
        {
            var query = (from c in Instance.News_News
                         where c.IsDeleted == false && c.IsShow == true
                         orderby c.DateCreated descending
                         select new NewsItem
                         {

                             Title = c.Title,
                             TitleAscii = c.TitleAscii,
                             Description = c.Description,
                             CateAscii = c.News_Category.FirstOrDefault().NameAscii
                             //PictureUrl = c.Gallery_Picture.Folder+c.Gallery_Picture.Url
                         }).ToList();
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameAscii"> </param>
        /// <returns></returns>
        //}
        public NewsItem GetNewsByNameAscii(string nameAscii)
        {
            var query = from c in Instance.News_News
                        where c.TitleAscii.ToLower().Equals(nameAscii.ToLower()) && c.IsDeleted == false && c.IsShow == true
                        select new NewsItem
                        {
                            ID = c.ID,
                            Title = c.Title,
                            TitleAscii = c.TitleAscii,
                            PictureUrl = c.Gallery_Picture.IsDeleted != true ? c.Gallery_Picture.Folder + c.Gallery_Picture.Url : "",
                            //Khanhnv 03/31/2015
                            Description = c.Description,
                            UserNameCreate = c.aspnet_Users.UserName,
                            //Khanhnv 03/31/2015
                            Details = c.Details,
                            CateName = c.News_Category.FirstOrDefault().Name,
                            CateAscii = c.News_Category.FirstOrDefault().NameAscii,
                            DateCreated = c.DateCreated,
                            //phuocnh 05/11/2015]
                            SeoDescription = c.SEODescription,
                            SeoKeyword = c.SEOKeyword,
                            SeoTitle = c.SEOTitle
                        };
            return query.FirstOrDefault();
        }
        #endregion
    }
}
