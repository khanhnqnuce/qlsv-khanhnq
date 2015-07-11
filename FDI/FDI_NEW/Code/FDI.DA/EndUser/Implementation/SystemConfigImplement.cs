using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.EndUser.Implementation
{
    public class SystemConfigImplement : InitDB, IRepositySystemConfig
    {
        #region get data
        public SystemConfigItem GetSystemConfigItemByID(int id)
        {
            var query = from c in Instance.System_Config
                        where c.ID == id
                        select new SystemConfigItem
                        {
                            Name = c.Name,
                            Address = c.Address,
                            Email = c.Email,
                            Email1 = c.Email1,
                            Phone = c.Phone,
                            EmailSend = c.EmailSend,
                            EmailSendPwd = c.EmailSendPwd,
                            EmailReceive = c.EmailReceive,
                            PhoneAdvice = c.PhoneAdvice,
                            PhoneAdvice1 = c.PhoneAdvice1,
                            PhoneAdvice2 = c.PhoneAdvice2,
                            BusinessLicence = c.BusinessLicence,
                            Fax = c.Fax
                        };
            return query.FirstOrDefault();
        }
        public List<SystemConfigItem> GetAllListSimple()
        {
            var query = from c in Instance.System_Config
                        where c.IsShow == true
                        select new SystemConfigItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            Address = c.Address,
                            Email = c.Email,
                            Email1 = c.Email1,
                            Phone = c.Phone,
                            PhoneAdvice = c.PhoneAdvice,
                            PhoneAdvice1 = c.PhoneAdvice1,
                            PhoneAdvice2 = c.PhoneAdvice2,
                            BusinessLicence = c.BusinessLicence,
                            Fax = c.Fax,
                            EmailReceive = c.EmailReceive,
                            EmailSend = c.EmailSend,
                            EmailSendPwd = c.EmailSendPwd,
                            //TotalItems = c.Shop_Category1.Count()

                        };
            return query.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TagItem> GetSystemTagItemByAll()
        {
            var query = from c in Instance.System_Tag
                        where c.IsDelete == false && c.IsHome == true && c.IsShow == true
                        select new TagItem
                        {
                            Name = c.Name,
                            NameAscii = c.NameAscii
                        };
            return query.ToList();
        }
        #endregion
    }
}
