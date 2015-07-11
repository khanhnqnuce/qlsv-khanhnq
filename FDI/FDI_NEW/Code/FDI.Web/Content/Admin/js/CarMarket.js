
function registerGallery() {
 
    // Đăng ký sự kiện gọi gallery picture dialog
    $('[data-type="gallery"]').click(function () {
        var multi = $(this).attr('data-multi');
        var value = $(this).attr('data-value');
        var container = $(this).attr('data-container'); 
        selectPicture("/Admin/GalleryPictureSelect/?MutilFile=" + multi + "&Container=" + container + "&ValuesSelected=" + value);
    });
    
}

function getValuePicture(container) {
    
    var obj = container;
    if (obj.indexOf(',') > 0) {
        container = obj.split(',')[0];
    }
    var arrRowId = '';
    $("#Text_" + container + " tr").each(function () {
        arrRowId += $(this).attr("id") + ",";
    });
    arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
    if (typeof selectMutil != "undefined" &&  selectMutil == false) {
        var strArr = arrRowId.split(',');
        arrRowId = strArr[strArr.length - 1];
    }
    $("#Value_" + container + "").val(arrRowId);
    return arrRowId;
}

function getValueVideo(container) {
    var arrRowId = '';
    $("#Text_" + container + " tr").each(function () {
        arrRowId += $(this).attr("id") + ",";
    });
    arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
    if (typeof selectMutil != "undefined" && selectMutil == false) {
        var strArr = arrRowId.split(',');
        arrRowId = strArr[strArr.length - 1];
    }
    $("#Value_" + container + "").val(arrRowId);
    return arrRowId;
}

function selectPicture(urlSelectImage)
{
    $("#dialog-form-2").html("");
    $("#dialog-form-2").dialog(
		{
		    title: "Chọn ảnh",
		    resizable: true,
		    modal: true,
			width: 950,
			height: 580,
			buttons: {
			    "Chọn ảnh": function () {
			        $("#btnPictureSelectSubmit").click();
			    },
			    "Chọn lại": function () {
			        $("#btnPictureSelectreset").click();
			    },
			    "Đóng": function () {
			        $(this).dialog("close");
			    }
			},
			show: {
			    effect: "fade",
			    duration: 500
			}
		}
	).dialogExtend({
	    closable: true,
	    maximizable: true,
	    minimizable: true,
	    minimizeLocation: "right",
	    collapsable: true,
	    dblclick: "maximize"
	}).load(encodeURI(urlSelectImage)).dialog("open");
    return false;
}

function selectVideo(urlSelectVideo) {
    $("#dialog-form-2").html("");
    $("#dialog-form-2").dialog(
		{
		    title: "Chọn video",
		    width: 950,
		    height: 580
		}
	).load(encodeURI(urlSelectVideo)).dialog("open");
    return false;
}

function createAutoTag(tagControls, urlRouters) { 
     $("#" + tagControls).keypress(function (e) {
        if (e.keyCode == 13) {
            addValues(tagControls , $(this).val(), urlRouters + "?do=Add", '');
            return false;
        }
    });

    $('#' + tagControls).autocomplete({
        serviceUrl: urlRouters,
        minChars: 1,
        delimiter: /(,|;)\s*/, // regex or character
        maxHeight: 400,
        width: 500,
        zIndex: 9999,
        deferRequestBy: 0, //miliseconds
    });
}

function createAutoTag(tagControls, urlRouters, labelKey) { 
     $("#" + tagControls).keypress(function (e) {
        if (e.keyCode == 13) {
            addValues(tagControls , $(this).val(), urlRouters + "?do=Add&KeyID=" + labelKey, labelKey);
            return false;
        }
    });

    $('#' + tagControls).autocomplete({
        serviceUrl: urlRouters,
        minChars: 1,
        delimiter: /(,|;)\s*/, // regex or character
        maxHeight: 400,
        width: 500,
        zIndex: 9999,
        params: { KeyID : labelKey},
        deferRequestBy: 0, //miliseconds
    });
}


//Thêm mới key
function addValues(container, value, urlRouters, key) {
    var controls_input = $("#" + container);
    var controls = $("#" + container + "_Value");
    $.post(encodeURI(urlRouters), { "values": "" + value + "" }, function (data) {
        if (data.Erros) {
            createMessage("Có lỗi xảy ra", "<b>Lỗi được thông báo:</b><br/>" + data.Message);
        }
        else {
            $(controls).append("<li id=\"" + container + "_" + data.ID + "\" name=\"" + data.ID + "\" key=\""+key+"\"><span>" + data.Message + "</span><a href=\"javascript:deletevalues('" + container + "_" + data.ID + "');\"><img border=\"0\" src=\"/Content/Admin/Images/gridview/act_filedelete.png\"></a></li>");
            $(controls_input).val("");
        }
    });
}

//Xóa key
function deletevalues(valueKey) {
    $("#" + valueKey).remove();
}


function createCKFider(instance, imageWidth) {
    $("#" + instance + "Button").click(function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            var htmlRespoint = "<input type=\"hidden\" name=\"" + instance + "\" value=\"" + fileUrl + "\" />";
            htmlRespoint += "<img src=\"" + fileUrl + "\" style=\"border:1px solid #ccc; width:" + imageWidth + "px; margin-top:2px;\" />";
            $("#" + instance + "Values").html(htmlRespoint);
        };
        finder.popup();
    });
}

function createUploader() {
    var uploader = new qq.FileUploader({
        element: document.getElementById('btnChosse'),
        action: '/Uploader/UploadFile.aspx',
        debug: true,
        onSubmit: function (id, fileName) {
            // check trùng file
            var exits = false;
            //check trong trường hợp mới upload
            $("#listFileAttach li").each(function (index, item) {
                if (fileName == $(this).children("span").attr("title"))
                    exits = true;
            });
            //check trên file đã upload
            $("#listFileAttachRemove li").each(function (index, item) {
                if (fileName == $(this).children("span").attr("title"))
                    exits = true;
            });

            if (exits) {
                createMessage("Thông báo", fileName + " Đã tồn tại.");
                return false;
            }
        },
        onComplete: function (id, fileName, responseJSON) {
            if (responseJSON.upload) {
                $("#listFileAttach").append(getHTMLDeleteLink(responseJSON));
                $("#listValueFileAttach").val(changeHiddenInput())
            } else {
                createMessage("Thông báo", responseJSON.message);
            }
        }
    });
}
/*file đính kèm*/
//Lấy về html file
function getHTMLDeleteLink(data) {
    return "<li><span id=\"" + data.fileserver + "\" title=\"" + data.filename + "\">" + data.filename + "</span><a href=\"javascript:DeleteFile('" + data.fileserver + "');\"><img src=\"/Content/Admin/Images/gridview/act_filedelete.png\" title=\"Xóa file đính kèm\" border=\"0\"></a></li>";
}

//xóa file
function DeleteFile(file) {
    $.post('/Uploader/DeleteFile.aspx', { del: file });
    $("#listFileAttach span[id='" + file + "']").parent().remove();
}


function DeleteFileUpdate(file) {
    var linkDelete = $("#listValueFileAttachRemove").val();
    var values = "," + file + linkDelete;
    values = values.substring(1);
    $("#listValueFileAttachRemove").val(values);
    $("#listFileAttachRemove span[id='" + file + "']").parent().remove();
}


//lấy dữ liệu từ list 
function changeHiddenInput() {
    var valueFile = '[';
    var total = $("#listFileAttach li").length;
    $("#listFileAttach li").each(function (i) {
        valueFile += '{"FileServer": "' + $(this).children("span").attr("id") + '"\,';
        valueFile += '"FileName": "' + $(this).children("span").attr("title") + '"\}';
        if (i + 1 < total)
            valueFile += ',';
    });
    valueFile += "]";
    return valueFile;
}


var config_description = {
    height: 200
};
var config_content = {
    toolbar: 'Full',
    height: 250
};

function LoadCKEDITOR(instanceName, fullEditor) {
    //if (CKEDITOR.instances[instanceName]) {
    //    CKEDITOR.remove(CKEDITOR.instances[instanceName]);
    //}
    
    if (fullEditor)
        CKEDITOR.replace(instanceName, config_content);
    else
        CKEDITOR.replace(instanceName, config_description);

}

function getValueMutilSelect(selectName) {
    var arrID = '';
    $.each($('select.mutil').multiSelect("getSelects"), function (key, value) {
        arrID += value + ",";
    });
    arrID = (arrID.length > 0) ? arrID.substring(0, arrID.length - 1) : arrID;
    return arrID;
}

function getValueFormMutilSelect(form) {
    var arrParam = '';
    var idMselect;
   
    $(form).find("input,textarea,hidden,select").not("input[type='checkbox'], input[type='radio']:checked, input[name='selectItem'], .ms-search input, .mutil").each(function () {
        idMselect = $(this).attr("name");
        if ($(this).val() != '' && $(this).val() != 'Từ khóa tìm kiếm')
            arrParam += "&" + idMselect + "=" + $(this).val();
    });
    $("[data-id='SearchIn'].multiSelectSearch").each(function () {
        idMselect = $(this).attr("data-id");
        if (getValueMutilSelect(idMselect) != '')
            arrParam += "&" + idMselect + "=" + getValueMutilSelect(idMselect);
    });
    if (arrParam != '')
        arrParam = arrParam.substring(1);
    return arrParam;
}

function getValueGalaryFormMutilSelect(form) {
    var arrParam = '';
    var idMselect;
    $(form).find("input,textarea,hidden,select").not("input[type='checkbox'], input[type='radio']:checked").each(function () {
        idMselect = $(this).attr("name");
        if ($(this).val() != '' && $(this).val() != 'Từ khóa tìm kiếm')
            arrParam += "&" + idMselect + "=" + $(this).val();
    });
    //alert(arrParam);
    //$(".multiSelectSearch").each(function () {
    //    idMselect = $(this).attr("data-id");
    //    if (getValueMutilSelect(idMselect) != '')
    //        arrParam += "&" + idMselect + "=" + getValueMutilSelect(idMselect);
    //});
    //alert(arrParam);
    if (arrParam != '')
        arrParam = arrParam.substring(1);
    return arrParam;
}

function createMessage(title, message) {

    $.notify(message, 'success');
    
    $("#dialog-message").attr("title", title);
    $("#dialog-message").html("<p>" + message + "</p>");
    $("#dialog-message").dialog({
        resizable: true,
        height: 'auto',
        width: 'auto',
        modal: true,
        buttons: {
            "Đóng lại": function () {
                $(this).dialog("close");
            }
        }
    });
}

function Reorder(eSelect, iCurrentField, numSelects) {
    var eForm = eSelect.form;
    var iNewOrder = eSelect.selectedIndex + 1;
    var iPrevOrder;
    var positions = new Array(numSelects);
    var ix;
    for (ix = 0; ix < numSelects; ix++) {
        positions[ix] = 0;
    }
    for (ix = 0; ix < numSelects; ix++) {
        positions[eSelect.form["ViewOrder" + ix].selectedIndex] = 1;
    }
    for (ix = 0; ix < numSelects; ix++) {
        if (positions[ix] == 0) {
            iPrevOrder = ix + 1;
            break;
        }
    }
    if (iNewOrder != iPrevOrder) {
        var iInc = iNewOrder > iPrevOrder ? -1 : 1
        var iMin = Math.min(iNewOrder, iPrevOrder);
        var iMax = Math.max(iNewOrder, iPrevOrder);
        for (var iField = 0; iField < numSelects; iField++) {
            if (iField != iCurrentField) {
                if (eSelect.form["ViewOrder" + iField].selectedIndex + 1 >= iMin &&
					eSelect.form["ViewOrder" + iField].selectedIndex + 1 <= iMax) {
                    eSelect.form["ViewOrder" + iField].selectedIndex += iInc;
                }
            }
        }
    }
}


function getUrlParemeter(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}

function getValueOrder(form, input) {
    var values = '';
    $("#" + form).find("select").each(function (index) {
        values += "|" + $(this).attr("id") + '_' + $(this).val();
    });
    values = values.substring(1);
    $("#" + input).val(values);
}

function getEditorContent(instanceName) {
    if (typeof (FCKeditorAPI) !== 'undefined') {
        var oEditor = FCKeditorAPI.GetInstance(instanceName);
        return oEditor.GetHTML(true);
    }
}

function updateEditor() {
    for (var name in CKEDITOR.instances)
        CKEDITOR.instances[name].updateElement();
}

function getValueFromAutoTag(classUL, controlsFillvalue) {
    var contentValues = '';
    $("."+classUL+" li").each(function () {
        if($(this).attr("key") != 'undefined' && $(this).attr("key") != '')
            contentValues += "," + $(this).attr("key") + "_" + $(this).attr("name");
        else
            contentValues += "," + $(this).attr("name");
    });
    if (contentValues != '')
        contentValues = contentValues.substring(1);
    $("#" + controlsFillvalue).val(contentValues);
}

//add by BienLV 07-03-2014
//Convert currency
function ConvertCurrency(str) {
    return str.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
}

//add by PheLV 11-04-2014
//Convert currency
function RemoveUnicode(str) {

    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_|–|”|“|`/g, "-");

    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1- 
    str = str.replace(/^\-+|\-+$/g, "");

    return str;
}