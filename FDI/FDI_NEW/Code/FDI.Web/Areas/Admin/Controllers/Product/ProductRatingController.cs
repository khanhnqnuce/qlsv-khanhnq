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
    public class ProductRatingController : BaseController
    {
        private readonly Shop_ProductRatingDA _productRatingDa = new Shop_ProductRatingDA("#");
        private readonly CustomerDA _customerDa = new CustomerDA("#");
        private readonly Shop_ProductDA _productDa = new Shop_ProductDA("#");
        private readonly Shop_Product_RateTypeDA _productRateTypeDa = new Shop_Product_RateTypeDA("#");

        public ActionResult Index()
        {
            var model = new ModelCustomerItem
                            {
                                SystemActionItem = systemActionItem,
                                ListItem = _customerDa.GetAll(),
                                ListProductItem = _productDa.GetAllListSimple()
                            };

            return View(model);
        }

        public ActionResult ListItems()
        {
            var model = new ModelProductRatingItem
            {
                SystemActionItem = systemActionItem,               
                ListItem = _productRatingDa.GetListSimpleByRequest(Request),
                PageHtml = _productRatingDa.GridHtmlPage
            };
            ViewData.Model = model;

            return View();
        }

        public ActionResult AjaxForm()
        {
            var productRating = new Shop_Product_Rating();
            ViewBag.ProductRateType = _productRateTypeDa.GetListSimpleAll();

            if (DoAction == ActionType.Edit)
                productRating = _productRatingDa.GetById(ArrID.FirstOrDefault());

            ViewData.Model = productRating;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        public ActionResult AjaxView()
        {
            ViewData.Model = _productRatingDa.GetById(ArrID.FirstOrDefault());
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();

            switch (DoAction)
            {
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

                        var productRating = _productRatingDa.GetById(ArrID.FirstOrDefault());
                        UpdateModel(productRating);
                        productRating.RateNote = Convert.ToString(productRating.RateNote);
                        _productRatingDa.Save();

                        msg = new JsonMessage
                        {
                            Erros = false,
                            Message = string.Format("Đã cập nhật")
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

                    var lstProductRating = _productRatingDa.GetListByArrID(ArrID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in lstProductRating)
                    {
                        item.IdDelete = true;
                        stbMessage.AppendFormat("Đã xóa");
                    }
                    msg.ID = string.Join(",", ArrID);
                    _productRatingDa.Save();
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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditRating()
        {
            int rateNumberId = Convert.ToInt32(Request["RateNumberID"]);
            int rateNumber = Convert.ToInt32(Request["RateNumber"]);
            int productRateId = Convert.ToInt32(Request["ProductRateId"]);

            var item = _productRatingDa.GetProductRateNumbeById(rateNumberId);

            if (item != null)
            {
                item.RateNumber = rateNumber;
                _productRatingDa.Save();
            }

            #region update avg mark
            var productRating = _productRatingDa.GetById(productRateId);
            if (productRating != null)
            {
                if (productRating.Shop_Product_RateNumber.Count > 0)
                {
                    int count = productRating.Shop_Product_RateNumber.Count;
                    var avgRate = productRating.Shop_Product_RateNumber.Sum(itemRateNumber => itemRateNumber.RateNumber.HasValue ? itemRateNumber.RateNumber.Value : (double)0);
                    productRating.RateAvg = avgRate / count;
                    _productRatingDa.Save();
                }
            }
            #endregion

            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}
