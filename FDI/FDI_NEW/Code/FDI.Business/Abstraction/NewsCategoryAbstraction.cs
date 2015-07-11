using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class NewsCategoryAbstraction
    {
        readonly IReposityNewsCategory _bridge;

        internal NewsCategoryAbstraction(IReposityNewsCategory implementation)
        {
            _bridge = implementation;
        }

        public List<NewsCategoryItem> GetListAbstract()
        {
            return _bridge.GetList();
        }

        public NewsCategoryItem GetNewsCategoryByNameAsciiAbstract(string nameAscii)
        {
            return _bridge.GetNewsCategoryByNameAscii(nameAscii);
        }

        public NewsCategoryItem GetNewsCategoryByIdAbstract(int id)
        {
            return _bridge.GetNewsCategoryById(id);
        }

        public int InsertNewsCategory(News_Category newsCategory)
        {
           return _bridge.InsertNewsCategory(newsCategory);
        }

        public int AddNewsCategory(News_Category newsCategory)
        {
            return _bridge.AddNewsCategory(newsCategory);
        }

        public void Save()
        {
            _bridge.Save();
        }

    }
}
