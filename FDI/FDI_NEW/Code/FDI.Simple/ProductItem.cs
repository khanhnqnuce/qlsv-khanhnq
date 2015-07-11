using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductItem : BaseSimple
    {
        public int? LabelID { get; set; }
        public int? BrandID { get; set; }
        public int? PictureID { get; set; }
        public int? ProductTypeID { get; set; }
        public int CountForumTheard { get; set; }
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string IncludeInfo { get; set; }
        public string Details { get; set; }
        public string Overview { get; set; }
        public int? DisplayOrder { get; set; }
        public int? DisplayOrderCategory { get; set; }

        public int? StockTypeID { get; set; }
        public int? DiscountID { get; set; }
        public decimal SizeLength { get; set; }
        public decimal SizeWidth { get; set; }
        public decimal SizeHeight { get; set; }
        public decimal Weight { get; set; }
        public decimal? PriceMarket { get; set; }
        public string Warranty { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsSlide { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsPreOrder { get; set; }
        public bool? IsDelete { get; set; }
        public int? Viewed { get; set; }
        public bool? IsShowOnHomePage { get; set; }
        public bool? IsAllowRating { get; set; }
        public bool? IsAllowComment { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string UrlProduct { get; set; }
        public string UrlPicture { get; set; }
        public string UrlPictureAuto { get; set; }
        public string DisplayDiscount { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeyword { get; set; }
        public decimal? PriceOld { get; set; }
        public decimal? PriceNew { get; set; }
      public string Policy { get; set; }
        public bool? IsShowFormIdea { get; set; }
        public bool? IsApplyNewTemplate { get; set; }
        public string UrlLabelPicture { get; set; }
        public string ProductTypeName { get; set; }
        public string Sku { get; set; }
     public string OverviewVideo { get; set; }
        public string Overview360 { get; set; }
        public string VideoDetail { get; set; }
        public string VideoRating { get; set; }
        public string HightlightsShortDes { get; set; }
        public string AttachedAccessories { get; set; }
        public int DisplayOrderSearch { get; set; }
        public string Guide { get; set; }
        public ProductVariantItem ProductVariant { get; set; }
        public ProductTypeItem ProductType { get; set; }
        public ProductBrandItem Brand { get; set; }
        public List<ProductVariantItem> ListProductVariant { get; set; }
        public List<ProductItem> ProductAccessories { get; set; }
        public List<PictureItem> ListPicture360 { get; set; }
        public List<PictureItem> ListPictureSlide { get; set; }
        public List<PictureItem> ListPictureGallery { get; set; }
        public List<PictureItem> ListPictureGalleryGuideSlide { get; set; }
        public string ShortDescription { get; set; }
        public string HightlightsDes { get; set; }
        public int? PictureHotID { get; set; }
        public string UrlHotPicture { get; set; }
        public string PromotionInfo { get; set; }
        public string InfoDriver { get; set; }
        public bool? IsComingSoon { get; set; }
        public int? DisplayOrderOnMobile { get; set; }
        public int? DisplayOrderApple { get; set; }
        public int? DisplayOrderBestSeller { get; set; }
        public int? DisplayOrderBestSellerType { get; set; }
        public int? DisplayOrderHotByCate { get; set; }
        public List<ProductItem> ListProductRef { get; set; }
        public List<ProductItem> ListProductAccessories { get; set; }
        public List<ProductGroupAttributeMapItem> ProductAttributes { get; set; }
        public List<TagItem> ProductTags { get; set; }
        public List<ProductRelatedItem> ListProductCompare { get; set; }
        public virtual IEnumerable<CategoryItem> ListShopCategoryItem { get; set; }


    }
    public class ModelProductItem : BaseModelSimple
    {
        
        public IEnumerable<ProductItem> ListItem { get; set; }
        public IEnumerable<ColorItem> ListColorItem { get; set; }
    }
}
