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
    public class ProductBrandController : BaseController
    {
        readonly Shop_BrandDA _brandDA = new Shop_BrandDA("#");

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(systemActionItem);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listitem = _brandDA.GetListSimpleByRequest(Request);
            var model = new ModelProductBrandItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = listitem,
                PageHtml = _brandDA.GridHtmlPage
            };
            ViewData.Model = model;

            return View();
        }

        public ActionResult AjaxTreeSelect()
        {
            var ltsSourceCategory = _brandDA.GetListSimpleAll();
            var ltsValues = MyBase.ConvertStringToListInt(Request["ValuesSelected"]);
            var stbHtml = new StringBuilder();
            _brandDA.BuildTreeViewCheckBox(ltsSourceCategory, 1, true, ltsValues, ref stbHtml);

            var model = new ModelProductBrandItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                StbHtml = stbHtml.ToString(),
                PageHtml = _brandDA.GridHtmlPage,
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"])
            };
            ViewData.Model = model;

            return View();
        }

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxView()
        {
            var brandModel = _brandDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = brandModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var brandModel = new Shop_Brand();

            if (DoAction == ActionType.Edit)
                brandModel = _brandDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = brandModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }


        /// <summary>
        /// Hứng các giá trị, phục vụ cho thêm, sửa, xóa, ẩn, hiện
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var brand = new Shop_Brand();

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

                        UpdateModel(brand);
                        brand.NameAscii = MyString.Slug(brand.Name);
                        if (Request != null)
                        {
                            brand.IsShow = Request["IsShow"] != null && Request["IsShow"] != string.Empty;

                            if (Request["Value_DefaultImages"] == string.Empty || Request["Value_LogoImages"] == string.Empty)
                            {
                                msg.Message = "Bạn phải chọn hình ảnh đại diện và logo quảng cáo!";
                                msg.Erros = true;
                                break;
                            }

                            brand.PictureID = Request["Value_DefaultImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_DefaultImages"]);
                            brand.LogoPictureID = Request["Value_LogoImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_LogoImages"]);
                        }
                        _brandDA.Add(brand);
                        _brandDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = brand.ID.ToString(),
                            Message = string.Format("Đã thêm mới <b>{0}</b>", Server.HtmlEncode(brand.Name))
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

                        brand = _brandDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(brand);
                        brand.NameAscii = MyString.Slug(brand.Name);
                        brand.IsShow = Request["IsShow"] != null && Request["IsShow"] != string.Empty;
                        brand.PictureID = Request["Value_DefaultImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_DefaultImages"]);
                        brand.LogoPictureID = Request["Value_LogoImages"] == string.Empty ? 0 : Convert.ToInt32(Request["Value_LogoImages"]);

                        _brandDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = brand.ID.ToString(),
                            Message = string.Format("Đã cập nhật <b>{0}</b>", Server.HtmlEncode(brand.Name))
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

                    var ltsBrandItems = _brandDA.GetListByArrID(ArrID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in ltsBrandItems)
                    {
                        if (item.Shop_Product.Any())
                        {
                            stbMessage.AppendFormat("<b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.Name));
                        }
                        else
                        {
                            _brandDA.Delete(item);
                            stbMessage.AppendFormat("Đã xóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _brandDA.Save();
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

        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["term"];
            var ltsResults = _brandDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
