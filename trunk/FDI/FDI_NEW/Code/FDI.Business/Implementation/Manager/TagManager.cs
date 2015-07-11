using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Implementation.Proxy;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Manager
{
    public class TagManager
    {
        readonly IProxyTag _Tag;
        readonly IProxyTag _TagCache;
        private static readonly TagManager Instance = new TagManager();

        public static TagManager GetInstance()
        {
            return Instance;
        }

        public TagManager()
        {
            _Tag = TagProxy.GetInstance;
            _TagCache = ConfigCache.EnableCache == 1 ? TagCacheProxy.GetInstance : null;
        }

        public List<TagItem> GetByNamAssi(string nameassi)
        {
            if (ConfigCache.EnableCache == 1 && _TagCache != null)
            {
                return _TagCache.GetByNamAssi(nameassi);
            }
            return _Tag.GetByNamAssi(nameassi);
        }
        public TagItem GetByNamAcssi(string nameacssi)
        {
            if (ConfigCache.EnableCache == 1 && _TagCache != null)
            {
                return _TagCache.GetByNamAcssi(nameacssi);
            }
            return _Tag.GetByNamAcssi(nameacssi);
        }

       

        public List<TagItem> GetAllListSimple()
        {
            if (ConfigCache.EnableCache == 1 && _TagCache != null)
            {
                return _TagCache.GetAllListSimple();
            }
            return _Tag.GetAllListSimple();
        }
        public List<TagItem> GetAllHomeListSimple()
        {
            if (ConfigCache.EnableCache == 1 && _TagCache != null)
            {
                return _TagCache.GetAllHomeListSimple();
            }
            return _Tag.GetAllHomeListSimple();
        }
        public List<TagItem> GetAllListSimpleByUrl(string url)
        {
            if (ConfigCache.EnableCache == 1 && _TagCache != null)
            {
                return _TagCache.GetAllListSimpleByUrl(url);
            }
            return _Tag.GetAllListSimpleByUrl(url);
        }
       
       
    }
}
