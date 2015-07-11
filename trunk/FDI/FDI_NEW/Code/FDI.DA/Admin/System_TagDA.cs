using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class System_TagDA : BaseDA
    {
        #region Constructer
        public System_TagDA()
        {
        }

        public System_TagDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public System_TagDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<TagItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.System_Tag
                        orderby c.Name
                        select new TagItem
                                   {
                            ID = c.ID,
                            Name = c.Name
                        };
            return query.ToList();
        }

        //lay list new item
        public List<NewsItem> GetListNews(string name)
        {
            var query = from c in FDIDB.News_News
                where c.System_Tag.Any(m => m.NameAscii.ToUpper().Contains(name.ToUpper()))
                select new NewsItem()
                {
                    ID = c.ID,
                    Title = c.Title,
                    TitleAscii = c.TitleAscii,
                    Details = c.Details,
                    Description = c.Description,
                    SeoDescription = c.SEODescription,
                    SalePercent = c.SalePercent,
                    SeoKeyword = c.SEOKeyword,
                    SeoTitle = c.SEOTitle,

                };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<TagItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            //edit by BienLV 12-04-2014 add c.IsDeleted = false
            //edit by hungdc 24/04/2014 add c.IsShow.Value
            var query = from c in FDIDB.System_Tag
                        orderby c.Name
                        where c.Name.StartsWith(keyword) && c.IsDelete == false && c.IsShow.Value
                        select new TagItem
                                   {
                            ID = c.ID,
                            Name = c.Name
                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<TagItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);

            var productCategoryName = httpRequest["ProductCategoryName"];
            if (!string.IsNullOrEmpty(productCategoryName))
            {
                int productCateID =
                    FDIDB.Shop_Category.Where(c => c.Name == productCategoryName).Select(c => c.ID).FirstOrDefault();
                var query = from o in FDIDB.Shop_Category_Tag_Mapping
                            where o.CategoryID == productCateID && o.System_Tag.IsHome.HasValue && o.System_Tag.IsHome.Value
                            select new TagItem
                                       {
                                ID = o.ID,
                                Name = o.System_Tag.Name,
                                IsDeleted = o.System_Tag.IsDelete != null && o.System_Tag.IsDelete.Value,
                                IsShow = o.System_Tag.IsShow != null && o.System_Tag.IsShow.Value,
                                NameAscii = o.System_Tag.NameAscii,
                                TotalAlbum = o.System_Tag.Gallery_Album.Count(),
                                //TotalGuide = o.System_Tag.Guide_Guide.Count(),
                                TotalNews = o.System_Tag.News_News.Count(),
                                TotalProduct = o.System_Tag.Shop_Product.Count(),
                                //TotalQuestion = o.System_Tag.FAQ_Question.Count(),
                                Weight = o.System_Tag.Weight,
                                IsHome = o.System_Tag.IsHome,
                                Link=o.System_Tag.Link,
                            };


                query = query.SelectByRequest(Request, ref TotalRecord);
                return query.ToList();
            }
            else
            {
                var query = from o in FDIDB.System_Tag
                            where o.IsDelete == false
                            orderby o.ID descending
                            select new TagItem
                                       {
                                ID = o.ID,
                                Name = o.Name,
                                IsDeleted = o.IsDelete != null && o.IsDelete.Value,
                                IsShow = o.IsShow != null && o.IsShow.Value,
                                TotalAlbum = o.Gallery_Album.Count(),
                                //TotalGuide = o.Guide_Guide.Count(),
                                TotalNews = o.News_News.Count(),
                                TotalProduct = o.Shop_Product.Count(),
                                //TotalQuestion = o.FAQ_Question.Count(),
                                Weight = o.Weight,
                                IsHome = o.IsHome
                            };
                query = query.SelectByRequest(Request, ref TotalRecord);
                return query.ToList();
            }
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<TagItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_Tag
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new TagItem
                                   {
                            ID = o.ID,
                            Name = o.Name,
                            IsDeleted = o.IsDelete != null && o.IsDelete.Value,
                            IsShow = o.IsShow != null && o.IsShow.Value,
                            NameAscii = o.NameAscii,
                            TotalAlbum = o.Gallery_Album.Count(),
                            //TotalGuide = o.Guide_Guide.Count(),
                            TotalNews = o.News_News.Count(),
                            TotalProduct = o.Shop_Product.Count(),
                            //TotalQuestion = o.FAQ_Question.Count(),

                        };
            TotalRecord = query.Count();
            return query.ToList();
        }

        public List<int> GetValuesArrayForSystemTag(string ltsSourceValues)
        {
            var ltsValues = new List<int>();
            if (!string.IsNullOrEmpty(ltsSourceValues))
            {
                if (ltsSourceValues.Contains(","))
                {
                    var tempStr = ltsSourceValues.Split(',');
                    ltsValues.AddRange(from t in tempStr
                                       select t.Trim()
                                       into nameCate select (from e in FDIDB.Shop_Category_Tag_Mapping
                                                             join c in FDIDB.Shop_Category on e.CategoryID equals c.ID
                                                             where c.Name == nameCate
                                                             select new
                                                                        {
                                                                            ID = c.ID,
                                                                        })
                                       into query where query.Any() select Convert.ToInt32(query.Select(c => c.ID).FirstOrDefault()));
                }
                else
                {
                    var query = from e in FDIDB.Shop_Category_Tag_Mapping
                                join c in FDIDB.Shop_Category
                                    on e.CategoryID equals c.ID
                                where c.Name == ltsSourceValues
                                select new
                                {
                                    ID = c.ID,
                                };
                    if (query.Any())
                    {
                        ltsValues.Add(Convert.ToInt32(query.Select(c => c.ID).FirstOrDefault()));
                    }
                }
            }
            return ltsValues;
        }


        public List<string> GetbyTag()
        {
            var dictionary = new Dictionary<string, int>();
            //var listProduct = (from c in  DB.Shop_Product
            //                  where c.IsDelete == false
            //                  select c.Name).ToList();

            //foreach (var shopProduct in listProduct)
            //{

            var sInput = "Việc bổ nhiệm này được xem là lựa chọn tối ưu nhằm khai thác hiệu quả năng lực quản trị và tổ chức triển khai của anh Bùi Quang Ngọc, để FPT chuẩn bị bệ phóng vững chãi cho những hướng chiến lược mới.";
            sInput = sInput.Replace(",", ""); //Just cleaning up a bit
            sInput = sInput.Replace(".", ""); //Just cleaning up a bit
            var arr = sInput.Split(' '); //Create an array of words

            //for (int i = 0; i < arr.Count()-1; i++)
            //{
            //    query.Add(arr[i] + " " + arr[i+1]);
            //}
            foreach (var word in arr.Where(word => word.Length >= 3))
            {
                if (dictionary.ContainsKey(word)) //if it's in the dictionary
                    dictionary[word] = dictionary[word] + 1; //Increment the count
                else
                    dictionary[word] = 1; //put it in the dictionary with a count 1
            }
            //}
            return dictionary.Where(m => m.Value > 50).Select(pair => string.Format("Key: {0}, Pair: {1}<br />", pair.Key, pair.Value)).ToList();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="tagID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Tag GetById(int tagID)
        {
            var query = from c in FDIDB.System_Tag where c.ID == tagID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Tag> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Tag where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public List<Shop_Category> GetListCategoryByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Category where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemTag">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_Tag systemTag)
        {
            var query = from c in FDIDB.System_Tag where ((c.Name == systemTag.Name) && (c.ID != systemTag.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="tagName">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Tag GetByName(string tagName)
        {
            var query = from c in FDIDB.System_Tag where ((c.Name == tagName)) select c;
            return query.FirstOrDefault();
        }


        public System_Tag AddOrGet(string tagName)
        {
            var systemTag = GetByName(tagName);
            if (systemTag == null)
            {
                var newTag = new System_Tag
                                 {
                    Name = tagName
                };
                FDIDB.System_Tag.Add(newTag);
                FDIDB.SaveChanges();
                return newTag;
            }
            return systemTag;
        }

        public void AddTagToCategoies(int tagId, int categotyId)
        {
            FDIDB.Shop_Category_Tag_Mapping.Add(new Shop_Category_Tag_Mapping
                                                        {
                TagID = tagId,
                CategoryID = categotyId
            });
        }

        public void DeleteTagToCategoies(int tagId)
        {
            var list = FDIDB.Shop_Category_Tag_Mapping.Where(t => t.TagID == tagId).ToList();
            foreach (var item in list)
            {
                FDIDB.Shop_Category_Tag_Mapping.Remove(item);
                FDIDB.SaveChanges();
            }

        }

        /// <summary>
        /// add by BienLV 12-04-2014
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="productId"></param>
        public void AddTagToProduct(int tagId, int productId)
        {
            var product = FDIDB.Shop_Product.Find(productId);
            var tag = FDIDB.System_Tag.Find(tagId);
            if (product == null || tag == null) return;
            product.System_Tag.Add(tag);
            FDIDB.SaveChanges();

        }

        /// <summary>
        /// add by BienLV 12-04-2014
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="productId"></param>
        public void RemoveTagFrompProduct(int tagId, int productId)
        {
            var product = FDIDB.Shop_Product.Find(productId);
            var tag = FDIDB.System_Tag.Find(tagId);
            if (product == null || tag == null) return;
            product.System_Tag.Remove(tag);
            FDIDB.SaveChanges();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemTag">bản ghi cần thêm</param>
        public void Add(System_Tag systemTag)
        {
            FDIDB.System_Tag.Add(systemTag);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemTag">Xóa bản ghi</param>
        public void Delete(System_Tag systemTag)
        {
            FDIDB.System_Tag.Remove(systemTag);
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
