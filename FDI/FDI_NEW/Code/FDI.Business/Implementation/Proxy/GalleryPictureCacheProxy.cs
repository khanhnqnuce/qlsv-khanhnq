using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Proxy
{
    public class GalleryPictureCacheProxy : GalleryPictureFacade, IProxyGalleryPicture
    {
        #region fields
        private static readonly Lazy<GalleryPictureCacheProxy> Lazy = new Lazy<GalleryPictureCacheProxy>(() => new GalleryPictureCacheProxy());
        readonly GalleryPictureProxy _galleryPictureProxy = GalleryPictureProxy.GetInstance;
        #endregion

        #region properties
        public static GalleryPictureCacheProxy GetInstance { get { return Lazy.Value; } }

        #endregion

        #region constructor
        private GalleryPictureCacheProxy()
        {
        }

        #endregion

        public int InsertGalleryPicture(Gallery_Picture galleryPicture)
        {
            return 0;
        }

        public void Save()
        {

        }

        public List<Gallery_Picture> GetList()
        {
            return null;
        }

		public List<Gallery_Picture> GetByCateName(string cateName)
		{
			var param = string.Format("GalleryPictureCacheProxyGetByCateName{0}", cateName);
			if (Cache.KeyExistsCache(param))
			{
				var lst = (List<Gallery_Picture>)Cache.GetCache(param);
				if (lst == null)
				{
					// delete old cache
					Cache.DeleteCache(param);
					// get new
					var retval = _galleryPictureProxy.GetByCateName(cateName);
					// insert new cache
					Cache.Set(param, retval, ConfigCache.TimeExpire);
					return retval;
				}
				return lst;
			}
			else
			{
				var lst = _galleryPictureProxy.GetByCateName(cateName);
				// insert new cache
				Cache.Set(param, lst, ConfigCache.TimeExpire);
				return lst;
			}
		}

        public VideoItem GetGalleryVideoByCateId(int ID)
        {
            var param = string.Format("GalleryPictureCacheProxyGetById{0}", ID);
            if (Cache.KeyExistsCache(param))
            {
                var lst = (VideoItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _galleryPictureProxy.GetGalleryVideoByCateId(ID);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _galleryPictureProxy.GetGalleryVideoByCateId(ID);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public Gallery_Picture GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Gallery_Picture GetById(int id)
        {
            var param = string.Format("GalleryPictureCacheProxyGetById{0}", id);
            if (Cache.KeyExistsCache(param))
            {
                var lst = (Gallery_Picture)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _galleryPictureProxy.GetById(id);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _galleryPictureProxy.GetById(id);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
    }
}
