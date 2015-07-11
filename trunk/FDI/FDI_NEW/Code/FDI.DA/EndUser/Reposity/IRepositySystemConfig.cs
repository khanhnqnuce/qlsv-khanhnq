using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IRepositySystemConfig
    {
        SystemConfigItem GetSystemConfigItemByID(int id);
        List<SystemConfigItem> GetAllListSimple();
        List<TagItem> GetSystemTagItemByAll();
    }
}
