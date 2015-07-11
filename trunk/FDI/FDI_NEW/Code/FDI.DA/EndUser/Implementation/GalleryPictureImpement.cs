using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.DA.EndUser.Implementation
{
    public class GalleryPictureImpement : InitDB, IReposityGalleryPicture
    {
        #region get data

        public List<Gallery_Picture> GetList()
        {
            return new List<Gallery_Picture>();
        }


        public VideoItem GetGalleryVideoByCateId(int ID)
        {
            var query = from c in Instance.Gallery_Video
                where c.CategoryID == ID
                select new VideoItem
                       {
                            Name = c.Name,
                            Url = c.Url
                       };
                                                                                        
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Gallery_Picture GetById(int ID)
        {
            string a = "";
            var query = from c in Instance.Gallery_Picture where c.ID == ID select c;
            return query.FirstOrDefault();
        }

        public Gallery_Picture GetByID(int id)
        {
            throw new NotImplementedException();
        }

	    public List<Gallery_Picture> GetByCateName(string cateName)
	    {
		    if (!string.IsNullOrEmpty(cateName))
		    {
				Gallery_Category objCate = Instance.Gallery_Category.FirstOrDefault(m => m.NameAscii.ToLower().Equals(cateName.ToLower()));
				var query = from c in Instance.Gallery_Picture where c.CategoryID == objCate.ID && c.IsDeleted == false && c.IsShow == true orderby c.DateCreated descending select c;
			    return query.ToList();
		    }
			return new List<Gallery_Picture>();
	    }  

        #endregion


        #region Insert, Update, Delete

        public int InsertGalleryPicture(Gallery_Picture galleryPicture)
        {
            Instance.Gallery_Picture.Add(galleryPicture);
            Instance.SaveChanges();
            return galleryPicture.ID;
        }

        public void Save()
        {
            Instance.SaveChanges();
        }

        #endregion

    }
}
