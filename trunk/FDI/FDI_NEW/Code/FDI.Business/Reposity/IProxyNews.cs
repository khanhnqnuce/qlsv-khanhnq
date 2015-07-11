using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyNews
    {
        List<NewsItem> GetList();
        List<NewsItem> GetListHots();
   
        NewsItem GetNewsByNameAscii(string nameAscii);
        List<NewsItem> NewsPage(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem);
    }
}
