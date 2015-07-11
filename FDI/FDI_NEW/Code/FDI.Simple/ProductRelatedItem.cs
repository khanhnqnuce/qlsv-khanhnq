using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductRelatedItem : BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string TypeAscii { get; set; }
        public string ProductPicture { get; set; }
        public int? LabelId { get; set; }
        public int? PictureId { get; set; }
        public string LabelUrl { get; set; }
        public string HightlightsShortDes { get; set; }
        public string HightlightsDes { get; set; }
        public string IncludeInfo { get; set; }
        public string PromotionInfo { get; set; }
        public int? ProductVariantId { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceMarket { get; set; }
    }
    public class ModelProductRelatedItem : BaseModelSimple
    {
        public IEnumerable<ProductRelatedItem> ListItem { get; set; }
    }
}
