using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.Admin;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.EndUser.Implementation
{

    public class NewsCategoryImplementation : InitDB, IReposityNewsCategory
    {
        #region get data

        public List<NewsCategoryItem> GetList()
        {
            var query = (from c in Instance.News_Category
                         where c.ID > 1 && c.IsDeleted == false && c.IsShow == true
                         select new NewsCategoryItem
                         {
                             ID = c.ID,
                             Name = c.Name,
                             NameAscii = c.NameAscii,
                             Description = c.Description,

                             ParentID = c.ParentID,
                             ListNewsItem = c.News_News.Where(o => o.IsDeleted == false && o.IsShow == true).OrderByDescending(o => o.DateCreated).Select(n => new NewsItem
                             {
                                 ID = n.ID,
                                 Title = n.Title,
                                 //KhanhNv 03/31/15
                                 //TitleAscii = c.NameAscii + "/" + n.TitleAscii + duoi,
                                 TitleAscii = n.TitleAscii,
                                 CateName = c.Name,
                                 CateAscii = c.NameAscii,
                                 UserNameCreate = n.aspnet_Users.UserName,
                                 //KhanhNv 03/31/15
                                 PictureUrl = n.Gallery_Picture.IsDeleted != true ? n.Gallery_Picture.Folder + n.Gallery_Picture.Url : "",
                                 Description = n.Description,
                                 DateCreated = n.DateCreated,
                                 StartDateDisplay = n.StartDateDisplay,
                                 IsHot = n.IsHot
                             })
                         }).ToList();
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameAscii"> </param>
        /// <returns></returns>
        //}
        public NewsCategoryItem GetNewsCategoryByNameAscii(string nameAscii)
        {
            var query = from c in Instance.News_Category
                        where c.NameAscii.ToLower().Equals(nameAscii.ToLower()) && c.IsDeleted == false
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Description = c.Description,
                                       ListNewsItem = c.News_News.Where(o => o.IsDeleted == false && o.IsShow == true).Select(n => new NewsItem
                                       {
                                           ID = n.ID,
                                           TitleAscii = n.TitleAscii,
                                           Title = n.Title,
                                           PriceAfterDown = n.Description,
                                           CateName = c.Name,
                                           CateAscii = c.NameAscii,
                                           UserNameCreate = n.aspnet_Users.UserName,
                                           PictureUrl = n.Gallery_Picture.IsDeleted != true ? n.Gallery_Picture.Folder + n.Gallery_Picture.Url : "",
                                           Description = n.Description,
                                           DateCreated = n.DateCreated,
                                           StartDateDisplay = n.StartDateDisplay,
                                           IsHot = n.IsHot,
                                           Details = n.Details,
                                           SeoTitle = n.SEOTitle,
                                           SeoDescription = n.SEODescription,
                                           SeoKeyword = n.SEOKeyword,
                                       }),
                                       SEOTitle = c.SEOTitle,
                                       SEODescription = c.SEODescription,
                                       SEOKeyword = c.SEOKeyword,
                                       CategoryItem = c.News_Category1.Where(u => u.IsDeleted == false).Select(v => new NewsCategoryitemnew
                                       {
                                           ID = v.ID,
                                           Name = v.Name,
                                           NameAscii = v.NameAscii,
                                           Description = v.Description,
                                           Class = v.Class
                                       }),
                                       Class = c.Class
                                   };
            return query.FirstOrDefault();
        }

        public NewsCategoryItem GetNewsCategoryById(int id)
        {
            var query = from c in Instance.News_Category
                        where c.ID == id && c.IsDeleted == false
                        select new NewsCategoryItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            Description = c.Description,
                        };
            return query.FirstOrDefault();
        }
        #endregion

        #region Insert, Update, Delete

        /// <summary>
        /// Edit by dongdt
        /// 12-05-14.
        /// </summary>
        /// <param name="newsCategory"></param>
        /// <returns></returns>
        public int InsertNewsCategory(News_Category newsCategory)
        {
            Instance.News_Category.Add(newsCategory);
            Instance.SaveChanges();
            return newsCategory.ID;
        }

        /// <summary>
        /// DongDT 27/12/2013
        /// </summary>
        /// <param name="newsCategory"></param>
        /// <returns></returns>
        public int AddNewsCategory(News_Category newsCategory)
        {
            Instance.News_Category.Add(newsCategory);
            Instance.SaveChanges();
            return newsCategory.ID;
        }
        public void Save()
        {
            Instance.SaveChanges();
        }

        #endregion
    }
}
