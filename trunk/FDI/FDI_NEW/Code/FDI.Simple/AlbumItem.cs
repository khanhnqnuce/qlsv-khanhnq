using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class AlbumItem :BaseSimple
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int? ImagesID { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsShow { get; set; }
        public bool IsVideo { get; set; }
        public bool? IsDeleted { get; set; }
        public int TotalPictures { get; set; }

        public virtual PictureItem Gallery_Picture { get; set; }
        //public virtual ICollection<Gallery_Picture> Gallery_Picture1 { get; set; }
        //public virtual ICollection<Gallery_Video> Gallery_Video { get; set; }
        public virtual IEnumerable<GalleryCategoryItem> Gallery_Category { get; set; }
        //public virtual ICollection<Shop_Product> Shop_Product { get; set; }
        //public virtual ICollection<System_Tag> System_Tag { get; set; }
    }
    public class ModelAlbumItem : BaseModelSimple
    {
        public IEnumerable<AlbumItem> ListItem { get; set; }
       
    }
}
