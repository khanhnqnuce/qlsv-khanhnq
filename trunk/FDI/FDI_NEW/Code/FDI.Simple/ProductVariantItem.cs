using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductVariantItem : BaseSimple
    {
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public string Sku { get; set; }
        public bool IsRecurring { get; set; }
        public int StockQuantity { get; set; }
        public bool DisplayStockAvailability { get; set; }
        public bool DisplayStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public bool NotifyAdminForQuantityBelow { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool DisableWishlistButton { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public bool CallForPrice { get; set; }
        public decimal Price { get; set; }
        public decimal PriceAfterTax { get; set; }
        public decimal PriceBeforeTax { get; set; }
        public decimal PriceOnlineBeforeTax { get; set; }
        public decimal PriceOnline { get; set; }
        public decimal PriceMarket { get; set; }
        public DateTime AvailableStartDateTimeUtc { get; set; }
        public DateTime AvailableEndDateTimeUtc { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public int Position { get; set; }
        public bool IsHomePage { get; set; }
        public bool IsSlider { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsAllowOrderOutStock { get; set; }
        public bool? IsApplyDiscount { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public int PreOrderID { get; set; }
        public string FrtVatGourpSa { get; set; }

        public string FullName { get; set; }
        public string ColorName { get; set; }
        public string ProductName { get; set; }

        public bool? IsApplyDiscountSpecial { get; set; }
        public bool? IsApplyDiscountCrossell { get; set; }
        public bool? IsApplyDiscountShoppingCart { get; set; }

        public decimal AmountDiscountOnline { get; set; }
        public decimal PriceInDiscount { get; set; }
        public bool OutStock { get; set; }
        public bool? IsVoucher { get; set; }
        public ColorItem Color { get; set; }
        public ProductItem Product { get; set; }
        public List<PictureItem> ListGallery { get; set; }
        public List<PromotionGroupOfProductItem> Promotion { get; set; }
        public string PromotionText { get; set; }
        public string PromotionAdmin { get; set; }
        public string PromotionSummary { get; set; }
        public decimal? MinDeposit { get; set; }
        public bool ShowBuyButton
        {
            get
            {
                return (((StockQuantity > 4 || IsAllowOrderOutStock)
                         && Price > 0
                         && !DisableBuyButton)
                        || AvailableForPreOrder);
            }
        }

    }
    public class ModelProductVariantItem : BaseModelSimple
    {
        public IEnumerable<ProductVariantItem> ListItem { get; set; }
    }
}
