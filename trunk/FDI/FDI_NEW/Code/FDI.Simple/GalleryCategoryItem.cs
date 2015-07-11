using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class GalleryCategoryItem : BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int ParentID { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public string Description { get; set; }
        public int TotalItems { get; set; }
        public int TotalChilds { get; set; }
        public bool IsShowInTab { get; set; }       
        public virtual IEnumerable<GalleryPictureItem> GalleryPicture { get; set; }
        public virtual IEnumerable<GalleryVideoItem> GalleryVideo { get; set; }
        public virtual IEnumerable<GalleryAlbumItem> GalleryAlbum { get; set; }

    }
    public class ModelGalleryCategoryItem : BaseModelSimple
    {
        public IEnumerable<GalleryCategoryItem> ListItem { get; set; }
    }
}
