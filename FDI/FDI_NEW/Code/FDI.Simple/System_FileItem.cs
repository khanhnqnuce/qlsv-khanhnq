using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;


namespace FDI.Simple
{
    public class SystemFileItem:BaseSimple
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int? TypeID { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual IEnumerable<Shop_Promote_File> ShopPromoteFile { get; set; }
        public virtual System_FileType SystemFileType { get; set; }
        public virtual IEnumerable<FAQAnswerItem> FAQAnswer { get; set; }
        public virtual IEnumerable<Guide_Guide> GuideGuide { get; set; }
        public virtual IEnumerable<News_Job> NewsJob { get; set; }
        public virtual IEnumerable<NewsItem> NewsNews { get; set; }
        public virtual IEnumerable<ProductItem> ShopProduct { get; set; }
    }
    public class ModelSystemFileItem : BaseModelSimple
    {
        public IEnumerable<SystemFileItem> ListItem { get; set; }
    }
}
