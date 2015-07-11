using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Implementation.Proxy;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Manager
{
    
    public class GalleryPictureManager
    {
        readonly IProxyGalleryPicture _galleryPicture;
        readonly IProxyGalleryPicture _galleryPictureCache;
        private static readonly GalleryPictureManager Instance = new GalleryPictureManager();

        public static GalleryPictureManager GetInstance()
        {
            return Instance;
        }

        public GalleryPictureManager()
        {
            _galleryPicture = GalleryPictureProxy.GetInstance;
            _galleryPictureCache = ConfigCache.EnableCache == 1 ? GalleryPictureCacheProxy.GetInstance : null;
        }

        public List<Gallery_Picture> GetList()
        {
            if (ConfigCache.EnableCache == 1 && _galleryPictureCache != null)
            {
                return _galleryPictureCache.GetList();
            }
            return _galleryPicture.GetList();
        }

		public List<Gallery_Picture> GetByCateName(string cateName)
		{
			if (ConfigCache.EnableCache == 1 && _galleryPictureCache != null)
			{
				return _galleryPictureCache.GetByCateName(cateName);
			}
			return _galleryPicture.GetByCateName(cateName);
		}

        public VideoItem GetGalleryVideoByCateId(int ID)
        {
            if (ConfigCache.EnableCache == 1 && _galleryPictureCache != null)
            {
                return _galleryPictureCache.GetGalleryVideoByCateId(ID);
            }
            return _galleryPicture.GetGalleryVideoByCateId(ID);
        }
       
        public Gallery_Picture GetByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _galleryPictureCache != null)
            {
                return _galleryPictureCache.GetByID(id);
            }
            return _galleryPicture.GetByID(id);
        }

        public int InsertGalleryPicture(Gallery_Picture galleryPictureDA)
        {
            return _galleryPicture.InsertGalleryPicture(galleryPictureDA);
        }

        public void Save()
        {
            _galleryPicture.Save();
        }

        public Gallery_Picture GetById(int id)
        {
            if (ConfigCache.EnableCache == 1 && _galleryPictureCache != null)
            {
                return _galleryPictureCache.GetById(id);
            }
            return _galleryPicture.GetById(id);
        }
    }
}
