using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CommentReportItem : BaseSimple
    {
        public int? Report { get; set; }
        public int? CommentID { get; set; }
        public string ListCustomerID { get; set; }
        public bool Reported { get; set; }

    }
    public class ModelCommentReportItem : BaseModelSimple
    {
        public IEnumerable<CommentReportItem> ListItem { get; set; }
    }
}
