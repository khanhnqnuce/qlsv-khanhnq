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
    public partial class Shop_Product_VariantDA : BaseDA
    {

        #region Constructer
        public Shop_Product_VariantDA()
        {
        }

        public Shop_Product_VariantDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_Product_VariantDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        public ProductVariantItem GetColorById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Variant
                        where c.ID == id
                        select new ProductVariantItem()
                        {
                            ID = c.ID,
                            ColorName = c.System_Color.Name,
                            ProductName = c.Shop_Product.Name,
                            FullName = c.Shop_Product.Name + " " + (c.System_Color != null ? !string.IsNullOrEmpty(c.System_Color.Name.Trim()) ? c.System_Color.Name : "" : ""),
                            Price = c.Price.HasValue ? c.Price.Value : (decimal)0,
                            // huyvq add
                            Sku = c.Sku
                        };

            return query.FirstOrDefault();
        }
        public ProductVariantItem GetColorBySku(string sku)
        {
            var query = from c in FDIDB.Shop_Product_Variant
                        where c.Sku == sku
                        select new ProductVariantItem()
                        {
                            ID = c.ID,
                            ColorName = c.System_Color.Name,
                            ProductName = c.Shop_Product.Name,
                            FullName = c.Shop_Product.Name + " " + (c.System_Color != null ? !string.IsNullOrEmpty(c.System_Color.Name.Trim()) ? c.System_Color.Name : "" : ""),
                            Price = c.Price.HasValue ? c.Price.Value : (decimal)0,
                            // huyvq add
                            Sku = c.Sku
                        };

            return query.FirstOrDefault();
        }

        public List<ColorItem> GetListColor()
        {
            var query = from c in FDIDB.System_Color
                        where c.IsShow == true
                        select new ColorItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       Value = c.Value
                                   };
            return query.ToList();
        }

        public List<ProductItem> GetListProduct()
        {
            return FDIDB.Shop_Product.Where(c => c.IsDelete == false).Select(c => new ProductItem()
            {
                ID = c.ID,
                Name = c.Name
            }).ToList();
        }

        public List<ProductVariantItem> getAllListSimple()
        {
            var query = from c in FDIDB.Shop_Product_Variant
                        where c.IsDeleted.Value == false
                        select new ProductVariantItem()
                        {
                            ID = c.ID,
                            ProductID = c.ProductID,
                            ColorID = c.ColorID,
                            Sku = c.Sku,
                            IsRecurring = c.IsRecurring.HasValue && c.IsRecurring.Value,
                            StockQuantity = c.StockQuantity.HasValue ? c.StockQuantity.Value : 0,
                            DisplayStockAvailability = c.DisplayStockAvailability.HasValue && c.DisplayStockAvailability.Value,
                            DisplayStockQuantity = c.DisplayStockQuantity.HasValue && c.DisplayStockQuantity.Value,
                            MinStockQuantity = c.MinStockQuantity.HasValue ? c.MinStockQuantity.Value : 0,
                            NotifyAdminForQuantityBelow = c.NotifyAdminForQuantityBelow.HasValue && c.NotifyAdminForQuantityBelow.Value,
                            OrderMinimumQuantity = c.OrderMinimumQuantity.HasValue ? c.OrderMinimumQuantity.Value : 0,
                            OrderMaximumQuantity = c.OrderMaximumQuantity.HasValue ? c.OrderMaximumQuantity.Value : 0,
                            DisableBuyButton = c.DisableBuyButton.HasValue && c.DisableBuyButton.Value,
                            DisableWishlistButton = c.DisableWishlistButton.HasValue && c.DisableWishlistButton.Value,
                            AvailableForPreOrder = c.AvailableForPreOrder.HasValue && c.AvailableForPreOrder.Value,
                            CallForPrice = c.CallForPrice.HasValue && c.CallForPrice.Value,
                            Price = c.Price.HasValue ? c.Price.Value : (decimal)0,
                            PriceAfterTax = c.PriceAfterTax.HasValue ? c.PriceAfterTax.Value : (decimal)0,
                            PriceBeforeTax = c.PriceBeforeTax.HasValue ? c.PriceBeforeTax.Value : (decimal)0,
                            PriceOnlineBeforeTax = c.PriceOnlineBeforeTax.HasValue ? c.PriceOnlineBeforeTax.Value : (decimal)0,
                            PriceOnline = c.PriceOnline.HasValue ? c.PriceOnline.Value : (decimal)0,
                            AvailableStartDateTimeUtc = c.AvailableStartDateTimeUtc.Value,
                            AvailableEndDateTimeUtc = c.AvailableStartDateTimeUtc.Value,
                            IsPublished = c.IsPublished.Value,
                            IsDeleted = c.IsDeleted.Value,
                            Position = c.Position.Value,
                            IsHomePage = c.IsHomePage.Value,
                            IsSlider = c.IsSlider.Value,
                            CreatedOnUtc = c.CreatedOnUtc.Value,
                            UpdatedOnUtc = c.UpdatedOnUtc.Value,
                            IsBestSeller = c.IsBestSeller.Value,
                            IsAllowOrderOutStock = c.IsAllowOrderOutStock.Value,
                            IsApplyDiscount = c.IsApplyDiscount.Value,
                            MetaKeyWords = c.MetaKeyWords,
                            MetaDescription = c.MetaDescription,
                            PreOrderID = c.PreOrderID.HasValue ? c.PreOrderID.Value : 0,

                            ColorName = c.System_Color.Name,
                            ProductName = c.Shop_Product.Name,
                        };
            return query.ToList();
        }

        public List<Gallery_Picture> getListPictureByArrID(List<int> ArrID)
        {
            var query = from c in FDIDB.Gallery_Picture where ArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<ProductVariantItem> getListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_Variant
                        where c.IsDeleted == false
                        select new ProductVariantItem()
                        {
                            ID = c.ID,
                            ProductID = c.ProductID,
                            ColorID = c.ColorID,
                            Sku = c.Sku,
                            //IsRecurring = c.IsRecurring.HasValue && c.IsRecurring.Value,
                            //StockQuantity = c.StockQuantity.HasValue ? c.StockQuantity.Value : 0,
                            //DisplayStockAvailability = c.DisplayStockAvailability.HasValue && c.DisplayStockAvailability.Value,
                            //DisplayStockQuantity = c.DisplayStockQuantity.HasValue && c.DisplayStockQuantity.Value,
                            //MinStockQuantity = c.MinStockQuantity.HasValue ? c.MinStockQuantity.Value : 0,
                            //NotifyAdminForQuantityBelow = c.NotifyAdminForQuantityBelow.HasValue && c.NotifyAdminForQuantityBelow.Value,
                            //OrderMinimumQuantity = c.OrderMinimumQuantity.HasValue ? c.OrderMinimumQuantity.Value : 0,
                            //OrderMaximumQuantity = c.OrderMaximumQuantity.HasValue ? c.OrderMaximumQuantity.Value : 0,
                            //DisableBuyButton = c.DisableBuyButton.HasValue && c.DisableBuyButton.Value,
                            //DisableWishlistButton = c.DisableWishlistButton.HasValue && c.DisableWishlistButton.Value,
                            //AvailableForPreOrder = c.AvailableForPreOrder.HasValue && c.AvailableForPreOrder.Value,
                            //CallForPrice = c.CallForPrice.HasValue && c.CallForPrice.Value,
                            Price = c.Price.HasValue ? c.Price.Value : (decimal)0,
                            //PriceAfterTax = c.PriceAfterTax.HasValue ? c.PriceAfterTax.Value : (decimal)0,
                            //PriceBeforeTax = c.PriceBeforeTax.HasValue ? c.PriceBeforeTax.Value : (decimal)0,
                            //PriceOnlineBeforeTax = c.PriceOnlineBeforeTax.HasValue ? c.PriceOnlineBeforeTax.Value : (decimal)0,
                            //PriceOnline = c.PriceOnline.HasValue ? c.PriceOnline.Value : (decimal)0,
                            //AvailableStartDateTimeUtc = c.AvailableStartDateTimeUtc.HasValue ? c.AvailableStartDateTimeUtc.Value : DateTime.Now,
                            //AvailableEndDateTimeUtc = c.AvailableEndDateTimeUtc.HasValue ? c.AvailableEndDateTimeUtc.Value : DateTime.Now,
                            //IsPublished = c.IsPublished.HasValue && c.IsPublished.Value,
                            //IsDeleted = c.IsDeleted.HasValue && c.IsDeleted.Value,
                            Position = c.Position.HasValue ? c.Position.Value : 0,
                            //IsHomePage = c.IsHomePage.HasValue && c.IsHomePage.Value,
                            //IsSlider = c.IsSlider.HasValue && c.IsSlider.Value,
                            //CreatedOnUtc = c.CreatedOnUtc.HasValue ? c.CreatedOnUtc.Value : DateTime.Now,
                            //UpdatedOnUtc = c.UpdatedOnUtc.HasValue ? c.UpdatedOnUtc.Value : DateTime.Now,
                            //IsBestSeller = c.IsBestSeller.HasValue && c.IsBestSeller.Value,
                            //IsAllowOrderOutStock = c.IsAllowOrderOutStock.HasValue && c.IsAllowOrderOutStock.Value,
                            //IsApplyDiscount = c.IsApplyDiscount.HasValue && c.IsApplyDiscount.Value,
                            //MetaKeyWords = c.MetaKeyWords,
                            //MetaDescription = c.MetaDescription,
                            //PreOrderID = c.PreOrderID.HasValue ? c.PreOrderID.Value : 0,

                            ColorName = c.System_Color.Name,
                            ProductName = c.Shop_Product.Name,
                            // huyvq add
                            //FullName = c.Shop_Product.Name + " " + (c.System_Color != null ? !string.IsNullOrEmpty(c.System_Color.Name.Trim()) ? c.System_Color.Name : "" : "")
                        };

            #region search by ProductID
            var productID = Convert.ToInt32(httpRequest["ProductID"]);
            if (productID != 0)
            {
                query = query.Where(c => c.ProductID == productID);
            }
            #endregion

            #region search by ColorID
            var colorID = Convert.ToInt32(httpRequest["ColorID"]);
            if (colorID != 0)
            {
                query = query.Where(c => c.ColorID == colorID);
            }
            #endregion

            query = query.SelectByRequest(Request, ref TotalRecord);

            return query.OrderByDescending(c => c.ID).ToList();
        }

        public Shop_Product_PreOrder GetProductPreOrderByID(int id)
        {
            return FDIDB.Shop_Product_PreOrder.FirstOrDefault(c => c.ID == id);
        }

        public List<Shop_Product_Variant> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Variant where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public decimal[] GetPriceBySkuFromHO(string sku)
        {
            var list = FDIDB.Database.SqlQuery<decimal>("EXEC FRT_GetHOPrice {0}", sku).ToArray();
            return list;
        }

        //public string GetRandomNumberWithSku()
        //{
        //    var exec = new ExecStore();
        //    var query = exec.GetRandomNumberWithSku();
        //    return query;

        //}

        #region Check Exits, Add, Update, Delete
        public bool CheckExitsBySku(string sku, int id)
        {
            var query = (from c in FDIDB.Shop_Product_Variant
                         where c.Sku.Equals(sku) && c.ID != id
                         select c).Any();

            return query;
        }

        public bool CheckExitsByColor(int colorId, int productId)
        {
            var query = (from c in FDIDB.Shop_Product_Variant
                         where c.ColorID == colorId && c.ProductID == productId
                         select c).Any();

            return query;
        }

        public bool CheckExitsByColor(int colorId, int productId, int id)
        {
            var query = from c in FDIDB.Shop_Product_Variant
                        where c.ColorID == colorId && c.ProductID == productId && c.ID != id && c.IsDeleted == false
                         select c.ID;

            return query.Any();
        }

        public Shop_Product_Variant getById(int productVariantID)
        {
            var query = from c in FDIDB.Shop_Product_Variant where c.ID == productVariantID select c;
            return query.FirstOrDefault();
        }

        public void Add(Shop_Product_Variant shopProductVariant)
        {
            FDIDB.Shop_Product_Variant.Add(shopProductVariant);
        }

        public void AddGalleryPicture(Shop_Product_Variant_Picture_Mapping pictureMapping)
        {
            FDIDB.Shop_Product_Variant_Picture_Mapping.Add(pictureMapping);
        }

        public void DeleteAllGalleryPicture(int productVariantId)
        {
            var listImage =
                FDIDB.Shop_Product_Variant_Picture_Mapping.Where(i => i.ProductVariantID == productVariantId).ToList();
            listImage.ForEach(p =>
            {
                FDIDB.Shop_Product_Variant_Picture_Mapping.Remove(p);
                Save();
            });
        }

        public void Delete(Shop_Product_Variant shopProductVariant)
        {

            FDIDB.Shop_Product_Variant.Remove(shopProductVariant);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }

        /// <summary>
        /// TungNT
        /// </summary>
        /// <param name="id"> </param>
        /// <returns></returns>
        public ProductVariantItem GetProductVariantItemById(int? id)
        {
            if (id == null || id == 0) return new ProductVariantItem();
            var query = from o in FDIDB.Shop_Product_Variant
                        where o.ID == id
                        select new ProductVariantItem
                                   {
                                       ProductID = o.ProductID,
                                       ColorID = o.ColorID,
                                       Sku = o.Sku,
                                       IsRecurring = o.IsRecurring.HasValue && o.IsRecurring.Value,
                                       IsVoucher = o.IsVoucher
                                   };
            return query.FirstOrDefault();
        }

        public List<ProductVariantItem> GetProductVariantAccessories(int productVariantID)
        {
            var productvariant = getById(productVariantID);

            var lstProductVariant = from pv in FDIDB.Shop_Product_Variant
                                    where
                                        (from c in FDIDB.Shop_Product_Variant
                                         join o in FDIDB.Shop_Product on c.ProductID equals o.ID
                                         join a in FDIDB.Shop_Product_Compatible_Accessories on c.ProductID equals a.ProductID
                                         where
                                             a.ProductID == productvariant.ProductID && a.IsDeleted == false && c.IsDeleted == false &&
                                             o.IsDelete == false
                                         select a.CompatibleAccessoriesID
                                             ).Contains(pv.ProductID)
                                    select new ProductVariantItem
                                               {
                                                   ID = pv.ID,
                                                   ProductID = pv.ProductID,
                                                   ColorID = pv.ColorID,
                                                   Sku = pv.Sku,
                                                   Price = pv.Price.Value,
                                                   ColorName = pv.System_Color.Name,
                                                   ProductName = pv.Shop_Product.Name,
                                                   FullName = pv.Shop_Product.Name + " " + pv.System_Color.Name
                                               };

            var result = lstProductVariant.ToList();
            return result;
        }
        #endregion
    }
}
