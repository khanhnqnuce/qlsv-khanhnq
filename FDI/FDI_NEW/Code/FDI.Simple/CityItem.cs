using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CityItem : BaseSimple
    {
        public string Name { get; set; }
        public bool Show { get; set; }
        public string Description { get; set; }
        public int TotalDistricts { get; set; }
    }
    public class ModelCityItem : BaseModelSimple
    {
        public IEnumerable<CityItem> ListItem { get; set; }
    }
}
