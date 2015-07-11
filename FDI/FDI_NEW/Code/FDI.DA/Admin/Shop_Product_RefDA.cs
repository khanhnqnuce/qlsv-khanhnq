using System.Collections.Generic;
using System.Linq;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Shop_Product_RefDA : BaseDA
    {
        #region Constructer
        public Shop_Product_RefDA()
        {
        }

        public Shop_Product_RefDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Shop_Product_RefDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        public List<ProductRefItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_Ref
                        where c.IsDelete.Value == false
                        select new ProductRefItem
                                   {
                                       ID = c.ID,
                                       ProductID = c.ProductID,
                                       PruductRefID = c.PruductRefID,
                                       IsDeleted = c.IsDelete.Value,
                                       ProductName = c.Shop_Product.Name,
                                       PruductRefName = c.Shop_Product1.Name,
                                   };
            return query.ToList();
        }

        public List<ProductRefItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_Ref
                        where c.IsDelete.Value == isShow
                        select new ProductRefItem
                                   {
                                       ID = c.ID,
                                       ProductID = c.ProductID,
                                       PruductRefID = c.PruductRefID,
                                       IsDeleted = c.IsDelete.Value,
                                       ProductName = c.Shop_Product.Name,
                                       PruductRefName = c.Shop_Product1.Name,
                                   };
            return query.ToList();
        }

        #region Check Exits, Add, Update, Delete
        public Shop_Product_Ref GetById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Ref where c.ID == id select c;
            return query.FirstOrDefault();
        }

        public List<ProductRefItem> GetByProductId(int productId)
        {
            var query = from c in FDIDB.Shop_Product_Ref
                        where c.ProductID == productId && c.IsDelete == false
                        select new ProductRefItem
                                   {
                                       ID = c.ID,
                                       ProductID = c.ProductID,
                                       PruductRefID = c.PruductRefID,
                                       IsDeleted = c.IsDelete.Value,
                                       ProductName = c.Shop_Product.Name,
                                       PruductRefName = c.Shop_Product1.Name,
                                   };
            return query.ToList();
        }

        public Shop_Product_Ref GetByProductIDAndProductRefID(int productId, int productRefId)
        {
            var query = from c in FDIDB.Shop_Product_Ref where c.ProductID == productId && c.PruductRefID == productRefId && c.IsDelete.Value == false select c;
            return query.FirstOrDefault();
        }

        public void Add(Shop_Product_Ref shopProductRef)
        {
            FDIDB.Shop_Product_Ref.Add(shopProductRef);
        }

        public void Delete(Shop_Product_Ref shopProductRef)
        {
            FDIDB.Shop_Product_Ref.Remove(shopProductRef);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
