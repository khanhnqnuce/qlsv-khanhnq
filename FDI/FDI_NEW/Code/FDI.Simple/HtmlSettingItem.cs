using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class HtmlSettingItem : BaseSimple
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Url { get; set; }
        public bool Ishow { get; set; }
    }
  
    public class ModelHtmlSettingItem : BaseModelSimple
    {
        public IEnumerable<HtmlSettingItem> ListItem { get; set; }
    }

}
