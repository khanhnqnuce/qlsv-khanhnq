using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyTag
    {
        List<TagItem> GetAllListSimple();
        List<TagItem> GetAllHomeListSimple();
       List<TagItem> GetAllListSimpleByUrl(string url);
       List<TagItem> GetByNamAssi(string nameassi);
       TagItem GetByNamAcssi(string nameacssi);
    }
}
