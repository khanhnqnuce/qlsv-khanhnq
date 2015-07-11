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
    public class SystemConfigProxy: SystemConfigFacade, IProxySystemConfig
    {
        #region fields
        private static readonly Lazy<SystemConfigProxy> lazy = new Lazy<SystemConfigProxy>(() => new SystemConfigProxy());
        #endregion

        #region properties
        public static SystemConfigProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private SystemConfigProxy()
        {
        }

        #endregion
        public SystemConfigItem GetSystemConfigItemByID(int id)
        {
            return InstanceSystemConfig.SystemConfigDal.GetSystemConfigItemByIDAbstract(id);
        }
        public List<SystemConfigItem> GetAllListSimple()
        {
            return InstanceSystemConfig.SystemConfigDal.GetAllListSimpleAbstract();
        }
        public List<TagItem> GetSystemTagItemByAll()
        {
            return InstanceSystemConfig.SystemConfigDal.GetSystemTagItemByAllAbstract();
        }
    }
}
