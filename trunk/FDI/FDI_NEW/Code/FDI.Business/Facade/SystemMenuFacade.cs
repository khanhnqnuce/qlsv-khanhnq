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
    public class SystemMenuFacade
    {
        public SystemMenuAbstraction SystemMenuDal;
        public CacheController cache;

        public SystemMenuFacade()
        {
            this.SystemMenuDal = new SystemMenuAbstraction(new SystemMenuImplementation());
            this.cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static SystemMenuFacade() { }

        static readonly SystemMenuFacade uniqueInstance = new SystemMenuFacade();

        public static SystemMenuFacade InstanceSystemMenu
        {
            get { return uniqueInstance; }
        }
    }
}
