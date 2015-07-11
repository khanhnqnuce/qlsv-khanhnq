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
    public class ShopProductFacade
    {
        public ShopProductAbstraction ShopProductDal;
        public CacheController Cache;

        public ShopProductFacade()
        {
            ShopProductDal = new ShopProductAbstraction(new ShopProductImplement());
            Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static ShopProductFacade() { }

        static readonly ShopProductFacade UniqueInstance = new ShopProductFacade();

        public static ShopProductFacade InstanceShopProduct
        {
            get { return UniqueInstance; }
        }
    }
}
