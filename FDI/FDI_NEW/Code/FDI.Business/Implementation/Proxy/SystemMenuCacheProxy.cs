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
    public class SystemMenuCacheProxy : SystemMenuFacade, IProxySystemMenu
    {
        #region fields
        private static readonly Lazy<SystemMenuCacheProxy> Lazy = new Lazy<SystemMenuCacheProxy>(() => new SystemMenuCacheProxy());
        readonly SystemMenuProxy _systemMenuProxy = SystemMenuProxy.GetInstance;
        #endregion

        #region properties
        public static SystemMenuCacheProxy GetInstance { get { return Lazy.Value; } }

        #endregion

        #region constructor
        private SystemMenuCacheProxy()
        {
        }

        #endregion
        public List<MenuItem> GetAllListSimple()
        {
            var param = string.Format("SystemMenuCacheProxyGetAllListSimple");
            
            if (cache.KeyExistsCache(param))
            {
                var lst = (List<MenuItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _systemMenuProxy.GetAllListSimple();
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire60);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _systemMenuProxy.GetAllListSimple();
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire60);
                return lst;
            }
        
        }
        public MenuItem GetAllListSimpleByUrl(string url)
        {
            var param = string.Format("ShopCategoryCacheProxyGetAllListSimpleByUrl{0}", url);

            if (cache.KeyExistsCache(param))
            {
                var lst = (MenuItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = _systemMenuProxy.GetAllListSimpleByUrl(url);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _systemMenuProxy.GetAllListSimpleByUrl(url);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
    }
}
