using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CommentLikeItem : BaseSimple
    {
        public int Like { get; set; }
        public int? CommentID { get; set; }
        public string ListCustomerID { get; set; }
        public bool Liked { get; set; }

    }
    public class ModelCommentLikeItem : BaseModelSimple
    {
        public IEnumerable<CommentLikeItem> ListItem { get; set; }
    }
}
