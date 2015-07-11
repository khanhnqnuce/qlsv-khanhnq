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
    
    public class GalleryPictureFacade 
    {
        public GalleryPictureAbstraction GalleryPictureDal;
        public CacheController Cache;

        public GalleryPictureFacade()
        {
            this.GalleryPictureDal = new GalleryPictureAbstraction(new GalleryPictureImpement());
            this.Cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static GalleryPictureFacade() { }

        static readonly GalleryPictureFacade UniqueInstance = new GalleryPictureFacade();

        public static GalleryPictureFacade InstanceGalleryPicture
        {
            get { return UniqueInstance; }
        }
    }
}
