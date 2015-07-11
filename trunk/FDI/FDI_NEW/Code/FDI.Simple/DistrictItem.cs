using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class DistrictItem : BaseSimple
    {
        public string Name { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public bool Show { get; set; }
        public string Description { get; set; }
        public int TotalItems { get; set; }
    }

    public class ModelDistrictItem : BaseModelSimple
    {
        public IEnumerable<DistrictItem> ListItem { get; set; }

    }


}
