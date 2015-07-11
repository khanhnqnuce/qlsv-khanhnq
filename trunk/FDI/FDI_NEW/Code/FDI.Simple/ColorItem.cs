using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ColorItem : BaseSimple
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsShow { get; set; }
        public int TotalProduct { get; set; }
    }
    public class ModelColorItem : BaseModelSimple
    {
        
        public IEnumerable<ColorItem> ListItem { get; set; }
    }
}
