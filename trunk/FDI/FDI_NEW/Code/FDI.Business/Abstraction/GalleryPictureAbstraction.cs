using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class GalleryPictureAbstraction
    {
        readonly IReposityGalleryPicture _bridge;

        internal GalleryPictureAbstraction(IReposityGalleryPicture implementation)
        {
            this._bridge = implementation;
        }

        public List<Gallery_Picture> GetListAbstract()
        {
            return this._bridge.GetList();
        }

		public List<Gallery_Picture> GetByCateNameAbstract(string cateName)
		{
			return this._bridge.GetByCateName(cateName);
		}

        public VideoItem GetGalleryVideoByCateIdAbstract(int ID)
        {
            return this._bridge.GetGalleryVideoByCateId(ID);
        }
        
        public Gallery_Picture GetByIdAbstract(int id)
        {
            return this._bridge.GetById(id);
        }

        public int InsertGalleryPictureAbstract(Gallery_Picture galleryPicture)
        {
            return this._bridge.InsertGalleryPicture(galleryPicture);
        }       
   
        public void Save()
        {
            this._bridge.Save();
        }
    }
}
