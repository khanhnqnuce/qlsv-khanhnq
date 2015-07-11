using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class AdvertisingZoneItem : BaseSimple
    {
        public string PageAscii { get; set; }
        public string Page { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ParentID { get; set; }
        public AdvertisingZoneItem ParentAdvertisingZoneItem { get; set; }
        public bool Show { get; set; }
        public int TotalItems { get; set; }
        public int TotalChilds { get; set; }
        public ICollection<AdvertisingPositionItem> AdvertisingPosition { get; set; }
    }
    public class ModelAdvertisingZoneItem : BaseModelSimple
    {
        public IEnumerable<AdvertisingZoneItem> ListItem { get; set; }
    }
}
