using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class AdvertisingPositionItem : BaseSimple
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class ModelAdvertisingPositionItem : BaseModelSimple
    {
        public IEnumerable<AdvertisingPositionItem> ListItem { get; set; }
    }
}
