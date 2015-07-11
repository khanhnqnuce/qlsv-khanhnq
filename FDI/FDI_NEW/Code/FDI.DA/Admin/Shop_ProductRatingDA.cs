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
    public partial class Shop_ProductRatingDA : BaseDA
    {
        #region Constructer
        public Shop_ProductRatingDA()
        {

        }

        public Shop_ProductRatingDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_ProductRatingDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        public List<ProductRatingItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from e in FDIDB.Shop_Product_Rating
                        where e.IdDelete.Value == false
                        select new ProductRatingItem
                                   {
                            ID = e.ID,
                            ProductID = e.ProductID.HasValue ? e.ProductID.Value : 0,
                            ProductName = e.Shop_Product.Name,
                            CustomerID = e.CustomerID.HasValue ? e.CustomerID.Value : 0,
                            CustomerName = e.Customer.UserName,
                            IdDelete = e.IdDelete.HasValue && e.IdDelete.Value,
                            DateCreated = e.DateCreated.Value,
                            RateNote = e.RateNote,
                            RateAvg = e.RateAvg.HasValue ? e.RateAvg.Value : 0,
                        };

            #region search by ProductID
            var productID = Convert.ToInt32(httpRequest["ProductID"]);
            if (productID != 0)
            {
                query = query.Where(c => c.ProductID == productID);
            }
            #endregion

            #region search by CustomerID
            var customerID = Convert.ToInt32(httpRequest["CustomerID"]);
            if (customerID != 0)
            {
                query = query.Where(c => c.CustomerID == customerID);
            }
            #endregion

            query = query.SelectByRequest(Request, ref TotalRecord);

            return query.OrderByDescending(c => c.ID).ToList();
        }

        public List<Shop_Product_Rating> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Rating where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        public Shop_Product_Rating GetByproductID(int productID)
        {
            var query = from e in FDIDB.Shop_Product_Rating
                        where e.ProductID == productID
                        select e;

            return query.FirstOrDefault();
        }

        public Shop_Product_Rating GetById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Rating where c.ID == id select c;
            return query.FirstOrDefault();
        }

        public Shop_Product_RateNumber GetProductRateNumbeById(int id)
        {
            var query = from c in FDIDB.Shop_Product_RateNumber
                        where c.ID == id
                        select c;
            return query.FirstOrDefault();
        }

        public List<Shop_Product_Rating> GetListByProductId(int productId)
        {
            var query = from c in FDIDB.Shop_Product_Rating where c.ProductID == productId select c;
            return query.ToList();
        }

        public List<ProductRatingItem> GetListAllProductId(int productId, HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            //var query = from e in  DB.Shop_Product_Rating
            //            where e.IsDeleteRateType.Value == false && e.ProductID == productId
            //            select new ProductRatingItem()
            //            {
            //                ID = e.ID,
            //                ProductID = e.ProductID.Value,
            //                ProductName = e.Shop_Product.Name,
            //                RateTypeID = e.RateTypeID.Value,
            //                RateName = e.Shop_Product_RateType.RateName,
            //                RateNumber = e.RateNumber.Value,
            //                RateNote = e.RateNote,
            //                CustomerName = e.Customer.UserName,
            //                CustomerID = e.CustomerID.HasValue ? e.CustomerID.Value : 0,
            //            };

            //query = query.SelectByRequest(Request, ref TotalRecord);

            //var queryGroup = from c in query.ToList()
            //                 group c by new { c.ID, c.CustomerID, c.CustomerName, c.RateNote }
            //                     into g
            //                     select new ProductRatingItem()
            //                     {
            //                         ID = g.Key.ID,
            //                         CustomerID = g.Key.CustomerID,
            //                         RateNote = g.Key.RateNote,
            //                         CustomerName = g.Key.CustomerName,
            //                         ProductID = g.Select(e => e.ProductID).FirstOrDefault(),
            //                     };

            //return queryGroup.ToList();

            return null;
        }

        public Shop_Product_Rating GetByproductId(int id)
        {
            var query = from c in FDIDB.Shop_Product_Rating where c.ProductID == id select c;
            return query.FirstOrDefault();
        }

        public List<ProductRatingItem> getListByProductId(int id)
        {
            var query = from c in FDIDB.Shop_Product_Rating
                        where c.ProductID == id
                        select new ProductRatingItem
                                   {
                            ID = c.ID,
                            ProductID = c.ProductID.Value,
                            ProductName = c.Shop_Product.Name,

                            RateNote = c.RateNote,
                        };

            return query.ToList();
        }

        public void Add(Shop_Product_Rating shopProductRating)
        {
            FDIDB.Shop_Product_Rating.Add(shopProductRating);
        }

        public void Delete(Shop_Product_Rating shopProductRating)
        {
            FDIDB.Shop_Product_Rating.Remove(shopProductRating);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
    }
}
