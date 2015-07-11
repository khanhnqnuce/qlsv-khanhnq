using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxySystemConfig
    {
        SystemConfigItem GetSystemConfigItemByID(int id);
        List<SystemConfigItem> GetAllListSimple();
        List<TagItem> GetSystemTagItemByAll();
    }
}
