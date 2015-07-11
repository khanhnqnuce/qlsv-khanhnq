using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Proxy
{
    public class TagCacheProxy : TagFacade, IProxyTag
    {
        #region fields
        private static readonly Lazy<TagCacheProxy> Lazy = new Lazy<TagCacheProxy>(() => new TagCacheProxy());
        readonly TagProxy _TagProxy = TagProxy.GetInstance;
        #endregion

        #region properties
        public static TagCacheProxy GetInstance { get { return Lazy.Value; } }

        #endregion

        #region constructor
        private TagCacheProxy()
        {
        }

        #endregion
        public List<TagItem> GetAllListSimple()
        {
            var param = string.Format("TagCacheProxyGetAllListSimple");

            if (cache.KeyExistsCache(param))
            {
                var lst = (List<TagItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _TagProxy.GetAllListSimple();
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire60);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _TagProxy.GetAllListSimple();
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire60);
                return lst;
            }

        }
        public List<TagItem> GetByNamAssi(string nameassi)
        {
            var param = string.Format("TagCacheProxynameassi{0}", nameassi);

            if (cache.KeyExistsCache(param))
            {
                var lst = (List<TagItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _TagProxy.GetByNamAssi(nameassi);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire60);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _TagProxy.GetByNamAssi(nameassi);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire60);
                return lst;
            }

        }
        public TagItem GetByNamAcssi(string nameacssi)
        {
            var param = string.Format("TagCacheProxynameacssi{0}", nameacssi);

            if (cache.KeyExistsCache(param))
            {
                var lst = (TagItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _TagProxy.GetByNamAcssi(nameacssi);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire60);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _TagProxy.GetByNamAcssi(nameacssi);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire60);
                return lst;
            }

        }
        public List<TagItem> GetAllHomeListSimple()
        {
            var param = string.Format("TagCacheProxyGetAllHomeListSimple");

            if (cache.KeyExistsCache(param))
            {
                var lst = (List<TagItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _TagProxy.GetAllHomeListSimple();
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire60);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _TagProxy.GetAllListSimple();
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire60);
                return lst;
            }

        }
        public List<TagItem> GetAllListSimpleByUrl(string url)
        {
            var param = string.Format("TageCacheProxyGetAllListSimpleByUrl{0}", url);

            if (cache.KeyExistsCache(param))
            {
                var lst = (List<TagItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _TagProxy.GetAllListSimpleByUrl(url);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _TagProxy.GetAllListSimpleByUrl(url);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
    }
}
