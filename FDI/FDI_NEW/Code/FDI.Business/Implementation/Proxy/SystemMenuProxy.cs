using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;

namespace FDI.Business.Implementation.Proxy
{
    public class SystemMenuProxy: SystemMenuFacade, IProxySystemMenu
    {
        #region fields
        private static readonly Lazy<SystemMenuProxy> Lazy = new Lazy<SystemMenuProxy>(() => new SystemMenuProxy());
        #endregion

        #region properties
        public static SystemMenuProxy GetInstance { get { return Lazy.Value; } }

        #endregion

        #region constructor
        private SystemMenuProxy()
        {
        }

        #endregion
        public List<MenuItem> GetAllListSimple()
        {
            return InstanceSystemMenu.SystemMenuDal.GetAllListSimpleAbstract();
        }
        public MenuItem GetAllListSimpleByUrl(string url)
        {
            return InstanceSystemMenu.SystemMenuDal.GetAllListSimpleByUrlAbstract(url);
        }
    }
}
