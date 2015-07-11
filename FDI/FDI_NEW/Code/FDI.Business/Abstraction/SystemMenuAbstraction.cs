using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class SystemMenuAbstraction
    {
        IRepositySystemMenu bridge;

        internal SystemMenuAbstraction(IRepositySystemMenu implementation)
        {
            this.bridge = implementation;
        }
        public List<MenuItem> GetAllListSimpleAbstract()
        {
            return this.bridge.GetAllListSimple();
        }


        public MenuItem GetAllListSimpleByUrlAbstract(string url)
        {
            return this.bridge.GetAllListSimpleByUrl(url);
        }


    }
}
