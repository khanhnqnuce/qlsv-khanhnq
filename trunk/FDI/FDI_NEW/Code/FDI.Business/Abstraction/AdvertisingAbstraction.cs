using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class AdvertisingAbstraction
    {
        IReposityAdvertising bridge;

        internal AdvertisingAbstraction(IReposityAdvertising implementation)
        {
            this.bridge = implementation;
        }
        public List<AdvertisingItem> GetAdvertisingItemByIDAbstract(int id)
        {
            return this.bridge.GetAdvertisingItemByID(id);
        }

    }
}
