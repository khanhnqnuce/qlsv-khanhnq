using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Advertising_PositionDA : BaseDA
    {
        #region Constructer
        public Advertising_PositionDA()
        {
        }

        public Advertising_PositionDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Advertising_PositionDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingPositionItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Advertising_Position
                        orderby c.Name
                        select new AdvertisingPositionItem
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
        public List<AdvertisingPositionItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Advertising_Position
                        where !c.IsDeleted.Value
                        orderby c.Name

                        select new AdvertisingPositionItem
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
        public List<AdvertisingPositionItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Advertising_Position
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new AdvertisingPositionItem
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
        public List<AdvertisingPositionItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Advertising_Position
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && !c.IsDeleted.Value
                        select new AdvertisingPositionItem
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
        public List<AdvertisingPositionItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Advertising_Position
                        orderby o.ID
                        where o.IsDeleted == false
                        select new AdvertisingPositionItem
                        {
                            Name = o.Name,
                            ID = o.ID
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        public Advertising_Position GetById(int positionID)
        {
            var query = from c in FDIDB.Advertising_Position where c.ID == positionID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        public List<Advertising_Position> GetListByArrId(List<int> ltArrID)
        {
            var query = from c in FDIDB.Advertising_Position where ltArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        public bool CheckExits(Advertising_Position template)
        {
            var query = from c in FDIDB.Advertising_Position where ((c.Name == template.Name) && (c.ID != template.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <returns>Bản ghi</returns>
        public Advertising_Position GetByName(string templateName)
        {
            var query = from c in FDIDB.Advertising_Position where ((c.Name == templateName)) select c;
            return query.FirstOrDefault();
        }


        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        public void Add(Advertising_Position template)
        {
            FDIDB.Advertising_Position.Add(template);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        public void Delete(Advertising_Position template)
        {
            FDIDB.Advertising_Position.Remove(template);
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
