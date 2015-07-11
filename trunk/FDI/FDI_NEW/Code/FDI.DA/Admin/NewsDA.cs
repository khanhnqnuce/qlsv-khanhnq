using System.Web;
using FDI.Base;
using System.Collections.Generic;
using System.Linq;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class NewsDA : BaseDA
    {
        #region Constructer
        public NewsDA()
        {
        }

        public NewsDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public NewsDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsItem> GetAllListSimple()
        {
            var query = from c in FDIDB.News_News
                        where !c.IsDeleted.Value
                        orderby c.Title

                        select new NewsItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.News_News
                        where (c.IsShow == isShow) && !c.IsDeleted.Value
                        orderby c.Title
                        select new NewsItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<NewsItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.News_News
                        orderby c.Title
                        where c.Title.StartsWith(keyword) && !c.IsDeleted.Value
                        select new NewsItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title

                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <param name="isShow"> </param>
        /// <returns></returns>
        public List<NewsItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.News_News
                        orderby c.Title
                        where c.IsShow == isShow && c.IsDeleted == false
                        && c.Title.StartsWith(keyword) && !c.IsDeleted.Value
                        select new NewsItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.News_News
                        where !o.IsDeleted.Value
                        select o;
            query = query.SelectByRequest(Request, ref TotalRecord);

            return query.OrderByDescending(c => c.ID).Select(c => new NewsItem{
                                                                                    ID = c.ID,
                                                                                    Title = c.Title,
                                                                                    TitleAscii = c.TitleAscii,
                                                                                    Details = c.Details,
                                                                                    Description = c.Description,
                                                                                    SeoDescription = c.SEODescription,
                                                                                    SalePercent = c.SalePercent,
                                                                                    SeoKeyword = c.SEOKeyword,
                                                                                    SeoTitle = c.SEOTitle,
                                                                                    StartPromotionDate =
                                                                                        c.StartPromotionDate,
                                                                                    EndPromotionDate =
                                                                                        c.EndPromotionDate,
                                                                                    IsHot = c.IsHot,
                                                                                    IsShow = c.IsShow,
                                                                                    IsDeleted = c.IsDeleted,
                                                                                    IsAllowComment = c.IsAllowComment,
                                                                                    CateAscii = c.News_Category.FirstOrDefault().NameAscii,
                                                                                    CateName = c.News_Category.FirstOrDefault().Name,
                                                                                    TotalComments =
                                                                                        c.News_Comment.Count(),
                                                                                    Author = c.Author,
                                                                                    Modifier = c.Modifier,
                                                                                    IP = c.IP

                                                                                }).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<NewsItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.News_News
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.ID descending
                        select new NewsItem
                                   {
                                       ID = o.ID,
                                       Title = o.Title,
                                       TitleAscii = o.TitleAscii,
                                       IsShow = o.IsShow,
                                       Details = o.Details,
                                       Description = o.Description,
                                       IsHot = o.IsHot,
                                       StartPromotionDate = o.StartPromotionDate.Value,
                                       EndPromotionDate = o.EndPromotionDate.Value,
                                       PromotionPictureIconID = o.PromotionPictureIconID,
                                       PromotionPictureID = o.PromotionPictureID,
                                       IsAllowComment = o.IsAllowComment,
                                       SeoDescription = o.SEODescription,
                                       SeoKeyword = o.SEOKeyword,
                                       SeoTitle = o.SEOTitle,
                                       Author = o.Author,
                                       Modifier = o.Modifier,
                                       IP = o.IP,
                                       PictureUrl = (o.PictureID.HasValue) ? o.Gallery_Picture.Folder + o.Gallery_Picture.Url : string.Empty,
                                       PicturePromotionUrl = (o.PromotionPictureID.HasValue) ? o.Gallery_Picture.Folder + o.Gallery_Picture.Url : string.Empty,
                                       NewsCategory = o.News_Category.Select(c => new NewsCategoryItem
                                                                                      {
                                                                                          ParentID = c.ID,
                                                                                          Name = c.Name,
                                                                                          NameAscii = c.NameAscii
                                                                                      }).ToList(),
                                       NewTag = o.System_Tag.Select(c => new TagItem
                                                                             {
                                                                                 ID = c.ID,
                                                                                 Name = c.Name,
                                                                                 NameAscii = c.NameAscii
                                                                             }).ToList(),
                                       TotalComments = o.News_Comment.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="newsID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public News_News GetById(int newsID)
        {
            var query = from c in FDIDB.News_News where c.ID == newsID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<News_Category> GetListCategoryByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.News_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Tag> GetListIntTagByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Tag where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }
        public List<System_Tag> GetListTagByArrID(List<string> ltsArrID)
        {
            var query = from c in FDIDB.System_Tag where ltsArrID.Contains(c.Name) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product> GetListIntProductByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product where ltsArrID.Contains(c.ID) && c.IsDelete == false && c.IsShow == false select c;
            return query.ToList();
        }
        public List<Shop_Product> GetListProductByArrID(List<string> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product where ltsArrID.Contains(c.Name) && c.IsDelete == false && c.IsShow == false select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<News_News> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.News_News where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="title">Tên bản ghi hiện tại</param>
        /// <param name="id">id của bạn ghi hiện tại</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(string title, int id)
        {
            var query = (from c in FDIDB.News_News
                         where c.Title == title && c.ID != id && c.IsDeleted == false
                         select c).Count();
            return query > 0;
        }

        /// <summary>
        /// Kiểm tra link ascii đã tồn tại hay chưa
        /// </summary>
        /// <param name="titleascii">Tên bản ghi hiện tại</param>
        /// <param name="id">id của bạn ghi hiện tại</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckTitleAsciiExits(string titleascii, int id)
        {
            var query = (from c in FDIDB.News_News
                         where c.TitleAscii == titleascii && c.ID != id && c.IsDeleted == false
                         select c).Count();
            return query > 0;
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="title"> </param>
        /// <returns>Bản ghi</returns>
        public News_News GetByName(string title)
        {
            var query = from c in FDIDB.News_News where ((c.Title == title)) select c;
            return query.FirstOrDefault();
        }



        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="newsNews">bản ghi cần thêm</param>
        public void Add(News_News newsNews)
        {
            FDIDB.News_News.Add(newsNews);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="newsNews">Xóa bản ghi</param>
        public void Delete(News_News newsNews)
        {
            FDIDB.News_News.Remove(newsNews);
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
