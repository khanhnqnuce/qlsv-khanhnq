using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;
using FDI.Web;

namespace FDI.Areas.Admin.Controllers
{
    public class ShopCommentController : BaseController
    {
        readonly Shop_CommentDA _shopCommentDA = new Shop_CommentDA("#");
        //   Shop_CommentReplyDA ShopReplyDA = new Shop_CommentReplyDA("#");
        readonly Shop_CommentLikeDA _shopLikeDA = new Shop_CommentLikeDA();
        readonly CustomerDA _customerDA = new CustomerDA();
        private readonly Shop_Product_VariantDA _productVariantDa = new Shop_Product_VariantDA("#");

        #region bình luận

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>        
        public ActionResult AjaxFormReply()
        {

            var replyModel = new Shop_Comment
            {

                IsActive = true,
                ShopCommentID = Convert.ToInt32(Request["ShopCommentID"])
            };

            if (DoAction == ActionType.Edit)
                replyModel = _shopCommentDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = replyModel;
            ViewBag.ShopComment = _shopCommentDA.GetById(replyModel.ShopCommentID.HasValue ? replyModel.ShopCommentID.Value : replyModel.ID);
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
        public ActionResult ReplyActions()
        {
            var msg = new JsonMessage();
            var shopReply = new Shop_Comment();
            var shopComment = new Shop_Comment();
            List<Shop_Comment> ltsShopReplyItems;

            StringBuilder stbMessage;
            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(shopReply);
                    shopReply.DateCreated = DateTime.Now;
                    shopReply.IsDeleted = false;
                    if (shopReply.ShopCommentID > 0)
                    {
                        if (shopReply.ShopCommentID != null)
                            shopComment = _shopCommentDA.GetById(shopReply.ShopCommentID.Value);
                    }

                    if (shopComment.ID > 0)
                    {
                        if (shopComment.DateCreated != null && Utility.CheckChamTraLoiComment(shopComment.DateCreated.Value,
                                                                                              shopReply.DateCreated))
                        {
                            shopReply.DatKPI = false;
                        }

                        else
                        { 
                            shopComment.DatKPI = true;
                            // ShopCommentDA.Save();
                            
                            shopReply.DatKPI = true;
                        }
                    }
                    else
                    {
                        shopReply.DatKPI = false;
                    }
                    _shopCommentDA.Add(shopReply);
                    _shopCommentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = shopReply.ID.ToString(),
                        Message = string.Format("Đã thêm mới trả lời: <b>{0}</b>", Server.HtmlEncode(shopReply.Title))

                    };
                    break;

                case ActionType.Edit:
                    shopReply = _shopCommentDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(shopReply);
                    shopReply.IsDeleted = false;
                    shopReply.IsActive = Request["IsActive"] != null && Convert.ToBoolean(Request["IsActive"]);
                    _shopCommentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = shopReply.ID.ToString(),
                        Message = string.Format("Đã cập nhật câu trả lời: <b>{0}</b>", Server.HtmlEncode(shopReply.Email))
                    };
                    break;

                case ActionType.Delete:
                    ltsShopReplyItems = _shopCommentDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsShopReplyItems)
                    {
                        item.IsDeleted = true;
                        stbMessage.AppendFormat("Đã xóa câu trả lời {0}", Server.HtmlEncode(item.Email));
                    }
                    _shopCommentDA.Save();
                    msg.ID = string.Join(",", ltsShopReplyItems.Select(o => o.ID));
                    msg.Message = "Xóa câu trả lời thành công";
                    break;

                case ActionType.Show:
                    ltsShopReplyItems = _shopCommentDA.GetListByArrID(ArrID).Where(o => o.IsActive != null && !o.IsActive.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsShopReplyItems)
                    {
                        item.IsActive = true;
                        stbMessage.AppendFormat("Đã hiển thị câu trả lời {0}", Server.HtmlEncode(item.Email));

                    }
                    _shopCommentDA.Save();
                    msg.ID = string.Join(",", ltsShopReplyItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsShopReplyItems = _shopCommentDA.GetListByArrID(ArrID).Where(o => o.IsActive != null && o.IsActive.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsShopReplyItems)
                    {
                        item.IsActive = false;
                        stbMessage.AppendFormat("Đã ẩn câu trả lời {0}", Server.HtmlEncode(item.Email));

                    }
                    _shopCommentDA.Save();
                    msg.ID = string.Join(",", ltsShopReplyItems.Select(o => o.ID));
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
            ViewBag.View = systemActionItem.View;
            var ltsAdmin = _customerDA.GetCustomerIsAdmin();
            ViewBag.ltsAdmin = ltsAdmin;
            return View();
        }

        public ActionResult AutoCustomer()
        {
            string query = Request["query"];
            var ltsResults = _customerDA.GetCustomerByEmail(query, 10);
            var customerItems = ltsResults as List<CustomerItem> ?? ltsResults.ToList();
            var resulValues = new AutoCompleteItem
            {
                query = query,
                data = customerItems.Select(o => o.ID.ToString()).ToList(),
                suggestions = customerItems.Select(o => o.Email).ToList()
            };
            return Json(resulValues, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {

            ViewBag.ViewFull = systemActionItem.ViewFull;
            ViewBag.Edit = systemActionItem.Edit;
            ViewBag.View = systemActionItem.View;
            ViewBag.Delete = systemActionItem.Delete;
            ViewBag.Show = systemActionItem.Show;
            ViewBag.Hide = systemActionItem.Hide;
            ViewBag.Order = systemActionItem.Order;
            ViewBag.Public = systemActionItem.Public;
            ViewBag.Complete = systemActionItem.Complete;
            var emailCustomer = Request["EmailCustomer"];
            //  ViewData.Model = ShopCommentDA.getListSimpleByEmail(Request, EmailCustomer);
            var ltsTemp = new List<int>();
            if (!string.IsNullOrEmpty(Request["Admin"]))
            {
                try
                {
                    ltsTemp = Request["Admin"].Contains(",") ? Request["Admin"].Trim().Split(',').Select(o => Convert.ToInt32(o)).ToList() : new List<int> { Convert.ToInt32(Request["Admin"]) };
                }
                catch (Exception)
                {
                    ltsTemp = new List<int>();
                }
            }
            ViewData.Model = _shopCommentDA.GetListSimpleByRequest(Request, emailCustomer, ltsTemp);
            ViewBag.PageHtml = _shopCommentDA.GridHtmlPage;
            return View();
        }


        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxView()
        {
            var shopCommentModel = _shopCommentDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = shopCommentModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        /// 
        /// 
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewsLike(int ShopCommentID)
        {
            int afterLike;
            var likeComment = _shopLikeDA.GetByCommentId(ShopCommentID);
            if (likeComment != null)
            {
                afterLike = likeComment.Like + 1;
                likeComment.Like = afterLike;
                UpdateModel(likeComment);
                _shopLikeDA.Save();
            }
            else
            {
                likeComment = new Shop_CommentLike { Like = 1 };
                afterLike = likeComment.Like;
                likeComment.ShopCommentID = ShopCommentID;
                _shopLikeDA.Add(likeComment);
                _shopLikeDA.Save();
            }

            return Json(new { Like = afterLike }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsUnLike(int ShopCommentID)
        {
            var likeComment = _shopLikeDA.GetByCommentId(ShopCommentID);
            var afterLike = 0;
            UpdateModel(likeComment);
            if (likeComment != null && likeComment.Like > 0)
            {
                afterLike = likeComment.Like - 1;
                likeComment.Like = afterLike;
            }
            _shopLikeDA.Save();
            return Json(new { Like = afterLike }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewsReplyLike(int ShopCommentID)
        {
            int afterLike;
            var likeComment = _shopLikeDA.GetByCommentReplyId(ShopCommentID);
            if (likeComment != null)
            {
                afterLike = likeComment.Like + 1;
                likeComment.Like = afterLike;
                UpdateModel(likeComment);
                _shopLikeDA.Save();
            }
            else
            {
                likeComment = new Shop_CommentLike { Like = 1 };
                afterLike = likeComment.Like;
                likeComment.ShopCommentReplyID = ShopCommentID;
                _shopLikeDA.Add(likeComment);
                _shopLikeDA.Save();
            }

            return Json(new { Like = afterLike }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsReplyUnLike(int ShopCommentID)
        {
            var likeComment = _shopLikeDA.GetByCommentReplyId(ShopCommentID);
            var afterLike = 0;

            if (likeComment != null && likeComment.Like > 0)
            {
                UpdateModel(likeComment);
                afterLike = likeComment.Like - 1;
                likeComment.Like = afterLike;
            }
            _shopLikeDA.Save();
            return Json(new { Like = afterLike }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxForm()
        {
            var shopCommentModel = new Shop_Comment
            {
                IsActive = true,
            };

            if (DoAction == ActionType.Edit)
                shopCommentModel = _shopCommentDA.GetById(ArrID.FirstOrDefault());
            ViewBag.Product = _productVariantDa.GetListProduct();
            ViewData.Model = shopCommentModel;
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
            var shopComment = new Shop_Comment();
            var shopCommentParent = new Shop_Comment();
            List<Shop_Comment> ltsCommentItems;
            StringBuilder stbMessage;
            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(shopComment);

                    shopComment.DateCreated = DateTime.Now;
                    shopComment.IsDeleted = false;
                    if (shopComment.ShopCommentID > 0)
                    {
                        if (shopComment.ShopCommentID != null)
                            shopCommentParent = _shopCommentDA.GetById(shopComment.ShopCommentID.Value);
                    }
                    if (shopCommentParent.ID > 0)
                    {
                        if (shopCommentParent.DateCreated != null && Utility.CheckChamTraLoiComment(shopCommentParent.DateCreated.Value,
                                                                                                    shopComment.DateCreated))
                        {
                            shopComment.DatKPI = false;
                        }
                        else
                        {
                            if (shopCommentParent.DatKPI.HasValue)
                            {
                                if (!shopCommentParent.DatKPI.Value)
                                {
                                    shopCommentParent.DatKPI = true;
                                    _shopCommentDA.Save();
                                }
                            }
                            shopComment.DatKPI = true;
                        }
                    }
                    else
                    {
                        shopComment.DatKPI = false;
                    }
                    _shopCommentDA.Add(shopComment);
                    _shopCommentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = shopComment.ID.ToString(),
                        Message = string.Format("Đã thêm mới bình luận: <b>{0}</b>", Server.HtmlEncode(shopComment.Title))
                    };
                    break;

                case ActionType.Edit:
                    shopComment = _shopCommentDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(shopComment);

                    shopComment.DateModified = DateTime.Now;
                    shopComment.IsActive = Request["IsActive"] != null && Convert.ToBoolean(Request["IsActive"]);

                    _shopCommentDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = shopComment.ID.ToString(),
                        Message = string.Format("Đã cập nhật bình luận: <b>{0}</b>", Server.HtmlEncode(shopComment.Title))
                    };
                    break;

                case ActionType.Delete:
                    ltsCommentItems = _shopCommentDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCommentItems)
                    {
                        item.IsDeleted = true;
                        stbMessage.AppendFormat("Đã xóa bình luận <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _shopCommentDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsCommentItems = _shopCommentDA.GetListByArrID(ArrID).Where(o => o.IsActive != null && !o.IsActive.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị

                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCommentItems)
                    {
                        item.IsActive = true;
                        stbMessage.AppendFormat("Đã hiển thị bình luận <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _shopCommentDA.Save();
                    msg.ID = string.Join(",", ltsCommentItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsCommentItems = _shopCommentDA.GetListByArrID(ArrID).Where(o => o.IsActive != null && o.IsActive.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị

                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCommentItems)
                    {
                        item.IsActive = false;
                        stbMessage.AppendFormat("Đã ẩn bình luận <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                    }
                    _shopCommentDA.Save();
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
        /// Convert from lookupdata to table
        /// </summary>
        /// <param name="LtsLookupData"></param>
        /// <returns></returns>
        public DataTable ConvertLookupDataToTable(List<ShopCommentItem> LtsLookupData)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("SanPham", typeof(string));
            dataTable.Columns.Add("KhachHang", typeof(string));

            dataTable.Columns.Add("TinhTrangPheDuyet", typeof(string));
            dataTable.Columns.Add("DuocTaoVao", typeof(string));
            dataTable.Columns.Add("DuocDuyetVao", typeof(string));
            dataTable.Columns.Add("ThoiGianChoDuyet", typeof(string));
            dataTable.Columns.Add("AdminPhuTrach", typeof(string));
            int i = 0;
            foreach (ShopCommentItem objShopCommentItem in LtsLookupData)
            {
                i++;
                DataRow dataRow = dataTable.NewRow();
                dataRow["STT"] = i;
                dataRow["SanPham"] = objShopCommentItem.Product.Name;
                dataRow["KhachHang"] = objShopCommentItem.Email;
                dataRow["TinhTrangPheDuyet"] = objShopCommentItem.IsActive != null && (objShopCommentItem.IsActive.Value) ? "Đã duyệt" : "Chưa duyệt";
                dataRow["DuocTaoVao"] = objShopCommentItem.DateCreated;
                dataRow["DuocDuyetVao"] = objShopCommentItem.NgayTraLoi;
                if (objShopCommentItem.DateCreated != null && Utility.CheckChamTraLoiComment(objShopCommentItem.DateCreated.Value, objShopCommentItem.NgayTraLoi))
                {
                    dataRow["ThoiGianChoDuyet"] = "Không đạt";
                }
                else
                {
                    dataRow["ThoiGianChoDuyet"] = "Đạt";
                }
                // dataRow["ThoiGianChoDuyet"] = objShopCommentItem.DateIsActive;
                dataRow["AdminPhuTrach"] = objShopCommentItem.NguoiDuyet;
                dataTable.Rows.Add(dataRow);

            }
            return dataTable;
        }
        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["term"];
            var ltsResults = _shopCommentDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

    }
}
