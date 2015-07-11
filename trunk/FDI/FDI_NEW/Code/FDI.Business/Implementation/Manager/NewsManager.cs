using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Implementation.Proxy;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Manager
{
    public class NewsManager
    {
        readonly IProxyNews _news;
        readonly IProxyNews _newsCache;
        private static readonly NewsManager Instance = new NewsManager();

        public static NewsManager GetInstance()
        {
            return Instance;
        }

        public NewsManager()
        {
            _news = NewsProxy.GetInstance;
            _newsCache = ConfigCache.EnableCache == 1 ? NewsCacheProxy.GetInstance : null;
        }
        public List<NewsItem> GetList()
        {
            if (ConfigCache.EnableCache == 1 && _newsCache != null)
            {
                return _newsCache.GetList();
            }
            return _news.GetList();
        }

        public List<NewsItem> GetListHots()
        {
            if (ConfigCache.EnableCache == 1 && _newsCache != null)
            {
                return _newsCache.GetListHots();
            }
            return _news.GetListHots();
        }
     
        public NewsItem GetNewsByNameAscii(string nameAcsii)
        {
            if (ConfigCache.EnableCache == 1 && _newsCache != null)
            {
                return _newsCache.GetNewsByNameAscii(nameAcsii);
            }
            return _news.GetNewsByNameAscii(nameAcsii);
        }

        //phuocnh
        //lay tin host theo danh muc
        public List<NewsItem> GetNewIsHostByCateAcsii(string cateAcsii)
        {
            var lst = GetListHots();
            if (lst != null)
            {
                return lst.Where(m => m.CateAscii == cateAcsii).ToList();
            }
            return  new List<NewsItem>();
        }



       
        public List<NewsItem> NewsPage(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem)
        {
            if (ConfigCache.EnableCache == 1 && _newsCache != null)
            {
                return _newsCache.NewsPage(currentPage, rowPerPage, type, listNewsItem);
            }
            return _news.NewsPage(currentPage, rowPerPage, type, listNewsItem);
        }
        //phuocnh 23052015
        //lay tin lien quan theo list tag
        public List<NewsItem> GetNewsRelated(IEnumerable<TagItem> lststr)
        {
            TagManager tagM = new TagManager();
            var tag = tagM.GetAllListSimple();
            tag = tag.Where(m => lststr.Any(o => o.Name.Contains(m.Name))).ToList();
            var query = tag.SelectMany(m => m.Newitems);
            return query.ToList();
        }

        //phuocnh 05/06/2015
        //tin host theo danh muc nameacssi
        public List<NewsItem> GetListIsReadMore(string nameacssi)
        {
            var lst = GetList();
            lst = lst.Where(m => m.CateAscii == nameacssi && m.IsReadMore == true).ToList();
            return lst;
        }
    }
}
