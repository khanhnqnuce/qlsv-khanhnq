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
    public class NewsCategoryController : BaseController
    {
        readonly News_CategoryDA _categoryDA = new News_CategoryDA("#");

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(systemActionItem);
        }

        public ActionResult AjaxTreeSelect()
        {
            var ltsSourceCategory = _categoryDA.GetAllListSimple();
            var ltsValues = MyBase.ConvertStringToListInt(Request["ValuesSelected"]);
            var stbHtml = new StringBuilder();
            _categoryDA.BuildTreeViewCheckBox(ltsSourceCategory, 1, true, ltsValues, ref stbHtml);
            var model = new ModelNewsCategoryItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"]),
                StbHtml = stbHtml.ToString()
            };
            ViewData.Model = model;
          
            return View();
        }

        public ActionResult AjaxSort()
        {
            var ltsSourceCategory = _categoryDA.GetAllListSimpleByParentID(ArrID.FirstOrDefault());
            ViewData.Model = ltsSourceCategory;
            return View();
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
           
            var ltsSourceCategory = _categoryDA.GetAllListSimple();
            var stbHtml = new StringBuilder();
            _categoryDA.BuildTreeView(ltsSourceCategory, 1, false, ref stbHtml, systemActionItem.Add, systemActionItem.Delete, systemActionItem.Edit, systemActionItem.View, systemActionItem.Show, systemActionItem.Hide, systemActionItem.Order);
            var model = new ModelNewsCategoryItem
            {
                SystemActionItem = systemActionItem,
                StbHtml = stbHtml.ToString()
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

            var categoryModel = _categoryDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = categoryModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var categoryModel = new News_Category
            {
                IsShow = true,
                DisplayOrder = 0,
                ParentID = (ArrID.Any()) ? ArrID.FirstOrDefault() : 0
            };

            if (DoAction == ActionType.Edit)
                categoryModel = _categoryDA.GetById(ArrID.FirstOrDefault());


            var ltsAllItems = _categoryDA.GetAllListSimple();
            ViewBag.ParentID = _categoryDA.GetAllSelectList(ltsAllItems, categoryModel.ID);
            ViewData.Model = categoryModel;
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
            var category = new News_Category();
            List<News_Category> ltsCategoryItems;
            StringBuilder stbMessage;

            switch (DoAction)
            {

                case ActionType.Add:
                    try
                    {
                        if (!systemActionItem.Add)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        UpdateModel(category);
                        //category.NameAscii = FDIUtils.MyString.Slug(category.Name);
                        category.IsDeleted = false;
                        _categoryDA.Add(category);
                        _categoryDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = category.ID.ToString(),
                            Message = string.Format("Đã thêm mới chuyên mục: <b>{0}</b>", Server.HtmlEncode(category.Name))
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
                        if (!systemActionItem.Edit)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        category = _categoryDA.GetById(ArrID.FirstOrDefault());
                        //category.NameAscii = FDIUtils.MyString.Slug(category.Name);
                        UpdateModel(category);
                        //Update IsShow
                        category.IsShow = Request["IsShow"] != null && Convert.ToBoolean(Request["IsShow"]);
                        category.IsShowInTab = Request["IsShowInTab"] != null && Convert.ToBoolean(Request["IsShowInTab"]);
                        _categoryDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = category.ID.ToString(),
                            Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>", Server.HtmlEncode(category.Name))
                        };
                    }
                    catch (Exception ex)
                    {

                        LogHelper.Instance.LogError(GetType(), ex);
                    }

                    break;

                case ActionType.Delete:
                    if (!systemActionItem.Delete)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsCategoryItems = _categoryDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCategoryItems)
                    {

                        try
                        {
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }


                    }
                    msg.ID = string.Join(",", ArrID);
                    _categoryDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    if (!systemActionItem.Show)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsCategoryItems = _categoryDA.GetListByArrID(ArrID).Where(o => !o.IsShow).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCategoryItems)
                    {
                        try
                        {
                            item.IsShow = true;
                            stbMessage.AppendFormat("Đã hiển thị chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _categoryDA.Save();
                    msg.ID = string.Join(",", ltsCategoryItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    if (!systemActionItem.Hide)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsCategoryItems = _categoryDA.GetListByArrID(ArrID).Where(o => o.IsShow).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCategoryItems)
                    {
                        try
                        {
                            item.IsShow = false;
                            stbMessage.AppendFormat("Đã ẩn chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _categoryDA.Save();
                    msg.ID = string.Join(",", ltsCategoryItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Order:
                    try
                    {
                        if (!systemActionItem.Order)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        if (!string.IsNullOrEmpty(Request["OrderValues"]))
                        {
                            var orderValues = Request["OrderValues"];
                            if (orderValues.Contains("|"))
                            {
                                foreach (var keyValue in orderValues.Split('|'))
                                {
                                    if (keyValue.Contains("_"))
                                    {
                                        var tempCategory = _categoryDA.GetById(Convert.ToInt32(keyValue.Split('_')[0]));
                                        tempCategory.DisplayOrder = Convert.ToInt32(keyValue.Split('_')[1]);
                                        _categoryDA.Save();
                                    }
                                }
                            }
                            msg.ID = string.Join(",", orderValues);
                            msg.Message = "Đã cập nhật lại thứ tự chuyên mục";
                        }
                    }
                    catch (Exception ex)
                    {

                        LogHelper.Instance.LogError(GetType(), ex);
                    }

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
            var ltsResults = _categoryDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string CheckNameAsciiExits(string NameAscii, int id)
        {
            var ascii = !string.IsNullOrEmpty(NameAscii) ? NameAscii : string.Empty;
            var result = _categoryDA.CheckTitleAsciiExits(ascii, id);

            return result ? "false" : "true";
        }
    }
}
