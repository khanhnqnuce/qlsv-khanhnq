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
    public partial class Gallery_CategoryDA : BaseDA
    {
        #region Constructer
        public Gallery_CategoryDA()
        {
        }

        public Gallery_CategoryDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Gallery_CategoryDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        #region các function lấy đệ quy

        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="ltsSource">Toàn bộ danh mục</param>
        /// <param name="categoryIDRemove">ID danh mục select</param>
        /// <param name="checkShow"> </param>
        /// <returns></returns>
        public List<GalleryCategoryItem> GetAllSelectList(List<GalleryCategoryItem> ltsSource, int categoryIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsShow).ToList();
            var ltsConvert = new List<GalleryCategoryItem>
                                 {
                                     new GalleryCategoryItem
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(ltsSource, 1, string.Empty, categoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="ltsSource">Toàn bộ danh mục</param>
        /// <param name="categoryIDRemove">ID danh mục select</param>
        /// <returns></returns>
        public List<GalleryCategoryItem> GetAllSelectList(List<GalleryCategoryItem> ltsSource, int categoryIDRemove)
        {
            var ltsConvert = new List<GalleryCategoryItem>
                                 {
                                     new GalleryCategoryItem
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
        /// <param name="rootID"> </param>
        /// <param name="space"></param>
        /// <param name="categoryIDRemove"> </param>
        /// <param name="ltsConvert"></param>
        private void BuildTreeListItem(List<GalleryCategoryItem> ltsItems, int rootID, string space, int categoryIDRemove, ref List<GalleryCategoryItem> ltsConvert)
        {
            space += "---";
            var ltsChils = ltsItems.Where(o => o.ParentID == rootID && o.ID != categoryIDRemove).OrderBy(o => o.DisplayOrder).ToList();
            foreach (var currentItem in ltsChils)
            {
                currentItem.Name = string.Format("|{0} {1}", space, currentItem.Name);
                ltsConvert.Add(currentItem);
                BuildTreeListItem(ltsItems, currentItem.ID, space, categoryIDRemove, ref ltsConvert);
            }
        }

        /// <summary>
        /// Hàm build ra treeview có checkbox chứa danh sách category
        /// </summary>
        public void BuildTreeViewLink(List<GalleryCategoryItem> ltsSource, int categoryID, bool checkShow, ref StringBuilder treeViewHtml)
        {
            var tempCategory = ltsSource.OrderBy(o => o.DisplayOrder).Where(m => m.ParentID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (var category in tempCategory)
            {
                var countQuery = ltsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.IsShow == checkShow);
                var totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\">");
                    if (!category.IsShow)
                        treeViewHtml.Append("<a href=\"" + PathPaging + Request.GetCategoryString() + category.ID + "\"><strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike></a>");
                    else
                        treeViewHtml.Append("<a href=\"" + PathPaging + Request.GetCategoryString() + category.ID + "\">" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</a>");
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewLink(ltsSource, category.ID, checkShow, ref treeViewHtml);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\">");
                    if (!category.IsShow)
                        treeViewHtml.Append("<a href=\"" + PathPaging + Request.GetCategoryString() + category.ID + "\"><strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike></a>");
                    else
                        treeViewHtml.Append("<a href=\"" + PathPaging + Request.GetCategoryString() + category.ID + "\">" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</a>");
                    treeViewHtml.Append("</span></li>\r\n");
                }
            }
        }


        /// <summary>
        /// Hàm build ra treeview có checkbox chứa danh sách category
        /// </summary>
        public void BuildTreeViewCheckBox(List<GalleryCategoryItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempCategory = ltsSource.OrderBy(o => o.DisplayOrder).Where(m => m.ParentID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (GalleryCategoryItem category in tempCategory)
            {
                var countQuery = ltsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
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
        /// Hàm build ra treeview chứa danh sách category
        /// </summary>
        public void BuildTreeView(List<GalleryCategoryItem> ltsSource, int categoryID, bool checkShow, ref StringBuilder treeViewHtml, bool add, bool delete, bool edit, bool view, bool show, bool hide, bool order)
        {
            var tempCategory = ltsSource.OrderBy(o => o.DisplayOrder).Where(m => m.ParentID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (GalleryCategoryItem category in tempCategory)
            {
                var countQuery = ltsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.IsShow == checkShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!category.IsShow)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    treeViewHtml.Append("</a>\r\n");
                    treeViewHtml.AppendFormat(" <i>({0})</i>\r\n", totalChild);
                    treeViewHtml.Append(buildEditToolByID(category, add, delete, edit, show, order) + "\r\n");
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeView(ltsSource, category.ID, checkShow, ref treeViewHtml, add, delete, edit, view, show, hide, order);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!category.IsShow)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    treeViewHtml.Append("</a> <i>(0)</i>" + buildEditToolByID(category, add, delete, edit, show, order) + "</span></li>\r\n");
                }
            }
        }

        /// Replace for upper function
        /// <summary>
        /// Build ra editor cho từng categoryitem
        /// </summary>
        /// <param name="categoryItem" />
        /// <param name="add"> </param>
        /// <param name="delete"> </param>
        /// <param name="edit"> </param>
        /// <param name="show"> </param>
        /// <param name="order"> </param>
        /// <returns></returns>
        private string buildEditToolByID(GalleryCategoryItem categoryItem, bool add, bool delete, bool edit, bool show, bool order)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");
            if (add)
            {
                strTool.AppendFormat("    <a title=\"Thêm mới category: {1}\" data-event=\"add\" href=\"#{0}\">\r\n",
                    categoryItem.ID, categoryItem.Name);
                strTool.Append("       <i class=\"fa fa-plus\"></i>");
                strTool.Append("    </a>");
            }
            if (edit)
            {
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" data-event=\"edit\" href=\"#{0}\">\r\n",
                    categoryItem.ID, categoryItem.Name);
                strTool.Append("       <i class=\"fa fa-pencil-square-o\"></i>");
                strTool.Append("    </a>");
            }
            if (show)
            {
                if (categoryItem.IsShow)
                {
                    strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\" data-event=\"hide\">\r\n", categoryItem.ID,
                        categoryItem.Name);
                    strTool.Append("       <i class=\"fa fa-minus-circle\"></i>");
                    strTool.Append("    </a>\r\n");
                }

                else
                {
                    strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\" data-event=\"show\">\r\n", categoryItem.ID,
                        categoryItem.Name);
                    strTool.Append("       <i class=\"fa fa-eye\"></i>");
                    strTool.Append("    </a>\r\n");
                }
            }
            if (delete)
            {
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" data-event=\"delete\">\r\n", categoryItem.ID,
                    categoryItem.Name);
                strTool.Append("       <i class=\"fa fa-trash-o\"></i>");
                strTool.Append("    </a>\r\n");
            }
            if (order)
            {
                strTool.AppendFormat(
                    "    <a title=\"Sắp xếp các category con: {1}\" href=\"#{0}\" data-event=\"sort\">\r\n",
                    categoryItem.ParentID, categoryItem.Name);
                strTool.Append("       <i class=\"fa fa-sort\"></i>");
                strTool.Append("    </a>\r\n");
            }
            strTool.Append("</div>\r\n");
            return strTool.ToString();
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<GalleryCategoryItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Gallery_Category
                        where c.ID > 1 && !c.IsDeleted.Value
                        orderby c.DisplayOrder
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()
                                   };
            return query.ToList();
        }

        public List<GalleryCategoryItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.Gallery_Category
                        where c.ID > 1 && c.ParentID == parentID && !c.IsDeleted.Value
                        orderby c.DisplayOrder
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<GalleryCategoryItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Gallery_Category
                        where (c.IsShow == isShow && c.ID > 1) && !c.IsDeleted.Value
                        orderby c.Name
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()

                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<GalleryCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Gallery_Category
                        where c.ID > 1 && !c.IsDeleted.Value
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()

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
        public List<GalleryCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Gallery_Category
                        where c.ID > 1
                        orderby c.Name
                        where c.IsShow == isShow && !c.IsDeleted.Value
                              && c.Name.StartsWith(keyword)
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()

                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<GalleryCategoryItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Gallery_Category
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<GalleryCategoryItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Gallery_Category
                        where ltsArrID.Contains(c.ID) && !c.IsDeleted.Value
                        select new GalleryCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.Gallery_Category1.Count(),
                                       TotalItems = c.Gallery_Album.Count()
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
        public Gallery_Category GetById(int categoryID)
        {
            var query = from c in FDIDB.Gallery_Category where c.ID == categoryID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Gallery_Category> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Gallery_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="galleryCategory">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Gallery_Category galleryCategory)
        {
            var query = from c in FDIDB.Gallery_Category where ((c.Name == galleryCategory.Name) && (c.ID != galleryCategory.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Gallery_Category GetByName(string name)
        {
            var query = from c in FDIDB.Gallery_Category where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="galleryCategory">bản ghi cần thêm</param>
        public void Add(Gallery_Category galleryCategory)
        {
            FDIDB.Gallery_Category.Add(galleryCategory);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="galleryCategory">Xóa bản ghi</param>
        public void Delete(Gallery_Category galleryCategory)
        {
            FDIDB.Gallery_Category.Remove(galleryCategory);
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
