using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class System_FileDA : BaseDA
    {
        #region Constructer
        public System_FileDA()
        {
        }

        public System_FileDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_FileDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<FileItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.System_File
                        orderby c.Name
                        select new FileItem
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
        public List<FileItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_File
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new FileItem
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
        /// <returns>Danh sách bản ghi</returns>
        public List<FileItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_File
                        select new FileItem
                                   {
                            ID = o.ID,
                            Name = o.Name,
                            Data = o.Data,
                            TypeName = o.System_FileType.Name,
                            TypeIcon = o.System_FileType.Icon,
                            TotalGuide = o.Guide_Guide.Count(),
                            TotalNews = o.News_News.Count(),
                            TotalProduct = o.Shop_Product.Count(),
                            TotalAnswer = o.FAQ_Answer.Count()
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<FileItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_File
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new FileItem
                                   {
                            ID = o.ID,
                            Name = o.Name,
                            Data = o.Data,
                            TypeName = o.System_FileType.Name,
                            TypeIcon = o.System_FileType.Icon,
                            TotalGuide = o.Guide_Guide.Count(),
                            TotalNews = o.News_News.Count(),
                            TotalProduct = o.Shop_Product.Count(),
                            TotalAnswer = o.FAQ_Answer.Count()
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_File GetById(int id)
        {
            var query = from c in FDIDB.System_File where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_File> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_File where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemFile">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_File systemFile)
        {
            var query = from c in FDIDB.System_File where ((c.Name == systemFile.Name) && (c.ID != systemFile.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_File GetByName(string name)
        {
            var query = from c in FDIDB.System_File where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemFile">bản ghi cần thêm</param>
        public void Add(System_File systemFile)
        {
            FDIDB.System_File.Add(systemFile);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemFile">Xóa bản ghi</param>
        public void Delete(System_File systemFile)
        {
            FDIDB.System_File.Remove(systemFile);
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
