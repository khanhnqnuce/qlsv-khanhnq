using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityNews
    {
        List<NewsItem> GetList();
        List<NewsItem> GetListHots();
        List<NewsItem> GetListById(int id);
        NewsItem GetNewsByNameAscii(string nameAscii);
        List<NewsItem> NewsPage(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem);
    }
}
