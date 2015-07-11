using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class HtmlSettingAbstraction
    {
        readonly IReposityHtmlSetting _bridge;

        internal HtmlSettingAbstraction(IReposityHtmlSetting implementation)
        {
            _bridge = implementation;
        }

        public List<HtmlSettingItem> GetListAbstract()
        {
            return _bridge.GetList();
        }

        //public List<HtmlSettingItem> GetListHotsAbstract()
        //{
        //    return _bridge.GetListHots();
        //}

        //public List<HtmlSettingItem> GetListByIdAbstract(int id)
        //{
        //    return _bridge.GetListById(id);
        //}

        public List<HtmlSettingItem> GetHtmlSettingByKeyAbstract(string key)
        {
            return _bridge.GetHtmlSettingByKey(key);
        }
        //public List<HtmlSettingItem> HtmlSettingPageAsciiAbstract(int currentPage, int rowPerPage, string type, List<HtmlSettingItem> listHtmlSettingItem)
        //{
        //    return _bridge.HtmlSettingPage(currentPage, rowPerPage, type, listHtmlSettingItem);
        //}
    }
}
