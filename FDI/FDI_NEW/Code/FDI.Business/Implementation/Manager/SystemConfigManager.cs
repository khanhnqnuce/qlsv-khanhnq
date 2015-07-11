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
    public class SystemConfigManager
    {
        readonly IProxySystemConfig _systemConfig;
        readonly IProxySystemConfig _systemConfigCache;
        private static readonly SystemConfigManager Instance = new SystemConfigManager();

        public static SystemConfigManager GetInstance()
        {
            return Instance;
        }

        public SystemConfigManager()
        {
            _systemConfig = SystemConfigProxy.GetInstance;
            _systemConfigCache = ConfigCache.EnableCache == 1 ? SystemConfigCacheProxy.GetInstance : null;
        }


        public SystemConfigItem GetSystemConfigItemByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _systemConfigCache != null)
            {
                return _systemConfigCache.GetSystemConfigItemByID(id);
            }
            return _systemConfig.GetSystemConfigItemByID(id);
        }

        public List<SystemConfigItem> GetAllListSimple()
        {
            if (ConfigCache.EnableCache == 1 && _systemConfigCache != null)
            {
                return _systemConfigCache.GetAllListSimple();
            }
            return _systemConfig.GetAllListSimple();
        }

        public List<TagItem> GetSystemTagItemByAll()
        {
            if (ConfigCache.EnableCache == 1 && _systemConfigCache != null)
            {
                return _systemConfigCache.GetSystemTagItemByAll();
            }
            return _systemConfig.GetSystemTagItemByAll();
        }
    }
}
