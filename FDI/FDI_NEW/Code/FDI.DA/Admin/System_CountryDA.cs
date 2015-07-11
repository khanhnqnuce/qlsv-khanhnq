using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class System_CountryDA : BaseDA
    {
        #region Constructer
        public System_CountryDA()
        {
        }

        public System_CountryDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_CountryDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<CountryItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.System_Country
                        orderby c.Name
                        select new CountryItem
                                   {
                                       ID = c.ID,
                                       Code = c.Code,
                                       Name = c.Name
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CountryItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.System_Country
                        where (c.Show == isShow)
                        orderby c.Name
                        select new CountryItem
                                   {
                                       ID = c.ID,
                                       Code = c.Code,
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
        public List<CountryItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_Country
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new CountryItem
                                   {
                                       ID = c.ID,
                                       Code = c.Code,
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
        public List<CountryItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.System_Country
                        orderby c.Name
                        where c.Show == isShow
                        && c.Name.StartsWith(keyword)
                        select new CountryItem
                                   {
                                       ID = c.ID,
                                       Code = c.Code,
                                       Name = c.Name
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<CountryItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_Country
                        select new CountryItem
                                   {
                                       ID = o.ID,
                                       Code = o.Code,
                                       Name = o.Name,
                                       Show = o.Show,
                                       Description = o.Description
                                       //CountryTotalProducts = o.Shop_Product.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<CountryItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_Country
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new CountryItem
                                   {
                                       ID = o.ID,
                                       Code = o.Code,
                                       Name = o.Name,
                                       Show = o.Show,
                                       Description = o.Description
                                       //CountryTotalProducts = o.Shop_Product.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public CountryItem GetCountryItemById(int countryId)
        {
            var query = from o in FDIDB.System_Country
                        where o.ID == countryId
                        orderby o.ID descending
                        select new CountryItem
                                   {
                                       ID = o.ID,
                                       Code = o.Code,
                                       Name = o.Name,
                                       Show = o.Show,
                                       Description = o.Description
                                       //CountryTotalProducts = o.Shop_Product.Count()
                                   };
            return query.FirstOrDefault();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Country GetById(int id)
        {
            var query = from c in FDIDB.System_Country where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Country> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Country where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemCountry">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_Country systemCountry)
        {
            var query = from c in FDIDB.System_Country where ((c.Name == systemCountry.Name) && (c.ID != systemCountry.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Country GetByName(string name)
        {
            var query = from c in FDIDB.System_Country where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemCountry">bản ghi cần thêm</param>
        public void Add(System_Country systemCountry)
        {
            FDIDB.System_Country.Add(systemCountry);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemCountry">Xóa bản ghi</param>
        public void Delete(System_Country systemCountry)
        {
            FDIDB.System_Country.Remove(systemCountry);
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
