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
    public class AdvertisingCacheProxy : AdvertisingFacade, IProxyAdvertising
    {
        #region fields
        private static readonly Lazy<AdvertisingCacheProxy> lazy = new Lazy<AdvertisingCacheProxy>(() => new AdvertisingCacheProxy());
        readonly AdvertisingProxy advertisingProxy = AdvertisingProxy.GetInstance;
        #endregion

        #region properties
        public static AdvertisingCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private AdvertisingCacheProxy()
        {
        }

        #endregion
        public List<AdvertisingItem> GetAdvertisingItemByID(int id)
        {
            string param = string.Format("AdvertisingCacheProxyGetAdvertisingItemByID{0}", id);
            if (cache.KeyExistsCache(param))
            {
                var lst = (List<AdvertisingItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = advertisingProxy.GetAdvertisingItemByID(id);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = advertisingProxy.GetAdvertisingItemByID(id);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
    }
}
