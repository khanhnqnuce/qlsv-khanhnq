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
    public partial class System_ColorDA : BaseDA
    {
        #region Constructer
        public System_ColorDA()
        {
        }

        public System_ColorDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_ColorDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ColorItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.System_Color
                        orderby c.Name
                        select new ColorItem
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
        public List<ColorItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_Color
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new ColorItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="Page">Trang hiển thị</param>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ColorItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_Color
                        select new ColorItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       Value = o.Value,
                                       IsShow = o.IsShow,
                                       Description = o.Description,
                                       //TotalProduct = o.Shop_Product.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ColorItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_Color
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new ColorItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name,
                                       Value = o.Value,
                                       IsShow = o.IsShow,
                                       Description = o.Description
                                       //,
                                       //ColorTotalProduct = o.Shop_Product.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="colorID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Color GetById(int colorID)
        {
            var query = from c in FDIDB.System_Color where c.ID == colorID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Color> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Color where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemColor">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_Color systemColor)
        {
            var query = from c in FDIDB.System_Color where ((c.Name == systemColor.Name) && (c.ID != systemColor.ID)) select c.ID;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="colorName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Color GetByName(string colorName)
        {
            var query = from c in FDIDB.System_Color where ((c.Name == colorName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemColor">bản ghi cần thêm</param>
        public void Add(System_Color systemColor)
        {
            FDIDB.System_Color.Add(systemColor);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemColor">Xóa bản ghi</param>
        public void Delete(System_Color systemColor)
        {
            FDIDB.System_Color.Remove(systemColor);
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
