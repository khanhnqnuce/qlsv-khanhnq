using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductCategoryItem : BaseSimple
    {
        public bool IsPublish { get; set; }
        public bool IsDelete { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int ParentID { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
        public bool? IsShowInTab { get; set; }
        public bool? IsShowInNavFilter { get; set; }
        public bool? IsShowOnBestSeller { get; set; }
        public bool? IsAllowFilter { get; set; }
        public int? FrtRoundingNumber { get; set; }
        public int? Rows { get; set; }
        public int TotalItems { get; set; }
        public int TotalChilds { get; set; }

        /// <summary>
        /// add by BienLV 13-01-2014
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// Add by BienLV 11-02-2014
        /// </summary>
        public string NameAsciiParent { get; set; }

        public IEnumerable<ProductCategoryItem> ProductCategoryItems { get; set; }

        public ProductCategoryItem()
        {
            ProductCategoryItems = new List<ProductCategoryItem>();
        }

        public IEnumerable<ProductItem> listProductItem { get; set; }
    }
    public class ModelProductCategoryItem : BaseModelSimple
    {

        public IEnumerable<ProductCategoryItem> ListItem { get; set; }

    }
}
