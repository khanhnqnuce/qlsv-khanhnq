using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class GalleryAlbumItem :BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int? ImagesID { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsShow { get; set; }
        public bool IsVideo { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class ModelGalleryAlbumItem : BaseModelSimple
    {
        public IEnumerable<GalleryAlbumItem> ListItem { get; set; }
    }
}
