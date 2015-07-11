using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class FAQCategoryItem :BaseSimple
    {
      
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int? ParentID { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsShow { get; set; }
        public bool IsShowInTab { get; set; }
        public int TotalChilds { get; set; }
        public int TotalItems { get; set; }

        public virtual IEnumerable<FAQCategoryItem> FAQCategoryItem1 { get; set; }
        public virtual FAQCategoryItem FAQCategoryItem2 { get; set; }
        public virtual IEnumerable<FAQQuestionItem> FAQQuestion { get; set; }
    }
    public class ModelFAQCategoryItem : BaseModelSimple
    {
        public IEnumerable<FAQCategoryItem> ListItem { get; set; }
    }
}
