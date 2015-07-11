using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class AdvertisingItem : BaseSimple
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public int? ZoneID { get; set; }
        public string ZoneName { get; set; }

        public int? TypeID { get; set; }
        public string TypeName { get; set; }

        public int? PictureID { get; set; }
        public string PictureUrl { get; set; }
        public int? Order { get; set; }
        public int? PositionID { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int? Click { get; set; }
        public bool Show { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateOnUtc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<AdvertisingTypeItem> AdvertisingType { get; set; }
        public List<AdvertisingZoneItem> AdvertisingZone { get; set; }

        public PictureItem GalleryPicture { get; set; }
        //public List<AdvertisingPositionItem> AdvertisingPosition { get; set; }
        public AdvertisingPositionItem AdvertisingPosition { get; set; }
    }
    public class ModelAdvertisingItem : BaseModelSimple
    {
        public IEnumerable<AdvertisingItem> ListItem { get; set; }
    }
}
