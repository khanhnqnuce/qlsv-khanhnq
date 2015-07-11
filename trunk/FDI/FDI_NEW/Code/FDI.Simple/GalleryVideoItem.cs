using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class GalleryVideoItem
    {
        public int? AlbumID { get; set; }
        public int? CategoryID { get; set; }
        public string Name { get; set; }
        public int? PictureID { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsShow { get; set; }
        public bool? IsDeleted { get; set; }

        //public virtual Gallery_Album Gallery_Album { get; set; }
        //public virtual Gallery_Category Gallery_Category { get; set; }
        //public virtual Gallery_Picture Gallery_Picture { get; set; }
        //public virtual ICollection<Shop_Campaign_Template> Shop_Campaign_Template { get; set; }
    }
    public class ModelGalleryVideoItem : BaseModelSimple
    {
        public IEnumerable<GalleryVideoItem> ListItem { get; set; }
    }
}
