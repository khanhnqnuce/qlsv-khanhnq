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
    public class HtmlSettingProxy: HtmlSettingFacade, IProxyHtmlSetting
    {
        #region fields
        private static readonly Lazy<HtmlSettingProxy> lazy = new Lazy<HtmlSettingProxy>(() => new HtmlSettingProxy());
        #endregion

        #region properties
        public static HtmlSettingProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private HtmlSettingProxy()
        {
        }

        #endregion

        public List<HtmlSettingItem> GetList()
        {
            return InstanceHtmlSetting.HtmlSettingDal.GetListAbstract();
        }

        //public List<HtmlSettingItem> GetListHots()
        //{
        //    return InstanceHtmlSetting.HtmlSettingDal.GetListHotsAbstract();
        //}

        public List<HtmlSettingItem> GetHtmlSettingByKey(string key)
        {
            return InstanceHtmlSetting.HtmlSettingDal.GetHtmlSettingByKeyAbstract(key);
        }
        //public List<HtmlSettingItem> HtmlSettingPage(int currentPage, int rowPerPage, string type, List<HtmlSettingItem> listHtmlSettingItem)
        //{
        //    return InstanceHtmlSetting.HtmlSettingDal.HtmlSettingPageAsciiAbstract(currentPage, rowPerPage, type, listHtmlSettingItem);
        //}
    }
}
