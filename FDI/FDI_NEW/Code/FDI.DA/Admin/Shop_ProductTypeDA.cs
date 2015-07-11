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
    public partial class Shop_ProductTypeDA : BaseDA
    {
        #region Constructer
        public Shop_ProductTypeDA()
        {
        }

        public Shop_ProductTypeDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_ProductTypeDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region tree

        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="ltsSource">Toàn bộ danh mục</param>
        /// <param name="categoryIDRemove">ID danh mục select</param>
        /// <param name="checkShow"> </param>
        /// <returns></returns>
        public List<ProductTypeItem> GetAllSelectList(List<ProductTypeItem> ltsSource, int categoryIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsActived).ToList();
            var ltsConvert = new List<ProductTypeItem>
                                 {
                                     new ProductTypeItem
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(ltsSource, 1, string.Empty, categoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="ltsItems"></param>
        /// <param name="RootID"> </param>
        /// <param name="space"></param>
        /// <param name="ltsConvert"></param>
        private void BuildTreeListItem(List<ProductTypeItem> ltsItems, int RootID, string space, int CategoryIDRemove, ref List<ProductTypeItem> ltsConvert)
        {
            space += "---";

            foreach (var t in ltsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                ltsConvert.Add(t);
            }
        }

        public void BuildTreeViewCheckBox(List<ProductTypeItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempCategory = ltsSource.OrderBy(o => o.Name).Where(m => m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsActived == checkShow);

            foreach (var category in tempCategory)
            {
                var countQuery = ltsSource.Where(m => m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.IsActived == checkShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (ltsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsActived)
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
                    if (!category.IsActived)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    treeViewHtml.Append("</span></li>\r\n");
                }
            }
        }

        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductTypeItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_Type
                        where c.IsActived
                        orderby c.Name descending
                        select new ProductTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       IsActived = c.IsActived,
                                       IsHasSize = c.IsHasSize,
                                       IsHasWeight = c.IsHasWeight,
                                       IsHasColor = c.IsHasColor,
                                       IsHasBrand = c.IsHasBrand,
                                       Description = c.Description,
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductTypeItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_Type
                        where (c.IsActived == isShow)
                        orderby c.Name
                        select new ProductTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       IsActived = c.IsActived,
                                       IsHasSize = c.IsHasSize,
                                       IsHasWeight = c.IsHasWeight,
                                       IsHasColor = c.IsHasColor,
                                       IsHasBrand = c.IsHasBrand,
                                       Description = c.Description,
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductTypeItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product_Type
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       IsActived = c.IsActived,
                                       IsHasSize = c.IsHasSize,
                                       IsHasWeight = c.IsHasWeight,
                                       IsHasColor = c.IsHasColor,
                                       IsHasBrand = c.IsHasBrand,
                                       Description = c.Description,
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
        public List<ProductTypeItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_Type
                        where c.IsActived == isShow && c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       IsActived = c.IsActived,
                                       IsHasSize = c.IsHasSize,
                                       IsHasWeight = c.IsHasWeight,
                                       IsHasColor = c.IsHasColor,
                                       IsHasBrand = c.IsHasBrand,
                                       Description = c.Description,
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductTypeItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_Type
                        select new ProductTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       IsActived = c.IsActived,
                                       IsHasSize = c.IsHasSize,
                                       IsHasWeight = c.IsHasWeight,
                                       IsHasColor = c.IsHasColor,
                                       IsHasBrand = c.IsHasBrand,
                                       Description = c.Description,

                                       ProductTypeTotalProducts = c.Shop_Product.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ProductTypeItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Type
                        where ltsArrID.Contains(c.ID)
                        orderby c.Name
                        select new ProductTypeItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       IsActived = c.IsActived,
                                       IsHasSize = c.IsHasSize,
                                       IsHasWeight = c.IsHasWeight,
                                       IsHasColor = c.IsHasColor,
                                       IsHasBrand = c.IsHasBrand,
                                       Description = c.Description,

                                       ProductTypeTotalProducts = c.Shop_Product.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        public List<Shop_Product_Type> GetListProductType()
        {
            return FDIDB.Shop_Product_Type.ToList();
        }

        #region Check Exits, Add, Update, Delete

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="productTypeId"> </param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Type GetById(int productTypeId)
        {
            var query = from c in FDIDB.Shop_Product_Type where c.ID == productTypeId select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product_Type> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Type where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopProductType">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Product_Type shopProductType)
        {
            var query = from c in FDIDB.Shop_Product_Type where ((c.Name == shopProductType.Name) && (c.ID != shopProductType.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="brandName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Type GetByName(string brandName)
        {
            var query = from c in FDIDB.Shop_Product_Type where ((c.Name == brandName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="shopProductType">bản ghi cần thêm</param>
        public void Add(Shop_Product_Type shopProductType)
        {
            FDIDB.Shop_Product_Type.Add(shopProductType);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="shopProductType">Xóa bản ghi</param>
        public void Delete(Shop_Product_Type shopProductType)
        {
            FDIDB.Shop_Product_Type.Remove(shopProductType);
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
