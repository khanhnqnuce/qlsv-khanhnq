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
    public partial class Shop_ProductDA : BaseDA
    {
        #region Constructer

        public Shop_ProductDA()
        {
        }

        public Shop_ProductDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_ProductDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }

        #endregion




        public List<ProductItem> GetAllSelectList(List<ProductItem> LtsSource, int CategoryIDRemove, bool checkShow)
        {
            if (checkShow)
                LtsSource = LtsSource.Where(o => o.IsDelete == false && o.IsShow == true).ToList();
            var ltsConvert = new List<ProductItem>
                                 {
                                     new ProductItem
                                         {
                                             ID = 1,
                                             Name = "Thư mục gốc"
                                         }
                                 };

            BuildTreeListItem(LtsSource, 1, string.Empty, CategoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        private void BuildTreeListItem(List<ProductItem> LtsItems, int RootID, string space, int CategoryIDRemove,
                                       ref List<ProductItem> LtsConvert)
        {
            space += "---";

            foreach (var t in LtsItems)
            {
                t.Name = string.Format("|{0} {1}", space, t.Name);
                LtsConvert.Add(t);
            }
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductItem> GetAllListSimpleByProductTypeID(int productTypeID)
        {
            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false && c.ProductTypeID == productTypeID
                        orderby c.Name
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       LabelID = c.LabelID.HasValue ? c.LabelID.Value : 0,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="productTypeID"> </param>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductItem> GetAllListSimpleByProductTypeID(int productTypeID, bool isShow)
        {
            var query = from c in FDIDB.Shop_Product
                        where (c.IsShow == isShow && c.IsDelete == false && c.ProductTypeID == productTypeID)
                        orderby c.Name
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };
            return query.ToList();
        }

        public bool CheckProductHot(int productId, int chk)
        {
            try
            {
                var product = FDIDB.Shop_Product.FirstOrDefault(c => c.ID == productId);

                if (product != null)
                {
                    product.IsHot = chk == 0;
                    FDIDB.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductItem> GetAllListSimple()
        {
            var query = from c in FDIDB.Shop_Product
                        where !c.IsDelete == false
                        orderby c.Name
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };
            return query.OrderByDescending(c => c.ID).ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Product
                        where (c.IsShow == isShow && c.IsDelete == false)
                        orderby c.Name
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };
            return query.ToList();
        }
        /// <summary>
        /// hungdc3 10/12/2013
        /// edit by hungdc3 24/04/2014 add c.IsShow
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductItem> getListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where c.Name.Contains(keyword) && c.IsDelete == false && c.IsShow == false
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       ProductType = new ProductTypeItem
                                                         {
                                                             NameAscii = c.Shop_Product_Type.NameAscii
                                                         }
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// hungdc3 10/12/2013
        /// edit by hungdc3 24/04/2014 add c.IsShow
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductItem> GetListSimpleByAutoCompleteForum(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where c.Name.Contains(keyword) && c.IsDelete == false && c.IsShow == false && c.Shop_Product_Variant.Count(p => p.IsDeleted == false && p.IsPublished == true) > 0
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       ProductType = new ProductTypeItem
                                                         {
                                                             NameAscii = c.Shop_Product_Type.NameAscii
                                                         }
                                   };
            return query.Take(showLimit).ToList();
        }
        /// <summary>
        /// phenv 4/04/2013
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductItem> GetListSimpleByApple(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where (c.Name.ToLower().Contains(keyword) || c.Shop_Product_Variant.Any(p => p.Sku.Contains(keyword)))
                        && c.BrandID == 2
                        && c.ProductTypeID <= 4
                        && (!c.DisplayOrderApple.HasValue || c.DisplayOrderApple == 0)
                        && c.Shop_Product_Variant.Count(p => p.IsPublished == true && p.IsDeleted == false) > 0
                        && c.IsDelete == false
                        && c.IsShow == false
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       UrlPicture = c.Gallery_Picture.Folder + c.Gallery_Picture.Url,
                                       DisplayOrderApple = c.DisplayOrderApple,
                                   };
            return query.Take(showLimit).ToList();
        }

        public List<ProductItem> GetListSortApple()
        {

            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false && c.BrandID == 2 && c.DisplayOrderApple.HasValue && c.DisplayOrderApple > 0
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       UrlPicture = c.Gallery_Picture.Folder + c.Gallery_Picture.Url,
                                       NameAscii = c.NameAscii,
                                       DisplayOrderApple = c.DisplayOrderApple,
                                   };

            return query.OrderByDescending(c => c.DisplayOrderApple).ToList();
        }

        /// <summary>
        /// create by BienLV 22-05-2014
        /// get list product sorted of bestseller product
        /// </summary>
        /// <param name="delete"></param>
        /// <returns></returns>
        public List<ProductItem> GetListSortBestSeller(bool delete = false)
        {

            var query = from c in FDIDB.Shop_Product
                        where c.Shop_Product_Variant.Count(p => p.Price > 0 && p.IsPublished == true && p.IsDeleted == delete) > 0 && c.IsDelete == delete && c.DisplayOrderBestSeller > 0
                        orderby c.DisplayOrderBestSeller descending
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       UrlPicture = c.Gallery_Picture.Folder + c.Gallery_Picture.Url,
                                       NameAscii = c.NameAscii,
                                       DisplayOrderBestSeller = c.DisplayOrderBestSeller
                                   };

            return query.ToList();
        }

        /// <summary>
        /// create by BienLV 30-05-2014
        /// get list product sorted of bestseller product of type
        /// </summary>
        /// <param name="delete"></param>
        /// <returns></returns>
        public List<ProductItem> GetListSortBestSellerType(bool delete = false)
        {

            var query = from c in FDIDB.Shop_Product
                        where c.Shop_Product_Variant.Count(p => p.Price > 0 && p.IsPublished == true && p.IsDeleted == delete) > 0 && c.IsDelete == delete && c.DisplayOrderBestSellerType > 0
                        orderby c.DisplayOrderBestSellerType descending
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       UrlPicture = c.Gallery_Picture.Folder + c.Gallery_Picture.Url,
                                       NameAscii = c.NameAscii,
                                       DisplayOrderBestSellerType = c.DisplayOrderBestSellerType,
                                       ProductType = new ProductTypeItem
                                                         {
                                                             ID = c.Shop_Product_Type != null ? c.Shop_Product_Type.ID : 0,
                                                             NameAscii = c.Shop_Product_Type.NameAscii
                                                         }
                                   };

            return query.ToList();
        }


        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<ProductItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
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
        public List<ProductItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.IsDelete == false
                        && c.Name.StartsWith(keyword)
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       Details = c.Details,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// create by BienLV 22-05-2014
        /// get auto complete products in page sort product bestseller mobile
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public List<ProductItem> GetAutoCompleteBestSeller(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where (c.Name.Contains(keyword) || c.Shop_Product_Variant.Any(p => p.Sku.Contains(keyword) && p.IsPublished == isShow && p.IsDeleted == false))
                        && c.IsShow == isShow
                        && c.IsDelete == false
                        && (c.DisplayOrderBestSeller == 0 || !c.DisplayOrderBestSeller.HasValue)
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       DisplayOrderBestSeller = c.DisplayOrderBestSeller.HasValue ? c.DisplayOrderBestSeller.Value : 0,
                                       UrlPicture = c.Gallery_Picture.Folder + c.Gallery_Picture.Url
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// create by BienLV 22-05-2014
        /// get auto complete products in page sort product bestseller mobile
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public List<ProductItem> GetAutoCompleteBestSellerType(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where (c.Name.Contains(keyword) || c.Shop_Product_Variant.Any(p => p.Sku.Contains(keyword) && p.IsPublished == isShow && p.IsDeleted == false))
                        && c.IsShow == isShow
                        && c.IsDelete == false
                        && (c.DisplayOrderBestSellerType == 0 || !c.DisplayOrderBestSellerType.HasValue)
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       DisplayOrderBestSellerType = c.DisplayOrderBestSellerType.HasValue ? c.DisplayOrderBestSellerType.Value : 0,
                                       UrlPicture = c.Gallery_Picture.Folder + c.Gallery_Picture.Url,
                                       ProductType = new ProductTypeItem
                                                         {
                                                             ID = c.Shop_Product_Type != null ? c.Shop_Product_Type.ID : 0,
                                                             NameAscii = c.Shop_Product_Type.NameAscii
                                                         }
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="productType"> </param>
        /// <param name="showLimit"></param>
        /// <param name="brand"> </param>
        /// <param name="isShow"> </param>
        /// <returns></returns>
        public List<ProductItem> GetAutoCompleteFilter(string keyword, string brand, string productType, int showLimit, bool isShow)
        {

            var query = from c in FDIDB.Shop_Product
                        orderby c.Name
                        where c.IsShow == isShow && c.IsDelete == false && c.Name.Contains(keyword)
                        select c;

            if (!string.IsNullOrEmpty(brand))
            {
                var brandId = Convert.ToInt32(brand);
                query = query.Where(c => c.BrandID == brandId);
            }

            if (!string.IsNullOrEmpty(productType))
            {
                var productTypeId = Convert.ToInt32(productType);
                query = query.Where(c => c.ProductTypeID == productTypeId);
            }

            return query.Select(c => new ProductItem
                                         {
                                             ID = c.ID,
                                             BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                             PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                             ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                             Name = c.Name,
                                             NameAscii = c.NameAscii,
                                         }).Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ProductItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false
                        select c;
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.Select(c => new ProductItem
                                                                      {
                                                                          ID = c.ID,

                                                                          Name = c.Name,
                                                                          NameAscii = c.NameAscii,
                                                                          Code = c.Code,
                                                                          DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                                                          StockTypeID = c.StockTypeID,
                                                                          DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                                                          SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                                                          SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                                                          SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                                                          Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                                                          Warranty = c.Warranty,
                                                                          IsShow = c.IsShow,
                                                                          CreatedOnUtc = c.CreatedOnUtc,
                                                                          UpdatedOnUtc = c.UpdatedOnUtc,
                                                                          CreateBy = c.CreateBy,
                                                                          UpdateBy = c.UpdateBy,
                                                                      }).ToList();
        }

        public List<ProductItem> GetListSimple()
        {
            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false
                        && c.DisplayOrder.HasValue
                        && c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage == true
                        orderby c.DisplayOrder ascending
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       LabelID = c.LabelID.HasValue ? c.LabelID.Value : 0,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };

            return query.ToList();
        }

        public List<ProductItem> GetListSimpleBrandHot(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);

            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false
                        && c.DisplayOrder.HasValue
                        && c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage == true
                        orderby c.DisplayOrder ascending
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       LabelID = c.LabelID.HasValue ? c.LabelID.Value : 0,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Sku = c.Shop_Product_Variant.Where(v => v.ProductID == c.ID).Select(v => v.Sku).FirstOrDefault(),
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };

            #region search by BrandID
            var brandID = Convert.ToInt32(httpRequest["BrandID"]);
            if (brandID != 0)
            {
                query = query.Where(c => c.BrandID == brandID);
            }
            #endregion

            return query.ToList();
        }

        public List<ProductItem> FilterProductByListCategory(HttpRequestBase httpRequest, List<int> listCategoryID, int rowLimit, bool delete = false)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product
                        where c.Shop_Category.Any(p => listCategoryID.Contains(p.ID)) && c.IsDelete == delete
                        select c;

            #region search by Name
            string keyword = Convert.ToString(httpRequest["Keyword"]);
            if (!String.IsNullOrEmpty(keyword))
            {
                if (httpRequest["SearchIn"] != null && httpRequest["SearchIn"].ToLower().Contains("name") && httpRequest["SearchIn"].ToLower().Contains("sku"))
                    query = query.Where(c => c.Name.ToLower().Contains(keyword.ToLower()) || c.Shop_Product_Variant.Select(p => p.Sku.ToLower()).Contains(keyword.ToLower()));
                else if (httpRequest["SearchIn"] != null && httpRequest["SearchIn"].ToLower().Equals("sku"))
                    query = query.Where(c => c.Shop_Product_Variant.Select(p => p.Sku.ToLower()).Contains(keyword.ToLower()));
                else if (httpRequest["SearchIn"] != null && httpRequest["SearchIn"].ToLower().Equals("name"))
                    query = query.Where(c => c.Name.ToLower().Contains(keyword.ToLower()));
            }
            #endregion
            var query1 = query.OrderByDescending(c => c.DisplayOrderCategory).Take(rowLimit).ToList();
            return query1.Select(shopProduct => new ProductItem
                                                    {
                                                        ID = shopProduct.ID,
                                                        Name = shopProduct.Name,
                                                        DisplayOrder = shopProduct.DisplayOrder.HasValue ? shopProduct.DisplayOrder.Value : 0,
                                                        DisplayOrderCategory = shopProduct.DisplayOrderCategory.HasValue ? shopProduct.DisplayOrderCategory.Value : 0,
                                                        IsShow = shopProduct.IsShow,
                                                        IsDelete = shopProduct.IsDelete,
                                                        IsHot = shopProduct.IsHot
                                                    }).ToList();
        }

        public List<ProductItem> GetListSimpleCategoryHot(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            int categoryId = Convert.ToInt32(httpRequest["CategoryID"]);
            var objShopProductDA = new Shop_ProductDA();
            var listCategoryID = objShopProductDA.GetShopCategoryChild(categoryId);

            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false
                              && c.DisplayOrder.HasValue
                              && c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage == true
                              && c.Shop_Category.Any(p => p.ID == listCategoryID.FirstOrDefault())
                        select c;

            query = listCategoryID.Aggregate(query, (current, categoryID) => current.Intersect(from c in FDIDB.Shop_Product
                                                                                               where c.Shop_Category.Any(p => p.ID == categoryID)
                                                                                               select c));

            var result = query.Select(c => new ProductItem
                                               {
                                                   ID = c.ID,
                                                   LabelID = c.LabelID.HasValue ? c.LabelID.Value : 0,
                                                   BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                                   PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                                   ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                                   Sku = c.Shop_Product_Variant.Where(v => v.ProductID == c.ID).Select(v => v.Sku).FirstOrDefault(),
                                                   Name = c.Name,
                                                   NameAscii = c.NameAscii,
                                               
                                                   Code = c.Code,
                                                   Description = c.Description,
                                                   IncludeInfo = c.IncludeInfo,
                                                   Details = c.Details,
                                                   DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                                   DisplayOrderCategory = c.DisplayOrderCategory.HasValue ? c.DisplayOrderCategory.Value : 0,
                                                   StockTypeID = c.StockTypeID,
                                                   DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                                   SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                                   SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                                   SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                                   Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                                   Warranty = c.Warranty,
                                                   IsShow = c.IsShow,
                                                   IsSlide = c.IsSlide,
                                                   IsHot = c.IsHot,
                                                   IsDelete = c.IsDelete,
                                                   Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                                   IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                                   IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                                   IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                                   CreatedOnUtc = c.CreatedOnUtc.Value,
                                                   UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                                   CreateBy = c.CreateBy,
                                                   UpdateBy = c.UpdateBy,

                                               }).ToList();

            return result.OrderByDescending(c => c.DisplayOrderCategory).ToList();

        }

        /// <summary>
        /// Get danh sách product in homepage
        /// </summary>
        /// hieunv16 - 18/03/2014 - created


        public List<ProductItem> GetListSimpleProductInHomePage(bool isShowHome)
        {

            var query = from c in FDIDB.Shop_Product
                        where c.IsDelete == false && c.DisplayOrder.HasValue && c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage == isShowHome
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       LabelID = c.LabelID.HasValue ? c.LabelID.Value : 0,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Sku = c.Shop_Product_Variant.Where(v => v.ProductID == c.ID).Select(v => v.Sku).FirstOrDefault(),
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };

            return query.OrderByDescending(c => c.DisplayOrder).ToList();
        }


        /// <summary>
        /// Get danh sách product in homepage paging
        /// </summary>
        /// hieunv16 - 18/03/2014 - created


        public List<ProductItem> GetListSimpleProductInHomePage(HttpRequestBase httpRequest, bool isShowHome)
        {
            Request = new ParramRequest(httpRequest);

            var query = from c in FDIDB.Shop_Product
                        where
                            c.IsDelete == false && c.DisplayOrder.HasValue && c.IsShowOnHomePage.HasValue &&
                            c.IsShowOnHomePage == isShowHome
                        select c;

            #region search by BrandID
            var brandID = Convert.ToInt32(httpRequest["BrandID"]);
            if (brandID != 0)
            {
                query = query.Where(c => c.BrandID == brandID);
            }
            #endregion

            #region search by ProductTypeID
            var productTypeID = Convert.ToInt32(httpRequest["ProductTypeID"]);
            if (productTypeID != 0)
            {
                query = query.Where(c => c.ProductTypeID == productTypeID);
            }
            #endregion
            string keyword = Convert.ToString(httpRequest["Keyword"]);
            if (!String.IsNullOrEmpty(keyword))
            {
                if (httpRequest["SearchIn"] != null && httpRequest["SearchIn"].ToLower().Contains("name") && httpRequest["SearchIn"].ToLower().Contains("sku"))
                    query = query.Where(c => c.Name.ToLower().Contains(keyword.ToLower()) || c.Shop_Product_Variant.Select(p => p.Sku.ToLower()).Contains(keyword.ToLower()));
                else if (httpRequest["SearchIn"] != null && httpRequest["SearchIn"].ToLower().Equals("sku"))
                    query = query.Where(c => c.Shop_Product_Variant.Select(p => p.Sku.ToLower()).Contains(keyword.ToLower()));
                else if (httpRequest["SearchIn"] != null && httpRequest["SearchIn"].ToLower().Equals("name"))
                    query = query.Where(c => c.Name.ToLower().Contains(keyword.ToLower()));
            }


            //query = query.SelectByRequest(Request, ref TotalRecord);
            var query1 = query.OrderByDescending(c => c.DisplayOrder).Take(100).ToList();
            return query1.Select(shopProduct => new ProductItem
                                                    {
                                                        ID = shopProduct.ID,
                                                        LabelID = shopProduct.LabelID.HasValue ? shopProduct.LabelID.Value : 0,
                                                        BrandID = shopProduct.BrandID.HasValue ? shopProduct.BrandID.Value : 0,
                                                        PictureID = shopProduct.PictureID.HasValue ? shopProduct.PictureID.Value : 0,
                                                        ProductTypeID = shopProduct.ProductTypeID.HasValue ? shopProduct.ProductTypeID.Value : 0,
                                                        Sku = shopProduct.Shop_Product_Variant.Where(v => v.ProductID == shopProduct.ID).Select(v => v.Sku).FirstOrDefault(),
                                                        Name = shopProduct.Name,
                                                        NameAscii = shopProduct.NameAscii,
                                                        Code = shopProduct.Code,
                                                        Description = shopProduct.Description,
                                                        IncludeInfo = shopProduct.IncludeInfo,
                                                        Details = shopProduct.Details,
                                                        DisplayOrder = shopProduct.DisplayOrder.HasValue ? shopProduct.DisplayOrder.Value : 0,
                                                        StockTypeID = shopProduct.StockTypeID,
                                                        DiscountID = shopProduct.DiscountID.HasValue ? shopProduct.DiscountID.Value : 0,
                                                        SizeLength = shopProduct.SizeLength.HasValue ? shopProduct.SizeLength.Value : 0,
                                                        SizeWidth = shopProduct.SizeWidth.HasValue ? shopProduct.SizeWidth.Value : 0,
                                                        SizeHeight = shopProduct.SizeHeight.HasValue ? shopProduct.SizeHeight.Value : 0,
                                                        Weight = shopProduct.Weight.HasValue ? shopProduct.Weight.Value : 0,
                                                        Warranty = shopProduct.Warranty,
                                                        IsShow = shopProduct.IsShow,
                                                        IsSlide = shopProduct.IsSlide,
                                                        IsHot = shopProduct.IsHot,
                                                        IsDelete = shopProduct.IsDelete,
                                                        Viewed = shopProduct.Viewed.HasValue ? shopProduct.Viewed.Value : 0,
                                                        IsShowOnHomePage = shopProduct.IsShowOnHomePage.HasValue && shopProduct.IsShowOnHomePage.Value,
                                                        IsAllowRating = shopProduct.IsAllowRating.HasValue && shopProduct.IsAllowRating.Value,
                                                        IsAllowComment = shopProduct.IsAllowComment.HasValue && shopProduct.IsAllowComment.Value,
                                                        CreatedOnUtc = shopProduct.CreatedOnUtc.Value,
                                                        UpdatedOnUtc = shopProduct.UpdatedOnUtc.Value,
                                                        CreateBy = shopProduct.CreateBy,
                                                        UpdateBy = shopProduct.UpdateBy
                                                    }).ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<ProductItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product
                        where ltsArrID.Contains(c.ID)
                        orderby c.ID descending
                        select new ProductItem
                                   {
                                       ID = c.ID,
                                       BrandID = c.BrandID.HasValue ? c.BrandID.Value : 0,
                                       PictureID = c.PictureID.HasValue ? c.PictureID.Value : 0,
                                       ProductTypeID = c.ProductTypeID.HasValue ? c.ProductTypeID.Value : 0,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IncludeInfo = c.IncludeInfo,
                                       Details = c.Details,
                                       DisplayOrder = c.DisplayOrder.HasValue ? c.DisplayOrder.Value : 0,
                                       StockTypeID = c.StockTypeID,
                                       DiscountID = c.DiscountID.HasValue ? c.DiscountID.Value : 0,
                                       SizeLength = c.SizeLength.HasValue ? c.SizeLength.Value : 0,
                                       SizeWidth = c.SizeWidth.HasValue ? c.SizeWidth.Value : 0,
                                       SizeHeight = c.SizeHeight.HasValue ? c.SizeHeight.Value : 0,
                                       Weight = c.Weight.HasValue ? c.Weight.Value : 0,
                                       Warranty = c.Warranty,
                                       IsShow = c.IsShow,
                                       IsSlide = c.IsSlide,
                                       IsHot = c.IsHot,
                                       IsDelete = c.IsDelete,
                                       Viewed = c.Viewed.HasValue ? c.Viewed.Value : 0,
                                       IsShowOnHomePage = c.IsShowOnHomePage.HasValue && c.IsShowOnHomePage.Value,
                                       IsAllowRating = c.IsAllowRating.HasValue && c.IsAllowRating.Value,
                                       IsAllowComment = c.IsAllowComment.HasValue && c.IsAllowComment.Value,
                                       CreatedOnUtc = c.CreatedOnUtc.Value,
                                       UpdatedOnUtc = c.UpdatedOnUtc.Value,
                                       CreateBy = c.CreateBy,
                                       UpdateBy = c.UpdateBy,
                                   };
            return query.ToList();
        }

        /// <summary>
        /// add by BienLV 11-03-2014
        /// </summary>
        /// <param name="productId">id of product</param>
        /// <returns></returns>
        public ProductItem GetOnlyPictures(int productId)
        {
            var query =
                FDIDB.Shop_Product.Where(c => c.ID == productId).Select(
                    c => new ProductItem
                             {
                                 ID = c.ID
                             });

            var result = query.FirstOrDefault();

            if (result != null)
            {
                var query360 = FDIDB.Shop_Product.Where(p => p.ID == result.ID).Select(p => p.Gallery_Picture2).FirstOrDefault();

                if (query360 != null)
                    result.ListPicture360 = query360.Select(p => new PictureItem
                                                                     {
                                                                         ID = p.ID,
                                                                         Url = p.Folder + p.Url,
                                                                         Name = p.Name,
                                                                         Description = p.Description,
                                                                         DateCreated = p.DateCreated,
                                                                         Gallery_Category = new GalleryCategoryItem
                                                                                                {
                                                                                                    Name = p.Gallery_Category != null ? p.Gallery_Category.Name : string.Empty
                                                                                                }
                                                                     }).ToList();

                var queryGalleryDefault = FDIDB.Shop_Product.Where(p => p.ID == result.ID).Select(p => p.Gallery_Picture3).FirstOrDefault();

                if (queryGalleryDefault != null)
                    result.ListPictureSlide = queryGalleryDefault.Select(p => new PictureItem
                                                                                  {
                                                                                      ID = p.ID,
                                                                                      Url = p.Folder + p.Url,
                                                                                      Name = p.Name,
                                                                                      Description = p.Description,
                                                                                      DateCreated = p.DateCreated,
                                                                                      Gallery_Category = new GalleryCategoryItem
                                                                                                            {
                                                                                                                Name = p.Gallery_Category != null ? p.Gallery_Category.Name : string.Empty
                                                                                                            }
                                                                                  }).ToList();

                var queryGallery = FDIDB.Shop_Product.Where(p => p.ID == result.ID).Select(p => p.Gallery_Picture3).FirstOrDefault();

                if (queryGallery != null)
                    result.ListPictureGallery = queryGallery.Select(p => new PictureItem
                                                                             {
                                                                                 ID = p.ID,
                                                                                 Url = p.Folder + p.Url,
                                                                                 Name = p.Name,
                                                                                 Description = p.Description,
                                                                                 DateCreated = p.DateCreated,
                                                                                 Gallery_Category = new GalleryCategoryItem
                                                                                                       {
                                                                                                           Name = p.Gallery_Category != null ? p.Gallery_Category.Name : string.Empty
                                                                                                       }
                                                                             }).ToList();

                var queryGuideGallery = FDIDB.Shop_Product.Where(p => p.ID == result.ID).Select(p => p.Gallery_Picture3).FirstOrDefault();

                if (queryGuideGallery != null)
                    result.ListPictureGalleryGuideSlide = queryGuideGallery.Select(p => new PictureItem
                                                                                            {
                                                                                                ID = p.ID,
                                                                                                Url = p.Folder + p.Url,
                                                                                                Name = p.Name,
                                                                                                Description = p.Description,
                                                                                                DateCreated = p.DateCreated,
                                                                                                Gallery_Category = new GalleryCategoryItem
                                                                                                                       {
                                                                                                                           Name = p.Gallery_Category != null ? p.Gallery_Category.Name : string.Empty
                                                                                                                       }
                                                                                            }).ToList();
            }
            return result;
        }

        public List<Shop_Category> GetListCategory()
        {
            return FDIDB.Shop_Category.Where(c => c.IsDelete.Value == false).ToList();
        }

        public List<ProductBrandItem> GetListBrand()
        {
            return FDIDB.Shop_Brand.Select(m=>new ProductBrandItem
            {
                ID = m.ID,
                Name = m.Name
            }).ToList();
        }

        public List<ProductTypeItem> GetListProductType()
        {
            return FDIDB.Shop_Product_Type.Select(m=>new ProductTypeItem
            {
                ID = m.ID,
                Name = m.Name
            }).ToList();
        }

        #region Check Exits, Add, Update, Delete
        public bool CheckExitsByName(string name, int productId, bool delete = false)
        {
            var query = (from c in FDIDB.Shop_Product
                         where c.Name.ToLower() == name.ToLower() && c.ID != productId && c.IsDelete == delete
                         select c).Count();

            return query > 0;
        }
        public bool CheckExitsByNameAscii(string name, int productId, bool delete = false)
        {
            var query = (from c in FDIDB.Shop_Product
                         where c.NameAscii == name && c.ID != productId && c.IsDelete == delete
                         select c).Count();

            return query > 0;
        }
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="productID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product GetById(int productID)
        {
            var query = from c in FDIDB.Shop_Product where c.ID == productID select c;
            return query.FirstOrDefault();
        }
        public List<Shop_Product> GetByArrId(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="id"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Category> GetListCategoryByID(int id)
        {
            var query = from c in FDIDB.Shop_Category where c.ID == id select c;
            return query.ToList();
        }

        public List<Shop_Category> GetListCategoryByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<Shop_Product_Attribute_Specification> GetListProductAttributeSpecificationByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Attribute_Specification
                        where ltsArrID.Contains(c.ID) && c.IsDeleted.Value == false
                        select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="id"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Gallery_Picture> GetListPictureByID(int id)
        {
            var query = from c in FDIDB.Gallery_Picture where c.ID == id select c;
            return query.ToList();
        }

        public List<Gallery_Picture> GetListPictureByArrID(List<int> arrID)
        {
            var query = from c in FDIDB.Gallery_Picture where arrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Tag> GetListTagByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Tag where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<System_Tag> GetListTagByArrTag(List<string> ltsArrTag)
        {
            var query = from c in FDIDB.System_Tag where ltsArrTag.Contains(c.Name) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<System_Tag> GetListIntTagByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Tag where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="shopProduct">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(Shop_Product shopProduct)
        {
            var query = from c in FDIDB.Shop_Product where ((c.Name == shopProduct.Name) && (c.ID != shopProduct.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// tag product
        /// </summary>
        /// <param name="productTagName"></param>
        /// <returns></returns>
        public Shop_Product AddOrGet(string productTagName)
        {
            var producttag = GetByName(productTagName);
            if (producttag == null)
            {
                var newproductTag = new Shop_Product
                                        {
                                            Name = productTagName
                                        };
                FDIDB.Shop_Product.Add(newproductTag);

                FDIDB.SaveChanges();
                return newproductTag;
            }
            return producttag;
        }
        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="productName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product GetByName(string productName)
        {
            var query = from c in FDIDB.Shop_Product where ((c.Name == productName)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="shopProduct">bản ghi cần thêm</param>
        public void Add(Shop_Product shopProduct)
        {
            try
            {
                FDIDB.Shop_Product.Add(shopProduct);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ex.StackTrace.ToString();
            }
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="shopProduct">Xóa bản ghi</param>
        public void Delete(Shop_Product shopProduct)
        {

            FDIDB.Shop_Product.Remove(shopProduct);

            shopProduct.Shop_Category.Clear();
            //Shop_Product.System_Color.Clear();
            shopProduct.System_Tag.Clear();
            shopProduct.System_File.Clear();

        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion

        /// <summary>
        /// add by BienLV 11-03-2014
        /// use to add picture of product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="gallery"></param>
        #region add picture

        public void AddList360(int productId, int[] gallery)
        {
            var product = FDIDB.Shop_Product.FirstOrDefault(p => p.ID == productId);
            if (product == null) return;
            product.Gallery_Picture2.Clear();
            FDIDB.SaveChanges();
            foreach (var galleryItem in gallery.Select(i => FDIDB.Gallery_Picture.FirstOrDefault(g => g.ID == i)))
            {
                product.Gallery_Picture2.Add(galleryItem);
                FDIDB.SaveChanges();
            }
        }

        public void AddListSlide(int productId, int[] gallery)
        {
            var product = FDIDB.Shop_Product.FirstOrDefault(p => p.ID == productId);
            if (product == null) return;
            product.Gallery_Picture3.Clear();
            FDIDB.SaveChanges();
            foreach (var galleryItem in gallery.Select(i => FDIDB.Gallery_Picture.FirstOrDefault(g => g.ID == i)))
            {
                product.Gallery_Picture3.Add(galleryItem);
                FDIDB.SaveChanges();
            }
        }

        public void AddListGallery(int productId, int[] gallery)
        {
            var product = FDIDB.Shop_Product.FirstOrDefault(p => p.ID == productId);
            if (product == null) return;
            product.Gallery_Picture3.Clear();
            FDIDB.SaveChanges();
            foreach (var galleryItem in gallery.Select(i => FDIDB.Gallery_Picture.FirstOrDefault(g => g.ID == i)))
            {
                product.Gallery_Picture3.Add(galleryItem);
                FDIDB.SaveChanges();
            }
        }

        public void AddListSlideGuide(int productId, int[] gallery)
        {
            var product = FDIDB.Shop_Product.FirstOrDefault(p => p.ID == productId);
            if (product == null) return;
            product.Gallery_Picture3.Clear();
            FDIDB.SaveChanges();
            foreach (var galleryItem in gallery.Select(i => FDIDB.Gallery_Picture.FirstOrDefault(g => g.ID == i)))
            {
                product.Gallery_Picture3.Add(galleryItem);
                FDIDB.SaveChanges();
            }
        }

        readonly List<int> _lstIntCate = new List<int>();
        public List<int> GetShopCategoryChild(int typeID)
        {
            var query = (from c in FDIDB.Shop_Category
                         where c.ParentID == typeID
                         select new CategoryItem
                                    {
                                        ID = c.ID,
                                        ParentID = c.ParentID.HasValue ? c.ParentID.Value : 0,
                                    });
            foreach (var categoryItem in query)
            {
                _lstIntCate.Add(categoryItem.ID);
                GetShopCategoryChild(categoryItem.ID);
            }
            return _lstIntCate;


        }
        #endregion
        // phenv - 1/7/2014
        public List<ShopProductToExcelItem> ShopProductToExcel(int? cateId)
        {
            var query = FDIDB.Database.SqlQuery<ShopProductToExcelItem>("exec [dbo].[FrtShopProductToExcel] {0}", cateId);
            return query.ToList();
        }
    }
}
