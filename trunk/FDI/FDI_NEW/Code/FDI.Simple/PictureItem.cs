using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class PictureItem : BaseSimple
    {
        public int? AlbumID { get; set; }
        public int? CategoryID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SourceID { get; set; }
        public string Folder { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }

        //public virtual ICollection<Customer> Customers { get; set; }
        public GalleryCategoryItem Gallery_Category { get; set; }
        public GalleryAlbumItem Gallery_Album { get; set; }
     
    }
    public class ModelPictureItem : BaseModelSimple
    {
        public string Container { get; set; }
        public IEnumerable<PictureItem> ListItem { get; set; }
       
    }
}
