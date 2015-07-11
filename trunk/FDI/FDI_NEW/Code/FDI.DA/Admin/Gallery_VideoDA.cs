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
    public partial class Gallery_VideoDA : BaseDA
    {
        #region Constructer
        public Gallery_VideoDA()
        {
        }

        public Gallery_VideoDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Gallery_VideoDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<VideoItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Gallery_Video
                        where !c.IsDeleted.Value
                        orderby c.Name

                        select new VideoItem
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
        public List<VideoItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Gallery_Video
                        where (c.IsShow == isShow) && !c.IsDeleted.Value
                        orderby c.IsShow
                        select new VideoItem
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
        public List<VideoItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Gallery_Video
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new VideoItem
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
        public List<VideoItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Gallery_Video
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new VideoItem
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
        public List<VideoItem> GetListSimpleByRequest(HttpRequestBase httpRequest, List<int> ltsIDNotInclude)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Video
                        where !ltsIDNotInclude.Contains(o.ID) && !o.IsDeleted.Value
                        select new VideoItem
                                   {

                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow.Value,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated.Value,
                                       Url = o.Url,
                                       AlbumID = o.AlbumID,
                                       GalleryAlbum = new GalleryAlbumItem
                                                           {
                                                               Name = o.Gallery_Album.Name,
                                                               NameAscii = o.Gallery_Album.NameAscii
                                                           },
                                       PictureID = o.PictureID,
                                       GalleryPicture = new GalleryPictureItem
                                                             {
                                                                 Name = o.Gallery_Picture.Name,
                                                                 Url = o.Gallery_Picture.Url
                                                             },
                                       CategoryID = o.CategoryID,
                                       GalleryCategory = new GalleryCategoryItem
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
        /// <returns>Danh sách bản ghi</returns>
        public List<VideoItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Video
                        where !o.IsDeleted.Value
                        orderby o.DateCreated descending
                        select new VideoItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow.Value,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated.Value,
                                       Url = o.Url,
                                       AlbumID = o.AlbumID,
                                       GalleryAlbum = new GalleryAlbumItem
                                       {
                                           Name = o.Gallery_Album.Name,
                                           NameAscii = o.Gallery_Album.NameAscii
                                       },
                                       PictureID = o.PictureID,
                                       GalleryPicture = new GalleryPictureItem
                                       {
                                           Name = o.Gallery_Picture.Name,
                                           Url = o.Gallery_Picture.Url
                                       },
                                       CategoryID = o.CategoryID,
                                       GalleryCategory = new GalleryCategoryItem
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
        public List<VideoItem> GetListSimpleByArrAlbumID(HttpRequestBase httpRequest, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Video
                        where ltsArrID.Contains((int)o.AlbumID) && !o.IsDeleted.Value
                        select new VideoItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow.Value,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated.Value,
                                       Url = o.Url,
                                       AlbumID = o.AlbumID,
                                       GalleryAlbum = new GalleryAlbumItem
                                       {
                                           Name = o.Gallery_Album.Name,
                                           NameAscii = o.Gallery_Album.NameAscii
                                       },
                                       PictureID = o.PictureID,
                                       GalleryPicture = new GalleryPictureItem
                                       {
                                           Name = o.Gallery_Picture.Name,
                                           Url = o.Gallery_Picture.Url
                                       },
                                       CategoryID = o.CategoryID,
                                       GalleryCategory = new GalleryCategoryItem
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
        public List<VideoItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Gallery_Video
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value
                        orderby o.ID descending
                        select new VideoItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       IsShow = o.IsShow.Value,
                                       Description = o.Description,
                                       DateCreated = o.DateCreated.Value,
                                       Url = o.Url,
                                       AlbumID = o.AlbumID,
                                       GalleryAlbum = new GalleryAlbumItem
                                       {
                                           Name = o.Gallery_Album.Name,
                                           NameAscii = o.Gallery_Album.NameAscii
                                       },
                                       PictureID = o.PictureID,
                                       GalleryPicture = new GalleryPictureItem
                                       {
                                           Name = o.Gallery_Picture.Name,
                                           Url = o.Gallery_Picture.Url
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
        public Gallery_Video GetById(int id)
        {
            var query = from c in FDIDB.Gallery_Video where c.ID == id select c;
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

        public Gallery_Category GetGalleryCategoryByID(int id)
        {
            var query = from c in FDIDB.Gallery_Category where c.ID == id select c;
            return query.FirstOrDefault();
        }

        public Gallery_Album GetGalleryAlbumByID(int id)
        {
            var query = from c in FDIDB.Gallery_Album where c.ID == id select c;
            return query.FirstOrDefault();
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
        public List<Gallery_Video> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Gallery_Video where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="galleryVideo">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Gallery_Video galleryVideo)
        {
            var query = from c in FDIDB.Gallery_Video where ((c.Name == galleryVideo.Name) && (c.ID != galleryVideo.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Gallery_Video GetByName(string name)
        {
            var query = from c in FDIDB.Gallery_Video where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="galleryVideo">bản ghi cần thêm</param>
        public void Add(Gallery_Video galleryVideo)
        {
            FDIDB.Gallery_Video.Add(galleryVideo);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="galleryVideo">Xóa bản ghi</param>
        public void Delete(Gallery_Video galleryVideo)
        {
            FDIDB.Gallery_Video.Remove(galleryVideo);
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
