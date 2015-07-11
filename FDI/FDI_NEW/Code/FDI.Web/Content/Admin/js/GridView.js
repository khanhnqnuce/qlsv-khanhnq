var lastGirdHeight;
var urlDeleteFrefix = "";
var urlHideFrefix = "";
var urlShowFrefix = "";
var imageLoading = "<div class='col-sm-12 text-left m-t-sm' style='position: absolute;'><img src='/images/loading52.gif'/></div>";
var urlLists = '';
var urlForm = '';
var urlShow = '';
var urlFormGroup = '';
var urlPostAction = '';
var urlPostActionGroup = '';
var urlPostRatingAction = '';
var urlEditRating = '';
var urlSort = '';
var urlSortGroup = '';
var urlView = '';
var urlHistory = '';
var urlEditPoint = '';
var urlFormRoleA = '';
var urlFormModule = '';
var urlViewGroup = '';
var formHeight = 'auto';
var formWidth = '600';

function loadAjax(urlContent, container) {

    $.ajax({
        url: encodeURI(urlContent),
        cache: false,
        type: "POST",
        success: function (data) {
            $(container).html(data);
        }
    });
}

function initAjaxLoad(urlListLoad, container, callback) {
    //alert(container);
    $.address.init().change(function (event) {
        var urlTransform = urlListLoad;
        var urlHistory = event.value;
        if (urlHistory.length > 0) {
            urlHistory = urlHistory.substring(1, urlHistory.length);
            if (urlTransform.indexOf('?') > 0)
                urlTransform = urlTransform + '&' + urlHistory;
            else
                urlTransform = urlTransform + '?' + urlHistory;
        }
        //alert(urlTransform);
        $(container).html(imageLoading);
        $.post(urlTransform, function (data) {
            //alert(data);
            $(container).html(data);
        }).complete(function () {
            if (callback && typeof (callback) === "function") {
                callback();
            }
        });
    });
}

function changeHashValue(key, value, source) {
    value = encodeURIComponent(value);
    var currentLink = source.substring(1);
    var returnLink = '#';
    var exits = false;
    if (currentLink.indexOf('&') > 0) { // lớn hơn 1
        var tempLink = currentLink.split('&');
        for (idx = 0; idx < tempLink.length; idx++) {
            if (key == tempLink[idx].split('=')[0]) { //check Exits
                returnLink += key + '=' + value;
                exits = true;
            }
            else {
                returnLink += tempLink[idx];
            }
            if (idx < tempLink.length - 1)
                returnLink += '&';
        }
        if (!exits)
            returnLink += '&' + key + '=' + value;
    } else if (currentLink.indexOf('=') > 0) { //Chỉ 1
        if (currentLink.match(/Page/)) {
            returnLink = '#' + key + '=' + value;
        } else {
            returnLink = '#' + currentLink + '&' + key + '=' + value;
        }
    }
    else {
        returnLink = '#' + key + '=' + value;
    }
    return returnLink;
}

//Chuyển trang với value mới
function changeHashUrl(key, value) {
    var currentLink = $.address.value();
    return changeHashValue(key, value, currentLink);
}


function registerGridView(selector) {
    //Đổi màu row
    $(selector + " .gridView tr").each(function (index) {
        if (index % 2 == 0)
            $(this).addClass("odd");
    });

    //Sắp xếp các cột
    //TúNT      22/11/2013      Update sort dont' lose param search, page
    $(selector + " .gridView th a").each(function (idx) {
        var link = $(this).attr("href");
        link = link.substring(1, link.length);
        var newLink = '';
        var currentLink = $.address.value();
        //Trường hợp ghi đè mọi thuộc tính khác trên grid với có số trang, thông tin tìm kiếm
        if (currentLink.indexOf('&') > 1) {
            var re = /(?:\x26)?(Field\x3D[^\x26]+)\x26/;
            //alert('link: '+link);
            //alert('current link: '+currentLink);
            if (currentLink.match(/\x26(Field\x3D[^\x26]+)\x26/))
                newLink = currentLink.replace(re, link + '&');
            if (currentLink.match(/(?:\x26)?(Field\x3D[^\x26]+)\x26/))
                newLink = currentLink.replace(re, '&' + link + '&');
            if (newLink.match(/^\x2F/))
                newLink = newLink.substring(1, newLink.length);
            if (newLink.match(/^\x26/))
                newLink = newLink.substring(1, newLink.length);
            //alert('newLink: '+newLink);
            $(this).attr("href", '#' + newLink);
        }
        //Trường hợp đã sort
        if ($.address.value().indexOf(link) > 0) {
            if ($.address.value().indexOf('FieldOption=1') > 0) { //Tăng dần
                newLink = newLink.replace('/', '');
                var tempLink = newLink;
                $(this).addClass('desc');
                var re2 = /\x26(FieldOption\x3D\d+)/;
                newLink = newLink.replace(re2, '&FieldOption=0');
                if (newLink == tempLink) {
                    var re4 = /\x26(x26FieldOption\x3D[^\x26]+)\x26/;
                    newLink = newLink.replace(re4, '&FieldOption=0');
                }
                newLink = newLink.replace('/', '');
                if (newLink.match(/^\x26/))
                    newLink = newLink.substring(1, newLink.length);
                $(this).attr("href", '#' + newLink);
            }
            else { //Giảm dần
                $(this).addClass('asc');
                var tempLink = newLink;
                var re5 = /\x26(FieldOption\x3D\d+)/;
                newLink = newLink.replace('/', '');
                newLink = newLink.replace(re5, '&FieldOption=1');
                if (newLink == tempLink) {
                    var re6 = /\x26(x26FieldOption\x3D[^\x26]+)\x26/;
                    newLink = newLink.replace(re6, 'FieldOption=1');
                }
                if (newLink == '')
                    newLink = link + '&FieldOption=1';
                newLink = newLink.replace('/', '');
                $(this).attr("href", '#' + newLink);
            }
        }
    });

    //khi người dùng click trên 1 row
    $(selector + " .gridView tr").not("first").click(function () {
        $(selector + " .gridView tr").removeClass("hightlight");
        $(this).addClass("hightlight");
    });

    //checkall
    $(selector + ' .checkAll').click(function () {
        var selectQuery = selector + " input.check[type='checkbox']";
        if ($(this).val() != '')
            selectQuery = selector + " #" + $(this).val() + " input.check[type='checkbox']";
        $(selectQuery).attr('checked', $(this).is(':checked'));
    });

    //checkall popup
    $(selector + ' .checkAllTopHome').click(function () {
        var selectQuery = selector + " input.check[type='checkbox']";
        if ($(this).val() != '')
            selectQuery = selector + " #" + $(this).val() + " input.check[type='checkbox']";
        $(selectQuery).attr('checked', $(this).is(':checked'));
    });

    //Nhảy trang
    $(selector + " .bottom-pager input").change(function () {
        var cPage = trim12($(this).val());
        var maxPage = $(selector + " .bottom-pager input[type=hidden]").val();
        if (cPage.length == 0)
            createMessage("Thông báo", "Yêu cầu nhập trang cần chuyển đến");
        else if (isNaN(cPage))
            createMessage("Thông báo", "trang chuyển đến phải là kiểu số");
        else if (parseInt(cPage) > maxPage)
            createMessage("Thông báo", "trang không được lớn hơn " + maxPage + "");
        else if (parseInt(cPage) <= 0) {
            createMessage("Thông báo", "trang phải lớn hơn 0");
        }
        else {
            window.location.href = changeHashUrl('Page', cPage);;
        }
    });

    //ẩn hiện nhóm
    $(selector + ' .gridView [data-event="group"]').click(function () {
        var idShowHide = $(this).attr("href");
        if ($(this).text() == '+') {
            $(idShowHide).show();
            $(this).text("-");
        } else {
            $(idShowHide).hide();
            $(this).text("+");
        }
        return false;
    });

    //Thay đổi số bản ghi trên trang
    $(selector + " .bottom-pager select").change(function () {
        var urlFWs = $.address.value();
        urlFWs = changeHashValue("Page", 1, urlFWs); //Replace  &Page=.. => Page=1
        urlFWs = changeHashValue("RowPerPage", $(this).val(), urlFWs); //Replace  &TenDonVi=.. => TenDonVi=donViNhan
        window.location.href = urlFWs;
    });

    //Đăng ký thêm nhiều mà không cần dialog nhiều
    $(selector + ' .gridView [data-event="addAll"]').click(function () {
        var arrRowId = '';
        var rowTitle = '';
        var linkFW = '';
        var linkFW = (linkFW == '') ? '#' + urlDeleteFrefix + 'Page=1' : '#' + urlDeleteFrefix + linkFW;
        $(selector + " input.check[type='checkbox']:checked").not("#checkAll").not(".checkAll").each(function () {
            arrRowId += $(this).val() + ",";
            rowTitle += "<li>" + $(this).parent().parent().attr("title") + "</li>";
        });
        rowTitle = "<ul>" + rowTitle + "</ul>";

        arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
        rowAdd(urlPostAction, arrRowId, rowTitle, linkFW);
        return false;
    });

    //Đăng ký thêm nhiều mà không cần dialog nhiều
    $(selector + ' .gridView [data-event="addAllTopHome"]').click(function () {
        var arrRowId = '';
        var rowTitle = '';
        var linkFW = '';
        var linkFW = (linkFW == '') ? '#' + urlDeleteFrefix + 'Page=1' : '#' + urlDeleteFrefix + linkFW;
        $(selector + " input.check[type='checkbox']:checked").not("#checkAllTopHome").not(".checkAllTopHome").each(function () {
            arrRowId += $(this).val() + ",";
            rowTitle += "<li>" + $(this).parent().parent().attr("title") + "</li>";
        });
        rowTitle = "<ul>" + rowTitle + "</ul>";

        arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
        rowAddShowpage(urlPostAction, arrRowId, rowTitle, linkFW);
        return false;
    });

    //Đăng ký xóa nhiều
    $('[data-event="deleteAll"][data-grid="' + selector + '"]').click(function () {
        var arrRowId = '';
        var rowTitle = '';
        var linkFW = '';
        var linkFW = (linkFW == '') ? '#' + urlDeleteFrefix + 'Page=1' : '#' + urlDeleteFrefix + linkFW;
        $(selector + " input.check[type='checkbox']:checked").not("#checkAll").not(".checkAll").each(function () {
            arrRowId += $(this).val() + ",";
            rowTitle += "<li>" + $(this).parent().parent().attr("title") + "</li>";
        });
        rowTitle = "<ul>" + rowTitle + "</ul>";

        arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
        rowDelete(urlPostAction, arrRowId, rowTitle, linkFW);
        return false;
    });

    //Đăng ký Hiển thị nhiều
    $('[data-event="showAll"][data-grid="' + selector + '"]').click(function () {
        var arrRowId = '';
        var rowTitle = '';
        var linkFW = '';
        var linkFW = (linkFW == '') ? '#' + urlShowFrefix + 'Page=1' : '#' + urlShowFrefix + linkFW;
        $(selector + " input.check[type='checkbox']:checked").not("#checkAll").not(".checkAll").each(function () {
            arrRowId += $(this).val() + ",";
            rowTitle += "<li>" + $(this).parent().parent().attr("title") + "</li>";
        });
        rowTitle = "<ul>" + rowTitle + "</ul>";

        arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
        rowShow(urlPostAction, arrRowId, rowTitle, linkFW);
        return false;
    });


    //Đăng ký ẩn nhiều
    $('[data-event="hideAll"][data-grid="' + selector + '"]').click(function () {
        var arrRowId = '';
        var rowTitle = '';
        var linkFW = '';
        var linkFW = (linkFW == '') ? '#' + urlHideFrefix + 'Page=1' : '#' + urlHideFrefix + linkFW;
        $(selector + " input.check[type='checkbox']:checked").not("#checkAll").not(".checkAll").each(function () {
            arrRowId += $(this).val() + ",";
            rowTitle += "<li>" + $(this).parent().parent().attr("title") + "</li>";
        });
        rowTitle = "<ul>" + rowTitle + "</ul>";

        arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
        rowHide(urlPostAction, arrRowId, rowTitle, linkFW);
        return false;
    });

    //Đăng ký duyệt nhiều
    $('[data-event="activeAll"][data-grid="' + selector + '"]').click(function () {
        var arrRowId = '';
        var rowTitle = '';
        var linkFW = '';
        var linkFW = (linkFW == '') ? '#' + urlHideFrefix + 'Page=1' : '#' + urlHideFrefix + linkFW;
        $(selector + " input.check[type='checkbox']:checked").not("#checkAll").not(".checkAll").each(function () {
            arrRowId += $(this).val() + ",";
            rowTitle += "<li>" + $(this).parent().parent().attr("rel") + "</li>";
        });
        rowTitle = "<ul>" + rowTitle + "</ul>";
        arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
        rowActive(urlPostAction, arrRowId, rowTitle, linkFW);
        return false;
    });

    //Đăng ký button xóa row nhóm
    $(selector + ' .gridView [data-event="delete_group"]').click(function () {
        rowDelete(urlPostActionGroup, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });

    //Đăng ký button hiển thị nhóm
    $(selector + ' .gridView [data-event="show_group"]').click(function () {
        rowShow(urlPostActionGroup, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });

    //Đăng ký button ẩn nhóm
    $(selector + ' .gridView [data-event="hide_group"]').click(function () {
        rowHide(urlPostActionGroup, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlHideFrefix + "Page=1");
        return false;
    });

    //Đăng ký button xóa row
    $(selector + ' .gridView [data-event="delete"]').click(function () {
        rowDelete(urlPostAction, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });

    // Đăng ký button add row (ShowPage)
    $(selector + ' .gridView [data-event="showpage"]').click(function () {
        rowAddShowpage(urlPostAction, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });

    //Đăng ký button hiển thị
    $(selector + ' .gridView [data-event="show"]').click(function () {
        rowShow(urlPostAction, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });

    //Đăng ký button duyệt
    $(selector + ' .gridView [data-event="active"]').click(function () {
        rowActive(urlPostAction, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });
    //Đăng ký button ẩn
    $(selector + ' .gridView [data-event="hide"]').click(function () {
        rowHide(urlPostAction, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlHideFrefix + "Page=1");
        return false;
    });

    //đăng ký Thêm row
    $(selector + ' .gridView [data-event="add"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Thêm mới bản ghi';

        var urlRequest = '';
        if (urlForm.indexOf('?') > 0)
            urlRequest = urlForm + '&do=add&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlForm + '?do=add&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });

    //Đăng ký thêm mới row không hiện thị trên dialog
    $(selector + ' .gridView [data-event="addNoD"]').click(function () {
        rowAdd(urlPostAction, $(this).attr("href").substring(1), escapeHTML($(this).attr("title")), "#" + urlDeleteFrefix + "Page=1");
        return false;
    });

    // xem thông tin
    $(selector + ' .gridView [data-event="showModule"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin';

        var urlRequest = '';
        if (urlShow.indexOf('?') > 0)
            urlRequest = urlShow + '&do=showModule&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlShow + '?do=showModule&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);
        return false;
    });

    //đăng ký sửa row
    $(selector + ' .gridView [data-event="edit"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Sửa thông tin bản ghi';

        var urlRequest = '';
        if (urlForm.indexOf('?') > 0)
            urlRequest = urlForm + '&do=edit&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlForm + '?do=edit&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });

    // Gán User cho Module
    $(selector + ' .gridView [data-event="userModule"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Gán User cho Module';

        var urlRequest = '';
        if (urlViewRole.indexOf('?') > 0)
            urlRequest = urlViewRole + '&do=userModule&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlViewRole + '?do=userModule&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });

    // Gán Role cho Module
    $(selector + ' .gridView [data-event="roleModule"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Gán Role cho Module';

        var urlRequest = '';
        if (urlRole.indexOf('?') > 0)
            urlRequest = urlRole + '&do=roleModule&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlRole + '?do=roleModule&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });

    //đăng ký sắp xếp
    $(selector + ' .gridView [data-event="sort"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Sắp xếp thứ tự hiển thị';

        var urlRequest = '';
        if (urlSort.indexOf('?') > 0)
            urlRequest = urlSort + '&do=sort&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlSort + '?do=sort&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });


    //đăng ký xem row
    $(selector + ' .gridView [data-event="view"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlView.indexOf('?') > 0)
            urlRequest = urlView + '&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlView + '?itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });
    // xem thong tin campaing - hungdc25-03-2014
    $(selector + ' .gridView [data-event="viewCampaign"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlView.indexOf('?') > 0)
            urlRequest = urlView + '&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlView + '?itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    //đăng ký xem lích sử của khách hàng
    $(selector + ' .gridView [data-event="history"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlView.indexOf('?') > 0)
            urlRequest = urlHistory + '&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlHistory + '?itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    //đăng ký xem danh sách sản phẩm của giảm giá
    $(selector + ' .gridView [data-event="productofdiscount"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = $(this).data("url");

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    $(selector + ' .gridView [data-event="RoleModule"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlFormModule.indexOf('?') > 0)
            urlRequest = urlFormModule + '&do=update&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlFormModule + '?do=update&itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    $(selector + ' .gridView [data-event="ActionRoleA"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlFormRoleA.indexOf('?') > 0)
            urlRequest = urlFormRoleA + '&do=edit&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlFormRoleA + '?do=edit&itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    //đăng ký xem xem và chỉnh sửa điểm khách hàng
    $(selector + ' .gridView [data-event="point"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlView.indexOf('?') > 0)
            urlRequest = urlEditPoint + '&do=edit&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlEditPoint + '?do=edit&itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    //đăng ký xem xem và chỉnh sửa đánh giá sản phẩm
    $(selector + ' .gridView [data-event="rating"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin';

        var urlRequest = '';
        if (urlView.indexOf('?') > 0)
            urlRequest = urlEditRating + '&do=edit&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlEditRating + '?do=edit&itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });

    //đăng ký Thêm row cho nhóm
    $(selector + ' .gridView [data-event="add_group"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Thêm mới bản ghi';

        var urlRequest = '';
        if (urlForm.indexOf('?') > 0)
            urlRequest = urlForm + '&do=add&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlForm + '?do=add&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });

    //đăng ký sửa row nhóm
    $(selector + ' .gridView [data-event="edit_group"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Sửa thông tin bản ghi';

        var urlRequest = '';
        if (urlFormGroup.indexOf('?') > 0)
            urlRequest = urlFormGroup + '&do=edit&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlFormGroup + '?do=edit&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });


    //đăng ký sắp xếp
    $(selector + ' .gridView [data-event="sort_group"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Sắp xếp thứ tự hiển thị';

        var urlRequest = '';
        if (urlSortGroup.indexOf('?') > 0)
            urlRequest = urlSortGroup + '&do=sort&ItemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlSortGroup + '?do=sort&ItemId=' + $(this).attr("href").substring(1);

        FdiDialog(urlRequest, titleDiag, formHeight, formWidth);

        return false;
    });

    //đăng ký xem row nhóm
    $(selector + ' .gridView [data-event="view_group"]').click(function () {
        var titleDiag = $(this).attr("title");
        if (titleDiag == '')
            titleDiag = 'Xem thông tin bản ghi';

        var urlRequest = '';
        if (urlViewGroup.indexOf('?') > 0)
            urlRequest = urlViewGroup + '&itemId=' + $(this).attr("href").substring(1);
        else
            urlRequest = urlViewGroup + '?itemId=' + $(this).attr("href").substring(1);

        FdiDialogView(urlRequest, titleDiag, viewHeight, viewWidth);

        return false;
    });
}

//Hiển thị row tren grid
function rowShow(urlPost, arrRowId, rowTitle, urlFw) {
    var titleDia = '';
    if (arrRowId == '')
        createMessage("Thông báo", "Bạn chưa chọn bản ghi nào");
    else {
        if (arrRowId.indexOf(',') > 0)
            titleDia = "Hiển thị các bản ghi đã chọn";
        else
            titleDia = "Hiển thị bản ghi đã chọn";
        $("#dialog-confirm").attr(titleDia);
        $("#dialog-confirm").html("<p><b>Bạn có chắc chắn muốn hiển thị:</b><br />" + rowTitle + "</p>");
        var comfirmReturn = false;
        $("#dialog-confirm").dialog({
            title: titleDia,
            resizable: false,
            height: 'auto',
            width: 'auto',
            modal: true,
            buttons: {
                "Tiếp tục": function () {
                    $(this).dialog("close");
                    $.post(encodeURI(urlPost), { "do": "show", "itemId": "" + arrRowId + "" }, function (data) {
                        if (data.Erros) {
                            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
                        }
                        else {
                            $.notify(data.Message, 'success');
                            var RowPerPage = $(".bottom-pager select").val();

                            window.location.href = urlFw + '&type=show&idShow=' + arrRowId + '&RowPerPage=' + RowPerPage;
                        }
                    });
                },
                "Hủy lệnh hiển thị": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

//Duyệt row tren grid
function rowActive(urlPost, arrRowId, rowTitle, urlFw) {
    var titleDia = '';
    if (arrRowId == '')
        createMessage("Thông báo", "Bạn chưa chọn bản ghi nào");
    else {
        if (arrRowId.indexOf(',') > 0)
            titleDia = "Duyệt các bản ghi đã chọn";
        else
            titleDia = "Duyệt bản ghi đã chọn";
        $("#dialog-confirm").attr(titleDia);
        $("#dialog-confirm").html("<p><b>Bạn có chắc chắn muốn duyệt:</b><br />" + rowTitle + "</p>");
        var comfirmReturn = false;
        $("#dialog-confirm").dialog({
            title: titleDia,
            resizable: false,
            height: 'auto',
            width: 'auto',
            modal: true,
            buttons: {
                "Tiếp tục": function () {
                    $(this).dialog("close");
                    $.post(encodeURI(urlPost), { "do": "active", "itemId": "" + arrRowId + "" }, function (data) {
                        if (data.Erros) {
                            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
                        }
                        else {
                            $.notify(data.Message, 'success');
                            window.location.href = urlFw + '&type=show&idShow=' + arrRowId;
                        }
                    });
                },
                "Hủy lệnh duyệt": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

//ẩn row tren grid
function rowHide(urlPost, arrRowId, rowTitle, urlFw) {
    var titleDia = '';
    if (arrRowId == '')
        createMessage("Thông báo", "Bạn chưa chọn bản ghi nào");
    else {
        if (arrRowId.indexOf(',') > 0)
            titleDia = "Ẩn các bản ghi đã chọn";
        else
            titleDia = "Ẩn bản ghi đã chọn";
        $("#dialog-confirm").attr(titleDia);
        $("#dialog-confirm").html("<p><b>Bạn có chắc chắn muốn ẩn:</b><br />" + rowTitle + "</p>");
        var comfirmReturn = false;
        $("#dialog-confirm").dialog({
            title: titleDia,
            resizable: false,
            height: 'auto',
            width: 'auto',
            modal: true,
            buttons: {
                "Tiếp tục": function () {
                    $(this).dialog("close");
                    $.post(encodeURI(urlPost), { "do": "hide", "itemId": "" + arrRowId + "" }, function (data) {
                        if (data.Erros) {
                            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
                        }
                        else {
                            $.notify(data.Message, 'success');
                            var RowPerPage = $(".bottom-pager select").val();
                            window.location.href = urlFw + '&type=hide&idHide=' + arrRowId + '&RowPerPage=' + RowPerPage;

                        }
                    });
                },
                "Hủy lệnh ẩn": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

//xoa row tren grid
function rowDelete(urlPost, arrRowId, rowTitle, urlFw) {
    var titleDia = '';
    if (arrRowId == '')
        createMessage("Thông báo", "Bạn chưa chọn bản ghi nào");
    else {
        if (arrRowId.indexOf(',') > 0)
            titleDia = "Xóa các bản ghi đã chọn";
        else
            titleDia = "Xóa bản ghi đã chọn";
        $("#dialog-confirm").attr(titleDia);
        $("#dialog-confirm").html("<p><b>Bạn có chắc chắn muốn xóa:</b><br />" + rowTitle + "</p>");
        var comfirmReturn = false;
        $("#dialog-confirm").dialog({
            title: titleDia,
            resizable: false,
            height: 'auto',
            width: 'auto',
            modal: true,
            buttons: {
                "Tiếp tục": function () {
                    $(this).dialog("close");
                    $.post(encodeURI(urlPost), { "do": "delete", "itemId": "" + arrRowId + "" }, function (data) {
                        if (data.Erros) {
                            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
                        }
                        else {
                            $.notify(data.Message, 'success');
                            window.location.href = urlFw + '&type=delete&idDelete=' + arrRowId;
                        }
                    });
                },
                "Hủy lệnh xóa": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

// Add row
function rowAddShowpage(urlPost, arrRowId, rowTitle, urlFw) {
    var titleDia = '';
    if (arrRowId == '')
        createMessage("Thông báo", "Bạn chưa chọn bản ghi nào");
    else {
        if (arrRowId.indexOf(',') > 0)
            titleDia = "Thêm các bản ghi đã chọn";
        else
            titleDia = "Thêm bản ghi đã chọn";
        $("#dialog-confirm").attr(titleDia);
        $("#dialog-confirm").html("<p><b>Bạn có chắc chắn muốn thêm:</b><br />" + rowTitle + "</p>");
        var comfirmReturn = false;
        $("#dialog-confirm").dialog({
            title: titleDia,
            resizable: false,
            height: 'auto',
            width: 'auto',
            modal: true,
            buttons: {
                "Tiếp tục": function () {
                    $(this).dialog("close");
                    $.post(encodeURI(urlPost), { "do": "Add", "itemId": "" + arrRowId + "" }, function (data) {
                        if (data.Erros) {
                            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
                        }
                        else {
                            $.notify(data.Message, 'success');
                            $('#brandGriditemsHomePage').load('/Admin/ProductTopOrder/ListItemsNotInHomePage?');
                        }
                    });
                },
                "Hủy lệnh thêm": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

//Thêm mới row cho các bảng maping không hiện thi dialog
function rowAdd(urlPost, arrRowId, rowTitle, urlFw) {
    var titleDia = '';
    if (arrRowId == '')
        createMessage("Thông báo", "Bạn chưa chọn bản ghi nào");
    else {
        if (arrRowId.indexOf(',') > 0)
            titleDia = "Thêm các bản ghi đã chọn";
        else
            titleDia = "Thêm bản ghi đã chọn";
        $("#dialog-confirm").attr(titleDia);
        $("#dialog-confirm").html("<p><b>Bạn có chắc chắn muốn thêm mới:</b><br />" + rowTitle + "</p>");
        var comfirmReturn = false;
        $("#dialog-confirm").dialog({
            title: titleDia,
            resizable: false,
            height: 'auto',
            width: 'auto',
            modal: true,
            buttons: {
                "Tiếp tục": function () {
                    $(this).dialog("close");
                    $.post(encodeURI(urlPost), { "do": "add", "itemId": "" + arrRowId + "" }, function (data) {
                        if (data.Erros) {
                            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
                        }
                        else {
                            $.notify(data.Message, 'success');
                            window.location.href = urlFw + '&type=add&idAdd=' + arrRowId;
                        }
                    });
                },
                "Hủy lệnh thêm mới": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

function escapeHTML(str) {
    var div = document.createElement('div');
    var text = document.createTextNode(str);
    div.appendChild(text);
    return div.innerHTML;
}


function trim12(str) {
    var str = str.replace(/^\s\s*/, ''),
		ws = /\s/,
		i = str.length;
    while (ws.test(str.charAt(--i)));
    return str.slice(0, i + 1);
}

function createCloseMessage(title, message, urlFw) {
    urlFw = urlFw.replace(/<\/?\w(?:[^"'>]|"[^"]*"|'[^']*')*>/g, "");

    $.notify(message, 'success');
    window.location.href = urlFw;
}


function setRolesEdit(status) {
    if (status)
        $(".act_edit").css("display", "marker");
    else
        $(".act_edit").css("display", "none");
}

function setRolesDelete(status) {
    if (status)
        $(".act_delete").css("display", "marker");
    else
        $(".act_delete").css("display", "none");
}

function setRolesAdd(status) {
    if (status) {
        $(".act_add").removeClass("act_add_hidden");
        $(".act_add").css("display", "marker");
    }
    else
        $(".act_add").css("display", "none");
}

function setRolesApproved(status) {
    if (status)
        $(".act_approved").css("display", "marker");
    else
        $(".act_approved").css("display", "none");
}


function setRolesRoles(status) {
    if (status)
        $(".act_roles").css("display", "marker");
    else
        $(".act_roles").css("display", "none");
}

function createAutoTag(tagControls, urlRouters) {
    $("#" + tagControls + "_Input").keypress(function (e) {
        if (e.keyCode == 13) {
            valuesAdd = trim12($(this).val());
            if (valuesAdd == '')
                createMessage('Đã có lỗi xảy ra.', 'Bạn phải nhập vào từ khóa tìm kiếm');
            else {
                addValues(tagControls, valuesAdd, urlRouters + "&do=Add", '');
            }
            return false;
        }
    });

    $('#' + tagControls + "_Input").autocomplete({
        serviceUrl: urlRouters,
        minChars: 1,
        delimiter: /(;)\s*/, // regex or character
        maxHeight: 400,
        width: 500,
        zIndex: 9999,
        deferRequestBy: 0, //miliseconds
    });
}
