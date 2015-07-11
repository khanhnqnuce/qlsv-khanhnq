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
    public class SystemMenuManager
    {
        readonly IProxySystemMenu _systemMenu;
        readonly IProxySystemMenu _systemMenuCache;
        private static readonly SystemMenuManager Instance = new SystemMenuManager();

        public static SystemMenuManager GetInstance()
        {
            return Instance;
        }

        public SystemMenuManager()
        {
            _systemMenu = SystemMenuProxy.GetInstance;
            _systemMenuCache = ConfigCache.EnableCache == 1 ? SystemMenuCacheProxy.GetInstance : null;
        }

        public MenuItem GetAllListSimpleByUrl(string url)
        {
            if (ConfigCache.EnableCache == 1 && _systemMenuCache != null)
            {
                return _systemMenuCache.GetAllListSimpleByUrl(url);
            }
            return _systemMenu.GetAllListSimpleByUrl(url);
        }

        public List<MenuItem> GetAllListSimple()
        {
            if (ConfigCache.EnableCache == 1 && _systemMenuCache != null)
            {
                return _systemMenuCache.GetAllListSimple();
            }
            return _systemMenu.GetAllListSimple();
        }

        //phuocnh 05/11/2015
        public List<MenuItem> GetAllChildByMenuId(int id)
        {
            var lst = GetAllListSimple();

            return lst.Where(m => m.MenuParentID == id).ToList();
        }
    }
}
