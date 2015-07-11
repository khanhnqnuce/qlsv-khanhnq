using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class AdvertisingDA : BaseDA
    {
        #region Constructer
        public AdvertisingDA()
        {
        }

        public AdvertisingDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public AdvertisingDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Advertisings
                        where !c.IsDeleted.Value
                        orderby c.ID descending

                        select new AdvertisingItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Advertisings
                        where (c.Show == isShow) && !c.IsDeleted.Value
                        orderby c.ID descending
                        select new AdvertisingItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<AdvertisingItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Advertisings

                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        orderby c.ID descending
                        select new AdvertisingItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name

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
        public List<AdvertisingItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Advertisings
                        orderby c.ID descending
                        where c.Show == isShow
                        && c.Name.StartsWith(keyword) && !c.IsDeleted.Value

                        select new AdvertisingItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="ltsIDNotInclude"></param>
        /// <returns></returns>
        public List<AdvertisingItem> GetListSimpleByRequest(HttpRequestBase httpRequest, List<int> ltsIDNotInclude)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Advertisings
                        where !ltsIDNotInclude.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.ID descending
                        select new AdvertisingItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       Show = o.Show,
                                       Content = o.Content,
                                       StartDate = o.StartDate,
                                       EndDate = o.EndDate,
                                       Url = o.Url,
                                       Order = o.Order,
                                       Link = o.Link,
                                       Click = o.Clicked,
                                       CreateOnUtc = o.CreateOnUtc,
                                       //PositionID = o.PositionID,
                                       PictureUrl = (o.PictureID.HasValue) ? o.Gallery_Picture.Folder + o.Gallery_Picture.Url : string.Empty,
                                       TypeID = o.TypeID,
                                       TypeName = (o.TypeID.HasValue) ? o.Advertising_Type.Name : string.Empty,

                                   };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.TypeID == Request.CategoryID);

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();

        }

        public List<ProductCategoryItem> Getlistsimple(bool isshow)
        {
            var query = from c in FDIDB.Shop_Category
                        where c.IsPublish == isshow && !c.IsDelete.Value && c.ParentID == 0
                        orderby c.Name
                        select new ProductCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                   };
            return query.ToList();
        }


        public List<AdvertisingItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);

            var query = from o in FDIDB.Advertisings
                        where !o.IsDeleted.Value
                        select o;

            if (!string.IsNullOrEmpty(httpRequest.QueryString["PositionID"]))
            {
                var positionId = Convert.ToInt32(httpRequest.QueryString["PositionID"]);
                query = query.Where(c => c.Advertising_Position.ID == positionId);
            }

            //if (!string.IsNullOrEmpty(httpRequest.QueryString["StartDate"]))
            //{
            //    var formDate = Convert.ToDateTime(httpRequest.QueryString["StartDate"]);
            //    query = query.Where(c => c.StartDate >= formDate);
            //}

            //if (!string.IsNullOrEmpty(httpRequest.QueryString["EndDate"]))
            //{
            //    var toDate = Convert.ToDateTime(httpRequest.QueryString["EndDate"]).AddDays(1);
            //    query = query.Where(c => c.EndDate < toDate);
            //}
            if (Request.CategoryID > 0)
                query = query.Where(o => o.ID == Request.CategoryID);

            query = query.SelectByRequest(Request, ref TotalRecord);

            return query.OrderByDescending(o => o.ID).Select(o => new AdvertisingItem
                                                                      {
                                                                          ID = o.ID,
                                                                          Name = o.Name,
                                                                          Show = o.Show,
                                                                          Content = o.Content,
                                                                          StartDate = o.StartDate,
                                                                          EndDate = o.EndDate,
                                                                          Width = o.Width,
                                                                          Height = o.Height,
                                                                          Order = o.Order,
                                                                          Url = o.Url,
                                                                          Click = o.Clicked,
                                                                          Link = o.Link,
                                                                       
                                                                          AdvertisingPosition = new AdvertisingPositionItem
                                                                          {
                                                                              Id = o.Advertising_Position.ID,
                                                                              Name = o.Advertising_Position.Name,
                                                                          },
                                                                    
                                                                          PictureUrl = (o.PictureID.HasValue) ? o.Gallery_Picture.Folder + o.Gallery_Picture.Url : string.Empty,
                                                                          TypeID = o.TypeID,
                                                                          TypeName = (o.TypeID.HasValue) ? o.Advertising_Type.Name : string.Empty,
                                                                          CreateOnUtc = o.CreateOnUtc
                                                                      }).ToList();


        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<AdvertisingItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Advertisings
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.ID descending
                        select new AdvertisingItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       Show = o.Show,
                                       Content = o.Content,
                                       StartDate = o.StartDate,
                                       EndDate = o.EndDate,
                                       CreateOnUtc = o.CreateOnUtc,
                                       Url = o.Url,
                                       Order = o.Order,
                                       Link = o.Link,
                                       //PositionID = o.PositionID,
                                       PictureUrl = (o.PictureID.HasValue) ? o.Gallery_Picture.Folder + o.Gallery_Picture.Url : string.Empty,
                                       TypeID = o.TypeID,
                                       TypeName = (o.TypeID.HasValue) ? o.Advertising_Type.Name : string.Empty,
                                       AdvertisingZone = o.Advertising_Zone.Select(c => new AdvertisingZoneItem
                                                                                            {
                                           ID = c.ID,
                                           PageAscii = c.PageAscii,
                                           Page = c.Page,
                                       }).ToList()
                                   };

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }


        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="advertisingID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Advertising GetById(int advertisingID)
        {
            var query = from c in FDIDB.Advertisings where c.ID == advertisingID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Advertising> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Advertisings where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<Advertising_Zone> GetListZoneByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Advertising_Zone where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<Shop_Category> GetListCategoryByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<Advertising_Type> GetListTypeByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Advertising_Type where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<Advertising_Position> GetListPositionByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Advertising_Position where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }
        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="ad">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Advertising ad)
        {
            var query = from c in FDIDB.Advertisings where ((c.Name == ad.Name) && (c.ID != ad.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="adName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Advertising GetByName(string adName)
        {
            var query = from c in FDIDB.Advertisings where ((c.Name == adName)) select c;
            return query.FirstOrDefault();
        }



        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="advertising">bản ghi cần thêm</param>
        public void Add(Advertising advertising)
        {
            FDIDB.Advertisings.Add(advertising);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="advertising">Xóa bản ghi</param>
        public void Delete(Advertising advertising)
        {
            FDIDB.Advertisings.Remove(advertising);
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
