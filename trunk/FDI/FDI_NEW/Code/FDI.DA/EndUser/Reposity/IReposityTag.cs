using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityTag
    {
        List<TagItem> GetAllListSimple();
        List<TagItem> GetAllListSimpleByUrl(string url);
        List<TagItem> GetByNamAssi(string nameassi);
        TagItem GetByNamAcssi(string nameacssi);
        List<TagItem> GetAllHomeListSimple();
    }
}
