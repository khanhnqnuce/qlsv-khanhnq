using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;

namespace FDI.Business.Implementation.Proxy
{
    public class NewsCategoryProxy : NewsCategoryFacade, IProxyNewsCategory
    {
        #region fields
        private static readonly Lazy<NewsCategoryProxy> lazy = new Lazy<NewsCategoryProxy>(() => new NewsCategoryProxy());
        #endregion

        #region properties
        public static NewsCategoryProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private NewsCategoryProxy()
        {
        }

        #endregion

        public List<NewsCategoryItem> GetList()
        {
            return InstanceNewsCategory.NewsCategoryDal.GetListAbstract();
        }

        public NewsCategoryItem GetNewsCategoryByNameAscii(string nameAscii)
        {
            return InstanceNewsCategory.NewsCategoryDal.GetNewsCategoryByNameAsciiAbstract(nameAscii);
        }

        public NewsCategoryItem GetNewsCategoryById(int id)
        {
            return InstanceNewsCategory.NewsCategoryDal.GetNewsCategoryByIdAbstract(id);
        }

        public int InsertNewsCategory(News_Category newsCategory)
        {
            return InstanceNewsCategory.NewsCategoryDal.InsertNewsCategory(newsCategory);
        }

        public int AddNewsCategory(News_Category newsCategory)
        {
            return InstanceNewsCategory.NewsCategoryDal.AddNewsCategory(newsCategory);
        }
        public void Save()
        {
            InstanceNewsCategory.NewsCategoryDal.Save();
        }
        //public List<ProductCategoryItem> GetAllListSimple()
        //{
        //    return InstanceShopCategory.ShopCategoryDal.GetAllListSimpleAbstract();
        //}
    }
}
