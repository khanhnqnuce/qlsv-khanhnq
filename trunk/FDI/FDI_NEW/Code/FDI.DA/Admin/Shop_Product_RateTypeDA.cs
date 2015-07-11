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
    public partial class Shop_Product_RateTypeDA : BaseDA
    {
        #region Constructer

        public Shop_Product_RateTypeDA()
        {
        }

        public Shop_Product_RateTypeDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_Product_RateTypeDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }

        #endregion

        #region tree

        public List<ProductRateTypeItem> GetAllSelectList(List<ProductRateTypeItem> ltsSource, int categoryIDRemove,
                                                          bool checkShow)
        {
            if (checkShow)
                ltsSource = ltsSource.Where(o => o.IsDelete == false).ToList();
            var ltsConvert = new List<ProductRateTypeItem>
                                                       {
                                                           new ProductRateTypeItem()
                                                               {
                                                                   ID = 1,
                                                                   RateName = "Thư mục gốc"
                                                               }
                                                       };

            BuildTreeListItem(ltsSource, 1, string.Empty, categoryIDRemove, ref ltsConvert);
            return ltsConvert;
        }

        private void BuildTreeListItem(List<ProductRateTypeItem> LtsItems, int RootID, string space,
                                       int CategoryIDRemove, ref List<ProductRateTypeItem> LtsConvert)
        {
            space += "---";

            foreach (var t in LtsItems)
            {
                t.RateName = string.Format("|{0} {1}", space, t.RateName);
                LtsConvert.Add(t);
            }
        }

        #endregion

        public List<ProductRateTypeItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_RateType
                        where c.IsDelete.Value == false
                        select new ProductRateTypeItem
                                   {
                            ID = c.ID,
                            RateName = c.RateName,
                            IsDelete = c.IsDelete.Value,
                        };
            return query.ToList();
        }

        public List<ProductRateTypeItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_RateType
                        where c.IsDelete.Value == isShow
                        select new ProductRateTypeItem()
                        {
                            ID = c.ID,
                            RateName = c.RateName,
                            IsDelete = c.IsDelete.Value,
                        };
            return query.ToList();
        }

        public List<ProductRateTypeItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_RateType
                        where c.IsDelete.Value == false
                        select new ProductRateTypeItem()
                        {
                            ID = c.ID,
                            RateName = c.RateName,
                            IsDelete = c.IsDelete.Value,
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.OrderByDescending(c => c.ID).ToList();
        }

        public List<Shop_Product_RateType> getListSimpleByArrID(List<int> LtsArrID)
        {
            var query = from c in FDIDB.Shop_Product_RateType
                        where LtsArrID.Contains(c.ID)
                        select c;
            TotalRecord = query.Count();
            return query.ToList();
        }

        public Shop_Product GetProductByProductID(int productID)
        {
            return FDIDB.Shop_Product.FirstOrDefault(c => c.ID == productID);
        }

        public List<ProductRatingItem> GetProductRatingByProductID(int productID)
        {
            var query = from c in FDIDB.Shop_Product_Rating
                        where c.ProductID == productID
                        select new ProductRatingItem()
                        {
                            ID = c.ID,
                            ProductID = c.ProductID.Value,
                            ProductName = c.Shop_Product.Name,
                            //RateTypeID = c.Shop_Product_RateType.ID,
                            //RateName = c.Shop_Product_RateType.RateName,
                            //RateNumber = c.RateNumber.HasValue ? c.RateNumber.Value : (double) 0,
                        };
            return query.ToList();
        }

        public List<ProductRatingItem> GetProductRating()
        {
            var query = from c in FDIDB.Shop_Product_Rating
                        select new ProductRatingItem()
                        {
                            ID = c.ID,
                            //RateTypeID = c.Shop_Product_RateType.ID,
                            //RateName = c.Shop_Product_RateType.RateName,
                            //RateNumber = c.RateNumber.HasValue ? c.RateNumber.Value : (double)0,
                        };
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        public Shop_Product_RateType getById(int id)
        {
            var query = from c in FDIDB.Shop_Product_RateType where c.ID == id select c;
            return query.FirstOrDefault();
        }

        public void Add(Shop_Product_RateType Shop_Product_RateType)
        {
            FDIDB.Shop_Product_RateType.Add(Shop_Product_RateType);
        }

        public void Delete(Shop_Product_RateType Shop_Product_RateType)
        {
            FDIDB.Shop_Product_RateType.Remove(Shop_Product_RateType);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
