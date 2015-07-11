using System;
using System.Collections.Generic;
using FDI.Entities;
using FDI.Filters;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    [LogActionFilter]
    public class ProductController : BaseController
    {
        readonly Shop_ProductDA _productDA = new Shop_ProductDA("#");
        readonly Shop_Product_RefDA _productRefDa = new Shop_Product_RefDA("#");
        readonly Shop_Product_Compatible_AccessoriesDA _productAccessoriesDa = new Shop_Product_Compatible_AccessoriesDA("#");
        readonly Shop_BrandDA _brandDA = new Shop_BrandDA("#");
        readonly Shop_ProductTypeDA _productTypeDA = new Shop_ProductTypeDA("#");
        readonly Shop_LabelDA _labelDA = new Shop_LabelDA("#");
        readonly Shop_CategoryDA _categoryDa = new Shop_CategoryDA("#");
        readonly Shop_ProductGroupAttributeDA _groupAttributeDa = new Shop_ProductGroupAttributeDA("#");
        readonly Shop_ProductAttributeDA _attributeDa = new Shop_ProductAttributeDA("#");
        readonly Shop_Product_Search_MappingDA _shopProductSearchMappingDa = new Shop_Product_Search_MappingDA("#");

        public ActionResult Index()
        {

            var model = new ModelProductBrandItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _productDA.GetListBrand(),
                ListProductTypeItem = _productDA.GetListProductType()
            };


            return View(model);
        }

        public ActionResult ListItems()
        {
            var model = new ModelProductItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = _productDA.GetListSimpleByRequest(Request),
                PageHtml = _productDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxView()
        {
            var productModel = _productDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = productModel;
            return View();
        }

        public ActionResult AjaxForm()
        {
            var productModel = new Shop_Product();

            if (DoAction == ActionType.Edit)
                productModel = _productDA.GetById(ArrID.FirstOrDefault());

            ViewBag.BrandType = _brandDA.GetListSimpleAll();
            ViewBag.ProductType = _productTypeDA.GetListSimpleAll();

            var ltsProductCategorySource = _categoryDa.GetListSimpleAll(false);
            ViewBag.CategoryID = _categoryDa.GetAllSelectList(ltsProductCategorySource, 0, false);

            ViewBag.LabelType = _labelDA.GetListSimpleAll();

            ViewData.Model = productModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        public ActionResult ProductAttribute(int productId, int productTypeId)
        {
            var list = _attributeDa.GetListAttributeByType(productTypeId);
            ViewData["ListAttribute"] = _attributeDa.GetAttributeProduct(productId);
            ViewBag.ProductID = productId;
            return View(list);
        }

        public ActionResult ProductImages(int productId)
        {
            ViewBag.ProductID = productId;
            var product = _productDA.GetOnlyPictures(productId);
            return View(product);
        }

        public ActionResult ProductRef(int productId)
        {
            ViewBag.ProductID = productId;
            ViewBag.BrandID = _productDA.GetListBrand();
            ViewBag.ProductTypeID = _productDA.GetListProductType(); ;
            var product = _productRefDa.GetByProductId(productId);
            return View(product);
        }

        public ActionResult ProductAccessories(int productId)
        {
            ViewBag.ProductID = productId;
            ViewBag.BrandID = _productDA.GetListBrand();
            ViewBag.ProductTypeID = _productDA.GetListProductType(); ;
            var product = _productAccessoriesDa.GetByProductId(productId);
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProductCopy()
        {
            var msg = new JsonMessage();
            var product = new Shop_Product();


            try
            {
                var pictureId = Convert.ToInt32(Request["Value_DefaultImages"]);
                var pictureHotId = Convert.ToInt32(Request["Value_DefaultImagesHot"]);
                var productTypeID = Convert.ToInt32(Request["selectProductType"]);
                var idValues = MyBase.ConvertStringToListInt(Request["Value_selectProductType"]);

                #region properties
                product.Name = Convert.ToString(product.Name);
                product.NameAscii = String.IsNullOrEmpty(product.Name) == false ? MyString.Slug(product.Name) : string.Empty;
                product.PictureHotID = pictureHotId;

                product.IsShow = Request["IsShow"] != null && Request["IsShow"] != string.Empty;
                product.IsSlide = Request["IsSlide"] != null && Request["IsSlide"] != string.Empty;
                product.IsHot = Request["IsHot"] != null && Request["IsHot"] != string.Empty;
                product.IsShowOnHomePage = Request["IsShowOnHomePage"] != null && Request["IsShowOnHomePage"] != string.Empty;
                product.IsAllowRating = Request["IsAllowRating"] != null && Request["IsAllowRating"] != string.Empty;
                product.IsAllowComment = Request["IsAllowComment"] != null && Request["IsAllowComment"] != string.Empty;
                product.IsApplyNewTemplate = Request["IsApplyNewTemplate"] != null && Request["IsApplyNewTemplate"] != string.Empty;

                product.Description = Convert.ToString(product.Description);
                product.IncludeInfo = Convert.ToString(product.IncludeInfo);
                product.Overview = Convert.ToString(product.Overview);
                //product.OverviewVideo = ConvertUtil.ToString(product.OverviewVideo);
                //product.Overview360 = ConvertUtil.ToString(product.Overview360);
                product.VideoDetail = Convert.ToString(product.VideoDetail);
                product.VideoRating = Convert.ToString(product.VideoRating);
                product.Details = Convert.ToString(product.Details);
                product.InfoDriver = Convert.ToString(product.InfoDriver);
                product.DisplayOrder = Convert.ToInt32(product.DisplayOrder);
                product.SizeHeight = Convert.ToDecimal(product.SizeHeight);
                product.SizeLength = Convert.ToDecimal(product.SizeLength);
                product.SizeWidth = Convert.ToDecimal(product.SizeWidth);
                product.Weight = Convert.ToDecimal(product.Weight);
                product.Warranty = Convert.ToString(product.Warranty);

                product.CreatedOnUtc = DateTime.Now;
                product.UpdatedOnUtc = DateTime.Now;
                product.StockTypeID = 0;
                product.IsDelete = false;
                product.ProductTypeID = productTypeID;
                product.Viewed = 0;
                #endregion

                #region Shop_Category
                var idValuesCategory = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                var categoriesSelected = _productDA.GetListCategoryByArrID(idValuesCategory);
                foreach (var item in categoriesSelected)
                {
                    product.Shop_Category.Add(item);
                }
                #endregion

                #region Gallery_Picture
                if (pictureId > 0)
                {
                    product.PictureID = pictureId;
                    var galleryPicturesSelected = _productDA.GetListPictureByID(pictureId);
                    foreach (var item in galleryPicturesSelected)
                    {
                        product.Gallery_Picture1.Add(item);
                    }
                }
                #endregion

                #region picture 360
                var idValuesPictures360 = MyBase.ConvertStringToListInt(Request["Value_Images360"]);
                var galleryPictures360Selected = _productDA.GetListPictureByArrID(idValuesPictures360);
                foreach (var item in galleryPictures360Selected)
                {
                    product.Gallery_Picture2.Add(item);
                }
                #endregion

                #region gallery picture
                var idValuesGalleryPicture = MyBase.ConvertStringToListInt(Request["Value_ImagesGalleryPicture"]);
                var galleryPicturesSlideSelected = _productDA.GetListPictureByArrID(idValuesGalleryPicture);
                foreach (var item in galleryPicturesSlideSelected)
                {
                    product.Gallery_Picture3.Add(item);
                }
                #endregion

                #region gallery picture detail
                var idValuesGalleryPictureDetail = MyBase.ConvertStringToListInt(Request["Value_ImagesGalleryPictureDetail"]);
                var galleryPicturesDetailSelected = _productDA.GetListPictureByArrID(idValuesGalleryPictureDetail);
                foreach (var item in galleryPicturesDetailSelected)
                {
                    product.Gallery_Picture3.Add(item);
                }
                #endregion

                #region System_Tag
                var idValuesTag = MyBase.ConvertStringToListString(Request["NewsTag"]); // system_tag
                var tagSelected = _productDA.GetListTagByArrTag(idValuesTag);
                foreach (var tag in tagSelected)
                {
                    product.System_Tag.Add(tag);
                }
                #endregion

                #region System_File
                foreach (var fileItem in ListFileUpload)
                {
                    product.System_File.Add(_productDA.GetFileData(fileItem));
                }
                #endregion

                product.CreateBy = User.Identity.Name;
                product.UpdateBy = User.Identity.Name;

                _productDA.Add(product);
                _productDA.Save();

                #region Shop_Product_Attribute_Specification
                var attributeSpecificationSelected = _productDA.GetListProductAttributeSpecificationByArrID(idValues);
                foreach (var item in attributeSpecificationSelected)
                {
                    product.Shop_Product_Attribute_Specification.Add(item);
                }
                _productDA.Save();
                #endregion

                msg = new JsonMessage
                {
                    Erros = false,
                    ID = product.ID.ToString(),
                    Message = string.Format("Đã thêm mới sản phẩm: <b>{0}</b>", Server.HtmlEncode(product.Name))
                };
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            if (string.IsNullOrEmpty(msg.Message))
            {
                msg.Message = "Không có hành động nào được thực hiện.";
                msg.Erros = true;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var product = new Shop_Product();
            List<Shop_Product> ltsproductItems;
            List<Gallery_Picture> galleryPicturesSelected;
            List<Shop_Category> categoriesSelected;
            StringBuilder stbMessage;
            List<int> idValuesCategory;
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

                        UpdateModel(product);

                        var pictureId = Convert.ToInt32(Request["Value_DefaultImages"]);

                        #region properties
                        //product.Name = Convert.ToString(product.Name);

                        product.NameAscii = !string.IsNullOrEmpty(product.Name) ? MyString.Slug(product.Name) : string.Empty;

                        if (!string.IsNullOrEmpty(Request["Value_DefaultImagesHot"]))
                            product.PictureHotID = Convert.ToInt32(Request["Value_DefaultImagesHot"]);

                        //product.PictureHotID = pictureHotId;

                        //product.IsShow = Request["IsShow"] != null && Request["IsShow"] != string.Empty;
                        //product.IsSlide = Request["IsSlide"] != null && Request["IsSlide"] != string.Empty;
                        //product.IsHot = Request["IsHot"] != null && Request["IsHot"] != string.Empty;
                        //product.IsShowOnHomePage = Request["IsShowOnHomePage"] != null && Request["IsShowOnHomePage"] != string.Empty;
                        //product.IsAllowRating = Request["IsAllowRating"] != null && Request["IsAllowRating"] != string.Empty;
                        //product.IsAllowComment = Request["IsAllowComment"] != null && Request["IsAllowComment"] != string.Empty;
                        //product.IsApplyNewTemplate = Request["IsApplyNewTemplate"] != null && Request["IsApplyNewTemplate"] != string.Empty;

                        //product.ShortDescription = Convert.ToString(product.ShortDescription);
                        //product.HightlightsDes = Convert.ToString(product.HightlightsDes);
                        //product.Description = Convert.ToString(product.Description);
                        //product.IncludeInfo = Convert.ToString(product.IncludeInfo);
                        //product.Overview = Convert.ToString(product.Overview);
                        //product.HightlightsShortDes = Convert.ToString(product.HightlightsShortDes);
                        //product.AttachedAccessories = Convert.ToString(product.AttachedAccessories);
                        //product.OverviewVideo = Convert.ToString(product.OverviewVideo);
                        //product.Overview360 = Convert.ToString(product.Overview360);
                        //product.VideoDetail = Convert.ToString(product.VideoDetail);
                        //product.VideoRating = Convert.ToString(product.VideoRating);
                        //product.Guide = Convert.ToString(product.Guide);
                        //product.Details = Convert.ToString(product.Details);
                        //product.InfoDriver = Convert.ToString(product.InfoDriver);
                        //product.DisplayOrder = Convert.ToInt32(product.DisplayOrder);
                        //product.SizeHeight = Convert.ToDecimal(product.SizeHeight);
                        //product.SizeLength = Convert.ToDecimal(product.SizeLength);
                        //product.SizeWidth = Convert.ToDecimal(product.SizeWidth);
                        //product.Weight = Convert.ToDecimal(product.Weight);
                        //product.Warranty = Convert.ToString(product.Warranty);

                        product.CreatedOnUtc = DateTime.Now;
                        product.UpdatedOnUtc = DateTime.Now;
                        product.StockTypeID = 0;
                        product.IsDelete = false;
                        //product.ProductTypeID = productTypeID;
                        product.Viewed = 0;
                        product.CreateBy = User.Identity.Name;
                        product.UpdateBy = User.Identity.Name;

                        _productDA.Add(product);

                        //_productDA.Save();
                        //product.DisplayOrderSearch = Convert.ToInt32(product.DisplayOrderSearch);
                        //product.PromotionInfo = Convert.ToString(product.PromotionInfo);
                        #endregion

                        #region Shop_Category
                        idValuesCategory = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                        categoriesSelected = _productDA.GetListCategoryByArrID(idValuesCategory);
                        foreach (var item in categoriesSelected)
                        {
                            product.Shop_Category.Add(item);
                        }
                        #endregion

                        #region Gallery_Picture
                        if (pictureId > 0)
                        {
                            product.PictureID = pictureId;
                            galleryPicturesSelected = _productDA.GetListPictureByID(pictureId);
                            foreach (var item in galleryPicturesSelected)
                            {
                                product.Gallery_Picture1.Add(item);
                                _productDA.Save();
                            }
                        }
                        #endregion

                        #region System_Tag

                        var idValues = MyBase.ConvertStringToListInt(Request["TagValues"]);
                        var tagSelected = _productDA.GetListIntTagByArrID(idValues);
                        foreach (var tag in tagSelected)
                        {
                            tag.IsDelete = false;
                            tag.IsShow = true;
                            tag.NameAscii = MyString.Slug(tag.Name);
                            product.System_Tag.Add(tag);
                        }

                        #endregion

                        #region System_File
                        foreach (var fileItem in ListFileUpload)
                        {
                            product.System_File.Add(_productDA.GetFileData(fileItem));
                        }
                        #endregion

                        product.CreateBy = User.Identity.Name;
                        product.UpdateBy = User.Identity.Name;
                        _productDA.Save();
                        try
                        {
                            var shopProductSearchMapping = new Shop_Product_Search_Mapping
                            {
                                ProductID = product.ID,
                                Description = product.NameAscii.Replace("-", " ")
                            };
                            _shopProductSearchMappingDa.Add(shopProductSearchMapping);
                            _shopProductSearchMappingDa.Save();

                        }
                        catch { }

                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = product.ID.ToString(),
                            Message = string.Format("Đã thêm mới sản phẩm: <b>{0}</b>", Server.HtmlEncode(product.Name))
                        };
                    }
                    catch (Exception)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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

                        product = _productDA.GetById(ArrID.FirstOrDefault());
                        //List<int> IDValuesEdit = FDIUtils.getValuesArray(Request["Value_selectProductType"]);
                        //var pictureIdEdit = string.IsNullOrEmpty(Request["Value_DefaultImages"]) ? product.PictureID : Convert.ToInt32(Request["Value_DefaultImages"]);
                        var pictureHotIdEdit = string.IsNullOrEmpty(Request["Value_DefaultImagesHot"]) ? product.PictureHotID : Convert.ToInt32(Request["Value_DefaultImagesHot"]);

                        UpdateModel(product);

                        #region properties
                        product.Name = Convert.ToString(product.Name);
                        //product.NameAscii = String.IsNullOrEmpty(product.Name) == false ?  Utils.MyString.Slug(product.Name) : string.Empty;

                        product.PictureHotID = pictureHotIdEdit;
                        product.UpdatedOnUtc = DateTime.Now;
                        product.StockTypeID = 0;
                        product.IsDelete = false;
                        //product.ProductTypeID = Request["selectProductType"] == "" ? product.ProductTypeID : Convert.ToInt32(Request["selectProductType"]);
                        product.Viewed = 0;

                        product.DisplayOrderSearch = Convert.ToInt32(product.DisplayOrderSearch);
                        product.PromotionInfo = Convert.ToString(product.PromotionInfo);
                        #endregion

                        #region brand
                        //product.BrandID = Request["Value_brandID"] == "" ? product.BrandID : Convert.ToInt32(Request["Value_brandID"]);

                        int brandId = Convert.ToInt32(Request["Value_brandID"]);
                        if (product.BrandID == null)
                        {
                            product.BrandID = brandId;
                        }
                        else
                        {
                            product.BrandID = brandId == product.BrandID ? brandId : product.BrandID;
                        }
                        #endregion

                        #region label
                        int labelId = Convert.ToInt32(Request["Value_LabelID"]);
                        if (product.LabelID == null)
                        {
                            product.LabelID = labelId;
                        }
                        else
                        {
                            product.LabelID = labelId == product.LabelID ? labelId : product.LabelID;
                        }
                        #endregion

                        product.UpdateBy = User.Identity.Name;

                        _productDA.Save();

                        #region Shop_Category
                        product.Shop_Category.Clear();
                        idValuesCategory = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                        categoriesSelected = _productDA.GetListCategoryByArrID(idValuesCategory);
                        foreach (var item in categoriesSelected)
                        {
                            product.Shop_Category.Add(item);
                            _productDA.Save();
                        }

                        //if (Request["CategoryID"] != "")
                        //{
                        //    product.Shop_Category.Clear();
                        //    categoriesSelected = productDA.getListCategoryByID(Convert.ToInt32(Request["CategoryID"]));
                        //    foreach (var item in categoriesSelected)
                        //    {
                        //        product.Shop_Category.Add(item);
                        //    }
                        //}

                        #endregion

                        #region Gallery_Picture
                        if (Request["Value_DefaultImages"] != null && Convert.ToInt32(Request["Value_DefaultImages"]) > 0)
                        {
                            product.PictureID = Convert.ToInt32(Request["Value_DefaultImages"]);
                            galleryPicturesSelected = _productDA.GetListPictureByID(Convert.ToInt32(Request["Value_DefaultImages"]));
                            foreach (var item in galleryPicturesSelected)
                            {
                                product.Gallery_Picture1.Add(item);
                                _productDA.Save();
                            }
                        }
                        #endregion

                        #region thêm và xóa tag
                        //product.System_Tag.Clear();
                        // add tags mới
                        //IDValuesTag =  Utils.getValuesArrayForTag(Request["NewsTag"]);
                        //tagSelected = productDA.getListTagByArrTag(IDValuesTag);
                        //foreach (var tag in tagSelected)
                        //{
                        //    product.System_Tag.Add(tag);

                        //}

                        // add lại tất cả tags (tags mới + tags đã có)
                        //IDValues =  Utils.getEditValuesArrayTag(Request["TagValues"]);
                        //tagSelected = productDA.getListIntTagByArrID(IDValues);
                        //foreach (var tag in tagSelected)
                        //{
                        //    product.System_Tag.Add(tag);

                        //}

                        //product.System_Tag.Clear();
                        //IDValuesTag =  Utils.getValuesArrayTag(Request["NewsTag"]);
                        //tagSelected = productDA.getListTagByArrTag(IDValuesTag);
                        //foreach (var tag in tagSelected)
                        //{
                        //    tag.IsDeleted = false;
                        //    tag.IsShow = true;
                        //    tag.NameAscii =  Utils.MyString.Slug(tag.Name);
                        //    product.System_Tag.Add(tag);
                        //}
                        //IDValues =  Utils.getEditValuesArrayTagNews(Request["TagValues"]);
                        //tagSelected = productDA.getListIntTagByArrID(IDValues);
                        //foreach (var tag in tagSelected)
                        //{
                        //    tag.IsDeleted = false;
                        //    tag.IsShow = true;
                        //    tag.NameAscii =  Utils.MyString.Slug(tag.Name);
                        //    product.System_Tag.Add(tag);
                        //}
                        #endregion

                        #region Thêm & xóa file đính kèm
                        foreach (var fileItem in ListFileUpload)
                        {
                            product.System_File.Add(_productDA.GetFileData(fileItem));

                        }

                        var newsListFileRemove = _productDA.GetListFileByArrID(ListFileRemove);
                        foreach (var fileRemove in newsListFileRemove)
                        {
                            product.System_File.Remove(fileRemove);

                        }
                        #endregion
                        _productDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = product.ID.ToString(),
                            Message = string.Format("Đã cập nhật sản phẩm: <b>{0}</b>", Server.HtmlEncode(product.Name))
                        };
                    }
                    catch (Exception ex)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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

                    ltsproductItems = _productDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsproductItems)
                    {
                        if (item.Shop_Product_Variant.Any())
                        {
                            stbMessage.AppendFormat("<b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.Name));
                        }
                        else
                        {
                            item.IsDelete = true;
                            _productDA.Save();
                            stbMessage.AppendFormat("Đã xóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _productDA.Save();
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

                    ltsproductItems = _productDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsproductItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _productDA.Save();
                    msg.ID = string.Join(",", ltsproductItems.Select(o => o.ID));
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

                    ltsproductItems = _productDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsproductItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _productDA.Save();
                    msg.ID = string.Join(",", ltsproductItems.Select(o => o.ID));
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

        //public ActionResult AutoComplete()
        //{
        //    var term = Request["term"];
        //    var LtsResults = productDA.GetListSimpleByAutoComplete(term, 10, true);
        //    return Json(LtsResults, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveSEOInfo(Shop_Product product)
        {
            var _product = _productDA.GetById(product.ID);

            UpdateModel(_product);

            _product.System_Tag.Clear();
            var idValuesTag = MyBase.ConvertStringToListString(Request["NewsTag"]);
            var tagSelected = _productDA.GetListTagByArrTag(idValuesTag);
            foreach (var tag in tagSelected)
            {
                tag.IsDelete = false;
                tag.IsShow = true;
                tag.NameAscii = MyString.Slug(tag.Name);
                _product.System_Tag.Add(tag);
            }
            var idValues = MyBase.ConvertStringToListInt(Request["TagValues"]);
            tagSelected = _productDA.GetListIntTagByArrID(idValues);
            foreach (var tag in tagSelected)
            {
                tag.IsDelete = false;
                tag.IsShow = true;
                tag.NameAscii = MyString.Slug(tag.Name);
                _product.System_Tag.Add(tag);
            }

            _productDA.Save();

            return Json(new JsonMessage
            {
                Erros = false,
                ID = product.ID.ToString(),
                Message = string.Format("Đã cập nhật sản phẩm: <b>{0}</b>", Server.HtmlEncode(_product.Name))
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AutoCompleteRefProduct(string name, string brand, string productType)
        {
            var list = _productDA.GetAutoCompleteFilter(name, brand, productType, 10, true);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoComplete()
        {
            if (DoAction == ActionType.Add) //Nếu thêm từ khóa
            {
                JsonMessage msg;
                var productValue = Request["Values"];
                if (string.IsNullOrEmpty(productValue))
                {
                    msg = new JsonMessage
                    {
                        Erros = true,
                        Message = "Bạn phải nhập tên từ khóa"
                    };
                }
                else
                {
                    //var tag = productDA.AddOrGet(productValue);
                    msg = new JsonMessage
                    {
                        Erros = false,
                        //ID = tag.ID.ToString(),
                        //Message = tag.Name
                    };
                }
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            string query = Request["query"];
            var ltsResults = _productDA.getListSimpleByAutoComplete(query, 10);
            var resulValues = new AutoCompleteItem
            {
                query = query,
                data = ltsResults.Select(o => o.ID.ToString()).ToList(),
                suggestions = ltsResults.Select(o => o.Name).ToList()
            };
            return Json(resulValues, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string CheckByName(string Name, int productId)
        {
            var nameAscii = !string.IsNullOrEmpty(Name) ? Name : string.Empty;
            var result = _productDA.CheckExitsByName(nameAscii, productId);
            return result ? "false" : "true";
        }

        [HttpPost]
        public ActionResult AddImagesSlide360(int productId, string listImage)
        {
            if (!string.IsNullOrEmpty(listImage))
            {
                var arrImageId = listImage.Split(',').Select(int.Parse).ToArray();
                _productDA.AddList360(productId, arrImageId);
            }
            else
            {
                _productDA.AddList360(productId, new int[] { });
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddImagesDefault(int productId, string listImage)
        {
            if (!string.IsNullOrEmpty(listImage))
            {
                var arrImageId = listImage.Split(',').Select(int.Parse).ToArray();
                _productDA.AddListSlide(productId, arrImageId);
            }
            else
            {
                _productDA.AddListSlide(productId, new int[] { });
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddImagesGallery(int productId, string listImage)
        {
            if (!string.IsNullOrEmpty(listImage))
            {
                var arrImageId = listImage.Split(',').Select(int.Parse).ToArray();
                _productDA.AddListGallery(productId, arrImageId);
            }
            else
            {
                _productDA.AddListGallery(productId, new int[] { });
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddImagesEnjoy(int productId, string listImage)
        {
            if (!string.IsNullOrEmpty(listImage))
            {
                var arrImageId = listImage.Split(',').Select(int.Parse).ToArray();
                _productDA.AddListSlideGuide(productId, arrImageId);
            }
            else
            {
                _productDA.AddListSlideGuide(productId, new int[] { });
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGroupAttribute(int groupId)
        {
            var groupAttr = _groupAttributeDa.getById(groupId);
            groupAttr.IsDeleted = true;
            _groupAttributeDa.Save();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddProductRef(int mainProduct, int productRef)
        {
            var productRefItem = new Shop_Product_Ref
            {
                IsDelete = false,
                ProductID = mainProduct,
                PruductRefID = productRef
            };

            _productRefDa.Add(productRefItem);

            _productRefDa.Save();

            return Json(new { Error = false, id = productRefItem.ID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductRef(int id)
        {
            var refProduct = _productRefDa.GetById(id);
            refProduct.IsDelete = true;

            _productRefDa.Save();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddProductAccessory(int mainProduct, int productAccessory)
        {
            var productAccessoryItem = new Shop_Product_Compatible_Accessories
            {
                ProductID = mainProduct,
                CompatibleAccessoriesID = productAccessory,
                IsDeleted = false
            };

            _productAccessoriesDa.Add(productAccessoryItem);

            _productAccessoriesDa.Save();

            return Json(new { Error = false, id = productAccessoryItem.ID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductAccessory(int id)
        {
            var productAccessoryItem = _productAccessoriesDa.GetById(id);
            productAccessoryItem.IsDeleted = true;

            _productAccessoriesDa.Save();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAttribute(int attrId)
        {
            var attr = _attributeDa.GetById(attrId);
            attr.IsDeleted = true;
            _attributeDa.Save();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string CheckByNameAscii(string NameAscii, int productId)
        {
            var nameAscii = !string.IsNullOrEmpty(NameAscii) ? NameAscii : string.Empty;
            var result = _productDA.CheckExitsByNameAscii(nameAscii, productId);
            return result ? "false" : "true";
        }
        // phenv - 1/7/2014
        public ActionResult ShopProductToExcel(int? cateId)
        {
            var list = _productDA.ShopProductToExcel(cateId);
            ViewBag.Category = _productDA.GetListCategory();
            ViewBag.CountTotal = list.Count();
            return View(list);
        }
        public ActionResult ShopProductExportExcel(int? cateId)
        {
            string fileName = string.Format("ShopProduct{0}.xlsx", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            string filePath = Path.Combine(Request.PhysicalApplicationPath, "File\\ExportImport", fileName);
            var folder = Request.PhysicalApplicationPath + "File\\ExportImport";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var lstorder = _productDA.ShopProductToExcel(cateId);

            ExportOrdersToExcel(filePath, lstorder);

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "text/xls", fileName);
        }
        public virtual void ExportOrdersToExcel(string filePath, IList<ShopProductToExcelItem> orders)
        {
            var newFile = new FileInfo(filePath);

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(newFile))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Orders");
                xlPackage.Workbook.CalcMode = ExcelCalcMode.Manual;
                //Create Headers and format them
                var properties = new string[]
                    {
                        "Số thứ tự",
                        "Tên sản phẩm",
                        "Name Ascii",
                        "Sku",
                        "Tên hãng",
                        "Giá"
                    };
                for (var i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];
                    //worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                var row = 2;
                var dem = 0;
                foreach (var order in orders)
                {
                    dem++;
                    int col = 1;

                    //order properties
                    worksheet.Cells[row, col].Value = dem;
                    col++;

                    worksheet.Cells[row, col].Value = order.Name;
                    col++;
                    //tenkh
                    worksheet.Cells[row, col].Value = order.NameAscii;
                    col++;

                    worksheet.Cells[row, col].Value = order.Sku;
                    col++;

                    worksheet.Cells[row, col].Value = order.Brand;
                    col++;

                    worksheet.Cells[row, col].Value = order.Price;
                    col++;

                    //next row
                    row++;
                }

                // we had better add some document properties to the spreadsheet 
                // set some core property values
                var nameexcel = "Danh sách sản phẩm" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                xlPackage.Workbook.Properties.Title = string.Format("{0} orders", nameexcel);
                xlPackage.Workbook.Properties.Author = "Admin-IT";
                xlPackage.Workbook.Properties.Subject = string.Format("{0} orders", "");
                //xlPackage.Workbook.Properties.Keywords = string.Format("{0} orders", _storeInformationSettings.StoreName);
                xlPackage.Workbook.Properties.Category = "Orders";
                //xlPackage.Workbook.Properties.Comments = string.Format("{0} orders", _storeInformationSettings.StoreName);

                // set some extended property values
                xlPackage.Workbook.Properties.Company = "Ecom -  .com.vn";
                //xlPackage.Workbook.Properties.HyperlinkBase = new Uri(_storeInformationSettings.StoreUrl);
                // save the new spreadsheet
                xlPackage.Save();
            }
        }
    }
}
