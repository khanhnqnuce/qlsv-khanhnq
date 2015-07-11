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
    public class AdvertisingManager
    {
        readonly IProxyAdvertising _Advertising;
        readonly IProxyAdvertising _AdvertisingCache;
        private static readonly AdvertisingManager Instance = new AdvertisingManager();

        public static AdvertisingManager GetInstance()
        {
            return Instance;
        }

        public AdvertisingManager()
        {
            _Advertising = AdvertisingProxy.GetInstance;
            _AdvertisingCache = ConfigCache.EnableCache == 1 ? AdvertisingCacheProxy.GetInstance : null;
        }
        public List<AdvertisingItem> GetAdvertisingItemByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _AdvertisingCache != null)
            {
                return _AdvertisingCache.GetAdvertisingItemByID(id);
            }
            return _Advertising.GetAdvertisingItemByID(id);
        }
    }
}
