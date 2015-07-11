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
    public class FAQQuestionController : BaseController
    {
        readonly FAQQuestionDA _questionDA = new FAQQuestionDA("#");
        readonly FAQ_CategoryDA _categoryDA = new FAQ_CategoryDA();
        readonly FAQ_AnswerDA _answerDA = new FAQ_AnswerDA("#");


        #region dành cho câu hỏi

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>        
        public ActionResult AjaxFormAnswer()
        {
            var answerModel = new FAQ_Answer
            {
                IsShow = true,
                QuestionID = Convert.ToInt32(Request["QuestionID"])
            };

            if (DoAction == ActionType.Edit)
                answerModel = _answerDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = answerModel;
            ViewBag.FAQ_Question = _questionDA.GetById(answerModel.QuestionID);
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
        public ActionResult AnswerActions()
        {
            var msg = new JsonMessage();
            var answer = new FAQ_Answer();
            List<FAQ_Answer> ltsAnswerItems;
            StringBuilder stbMessage;
            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(answer);
                    answer.TitleAscii = MyString.Slug(answer.Title);
                    answer.DateCreated = DateTime.Now;
                    _answerDA.Add(answer);
                    _answerDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = answer.ID.ToString(),
                        Message = string.Format("Đã thêm mới câu trả lời: <b>{0}</b>", Server.HtmlEncode(answer.Title))
                    };
                    break;

                case ActionType.Edit:
                    answer = _answerDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(answer);
                    answer.TitleAscii = MyString.Slug(answer.Title);

                    _answerDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = answer.ID.ToString(),
                        Message = string.Format("Đã cập nhật câu trả lời: <b>{0}</b>", Server.HtmlEncode(answer.Title))
                    };
                    break;

                case ActionType.Delete:
                    ltsAnswerItems = _answerDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAnswerItems)
                    {
                        _answerDA.Delete(item);
                        stbMessage.AppendFormat("Đã xóa câu trả lời <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _answerDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsAnswerItems = _answerDA.GetListByArrID(ArrID).Where(o => !o.IsShow).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAnswerItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị câu trả lời <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _answerDA.Save();
                    msg.ID = string.Join(",", ltsAnswerItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsAnswerItems = _answerDA.GetListByArrID(ArrID).Where(o => o.IsShow).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAnswerItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn câu trả lời <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _answerDA.Save();
                    msg.ID = string.Join(",", ltsAnswerItems.Select(o => o.ID));
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

        #endregion

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
            var listFAQQuestionItem = _questionDA.GetListSimpleByRequest(Request);
            var model = new ModelFAQQuestionItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listFAQQuestionItem,
                PageHtml = _questionDA.GridHtmlPage
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
            var questionModel = _questionDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = questionModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        /// 

        //[ValidateInput(false)]
        public ActionResult AjaxForm()
        {
            var questionModel = new FAQ_Question
            {
                IsShow = true
            };

            if (DoAction == ActionType.Edit)
                questionModel = _questionDA.GetById(ArrID.FirstOrDefault());

            ViewBag.CategoryID = _categoryDA.GetAllListSimple();
            ViewData.Model = questionModel;
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
            var question = new FAQ_Question();
            List<FAQ_Question> ltsQuestionItems;
            StringBuilder stbMessage;
            switch (DoAction)
            {
                case ActionType.Add:
                    if (!systemActionItem.Add)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    UpdateModel(question);
                    question.TitleAscii = MyString.Slug(question.Title);
                    question.DateCreated = DateTime.Now;
                    _questionDA.Add(question);
                    _questionDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = question.ID.ToString(),
                        Message = string.Format("Đã thêm mới câu hỏi: <b>{0}</b>", Server.HtmlEncode(question.Title))
                    };
                    break;

                case ActionType.Edit:
                    if (!systemActionItem.Edit)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    question = _questionDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(question);
                    question.IsShow = Request["IsShow"] != null && Convert.ToBoolean(Request["IsShow"]);
                    question.TitleAscii = MyString.Slug(question.Title);
                    _questionDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = question.ID.ToString(),
                        Message = string.Format("Đã cập nhật câu hỏi: <b>{0}</b>", Server.HtmlEncode(question.Title))
                    };
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
                    ltsQuestionItems = _questionDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsQuestionItems)
                    {
                        if (item.FAQ_Answer.Count > 0)
                        {
                            stbMessage.AppendFormat("Đã có câu trả lời trong <b>{0}</b>.<br /> nên không thể xóa", Server.HtmlEncode(item.Title));
                        }
                        else
                        {
                            _questionDA.Delete(item);
                            stbMessage.AppendFormat("Đã xóa câu hỏi <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _questionDA.Save();
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
                    ltsQuestionItems = _questionDA.GetListByArrID(ArrID).Where(o => !o.IsShow).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsQuestionItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị câu hỏi <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _questionDA.Save();
                    msg.ID = string.Join(",", ltsQuestionItems.Select(o => o.ID));
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
                    ltsQuestionItems = _questionDA.GetListByArrID(ArrID).Where(o => o.IsShow).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsQuestionItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn câu hỏi <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _questionDA.Save();
                    msg.ID = string.Join(",", ltsQuestionItems.Select(o => o.ID));
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
            var ltsResults = _questionDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
