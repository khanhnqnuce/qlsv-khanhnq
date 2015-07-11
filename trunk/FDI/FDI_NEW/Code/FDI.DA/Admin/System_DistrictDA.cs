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
    public partial class System_DistrictDA : BaseDA
    {
        #region Constructer
        public System_DistrictDA()
        {
        }

        public System_DistrictDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_DistrictDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<DistrictItem> GetAllListSimple()
        {
            var query = from c in FDIDB.System_District
                        orderby c.Name
                        select new DistrictItem
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
        public List<DistrictItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.System_District
                        where (c.Show == isShow)
                        orderby c.Name
                        select new DistrictItem
                                   {
                            ID = c.ID,
                            Name = c.Name.Trim()
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="cityId">Id cua thanh pho can lay</param>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<DistrictItem> GetListByCity(int cityId, bool isShow)
        {
            try
            {
                var query = from c in FDIDB.System_District
                            where c.CityID == cityId && c.Show == isShow
                            orderby c.Name
                            select new DistrictItem
                                       {
                                ID = c.ID,
                                Name = c.Name.Trim()
                            };
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<DistrictItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_District
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new DistrictItem
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
        public List<DistrictItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.System_District
                        orderby c.Name
                        where c.Show == isShow
                        && c.Name.StartsWith(keyword)
                        select new DistrictItem
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
        public List<DistrictItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_District
                        select new DistrictItem
                                   {
                            ID = o.ID,
                            Name = o.Name.Trim(),
                            Show = o.Show,
                            Description = o.Description,
                            CityName = o.System_City.Name.Trim(),
                            CityID = o.System_City.ID
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<DistrictItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_District
                        where ltsArrID.Contains(o.CityID)
                        orderby o.ID descending
                        select new DistrictItem
                                   {
                            ID = o.ID,
                            Name = o.Name.Trim(),
                            Show = o.Show,
                            Description = o.Description,
                            CityName = o.System_City.Name.Trim(),
                            CityID = o.System_City.ID
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public DistrictItem GetDistrictItemById(int districtId)
        {
            var query = from o in FDIDB.System_District
                        where o.ID == districtId
                        select new DistrictItem
                                   {
                            ID = o.ID,
                            Name = o.Name.Trim(),
                            Show = o.Show,
                            Description = o.Description,
                            CityName = o.System_City.Name.Trim(),
                            CityID = o.System_City.ID
                        };
            return query.FirstOrDefault();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_District GetById(int id)
        {
            var query = from c in FDIDB.System_District where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_District> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_District where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemDistrict">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_District systemDistrict)
        {
            var query = from c in FDIDB.System_District where ((c.Name == systemDistrict.Name) && (c.ID != systemDistrict.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_District GetByName(string name)
        {
            var query = from c in FDIDB.System_District where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemDistrict">bản ghi cần thêm</param>
        public void Add(System_District systemDistrict)
        {
            FDIDB.System_District.Add(systemDistrict);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemDistrict">Xóa bản ghi</param>
        public void Delete(System_District systemDistrict)
        {
            FDIDB.System_District.Remove(systemDistrict);
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
