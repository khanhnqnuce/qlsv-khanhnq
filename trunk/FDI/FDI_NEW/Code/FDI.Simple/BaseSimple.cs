using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class BaseSimple
    {
        public int ID { get; set; }        
    }

    public class BaseModelSimple
    {
        public string PageHtml { get; set; }
        public string Container { get; set; }
        public string StbHtml { get; set; }
        public bool SelectMutil { get; set; }

        public string ValuesSelected { get; set; }
        public SystemActionItem SystemActionItem { get; set; }
    }

}
