using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Advertising_TypeDA : BaseDA
    {
        #region Constructer
        public Advertising_TypeDA()
        {
        }

        public Advertising_TypeDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Advertising_TypeDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingTypeItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Advertising_Type
                        orderby c.Name
                        select new AdvertisingTypeItem
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
        public List<AdvertisingTypeItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Advertising_Type
                        where !c.IsDeleted.Value
                        orderby c.Name

                        select new AdvertisingTypeItem
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
        public List<AdvertisingTypeItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Advertising_Type
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new AdvertisingTypeItem
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
        public List<AdvertisingTypeItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Advertising_Type
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new AdvertisingTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingTypeItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Advertising_Type //select o;
                        select new AdvertisingTypeItem
                        {
                            ID = o.ID,
                            Name = o.Name

                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        public Advertising_Type GetById(int advertisingID)
        {
            var query = from c in FDIDB.Advertising_Type where c.ID == advertisingID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        public List<Advertising_Type> GetListByArrId(List<int> ltArrID)
        {
            var query = from c in FDIDB.Advertising_Type where ltArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        public bool CheckExits(Advertising_Type template)
        {
            var query = from c in FDIDB.Advertising_Type where ((c.Name == template.Name) && (c.ID != template.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <returns>Bản ghi</returns>
        public Advertising_Type GetByName(string templateName)
        {
            var query = from c in FDIDB.Advertising_Type where ((c.Name == templateName)) select c;
            return query.FirstOrDefault();
        }


        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        public void Add(Advertising_Type template)
        {
            FDIDB.Advertising_Type.Add(template);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        public void Delete(Advertising_Type template)
        {
            FDIDB.Advertising_Type.Remove(template);
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
