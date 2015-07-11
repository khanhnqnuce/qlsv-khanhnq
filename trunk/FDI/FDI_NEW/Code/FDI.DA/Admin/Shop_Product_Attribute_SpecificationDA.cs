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
    public partial class Shop_Product_Attribute_SpecificationDA : BaseDA
    {
        #region Constructer
        public Shop_Product_Attribute_SpecificationDA()
        {
        }

        public Shop_Product_Attribute_SpecificationDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_Product_Attribute_SpecificationDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region tree
        public void BuildTreeViewCheckBox(List<Shop_Product_Attribute_Specification> LtsSource, int CategoryID, bool CheckShow, List<int> LtsValues, ref StringBuilder TreeViewHtml, int productID)
        {
            LtsSource = LtsSource.Where(c => c.IsDeleted == CheckShow).OrderBy(o => o.DisplayOrder).ToList();
            int totalChild = LtsSource.Count();

            for (int i = 0; i < totalChild; i++)
            {
                string sttCheck = string.Empty;
                int attSpecID = LtsSource[i].ID;
                var item = from e in FDIDB.Shop_Product
                           where e.ID == productID
                           select new
                           {
                               ProductAtttSpecID = e.Shop_Product_Attribute_Specification.Where(c => c.ID == attSpecID).Select(c => new ProductAttributeSpecificationItem()
                               {
                                   ID = c.ID,
                               }),
                           };

                if (item.Select(c => c.ProductAtttSpecID).Any())
                {
                    int id = item.Select(c => c.ProductAtttSpecID).FirstOrDefault().Select(c => c.ID).FirstOrDefault();
                    sttCheck = id > 0 ? " checked" : sttCheck;
                }

                TreeViewHtml.Append("<li title=\"" + LtsSource[i].Name + "\" class=\"unselect\" id=\"" + LtsSource[i].ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + LtsSource[i].ID + "\" name=\"Category_" + LtsSource[i].ID + "\" value=\"" + LtsSource[i].ID.ToString() + "\" type=\"checkbox\" title=\"" + LtsSource[i].Name + "\" " + (sttCheck) + "/> ");
                TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(LtsSource[i].Name));
                TreeViewHtml.Append("</span>\r\n");
                TreeViewHtml.Append("<ul>\r\n");
                TreeViewHtml.Append("</ul>\r\n");
                TreeViewHtml.Append("</li>\r\n");
            }
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeSpecificationItem> getListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.IsDeleted.Value == false
                        orderby c.Name
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productAttributeName = c.AttributeID.HasValue ? (from e in FDIDB.Shop_Product_Attribute where e.ID == c.AttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="IsShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeSpecificationItem> getListSimpleAll(bool IsShow)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.IsDeleted.Value == false
                        orderby c.Name
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productAttributeName = c.AttributeID.HasValue ? (from e in FDIDB.Shop_Product_Attribute where e.ID == c.AttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.ToList();
        }


        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductAttributeSpecificationItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.IsDeleted.Value == false && c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productAttributeName = c.AttributeID.HasValue ? (from e in FDIDB.Shop_Product_Attribute where e.ID == c.AttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductAttributeSpecificationItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool IsShow)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.IsDeleted.Value == false && c.Name.Contains(keyword)
                        orderby c.Name
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productAttributeName = c.AttributeID.HasValue ? (from e in FDIDB.Shop_Product_Attribute where e.ID == c.AttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="attribute"> </param>
        /// <param name="keyword"> </param>
        /// <param name="delete"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeSpecificationItem> GetAutocompleByAttribute(int attribute, string keyword, bool delete = false)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.Name.ToLower().Contains(keyword.ToLower()) && c.AttributeID == attribute && c.IsDeleted.Value == delete
                        orderby c.Name
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="Page">Trang hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeSpecificationItem> getListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.IsDeleted.Value == false
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productAttributeName = c.AttributeID.HasValue ? (from e in FDIDB.Shop_Product_Attribute where e.ID == c.AttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="LtsArrID"></param>
        /// <returns></returns>
        public List<ProductAttributeSpecificationItem> getListSimpleByArrID(List<int> LtsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where c.IsDeleted.Value == false && LtsArrID.Contains(c.ID)
                        orderby c.Name
                        select new ProductAttributeSpecificationItem()
                        {
                            ID = c.ID,
                            AttributeID = c.AttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productAttributeName = c.AttributeID.HasValue ? (from e in FDIDB.Shop_Product_Attribute where e.ID == c.AttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        public List<Shop_Product_Attribute_Specification> getListProductAttributeSpecificationByArrID(List<int> LtsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where LtsArrID.Contains(c.ID) && c.IsDeleted.Value == false
                        select c;
            return query.ToList();
        }

        

        public List<Shop_Product_Attribute_Specification> getListSimpleByAttributeID(int AttributeID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification where c.AttributeID == AttributeID select c;
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Attribute_Specification getById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="LtsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product_Attribute_Specification> getListByArrID(List<int> LtsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification where LtsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="Shop_Product_Attribute_Specification">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool checkExits(Shop_Product_Attribute_Specification Shop_Product_Attribute_Specification)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification where ((c.Name == Shop_Product_Attribute_Specification.Name) && (c.ID != Shop_Product_Attribute_Specification.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Attribute_Specification getByName(string name)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="Shop_Product_Attribute_Specification">bản ghi cần thêm</param>
        public void Add(Shop_Product_Attribute_Specification Shop_Product_Attribute_Specification)
        {
            FDIDB.Shop_Product_Attribute_Specification.Add(Shop_Product_Attribute_Specification);
        }

        /// <summary>
        /// add by BienLV 03-03-2014
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="nameSpec"></param>
        /// <returns></returns>
        public int Add(int attribute, string nameSpec)
        {
            var attrSpec = new Shop_Product_Attribute_Specification()
            {
                AttributeID = attribute,
                IsAllowFilter = false,
                Name = nameSpec,
                IsDeleted = false,
                DisplayOrder = 0
            };
            FDIDB.Shop_Product_Attribute_Specification.Add(attrSpec);
            FDIDB.SaveChanges();
            return attrSpec.ID;
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="Shop_Product_Attribute_Specification">Xóa bản ghi</param>
        public void Delete(Shop_Product_Attribute_Specification Shop_Product_Attribute_Specification)
        {
            FDIDB.Shop_Product_Attribute_Specification.Remove(Shop_Product_Attribute_Specification);
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
