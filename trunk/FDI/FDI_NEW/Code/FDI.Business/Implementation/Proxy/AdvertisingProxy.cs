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
    public class AdvertisingProxy : AdvertisingFacade, IProxyAdvertising
    {
        #region fields
        private static readonly Lazy<AdvertisingProxy> lazy = new Lazy<AdvertisingProxy>(() => new AdvertisingProxy());
        #endregion

        #region properties
        public static AdvertisingProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private AdvertisingProxy()
        {
        }

        #endregion
        public List<AdvertisingItem> GetAdvertisingItemByID(int id)
        {
            return InstanceAdvertising.customerDal.GetAdvertisingItemByIDAbstract(id);
        }
    }
}
