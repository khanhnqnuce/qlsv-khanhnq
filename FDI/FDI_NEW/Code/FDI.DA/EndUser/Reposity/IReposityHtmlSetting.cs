using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityHtmlSetting
    {
        List<HtmlSettingItem> GetList();
        //List<HtmlSettingItem> GetListHots();
        //List<HtmlSettingItem> GetListById(int id);
        List<HtmlSettingItem> GetHtmlSettingByKey(string key);
        //List<HtmlSettingItem> HtmlSettingPage(int currentPage, int rowPerPage, string type, List<HtmlSettingItem> listHtmlSettingItem);
    }
}
