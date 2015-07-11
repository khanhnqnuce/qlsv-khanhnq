using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class System_MenuDA : BaseDA
    {
        #region Constructer
        public System_MenuDA()
        {
        }

        public System_MenuDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_MenuDA(string pathPaging, string pathPagingExt)
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
        public List<MenuItem> GetAllSelectList(List<MenuItem> ltsSource, int categoryIDRemove)
        {

            var ltsConvert = new List<MenuItem>
                                 {
                                     new MenuItem
                                         {
                                             MenuID = 1,
                                             MenuTitle = "Thư mục gốc"
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
        /// <param name="menuIDRemove"> </param>
        /// <param name="ltsConvert"></param>
        private void BuildTreeListItem(List<MenuItem> ltsItems, int rootID, string space, int menuIDRemove, ref List<MenuItem> ltsConvert)
        {
            space += "---";
            var LtsChils = ltsItems.Where(o => o.MenuParentID == rootID && o.MenuID != menuIDRemove).ToList();
            foreach (var currentItem in LtsChils)
            {
                currentItem.MenuTitle = string.Format("|{0} {1}", space, currentItem.MenuTitle);
                ltsConvert.Add(currentItem);
                BuildTreeListItem(ltsItems, currentItem.MenuID, space, menuIDRemove, ref ltsConvert);
            }
        }


        /// <summary>
        /// Hàm build ra treeview chứa danh sách menu
        /// </summary>
        /// <param name="ltsSource"></param>
        /// <param name="menuID"></param>
        /// <param name="checkShow"></param>
        /// <param name="treeViewHtml"></param>
        /// <param name="add"></param>
        /// <param name="delete"></param>
        /// <param name="edit"></param>
        /// <param name="show"></param>
        /// <param name="order"></param>

        public void BuildTreeView(List<MenuItem> ltsSource, int menuID, bool checkShow, ref StringBuilder treeViewHtml, bool add, bool delete, bool edit, bool show, bool order)
        {
            var tempMenu = ltsSource.Where(m => m.MenuParentID == menuID && m.MenuID > 1);
            if (checkShow)
                tempMenu = tempMenu.Where(m => m.MenuShow == checkShow);

            foreach (var menu in tempMenu)
            {
                var countQuery = ltsSource.Where(m => m.MenuParentID == menu.MenuID && m.MenuID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.MenuShow == checkShow);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + menu.MenuDescription + "\" class=\"unselect\" id=\"" + menu.MenuID.ToString() + "\"><span class=\"folder\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!menu.MenuShow)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(menu.MenuTitle) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(menu.MenuTitle));
                    treeViewHtml.Append("</a>\r\n");
                    treeViewHtml.AppendFormat(" <i>({0})</i>\r\n", totalChild);
                    treeViewHtml.Append(buildEditToolByID(menu, add, delete, edit, show, order) + "\r\n");
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeView(ltsSource, menu.MenuID, checkShow, ref treeViewHtml, add, delete, edit, show, order);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + menu.MenuDescription + "\" class=\"unselect\" id=\"" + menu.MenuID.ToString() + "\"><span class=\"file\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!menu.MenuShow)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(menu.MenuTitle) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(menu.MenuTitle));
                    treeViewHtml.Append("</a> <i>(0)</i>" + buildEditToolByID(menu, add, delete, edit, show, order) + "</span></li>\r\n");
                }
            }
        }

        /// Replace for upper function
        /// <summary>
        /// Build ra editor cho từng menuitem
        /// </summary>
        /// <param name="menuItem"> </param>
        /// <param name="add"> </param>
        /// <param name="delete"> </param>
        /// <param name="edit"> </param>
        /// <param name="show"> </param>
        /// <param name="order"> </param>
        /// <returns></returns>
        private string buildEditToolByID(MenuItem menuItem, bool add, bool delete, bool edit, bool show, bool order)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");

            if (add)
            {
                strTool.AppendFormat("    <a title=\"Thêm mới menu: {1}\" data-event=\"add\" href=\"#{0}\">\r\n", menuItem.MenuID, menuItem.MenuTitle);
                strTool.Append("       <i class=\"fa fa-plus\"></i>");
                strTool.Append("    </a>");
            }
            if (edit)
            {
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" data-event=\"edit\" href=\"#{0}\">\r\n", menuItem.MenuID, menuItem.MenuTitle);
                strTool.Append("       <i class=\"fa fa-pencil-square-o\"></i>");
                strTool.Append("    </a>");
            }

            if (show)
            {
                if (menuItem.MenuShow)
                {
                    strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\" data-event=\"hide\">\r\n", menuItem.MenuID, menuItem.MenuTitle);
                    strTool.Append("       <i class=\"fa fa-minus-circle\"></i>");
                    strTool.Append("    </a>\r\n");
                }
                else
                {
                    strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\" data-event=\"show\">\r\n", menuItem.MenuID, menuItem.MenuTitle);
                    strTool.Append("       <i class=\"fa fa-eye\"></i>");
                    strTool.Append("    </a>\r\n");
                }
            }
            if (delete)
            {
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" data-event=\"delete\">\r\n", menuItem.MenuID, menuItem.MenuTitle);
                strTool.Append("       <i class=\"fa fa-trash-o\"></i>");
                strTool.Append("    </a>\r\n");
            }

            if (order)
            {
                strTool.AppendFormat("    <a title=\"Sắp xếp các menu con: {1}\" href=\"#{0}\" data-event=\"sort\">\r\n", menuItem.MenuParentID, menuItem.MenuTitle);
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
        public List<MenuItem> GetAllListSimple()
        {
            var query = from c in FDIDB.System_Menu
                        where c.MenuID > 1
                        orderby c.MenuOrder
                        select new MenuItem
                                   {
                                       MenuID = c.MenuID,
                                       MenuTitle = c.MenuTitle,
                                       //MenuDescription = c.MenuDescription,
                                       MenuLink = c.MenuLink,
                                       MenuOrder = c.MenuOrder,
                                       MenuParentID = c.MenuParentID,
                                       MenuShow = c.MenuShow,
                                       MenuTaget = c.MenuTaget,
                                       //rel = c.Rel

                                   };
            return query.ToList();
        }

        public List<MenuItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.System_Menu
                        where c.MenuID > 1 && c.MenuParentID == parentID
                        orderby c.MenuOrder
                        select new MenuItem
                                   {
                                       MenuID = c.MenuID,
                                       MenuTitle = c.MenuTitle,
                                       MenuDescription = c.MenuDescription,
                                       MenuLink = c.MenuLink,
                                       MenuOrder = c.MenuOrder,
                                       MenuParentID = c.MenuParentID,
                                       MenuShow = c.MenuShow,
                                       MenuTaget = c.MenuTaget
                            ,
                                       //rel = c.Rel
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<MenuItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.System_Menu
                        where (c.MenuShow == isShow && c.MenuID > 1)
                        orderby c.MenuTitle
                        select new MenuItem
                                   {
                                       MenuID = c.MenuID,
                                       MenuTitle = c.MenuTitle,
                                       MenuDescription = c.MenuDescription,
                                       MenuLink = c.MenuLink,
                                       MenuOrder = c.MenuOrder,
                                       MenuParentID = c.MenuParentID,
                                       MenuShow = c.MenuShow,
                                       MenuTaget = c.MenuTaget
                            ,
                                       //rel = c.Rel
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<MenuItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_Menu
                        where c.MenuID > 1
                        orderby c.MenuTitle
                        where c.MenuTitle.StartsWith(keyword)
                        select new MenuItem
                                   {
                                       MenuID = c.MenuID,
                                       MenuTitle = c.MenuTitle,
                                       MenuLink = c.MenuLink,
                                       MenuOrder = c.MenuOrder,
                                       MenuParentID = c.MenuParentID,
                                       MenuShow = c.MenuShow,
                                       MenuTaget = c.MenuTaget
                            ,
                                       //rel = c.Rel
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
        public List<MenuItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.System_Menu
                        where c.MenuID > 1
                        orderby c.MenuTitle
                        where c.MenuShow == isShow
                        && c.MenuTitle.StartsWith(keyword)
                        select new MenuItem
                                   {
                                       MenuID = c.MenuID,
                                       MenuTitle = c.MenuTitle,
                                       MenuLink = c.MenuLink,
                                       MenuOrder = c.MenuOrder,
                                       MenuParentID = c.MenuParentID,
                                       MenuShow = c.MenuShow,
                                       MenuTaget = c.MenuTaget
                            ,
                                       //rel = c.Rel
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<MenuItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_Menu
                        select new MenuItem
                                   {
                                       MenuID = o.MenuID,
                                       MenuTitle = o.MenuTitle,
                                       MenuShow = o.MenuShow,
                                       MenuDescription = o.MenuDescription,
                                       MenuLink = o.MenuLink,
                                       MenuOrder = o.MenuOrder,
                                       MenuParentID = o.MenuParentID,
                                       MenuTaget = o.MenuTaget
                            ,
                                       //rel = o.Rel
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<MenuItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_Menu
                        where ltsArrID.Contains(o.MenuID)
                        select new MenuItem
                                   {
                                       MenuID = o.MenuID,
                                       MenuTitle = o.MenuTitle,
                                       MenuShow = o.MenuShow,
                                       MenuDescription = o.MenuDescription,
                                       MenuLink = o.MenuLink,
                                       MenuOrder = o.MenuOrder,
                                       MenuParentID = o.MenuParentID,
                                       MenuTaget = o.MenuTaget
                            ,
                                       //rel = o.Rel
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="menuID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Menu GetById(int menuID)
        {
            var query = from c in FDIDB.System_Menu where c.MenuID == menuID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Menu> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Menu where ltsArrID.Contains(c.MenuID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemMenu">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_Menu systemMenu)
        {
            var query = from c in FDIDB.System_Menu where ((c.MenuTitle == systemMenu.MenuTitle) && (c.MenuID != systemMenu.MenuID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="menuTitle">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Menu GetByName(string menuTitle)
        {
            var query = from c in FDIDB.System_Menu where ((c.MenuTitle == menuTitle)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemMenu">bản ghi cần thêm</param>
        public void Add(System_Menu systemMenu)
        {
            FDIDB.System_Menu.Add(systemMenu);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemMenu">Xóa bản ghi</param>
        public void Delete(System_Menu systemMenu)
        {
            FDIDB.System_Menu.Remove(systemMenu);
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
