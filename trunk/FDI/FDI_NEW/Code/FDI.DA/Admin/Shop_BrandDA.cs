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
    public partial class Shop_BrandDA : BaseDA
    {
        #region Constructer
        public Shop_BrandDA()
        {
        }

        public Shop_BrandDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_BrandDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region tree
        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="LtsSource">Toàn bộ danh mục</param>
        /// <param name="CategoryIDRemove">ID danh mục select</param>
        /// <returns></returns>
        public List<ProductBrandItem> GetAllSelectList(List<ProductBrandItem> LtsSource, int CategoryIDRemove, bool checkShow)
        {
            if (checkShow)
                LtsSource = LtsSource.Where(o => o.IsShow).ToList();
            var ltsConvert = new List<ProductBrandItem>
                                 {
                                     new ProductBrandItem
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(LtsSource, 1, string.Empty, CategoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="LtsItems"></param>
        /// <param name="space"></param>
        /// <param name="LtsConvert"></param>
        private void BuildTreeListItem(List<ProductBrandItem> LtsItems, int RootID, string space, int CategoryIDRemove, ref List<ProductBrandItem> LtsConvert)
        {
            space += "---";

            foreach (var t in LtsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                LtsConvert.Add(t);
            }
        }

        #endregion

        /// <summary>
        /// Hàm build ra treeview có checkbox chứa danh sách category
        /// </summary>
        /// <param name="ltsSource"> </param>
        /// <param name="categoryID"> </param>
        /// <param name="checkShow"> </param>
        /// <param name="ltsValues"> </param>
        /// <param name="treeViewHtml"> </param>
        public void BuildTreeViewCheckBox(List<ProductBrandItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempCategory = ltsSource.OrderBy(o => o.Order).Where(m => m.ID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (var category in tempCategory)
            {
                var countQuery = ltsSource.Where(m => m.ID == category.ID && m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.IsShow == checkShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (ltsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsShow)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox(ltsSource, category.ID, checkShow, ltsValues, ref treeViewHtml);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (ltsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsShow)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    treeViewHtml.Append("</span></li>\r\n");
                }
            }
        }


        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductBrandItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Brand
                        orderby c.Name
                        select new ProductBrandItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Link = c.Link,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShow = c.IsShow,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       LogoPictureID = c.LogoPictureID.HasValue ? c.LogoPictureID.Value : 0,
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductBrandItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Brand
                        where (c.IsShow == isShow)
                        orderby c.Name
                        select new ProductBrandItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Link = c.Link,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShow = c.IsShow,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       LogoPictureID = c.LogoPictureID.HasValue ? c.LogoPictureID.Value : 0,
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductBrandItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Brand
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductBrandItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Link = c.Link,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShow = c.IsShow,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       LogoPictureID = c.LogoPictureID.HasValue ? c.LogoPictureID.Value : 0,
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
        public List<ProductBrandItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Brand
                        where c.IsShow == isShow && c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductBrandItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Link = c.Link,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShow = c.IsShow,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       LogoPictureID = c.LogoPictureID.HasValue ? c.LogoPictureID.Value : 0,
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductBrandItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Brand
                        select new ProductBrandItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Link = c.PictureID.HasValue ? (from e in FDIDB.Gallery_Picture where c.PictureID.Value == e.ID select e.Url).FirstOrDefault() : string.Empty,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShow = c.IsShow,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       LogoPictureID = c.LogoPictureID.HasValue ? c.LogoPictureID.Value : 0,
                                       BrandTotalProduct = c.Shop_Product.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ProductBrandItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Brand
                        where ltsArrID.Contains(c.ID)
                        orderby c.Order
                        select new ProductBrandItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Link = c.PictureID.HasValue ? (from e in FDIDB.Gallery_Picture where c.PictureID.Value == e.ID select e.Url).FirstOrDefault() : string.Empty,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShow = c.IsShow,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       LogoPictureID = c.LogoPictureID.HasValue ? c.LogoPictureID.Value : 0,
                                       BrandTotalProduct = c.Shop_Product.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="brandID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Brand GetById(int brandID)
        {
            var query = from c in FDIDB.Shop_Brand where c.ID == brandID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Brand> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Brand where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopBrand">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Brand shopBrand)
        {
            var query = from c in FDIDB.Shop_Brand where ((c.Name == shopBrand.Name) && (c.ID != shopBrand.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="brandName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Brand GetByName(string brandName)
        {
            var query = from c in FDIDB.Shop_Brand where ((c.Name == brandName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="shopBrand">bản ghi cần thêm</param>
        public void Add(Shop_Brand shopBrand)
        {
            FDIDB.Shop_Brand.Add(shopBrand);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="shopBrand">Xóa bản ghi</param>
        public void Delete(Shop_Brand shopBrand)
        {
            FDIDB.Shop_Brand.Remove(shopBrand);
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
