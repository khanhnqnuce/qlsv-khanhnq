using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.EndUser.Implementation
{
    public class ShopProductImplement : InitDB, IReposityShopProduct
    {

        public List<ProductItem> GetPageBySPQuery(IQueryable<ProductItem> query, int currentPage, int rowPerPage)
        {
            #region lấy về bảng tạm & danh sách ID

            var ltsProductItem = query.ToList(); //Lấy về tất cả
            var ltsIdSelect = new List<int>();
            if (ltsProductItem.Any())
            {
                TongSoBanGhiSauKhiQuery = ltsProductItem.Count(); // Tổng số lấy về        
                int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
                int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
                intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

                for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
                {
                    ltsIdSelect.Add(Convert.ToInt32(ltsProductItem[rowIndex].ID));
                }

            }
            else
                TongSoBanGhiSauKhiQuery = 0;
            #endregion
            //Query listItem theo listID
            var iquery = from c in ltsProductItem
                         where ltsIdSelect.Contains(c.ID)
                         select c;

            return iquery.ToList();
        }
        public List<ProductItem> GetPageBySPQuery2(List<ProductItem> query, int currentPage, int rowPerPage)
        {
            #region lấy về bảng tạm & danh sách ID

            var ltsProductItem = query; //Lấy về tất cả
            var ltsIdSelect = new List<int>();
            if (ltsProductItem.Any())
            {
                TongSoBanGhiSauKhiQuery = ltsProductItem.Count(); // Tổng số lấy về        
                int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
                int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
                intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

                for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
                {
                    ltsIdSelect.Add(Convert.ToInt32(ltsProductItem[rowIndex].ID));
                }

            }
            else
                TongSoBanGhiSauKhiQuery = 0;
            #endregion
            //Query listItem theo listID
            var iquery = from c in ltsProductItem
                         where ltsIdSelect.Contains(c.ID)

                         select c;

            return iquery.ToList();
        }


        public List<ProductItem> ProductsPage(int currentPage, int rowPerPage, string type, List<ProductItem> listProductItem)
        {
            listProductItem = GetPageBySPQuery2(listProductItem, currentPage, rowPerPage);
            return listProductItem;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductItem GetProductItemByID(int id)
        {
            var query = from c in Instance.Shop_Product
                        where c.ID == id
                        select new ProductItem
                        {
                            ID = c.ID
                        };
            return query.FirstOrDefault();
        }

        public ProductItem GetProductItemByNameAscii(string nameAscii, string lang)
        {
            var query = from c in Instance.Shop_Product
                        where c.NameAscii.ToLower().Equals(nameAscii.ToLower()) && c.IsShow == true && c.IsDelete == false
                        select new ProductItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            Details = c.Details,
                            SEOTitle = c.SEOTitle,
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            HightlightsDes = c.HightlightsDes,
                            UrlPicture = c.Gallery_Picture.Folder+c.Gallery_Picture.Url,
                            Code = c.Code,
                            PriceOld = c.PriceOld,
                            PriceNew = c.PriceNew,
                            CreatedOnUtc = c.CreatedOnUtc.Value,

                          


                            
                        };
            return query.FirstOrDefault();
        }

        public List<ProductItem> RelatedProducts(int currentPage, int rowPerPage,string type, List<ProductItem> listproductItem)
        {
            listproductItem = GetPageBySPQuery2(listproductItem, currentPage, rowPerPage);
            return listproductItem;
        }

        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            var query = from c in Instance.Shop_Category
                        where c.ID >= 1 && c.IsDelete == false && c.IsPublish == true
                        orderby c.Order
                        select new ProductCategoryItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            ParentID = c.ParentID.Value,
                            SEOTitle = c.SEOTitle,
                            IsShowInNavFilter = c.IsShowInNavFilter,
                            listProductItem = c.Shop_Product.Where(o => o.IsDelete == false && o.IsShow == true).OrderBy(m => m.CreatedOnUtc).Select(p => new ProductItem
                                                {
                                                    ID = p.ID,
                                                    Name = p.Name,
                                                    NameAscii = c.NameAscii + "/" + p.NameAscii,
                                                    UrlPicture = p.Gallery_Picture.Folder + p.Gallery_Picture.Url,
                                                    ShortDescription = p.ShortDescription,
                                                    Code = p.Code,
                                                    IsSlide = p.IsSlide,
                                                    PriceNew = p.PriceNew,
                                                    IsShowOnHomePage = p.IsShowOnHomePage,
                                                    IsHot = p.IsHot,
                                                })
                    };
            return query.ToList();
        }

    }
}
