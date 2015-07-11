using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.DA.EndUser.Implementation
{
    public class TagImplementation : InitDB, IReposityTag
    {
        public List<TagItem> GetAllListSimple()
        {
            var query = from c in Instance.System_Tag
                where c.IsDelete == false && c.IsHome==true
                        orderby c.ID
                        
                        select new TagItem
                        {
                           ID = c.ID,
                           Name = c.Name,
                           NameAscii = c.NameAscii,
                           
                           Newitems = c.News_News.Where(m=>m.IsDeleted==false).Select(n=>new NewsItem()
                           {
                               ID = c.ID,
                               Title = n.Title,
                               TitleAscii = n.TitleAscii ,
                               Description = n.Description,
                               CateAscii = n.News_Category.FirstOrDefault().NameAscii,
                               IsHot = n.IsHot,
                               PictureUrl = n.Gallery_Picture.Folder + n.Gallery_Picture.Url,
                               DateCreated = n.DateCreated,
                               UserNameCreate = n.aspnet_Users.UserName,

                           })
                        };
            return query.ToList();
        }
        public List<TagItem> GetByNamAssi(string nameassi)
        {
            var query = from c in Instance.System_Tag
                        where c.IsDelete == false && c.News_News.Any(m => m.TitleAscii == nameassi)
                        orderby c.ID descending 

                        select new TagItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            Newitems = c.News_News.Where(m => m.IsDeleted == false).Select(n => new NewsItem()
                            {
                                ID = c.ID,
                                Title = n.Title,
                                TitleAscii = n.TitleAscii,
                                Description = n.Description,
                                CateAscii = n.News_Category.FirstOrDefault().NameAscii,
                                IsHot = n.IsHot,
                                PictureUrl = n.Gallery_Picture.Folder + n.Gallery_Picture.Url,
                                DateCreated = n.DateCreated,
                                UserNameCreate = n.aspnet_Users.UserName,

                            })
                        };
            return query.ToList();
        }
        public TagItem GetByNamAcssi(string nameacssi)
        {
            var query = from c in Instance.System_Tag
                        where c.IsDelete == false && c.NameAscii==nameacssi
                        orderby c.ID descending

                        select new TagItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            SEOKeyword = c.SEOKeyword,
                            SEODescription = c.SEODescription,
                            SEOTitle = c.SEOTitle,
                            Description = c.Description,
                            Newitems = c.News_News.Where(m => m.IsDeleted == false).Select(n => new NewsItem()
                            {
                                ID = c.ID,
                                Title = n.Title,
                                TitleAscii = n.TitleAscii,
                                Description = n.Description,
                                CateAscii = n.News_Category.FirstOrDefault().NameAscii,
                                IsHot = n.IsHot,
                                PictureUrl = n.Gallery_Picture.Folder + n.Gallery_Picture.Url,
                                DateCreated = n.DateCreated,
                                UserNameCreate = n.aspnet_Users.UserName,

                            })
                        };
            return query.FirstOrDefault();
        }
        public List<TagItem> GetAllHomeListSimple()
        {
            var query = from c in Instance.System_Tag
                        where c.IsDelete == false && c.IsHome == true && c.IsHome==true
                        orderby c.ID

                        select new TagItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            Link = c.Link
                        };
            return query.ToList();
        }

        public List<TagItem> GetAllListSimpleByUrl(string url)
        {
            var query = from c in Instance.System_Tag
                        where c.IsDelete==false && c.News_News.Any(m => m.TitleAscii==url)
                        select new TagItem
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii
                        };
            return query.ToList();
        }

        //phuocnh
        public List<TagItem> GetNew(List<string> lst)
        {
            var query=from c in Instance.System_Tag
                      where lst.Contains(c.Name)
                      select new TagItem
                      {
                          ID = c.ID,
                          Name = c.Name,
                          NameAscii = c.NameAscii
                      };
            return query.ToList();
        }
        
    }
}
