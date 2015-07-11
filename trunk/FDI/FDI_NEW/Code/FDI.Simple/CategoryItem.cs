using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CategoryItem : BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int ParentID { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
        public string Description { get; set; }
        public int TotalItems { get; set; }
        public int TotalChilds { get; set; }
    }
    public class ModelCategoryItem : BaseModelSimple
    {
        public IEnumerable<CategoryItem> ListItem { get; set; }
    }
    public class ModelCategoryItemListNews : BaseModelSimple
    {
        public  string SeoTitle { get; set; }
        public  string SeoDescription { get; set; }
        public  string SeoKeywords { get;set; }
        public IEnumerable<NewsItem> ListItem { get; set; }
    }
}

