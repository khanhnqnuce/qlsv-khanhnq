using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class CommentNewsItem:BaseSimple
    {
       
        public string Name { get; set; }

        public string LinkNews { get; set; }
        public bool? IsActive { get; set; }
        public string Email { get; set; }

        public DateTime? CreateDate { get; set; }
    }
    public class ModelCommentNewsItem : BaseModelSimple
    {
        public IEnumerable<CommentNewsItem> ListItem { get; set; }
    }
}
