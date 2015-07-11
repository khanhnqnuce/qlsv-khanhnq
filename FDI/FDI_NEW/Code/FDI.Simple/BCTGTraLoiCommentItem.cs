using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class BCTGTraLoiCommentItem : BaseSimple
    {
        public int ToTalDatKPI { get; set; }
        public int ToTalKoDatKPI { get; set; }
        public int TotalComment { get; set; }
        public int ThoiGianTraLoiComment { get; set; }
        public DateTime NgayTraloi { get; set; }
        public DateTime NgayTao { get; set; }
        public List<ShopCommentItem> LtsShopCommentItem { get; set; }
    }
    public class ModelBCTGTraLoiCommentItem : BaseModelSimple
    {
        public IEnumerable<BCTGTraLoiCommentItem> ListItem { get; set; }
    }
}
