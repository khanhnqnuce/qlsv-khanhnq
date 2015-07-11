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
    public partial class Shop_ProductVariantRecurringDA : BaseDA
    {
        #region Constructer
        public Shop_ProductVariantRecurringDA()
        {

        }

        public Shop_ProductVariantRecurringDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Shop_ProductVariantRecurringDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="delete"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ShopProductVaritantRecurringItem> GetListSimpleByRequest(HttpRequestBase httpRequest, bool delete = false)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Shop_Product_Variant_Recurring
                        select new ShopProductVaritantRecurringItem
                                   {
                            ID = c.ID,
                            ProductVariantID = c.ProductVariantID,
                            MustRepaidPercent = c.MustRepaidPercent,
                            RecurringLength = c.RecurringLength,
                            InterestRate = c.InterestRate,
                            MetaKeywordRecurring = c.MetaKeywordRecurring,
                            MetaDescriptionRecurring = c.MetaDescriptionRecurring,
                            RecurringTitle = c.RecurringTitle,
                            IsDelete = c.IsDelete
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Shop_Product_Variant_Recurring> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Shop_Product_Variant_Recurring where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về bản ghi qua id của sản phẩm
        /// </summary>
        /// <param name="productId">ID sản phẩm</param>
        /// <param name="delete"> </param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Variant_Recurring GetByProductId(int productId, bool delete = false)
        {
            var query = from c in FDIDB.Shop_Product_Variant_Recurring where c.ProductVariantID == productId && c.IsDelete == delete select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="productRecurringId">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Variant_Recurring GetById(int productRecurringId)
        {
            var query = from c in FDIDB.Shop_Product_Variant_Recurring where c.ID == productRecurringId select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="productRecurringId">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public ShopProductVaritantRecurringItem GetItemById(int productRecurringId)
        {
            var query = from c in FDIDB.Shop_Product_Variant_Recurring
                        where c.ID == productRecurringId
                        select new ShopProductVaritantRecurringItem
                                   {
                            ID = c.ID,
                            ProductVariantID = c.ProductVariantID,
                            MustRepaidPercent = c.MustRepaidPercent,
                            RecurringLength = c.RecurringLength,
                            InterestRate = c.InterestRate,
                            MetaKeywordRecurring = c.MetaKeywordRecurring,
                            MetaDescriptionRecurring = c.MetaDescriptionRecurring,
                            RecurringTitle = c.RecurringTitle,
                            IsDelete = c.IsDelete
                        };
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="productRecurring">bản ghi cần thêm</param>
        public void Add(Shop_Product_Variant_Recurring productRecurring)
        {
            FDIDB.Shop_Product_Variant_Recurring.Add(productRecurring);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="productRecurring">Xóa bản ghi</param>
        public void Delete(Shop_Product_Variant_Recurring productRecurring)
        {
            FDIDB.Shop_Product_Variant_Recurring.Remove(productRecurring);
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
    }
}
