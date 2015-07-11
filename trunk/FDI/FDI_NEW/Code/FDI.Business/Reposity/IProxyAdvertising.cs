using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyAdvertising
    {
        List<AdvertisingItem> GetAdvertisingItemByID(int id);
    }
}
