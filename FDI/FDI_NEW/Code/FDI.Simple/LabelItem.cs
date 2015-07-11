using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class LabelItem : BaseSimple
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsShow { get; set; }
        public bool IsShowInSearch { get; set; }
        public int PictureID { get; set; }
    }
    public class ModelLabelItem : BaseModelSimple
    {
        public IEnumerable<LabelItem> ListItem { get; set; }
    }
}
