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
    public class TagFacade
    {
        public TagAbstraction TagDal;
        public CacheController cache;

        public TagFacade()
        {
            this.TagDal = new TagAbstraction(new TagImplementation());
            this.cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static TagFacade() { }

        static readonly TagFacade uniqueInstance = new TagFacade();

        public static TagFacade InstanceTag
        {
            get { return uniqueInstance; }
        }
    }
}
