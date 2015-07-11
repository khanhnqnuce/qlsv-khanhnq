using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class SystemConfigItem : BaseSimple
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PhoneAdvice { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string BusinessLicence { get; set; }
        public string PhoneAdvice1 { get; set; }
        public string PhoneAdvice2 { get; set; }
        public bool? IsShow { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public DateTime? DateCreate { get; set; }
        public string GoogleMap { get; set; }
        public string EmailSend { get; set; }        
        public string EmailSendPwd { get; set; }
        public string EmailReceive { get; set; }
    }
    public class ModelSystemConfigItem : BaseModelSimple
    {
        public IEnumerable<SystemConfigItem> ListItem { get; set; }
    }
}
