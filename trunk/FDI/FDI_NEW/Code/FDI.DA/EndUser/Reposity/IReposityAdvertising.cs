using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityAdvertising
    {
        List<AdvertisingItem> GetAdvertisingItemByID(int id);
    }
}
