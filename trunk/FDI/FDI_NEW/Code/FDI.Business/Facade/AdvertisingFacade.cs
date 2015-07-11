using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Abstraction;
using FDI.DA.EndUser.Implementation;
using FDI.Memcached;
using FDI.Utils;

namespace FDI.Business.Facade
{
    public class AdvertisingFacade
    {
        public AdvertisingAbstraction customerDal;
        public CacheController cache;

        public AdvertisingFacade()
        {
            this.customerDal = new AdvertisingAbstraction(new AdvertisingImplement());
            this.cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static AdvertisingFacade() { }

        static readonly AdvertisingFacade uniqueInstance = new AdvertisingFacade();

        public static AdvertisingFacade InstanceAdvertising
        {
            get { return uniqueInstance; }
        }
    }
}
