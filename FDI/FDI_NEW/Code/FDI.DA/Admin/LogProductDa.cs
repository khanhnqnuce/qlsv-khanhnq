using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.Admin
{
    public class LogProductDa : BaseDA
    {
        #region contructor

        public LogProductDa()
        {
        }

        public LogProductDa(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public LogProductDa(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion

        #region select
        public Shop_Product GetShopProductById(int id)
        {
            var products = from product in FDIDB.Shop_Product
                           where product.ID == id
                           select product;
            return products.FirstOrDefault();
        }

        public String[] GetArrayCategoryByIdProduct(int id)
        {
            var query = from sp in FDIDB.Shop_Product
                        where sp.ID == id
                        select sp;
            var product = query.FirstOrDefault();
            String[] category = null;

            if (product != null)
            {
                category = product.Shop_Category.Select(x => Convert.ToString(x.ID)).ToArray();
            }
            return category;
        }

        public Shop_Product_Variant GetVariantById(int id)
        {
            var query = from spv in FDIDB.Shop_Product_Variant
                        where spv.ID == id
                        select spv;
            return query.FirstOrDefault();
        }

        //public IList<LogProductItem> GetAllLogShopProduct(HttpRequestBase httpRequest)
        //{
        //    Request = new ParramRequest(httpRequest);
        //    var query = from lsp in FDIDB.Log_Shop_Product
        //                select new LogProductItem()
        //                {
        //                    ID = lsp.ID,
        //                    ProductId = lsp.ProductId ?? 0,
        //                    UserEdited = lsp.UserEdited,
        //                    PropertiesChanged = lsp.PropertiesChanged,
        //                    OldValue = lsp.OldValue,
        //                    NewValue = lsp.NewValue,
        //                    DateChanged = lsp.DateChanged,
        //                    TypeActionName = lsp.TypeActionName
        //                };
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(httpRequest.QueryString["fromCreateDate"]))
        //        {
        //            var formDate = Convert.ToDateTime(httpRequest.QueryString["fromCreateDate"]);
        //            query = query.Where(c => c.DateChanged > formDate);
        //        }

        //        if (!string.IsNullOrEmpty(httpRequest.QueryString["toCreateDate"]))
        //        {
        //            var toDate = Convert.ToDateTime(httpRequest.QueryString["toCreateDate"]).AddDays(1);
        //            query = query.Where(c => c.DateChanged < toDate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Instance.LogError(this.GetType(), ex);
        //    }
        //    query = query.SelectByRequest(Request, ref TotalRecord);
        //    return query.OrderBy(x => x.ID).ToList();
        //}
        #endregion

        #region insert
        /// <summary>
        /// Insert Log
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userEdited"></param>
        /// <param name="propertiesChanged"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="actionTable"></param>
        //public void InsertLog(int productId, string userEdited, string propertiesChanged, string oldValue, string newValue, string actionTable)
        //{
        //    try
        //    {
        //        var logProduct = new Log_Shop_Product()
        //        {
        //            ProductId = productId,
        //            UserEdited = userEdited,
        //            PropertiesChanged = propertiesChanged,
        //            OldValue = oldValue,
        //            NewValue = newValue,
        //            DateChanged = DateTime.Now,
        //            TypeActionName = actionTable
        //        };
        //        FDIDB.Log_Shop_Product.Add(logProduct);
        //        Save();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// Nghiatc2.
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            try
            {
                FDIDB.SaveChanges();

            }
            catch (Exception)
            {

            }
        }
        #endregion

    }
}
