using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class News_CommentDA : BaseDA
    {
        #region Constructer
        public News_CommentDA()
        {
        }

        public News_CommentDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public News_CommentDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsCommentItem> GetAllListSimple()
        {
            var query = from c in FDIDB.News_Comment
                        orderby c.Title
                        select new NewsCommentItem
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
        public List<NewsCommentItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.News_Comment
                        where (c.IsShow == isShow) && !c.IsDeleted.Value
                        orderby c.Title
                        select new NewsCommentItem
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
        public List<NewsCommentItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.News_Comment
                        orderby c.Title
                        where c.Title.StartsWith(keyword) && !c.IsDeleted.Value
                        select new NewsCommentItem
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
        public List<NewsCommentItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.News_Comment
                        orderby c.Title
                        where c.IsShow == isShow
                        && c.Title.StartsWith(keyword) && !c.IsDeleted.Value
                        select new NewsCommentItem
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
        /// <param name="emailCustomer"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsCommentItem> GetListSimpleByRequest(HttpRequestBase httpRequest, string emailCustomer)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.News_Comment
                        where !o.IsDeleted.Value && o.NewsCommentID == null
                        orderby o.DateCreated descending
                        select new NewsCommentItem
                        {
                            ID = o.ID,
                            CustomerID = o.CustomerID,
                            NewsID = o.NewsID,
                            Title = o.Title,
                            Name = o.Name,
                            IsShow = o.IsShow.Value,
                            DateCreated = o.DateCreated,
                            Email = o.Email,
                            NewsItem = new NewsItem
                                           {
                                               ID = o.News_News.ID,
                                               Title = o.News_News.Title,
                                               IEnumerableNewsCategory = o.News_News.News_Category.Select(c => new NewsCategoryItem
                                                                                                                   {
                                                                                                                       ID = c.ID,
                                                                                                                       Name = c.Name,
                                                                                                                       NameAscii = c.NameAscii
                                                                                                                   })
                                           },

                            CommentLikeItem = o.Comment_Like.Select(c => new CommentLikeItem
                                                                                   {
                                                                                       ID = c.ID,
                                                                                       Like = c.Like,
                                                                                       CommentID = c.CommentID,
                                                                                       ListCustomerID = c.ListCustomerID
                                                                                   }).FirstOrDefault(),
                            CommentReportItem = o.News_CommentReport.Select(r => new CommentReportItem
                                                                                   {
                                                                                       ID = r.ID,
                                                                                       Report = r.Report,
                                                                                       CommentID = r.NewsCommentID,
                                                                                       ListCustomerID = r.ListCustomer
                                                                                   }).FirstOrDefault(),

                            Content = o.Content,
                            TotalReply = o.News_Comment1.Count
                        };

            if (!String.IsNullOrEmpty(Request.ProductName))
            {
                query = query.Where(o => o.NewsItem.Title.ToLower().Contains(Request.ProductName.ToLower()));
            }

            if (!String.IsNullOrEmpty(emailCustomer))
            {
                query = query.Where(o => o.Email.Contains(emailCustomer));
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromDate"]))
            {
                var tuNgay = Convert.ToString(httpRequest.QueryString["FromDate"]);
                if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
                {
                    tuNgay += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                }
                else
                {
                    tuNgay += " 00:00:00";
                }
                var formDate = Convert.ToDateTime(tuNgay);
                query = query.Where(c => c.DateCreated.Value >= formDate);
            }

            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
                {
                    denNgay += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                }
                else
                {
                    denNgay += " 23:30:00";
                }
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.DateCreated < toDate);
            }

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        public List<NewsCommentItem> GetListSimpleByEmail(HttpRequestBase httpRequest, string emailCustomer, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.News_Comment
                        where o.IsDeleted == false
                        orderby o.DateCreated descending
                        select new NewsCommentItem
                        {
                            ID = o.ID,
                            CustomerID = o.CustomerID,
                            NewsCommentID = o.NewsCommentID.HasValue ? o.NewsCommentID.Value : 0,
                            NewsID = o.NewsID,
                            Title = o.Title,
                            Name = o.Name,
                            IsShow = o.IsShow.HasValue && o.IsShow.Value,
                            DatKPI = o.DatKPI.HasValue && o.DatKPI.Value,
                            DateCreated = o.DateCreated,
                            NgayTraLoi = o.NewsCommentID.HasValue ? o.News_Comment2.DateCreated : o.News_Comment1.FirstOrDefault().DateCreated,
                            Email = o.Email,
                            NewsItem = new NewsItem
                                         {
                                             ID = o.News_News.ID,
                                             Title = o.News_News.Title,
                                             
                                         },
                          
                            Content = o.Content,
                            TotalReply = o.News_Comment1.Count()

                        };

            if (!String.IsNullOrEmpty(Request.ProductName))
            {
                query = query.Where(o => o.NewsItem.Title.ToLower().Contains(Request.ProductName.ToLower()));
            }
            if (ltsArrID.Count > 0)
            {
                query = query.Where(o => ltsArrID.Contains(o.CustomerID.Value));
            }
            if (!String.IsNullOrEmpty(emailCustomer))
            {
                query = query.Where(o => o.Email.Contains(emailCustomer));
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromDate"]))
            {
                string tuNgay = Convert.ToString(httpRequest.QueryString["FromDate"]);
                if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
                {
                    tuNgay += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                }
                else
                {
                    tuNgay += " 00:00:00";
                }
                var formDate = Convert.ToDateTime(tuNgay);
                query = query.Where(c => c.DateCreated.Value >= formDate);
            }

            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
                {
                    denNgay += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                }
                else
                {
                    denNgay += " 23:30:00";
                }
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.DateCreated < toDate);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["Category"]))
            {
                var cate = Convert.ToInt32(httpRequest.QueryString["Category"]);
                query = query.Where(o => o.NewsItem.IEnumerableNewsCategory.FirstOrDefault().ID == cate);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["IsShow"]))
            {
                query = Convert.ToString(httpRequest.QueryString["IsShow"]) == "1" ? query.Where(c => c.IsShow) : query.Where(c => c.IsShow == false);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["DatKPI"]))
            {
                query = Convert.ToString(httpRequest.QueryString["DatKPI"]) == "1" ? query.Where(c => c.DatKPI == true) : query.Where(c => c.DatKPI == false || c.DatKPI == null);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["NewsCommentID"]))
            {
                query = Convert.ToInt32(httpRequest.QueryString["NewsCommentID"]) > 0 ? query.Where(c => c.NewsCommentID > 0) : query.Where(c => c.NewsCommentID == 0);
            }
           
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        public List<NewsCommentItem> GetListSimpleByEmailNoPaging(HttpRequestBase httpRequest, string emailCustomer, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.News_Comment
                        where !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new NewsCommentItem
                        {
                            ID = o.ID,
                            CustomerID = o.CustomerID.HasValue ? o.CustomerID.Value : 0,
                            NewsID = o.NewsID.HasValue ? o.NewsID : 0,
                            NewsCommentID = o.NewsCommentID.HasValue ? o.NewsCommentID.Value : 0,
                            Title = o.Title,
                            Name = o.Name,
                            IsShow = o.IsShow.HasValue && o.IsShow.Value,

                            DatKPI = o.DatKPI.HasValue && o.DatKPI.Value,
                            DateCreated = o.DateCreated,
                            Email = o.Email,
                            Content = o.Content,
                            NewsItem = new NewsItem
                                           {
                                               ID = o.News_News.ID,
                                               Title = o.News_News.Title
                                           }
                        };
            if (!String.IsNullOrEmpty(emailCustomer))
            {
                query = query.Where(o => o.Email.Contains(emailCustomer));
            }
            if (ltsArrID.Count > 0)
            {
                query = query.Where(o => ltsArrID.Contains(o.CustomerID.Value));
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromDate"]))
            {
                var tuNgay = Convert.ToString(httpRequest.QueryString["FromDate"]);
                if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
                {
                    tuNgay += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                }
                else
                {
                    tuNgay += " 00:00:00";
                }
                var formDate = Convert.ToDateTime(tuNgay);
                query = query.Where(c => c.DateCreated.Value >= formDate);
            }

            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
                {
                    denNgay += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                }
                else
                {
                    denNgay += " 23:30:00";
                }
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.DateCreated < toDate);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["IsShow"]))
            {
                query = Convert.ToString(httpRequest.QueryString["IsShow"]) == "1" ? query.Where(c => c.IsShow) : query.Where(c => c.IsShow == false);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["DatKPI"]))
            {
                query = Convert.ToString(httpRequest.QueryString["DatKPI"]) == "1" ? query.Where(c => c.DatKPI == true) : query.Where(c => c.DatKPI == false || c.DatKPI == null);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["NewsCommentID"]))
            {
                query = Convert.ToInt32(httpRequest.QueryString["NewsCommentID"]) > 0 ? query.Where(c => c.NewsCommentID > 0) : query.Where(c => c.NewsCommentID == 0);
            }

            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<NewsCommentItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.News_Comment
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new NewsCommentItem
                                   {
                                       ID = o.ID,
                                       NewsID = o.NewsID,
                                       Title = o.Title,
                                       Name = o.Name,
                                       IsShow = o.IsShow.HasValue,
                                       DateCreated = o.DateCreated,
                                       Email = o.Email,
                                       Content = o.Content,
                                       TotalReply = o.News_Comment1.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="commentID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public News_Comment GetById(int commentID)
        {
            var query = from c in FDIDB.News_Comment where c.ID == commentID select c;
            return query.FirstOrDefault();
        }



        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<News_Comment> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.News_Comment where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="newsComment">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(News_Comment newsComment)
        {
            var query = from c in FDIDB.News_Comment where ((c.Title == newsComment.Title) && (c.ID != newsComment.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="commentName"></param>
        /// <returns></returns>
        public News_Comment GetByName(string commentName)
        {
            var query = from c in FDIDB.News_Comment where ((c.Title == commentName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="newsComment">bản ghi cần thêm</param>
        public void Add(News_Comment newsComment)
        {
            FDIDB.News_Comment.Add(newsComment);
            Save();
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="newsComment">Xóa bản ghi</param>
        public void Delete(News_Comment newsComment)
        {
            FDIDB.News_Comment.Remove(newsComment);
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
