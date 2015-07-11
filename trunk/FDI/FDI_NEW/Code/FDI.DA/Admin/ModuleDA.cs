using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class ModuleDA:BaseDA
    {
        #region Constructer
        
        public ModuleDA(string pathPaging)
        {
            PathPaging = pathPaging;
          
        }

        public ModuleDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
            
        }
        #endregion

        public Module GetById(int id)
        {
            var query = from c in FDIDB.Modules
                        where c.ID == id
                        select c;
            return query.FirstOrDefault();
        }


         #region các function lấy đệ quy

        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="ltsSource">Toàn bộ danh mục</param>
        /// <param name="moduleIDRemove"> </param>
        /// <param name="checkShow"> </param>
        /// <returns></returns>
        public List<ModuleItem> GetAllSelectList(List<ModuleItem> ltsSource, int moduleIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsShow == true).ToList();
            var ltsConvert = new List<ModuleItem>
                                 {
                                     new ModuleItem
                                         {
                                             ID = 1,
                                             NameModule = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(ltsSource, 1, string.Empty, moduleIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="ltsItems"></param>
        /// <param name="rootID"> </param>
        /// <param name="space"></param>
        /// <param name="moduleIDRemove"> </param>
        /// <param name="ltsConvert"></param>
        private void BuildTreeListItem(IEnumerable<ModuleItem> ltsItems, int rootID, string space, int moduleIDRemove, ref List<ModuleItem> ltsConvert)
        {
            space += "---";
            var ltsChils = ltsItems.Where(o => o.PrarentID == rootID && o.ID != moduleIDRemove).OrderBy(o => o.Ord).ToList();
            foreach (var currentItem in ltsChils)
            {
                currentItem.NameModule = string.Format("|{0} {1}", space, currentItem.NameModule);
                ltsConvert.Add(currentItem);
                BuildTreeListItem(ltsItems, currentItem.ID, space, moduleIDRemove, ref ltsConvert);
            }
        }

        /// <summary>
        /// Hàm build ra treeview có checkbox chứa danh sách category
        /// </summary>
        public void BuildTreeViewCheckBox(List<ModuleItem> ltsSource, int moduleID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempModule = ltsSource.OrderBy(o => o.Ord).Where(m => m.PrarentID == moduleID && m.ID > 1);
            if (checkShow)
                tempModule = tempModule.Where(m => m.IsShow == true);

            foreach (var module in tempModule)
            {
                var countQuery = ltsSource.Where(m => m.PrarentID == module.ID && m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.IsShow == true);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + module.Content + "\" class=\"unselect\" id=\"" + module.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + module.ID + "\" name=\"Category_" + module.ID + "\" value=\"" + module.ID + "\" type=\"checkbox\" title=\"" + module.NameModule + "\" " + (ltsValues.Contains(module.ID) ? " checked" : string.Empty) + "/> ");
                    if (!module.IsShow == true)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(module.NameModule) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(module.NameModule));
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox(ltsSource, module.ID, checkShow, ltsValues, ref treeViewHtml);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + module.Content + "\" class=\"unselect\" id=\"" + module.ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + module.ID + "\" name=\"Category_" + module.ID + "\" value=\"" + module.ID + "\" type=\"checkbox\" title=\"" + module.NameModule + "\" " + (ltsValues.Contains(module.ID) ? " checked" : string.Empty) + "/> ");
                    if (!module.IsShow == true)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(module.NameModule) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(module.NameModule));
                    treeViewHtml.Append("</span></li>\r\n");
                }
            }
        }


        /// <summary>
        /// Hàm build ra treeview chứa danh sách category
        /// </summary>
        public void BuildTreeView(List<ModuleItem> ltsSource, int moduleID, bool checkShow, ref StringBuilder treeViewHtml, bool add, bool delete, bool edit, bool show, bool order)
        {
            var tempModule = ltsSource.OrderBy(o => o.Ord).Where(m => m.PrarentID == moduleID && m.ID > 1);
            if (checkShow)
                tempModule = tempModule.Where(m => m.IsShow == true);

            foreach (var module in tempModule)
            {
                var countQuery = ltsSource.Where(m => m.PrarentID == module.ID && m.ID > 1);
                if (checkShow)
                    countQuery = countQuery.Where(m => m.IsShow == true);
                var totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + module.Content + "\" class=\"unselect\" id=\"" + module.ID.ToString() + "\"><span class=\"folder\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!module.IsShow == true)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(module.NameModule) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(module.NameModule));
                    treeViewHtml.Append("</a>\r\n");
                    treeViewHtml.AppendFormat(" <i>({0})</i>\r\n", totalChild);
                    treeViewHtml.Append(buildEditToolByID(module, add, delete, edit, show, order) + "\r\n");
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeView(ltsSource, module.ID, checkShow, ref treeViewHtml, add, delete, edit, show, order);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + module.Content + "\" class=\"unselect\" id=\"" + module.ID.ToString() + "\"><span class=\"file\"><a class=\"tool\" href=\"javascript:;\">");
                    if (!module.IsShow == true)
                        treeViewHtml.Append("<strike>" + HttpContext.Current.Server.HtmlEncode(module.NameModule) + "</strike>");
                    else
                        treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(module.NameModule));
                    treeViewHtml.Append("</a> <i>(0)</i>" + buildEditToolByID(module, add, delete, edit, show, order) + "</span></li>\r\n");
                }
            }
        }

        /// Replace for upper function
        /// <summary>
        /// Build ra editor cho từng FAQCategoryItem
        /// </summary>
        /// <returns></returns>
        private string buildEditToolByID(ModuleItem moduleItem, bool add, bool delete, bool edit, bool show, bool order)
        {
            var strTool = new StringBuilder();
            strTool.Append("<div class=\"quickTool\">\r\n");
            //strTool.AppendFormat("    <a title=\"Xem tính năng: {1}\" class=\"showModule\" href=\"#{0}\">\r\n", moduleItem.Id, moduleItem.NameModule);
            //strTool.Append("        <img border=\"0\" title=\"Xem tính năng\" src=\"/Content/Admin/images/gridview/show.gif\">\r\n");
            //strTool.Append("    </a>");

            strTool.AppendFormat("    <a title=\"Gán Role: {1}\" data-event=\"roleModule\" href=\"#{0}\">\r\n", moduleItem.ID, moduleItem.NameModule);
            strTool.Append("       <i class=\"fa fa-users icon\"></i>");
            strTool.Append("    </a>");

            strTool.AppendFormat("    <a title=\"Gán User: {1}\" data-event=\"userModule\" href=\"#{0}\">\r\n", moduleItem.ID, moduleItem.NameModule);
            strTool.Append("       <i class=\"fa fa-user\"></i>");
            strTool.Append("    </a>");
            
            if (add)
            {
                strTool.AppendFormat("<a title=\"Thêm mới Module: {1}\" data-event=\"add\" href=\"#{0}\">\r\n",
                                     moduleItem.ID, moduleItem.NameModule);
                strTool.Append("       <i class=\"fa fa-plus\"></i>");
                strTool.Append("    </a>");
            }
            if (edit)
            {
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" data-event=\"edit\" href=\"#{0}\">\r\n", moduleItem.ID, moduleItem.NameModule);
                strTool.Append("       <i class=\"fa fa-pencil-square-o\"></i>");
                strTool.Append("    </a>");
            }
            if (show)
            {
                if (moduleItem.IsShow != null && moduleItem.IsShow.Value)
                {
                    strTool.AppendFormat("    <a title=\"Ẩn: {1}\" href=\"#{0}\" data-event=\"hide\">\r\n", moduleItem.ID, moduleItem.NameModule);
                    strTool.Append("<i class=\"fa fa-minus-circle\"></i>");
                    strTool.Append("    </a>\r\n");
                    
                }
                else
                {
                    strTool.AppendFormat("    <a title=\"Hiển thị: {1}\" href=\"#{0}\" data-event=\"show\">\r\n",
                                         moduleItem.ID, moduleItem.NameModule);
                    strTool.Append("<i class=\"fa fa-eye\"></i>");
                    strTool.Append("    </a>\r\n");
                  
                }
            }
            if (delete)
            {
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" data-event=\"delete\">\r\n", moduleItem.ID,
                                     moduleItem.NameModule);
                strTool.Append("       <i class=\"fa fa-trash-o\"></i>");
                strTool.Append("    </a>\r\n");
            }
            if (order)
            {
                strTool.AppendFormat("    <a title=\"Sắp xếp các Module con: {1}\" href=\"#{0}\" data-event=\"sort\">\r\n",
                                     moduleItem.PrarentID, moduleItem.NameModule);
                strTool.Append("       <i class=\"fa fa-sort\"></i>");
                strTool.Append("    </a>\r\n");
            }
            strTool.Append("</div>\r\n");
            return strTool.ToString();
        }
        #endregion

        public List<ModuleItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Modules
                        where c.ID > 1
                        orderby c.Ord
                        select new ModuleItem
                                   {
                                        ID = c.ID,
                                        NameModule = c.NameModule,
                                        Tag = c.Tag.ToLower(),
                                        Ord = c.Ord,
                                        PrarentID = c.PrarentID.Value,
                                        Content = c.Content,
                                        IsShow = c.IsShow != null && c.IsShow.Value
                                    };
            return query.ToList();
        }

        public List<ModuleItem> GetAllListSimpleByParentID(int parentID)
        {
            var query = from c in FDIDB.Modules
                        where c.ID > 1 && c.PrarentID == parentID
                        orderby c.Ord
                        select new ModuleItem
                                   {
                                        ID = c.ID,
                                        NameModule = c.NameModule,
                                        Tag = c.Tag,
                                        Ord = c.Ord,
                                        PrarentID = c.PrarentID.Value,
                                        Content = c.Content,
                                        IsShow = c.IsShow != null && c.IsShow.Value
                                    };
            return query.ToList();
        }


        public List<ModuleItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Modules
                        select new ModuleItem
                                   {
                                        ID = c.ID,
                                        NameModule = c.NameModule,
                                        Tag = c.Tag,
                                        Ord = c.Ord,
                                        PrarentID = c.PrarentID.Value,
                                        Content = c.Content,
                                        IsShow = c.IsShow != null && c.IsShow.Value

                                    };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ModuleItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Modules
                        where c.ID > 1
                        orderby c.NameModule
                        where c.NameModule.StartsWith(keyword)
                        select new ModuleItem
                                   {
                                       ID = c.ID,
                                       NameModule = c.NameModule,
                                       Tag = c.Tag,
                                       Ord = c.Ord,
                                       PrarentID = c.PrarentID.Value,
                                       Content = c.Content,
                                       IsShow = c.IsShow != null && c.IsShow.Value

                                   };
            return query.Take(showLimit).ToList();
        }

        public aspnet_Roles GetByRoleId(Guid roleId)
        {
            var query = from c in FDIDB.aspnet_Roles
                        where c.RoleId == roleId
                        select c;
            return query.FirstOrDefault();
        }

        public aspnet_Users GetByUserId(Guid id)
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.UserId == id
                        select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <param name="isShow"> </param>
        /// <returns></returns>
        public List<ModuleItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Modules
                        where c.ID > 1
                        orderby c.NameModule
                        where c.IsShow == isShow
                        && c.NameModule.StartsWith(keyword)
                        select new ModuleItem
                                   {
                                        ID = c.ID,
                                        NameModule = c.NameModule,
                                        Tag = c.Tag,
                                        Ord = c.Ord,
                                        PrarentID = c.PrarentID.Value,
                                        Content = c.Content,
                                        IsShow = c.IsShow != null && c.IsShow.Value
                                    };
            return query.Take(showLimit).ToList();
        }
        public List<Module> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Modules where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<ModuleadminItem> GetListModuleadminSimple()
        {
            var query = from c in FDIDB.Modules
                        where c.ID > 1 && c.IsShow == true
                        orderby c.Ord
                        select new ModuleadminItem
                                   {
                                       ID = c.ID,
                                       NameModule = c.NameModule,
                                       Url = c.Tag,
                                       ClassCss = c.ClassCss,
                                       Ord = c.Ord,
                                       PrarentID = c.PrarentID.Value,
                                       Content = c.Content,
                                       IsShow = c.IsShow != null && c.IsShow.Value,
                                       Active = 0
                                   };
            return query.ToList();
        }

        public aspnet_Roles RoleModuleActive2(Guid roleId)
        {
            var listRoleModuleItem = from a in FDIDB.aspnet_Roles
                                      where a.RoleId == roleId
                                      select a;
                                      //{
                                      //    RoleId = a.RoleId,
                                      //    RoleName = a.RoleName,
                                      //    Modules = a.Modules.Select(m=>new ModuleItem
                                      //                                      {
                                      //                                          ID = m.ID,
                                      //                                          PrarentID = m.PrarentID,
                                      //                                          NameModule = m.NameModule
                                      //                                      }),
                                      //  ActiveRoles = a.ActiveRoles.Select(n=>new ActiveRoleItem
                                      //                                            {
                                      //                                                ID = n.Id,
                                      //                                                NameActive = n.NameActive
                                      //                                            }),
                                      //  RoleModuleActive = a.Role_ModuleActive.Select(r=>new Role_ModuleActiveItem
                                      //                                                       {
                                      //                                                           ID = r.ID,
                                      //                                                           Active = r.Active,
                                      //                                                           Check = r.Check
                                      //                                                       })
                                      //}).FirstOrDefault();


           
            return listRoleModuleItem.FirstOrDefault();
        }

        public Module GetlistByTag(string tag)
        {
            var query = (from c in FDIDB.Modules
                         where c.Tag.ToLower() == tag
                         select c).FirstOrDefault();
            return query;
            //if (query != null)
            //    if(query.User_ModuleActive.Any())
            //    {
            //        return query;
            //    }
            //    else
            //    {
            //        query = from c in FDIDB.Modules
            //                where c.
            //    }

            //else
            //{
            //    return new Module();

            //}
        }

        public void Add(Module module)
        {
            FDIDB.Modules.Add(module);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="module"> </param>
       
        public void Delete(Module module)
        {
            FDIDB.Modules.Remove(module);
        }
        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }

    }
}
