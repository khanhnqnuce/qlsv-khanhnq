using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class NewsCommentItem : BaseSimple
    {

        public int? CustomerID { get; set; }
        public int NewsCommentID { get; set; }
        public int? NewsID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsDeleled { get; set; }
        public bool IsShow { get; set; }
        public int TotalLike { get; set; }
        public int TotalReply { get; set; }
        public CommentLikeItem CommentLikeItem { get; set; }
        public CommentReportItem CommentReportItem { get; set; }
        public CustomerItem Customer { get; set; }
        public NewsItem NewsItem { get; set; }
        public int ThoiGianTraLoiComment { get; set; }
        public bool? DatKPI { get; set; }
        public DateTime? NgayTraLoi { get; set; }
    }
    public class ModelNewsCommentItem : BaseModelSimple
    {
        public IEnumerable<NewsCommentItem> ListItem { get; set; }
    }
}
