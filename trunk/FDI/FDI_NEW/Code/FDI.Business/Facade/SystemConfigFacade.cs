using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Memcached;
using FDI.Business.Abstraction;
using FDI.DA.EndUser.Implementation;
using FDI.Utils;

namespace FDI.Business.Facade
{
    public class SystemConfigFacade
    {
        public SystemConfigAbstraction SystemConfigDal;
        public CacheController Cache;

        public SystemConfigFacade()
        {
            SystemConfigDal = new SystemConfigAbstraction(new SystemConfigImplement());
            Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static SystemConfigFacade() { }

        static readonly SystemConfigFacade UniqueInstance = new SystemConfigFacade();

        public static SystemConfigFacade InstanceSystemConfig
        {
            get { return UniqueInstance; }
        }
    }
}
