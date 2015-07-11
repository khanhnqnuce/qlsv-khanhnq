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
    public partial class Shop_CategoryDA : BaseDA
    {
        #region Constructer
        public Shop_CategoryDA()
        {
        }

        public Shop_CategoryDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_CategoryDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region các function lấy đệ quy
        public List<ProductCategoryItem> GetAllSelectListForProduct(List<ProductCategoryItem> ltsSource, int categoryIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(c => c.IsPublish && c.IsDelete == false).ToList();
            var ltsConvert = new List<ProductCategoryItem>
                                 {
                                     new ProductCategoryItem
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItemForProduct(ltsSource, 1, string.Empty, categoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        private void BuildTreeListItemForProduct(List<ProductCategoryItem> LtsItems, int RootID, string space, int CategoryIDRemove, ref List<ProductCategoryItem> LtsConvert)
        {
            //space += "---";
            //var ltsChils = LtsItems.Where(o => o.ParentID == RootID && o.ID != CategoryIDRemove && o.IsDelete == false).OrderBy(o => o.Order).ToList();
            //if (ltsChils.Count > 0)
            //{
            //    foreach (var currentItem in ltsChils)
            //    {
            //        currentItem.Name = string.Format("|{0} {1}", space, currentItem.Name);
            //        LtsConvert.Add(currentItem);
            //        BuildTreeListItem(LtsItems, currentItem.ID, space, CategoryIDRemove, ref LtsConvert);
            //    }
            //}

            space += "---";

            foreach (var t in LtsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                LtsConvert.Add(t);
            }
        }


        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="ltsSource">Toàn bộ danh mục</param>
        /// <param name="categoryIDRemove">ID danh mục select</param>
        /// <param name="checkShow"> </param>
        /// <returns></returns>
        public List<ProductCategoryItem> GetAllSelectList(List<ProductCategoryItem> ltsSource, int categoryIDRemove, bool checkShow)
        {

            var ltsConvert = new List<ProductCategoryItem>
                                 {
                                     new ProductCategoryItem
                                         {
                                             ID = 0,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(ltsSource, 0, string.Empty, categoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="LtsItems"></param>
        /// <param name="space"></param>
        /// <param name="LtsConvert"></param>
        private void BuildTreeListItem(List<ProductCategoryItem> LtsItems, int RootID, string space, int CategoryIDRemove, ref List<ProductCategoryItem> LtsConvert)
        {

            space += "---";
            var ltsChils = LtsItems.Where(o => o.ParentID == RootID && o.ID != CategoryIDRemove).ToList();
            if (ltsChils.Count > 0)
            {
                foreach (var currentItem in ltsChils)
                {
                    currentItem.Name = string.Format("|{0} {1}", space, currentItem.Name);
                    LtsConvert.Add(currentItem);
                    BuildTreeListItem(LtsItems, currentItem.ID, space, CategoryIDRemove, ref LtsConvert);
                }
            }
        }

        /// <summary>
        /// Hàm build ra treeview có checkbox chứa danh sách category
        /// </summary>
        /// <param name="MaDonVi"></param>
        public void BuildTreeViewCheckBox(List<ProductCategoryItem> LtsSource, int CategoryID, bool CheckShow, List<int> LtsValues, ref StringBuilder TreeViewHtml)
        {
            IEnumerable<ProductCategoryItem> tempCategory = LtsSource.OrderBy(o => o.Order).Where(m => m.ParentID == CategoryID && m.ID > 1 && m.IsDelete == false);
            if (CheckShow)
                tempCategory = tempCategory.Where(m => m.IsPublish == CheckShow);

            foreach (ProductCategoryItem category in tempCategory)
            {
                var countQuery = LtsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
                if (CheckShow)
                    countQuery = countQuery.Where(m => m.IsPublish == CheckShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (LtsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsPublish)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox(LtsSource, category.ID, CheckShow, LtsValues, ref TreeViewHtml);
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (LtsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsPublish)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</span></li>\r\n");
                }
            }
        }

        public void BuildTreeViewCheckBoxNoRoot(List<ProductCategoryItem> LtsSource, int CategoryID, bool CheckShow, List<int> LtsValues, ref StringBuilder TreeViewHtml)
        {
            IEnumerable<ProductCategoryItem> tempCategory = LtsSource.OrderBy(o => o.Order).Where(m => m.ParentID == CategoryID && m.IsDelete == false);
            if (CheckShow)
                tempCategory = tempCategory.Where(m => m.IsPublish == CheckShow);

            foreach (ProductCategoryItem category in tempCategory)
            {
                var countQuery = LtsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
                if (CheckShow)
                    countQuery = countQuery.Where(m => m.IsPublish == CheckShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (LtsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsPublish)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox(LtsSource, category.ID, CheckShow, LtsValues, ref TreeViewHtml);
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (LtsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsPublish)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</span></li>\r\n");
                }
            }
        }
        /// <summary>
        /// Hàm build ra treeview chứa danh sách category
        /// </summary>
        /// <param name="MaDonVi"></param>
        public void BuildTreeView(List<ProductCategoryItem> LtsSource, int CategoryID, bool CheckShow, ref StringBuilder TreeViewHtml)
        {
            IEnumerable<ProductCategoryItem> tempCategory = LtsSource.OrderBy(o => o.Order).Where(m => m.ParentID == CategoryID && m.ID >= 1 && m.IsDelete == false);
            if (CheckShow)
                tempCategory = tempCategory.Where(m => m.IsPublish == CheckShow);

            foreach (ProductCategoryItem category in tempCategory)
            {
                var countQuery = LtsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
                if (CheckShow)
                    countQuery = countQuery.Where(m => m.IsPublish == CheckShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!category.IsPublish)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</a>\r\n");
                    TreeViewHtml.AppendFormat(" <i>({0})</i>\r\n", totalChild);
                    TreeViewHtml.Append(buildEditToolByID(category) + "\r\n");
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    BuildTreeView(LtsSource, category.ID, CheckShow, ref TreeViewHtml);
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!category.IsPublish)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</a> <i>(0)</i>" + buildEditToolByID(category) + "</span></li>\r\n");
                }
            }
        }

        /// Replace for upper function
        /// <summary>
        /// Build ra editor cho từng categoryitem
        /// </summary>
        /// <param name="categoryItem"> </param>
        /// <returns></returns>
        private string buildEditToolByID(ProductCategoryItem categoryItem)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");
            strTool.AppendFormat("    <a title=\"Thêm mới category: {1}\" class=\"add\" href=\"#{0}\">\r\n", categoryItem.ID, categoryItem.Name);
            strTool.Append("        <img border=\"0\" title=\"Thêm mới category\" src=\"/Content/Admin/images/gridview/add.gif\">\r\n");
            strTool.Append("    </a>");
            strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" class=\"edit\" href=\"#{0}\">\r\n", categoryItem.ID, categoryItem.Name);
            strTool.Append("        <img border=\"0\" title=\"Sửa category\" src=\"/Content/Admin/images/gridview/edit.gif\">\r\n");
            strTool.Append("    </a>");
            if (categoryItem.IsPublish)
            {
                strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\" class=\"hide\">\r\n", categoryItem.ID, categoryItem.Name);
                strTool.Append("        <img border=\"0\" title=\"Đang hiển thị\" src=\"/Content/Admin/images/gridview/show.gif\">\r\n");
                strTool.Append("    </a>\r\n");
            }
            else
            {
                strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\" class=\"show\">\r\n", categoryItem.ID, categoryItem.Name);
                strTool.Append("        <img border=\"0\" title=\"Đang ẩn\" src=\"/Content/Admin/images/gridview/hide.gif\">\r\n");
                strTool.Append("    </a>\r\n");
            }
            strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" class=\"delete\">\r\n", categoryItem.ID, categoryItem.Name);
            strTool.Append("        <img border=\"0\" title=\"Xóa category\" src=\"/Content/Admin/images/gridview/delete.gif\">\r\n");
            strTool.Append("    </a>\r\n");

            strTool.AppendFormat("    <a title=\"Sắp xếp các category con: {1}\" href=\"#{0}\" class=\"sort\">\r\n", categoryItem.ID, categoryItem.Name);
            strTool.Append("        <img border=\"0\" title=\"Xắp xếp category\" src=\"/Content/Admin/images/gridview/sort.gif\">\r\n");
            strTool.Append("    </a>\r\n");

            strTool.Append("</div>\r\n");
            return strTool.ToString();
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductCategoryItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Shop_Category
                        where c.ID >= 1 && c.IsDelete == false
                        orderby c.Order
                        select new ProductCategoryItem
                                   {
                            ID = c.ID,
                            IsPublish = c.IsPublish.Value,
                            IsDelete = c.IsDelete.Value,
                            MetaDescription = c.MetaDescription,
                            MetaKeyword = c.MetaKeyword,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            Description = c.Description,
                            Order = c.Order,
                            IsShowInTab = c.IsShowInTab,
                            IsShowOnBestSeller = c.IsShowOnBestSeller,
                            FrtRoundingNumber = c.FrtRoundingNumber,

                            TotalItems = c.Shop_Product.Count()
                        };
            return query.ToList();
        }

        public List<ProductCategoryItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.Shop_Category
                        where c.ID > 1 && c.ParentID == parentID && c.IsDelete == false
                        orderby c.Order
                        select new ProductCategoryItem
                                   {
                                       ID = c.ID,
                                       IsPublish = c.IsPublish.Value,
                                       //IsDelete = c.IsDelete.Value,
                                       MetaDescription = c.MetaDescription,
                                       MetaKeyword = c.MetaKeyword,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       ParentID = c.ParentID.Value,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShowInTab = c.IsShowInTab,
                                       IsShowOnBestSeller = c.IsShowOnBestSeller,
                                       FrtRoundingNumber = c.FrtRoundingNumber,

                                       //TotalItems = c.Shop_Product.Count()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductCategoryItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Category
                        where c.IsDelete == false
                        orderby c.Name
                        select new ProductCategoryItem
                                   {
                            ID = c.ID,
                            IsPublish = c.IsPublish.Value,
                            IsDelete = c.IsDelete.Value,
                            MetaDescription = c.MetaDescription,
                            MetaKeyword = c.MetaKeyword,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            Description = c.Description,
                            Order = c.Order,
                            IsShowInTab = c.IsShowInTab,
                            IsShowOnBestSeller = c.IsShowOnBestSeller.Value,
                            FrtRoundingNumber = c.FrtRoundingNumber.Value,

                            TotalItems = c.Shop_Product.Count()
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Category
                        where c.ID > 1 && c.IsDelete.Value == false
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new ProductCategoryItem
                                   {
                            ID = c.ID,
                            IsPublish = c.IsPublish.Value,
                            IsDelete = c.IsDelete.Value,
                            MetaDescription = c.MetaDescription,
                            MetaKeyword = c.MetaKeyword,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            Description = c.Description,
                            Order = c.Order,
                            IsShowInTab = c.IsShowInTab,
                            IsShowOnBestSeller = c.IsShowOnBestSeller.Value,
                            FrtRoundingNumber = c.FrtRoundingNumber.Value,

                            TotalItems = c.Shop_Product.Count()
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
        public List<ProductCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Category
                        where c.ID > 1 && c.IsDelete.Value == false && c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductCategoryItem
                                   {
                            ID = c.ID,
                            IsPublish = c.IsPublish.Value,
                            IsDelete = c.IsDelete.Value,
                            MetaDescription = c.MetaDescription,
                            MetaKeyword = c.MetaKeyword,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            Description = c.Description,
                            Order = c.Order,
                            IsShowInTab = c.IsShowInTab,
                            IsShowOnBestSeller = c.IsShowOnBestSeller.Value,
                            FrtRoundingNumber = c.FrtRoundingNumber.Value,

                            TotalItems = c.Shop_Product.Count()
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
        public List<ProductCategoryItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Category
                        where c.IsDelete.Value == false && c.ParentID >= 0
                        select new ProductCategoryItem
                                   {
                            ID = c.ID,
                            IsPublish = c.IsPublish.Value,
                            IsDelete = c.IsDelete.Value,
                            MetaDescription = c.MetaDescription,
                            MetaKeyword = c.MetaKeyword,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            Description = c.Description,
                            Order = c.Order,
                            IsShowInTab = c.IsShowInTab,
                            IsShowOnBestSeller = c.IsShowOnBestSeller.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            FrtRoundingNumber = c.FrtRoundingNumber.Value,

                            TotalItems = c.Shop_Product.Count()
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ProductCategoryItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Category
                        where ltsArrID.Contains(c.ID) && c.IsDelete.Value == false
                        select new ProductCategoryItem
                                   {
                            ID = c.ID,
                            IsPublish = c.IsPublish.Value,
                            IsDelete = c.IsDelete.Value,
                            MetaDescription = c.MetaDescription,
                            MetaKeyword = c.MetaKeyword,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            Description = c.Description,
                            Order = c.Order,
                            IsShowInTab = c.IsShowInTab,
                            IsShowOnBestSeller = c.IsShowOnBestSeller.Value,
                            FrtRoundingNumber = c.FrtRoundingNumber.Value,

                            TotalItems = c.Shop_Product.Count()
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="categoryID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Category GetById(int categoryID)
        {
            var query = from c in FDIDB.Shop_Category where c.ID == categoryID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Category> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopCategory">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Category shopCategory)
        {
            var query = from c in FDIDB.Shop_Category where ((c.Name == shopCategory.Name) && (c.ID != shopCategory.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="categoryName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Category GetByName(string categoryName)
        {
            var query = from c in FDIDB.Shop_Category where ((c.Name == categoryName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="shopCategory">bản ghi cần thêm</param>
        public void Add(Shop_Category shopCategory)
        {
            FDIDB.Shop_Category.Add(shopCategory);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="shopCategory">Xóa bản ghi</param>
        public void Delete(Shop_Category shopCategory)
        {
            FDIDB.Shop_Category.Remove(shopCategory);
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
