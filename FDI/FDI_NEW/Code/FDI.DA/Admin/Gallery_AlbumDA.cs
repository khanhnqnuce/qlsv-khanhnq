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
    public partial class Gallery_AlbumDA : BaseDA
    {
        #region Constructer
        public Gallery_AlbumDA()
        {
        }

        public Gallery_AlbumDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Gallery_AlbumDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion


        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AlbumItem> GetAllListSimple(bool isShow)
        {
            var query = from c in FDIDB.Gallery_Album
                        where c.IsShow == isShow && !c.IsDeleted.Value
                        orderby c.DateCreated descending
                        select new AlbumItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AlbumItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Gallery_Album
                        where !c.IsDeleted.Value
                        orderby c.DateCreated descending
                        select new AlbumItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.ToList();
        }
        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AlbumItem> GetAllListVideoSimple()
        {
            var query = from c in FDIDB.Gallery_Album
                        where c.IsVideo && !c.IsDeleted.Value
                        orderby c.DateCreated descending
                        select new AlbumItem
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
        public List<AlbumItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Gallery_Album
                        where (c.IsShow == isShow) && !c.IsDeleted.Value && c.IsVideo
                        orderby c.DateCreated descending
                        select new AlbumItem
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
        public List<AlbumItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Gallery_Album
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new AlbumItem
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
        public List<AlbumItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Gallery_Album
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new AlbumItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.Take(showLimit).ToList();
        }

        public List<PictureItem> GetListSimpleByRequest(HttpRequestBase httpRequest, List<int> ltsIDNotInclude)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Picture
                        where !ltsIDNotInclude.Contains(o.ID) && !o.IsDeleted.Value
                        select new PictureItem
                        {
                            ID = o.ID,
                            Name = o.Name,
                            IsShow = o.IsShow,
                            Description = o.Description,
                            DateCreated = o.DateCreated,
                            Url = o.Url,
                            AlbumID = o.AlbumID,

                            Gallery_Album = new GalleryAlbumItem
                                                {
                                                    Name = o.Gallery_Album.Name,
                                                    NameAscii = o.Gallery_Album.NameAscii,
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

        public List<AlbumItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Album
                        where !o.IsDeleted.Value
                        select new AlbumItem
                        {
                            ID = o.ID,
                            Name = o.Name,
                            NameAscii = o.NameAscii,
                            IsShow = o.IsShow,
                            Description = o.Description,
                            Gallery_Category = o.Gallery_Category.Select(c => new GalleryCategoryItem
                            {
                                ID = c.ID,
                                Name = c.Name,
                                NameAscii = o.NameAscii
                            }),
                            TotalPictures = o.Gallery_Picture.Count()
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AlbumItem> GetListSimpleByRequest(HttpRequestBase httpRequest, bool albumIsVideo)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Gallery_Album
                        where !o.IsDeleted.Value && o.IsVideo == albumIsVideo
                        //let galleryPicture = o.Gallery_Picture.FirstOrDefault()
                        //where galleryPicture != null
                        select new AlbumItem
                        {
                            ID = o.ID,
                            Name = o.Name,
                            NameAscii = o.NameAscii,
                            IsShow = o.IsShow,
                            Description = o.Description,

                            //Gallery_Picture = new PictureItem
                            //                      {
                            //                          ID = galleryPicture.ID,
                            //                          Url = galleryPicture.Url,
                            //                      },
                            Gallery_Category = o.Gallery_Category.Select(c => new GalleryCategoryItem
                            {
                                ID = c.ID,
                                Name = c.Name,
                                NameAscii = o.NameAscii
                            }),
                            TotalPictures = o.Gallery_Picture.Count()
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <param name="albumIsVideo"> </param>
        /// <returns></returns>
        public List<AlbumItem> GetListSimpleByArrID(List<int> ltsArrID, bool albumIsVideo = false)
        {
            var query = from o in FDIDB.Gallery_Album
                        where ltsArrID.Contains(o.ID) && !o.IsDeleted.Value && o.IsVideo == albumIsVideo
                        orderby o.ID descending
                        //let galleryPicture = o.Gallery_Picture.FirstOrDefault()
                        //where galleryPicture != null
                        select new AlbumItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       NameAscii = o.NameAscii,
                                       IsShow = o.IsShow,
                                       ImagesID = o.ImagesID,
                                       Description = o.Description,
                                       //Gallery_Picture = new PictureItem
                                       //                      {
                                       //                          Url = galleryPicture.Url,
                                       //                      },
                                       Gallery_Category = o.Gallery_Category.Select(c => new GalleryCategoryItem
                                                                                             {
                                                                                                 ID = c.ID,
                                                                                                 Name = c.Name,
                                                                                                 NameAscii = c.NameAscii
                                                                                             }),
                                       TotalPictures = o.Gallery_Picture.Count()
                                   };
            //query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Gallery_Album GetById(int id)
        {
            var query = from c in FDIDB.Gallery_Album where c.ID == id select c;
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
        public List<Gallery_Album> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Gallery_Album where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="galleryAlbum">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Gallery_Album galleryAlbum)
        {
            var query = from c in FDIDB.Gallery_Album where ((c.Name == galleryAlbum.Name) && (c.ID != galleryAlbum.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Gallery_Album GetByName(string name)
        {
            var query = from c in FDIDB.Gallery_Album where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="galleryAlbum">bản ghi cần thêm</param>
        public void Add(Gallery_Album galleryAlbum)
        {
            FDIDB.Gallery_Album.Add(galleryAlbum);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="galleryAlbum">Xóa bản ghi</param>
        public void Delete(Gallery_Album galleryAlbum)
        {
            FDIDB.Gallery_Album.Remove(galleryAlbum);
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
