using System.Collections.Generic;
using System.Linq;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Shop_Product_Compatible_AccessoriesDA : BaseDA
    {
        #region Constructer
        public Shop_Product_Compatible_AccessoriesDA()
        {
        }

        public Shop_Product_Compatible_AccessoriesDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Shop_Product_Compatible_AccessoriesDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        public List<ProductCompatibleAccessoriesItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.Shop_Product_Compatible_Accessories
                        where c.IsDeleted.Value == false
                        select new ProductCompatibleAccessoriesItem
                                   {
                                       ID = c.ID,
                                       ProductID = c.ProductID.Value,
                                       CompatibleAccessoriesID = c.CompatibleAccessoriesID.Value,
                                       IsDeleted = c.IsDeleted.Value,
                                       ProductName = c.Shop_Product.Name,
                                       CompatibleAccessoriesName = c.Shop_Product1.Name,
                                   };
            return query.ToList();
        }

        public List<ProductCompatibleAccessoriesItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.Shop_Product_Compatible_Accessories
                        where c.IsDeleted.Value == isShow
                        select new ProductCompatibleAccessoriesItem
                                   {
                                       ID = c.ID,
                                       ProductID = c.ProductID.Value,
                                       CompatibleAccessoriesID = c.CompatibleAccessoriesID.Value,
                                       IsDeleted = c.IsDeleted.Value,
                                       ProductName = c.Shop_Product.Name,
                                       CompatibleAccessoriesName = c.Shop_Product1.Name,
                                   };
            return query.ToList();
        }


        #region Check Exits, Add, Update, Delete
        public Shop_Product_Compatible_Accessories GetById(int id)
        {
            var query = from c in FDIDB.Shop_Product_Compatible_Accessories where c.ID == id select c;
            return query.FirstOrDefault();
        }

        public List<ProductCompatibleAccessoriesItem> GetByProductId(int productId)
        {
            var query = from c in FDIDB.Shop_Product_Compatible_Accessories
                        where c.ProductID == productId && c.IsDeleted == false
                        select new ProductCompatibleAccessoriesItem
                                   {
                                       ID = c.ID,
                                       ProductID = c.ProductID.Value,
                                       CompatibleAccessoriesID = c.CompatibleAccessoriesID.Value,
                                       IsDeleted = c.IsDeleted.Value,
                                       ProductName = c.Shop_Product.Name,
                                       CompatibleAccessoriesName = c.Shop_Product1.Name,
                                   };
            return query.ToList();
        }

        public Shop_Product_Compatible_Accessories GetByProductIDAndCompatibleAccessoriesID(int productId, int compatibleAccId)
        {
            var query = from c in FDIDB.Shop_Product_Compatible_Accessories where c.ProductID.Value == productId && c.CompatibleAccessoriesID.Value == compatibleAccId && c.IsDeleted.Value == false select c;
            return query.FirstOrDefault();
        }

        public void Add(Shop_Product_Compatible_Accessories shopProductCompatibleAccessories)
        {
            FDIDB.Shop_Product_Compatible_Accessories.Add(shopProductCompatibleAccessories);
        }

        public void Delete(Shop_Product_Compatible_Accessories shopProductCompatibleAccessories)
        {
            FDIDB.Shop_Product_Compatible_Accessories.Remove(shopProductCompatibleAccessories);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
