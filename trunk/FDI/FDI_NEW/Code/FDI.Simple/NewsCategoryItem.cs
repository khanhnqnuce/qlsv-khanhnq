using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;

namespace FDI.Simple
{
    [Serializable]
    public class NewsCategoryItem : BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int ParentID { get; set; }
        public NewsCategoryItem ParentCategoryItem { get; set; }
        public IEnumerable<NewsCategoryitemnew> CategoryItem { get; set; }
        public string Description { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeyword { get; set; }
        public string Class { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsShow { get; set; }
        public bool IsShowInTab { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsShowInHeader { get; set; }
        public bool IsShowInFooter { get; set; }
        public int TotalItems { get; set; }
        public int TotalChilds { get; set; }
        public string Url { get; set; }
        public virtual IEnumerable<NewsItem> ListNewsItem { get; set; }
    }

    public class NewsCategoryitemnew
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
    }

    public class ModelNewsCategoryItem : BaseModelSimple
    {
        public string Container { get; set; }
        public bool SelectMutil { get; set; }

        public IEnumerable<NewsCategoryItem> ListItem { get; set; }
    }
}
