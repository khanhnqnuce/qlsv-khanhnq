using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityGalleryPicture
    {
        List<Gallery_Picture> GetList();

        VideoItem GetGalleryVideoByCateId(int ID);

        Gallery_Picture GetByID(int id);

        Gallery_Picture GetById(int id);

		List<Gallery_Picture> GetByCateName(string cateName);

        int InsertGalleryPicture(Gallery_Picture galleryPicture);

        void Save();
    }
}
