using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class TagItem : BaseSimple
    {
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public string NameAscii { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsHome { get; set; }
        public int? Weight { get; set; }
        public int TotalAlbum { get; set; }
        public int TotalGuide { get; set; }
        public int TotalNews { get; set; }
        public int TotalProduct { get; set; }
        public int TotalQuestion { get; set; }
        public string Link { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeyword { get; set; }
        public string Description { get; set; }

        public IEnumerable<NewsItem> Newitems { get; set; }
    }
    public class ModelTagItem : BaseModelSimple
    {
        public IEnumerable<TagItem> ListItem { get; set; }

    }
    public class ModelTagItemListNewItem : BaseModelSimple
    {
        public ModelTagItemListNewItem()
        {
            tagitem = new TagItem();
        }
        public IEnumerable<NewsItem> ListItem { get; set; }
        public TagItem tagitem { get; set; }
        public string HtmlPage { get; set; }
    }


}
