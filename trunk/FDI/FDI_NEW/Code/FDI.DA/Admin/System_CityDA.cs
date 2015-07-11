using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class System_CityDA : BaseDA
    {
        #region Constructer
        public System_CityDA()
        {
        }

        public System_CityDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_CityDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<CityItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.System_City
                        orderby c.Name
                        select new CityItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CityItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.System_City
                        where (c.Show == isShow)
                        orderby c.Name
                        select new CityItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<CityItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_City
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new CityItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
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
        public List<CityItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.System_City
                        orderby c.Name
                        where c.Show == isShow
                        && c.Name.StartsWith(keyword)
                        select new CityItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CityItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_City
                        select new CityItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name.Trim(),
                                       Show = o.Show,
                                       Description = o.Description,
                                       TotalDistricts = o.System_District.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<CityItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_City
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new CityItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name.Trim(),
                                       Show = o.Show,
                                       Description = o.Description,
                                       TotalDistricts = o.System_District.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public CityItem GetCityItemById(int cityId)
        {
            var query = from o in FDIDB.System_City
                        where o.ID == cityId
                        orderby o.ID descending
                        select new CityItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name.Trim(),
                                       Show = o.Show,
                                       Description = o.Description,
                                       TotalDistricts = o.System_District.Count()
                                   };
            return query.FirstOrDefault();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="cityID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_City GetById(int cityID)
        {
            var query = from c in FDIDB.System_City where c.ID == cityID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_City> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_City where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemCity">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_City systemCity)
        {
            var query = from c in FDIDB.System_City where ((c.Name == systemCity.Name) && (c.ID != systemCity.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="cityName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_City GetByName(string cityName)
        {
            var query = from c in FDIDB.System_City where ((c.Name == cityName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemCity">bản ghi cần thêm</param>
        public void Add(System_City systemCity)
        {
            FDIDB.System_City.Add(systemCity);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemCity">Xóa bản ghi</param>
        public void Delete(System_City systemCity)
        {
            FDIDB.System_City.Remove(systemCity);
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
