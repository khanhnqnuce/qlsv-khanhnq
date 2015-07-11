using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Abstraction;
using FDI.DA.EndUser.Implementation;
using FDI.Memcached;
using FDI.Utils;

namespace FDI.Business.Facade
{
    public class HtmlSettingFacade
    {
        public HtmlSettingAbstraction HtmlSettingDal;
        public CacheController Cache;

        public HtmlSettingFacade()
        {
            HtmlSettingDal = new HtmlSettingAbstraction(new HtmlSettingImplement());
            Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static HtmlSettingFacade() { }

        static readonly HtmlSettingFacade UniqueInstance = new HtmlSettingFacade();

        public static HtmlSettingFacade InstanceHtmlSetting
        {
            get { return UniqueInstance; }
        }
    }
}
