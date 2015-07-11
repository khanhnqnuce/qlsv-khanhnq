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
    public class NewsFacade
    {
        public NewsAbstraction NewsDal;
        public CacheController Cache;

        public NewsFacade()
        {
            NewsDal = new NewsAbstraction(new NewsImplement());
            Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static NewsFacade() { }

        static readonly NewsFacade UniqueInstance = new NewsFacade();

        public static NewsFacade InstanceNews
        {
            get { return UniqueInstance; }
        }
    }
}
