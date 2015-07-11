using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class VideoItem:BaseSimple
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

        public GalleryAlbumItem GalleryAlbum { get; set; }
        public GalleryCategoryItem GalleryCategory { get; set; }
        public GalleryPictureItem GalleryPicture { get; set; }
    }
    public class ModelVideoItem : BaseModelSimple
    {
        public string Container { get; set; }
        public IEnumerable<VideoItem> ListItem { get; set; }
    }
}
