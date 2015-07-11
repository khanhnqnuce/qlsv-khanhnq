using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductBrandItem : BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
        public int PictureID { get; set; }
        public int LogoPictureID { get; set; }

        public int BrandTotalProduct { get; set; }
    }
    public class ModelProductBrandItem : BaseModelSimple
    {
      
        public IEnumerable<ProductBrandItem> ListItem { get; set; }
        public IEnumerable<ProductTypeItem> ListProductTypeItem { get; set; }
    }
}
