using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;

namespace FDI.DA.Admin
{
    public class Shop_Product_Search_MappingDA : BaseDA
    {
        #region Constructer

        public Shop_Product_Search_MappingDA()
        {
        }

        public Shop_Product_Search_MappingDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public Shop_Product_Search_MappingDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }

        #endregion



        #region Check Exits, Add, Update, Delete


        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="ProductID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Shop_Product_Search_Mapping GetById(int ProductID)
        {
            var query = from c in FDIDB.Shop_Product_Search_Mapping where c.ProductID == ProductID select c;
            return query.FirstOrDefault();
        }

        public void Add(Shop_Product_Search_Mapping shopProduct)
        {
            try
            {
                FDIDB.Shop_Product_Search_Mapping.Add(shopProduct);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ex.StackTrace.ToString();
            }
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
