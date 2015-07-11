using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;

namespace FDI.Business.Implementation.Proxy
{
    public class GalleryPictureProxy : GalleryPictureFacade, IProxyGalleryPicture
    {
        #region fields
        private static readonly Lazy<GalleryPictureProxy> Lazy = new Lazy<GalleryPictureProxy>(() => new GalleryPictureProxy());
        #endregion

        #region properties
        public static GalleryPictureProxy GetInstance { get { return Lazy.Value; } }

        #endregion

        #region constructor
        private GalleryPictureProxy()
        {

        }

        #endregion        

        public Gallery_Picture GetByID(int id)
        {
            return InstanceGalleryPicture.GalleryPictureDal.GetByIdAbstract(id);
        }

        public VideoItem GetGalleryVideoByCateId(int ID)
        {
            return InstanceGalleryPicture.GalleryPictureDal.GetGalleryVideoByCateIdAbstract(ID);
        }

        public List<Gallery_Picture> GetList()
        {
            return InstanceGalleryPicture.GalleryPictureDal.GetListAbstract();
        }

		public List<Gallery_Picture> GetByCateName(string cateName)
		{
			return InstanceGalleryPicture.GalleryPictureDal.GetByCateNameAbstract(cateName);
		}

        public int InsertGalleryPicture(Gallery_Picture galleryPicture)
        {
            return InstanceGalleryPicture.GalleryPictureDal.InsertGalleryPictureAbstract(galleryPicture);
        }

        public Gallery_Picture GetById(int id)
        {
            return InstanceGalleryPicture.GalleryPictureDal.GetByIdAbstract(id);
        }
        
        public void Save()
        {
            InstanceGalleryPicture.GalleryPictureDal.Save();
        }
    }
}
