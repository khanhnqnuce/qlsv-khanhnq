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
    public partial class Shop_LabelDA : BaseDA
    {
        #region Constructer
        public Shop_LabelDA()
        {
        }

        public Shop_LabelDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_LabelDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region tree
        public List<LabelItem> GetAllSelectList(List<LabelItem> ltsSource, int categoryIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsShow).ToList();
            var ltsConvert = new List<LabelItem>
                                 {
                                     new LabelItem
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(ltsSource, 1, string.Empty, categoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        private void BuildTreeListItem(List<LabelItem> ltsItems, int RootID, string space, int CategoryIDRemove, ref List<LabelItem> ltsConvert)
        {
            space += "---";

            foreach (var t in ltsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                ltsConvert.Add(t);
            }
        }

        public void BuildTreeViewCheckBox(List<LabelItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempCategory = ltsSource.Where(m => m.ID == categoryID && m.ID > 1);
            if (checkShow)
                tempCategory = tempCategory.Where(m => m.IsShow == checkShow);

            foreach (LabelItem category in tempCategory)
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
        #endregion

        public List<LabelItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Label
                        select new LabelItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       IsShow = c.IsShow,
                                       IsShowInSearch = c.IsShowInSearch,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                   };
            return query.ToList();
        }

        public List<LabelItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Label
                        where (c.IsShow == isShow)
                        orderby c.Name
                        select new LabelItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       IsShow = c.IsShow,
                                       IsShowInSearch = c.IsShowInSearch,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                   };
            return query.ToList();
        }

        public List<LabelItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Label
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new LabelItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       IsShow = c.IsShow,
                                       IsShowInSearch = c.IsShowInSearch,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                   };
            return query.Take(showLimit).ToList();
        }

        public List<LabelItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Label
                        where c.IsShow == isShow && c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new LabelItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       IsShow = c.IsShow,
                                       IsShowInSearch = c.IsShowInSearch,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                   };
            return query.Take(showLimit).ToList();
        }

        public List<LabelItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Label
                        select new LabelItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       IsShow = c.IsShow,
                                       IsShowInSearch = c.IsShowInSearch,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        public List<LabelItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Label
                        where ltsArrID.Contains(c.ID)
                        select new LabelItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Description = c.Description,
                                       IsShow = c.IsShow,
                                       IsShowInSearch = c.IsShowInSearch,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        public Shop_Label GetById(int id)
        {
            var query = from c in FDIDB.Shop_Label where c.ID == id select c;
            return query.FirstOrDefault();
        }

        public List<Shop_Label> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Label where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public bool CheckExits(Shop_Label shopLabel)
        {
            var query = from c in FDIDB.Shop_Label where ((c.Name == shopLabel.Name) && (c.ID != shopLabel.ID)) select c;
            return query.Any();
        }

        public Shop_Label GetByName(string brandName)
        {
            var query = from c in FDIDB.Shop_Label where ((c.Name == brandName)) select c;
            return query.FirstOrDefault();
        }

        public void Add(Shop_Label shopLabel)
        {
            FDIDB.Shop_Label.Add(shopLabel);
        }

        public void Delete(Shop_Label shopLabel)
        {
            FDIDB.Shop_Label.Remove(shopLabel);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }

}
