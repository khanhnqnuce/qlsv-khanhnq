using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IRepositySystemMenu
    {
        List<MenuItem> GetAllListSimple();
        MenuItem GetAllListSimpleByUrl(string url);
    }
}
