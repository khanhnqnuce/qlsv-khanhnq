using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class SystemActionItem
    {
        public bool ViewFull { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Show { get; set; }
        public bool Hide { get; set; }
        public bool Order { get; set; }
        public bool Active { get; set; }
        public bool Public { get; set; }
        public bool Complete { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsKT { get; set; }
    }
}
