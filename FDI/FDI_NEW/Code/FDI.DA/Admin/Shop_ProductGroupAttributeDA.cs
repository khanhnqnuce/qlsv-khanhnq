using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.Admin
{
    public partial class Shop_ProductGroupAttributeDA : BaseDA
    {
        Shop_ProductAttributeDA productAttributeDa = new Shop_ProductAttributeDA();

        #region Constructer
        public Shop_ProductGroupAttributeDA()
        {
        }

        public Shop_ProductGroupAttributeDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_ProductGroupAttributeDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region tree
        /// <summary>
        /// Lấy về cây có tổ chức
        /// </summary>
        /// <param name="LtsSource">Toàn bộ danh mục</param>
        /// <param name="CategoryIDRemove">ID danh mục select</param>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> getAllSelectList(List<ProductGroupAttributeItem> LtsSource, int CategoryIDRemove, bool checkShow)
        {
            if (checkShow)
                LtsSource = LtsSource.Where(o => o.IsDeleted).ToList();
            var ltsConvert = new List<ProductGroupAttributeItem>
                                 {
                                     new ProductGroupAttributeItem()
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(LtsSource, 1, string.Empty, CategoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="LtsItems"></param>
        /// <param name="RootCategoryID"></param>
        /// <param name="space"></param>
        /// <param name="LtsConvert"></param>
        private void BuildTreeListItem(List<ProductGroupAttributeItem> LtsItems, int RootID, string space, int CategoryIDRemove, ref List<ProductGroupAttributeItem> LtsConvert)
        {
            space += "---";

            foreach (var t in LtsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                LtsConvert.Add(t);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LtsSource"></param>
        /// <param name="CategoryID">ProductTypeID</param>
        /// <param name="CheckShow"></param>
        /// <param name="LtsValues"></param>
        /// <param name="TreeViewHtml"></param>
        public void BuildTreeViewCheckBox(List<Shop_Product_Group_Attribute> LtsSource, int CategoryID, bool CheckShow, List<int> LtsValues, ref StringBuilder TreeViewHtml, int productID)
        {
            // Create new stopwatch
            var stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            var lst = (from e in LtsSource
                       where e.ProductTypeID == CategoryID
                             && e.IsDeleted == false
                       select new ProductGroupAttributeItem
                                  {
                           ID = e.ID,
                           Description = e.Description,
                           Name = e.Name,
                           LstProAttrItem = e.Shop_Product_Attribute.Where(c => c.GroupAttributeID == e.ID && c.IsDeleted == false).Select(p => new ProductAttributeItem()
                           {
                               ID = p.ID,
                               Name = p.Name,
                               LstProductAttrSpecItem = p.Shop_Product_Attribute_Specification.Where(m => m.AttributeID == p.ID && m.IsDeleted == false).Select(m => new ProductAttributeSpecificationItem()
                               {
                                   ID = m.ID,
                                   Name = m.Name,
                                   //                                                                                                                              sttCheck =  DB.Shop_Product.Where(n => n.ID == productID && 
                                   //n.Shop_Product_Attribute_Specification.Where(k => k.ID == m.ID).Count() > 0                                                                                                                                ).Select(n => n.ID).FirstOrDefault() > 0 ? " checked" : "",
                               }).ToList()
                           }).ToList(),
                       }).AsParallel().ToList();

            lst = lst.Where(c => c.LstProAttrItem.Count > 0 && c.LstProAttrItem.Any(p => p.LstProductAttrSpecItem.Count > 0)).ToList();

            var queryProduct = (from e in FDIDB.Shop_Product_Attribute_Specification
                                where e.Shop_Product.Any(c => c.ID == productID)
                                select new
                                {
                                    Id = e.ID,
                                }).ToList();

            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    var lst1 = item.LstProAttrItem.Where(c => c.LstProductAttrSpecItem.Count > 0).ToList();
                    if (lst1.Count > 0)
                    {

                        TreeViewHtml.Append("<li title=\"" + item.Description + "\" class=\"unselect\" id=\"" +
                                            item.ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" +
                                            item.ID + "\" name=\"Category_" + item.ID + "\" value=\"" +
                                            item.ID.ToString() + "\" type=\"checkbox\" title=\"" + item.Name + "\" " +
                                            (string.Empty) + "/> ");
                        TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(item.Name));
                        TreeViewHtml.Append("</span>\r\n");
                        TreeViewHtml.Append("<ul>\r\n");

                        TreeViewHtml.Append("<ul id='SelectTreeProductAttribute' class='filetree' style='border:0px;'>");

                        #region attr item

                        foreach (var productAttributeItem in lst1)
                        {
                            TreeViewHtml.Append("<li title=\"" + productAttributeItem.Name +
                                                "\" class=\"unselect\" id=\"" +
                                                productAttributeItem.ID.ToString() +
                                                "\"><span class=\"folder\"> <input id=\"Category_" +
                                                productAttributeItem.ID +
                                                "\" name=\"Category_" + productAttributeItem.ID + "\" value=\"" +
                                                productAttributeItem.ID.ToString() + "\" type=\"checkbox\" title=\"" +
                                                productAttributeItem.Name + "\" " + (string.Empty) + "/> ");
                            TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(productAttributeItem.Name));
                            TreeViewHtml.Append("</span>\r\n");
                            TreeViewHtml.Append("<ul>\r\n");


                            TreeViewHtml.Append(
                                "<ul id='SelectTreeProductAttributeSpec' class='filetree' style='border:0px;'>");

                            #region spec

                            foreach (var productAttributeSpecItem in productAttributeItem.LstProductAttrSpecItem)
                            {
                                //  string sttCheck = productAttributeSpecItem.sttCheck;
                                string sttCheck = string.Empty;
                                int i = productAttributeSpecItem.ID;
                                if (queryProduct.Any(c => c.Id == i))
                                {
                                    sttCheck = " checked";
                                }

                                TreeViewHtml.Append("<li title=\"" + productAttributeSpecItem.Name +
                                                    "\" class=\"unselect\" id=\"" +
                                                    productAttributeSpecItem.ID.ToString() +
                                                    "\"><span class=\"file\"> <input id=\"Category_" +
                                                    productAttributeSpecItem.ID + "\" name=\"Category_" +
                                                    productAttributeSpecItem.ID + "\" value=\"" +
                                                    productAttributeSpecItem.ID.ToString() +
                                                    "\" type=\"checkbox\" title=\"" + productAttributeSpecItem.Name +
                                                    "\" " + (sttCheck) + "/> ");
                                TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(productAttributeSpecItem.Name));
                                TreeViewHtml.Append("</span>\r\n");
                                TreeViewHtml.Append("<ul>\r\n");
                                TreeViewHtml.Append("</ul>\r\n");
                                TreeViewHtml.Append("</li>\r\n");
                            }

                            #endregion

                            TreeViewHtml.Append("</ul>");

                            TreeViewHtml.Append("</ul>\r\n");
                            TreeViewHtml.Append("</li>\r\n");
                        }

                        #endregion

                        TreeViewHtml.Append("</ul>");

                        TreeViewHtml.Append("</ul>\r\n");
                        TreeViewHtml.Append("</li>\r\n");
                    }
                }
            }

            // Stop timing
            stopwatch.Stop();

            // Write result
            string s = stopwatch.Elapsed.ToString();
        }
        //ViệtDD
        public void BuildTreeViewCheckBox2(List<ProductGroupAttributeItem> LtsSource, int CategoryID, bool CheckShow, List<int> LtsValues, ref StringBuilder TreeViewHtml, int productID)
        {
            LtsSource = LtsSource.Where((c => c.ProductTypeID == CategoryID && c.IsDeleted == CheckShow)).OrderBy(o => o.DisplayOrder).ToList();
            int totalChild = LtsSource.Count();

            for (int i = 0; i < totalChild; i++)
            {

                List<ProductAttributeItem> lstSourceProductAtt = productAttributeDa.GetListSimpleByArrID(LtsSource[i].ID);
                if (lstSourceProductAtt != null && lstSourceProductAtt.Count > 0)
                {
                    TreeViewHtml.Append("<li title=\"" + LtsSource[i].Description + "\" class=\"unselect\" id=\"" + LtsSource[i].ID.ToString() + "\"><span class=\"folder\"> <input id=\"Category_" + LtsSource[i].ID + "\" name=\"Category_" + LtsSource[i].ID + "\" value=\"" + LtsSource[i].ID.ToString() + "\" type=\"checkbox\" title=\"" + LtsSource[i].Name + "\" " + (string.Empty) + "/> ");
                    TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(LtsSource[i].Name));
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");

                    var stbProductAttHtml = new StringBuilder();
                    var lstValueProductAtt = MyBase.ConvertStringToListInt(LtsSource[i].ID.ToString());

                    TreeViewHtml.Append("<ul id='SelectTreeProductAttribute' class='filetree' style='border:0px;'>");
                    productAttributeDa.BuildTreeViewCheckBox2(lstSourceProductAtt, LtsSource[i].ID, lstValueProductAtt, ref stbProductAttHtml);
                    TreeViewHtml.Append(stbProductAttHtml);
                    TreeViewHtml.Append("</ul>");

                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
                else
                {
                    TreeViewHtml.Append("<li title=\"" + LtsSource[i].Description + "\" class=\"unselect\" id=\"" + LtsSource[i].ID.ToString() + "\"><span class=\"file\"> <input id=\"Category_" + LtsSource[i].ID + "\" name=\"Category_" + LtsSource[i].ID + "\" value=\"" + LtsSource[i].ID.ToString() + "\" type=\"checkbox\" title=\"" + LtsSource[i].Name + "\" " + (string.Empty) + "/> ");
                    TreeViewHtml.Append(HttpContext.Current.Server.HtmlEncode(LtsSource[i].Name));
                    TreeViewHtml.Append("</span>\r\n");
                    TreeViewHtml.Append("<ul>\r\n");
                    TreeViewHtml.Append("</ul>\r\n");
                    TreeViewHtml.Append("</li>\r\n");
                }
            }
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductGroupAttributeItem> getListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where c.IsDeleted.Value == false
                        orderby c.Name
                        select new ProductGroupAttributeItem()
                        {
                            ID = c.ID,
                            ProductTypeID = c.ProductTypeID.Value,
                            Name = c.Name,
                            Description = c.Description,
                            DisplayOrder = c.DisplayOrder.Value,
                            CreatedBy = c.CreatedBy.Value,
                            UpdatedBy = c.UpdatedBy.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productTypeName = c.ProductTypeID.HasValue ? (from e in FDIDB.Shop_Product_Type where e.ID == c.ProductTypeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.ToList();
        }

        public List<Shop_Product_Group_Attribute> getListAll()
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where c.IsDeleted.Value == false
                        select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="IsShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductGroupAttributeItem> getListSimpleAll(bool IsShow)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        orderby c.Name
                        select new ProductGroupAttributeItem()
                        {
                            ID = c.ID,
                            ProductTypeID = c.ProductTypeID.Value,
                            Name = c.Name,
                            Description = c.Description,
                            DisplayOrder = c.DisplayOrder.Value,
                            CreatedBy = c.CreatedBy.Value,
                            UpdatedBy = c.UpdatedBy.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productTypeName = c.ProductTypeID.HasValue ? (from e in FDIDB.Shop_Product_Type where e.ID == c.ProductTypeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductGroupAttributeItem()
                        {
                            ID = c.ID,
                            ProductTypeID = c.ProductTypeID.Value,
                            Name = c.Name,
                            Description = c.Description,
                            DisplayOrder = c.DisplayOrder.Value,
                            CreatedBy = c.CreatedBy.Value,
                            UpdatedBy = c.UpdatedBy.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productTypeName = c.ProductTypeID.HasValue ? (from e in FDIDB.Shop_Product_Type where e.ID == c.ProductTypeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool IsShow)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where c.Name.StartsWith(keyword)
                        orderby c.Name
                        select new ProductGroupAttributeItem()
                        {
                            ID = c.ID,
                            ProductTypeID = c.ProductTypeID.Value,
                            Name = c.Name,
                            Description = c.Description,
                            DisplayOrder = c.DisplayOrder.Value,
                            CreatedBy = c.CreatedBy.Value,
                            UpdatedBy = c.UpdatedBy.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productTypeName = c.ProductTypeID.HasValue ? (from e in FDIDB.Shop_Product_Type where e.ID == c.ProductTypeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="Page">Trang hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductGroupAttributeItem> getListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where c.IsDeleted.Value == false
                        select new ProductGroupAttributeItem()
                        {
                            ID = c.ID,
                            ProductTypeID = c.ProductTypeID.Value,
                            Name = c.Name,
                            Description = c.Description,
                            DisplayOrder = c.DisplayOrder.Value,
                            CreatedBy = c.CreatedBy.Value,
                            UpdatedBy = c.UpdatedBy.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productTypeName = c.ProductTypeID.HasValue ? (from e in FDIDB.Shop_Product_Type where e.ID == c.ProductTypeID.Value select e.Name).FirstOrDefault() : string.Empty,
                            IsAllowFiltering = c.IsAllowFiltering.HasValue && c.IsAllowFiltering.Value,
                            IsShowOnProductPage = c.IsShowOnProductPage.HasValue && c.IsShowOnProductPage.Value,
                        };

            #region search by ProductTypeID
            int productTypeId = Convert.ToInt32(httpRequest["ProductTypeID"]);
            if (productTypeId != 0)
            {
                query = query.Where(c => c.ProductTypeID == productTypeId);
            }
            #endregion

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="LtsArrID"></param>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> getListSimpleByArrID(List<int> LtsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute
                        where LtsArrID.Contains(c.ID)
                        orderby c.Name
                        select new ProductGroupAttributeItem()
                        {
                            ID = c.ID,
                            ProductTypeID = c.ProductTypeID.Value,
                            Name = c.Name,
                            Description = c.Description,
                            DisplayOrder = c.DisplayOrder.Value,
                            CreatedBy = c.CreatedBy.Value,
                            UpdatedBy = c.UpdatedBy.Value,
                            IsDeleted = c.IsDeleted.Value,
                            productTypeName = c.ProductTypeID.HasValue ? (from e in FDIDB.Shop_Product_Type where e.ID == c.ProductTypeID.Value select e.Name).FirstOrDefault() : string.Empty,
                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Group_Attribute getById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="LtsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product_Group_Attribute> getListByArrID(List<int> LtsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute where LtsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="Shop_Product_Group_Attribute">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool checkExits(Shop_Product_Group_Attribute Shop_Product_Group_Attribute)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute where ((c.Name == Shop_Product_Group_Attribute.Name) && (c.ID != Shop_Product_Group_Attribute.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Group_Attribute getByName(string name)
        {
            var query = from c in FDIDB.Shop_Product_Group_Attribute where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="Shop_Product_Group_Attribute">bản ghi cần thêm</param>
        public void Add(Shop_Product_Group_Attribute Shop_Product_Group_Attribute)
        {
            FDIDB.Shop_Product_Group_Attribute.Add(Shop_Product_Group_Attribute);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="Shop_Product_Group_Attribute">Xóa bản ghi</param>
        public void Delete(Shop_Product_Group_Attribute Shop_Product_Group_Attribute)
        {
            FDIDB.Shop_Product_Group_Attribute.Remove(Shop_Product_Group_Attribute);
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
