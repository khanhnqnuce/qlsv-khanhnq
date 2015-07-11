using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class NewsAbstraction
    {
        readonly IReposityNews _bridge;

        internal NewsAbstraction(IReposityNews implementation)
        {
            _bridge = implementation;
        }

        public List<NewsItem> GetListAbstract()
        {
            return _bridge.GetList();
        }

        public List<NewsItem> GetListHotsAbstract()
        {
            return _bridge.GetListHots();
        }

        public List<NewsItem> GetListByIdAbstract(int id)
        {
            return _bridge.GetListById(id);
        }

        public NewsItem GetNewsByNameAsciiAbstract(string nameAscii)
        {
            return _bridge.GetNewsByNameAscii(nameAscii);
        }
        public List<NewsItem> NewsPageAsciiAbstract(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem)
        {
            return _bridge.NewsPage(currentPage, rowPerPage, type, listNewsItem);
        }
    }
}
