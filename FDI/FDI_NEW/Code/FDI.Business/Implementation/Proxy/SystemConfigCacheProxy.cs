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
    class SystemConfigCacheProxy: SystemConfigFacade, IProxySystemConfig
    {
        #region fields
        private static readonly Lazy<SystemConfigCacheProxy> lazy = new Lazy<SystemConfigCacheProxy>(() => new SystemConfigCacheProxy());
        readonly SystemConfigProxy _systemConfigProxy = SystemConfigProxy.GetInstance;
        #endregion

        #region properties
        public static SystemConfigCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private SystemConfigCacheProxy()
        {
        }

        #endregion
        public SystemConfigItem GetSystemConfigItemByID(int id)
        {
            var param = string.Format("SystemConfigCacheProxyGetSystemConfigItemByID{0}", id);
            
            if (Cache.KeyExistsCache(param))
            {
                var lst = (SystemConfigItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _systemConfigProxy.GetSystemConfigItemByID(id);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire360);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _systemConfigProxy.GetSystemConfigItemByID(id);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire360);
                return lst;
            }
        }

        public List<SystemConfigItem> GetAllListSimple()
        {
            const string param = "SystemConfigCacheProxyGetAllListSimple";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<SystemConfigItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _systemConfigProxy.GetAllListSimple();
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire360);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _systemConfigProxy.GetAllListSimple();
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire360);
                return lst;
            }
        }
        public List<TagItem> GetSystemTagItemByAll()
        {
            const string param = "SystemConfigCacheProxyGetSystemTagItemByAll";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<TagItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _systemConfigProxy.GetSystemTagItemByAll();
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire360);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _systemConfigProxy.GetSystemTagItemByAll();
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire360);
                return lst;
            }
        }
    }
}
