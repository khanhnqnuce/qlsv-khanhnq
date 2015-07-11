using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class SystemConfigAbstraction
    {
        IRepositySystemConfig bridge;

        internal SystemConfigAbstraction(IRepositySystemConfig implementation)
        {
            this.bridge = implementation;
        }
        public SystemConfigItem GetSystemConfigItemByIDAbstract(int id)
        {
            return this.bridge.GetSystemConfigItemByID(id);
        }
        public List<SystemConfigItem>  GetAllListSimpleAbstract()
        {
            return this.bridge.GetAllListSimple();
        }
        public List<TagItem> GetSystemTagItemByAllAbstract()
        {
            return this.bridge.GetSystemTagItemByAll();
        }
    }
}
