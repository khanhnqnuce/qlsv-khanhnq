using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class NewsItem : BaseSimple
    {
        public string keyID { get; set; }
        public string Title { get; set; }
        public string TitleAscii { get; set; }
        public string CateAscii { get; set; }
        public string CateName { get; set; }
        //public string AdminName { get; set; }
        public int CateId { get; set; }
        public string Description { get; set; }
        public int? PictureID { get; set; }
        public string PictureUrl { get; set; }
        public string PicturePromotionUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsAllowComment { get; set; }
        public bool? IsDeleted { get; set; }
        public string Details { get; set; }
        public int TotalComments { get; set; }
        public string PriceBeforeDown { get; set; }
        public string PriceAfterDown { get; set; }
        //ảnh săn khuyến mãi
        public int? PromotionPictureID { get; set; }
        //ảnh icon khuyến mãi
        public int? PromotionPictureIconID { get; set; }
        public string Link { get; set; }
        //mô tả khuyến mãi
        public string PromotionDescription { get; set; }
        public string PromotionCaption { get; set; }
        //thông tin mô tả khuyến mãi
        public string PromotionInfo { get; set; }
        //ngày bắt đầu khuyến mãi
        public DateTime? StartPromotionDate { get; set; }
        //ngày kết thúc khuyến mãi
        public DateTime? EndPromotionDate { get; set; }
        public int? clicked { get; set; }
        public int? SalePercent { get; set; }
        public List<NewsCategoryItem> NewsCategory { get; set; }
        public IEnumerable<NewsCategoryItem> IEnumerableNewsCategory { get; set; }
        public IEnumerable<TagItem> NewTag { get; set; }
        public List<NewsCommentItem> NewsComment { get; set; }
        public List<ProductItem> NewsProduct { get; set; }
        public List<ProductItem> NewsProducts { get; set; }
        public int NewsProductRefrenceId { get; set; }
        public int? TotalOrder { get; set; }
        public int? Viewed { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public PictureItem GalleryPicture { get; set; }
        //ngày bắt đầu hiển thị tin tức lên web
        public DateTime? StartDateDisplay { get; set; }
        public bool? IsAppendId { get; set; }
        //public int TotalRecords { get; set; }
        public Guid? Author { get; set; }
        public Guid? Modifier { get; set; }
        public string IP { get; set; }
        public bool? NoIndex { get; set; }
        public string GoogleMap { get; set; }
        public bool? IsReadMore { get; set; }
        //Khanhnv 03/31/15
        public string UserNameCreate;
        //Khanhnv 03/31/15
    }
    public class ModelNewsItem : BaseModelSimple
    {
        public IEnumerable<NewsItem> ListItem { get; set; }
        public IEnumerable<NewsCategoryItem> ListCategoryItems { get; set; }
        public IEnumerable<PictureItem> ListPictureItems { get; set; }
        public string TenDanhMuc;
    }

    public class ModelNewsINewsRelated : BaseModelSimple
    {
        public NewsItem Item { get; set; }
        public IEnumerable<NewsItem> ListItems { get; set; }
        public string TenDanhMuc;
    }

    public class ModelNewsDichVuItem : BaseModelSimple
    {
        public IEnumerable<NewsItem> ListNewsItem { get; set; }
        public NewsItem NewsItem { get; set; }
        public string TenDanhMuc;
        public string DanhMuc;
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public NewsCategoryItem Cate { get; set; }
        public ModelNewsDichVuItem()
        {
            DanhMuc = "";
        }
    }

    public class ModelNewsGioiThieuItem : BaseModelSimple
    {
        public IEnumerable<NewsItem> ListNewsGioiThieu { get; set; }
        public IEnumerable<NewsItem> ListNewsDichVu { get; set; }
        public NewsItem NewsItem { get; set; }
        public string TenDanhMucGioiThieu;
        public string TenDanhMucDichVu;
    }
}
