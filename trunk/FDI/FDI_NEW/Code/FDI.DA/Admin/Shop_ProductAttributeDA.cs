using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.Admin
{
    public partial class Shop_ProductAttributeDA : BaseDA
    {
        private readonly Shop_Product_Attribute_SpecificationDA _productAttributeSpecificationDa =
            new Shop_Product_Attribute_SpecificationDA();

        #region Constructer
        public Shop_ProductAttributeDA()
        {
        }

        public Shop_ProductAttributeDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_ProductAttributeDA(string pathPaging, string pathPagingExt)
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
        public List<ProductAttributeItem> GetAllSelectList(List<ProductAttributeItem> ltsSource, int categoryIDRemove, bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsDeleted).ToList();
            var ltsConvert = new List<ProductAttributeItem>
                                                        {
                                                            new ProductAttributeItem
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
        /// <param name="space"></param>
        /// <param name="LtsConvert"></param>
        private void BuildTreeListItem(List<ProductAttributeItem> ltsItems, int RootID, string space, int CategoryIDRemove, ref List<ProductAttributeItem> LtsConvert)
        {
            space += "---";

            foreach (var t in ltsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                LtsConvert.Add(t);
            }
        }

        public void BuildTreeViewCheckBox(List<ProductAttributeItem> ltsSource, int categoryID, bool checkShow, List<int> ltsValues, ref StringBuilder treeViewHtml, int productID)
        {
            ltsSource = ltsSource.Where((c => c.GroupAttributeID == categoryID && c.IsDeleted == checkShow)).OrderBy(o => o.DisplayOrder).ToList();
            int totalChild = ltsSource.Count();

            for (int i = 0; i < totalChild; i++)
            {
              

                var lstSourceProductAttSpec = _productAttributeSpecificationDa.getListSimpleByAttributeID(ltsSource[i].ID);
                if (lstSourceProductAttSpec != null && lstSourceProductAttSpec.Count > 0)
                {
                    treeViewHtml.Append("<li title=\"" + ltsSource[i].Name + "\" class=\"unselect\" id=\"" + ltsSource[i].ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + ltsSource[i].ID + "\" name=\"Category_" + ltsSource[i].ID + "\" value=\"" + ltsSource[i].ID.ToString() + "\" type=\"checkbox\" title=\"" + ltsSource[i].Name + "\" " + (string.Empty) + "/> ");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(ltsSource[i].Name));
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");

                    var stbProductAttSpecHtml = new StringBuilder();
                    var lstValueProductAttSpec = MyBase.ConvertStringToListInt(ltsSource[i].ID.ToString());

                    treeViewHtml.Append("<ul id='SelectTreeProductAttributeSpec' class='filetree' style='border:0px;'>");
                    _productAttributeSpecificationDa.BuildTreeViewCheckBox(lstSourceProductAttSpec, ltsSource[i].ID, false, lstValueProductAttSpec, ref stbProductAttSpecHtml, productID);
                    treeViewHtml.Append(stbProductAttSpecHtml);
                    treeViewHtml.Append("</ul>");

                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + ltsSource[i].Name + "\" class=\"unselect\" id=\"" + ltsSource[i].ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + ltsSource[i].ID + "\" name=\"Category_" + ltsSource[i].ID + "\" value=\"" + ltsSource[i].ID.ToString() + "\" type=\"checkbox\" title=\"" + ltsSource[i].Name + "\" " + (string.Empty) + "/> ");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(ltsSource[i].Name));
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");

                    var stbProductAttSpecHtml = new StringBuilder();
                    var lstValueProductAttSpec = MyBase.ConvertStringToListInt(ltsSource[i].ID.ToString());

                    treeViewHtml.Append("<ul id='SelectTreeProductAttributeSpec' class='filetree' style='border:0px;'>");
                    _productAttributeSpecificationDa.BuildTreeViewCheckBox(lstSourceProductAttSpec, ltsSource[i].ID, false, lstValueProductAttSpec, ref stbProductAttSpecHtml, productID);
                    treeViewHtml.Append(stbProductAttSpecHtml);
                    treeViewHtml.Append("</ul>");

                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
            }
        }

        /// <summary>
        /// vietdd
        /// </summary>
        /// <param name="ltsSource"></param>
        /// <param name="attributeID"></param>
        /// <param name="ltsValues"></param>
        /// <param name="treeViewHtml"></param>
        public void BuildTreeViewCheckBox2(List<ProductAttributeItem> ltsSource, int attributeID, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempAttribute = ltsSource.OrderBy(o => o.ID).Where(m => m.GroupAttributeID == attributeID && m.ID > 1 && m.IsDeleted == false);


            foreach (var attribute in tempAttribute)
            {
                var countQuery = ltsSource.Where(m => m.GroupAttributeID == attribute.ID && m.ID > 1);

                var totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + attribute.Name + "\" class=\"unselect\" id=\"" + attribute.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + attribute.ID + "\" name=\"Category_" + attribute.ID + "\" value=\"" + attribute.ID + "\" type=\"checkbox\" title=\"" + attribute.Name + "\" " + (ltsValues.Contains(attribute.ID) ? " checked" : string.Empty) + "/> ");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(attribute.Name));
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox2(ltsSource, attribute.ID, ltsValues, ref treeViewHtml);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + attribute.Name + "\" class=\"unselect\" id=\"" + attribute.ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + attribute.ID + "\" name=\"Category_" + attribute.ID + "\" value=\"" + attribute.ID + "\" type=\"checkbox\" title=\"" + attribute.Name + "\" " + (ltsValues.Contains(attribute.ID) ? " checked" : string.Empty) + "/> ");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(attribute.Name));
                    treeViewHtml.Append("</span></li>\r\n");
                }
            }
        }

        public void BuildTreeViewCheckBoxNoRoot(List<ProductAttributeItem> ltsSource, int attributeID, List<int> ltsValues, ref StringBuilder treeViewHtml)
        {
            var tempAttribute = ltsSource.OrderBy(o => o.ID).Where(m => m.GroupAttributeID == attributeID && m.IsDeleted == false);

            foreach (var attribute in tempAttribute)
            {
                var countQuery = ltsSource.Where(m => m.GroupAttributeID == attribute.ID && m.ID > 1);
                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + attribute.Name + "\" class=\"unselect\" id=\"" + attribute.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Attribute_" + attribute.ID + "\" name=\"Attribute_" + attribute.ID + "\" value=\"" + attribute.ID + "\" type=\"checkbox\" title=\"" + attribute.Name + "\" " + (ltsValues.Contains(attribute.ID) ? " checked" : string.Empty) + "/> ");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(attribute.Name));
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeViewCheckBox2(ltsSource, attribute.ID, ltsValues, ref treeViewHtml);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + attribute.Name + "\" class=\"unselect\" id=\"" + attribute.ID.ToString() + "\"><span class=\"file\"> <input id=\"Attribute_" + attribute.ID + "\" name=\"Attribute_" + attribute.ID + "\" value=\"" + attribute.ID + "\" type=\"checkbox\" title=\"" + attribute.Name + "\" " + (ltsValues.Contains(attribute.ID) ? " checked" : string.Empty) + "/> ");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(attribute.Name));
                    treeViewHtml.Append("</span></li>\r\n");
                }
            }
        }

        public void BuildTreeView(List<ProductAttributeItem> ltsSource, int attributeID, ref StringBuilder treeViewHtml)
        {
            var tempAttribute = ltsSource.OrderBy(o => o.ID).Where(m => m.GroupAttributeID == attributeID && m.ID > 1 && m.IsDeleted == false);

            foreach (var attribute in tempAttribute)
            {
                var countQuery = ltsSource.Where(m => m.GroupAttributeID == attribute.ID && m.ID > 1);

                int totalChild = countQuery.Count();
                if (totalChild > 0)
                {
                    treeViewHtml.Append("<li title=\"" + attribute.Name + "\" class=\"unselect\" id=\"" + attribute.ID.ToString() + "\"><span class=\"folder\"><a class=\"tool\" href=\"javascript:;\">");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(attribute.Name));
                    treeViewHtml.Append("</a>\r\n");
                    treeViewHtml.AppendFormat(" <i>({0})</i>\r\n", totalChild);
                    treeViewHtml.Append(buildEditToolByID(attribute) + "\r\n");
                    treeViewHtml.Append("</span>\r\n");
                    treeViewHtml.Append("<ul>\r\n");
                    BuildTreeView(ltsSource, attribute.ID, ref treeViewHtml);
                    treeViewHtml.Append("</ul>\r\n");
                    treeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    treeViewHtml.Append("<li title=\"" + attribute.Name + "\" class=\"unselect\" id=\"" + attribute.ID.ToString() + "\"><span class=\"file\"><a class=\"tool\" href=\"javascript:;\">");
                    treeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(attribute.Name));
                    treeViewHtml.Append("</a> <i>(0)</i>" + buildEditToolByID(attribute) + "</span></li>\r\n");
                }
            }
        }

        private string buildEditToolByID(ProductAttributeItem attributeItem)
        {
            if (attributeItem != null)
            {
                var strTool = new StringBuilder();
                strTool.Append("<div class=\"quickTool\">\r\n");
                strTool.AppendFormat("    <a title=\"Thêm mới attribute: {1}\" class=\"add\" href=\"#{0}\">\r\n",
                                     attributeItem.ID, attributeItem.Name);
                strTool.Append(
                    "        <img border=\"0\" title=\"Thêm mới attribute\" src=\"/Content/Admin/images/gridview/add.gif\">\r\n");
                strTool.Append("    </a>");
                strTool.AppendFormat("    <a title=\"Chỉnh sửa: {1}\" class=\"edit\" href=\"#{0}\">\r\n",
                                     attributeItem.ID, attributeItem.Name);
                strTool.Append(
                    "        <img border=\"0\" title=\"Sửa attribute\" src=\"/Content/Admin/images/gridview/edit.gif\">\r\n");
                strTool.Append("    </a>");
                strTool.AppendFormat("    <a title=\"Xóa: {1}\" href=\"#{0}\" class=\"delete\">\r\n", attributeItem.ID,
                                     attributeItem.Name);
                strTool.Append(
                    "        <img border=\"0\" title=\"Xóa attribute\" src=\"/Content/Admin/images/gridview/delete.gif\">\r\n");
                strTool.Append("    </a>\r\n");

                strTool.AppendFormat(
                    "    <a title=\"Sắp xếp các attribute con: {1}\" href=\"#{0}\" class=\"sort\">\r\n",
                    attributeItem.ID, attributeItem.Name);
                strTool.Append(
                    "        <img border=\"0\" title=\"Xắp xếp attribute\" src=\"/Content/Admin/images/gridview/sort.gif\">\r\n");
                strTool.Append("    </a>\r\n");

                strTool.Append("</div>\r\n");
                return strTool.ToString();
            }
            return string.Empty;
        }

        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        where c.IsDeleted.Value == false
                        orderby c.Name
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        orderby c.Name
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductAttributeItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
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
        public List<ProductAttributeItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// add by BienLV 01-03-2014
        /// </summary>
        /// <param name="typeProduct"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> GetListAttributeByType(int typeProduct, bool isDelete = false)
        {
            var query = (from c in FDIDB.Shop_Product_Attribute
                         where c.Shop_Product_Group_Attribute.ProductTypeID == typeProduct && c.Shop_Product_Group_Attribute.IsDeleted == isDelete && c.IsDeleted == isDelete
                         orderby c.Shop_Product_Group_Attribute.DisplayOrder, c.DisplayOrder
                         select c).ToList()
                        .GroupBy(c => new { c.Shop_Product_Group_Attribute.ID, c.Shop_Product_Group_Attribute.Name })
                        .Select(g => new ProductGroupAttributeItem
                                         {
                            ID = g.Key.ID,
                            Name = g.Key.Name,
                            LstProAttrItem = g.Select(a => new ProductAttributeItem
                                                               {
                                ID = a.ID,
                                Name = a.Name
                            }).ToList()
                        });
            return query.ToList();
        }

        /// <summary>
        /// add by BienLV 01-03-2014
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        public List<ProductAttributeSpecificationItem> GetAttributeProduct(int productId, bool delete = false)
        {
            var query = (from c in FDIDB.Shop_Product_Attribute_Specification
                         where c.Shop_Product.Any(p => p.ID == productId)
                         orderby c.Shop_Product_Attribute.Shop_Product_Group_Attribute.DisplayOrder,
                             c.Shop_Product_Attribute.DisplayOrder ascending
                         select new ProductAttributeSpecificationItem
                                    {
                             ID = c.ID,
                             Name = c.Name,
                             AttributeID = c.Shop_Product_Attribute.ID,
                             productAttributeName = c.Shop_Product_Attribute.Name
                         });
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductAttributeItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_Attribute
                        where c.IsDeleted.Value == false
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };

            #region search by GroupAttributeID
            int groupAttributeID = Convert.ToInt32(httpRequest["ProductGroupAttributeID"]);
            if (groupAttributeID != 0)
            {
                query = query.Where(c => c.GroupAttributeID == groupAttributeID);
            }
            #endregion

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ProductAttributeItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        where ltsArrID.Contains(c.ID)
                        orderby c.Name
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        public List<ProductAttributeItem> GetListSimpleByArrID(int groupAttributeID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        where c.GroupAttributeID == groupAttributeID
                        orderby c.Name
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            GroupAttributeID = c.GroupAttributeID.Value,
                            Name = c.Name,
                            DisplayOrder = c.DisplayOrder.Value,
                            IsAllowFilter = c.IsAllowFilter.Value,
                            IsAllowCompare = c.IsAllowCompare.Value,
                            IsDeleted = c.IsDeleted.Value,
                            GroupAttributeName = c.GroupAttributeID.HasValue ? (from e in FDIDB.Shop_Product_Group_Attribute where e.ID == c.GroupAttributeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        public List<Shop_Product_Group_Attribute> GetListProductGroupAtt()
        {
            return FDIDB.Shop_Product_Group_Attribute.Where(c => c.IsDeleted.Value == false).ToList();
        }

        public List<ProductGroupAttributeItem> GetListProductGroupAttribute()
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where c.IsDeleted.Value == false
                        select new ProductGroupAttributeItem
                                   {
                            ID = c.ID,
                            Name = c.Shop_Product_Type.Name + " - " + c.Name,
                        };

            return query.ToList();
        }

        public List<Shop_Product_Attribute> GetListProductAttribute()
        {
            return FDIDB.Shop_Product_Attribute.Where(c => c.IsDeleted.Value == false).ToList();
        }

        public List<ProductAttributeItem> GetListProductAttributeItem()
        {
            var query = from c in FDIDB.Shop_Product_Attribute
                        where c.IsDeleted.Value == false
                        select new ProductAttributeItem
                                   {
                            ID = c.ID,
                            Name = c.Shop_Product_Group_Attribute.Shop_Product_Type.Name + " - " + c.Shop_Product_Group_Attribute.Name + " - " + c.Name,
                        };

            return query.OrderByDescending(c => c.Name).ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Attribute GetById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Attribute where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product_Attribute> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopProductAttribute">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Product_Attribute shopProductAttribute)
        {
            var query = from c in FDIDB.Shop_Product_Attribute where ((c.Name == shopProductAttribute.Name) && (c.ID != shopProductAttribute.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Attribute GetByName(string name)
        {
            var query = from c in FDIDB.Shop_Product_Attribute where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="shopProductAttribute">bản ghi cần thêm</param>
        public void Add(Shop_Product_Attribute shopProductAttribute)
        {
            FDIDB.Shop_Product_Attribute.Add(shopProductAttribute);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="shopProductAttribute">Xóa bản ghi</param>
        public void Delete(Shop_Product_Attribute shopProductAttribute)
        {
            FDIDB.Shop_Product_Attribute.Remove(shopProductAttribute);
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
