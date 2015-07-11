using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
     [Serializable]
    public class FAQQuestionItem : BaseSimple
    {
        public string Title { get; set; }
        public string TitleAscii { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsShow { get; set; }
        public int CategoryID { get; set; }
        public int TotalAnswers { get; set; }
        public virtual IEnumerable<FAQAnswerItem> FAQ_Answer { get; set; }
        public virtual FAQCategoryItem FAQ_Category { get; set; }
        public virtual IEnumerable<ProductItem> Shop_Product { get; set; }
    }
     public class ModelFAQQuestionItem : BaseModelSimple
     {
         public IEnumerable<FAQQuestionItem> ListItem { get; set; }
     }
}
