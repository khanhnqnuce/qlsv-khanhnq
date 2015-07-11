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
    public class ProductLabelController : BaseController
    {
        private readonly Shop_LabelDA _shopLabelDa = new Shop_LabelDA("#");

        public ActionResult Index()
        {
            return View(systemActionItem);
        }

        public ActionResult ListItems()
        {
            var model = new ModelLabelItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = _shopLabelDa.GetListSimpleByRequest(Request),
                PageHtml = _shopLabelDa.GridHtmlPage
            };
            ViewData.Model = model;
            
            return View();
        }

        public ActionResult AjaxView()
        {
            var shopLabelModel = _shopLabelDa.GetById(ArrID.FirstOrDefault());
            ViewData.Model = shopLabelModel;
            return View();
        }

        public ActionResult AjaxForm()
        {
            var shopLabelModel = new Shop_Label();

            if (DoAction == ActionType.Edit)
                shopLabelModel = _shopLabelDa.GetById(ArrID.FirstOrDefault());

            ViewData.Model = shopLabelModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var shopLabel = new Shop_Label();
            List<Shop_Label> ltsshopLabelItems;
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

                        UpdateModel(shopLabel);
                        shopLabel.Name = Convert.ToString(shopLabel.Name);
                        shopLabel.IsShow = Request["IsShow"] != null && Request["IsShow"] != string.Empty;
                        shopLabel.IsShowInSearch = Request["IsShowInSearch"] != null && Request["IsShowInSearch"] != string.Empty;

                        if (Request["Value_DefaultImages"] == string.Empty)
                        {
                            msg.Message = "Bạn chưa chọn hình ảnh";
                            msg.Erros = true;
                            break;
                        }

                        shopLabel.PictureID = Convert.ToInt32(Request["Value_DefaultImages"]);

                        _shopLabelDa.Add(shopLabel);
                        _shopLabelDa.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = shopLabel.ID.ToString(),
                            Message = string.Format("Đã thêm mới <b>{0}</b>", Server.HtmlEncode(shopLabel.Name))
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

                        shopLabel = _shopLabelDa.GetById(ArrID.FirstOrDefault());
                        UpdateModel(shopLabel);
                        shopLabel.Name = Convert.ToString(shopLabel.Name);
                        shopLabel.IsShow = Request["IsShow"] != null && Request["IsShow"] != string.Empty;
                        shopLabel.IsShowInSearch = Request["IsShowInSearch"] != null && Request["IsShowInSearch"] != string.Empty;
                        shopLabel.PictureID = Request["Value_DefaultImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_DefaultImages"]);

                        _shopLabelDa.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = shopLabel.ID.ToString(),
                            Message = string.Format("Đã cập nhật <b>{0}</b>", Server.HtmlEncode(shopLabel.Name))
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

                    ltsshopLabelItems = _shopLabelDa.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsshopLabelItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _shopLabelDa.Save();
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

                    ltsshopLabelItems = _shopLabelDa.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsshopLabelItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _shopLabelDa.Save();
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
    }
}
