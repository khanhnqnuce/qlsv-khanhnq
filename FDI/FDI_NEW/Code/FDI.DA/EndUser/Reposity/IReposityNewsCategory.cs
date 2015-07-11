using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityNewsCategory
    {
        List<NewsCategoryItem> GetList();

        NewsCategoryItem GetNewsCategoryByNameAscii(string nameAscii);
        NewsCategoryItem GetNewsCategoryById(int id);
        int InsertNewsCategory(News_Category newsCategory);

        int AddNewsCategory(News_Category newsCategory);

        void Save();
   
   
    }
}
