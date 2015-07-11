using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class TagAbstraction
    {
        IReposityTag bridge;

        internal TagAbstraction(IReposityTag implementation)
        {
            this.bridge = implementation;
        }
        public List<TagItem> GetAllListSimpleAbstract()
        {
            return this.bridge.GetAllListSimple();
        }
        public List<TagItem> GetByNamAssiAbstract(string nameassi)
        {
            return this.bridge.GetByNamAssi(nameassi);
        }
        public TagItem GetByNamAcssiAbstract(string nameacssi)
        {
            return this.bridge.GetByNamAcssi(nameacssi);
        }
        public List<TagItem> GetAllHomeListSimpleAbstract()
        {
            return this.bridge.GetAllHomeListSimple();
        }


        public List<TagItem> GetAllListSimpleByUrlAbstract(string url)
        {
            return this.bridge.GetAllListSimpleByUrl(url);
        }


    }
}
