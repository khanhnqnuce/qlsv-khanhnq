using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class ProductVariantController : BaseController
    {
        private readonly Shop_Product_VariantDA _productVariantDa = new Shop_Product_VariantDA("#");
        private readonly Shop_ProductVariantRecurringDA _productVariantRecurringDA = new Shop_ProductVariantRecurringDA("#");

        public ActionResult Index()
        {           
            var model = new ModelProductItem
                            {
                                SystemActionItem = systemActionItem,
                                ListItem = _productVariantDa.GetListProduct(),
                                ListColorItem = _productVariantDa.GetListColor()
                            };

            return View(model);
        }

        public ActionResult ListItems()
        {
            var model = new ModelProductVariantItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _productVariantDa.getListSimpleByRequest(Request),
                PageHtml = _productVariantDa.GridHtmlPage
            };

            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxForm()
        {
            var productModel = new Shop_Product_Variant();

            if (DoAction == ActionType.Edit)
                productModel = _productVariantDa.getById(ArrID.FirstOrDefault());


            ViewData.Model = productModel;
            ViewBag.ColorID = _productVariantDa.GetListColor();
            ViewBag.ProductID = _productVariantDa.GetListProduct();
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVariant()
        {
            var msg = new JsonMessage();
            var productVariant = new Shop_Product_Variant();
            //List<Shop_Product_Variant> lstProductVariant;
            //List<Gallery_Picture> pictureSelected;

            try
            {
                //UpdateModel(productVariant);

                #region check color
                productVariant.ProductID = Convert.ToInt32(Request["ProductID"]);
                productVariant.ColorID = Convert.ToInt32(Request["ColorID"]);
                var checkColor = _productVariantDa.CheckExitsByColor(productVariant.ColorID, productVariant.ProductID);
                if (checkColor)
                {
                    msg = new JsonMessage
                              {
                                  Erros = true,
                                  ID = productVariant.ID.ToString(),
                                  Message = string.Format("Biến thể sản phẩm này đã có")
                              };

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region properties
                productVariant.Sku = Convert.ToString(Request["Sku"]);
                productVariant.IsRecurring = Request["IsRecurring"] != null && Request["IsRecurring"] != string.Empty;
                productVariant.DisplayStockAvailability = Request["DisplayStockAvailability"] != null && Request["DisplayStockAvailability"] != string.Empty;
                productVariant.DisplayStockQuantity = Request["DisplayStockQuantity"] != null && Request["DisplayStockQuantity"] != string.Empty;
                productVariant.NotifyAdminForQuantityBelow = Request["NotifyAdminForQuantityBelow"] != null && Request["NotifyAdminForQuantityBelow"] != string.Empty;
                productVariant.DisableBuyButton = Request["DisableBuyButton"] != null && Request["DisableBuyButton"] != string.Empty;
                productVariant.DisableWishlistButton = Request["DisableWishlistButton"] != null && Request["DisableWishlistButton"] != string.Empty;
                productVariant.AvailableForPreOrder = Request["AvailableForPreOrder"] != null && Request["AvailableForPreOrder"] != string.Empty;
                productVariant.CallForPrice = Request["CallForPrice"] != null && Request["CallForPrice"] != string.Empty;

                productVariant.IsPublished = Request["IsPublished"] != null && Request["IsPublished"] != string.Empty;
                productVariant.IsHomePage = Request["IsHomePage"] != null && Request["IsHomePage"] != string.Empty;
                productVariant.IsSlider = Request["IsSlider"] != null && Request["IsSlider"] != string.Empty;
                productVariant.IsBestSeller = Request["IsBestSeller"] != null && Request["IsBestSeller"] != string.Empty;

                productVariant.IsAllowOrderOutStock = Request["IsAllowOrderOutStock"] != null && Request["IsAllowOrderOutStock"] != string.Empty;
                productVariant.IsApplyDiscount = Request["IsApplyDiscount"] != null && Request["IsApplyDiscount"] != string.Empty;

                productVariant.IsApplyDiscountCrossell = Request["IsApplyDiscountCrossell"] != null && Request["IsApplyDiscountCrossell"] != string.Empty;
                productVariant.IsApplyDiscountShoppingCart = Request["IsApplyDiscountShoppingCart"] != null && Request["IsApplyDiscountShoppingCart"] != string.Empty;
                productVariant.IsApplyDiscountSpecial = Request["IsApplyDiscountSpecial"] != null && Request["IsApplyDiscountSpecial"] != string.Empty;

                productVariant.AvailableStartDateTimeUtc = MyDateTime.ToDate(Request["AvailableStartDateTimeUtc"]);
                productVariant.AvailableEndDateTimeUtc = MyDateTime.ToDate(Request["AvailableEndDateTimeUtc"]);
                productVariant.UpdatedOnUtc = DateTime.Now;

                productVariant.MetaDescription = Convert.ToString(Request["MetaDescription"]);
                productVariant.MetaKeyWords = Convert.ToString(Request["MetaKeyWords"]);

                productVariant.Price = Convert.ToDecimal(Request["Price"].Replace(".", ""));
                productVariant.PriceAfterTax = Convert.ToDecimal(Request["PriceAfterTax"].Replace(".", ""));
                productVariant.PriceBeforeTax = Convert.ToDecimal(Request["PriceBeforeTax"].Replace(".", ""));
                productVariant.PriceOnlineBeforeTax = Convert.ToDecimal(Request["PriceOnlineBeforeTax"].Replace(".", ""));
                productVariant.PriceOnline = Convert.ToDecimal(Request["PriceOnline"].Replace(".", ""));


                productVariant.CreatedOnUtc = DateTime.Now;
                productVariant.UpdatedOnUtc = DateTime.Now;
                productVariant.AvailableStartDateTimeUtc = MyDateTime.ToDate(Request["AvailableStartDateTimeUtc"]);
                productVariant.AvailableEndDateTimeUtc = MyDateTime.ToDate(Request["AvailableEndDateTimeUtc"]);
                productVariant.IsDeleted = false;

                productVariant.StockQuantity = Convert.ToInt32(Request["StockQuantity"]);
                productVariant.MinStockQuantity = Convert.ToInt32(Request["MinStockQuantity"]);
                productVariant.OrderMaximumQuantity = Convert.ToInt32(Request["OrderMaximumQuantity"]);
                productVariant.OrderMinimumQuantity = Convert.ToInt32(Request["OrderMinimumQuantity"]);
                productVariant.Position = Convert.ToInt32(Request["Position"]);
                #endregion

                #region Shop_Product_PreOrder
                if (productVariant.AvailableForPreOrder == true && (String.IsNullOrEmpty(Convert.ToString(Request["PreOrderShortDescription"])) == false || String.IsNullOrEmpty(Convert.ToString(Request["PreOrderDescription"])) == false))
                {
                    var productPreOrder = new Shop_Product_PreOrder
                                              {
                                                  PreOrderShortDescription =
                                                      Convert.ToString(
                                                          Server.HtmlDecode(Request["PreOrderShortDescription"])),
                                                  PreOrderDescription =
                                                      Convert.ToString(Server.HtmlDecode(Request["PreOrderDescription"]))
                                              };

                    productVariant.Shop_Product_PreOrder = productPreOrder;
                }
                else
                {
                    productVariant.PreOrderID = 0;
                }
                #endregion

                productVariant.CreatedOnUtc = DateTime.Now;

                _productVariantDa.Add(productVariant);

                #region picture
                var idValues = MyBase.ConvertStringToListInt(Request["Value_Images"]);
                var displayOrder = 0;
                //pictureSelected = productVariantDa.getListPictureByArrID(IDValues);
                foreach (var item in idValues)
                {
                    _productVariantDa.AddGalleryPicture(new Shop_Product_Variant_Picture_Mapping
                                                            {
                                                                DisplayOrder = displayOrder,
                                                                PictureID = item,
                                                                ProductVariantID = productVariant.ID
                                                            });
                    displayOrder++;
                }
                #endregion

                #region Recurring
                //create by BienLV 22-11-2013
                //insert recurring
                if (productVariant.IsRecurring.Value)
                {
                    _productVariantRecurringDA.Add(new Shop_Product_Variant_Recurring
                                                       {
                                                           ID = Int32.Parse(Request["productRecurring"]),
                                                           ProductVariantID = productVariant.ID,
                                                           RecurringTitle = Request["RecurringTitle"],
                                                           MustRepaidPercent = float.Parse(Request["MustRepaidPercent"]),
                                                           RecurringLength = Request["RecurringLength"],
                                                           InterestRate = float.Parse(Request["InterestRate"]),
                                                           MetaKeywordRecurring = Request["MetaKeywordRecurring"],
                                                           MetaDescriptionRecurring = Request["MetaDescriptionRecurring"],
                                                           IsDelete = false
                                                       });
                    _productVariantRecurringDA.Save();
                }
                #endregion

                _productVariantDa.Save();
                msg = new JsonMessage
                          {
                              Erros = false,
                              Message = string.Format("Đã thêm mới sản phẩm")
                          };
            }
            catch (Exception ex)
            {
                LogHelper.Instance.LogError(GetType(), ex);
            }

            if (string.IsNullOrEmpty(msg.Message))
            {
                msg.Message = "Không có hành động nào được thực hiện.";
                msg.Erros = true;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxView()
        {
            var productVariant = _productVariantDa.getById(ArrID.FirstOrDefault());
            ViewData.Model = productVariant;
            return View();
        }

        public ActionResult ProductRecurring(int productId)
        {
            var productVariantRecurring = _productVariantRecurringDA.GetByProductId(productId);

            return productVariantRecurring != null
                       ? View(productVariantRecurring)
                       : View(new Shop_Product_Variant_Recurring());
        }

        public ActionResult ProductRecurringView(int productId)
        {
            var productVariantRecurring = _productVariantRecurringDA.GetByProductId(productId);
            return View(productVariantRecurring);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var productVariant = new Shop_Product_Variant();
            List<int> idValues;
            //List<Gallery_Picture> pictureSelected;

            switch (DoAction)
            {
                case ActionType.Add:
                    try
                    {
                        if (systemActionItem.Add != true)
                        {
                            msg = new JsonMessage
                                      {
                                          Erros = true,
                                          Message = "Bạn chưa được phân quyền cho chức năng này!"
                                      };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }

                        UpdateModel(productVariant);
                        productVariant.CreatedOnUtc = DateTime.Now;
                        productVariant.IsDeleted = false;
                        //productVariant.UserCreated = User.Identity.Name;
                        _productVariantDa.Add(productVariant);
                        _productVariantDa.Save();
                        //#region check color
                        //productVariant.ProductID = Convert.ToInt32(Request["ProductID"]);
                        //productVariant.ColorID = Convert.ToInt32(Request["ColorID"]);
                        //checkColor = _productVariantDa.CheckExitsByColor(productVariant.ColorID, productVariant.ProductID);
                        //if (checkColor)
                        //{
                        //    msg = new JsonMessage
                        //              {
                        //                  Erros = true,
                        //                  ID = productVariant.ID.ToString(),
                        //                  Message = string.Format("Biến thể sản phẩm này đã có")
                        //              };

                        //    break;
                        //}
                        //#endregion

                        //#region properties
                        //productVariant.IsRecurring = Request["IsRecurring"] != null && Request["IsRecurring"] != string.Empty;
                        //productVariant.DisplayStockAvailability = Request["DisplayStockAvailability"] != null && Request["DisplayStockAvailability"] != string.Empty;
                        //productVariant.DisplayStockQuantity = Request["DisplayStockQuantity"] != null && Request["DisplayStockQuantity"] != string.Empty;
                        //productVariant.NotifyAdminForQuantityBelow = Request["NotifyAdminForQuantityBelow"] != null && Request["NotifyAdminForQuantityBelow"] != string.Empty;
                        //productVariant.DisableBuyButton = Request["DisableBuyButton"] != null && Request["DisableBuyButton"] != string.Empty;
                        //productVariant.DisableWishlistButton = Request["DisableWishlistButton"] != null && Request["DisableWishlistButton"] != string.Empty;
                        //productVariant.CallForPrice = Request["CallForPrice"] != null && Request["CallForPrice"] != string.Empty;

                        //productVariant.IsPublished = Request["IsPublished"] != null && Request["IsPublished"] != string.Empty;
                        //productVariant.IsHomePage = Request["IsHomePage"] != null && Request["IsHomePage"] != string.Empty;
                        //productVariant.IsSlider = Request["IsSlider"] != null && Request["IsSlider"] != string.Empty;
                        //productVariant.IsBestSeller = Request["IsBestSeller"] != null && Request["IsBestSeller"] != string.Empty;

                        //productVariant.IsAllowOrderOutStock = Request["IsAllowOrderOutStock"] != null && Request["IsAllowOrderOutStock"] != string.Empty;
                        //productVariant.IsApplyDiscount = Request["IsApplyDiscount"] != null && Request["IsApplyDiscount"] != string.Empty;
                        //productVariant.IsApplyDiscountCrossell = Request["IsApplyDiscountCrossell"] != null && Request["IsApplyDiscountCrossell"] != string.Empty;
                        //productVariant.IsApplyDiscountShoppingCart = Request["IsApplyDiscountShoppingCart"] != null && Request["IsApplyDiscountShoppingCart"] != string.Empty;
                        //productVariant.IsApplyDiscountSpecial = Request["IsApplyDiscountSpecial"] != null && Request["IsApplyDiscountSpecial"] != string.Empty;

                        //productVariant.CreatedOnUtc = DateTime.Now;
                        //productVariant.UpdatedOnUtc = DateTime.Now;
                        //productVariant.AvailableStartDateTimeUtc = ConvertUtil.ToDate(Request["AvailableStartDateTimeUtc"]);
                        //productVariant.AvailableEndDateTimeUtc = ConvertUtil.ToDate(Request["AvailableEndDateTimeUtc"]);
                        //productVariant.IsDeleted = false;

                        //productVariant.StockQuantity = Convert.ToInt32(Request["StockQuantity"]);
                        //productVariant.MinStockQuantity = Convert.ToInt32(Request["MinStockQuantity"]);
                        //productVariant.OrderMaximumQuantity = Convert.ToInt32(Request["OrderMaximumQuantity"]);
                        //productVariant.OrderMinimumQuantity = Convert.ToInt32(Request["OrderMinimumQuantity"]);
                        //productVariant.Position = Convert.ToInt32(Request["Position"]);
                        //#endregion

                        //#region Shop_Product_PreOrder
                        //if (productVariant.AvailableForPreOrder == true && (String.IsNullOrEmpty(Convert.ToString(Request["PreOrderShortDescription"])) == false || String.IsNullOrEmpty(Convert.ToString(Request["PreOrderDescription"])) == false))
                        //{
                        //    var productPreOrder = new Shop_Product_PreOrder
                        //                              {
                        //                                  PreOrderShortDescription =
                        //                                      Convert.ToString(
                        //                                          Server.HtmlDecode(Request["PreOrderShortDescription"])),
                        //                                  PreOrderDescription =
                        //                                      Convert.ToString(
                        //                                          Server.HtmlDecode(Request["PreOrderDescription"]))
                        //                              };

                        //    productVariant.Shop_Product_PreOrder = productPreOrder;
                        //}
                        //else
                        //{
                        //    productVariant.PreOrderID = 0;
                        //}
                        //#endregion

                        
                        //productVariant.UserCreated = User.Identity.Name;
                        //productVariant.UserUpdated = User.Identity.Name;
                        

                        #region picture
                        idValues = MyBase.ConvertStringToListInt(Request["Value_Images"]);
                        var displayOrder = 0;
                        //pictureSelected = productVariantDa.getListPictureByArrID(IDValues);
                        foreach (var item in idValues)
                        {
                            _productVariantDa.AddGalleryPicture(new Shop_Product_Variant_Picture_Mapping
                                                                    {
                                                                        DisplayOrder = displayOrder,
                                                                        PictureID = item,
                                                                        ProductVariantID = productVariant.ID
                                                                    });
                            displayOrder++;
                        }
                        #endregion

                        //#region Recurring
                        ////create by BienLV 22-11-2013
                        ////insert recurring
                        //if (productVariant.IsRecurring != null && productVariant.IsRecurring.Value)
                        //{
                        //    _productVariantRecurringDA.Add(new Shop_Product_Variant_Recurring
                        //                                       {
                        //                                           ID = Int32.Parse(!string.IsNullOrEmpty(Request["productRecurring"]) ? Request["productRecurring"] : "0"),
                        //                                           ProductVariantID = productVariant.ID,
                        //                                           RecurringTitle = Request["RecurringTitle"],
                        //                                           MustRepaidPercent = float.Parse(!string.IsNullOrEmpty(Request["MustRepaidPercent"]) ? Request["MustRepaidPercent"] : "0"),
                        //                                           RecurringLength = Request["RecurringLength"],
                        //                                           InterestRate = float.Parse(!string.IsNullOrEmpty(Request["InterestRate"]) ? Request["InterestRate"] : "0"),
                        //                                           MetaKeywordRecurring = Request["MetaKeywordRecurring"],
                        //                                           MetaDescriptionRecurring = Request["MetaDescriptionRecurring"],
                        //                                           IsDelete = false
                        //                                       });
                        //    _productVariantRecurringDA.Save();
                        //}
                        //#endregion

                        _productVariantDa.Save();
                        msg = new JsonMessage
                                  {
                                      Erros = false,
                                      Message = string.Format("Đã thêm mới sản phẩm")
                                  };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;
                case ActionType.Edit:
                    try
                    {
                        if (systemActionItem.Edit != true)
                        {
                            msg = new JsonMessage
                                      {
                                          Erros = true,
                                          Message = "Bạn chưa được phân quyền cho chức năng này!"
                                      };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }

                        productVariant = _productVariantDa.getById(ArrID.FirstOrDefault());

                        UpdateModel(productVariant);
                        #region check color
                        productVariant.ProductID = Convert.ToInt32(Request["ProductID"]);
                        productVariant.ColorID = Convert.ToInt32(Request["ColorID"]);
                        bool checkColor = _productVariantDa.CheckExitsByColor(productVariant.ColorID, productVariant.ProductID, productVariant.ID);
                        if (checkColor)
                        {
                            msg = new JsonMessage
                                      {
                                          Erros = false,
                                          Message = string.Format("Biến thể sản phẩm này đã có ")
                                      };

                            break;
                        }
                        #endregion

                        #region properties
                        //productVariant.Sku = Convert.ToString(Request["Sku"]);
                        //productVariant.IsRecurring = Request["IsRecurring"] != null && Request["IsRecurring"] != string.Empty;
                        //productVariant.DisplayStockAvailability = Request["DisplayStockAvailability"] != null && Request["DisplayStockAvailability"] != string.Empty;
                        //productVariant.DisplayStockQuantity = Request["DisplayStockQuantity"] != null && Request["DisplayStockQuantity"] != string.Empty;
                        //productVariant.NotifyAdminForQuantityBelow = Request["NotifyAdminForQuantityBelow"] != null && Request["NotifyAdminForQuantityBelow"] != string.Empty;
                        //productVariant.DisableBuyButton = Request["DisableBuyButton"] != null && Request["DisableBuyButton"] != string.Empty;
                        //productVariant.DisableWishlistButton = Request["DisableWishlistButton"] != null && Request["DisableWishlistButton"] != string.Empty;
                        //productVariant.CallForPrice = Request["CallForPrice"] != null && Request["CallForPrice"] != string.Empty;

                        //productVariant.IsPublished = Request["IsPublished"] != null && Request["IsPublished"] != string.Empty;
                        //productVariant.IsHomePage = Request["IsHomePage"] != null && Request["IsHomePage"] != string.Empty;
                        //productVariant.IsSlider = Request["IsSlider"] != null && Request["IsSlider"] != string.Empty;
                        //productVariant.IsBestSeller = Request["IsBestSeller"] != null && Request["IsBestSeller"] != string.Empty;

                        //productVariant.IsAllowOrderOutStock = Request["IsAllowOrderOutStock"] != null && Request["IsAllowOrderOutStock"] != string.Empty;
                        //productVariant.IsApplyDiscount = Request["IsApplyDiscount"] != null && Request["IsApplyDiscount"] != string.Empty;
                        //productVariant.IsApplyDiscountCrossell = Request["IsApplyDiscountCrossell"] != null && Request["IsApplyDiscountCrossell"] != string.Empty;
                        //productVariant.IsApplyDiscountShoppingCart = Request["IsApplyDiscountShoppingCart"] != null && Request["IsApplyDiscountShoppingCart"] != string.Empty;
                        //productVariant.IsApplyDiscountSpecial = Request["IsApplyDiscountSpecial"] != null && Request["IsApplyDiscountSpecial"] != string.Empty;

                        //productVariant.AvailableStartDateTimeUtc = ConvertUtil.ToDate(Request["AvailableStartDateTimeUtc"]);
                        //productVariant.AvailableEndDateTimeUtc = ConvertUtil.ToDate(Request["AvailableEndDateTimeUtc"]);
                        //productVariant.UpdatedOnUtc = DateTime.Now;

                        //productVariant.MetaDescription = Convert.ToString(Request["MetaDescription"]);
                        //productVariant.MetaKeyWords = Convert.ToString(Request["MetaKeyWords"]);

                        //productVariant.Price = Convert.ToDecimal(Request["Price"].Replace(".", ""));
                        //productVariant.PriceAfterTax = Convert.ToDecimal(Request["PriceAfterTax"].Replace(".", ""));
                        //productVariant.PriceBeforeTax = Convert.ToDecimal(Request["PriceBeforeTax"].Replace(".", ""));
                        //productVariant.PriceOnlineBeforeTax = Convert.ToDecimal(Request["PriceOnlineBeforeTax"].Replace(".", ""));
                        //productVariant.PriceOnline = Convert.ToDecimal(Request["PriceOnline"].Replace(".", ""));
                        ////productVariant.PriceMarket = Convert.ToDecimal(Request["PriceMarket"].Replace(".", ""));

                        //productVariant.StockQuantity = Convert.ToInt32(Request["StockQuantity"]);
                        //productVariant.MinStockQuantity = Convert.ToInt32(Request["MinStockQuantity"]);
                        //productVariant.OrderMaximumQuantity = Convert.ToInt32(Request["OrderMaximumQuantity"]);
                        //productVariant.OrderMinimumQuantity = Convert.ToInt32(Request["OrderMinimumQuantity"]);
                        //productVariant.Position = Convert.ToInt32(Request["Position"]);
                        #endregion

                        #region Shop_Product_PreOrder
                        var preOrderId = productVariant.PreOrderID.HasValue ? productVariant.PreOrderID.Value : 0;
                        if (preOrderId > 0 && productVariant.AvailableForPreOrder.HasValue && productVariant.AvailableForPreOrder.Value)
                        {
                            var productPreOrderEdit = _productVariantDa.GetProductPreOrderByID(productVariant.PreOrderID.HasValue? productVariant.PreOrderID.Value:0);
                            productPreOrderEdit.PreOrderShortDescription = Convert.ToString(Server.HtmlDecode(Request["PreOrderShortDescription"]));
                            productPreOrderEdit.PreOrderDescription = Convert.ToString(Server.HtmlDecode(Request["PreOrderDescription"]));
                            productVariant.Shop_Product_PreOrder = productPreOrderEdit;
                        }
                        #endregion

                        productVariant.UpdatedOnUtc = DateTime.Now;
                       
                        #region picture
                        _productVariantDa.DeleteAllGalleryPicture(productVariant.ID);
                        idValues = MyBase.ConvertStringToListInt(Request["Value_Images"]);
                        var displayOrder = 0;
                        //pictureSelected = productVariantDa.getListPictureByArrID(IDValues);
                        foreach (var item in idValues)
                        {
                            _productVariantDa.AddGalleryPicture(new Shop_Product_Variant_Picture_Mapping
                                                                    {
                                                                        DisplayOrder = displayOrder,
                                                                        PictureID = item,
                                                                        ProductVariantID = productVariant.ID
                                                                    });
                            displayOrder++;
                        }
                        #endregion

                        //#region Recurring
                        ////create by BienLV 22-11-2013
                        ////update recurring
                        //var recurring = _productVariantRecurringDA.GetByProductId(productVariant.ID);
                        //if (productVariant.IsRecurring.Value)
                        //{
                        //    var prodcutRecurring = new Shop_Product_Variant_Recurring
                        //                               {
                        //                                   ID = Int32.Parse(!string.IsNullOrEmpty(Request["productRecurring"]) ? Request["productRecurring"] : "0"),
                        //                                   ProductVariantID = productVariant.ID,
                        //                                   RecurringTitle = Request["RecurringTitle"],
                        //                                   MustRepaidPercent = float.Parse(!string.IsNullOrEmpty(Request["MustRepaidPercent"]) ? Request["MustRepaidPercent"] : "0"),
                        //                                   RecurringLength = Request["RecurringLength"],
                        //                                   InterestRate = float.Parse(!string.IsNullOrEmpty(Request["InterestRate"]) ? Request["InterestRate"] : "0"),
                        //                                   MetaKeywordRecurring = Request["MetaKeywordRecurring"],
                        //                                   MetaDescriptionRecurring = Request["MetaDescriptionRecurring"],
                        //                                   IsDelete = false
                        //                               };

                        //    if (recurring == null)
                        //    {
                        //        _productVariantRecurringDA.Add(prodcutRecurring);
                        //        _productVariantRecurringDA.Save();
                        //    }
                        //    else
                        //    {
                        //        UpdateModel(prodcutRecurring);
                        //        _productVariantRecurringDA.Save();
                        //    }
                        //}
                        //else
                        //{
                        //    if (recurring != null)
                        //    {
                        //        recurring.IsDelete = true;
                        //        UpdateModel(recurring);
                        //        _productVariantRecurringDA.Save();
                        //    }
                        //}
                        //#endregion

                        _productVariantDa.Save();

                        msg = new JsonMessage
                                  {
                                      Erros = false,
                                      ID = productVariant.ID.ToString(),
                                      Message = string.Format("Đã sửa sản phẩm: <b>{0}</b>", Server.HtmlEncode(productVariant.Shop_Product.Name + " màu " + productVariant.System_Color.Name))
                                  };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }

                    break;
                case ActionType.Delete:
                    if (systemActionItem.Delete != true)
                    {
                        msg = new JsonMessage
                                  {
                                      Erros = true,
                                      Message = "Bạn chưa được phân quyền cho chức năng này!"
                                  };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                    var lstProductVariant = _productVariantDa.GetListByArrID(ArrID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in lstProductVariant)
                    {
                        if (item.Shop_Product_Variant_Recurring.Any() || item.Shop_Promotion_Product_Variant.Any())
                        {
                            stbMessage.AppendFormat("<b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.Shop_Product.Name + " màu " + item.System_Color.Name));
                            //msg.Erros = true;
                        }
                        else
                        {
                            item.IsDeleted = true;
                            _productVariantDa.Save();
                            stbMessage.AppendFormat("Đã xóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Shop_Product.Name + " màu " + item.System_Color.Name));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _productVariantDa.Save();
                    msg.Message = stbMessage.ToString();

                    break;
            }

            if (string.IsNullOrEmpty(msg.Message))
            {
                msg.Message = "Không có hành động nào được thực hiện.";
                msg.Erros = true;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string CheckBySku(string Sku, int productId)
        {
            var result = _productVariantDa.CheckExitsBySku(Sku, productId);
            return result ? "false" : "true";
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPriceBySku(string sku)
        {
            try
            {
                var price = _productVariantDa.GetPriceBySkuFromHO(sku);

                return Json(new { error = false, result = price }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
            }

            return Json(new { error = true }, JsonRequestBehavior.AllowGet);

        }
    }
}
