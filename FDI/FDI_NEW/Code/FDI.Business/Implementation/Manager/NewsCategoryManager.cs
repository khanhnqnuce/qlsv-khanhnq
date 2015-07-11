using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Implementation.Proxy;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Manager
{
    public class NewsCategoryManager
    {
        readonly IProxyNewsCategory _newsCategory;
        readonly IProxyNewsCategory _newsCategoryCache;
        private static readonly NewsCategoryManager Instance = new NewsCategoryManager();

        public static NewsCategoryManager GetInstance()
        {
            return Instance;
        }

        public NewsCategoryManager()
        {
            _newsCategory = NewsCategoryProxy.GetInstance;
            _newsCategoryCache = ConfigCache.EnableCache == 1 ? NewsCategoryCacheProxy.GetInstance : null;
        }
        public List<NewsCategoryItem> GetList()
        {
            if (ConfigCache.EnableCache == 1 && _newsCategoryCache != null)
            {
                return _newsCategoryCache.GetList();
            }
            return _newsCategory.GetList();
        }

        public NewsCategoryItem GetNewsCategoryByNameAscii(string nameAcsii)
        {
            if (ConfigCache.EnableCache == 1 && _newsCategoryCache != null)
            {
                return _newsCategoryCache.GetNewsCategoryByNameAscii(nameAcsii);
            }
            return _newsCategory.GetNewsCategoryByNameAscii(nameAcsii);
        }

        public NewsCategoryItem GetNewsCategoryByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _newsCategoryCache != null)
            {
                return _newsCategoryCache.GetNewsCategoryById(id);
            }
            return _newsCategory.GetNewsCategoryById(id);
        }

        public int InsertNewsCategory(News_Category newsCategory)
        {
            return _newsCategory.InsertNewsCategory(newsCategory);
        }

        public int AddNewsCategory(News_Category newsCategory)
        {
            return _newsCategory.AddNewsCategory(newsCategory);
        }

        public void Save()
        {
            _newsCategory.Save();
        }
    }
}
