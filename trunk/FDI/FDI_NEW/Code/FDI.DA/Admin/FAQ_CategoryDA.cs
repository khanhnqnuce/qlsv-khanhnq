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
    public partial class FAQ_CategoryDA : BaseDA
    {
        #region Constructer
        public FAQ_CategoryDA()
        {
        }

        public FAQ_CategoryDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public FAQ_CategoryDA(string pathPaging, string pathPagingExt)
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
        public List<FAQCategoryItem> GetAllSelectList(List<FAQCategoryItem> ltsSource, int categoryIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsShow).ToList();
            var ltsConvert = new List<FAQCategoryItem>
                                 {
                                     new FAQCategoryItem
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
        private void BuildTreeListItem(List<FAQCategoryItem> ltsItems, int rootID, string space, int categoryIDRemove, ref List<FAQCategoryItem> ltsConvert)
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
        /// <param name="ltsSource"> </param>
        /// <param name="categoryID"> </param>
        /// <param name="checkShow"> </param>
        /// <param name="ltsValues"> </param>
        /// <param name="TreeViewHtml"> </param>
        public void BuildTreeViewCheckBox(List<FAQCategoryItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder TreeViewHtml)
        {
            if (TreeViewHtml == null) throw new ArgumentNullException("TreeViewHtml");
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
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (ltsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsShow)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Name) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Name));
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox(ltsSource, category.ID, checkShow, ltsValues, ref TreeViewHtml);
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li title=\"" + category.Description + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + category.ID + "\" name=\"Category_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Name + "\" " + (ltsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.IsShow)
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
        /// <param name="ltsSource"> </param>
        /// <param name="categoryID"> </param>
        /// <param name="checkShow"> </param>
        /// <param name="treeViewHtml"> </param>
        ///  <param name="add"> </param>
        /// <param name="delete"> </param>
        /// <param name="edit"> </param>
        /// <param name="show"> </param>
        /// <param name="order"> </param>
        public void BuildTreeView(List<FAQCategoryItem> ltsSource, int categoryID, bool checkShow, ref StringBuilder treeViewHtml, bool add, bool delete, bool edit, bool show, bool order)
        {
            var tempCategory = ltsSource.OrderBy(o => o.DisplayOrder).Where(m => m.ParentID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (FAQCategoryItem category in tempCategory)
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
                    BuildTreeView(ltsSource, category.ID, checkShow, ref treeViewHtml, add, delete, edit, show,
                                  order);
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
        /// Build ra editor cho từng FAQCategoryItem
        /// </summary>
        /// <param name="faqCategoryItem"> </param>
        /// <param name="add"> </param>
        /// <param name="delete"> </param>
        /// <param name="edit"> </param>
        /// <param name="show"> </param>
        /// <param name="order"> </param>
        /// <returns></returns>
        private string buildEditToolByID(FAQCategoryItem faqCategoryItem, bool add, bool delete, bool edit, bool show, bool order)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");
            if (add)
            {
                strTool.AppendFormat("    <a title=\"Thêm mới category: {1}\" data-event=\"add\" href=\"#{0}\">\r\n",
                                     faqCategoryItem.ID, faqCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-plus\"></i>");
                strTool.Append("    </a>");
            }
            if (edit)
            {
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" data-event=\"edit\" href=\"#{0}\">\r\n",
                                     faqCategoryItem.ID, faqCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-pencil-square-o\"></i>");
                strTool.Append("    </a>");
            }

            if (show)
            {
                if (faqCategoryItem.IsShow)
                {
                    strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\" data-event=\"hide\">\r\n",
                                         faqCategoryItem.ID, faqCategoryItem.Name);
                    strTool.Append("       <i class=\"fa fa-minus-circle\"></i>");
                    strTool.Append("    </a>\r\n");
                }
                else
                {
                    strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\" data-event=\"show\">\r\n",
                                         faqCategoryItem.ID, faqCategoryItem.Name);
                    strTool.Append("       <i class=\"fa fa-eye\"></i>");
                    strTool.Append("    </a>\r\n");
                }
            }

            if (delete)
            {
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" data-event=\"delete\">\r\n", faqCategoryItem.ID,
                                     faqCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-trash-o\"></i>");
                strTool.Append("    </a>\r\n");
            }
            if (order)
            {
                strTool.AppendFormat(
                    "    <a title=\"Sắp xếp các category con: {1}\" href=\"#{0}\" data-event=\"sort\">\r\n",
                    faqCategoryItem.ParentID, faqCategoryItem.Name);
                strTool.Append("       <i class=\"fa fa-sort\"></i>");
                strTool.Append("    </a>\r\n");

                strTool.Append("</div>\r\n");
            }
            return strTool.ToString();
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQCategoryItem> GetAllListSimple()
        {
            var query = from c in FDIDB.FAQ_Category
                        where c.ID > 1
                        orderby c.DisplayOrder
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()
                                   };
            return query.ToList();
        }

        public List<FAQCategoryItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.FAQ_Category
                        where c.ID > 1 && c.ParentID == parentID
                        orderby c.DisplayOrder
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQCategoryItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.FAQ_Category
                        where (c.IsShow == isShow && c.ID > 1)
                        orderby c.Name
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()

                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<FAQCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.FAQ_Category
                        where c.ID > 1
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()

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
        public List<FAQCategoryItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.FAQ_Category
                        where c.ID > 1
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword)
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()

                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQCategoryItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.FAQ_Category
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<FAQCategoryItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.FAQ_Category
                        where ltsArrID.Contains(c.ID)
                        select new FAQCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       DisplayOrder = c.DisplayOrder,
                                       ParentID = c.ParentID,
                                       IsShow = c.IsShow,
                                       NameAscii = c.NameAscii,
                                       TotalChilds = c.FAQ_Category1.Count(),
                                       TotalItems = c.FAQ_Question.Count()
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
        public FAQ_Category GetById(int categoryID)
        {
            var query = from c in FDIDB.FAQ_Category where c.ID == categoryID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<FAQ_Category> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.FAQ_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="faqCategory">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(FAQ_Category faqCategory)
        {
            var query = from c in FDIDB.FAQ_Category where ((c.Name == faqCategory.Name) && (c.ID != faqCategory.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="categoryName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public FAQ_Category GetByName(string categoryName)
        {
            var query = from c in FDIDB.FAQ_Category where ((c.Name == categoryName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="faqCategory">bản ghi cần thêm</param>
        public void Add(FAQ_Category faqCategory)
        {
            FDIDB.FAQ_Category.Add(faqCategory);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="faqCategory">Xóa bản ghi</param>
        public void Delete(FAQ_Category faqCategory)
        {
            FDIDB.FAQ_Category.Remove(faqCategory);
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
