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
    public class ShopCategoryFacade
    {
        public ShopCategoryAbstraction ShopCategoryDal;
        public CacheController Cache;

        public ShopCategoryFacade()
        {
            ShopCategoryDal = new ShopCategoryAbstraction(new ShopCategoryImplementation());
            Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static ShopCategoryFacade() { }

        static readonly ShopCategoryFacade UniqueInstance = new ShopCategoryFacade();

        public static ShopCategoryFacade InstanceShopCategory
        {
            get { return UniqueInstance; }
        }
    }
}
