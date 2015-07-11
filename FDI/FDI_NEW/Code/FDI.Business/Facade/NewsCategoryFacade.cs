using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Abstraction;
using FDI.DA.EndUser.Implementation;
using FDI.Memcached;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Facade
{
    public class NewsCategoryFacade
    {
        public NewsCategoryAbstraction NewsCategoryDal;
        public CacheController Cache;

        public NewsCategoryFacade()
        {
            NewsCategoryDal = new NewsCategoryAbstraction(new NewsCategoryImplementation());
            Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static NewsCategoryFacade() { }

        static readonly NewsCategoryFacade UniqueInstance = new NewsCategoryFacade();

        public static NewsCategoryFacade InstanceNewsCategory
        {
            get { return UniqueInstance; }
        }
    }
}
