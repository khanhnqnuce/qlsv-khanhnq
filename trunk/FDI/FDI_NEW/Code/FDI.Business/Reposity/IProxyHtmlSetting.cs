using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyHtmlSetting
    {
        List<HtmlSettingItem> GetList();
        //List<HtmlSettingItem> GetListHots();

        List<HtmlSettingItem> GetHtmlSettingByKey(string key);
        //List<HtmlSettingItem> HtmlSettingPage(int currentPage, int rowPerPage, string type, List<HtmlSettingItem> listHtmlSettingItem);
    }
}
