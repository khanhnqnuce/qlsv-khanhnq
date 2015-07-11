using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Shop_CommentDA : BaseDA
    {
        #region Constructer
        public Shop_CommentDA()
        {
        }

        public Shop_CommentDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Shop_CommentDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ShopCommentItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Shop_Comment
                        orderby c.Title
                        select new ShopCommentItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isActive">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ShopCommentItem> GetListSimpleAll(bool isActive)
        {
            var query = from c in FDIDB.Shop_Comment
                        where (c.IsActive == isActive) && !c.IsDeleted.Value
                        orderby c.Title
                        select new ShopCommentItem
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
        public List<ShopCommentItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Comment
                        orderby c.Title
                        where c.Title.StartsWith(keyword) && !c.IsDeleted.Value
                        select new ShopCommentItem
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
        /// <param name="isActive"> </param>
        /// <returns></returns>
        public List<ShopCommentItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isActive)
        {
            var query = from c in FDIDB.Shop_Comment
                        orderby c.Title
                        where c.IsActive == isActive
                        && c.Title.StartsWith(keyword) && !c.IsDeleted.Value
                        select new ShopCommentItem
                                   {
                                       ID = c.ID,
                                       Title = c.Title
                                   };
            return query.Take(showLimit).ToList();
        }
        public List<ShopCommentItem> GetListSimpleByCustomerID(HttpRequestBase httpRequest, List<int> ltsCustomerID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Shop_Comment
                        where !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new ShopCommentItem
                        {
                            ID = o.ID,
                            ProductID = o.ProductID,
                            Title = o.Title,
                            Name = o.Name,
                            IsActive = o.IsActive,
                            DateCreated = o.DateCreated,
                            DateModified = o.DateModified,
                            Email = o.Email,
                            LinkUrl = o.LinkUrl,
                            ModifiedBy = o.ModifiedBy,
                            //Customer = o.Customer,
                            Content = o.Content,
                            TotalReply = o.Shop_Comment1.Count()

                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="emailCustomer"> </param>
        /// <param name="ltsArrID"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ShopCommentItem> GetListSimpleByRequest(HttpRequestBase httpRequest, string emailCustomer, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);

            var query = from o in FDIDB.Shop_Comment
                        where !o.IsDeleted.Value && (o.ShopCommentID == null || o.ShopCommentID == 0)
                        orderby o.DateCreated descending
                        select new ShopCommentItem
                        {
                            ID = o.ID,
                            ProductID = o.ProductID,
                            Title = o.Title,
                            CustomerID = o.CustomerID,
                            Name = o.Name,
                            IsActive = o.IsActive,
                            DateCreated = o.DateCreated,
                            DateModified = o.DateModified,
                            Email = o.Email,
                            LinkUrl = o.LinkUrl,
                            ModifiedBy = o.ModifiedBy,
                            Content = o.Content,
                            Product = new ProductItem
                            {
                                ID = o.Shop_Product.ID,
                                Name = o.Shop_Product.Name
                            },

                            NgayTraLoi = (o.Shop_Comment1.Any()) ? o.Shop_Comment1.FirstOrDefault().DateCreated : o.DateCreated,
                            CommentLikeItem = o.Shop_CommentLike.Select(c => new CommentLikeItem
                                               {
                                                   ID = c.ID,
                                                   Like = c.Like,
                                                   CommentID = c.ShopCommentID,
                                                   ListCustomerID = c.ListCustomerID
                                               }).FirstOrDefault(),
                            CommentReportItem = o.Shop_CommentReport.Select(c => new CommentReportItem
                                                {
                                                    ID = c.ID,
                                                    Report = c.Report,
                                                    CommentID = c.ShopCommentID,
                                                    ListCustomerID = c.ListCustomerID
                                                }).FirstOrDefault(),

                            TotalReply = o.Shop_Comment1.Count()

                        };


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
                var tuNgay = Convert.ToString(httpRequest.QueryString["FromDate"]);
                tuNgay += " 00:00:00";
                var formDate = Convert.ToDateTime(tuNgay);
                query = query.Where(c => c.DateCreated >= formDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                denNgay += " 23:30:00";
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.DateCreated <= toDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
            {
                var tuGio = Convert.ToString("01/04/2014");
                tuGio += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                var formDate = Convert.ToDateTime(tuGio);
                query = query.Where(c => c.DateCreated.Value.Hour >= formDate.Hour);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
            {
                var denGio = Convert.ToString("01/04/2014");
                denGio += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                var toDate = Convert.ToDateTime(denGio);
                query = query.Where(c => c.DateCreated.Value.Hour <= toDate.Hour);

            }
            if (!String.IsNullOrEmpty(Request.ProductName))
            {
                query = query.Where(o => o.Product.Name.ToLower().Contains(Request.ProductName.ToLower()));
            }
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }
        public BCTGTraLoiCommentItem GetBCThoiGianTraLoi(HttpRequestBase httpRequest, string emailCustomer, List<int> ltsArrID, bool breakTime)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Shop_Comment
                        where !o.IsDeleted.Value && o.ShopCommentID > 0 && o.IsActive == true
                        orderby o.DateCreated descending
                        select new ShopCommentItem
                        {
                            CustomerID = o.CustomerID,
                            Email = o.Email,
                            IsActive = o.IsActive,
                            NgayTraLoi = o.Shop_Comment2.DateCreated,
                            DatKPI = o.DatKPI,
                            DateCreated = o.DateCreated,
                            DateModified = o.DateModified,
                            ShopCommentID = o.ShopCommentID
                        };
            query = query.Where(c => c.NgayTraLoi != null);
            if (ltsArrID.Count > 0)
            {
                query = query.Where(o => ltsArrID.Contains(o.CustomerID.Value));
            }
            if (!String.IsNullOrEmpty(emailCustomer))
            {
                query = query.Where(o => o.Email.Contains(emailCustomer));
            }
            if (breakTime)
            {

                query = query.Where(c => c.NgayTraLoi.Value.Hour != 12 && c.NgayTraLoi.Value.Hour != 19);


            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromDate"]))
            {
                var tuNgay = Convert.ToString(httpRequest.QueryString["FromDate"]);
                tuNgay += " 00:00:00";
                var formDate = Convert.ToDateTime(tuNgay);

                query = query.Where(c => c.NgayTraLoi >= formDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                denNgay += " 23:30:00";
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.NgayTraLoi <= toDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
            {
                var tuGio = Convert.ToString("01/04/2014");
                tuGio += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                var formDate = Convert.ToDateTime(tuGio);
                query = query.Where(c => c.NgayTraLoi.Value.Hour >= formDate.Hour);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
            {
                var denGio = Convert.ToString("01/04/2014");
                denGio += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                var toDate = Convert.ToDateTime(denGio);
                query = query.Where(c => c.NgayTraLoi.Value.Hour <= toDate.Hour);

            }


            if (!string.IsNullOrEmpty(httpRequest.QueryString["DatKPI"]))
            {
                query = Convert.ToString(httpRequest.QueryString["DatKPI"]) == "1" ? query.Where(c => c.DatKPI == true) : query.Where(c => c.DatKPI == false || c.DatKPI == null);
            }



            if (query.Any())
            {
                var shopCommentItem = query.FirstOrDefault();
                if (shopCommentItem != null)
                    if (shopCommentItem.DateCreated != null)
                        if (shopCommentItem.NgayTraLoi != null)
                            return new BCTGTraLoiCommentItem
                                       {
                                           TotalComment = query.Count(),
                                           ToTalDatKPI = query.Count(b => b.DatKPI == true),
                                           ToTalKoDatKPI = query.Count(b => b.DatKPI == false),
                                           NgayTao = shopCommentItem.DateCreated.Value,
                                           NgayTraloi = shopCommentItem.NgayTraLoi.Value,
                                           ThoiGianTraLoiComment = query.Where(b => b.NgayTraLoi != null).Select(b => EntityFunctions.DiffMinutes(b.NgayTraLoi.Value, b.DateCreated.Value).Value).Sum()
                                       };
            }
            return null;
        }
        public List<ShopCommentItem> GetListSimpleByEmail(HttpRequestBase httpRequest, string emailCustomer, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Shop_Comment
                        where !o.IsDeleted.Value && o.IsActive != null
                        orderby o.DateCreated descending
                        select new ShopCommentItem
                        {
                            ID = o.ID,
                            CustomerID = o.CustomerID,
                            ProductID = o.ProductID.HasValue ? o.ProductID : 0,
                            ShopCommentID = o.ShopCommentID.HasValue ? o.ShopCommentID : 0,
                            Title = o.Title,
                            Name = o.Name,
                            IsActive = o.IsActive.HasValue && o.IsActive.Value,
                            NgayTraLoi = o.ShopCommentID.HasValue ? o.Shop_Comment2.DateCreated : o.Shop_Comment1.FirstOrDefault().DateCreated,
                            //ThoiGianTraLoiComment = o.DateCreated.HasValue&&o.Shop_Comment2.DateCreated.HasValue?(int)(o.DateCreated.Value - o.Shop_Comment2.DateCreated.Value).TotalMinutes:0,
                            DatKPI = o.DatKPI.HasValue && o.DatKPI.Value,
                            DateCreated = o.DateCreated,
                            //ThoiGianTraLoiComment = (NgayTraLoi - DateCreated)
                            DateModified = o.DateModified,
                            Email = o.Email,
                            Content = o.Content,
                            Product = new ProductItem
                                          {
                                              ID = o.Shop_Product.ID,
                                              Name = o.Shop_Product.Name
                                          },
                        };
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
                var tuNgay = Convert.ToString(httpRequest.QueryString["FromDate"]);
                tuNgay += " 00:00:00";
                var formDate = Convert.ToDateTime(tuNgay);
                query = query.Where(c => c.DateCreated >= formDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                denNgay += " 23:30:00";
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.DateCreated <= toDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
            {
                var tuGio = Convert.ToString("01/04/2014");
                tuGio += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                var formDate = Convert.ToDateTime(tuGio);
                query = query.Where(c => c.DateCreated.Value.Hour >= formDate.Hour);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
            {
                var denGio = Convert.ToString("01/04/2014");
                denGio += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                var toDate = Convert.ToDateTime(denGio);
                query = query.Where(c => c.DateCreated.Value.Hour <= toDate.Hour);

            }

            if (!string.IsNullOrEmpty(httpRequest.QueryString["IsActive"]))
            {
                query = Convert.ToString(httpRequest.QueryString["IsActive"]) == "1" ? query.Where(c => c.IsActive == true) : query.Where(c => c.IsActive == false);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["DatKPI"]))
            {
                query = Convert.ToString(httpRequest.QueryString["DatKPI"]) == "1" ? query.Where(c => c.DatKPI == true) : query.Where(c => c.DatKPI == false || c.DatKPI == null);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ShopCommentID"]))
            {
                query = Convert.ToInt32(httpRequest.QueryString["ShopCommentID"]) > 0 ? query.Where(c => c.ShopCommentID > 0) : query.Where(c => c.ShopCommentID == 0 || c.ShopCommentID == null);
            }
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }
        public List<ShopCommentItem> GetListSimpleByEmailNoPaging(HttpRequestBase httpRequest, string emailCustomer, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Shop_Comment
                        where !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new ShopCommentItem
                        {
                            ID = o.ID,
                            ProductID = o.ProductID,
                            CustomerID = o.CustomerID.Value,
                            ShopCommentID = o.ShopCommentID,
                            Title = o.Title,
                            Name = o.Name,
                            IsActive = o.IsActive,
                            NgayTraLoi = o.Shop_Comment1.FirstOrDefault().DateCreated,
                            DatKPI = o.DatKPI,
                            DateCreated = o.DateCreated,
                            DateModified = o.DateModified,
                            Email = o.Email,
                            Content = o.Content,
                            Product = new ProductItem
                            {
                                ID = o.Shop_Product.ID,
                                Name = o.Shop_Product.Name
                            },
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
                tuNgay += " 00:00:00";
                var formDate = Convert.ToDateTime(tuNgay);
                query = query.Where(c => c.DateCreated >= formDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToDate"]))
            {
                var denNgay = Convert.ToString(httpRequest.QueryString["ToDate"]);
                denNgay += " 23:30:00";
                var toDate = Convert.ToDateTime(denNgay);
                query = query.Where(c => c.DateCreated <= toDate);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["FromTime"]))
            {
                var tuGio = Convert.ToString("01/04/2014");
                tuGio += " " + Convert.ToString(httpRequest.QueryString["FromTime"]);
                var formDate = Convert.ToDateTime(tuGio);
                query = query.Where(c => c.DateCreated.Value.Hour >= formDate.Hour);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ToTime"]))
            {
                var denGio = Convert.ToString("01/04/2014");
                denGio += " " + Convert.ToString(httpRequest.QueryString["ToTime"]);
                var toDate = Convert.ToDateTime(denGio);
                query = query.Where(c => c.DateCreated.Value.Hour <= toDate.Hour);

            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["IsActive"]))
            {
                query = Convert.ToString(httpRequest.QueryString["IsActive"]) == "1" ? query.Where(c => c.IsActive == true) : query.Where(c => c.IsActive == false);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["DatKPI"]))
            {
                query = Convert.ToString(httpRequest.QueryString["DatKPI"]) == "1" ? query.Where(c => c.DatKPI == true) : query.Where(c => c.DatKPI == false || c.DatKPI == null);
            }
            if (!string.IsNullOrEmpty(httpRequest.QueryString["ShopCommentID"]))
            {
                query = Convert.ToInt32(httpRequest.QueryString["ShopCommentID"]) > 0 ? query.Where(c => c.ShopCommentID > 0) : query.Where(c => c.ShopCommentID == 0);
            }

            //     query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }
        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ShopCommentItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Shop_Comment
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new ShopCommentItem
                                   {
                                       ID = o.ID,
                                       ProductID = o.ProductID,
                                       Title = o.Title,
                                       Name = o.Name,
                                       IsActive = o.IsActive.Value,
                                       DateCreated = o.DateCreated.Value,
                                       DateModified = o.DateModified,
                                       Email = o.Email,
                                       LinkUrl = o.LinkUrl,
                                       ModifiedBy = o.ModifiedBy,
                                       Content = o.Content,
                                       TotalReply = o.Shop_Comment1.Count()
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
        public Shop_Comment GetById(int commentID)
        {
            var query = from c in FDIDB.Shop_Comment where c.ID == commentID select c;
            return query.FirstOrDefault();
        }



        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Comment> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Comment where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopComment">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Comment shopComment)
        {
            var query = from c in FDIDB.Shop_Comment where ((c.Title == shopComment.Title) && (c.ID != shopComment.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="commentName"></param>
        /// <returns></returns>
        public Shop_Comment GetByName(string commentName)
        {
            var query = from c in FDIDB.Shop_Comment where ((c.Title == commentName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="shopComment">bản ghi cần thêm</param>
        public void Add(Shop_Comment shopComment)
        {
            FDIDB.Shop_Comment.Add(shopComment);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="shopComment">Xóa bản ghi</param>
        public void Delete(Shop_Comment shopComment)
        {
            FDIDB.Shop_Comment.Remove(shopComment);
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
