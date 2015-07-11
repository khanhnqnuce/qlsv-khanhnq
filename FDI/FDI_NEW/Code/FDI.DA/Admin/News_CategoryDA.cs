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
    public class News_CategoryDA : BaseDA
    {
        #region Constructer
        public News_CategoryDA()
        {
        }

        public News_CategoryDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public News_CategoryDA(string pathPaging, string pathPagingExt)
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
        /// <returns></returns>
        public List<NewsCategoryItem> GetAllSelectList(List<NewsCategoryItem> ltsSource, int categoryIDRemove)
        {

            var ltsConvert = new List<NewsCategoryItem>
                                 {
                                     new NewsCategoryItem
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
        private void BuildTreeListItem(List<NewsCategoryItem> ltsItems, int rootID, string space, int categoryIDRemove, ref List<NewsCategoryItem> ltsConvert)
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
        public void BuildTreeViewCheckBox(List<NewsCategoryItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempCategory = ltsSource.OrderBy(o => o.DisplayOrder).Where(m => m.ParentID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (var category in tempCategory)
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
        /// <param name="ltsSource">List category</param>
        /// <param name="categoryID">ID parent</param>
        /// <param name="checkShow">Check hiển thị</param>
        /// <param name="treeViewHtml">String output</param>
        /// <param name="add"> </param>
        /// <param name="delete"> </param>
        /// <param name="edit"> </param>
        /// <param name="view"> </param>
        /// <param name="show"> </param>
        /// <param name="hide"> </param>
        /// <param name="order"> </param>
        public void BuildTreeView(List<NewsCategoryItem> ltsSource, int categoryID, bool checkShow, ref StringBuilder treeViewHtml, bool add, bool delete, bool edit, bool view, bool show, bool hide, bool order)
        {
            var tempCategory = ltsSource.OrderBy(o => o.DisplayOrder).Where(m => m.ParentID == categoryID && m.ID >= 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (var category in tempCategory)
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
        /// Build ra editor cho từng NewsCategoryItem
        /// </summary>
        /// <param name="newsCategoryItem"> </param>
        /// <param name="add"> </param>
        /// <param name="delete"> </param>
        /// <param name="edit"> </param>
        /// <param name="show"> </param>
        /// <param name="order"> </param>
        /// <returns></returns>
        private string buildEditToolByID(NewsCategoryItem newsCategoryItem, bool add, bool delete, bool edit, bool show, bool order)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");
            if (add)
            {
                strTool.AppendFormat("    <a title=\"Thêm mới category: {1}\" data-event=\"add\" href=\"#{0}\">\r\n", newsCategoryItem.ID, newsCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-plus\"></i>");
                strTool.Append("    </a>");
            }
            if (edit)
            {
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" data-event=\"edit\" href=\"#{0}\">\r\n", newsCategoryItem.ID, newsCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-pencil-square-o\"></i>");
                strTool.Append("    </a>");
            }
            if (show)
            {
                if (newsCategoryItem.IsShow)
                {
                    strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\" data-event=\"hide\">\r\n", newsCategoryItem.ID, newsCategoryItem.Name);
                    strTool.Append("       <i class=\"fa fa-minus-circle\"></i>");
                    strTool.Append("    </a>\r\n");
                }
                else
                {
                    strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\" data-event=\"show\">\r\n", newsCategoryItem.ID, newsCategoryItem.Name);
                    strTool.Append("       <i class=\"fa fa-eye\"></i>");
                    strTool.Append("    </a>\r\n");
                }
            }
            if (delete)
            {
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" data-event=\"delete\">\r\n", newsCategoryItem.ID, newsCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-trash-o\"></i>");
                strTool.Append("    </a>\r\n");
            }

            if (order)
            {
                strTool.AppendFormat("    <a title=\"Sắp xếp các category con: {1}\" href=\"#{0}\" data-event=\"sort\">\r\n", newsCategoryItem.ParentID, newsCategoryItem.Name);
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
        public List<NewsCategoryItem> GetAllListSimple()
        {
            var query = from c in FDIDB.News_Category
                        where c.ID > 1 && !c.IsDeleted.Value
                        orderby c.DisplayOrder
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       //Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       IsShowInTab = c.IsShowInTab,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()
                                   };
            return query.ToList();
        }

        public List<NewsCategoryItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.News_Category
                        where c.ID > 1 && c.ParentID == parentID && !c.IsDeleted.Value
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsCategoryItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.News_Category
                        where (c.IsShow == isShow && c.ID > 1)
                        orderby c.Name
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()

                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<NewsCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.News_Category
                        where c.ID > 1
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()
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
        public List<NewsCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.News_Category
                        where c.ID > 1
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword)
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<NewsCategoryItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.News_Category
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<NewsCategoryItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.News_Category
                        where ltsArrID.Contains(c.ID)
                        select new NewsCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.News_Category1.Count(),
                                       TotalItems = c.News_News.Count()
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra link ascii đã tồn tại hay chưa
        /// </summary>
        /// <param name="NameAscii">Tên bản ghi hiện tại</param>
        /// <param name="id">id của bạn ghi hiện tại</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckTitleAsciiExits(string NameAscii, int id)
        {
            var query = (from c in FDIDB.News_Category
                         where c.NameAscii == NameAscii && c.ID != id && c.IsDeleted == false
                         select c).Count();
            return query > 0;
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="categoryID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public News_Category GetById(int categoryID)
        {
            var query = from c in FDIDB.News_Category where c.ID == categoryID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<News_Category> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.News_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="newsCategory">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(News_Category newsCategory)
        {
            var query = from c in FDIDB.News_Category where ((c.Name == newsCategory.Name) && (c.ID != newsCategory.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="categoryName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public News_Category GetByName(string categoryName)
        {
            var query = from c in FDIDB.News_Category where ((c.Name == categoryName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="newsCategory">bản ghi cần thêm</param>
        public void Add(News_Category newsCategory)
        {
            FDIDB.News_Category.Add(newsCategory);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="newsCategory">Xóa bản ghi</param>
        public void Delete(News_Category newsCategory)
        {
            FDIDB.News_Category.Remove(newsCategory);
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
