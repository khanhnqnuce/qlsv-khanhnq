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
    public partial class Advertising_ZoneDA : BaseDA
    {
        #region Constructer
        public Advertising_ZoneDA()
        {
        }

        public Advertising_ZoneDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Advertising_ZoneDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region các function lấy đệ quy

        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="ltsSource">Toàn bộ danh mục</param>
        /// <param name="ZoneIDRemove"> </param>
        /// <returns></returns>
        public List<AdvertisingZoneItem> GetAllSelectList(List<AdvertisingZoneItem> ltsSource, int ZoneIDRemove)
        {

            var ltsConvert = new List<AdvertisingZoneItem>
                                 {
                                     new AdvertisingZoneItem
                                         {
                                             ID = 1,
                                             Page = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(ltsSource, 1, string.Empty, ZoneIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="LtsItems"></param>
        /// <param name="RootID"></param>
        /// <param name="space"></param>
        /// <param name="CategoryIDRemove"></param>
        /// <param name="ZoneIDRemove"> </param>
        /// <param name="LtsConvert"></param>
        private void BuildTreeListItem(List<AdvertisingZoneItem> LtsItems, int RootID, string space, int ZoneIDRemove, ref List<AdvertisingZoneItem> LtsConvert)
        {
            space += "---";
            var ltsChils = LtsItems.Where(o => o.ParentID == RootID && o.ID != ZoneIDRemove);
            foreach (var currentItem in ltsChils)
            {
                currentItem.Page = string.Format("|{0} {1}", space, currentItem.Page);
                LtsConvert.Add(currentItem);
                BuildTreeListItem(LtsItems, currentItem.ID, space, ZoneIDRemove, ref LtsConvert);
            }
        }

        /// <summary>
        /// Hàm build ra treeview có checkbox chứa danh sách page
        /// </summary>
        public void BuildTreeViewCheckBox(List<AdvertisingZoneItem> LtsSource, int ZoneID, bool CheckShow, List<int> LtsValues, ref StringBuilder TreeViewHtml)
        {
            var tempCategory = LtsSource.Where(m => m.ParentID == ZoneID && m.ID > 0);
            if (CheckShow)
                tempCategory = tempCategory.Where(m => m.Show == CheckShow);

            foreach (var category in tempCategory)
            {
                var countQuery = LtsSource.Where(m => m.ParentID == category.ID && m.ID > 0);
                if (CheckShow)
                    countQuery = countQuery.Where(m => m.Show == CheckShow);
                var totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    TreeViewHtml.Append("<li class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Zone_" + category.ID + "\" name=\"Zone_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Page + "\" " + (LtsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.Show)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Page) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Page));
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox(LtsSource, category.ID, CheckShow, LtsValues, ref TreeViewHtml);
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Zone_" + category.ID + "\" name=\"Zone_" + category.ID + "\" value=\"" + category.ID + "\" type=\"checkbox\" title=\"" + category.Page + "\" " + (LtsValues.Contains(category.ID) ? " checked" : string.Empty) + "/> ");
                    if (!category.Show)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Page) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Page));
                    TreeViewHtml.Append("</span></li>\r\n");
                }
            }
        }


        /// <summary>
        /// Hàm build ra treeview chứa danh sách page
        /// </summary>
        /// <param name="ltsSource"></param>
        /// <param name="categoryID"></param>
        /// <param name="checkShow"></param>
        /// <param name="TreeViewHtml"></param>
        public void BuildTreeView(List<AdvertisingZoneItem> ltsSource, int categoryID, bool checkShow, ref StringBuilder TreeViewHtml, bool Add, bool Delete, bool Edit, bool view, bool Show, bool Hide, bool Order)
        {
            var tempCategory = ltsSource.Where(m => m.ParentID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.Show == checkShow);

            foreach (AdvertisingZoneItem category in tempCategory)
            {
                var countQuery = ltsSource.Where(m => m.ParentID == category.ID && m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.Show == checkShow);
                var totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    TreeViewHtml.Append("<li title=\"" + category.Page + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"folder\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!category.Show)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Page) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Page));
                    TreeViewHtml.Append("</a>\r\n");
                    TreeViewHtml.AppendFormat(" <i>({0})</i>\r\n", totalChild);
                    TreeViewHtml.Append(buildEditToolByID(category, Add, Delete, Edit, Show) + "\r\n");
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    BuildTreeView(ltsSource, category.ID, checkShow, ref TreeViewHtml, Add, Delete, Edit, view, Show, Hide, Order);
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li title=\"" + category.Page + "\" class=\"unselect\" id=\"" + category.ID.ToString() + "\"><span class=\"file\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!category.Show)
                        TreeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(category.Page) + "</strike>");
                    else
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(category.Page));
                    TreeViewHtml.Append("</a> <i>(0)</i>" + buildEditToolByID(category, Add, Delete, Edit, Show) + "</span></li>\r\n");
                }
            }
        }

        /// Replace for upper function
        /// <summary>
        /// Build ra editor cho từng pageitem
        /// </summary>
        /// <returns></returns>
        private string buildEditToolByID(AdvertisingZoneItem zoneItem, bool add, bool delete, bool edit, bool Show)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");
            if (add)
            {
                strTool.AppendFormat("    <a title=\"Thêm mới zone: {1}\"  data-event=\"add\" href=\"#{0}\">\r\n", zoneItem.ID, zoneItem.Page);
                strTool.Append("       <i class=\"fa fa-plus\"></i>");
                strTool.Append("    </a>");
            }
            if (edit)
            {
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\"  data-event=\"edit\" href=\"#{0}\">\r\n", zoneItem.ID, zoneItem.Page);
                strTool.Append("       <i class=\"fa fa-pencil-square-o\"></i>");
                strTool.Append("    </a>");
            }
            if (Show)
            {
                if (zoneItem.Show)
                {
                    strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\"  data-event=\"hide\">\r\n", zoneItem.ID, zoneItem.Page);
                    strTool.Append("<i class=\"fa fa-minus-circle\"></i>");
                    strTool.Append("    </a>\r\n");
                }
                else
                {
                    strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\"  data-event=\"show\">\r\n", zoneItem.ID, zoneItem.Page);
                    strTool.Append("<i class=\"fa fa-eye\"></i>");
                    strTool.Append("    </a>\r\n");
                }
            }
            if (delete)
            {
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\"  data-event=\"delete\">\r\n", zoneItem.ID, zoneItem.Page);
                strTool.Append("       <i class=\"fa fa-trash-o\"></i>");
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
        public List<AdvertisingZoneItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Advertising_Zone
                        where c.ID > 1 && !c.IsDeleted.Value
                        orderby c.Page
                        select new AdvertisingZoneItem
                                   {
                            ID = c.ID,
                            PageAscii = c.PageAscii,
                            Page = c.Page,
                            ParentID = c.ParentID,
                            Show = c.Show,
                            TotalChilds = c.Advertisings.Count(),
                            TotalItems = c.Advertisings.Count()
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="show"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingZoneItem> GetListSimpleAll(bool show)
        {
            var query = from c in FDIDB.Advertising_Zone

                        select new AdvertisingZoneItem
                                   {
                            ID = c.ID,
                            PageAscii = c.PageAscii,
                            Page = c.Page,
                            ParentID = c.ParentID,
                            Show = c.Show,
                            TotalChilds = c.Advertisings.Count(),
                            TotalItems = c.Advertisings.Count()
                        };
            return query.ToList();
        }

        public List<AdvertisingZoneItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.Advertising_Zone
                        where c.ID > 1 && c.ParentID == parentID

                        select new AdvertisingZoneItem
                                   {
                            ID = c.ID,
                            PageAscii = c.PageAscii,
                            Page = c.Page,
                            ParentID = c.ParentID,
                            Show = c.Show,
                            TotalChilds = c.Advertisings.Count(),
                            TotalItems = c.Advertisings.Count()
                        };
            return query.ToList();
        }
        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<AdvertisingZoneItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Advertising_Zone
                        orderby c.PageAscii
                        where c.PageAscii.StartsWith(keyword)
                        select new AdvertisingZoneItem
                                   {
                            ID = c.ID,
                            PageAscii = c.PageAscii,
                            Page = c.Page
                        };
            return query.Take(showLimit).ToList();
        }


        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="ltsIDNotInclude"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingZoneItem> GetListSimpleByRequest(HttpRequestBase httpRequest, List<int> ltsIDNotInclude)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Advertising_Zone
                        where !ltsIDNotInclude.Contains(o.ID)
                        select new AdvertisingZoneItem
                                   {
                            ID = o.ID,
                            PageAscii = o.PageAscii,
                            Page = o.Page
                        };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.ID == Request.CategoryID);

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();

        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<AdvertisingZoneItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Advertising_Zone

                        select new AdvertisingZoneItem
                                   {
                            ID = o.ID,
                            PageAscii = o.PageAscii,
                            Page = o.Page
                        };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.ID == Request.CategoryID);

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<AdvertisingZoneItem> GetListSimpleByArrZoneID(HttpRequestBase httpRequest, List<int> ltsArrID)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.Advertising_Zone
                        where ltsArrID.Contains(o.ID)
                        select new AdvertisingZoneItem
                                   {
                            ID = o.ID,
                            PageAscii = o.PageAscii,
                            Page = o.Page
                        };
            if (Request.CategoryID > 0)
                query = query.Where(o => o.ID == Request.CategoryID);
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }
        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<AdvertisingZoneItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.Advertising_Zone
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new AdvertisingZoneItem
                                   {
                            ID = o.ID,
                            PageAscii = o.PageAscii,
                            Page = o.Page
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="zoneID"> </param>
        /// <returns>Bản ghi</returns>
        public Advertising_Zone GetById(int zoneID)
        {
            var query = from c in FDIDB.Advertising_Zone where c.ID == zoneID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Advertising_Zone> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Advertising_Zone where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }


        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="advertisingZone"> </param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Advertising_Zone advertisingZone)
        {
            var query = from c in FDIDB.Advertising_Zone where ((c.PageAscii == advertisingZone.PageAscii) && (c.ID != advertisingZone.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="pageAscii">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Advertising_Zone GetByPageAscii(string pageAscii)
        {
            var query = from c in FDIDB.Advertising_Zone where ((c.PageAscii == pageAscii)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="advertisingZone"> </param>
        public void Add(Advertising_Zone advertisingZone)
        {
            FDIDB.Advertising_Zone.Add(advertisingZone);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        public void Delete(Advertising_Zone advertisingZone)
        {
            FDIDB.Advertising_Zone.Remove(advertisingZone);
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
