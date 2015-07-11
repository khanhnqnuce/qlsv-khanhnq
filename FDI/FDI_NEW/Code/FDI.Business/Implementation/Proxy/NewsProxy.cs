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
    public class NewsProxy: NewsFacade, IProxyNews
    {
        #region fields
        private static readonly Lazy<NewsProxy> lazy = new Lazy<NewsProxy>(() => new NewsProxy());
        #endregion

        #region properties
        public static NewsProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private NewsProxy()
        {
        }

        #endregion

        public List<NewsItem> GetList()
        {
            return InstanceNews.NewsDal.GetListAbstract();
        }

        public List<NewsItem> GetListHots()
        {
            return InstanceNews.NewsDal.GetListHotsAbstract();
        }

        public NewsItem GetNewsByNameAscii(string nameAscii)
        {
            return InstanceNews.NewsDal.GetNewsByNameAsciiAbstract(nameAscii);
        }
        public List<NewsItem> NewsPage(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem)
        {
            return InstanceNews.NewsDal.NewsPageAsciiAbstract(currentPage, rowPerPage, type, listNewsItem);
        }
    }
}
