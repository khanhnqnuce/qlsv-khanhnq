using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;

namespace FDI.Business.Implementation.Proxy
{
    public class TagProxy: TagFacade, IProxyTag
    {
        #region fields
        private static readonly Lazy<TagProxy> Lazy = new Lazy<TagProxy>(() => new TagProxy());
        #endregion

        #region properties
        public static TagProxy GetInstance { get { return Lazy.Value; } }

        #endregion

        #region constructor
        private TagProxy()
        {
        }

        #endregion
        public List<TagItem> GetAllListSimple()
        {
            return InstanceTag.TagDal.GetAllListSimpleAbstract();
        }
        public TagItem GetByNamAcssi(string nameacssi)
        {
            return InstanceTag.TagDal.GetByNamAcssiAbstract(nameacssi);
        }
        public List<TagItem> GetByNamAssi(string nameassi)
        {
            return InstanceTag.TagDal.GetByNamAssiAbstract(nameassi);
        }
        public List<TagItem> GetAllHomeListSimple()
        {
            return InstanceTag.TagDal.GetAllHomeListSimpleAbstract();
        }
        public List<TagItem> GetAllListSimpleByUrl(string url)
        {
            return InstanceTag.TagDal.GetAllListSimpleByUrlAbstract(url);
        }
    }
}
