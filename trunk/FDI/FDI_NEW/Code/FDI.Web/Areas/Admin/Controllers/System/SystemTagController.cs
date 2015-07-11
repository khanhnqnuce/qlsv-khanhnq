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
    public class SystemTagController : BaseController
    {
        //
        // GET: /Admin/SystemTag/

        readonly System_TagDA _tagDA = new System_TagDA("#");
        public ActionResult AutoComplete()
        {
            if (DoAction == ActionType.Add) //Nếu thêm từ khóa
            {
                JsonMessage msg;
                var tagValue = Request["Values"];
                if (string.IsNullOrEmpty(tagValue))
                {
                    msg = new JsonMessage
                    {
                        Erros = true,
                        Message = "Bạn phải nhập tên từ khóa"
                    };
                }
                else
                {
                    var tag = _tagDA.AddOrGet(tagValue);
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = tag.ID.ToString(),
                        Message = tag.Name
                    };
                }
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            var query = Request["query"];
            var ltsResults = _tagDA.GetListSimpleByAutoComplete(query, 10);
            var resulValues = new AutoCompleteItem
            {
                query = query,
                data = ltsResults.Select(o => o.ID.ToString()).ToList(),
                suggestions = ltsResults.Select(o => o.Name).ToList()
            };
            return Json(resulValues, JsonRequestBehavior.AllowGet);
        }


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
            var model = new ModelTagItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _tagDA.GetListSimpleByRequest(Request),
                PageHtml = _tagDA.GridHtmlPage
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
            var tagModel = _tagDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = tagModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>

        public ActionResult AjaxForm()
        {
            var tagModel = new System_Tag();
            if (DoAction == ActionType.Edit)
                tagModel = _tagDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = tagModel;
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
            var tag = new System_Tag();
            List<System_Tag> ltsTagItems;
            List<int> idValues;
            StringBuilder stbMessage;

            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(tag);
                    tag.NameAscii = MyString.Slug(tag.Name);
                    tag.IsDelete = false;
                    tag.IsShow = true;
                    _tagDA.Add(tag);
                    _tagDA.Save();
                    #region Cập nhật category
                    idValues = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                    foreach (var category in idValues)
                    {
                        _tagDA.AddTagToCategoies(tag.ID, category);
                        _tagDA.Save();
                    }
                    #endregion

                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = tag.ID.ToString(),
                        Message = string.Format("Đã thêm mới từ khóa: <b>{0}</b>", Server.HtmlEncode(tag.Name))
                    };
                    break;

                case ActionType.Edit:
                    tag = _tagDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(tag);
                    tag.IsHome = Request["IsHome"] != null && Convert.ToBoolean(Request["IsHome"]);
                    //idValues = FDIUtils.getValuesArray(Request["Weight"]);
                    //tag.Weight = idValues.f;
                    tag.NameAscii = MyString.Slug(tag.Name);
                    _tagDA.Save();

                    #region Cập nhật category
                    idValues = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                    foreach (var category in idValues)
                    {
                        _tagDA.AddTagToCategoies(tag.ID, category);
                        _tagDA.Save();
                    }
                    #endregion
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = tag.ID.ToString(),
                        Message = string.Format("Đã cập nhật từ khóa: <b>{0}</b>", Server.HtmlEncode(tag.Name))
                    };
                    break;
                case ActionType.Delete:
                    ltsTagItems = _tagDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsTagItems)
                    {

                        item.IsDelete = true;
                        stbMessage.AppendFormat("Đã xóa từ khóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));

                    }
                    msg.ID = string.Join(",", ArrID);
                    _tagDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsTagItems = _tagDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && !o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsTagItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị từ khóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _tagDA.Save();
                    msg.ID = string.Join(",", ltsTagItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsTagItems = _tagDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsTagItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn từ khóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _tagDA.Save();
                    msg.ID = string.Join(",", ltsTagItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;
                //case ActionType.Delete:
                //    ltsTagItems = tagDA.getListByArrID(ArrID);
                //    stbMessage = new StringBuilder();
                //    foreach (var item in ltsTagItems)
                //    {
                //        tagDA.DeleteTagToCategoies(item.ID);
                //        item.News_News.Clear();
                //        item.FAQ_Question.Clear();
                //        item.Gallery_Album.Clear();
                //        item.Shop_Product.Clear();
                //        tagDA.Delete(item);

                //        stbMessage.AppendFormat("Đã xóa từ khóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                //    }
                //    msg.ID = string.Join(",", ArrID);
                //    tagDA.Save();
                //    msg.Message = stbMessage.ToString();
                //    break;
            }

            if (string.IsNullOrEmpty(msg.Message))
            {
                msg.Message = "Không có hành động nào được thực hiện.";
                msg.Erros = true;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// add by BienLV 12-04-2014
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AutoCompleteTag(string name)
        {
            var list = new List<TagItem>();
            if (!string.IsNullOrEmpty(name))
                list = _tagDA.GetListSimpleByAutoComplete(name, 10);
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTagToProduct(int tagId, int productId)
        {
            _tagDA.AddTagToProduct(tagId, productId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveTagFromProduct(int tagId, int productId)
        {
            _tagDA.RemoveTagFrompProduct(tagId, productId);
            return Json(true);
        }
    }
}
