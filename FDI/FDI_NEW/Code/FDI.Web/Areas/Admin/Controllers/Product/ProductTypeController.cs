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
    public class ProductTypeController : BaseController
    {
        readonly Shop_ProductTypeDA _productTypeDA = new Shop_ProductTypeDA("#");

        public ActionResult Index()
        {
           
            return View(systemActionItem);
        }

        public ActionResult ListItems()
        {
            var model = new ModelProductTypeItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _productTypeDA.GetListSimpleByRequest(Request),
                PageHtml = _productTypeDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxView()
        {
            var productTypeModel = _productTypeDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = productTypeModel;
            return View();
        }

        public ActionResult AjaxForm()
        {
            var productTypeModel = new Shop_Product_Type();

            if (DoAction == ActionType.Edit)
                productTypeModel = _productTypeDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = productTypeModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var productType = new Shop_Product_Type();
            List<Shop_Product_Type> ltsproductTypeItems;
            StringBuilder stbMessage;

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

                        UpdateModel(productType);

                        #region properties
                        if (Request["Value_DefaultImages"] == string.Empty)
                        {
                            msg.Message = "Bạn phải chọn hình ảnh đại diện!";
                            msg.Erros = true;
                            break;
                        }

                        productType.PictureID = Request["Value_DefaultImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_DefaultImages"]);

                        //if (productType.PictureID == 0)
                        //{
                        //    msg = new JsonMessage()
                        //    {
                        //        Erros = false,
                        //        Message = string.Format("Thiếu hình đại diện")
                        //    };
                        //    break;
                        //}

                        productType.Name = Convert.ToString(productType.Name);
                        productType.NameAscii = String.IsNullOrEmpty(productType.Name) == false ? MyString.Slug(productType.Name) : string.Empty;
                        productType.IsActived = Request["IsActived"] != null && Request["IsActived"] != string.Empty;
                        productType.IsHasSize = Request["IsHasSize"] != null && Request["IsHasSize"] != string.Empty;
                        productType.IsHasWeight = Request["IsHasWeight"] != null && Request["IsHasWeight"] != string.Empty;
                        productType.IsHasColor = Request["IsHasColor"] != null && Request["IsHasColor"] != string.Empty;
                        productType.IsHasBrand = Request["IsHasBrand"] != null && Request["IsHasBrand"] != string.Empty;
                        productType.Description = Convert.ToString(productType.Description);
                        #endregion

                        _productTypeDA.Add(productType);
                        _productTypeDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = productType.ID.ToString(),
                            Message = string.Format("Đã thêm mới <b>{0}</b>", Server.HtmlEncode(productType.Name))
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

                        productType = _productTypeDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(productType);

                        #region properties
                        productType.PictureID = Request["Value_DefaultImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_DefaultImages"]);

                        productType.Name = Convert.ToString(productType.Name);
                        productType.NameAscii = String.IsNullOrEmpty(productType.Name) == false ? MyString.Slug(productType.Name) : string.Empty;
                        productType.IsActived = Request["IsActived"] != null && Request["IsActived"] != string.Empty;
                        productType.IsHasSize = Request["IsHasSize"] != null && Request["IsHasSize"] != string.Empty;
                        productType.IsHasWeight = Request["IsHasWeight"] != null && Request["IsHasWeight"] != string.Empty;
                        productType.IsHasColor = Request["IsHasColor"] != null && Request["IsHasColor"] != string.Empty;
                        productType.IsHasBrand = Request["IsHasBrand"] != null && Request["IsHasBrand"] != string.Empty;
                        productType.Description = Convert.ToString(productType.Description);
                        #endregion

                        _productTypeDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = productType.ID.ToString(),
                            Message = string.Format("Đã cập nhật <b>{0}</b>", Server.HtmlEncode(productType.Name))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;

                case ActionType.Show:
                    if (systemActionItem.Show != true)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                    ltsproductTypeItems = _productTypeDA.GetListByArrID(ArrID).Where(o => o.IsActived == false).ToList();
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsproductTypeItems)
                    {
                        item.IsActived = true;
                        stbMessage.AppendFormat("Đã hiển thị <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _productTypeDA.Save();
                    msg.ID = string.Join(",", ltsproductTypeItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    if (systemActionItem.Hide != true)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                    ltsproductTypeItems = _productTypeDA.GetListByArrID(ArrID).Where(o => o.IsActived == true).ToList();
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsproductTypeItems)
                    {
                        item.IsActived = false;
                        stbMessage.AppendFormat("Đã ẩn <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _productTypeDA.Save();
                    msg.ID = string.Join(",", ltsproductTypeItems.Select(o => o.ID));
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

        public ActionResult AutoComplete()
        {
            var term = Request["term"];
            var ltsResults = _productTypeDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
