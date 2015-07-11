using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.DA.EndUser.Implementation
{
    public class AdvertisingImplement : InitDB, IReposityAdvertising
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AdvertisingItem> GetAdvertisingItemByID(int id)
        {
            var query = from c in Instance.Advertisings
                        where c.PositionID == id && c.IsDeleted == false
                        orderby c.Order
                        select new AdvertisingItem
                        {
                            
                            Link = c.Link,
                            Name = c.Name,
                            Width = c.Width,
                            Height = c.Height,
                            PictureUrl = c.Gallery_Picture.Folder+c.Gallery_Picture.Url,
                            //GalleryPicture = new PictureItem
                            //{
                            //    Url = c.Gallery_Picture.Url,
                            //    Folder = c.Gallery_Picture.Folder
                            //}
                        };
            return query.ToList();
        }
    }
}
