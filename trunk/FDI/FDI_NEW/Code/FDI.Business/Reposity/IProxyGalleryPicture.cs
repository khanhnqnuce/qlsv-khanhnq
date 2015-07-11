using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyGalleryPicture
    {
        List<Gallery_Picture> GetList();

		List<Gallery_Picture> GetByCateName(string cateName);

        VideoItem GetGalleryVideoByCateId(int ID);

        Gallery_Picture GetByID(int id);

        Gallery_Picture GetById(int id);

        int InsertGalleryPicture(Gallery_Picture galleryPicture);

        void Save();
    }
}
