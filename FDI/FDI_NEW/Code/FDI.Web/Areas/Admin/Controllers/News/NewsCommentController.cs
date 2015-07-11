using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class NewsCommentController : BaseController
    {
        readonly News_CommentDA _commentDA = new News_CommentDA("#");
        readonly CustomerDA _customerDA = new CustomerDA("#");
        readonly News_LikeDA _likeDA = new News_LikeDA();
        #region bình luận

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>        
        public ActionResult AjaxFormReply()
        {
            var replyModel = new News_Comment
            {

                IsShow = true,
                ID = Convert.ToInt32(Request["CommentID"])
            };

            if (DoAction == ActionType.Edit)
                replyModel = _commentDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = replyModel;
            ViewBag.NewsComment = _commentDA.GetById(replyModel.NewsCommentID.HasValue ? replyModel.NewsCommentID.Value : replyModel.ID);
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            ViewBag.HoTen = "";
            ViewBag.Email = "";
            if (User.Identity.IsAuthenticated)
            {
                //var nameu = User.Identity.Name;
                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                {
                    ViewBag.HoTen = membershipUser.UserName;
                    ViewBag.Email = membershipUser.Email;
                }
            }
            CustomerItem objcustomer = _customerDA.GetCustomerItemByEmail(ViewBag.Email);
            if (objcustomer != null)
                ViewBag.Customer = objcustomer;
            return View();

        }


        /// <summary>
        /// Hứng các giá trị, phục vụ cho thêm, sửa, xóa, ẩn, hiện
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReplyActions()
        {
            var msg = new JsonMessage();
            var reply = new News_Comment();
            List<News_Comment> ltsReplyItems;
            StringBuilder stbMessage;
            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(reply);
                    reply.DateCreated = DateTime.Now;
                    reply.IsDeleted = false;
                    _commentDA.Add(reply);
                    _commentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = reply.ID.ToString(),
                        Message = string.Format("Đã thêm mới trả lời: <b>{0}</b>", Server.HtmlEncode(reply.Title))

                    };
                    break;

                case ActionType.Edit:
                    reply = _commentDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(reply);
                    reply.IsShow = Request["IsShow"] != null && Convert.ToBoolean(Request["IsShow"]);
                    _commentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = reply.ID.ToString(),
                        Message = string.Format("Đã cập nhật câu trả lời: <b>{0}</b>", Server.HtmlEncode(reply.Email))
                    };
                    break;

                case ActionType.Delete:
                    ltsReplyItems = _commentDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsReplyItems)
                    {
                        item.IsDeleted = true;
                        stbMessage.AppendFormat("Đã xóa câu trả lời {0}", Server.HtmlEncode(item.Email));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _commentDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsReplyItems = _commentDA.GetListByArrID(ArrID).Where(o => !o.IsShow.HasValue).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsReplyItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị câu trả lời {0}", Server.HtmlEncode(item.Email));

                    }

                    _commentDA.Save();
                    msg.ID = string.Join(",", ltsReplyItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsReplyItems = _commentDA.GetListByArrID(ArrID).Where(o => o.IsShow.HasValue).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsReplyItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn câu trả lời {0}", Server.HtmlEncode(item.Email));

                    }
                    _commentDA.Save();
                    msg.ID = string.Join(",", ltsReplyItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;
            }

            if (!string.IsNullOrEmpty(msg.Message)) return Json(msg, JsonRequestBehavior.AllowGet);
            msg.Message = "Không có hành động nào được thực hiện.";
            msg.Erros = true;

            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        #endregion

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Add = systemActionItem.Add;
            return View();
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var emailCustomer = Request["EmailCustomer"];
            var listItem = _commentDA.GetListSimpleByRequest(Request, emailCustomer);
            var model = new ModelNewsCommentItem
            {
                SystemActionItem = systemActionItem,

                ListItem = listItem
            };
            ViewData.Model = model;

            ViewBag.PageHtml = _commentDA.GridHtmlPage;
            return View();
        }

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxView()
        {
            var commentModel = _commentDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = commentModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewsLike(int CommentID)
        {
            int afterLike;
            var likeComment = _likeDA.GetByCommentId(CommentID);
            if (likeComment != null)
            {
                afterLike = likeComment.Like + 1;
                likeComment.Like = afterLike;
                UpdateModel(likeComment);
                _likeDA.Save();
            }
            else
            {
                likeComment = new Comment_Like { Like = 1 };
                afterLike = likeComment.Like;
                likeComment.CommentID = CommentID;
                _likeDA.Add(likeComment);
                _likeDA.Save();
            }

            return Json(new { Like = afterLike }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsUnLike(int commentID)
        {
            var likeComment = _likeDA.GetByCommentId(commentID);
            var afterLike = 0;
            UpdateModel(likeComment);
            if (likeComment != null && likeComment.Like > 0)
            {
                afterLike = likeComment.Like - 1;
                likeComment.Like = afterLike;
            }
            //else if (likeComment == null)
            //{
            //    likeComment.Like = 0;
            //}

            _likeDA.Save();
            return Json(new { Like = afterLike }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxForm()
        {
            var commentModel = new News_Comment
            {
                IsShow = true,
            };

            if (DoAction == ActionType.Edit)
                commentModel = _commentDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = commentModel;
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
            var comment = new News_Comment();

            List<News_Comment> ltsCommentItems;
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
                    UpdateModel(comment);

                    comment.DateCreated = DateTime.Now;
                    comment.IsDeleted = false;
                    _commentDA.Add(comment);
                    _commentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = comment.ID.ToString(),
                        Message = string.Format("Đã thêm mới bình luận: <b>{0}</b>", Server.HtmlEncode(comment.Title))
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
                    comment = _commentDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(comment);
                    comment.DateCreated = DateTime.Now;

                    comment.IsShow = Request["IsShow"] != null && Convert.ToBoolean(Request["IsShow"]);

                    _commentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = comment.ID.ToString(),
                        Message = string.Format("Đã cập nhật bình luận: <b>{0}</b>", Server.HtmlEncode(comment.Title))
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
                    ltsCommentItems = _commentDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCommentItems)
                    {
                        item.IsDeleted = true;
                        stbMessage.AppendFormat("Đã xóa bình luận <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _commentDA.Save();
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
                    ltsCommentItems = _commentDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && !o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị

                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCommentItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị bình luận <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _commentDA.Save();
                    msg.ID = string.Join(",", ltsCommentItems.Select(o => o.ID));
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
                    ltsCommentItems = _commentDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị

                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCommentItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn bình luận <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _commentDA.Save();
                    msg.ID = string.Join(",", ltsCommentItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;
            }

            if (!string.IsNullOrEmpty(msg.Message)) return Json(msg, JsonRequestBehavior.AllowGet);
            msg.Message = "Không có hành động nào được thực hiện.";
            msg.Erros = true;

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["term"];
            var ltsResults = _commentDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
