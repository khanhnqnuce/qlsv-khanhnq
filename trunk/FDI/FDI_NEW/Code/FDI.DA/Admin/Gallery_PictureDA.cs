using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Gallery_PictureDA : BaseDA
    {
        #region Constructer
        public Gallery_PictureDA()
        {
        }

        public Gallery_PictureDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Gallery_PictureDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<PictureItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Gallery_Picture
                        where !c.IsDeleted.Value
                        orderby c.Name
                        select new PictureItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.ToList();
        }

        public List<PictureItem> GetListmaxSimple()
        {
            var query = (from c in FDIDB.Gallery_Picture
                         where !c.IsDeleted.Value
                         orderby c.ID descending
                         select new PictureItem
                                    {
                                        ID = c.ID,
                                        Name = c.Name
                                    }).Take(1).ToList();
            return query;
        }
        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<PictureItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Gallery_Picture
                        where (c.IsShow == isShow) && !c.IsDeleted.Value
                        orderby c.Name
                        select new PictureItem
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
        public List<PictureItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Gallery_Picture
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new PictureItem
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
        public List<PictureItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Gallery_Picture
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new PictureItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="ltsIDNotInclude"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<PictureItem> GetListSimpleByRequest(HttpRequestBase httpRequest, List<int> ltsIDNotInclude)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Picture
                        where !ltsIDNotInclude.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new PictureItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated,
                                       Url = o.Folder + o.Url,
                                       AlbumID = o.AlbumID,
                                       Gallery_Album = new GalleryAlbumItem
                                                           {
                                                               Name = o.Gallery_Album.Name,
                                                               NameAscii = o.Gallery_Album.NameAscii
                                                           },

                                       CategoryID = o.CategoryID,
                                       Gallery_Category = new GalleryCategoryItem
                                                              {
                                                                  Name = o.Gallery_Category.Name,
                                                                  NameAscii = o.Gallery_Category.NameAscii
                                                              }

                                   };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.CategoryID == Request.CategoryID);

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();

        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<PictureItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Picture
                        where !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new PictureItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated,
                                       Url = o.Folder + o.Url,
                                       AlbumID = o.AlbumID,
                                       Gallery_Album = new GalleryAlbumItem
                                       {
                                           Name = o.Gallery_Album.Name,
                                           NameAscii = o.Gallery_Album.NameAscii
                                       },

                                       CategoryID = o.CategoryID,
                                       Gallery_Category = new GalleryCategoryItem
                                                              {
                                                                  Name = o.Gallery_Category.Name,
                                                                  NameAscii = o.Gallery_Category.NameAscii
                                                              }
                                   };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.CategoryID == Request.CategoryID);

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<PictureItem> GetListSimpleByArrAlbumID(HttpRequestBase httpRequest, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Picture
                        where ltsArrID.Contains((int)o.AlbumID) && !o.IsDeleted.Value
                        select new PictureItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated,
                                       Url = o.Folder + o.Url,
                                       AlbumID = o.AlbumID,
                                       Gallery_Album = new GalleryAlbumItem
                                       {
                                           Name = o.Gallery_Album.Name,
                                           NameAscii = o.Gallery_Album.NameAscii
                                       },

                                       CategoryID = o.CategoryID,
                                       Gallery_Category = new GalleryCategoryItem
                                       {
                                           Name = o.Gallery_Category.Name,
                                           NameAscii = o.Gallery_Category.NameAscii
                                       }
                                   };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.CategoryID == Request.CategoryID);
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }
        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<PictureItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Gallery_Picture
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.ID descending
                        select new PictureItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated,
                                       Url = o.Folder + o.Url,
                                       AlbumID = o.AlbumID,
                                       Gallery_Album = new GalleryAlbumItem
                                       {
                                           Name = o.Gallery_Album.Name,
                                           NameAscii = o.Gallery_Album.NameAscii
                                       }
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Gallery_Picture GetById(int id)
        {
            var query = from c in FDIDB.Gallery_Picture where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Gallery_Category> GetListCategoryByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Gallery_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Tag> GetListTagByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Tag where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }


        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product> GetListProductByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }


        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Gallery_Picture> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Gallery_Picture where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="galleryPicture">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Gallery_Picture galleryPicture)
        {
            var query = from c in FDIDB.Gallery_Picture where ((c.Name == galleryPicture.Name) && (c.ID != galleryPicture.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Gallery_Picture GetByName(string name)
        {
            var query = from c in FDIDB.Gallery_Picture where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="galleryPicture">bản ghi cần thêm</param>
        public void Add(Gallery_Picture galleryPicture)
        {
            FDIDB.Gallery_Picture.Add(galleryPicture);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="galleryPicture">Xóa bản ghi</param>
        public void Delete(Gallery_Picture galleryPicture)
        {
            FDIDB.Gallery_Picture.Remove(galleryPicture);
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
