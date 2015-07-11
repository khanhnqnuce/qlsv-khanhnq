using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ShopCommentItem : BaseSimple
    {
        public int? ProductID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime DateIsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string LinkUrl { get; set; }
        public int? CustomerID { get; set; }
        public CustomerItem Customer { get; set; }
        public string ModifiedBy { get; set; }
        public string NguoiDuyet { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ShopCommentID { get; set; }
        public IEnumerable<ShopCommentItem> LtsShopCommentReply { get; set; }
        public ProductItem Product { get; set; }
        public CommentLikeItem CommentLikeItem { get; set; }
        public CommentReportItem CommentReportItem { get; set; }
        public ShopCommentItem ShopCommentReply { get; set; }
        public int ThoiGianTraLoiComment { get; set; }
        public int? CommentID { get; set; }
        public bool? IsActive { get; set; }
        public bool? DatKPI { get; set; }
        public int TotalReply { get; set; }

        public DateTime? NgayTraLoi { get; set; }

    }
    public class ModelShopCommentItem : BaseModelSimple
    {
        public IEnumerable<ShopCommentItem> ListItem { get; set; }
    }
}
