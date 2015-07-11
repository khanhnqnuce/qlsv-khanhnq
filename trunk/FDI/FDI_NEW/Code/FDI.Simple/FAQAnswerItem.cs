using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class FAQAnswerItem :BaseSimple
    {
        public string Title { get; set; }
        public string TitleAscii { get; set; }
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsShow { get; set; }

        public virtual FAQQuestionItem FAQ_Question { get; set; }
        public virtual IEnumerable<SystemFileItem> System_File { get; set; }
    }
    public class ModelFAQAnswerItem : BaseModelSimple
    {
        public IEnumerable<FAQAnswerItem> ListItem { get; set; }
    }
}
