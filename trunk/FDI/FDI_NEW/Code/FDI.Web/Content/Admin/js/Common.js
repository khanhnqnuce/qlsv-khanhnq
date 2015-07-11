
// Form AddAjax
$("#btn_add").click(function () {
    $(document).on('keyup', '.autoPermalink', function (e) {
        e.preventDefault();
        $('#TitleAscii').val(RemoveUnicode($(this).val()));
    });

    $("#dialog-form").dialog(
        {
            title: "Thêm mới",
            resizable: true,
            height: formHeight,
            width: formWidth,
            modal: true,
            buttons: {
                "Cập nhật": function () {
                    $("#btnSave").click();
                },
                "Nhập lại": function () {
                    $("#btnReset").click();
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
    }).load(encodeURI(urlForm + "?do=Add")).dialog("open");

    return false;
});

function FdiDialog(url, title, height, width) {
    $("body").showLoading({
        'afterShow':
        function () {
            setTimeout("jQuery('#activity_pane').hideLoading()", 1000);
        }
    });
    $.post(url, function (data) {
        $("#dialog-form").html(data).dialog({
            title: title,
            resizable: true,
            height: height,
            width: width,
            modal: true,
            buttons: {
                "Cập nhật": function () {
                    $("#btnSave").click();
                },
                "Nhập lại": function () {
                    $("#btnReset").click();
                },
                "Đóng": function () {
                    $(this).dialog("close");
                }
            },
            show: {
                effect: "fade",
                duration: 500
            }
        }).dialogExtend({
            closable: true,
            maximizable: true,
            minimizable: true,
            minimizeLocation: "right",
            collapsable: true,
            dblclick: "maximize"
        }).dialog("open");

        $("body").hideLoading();
    });
}

function FdiDialogView(url, title, height, width) {
    $("body").showLoading({
        'afterShow':
        function () {
            setTimeout("jQuery('#activity_pane').hideLoading()", 1000);
        }
    });
    $.post(url, function (data) {
        $("#dialog-form").html(data).dialog({
            title: title,
            resizable: true,
            height: height,
            width: width,
            modal: true,
            buttons: { 
                "Đóng": function () {
                    $(this).dialog("close");
                }
            },
            show: {
                effect: "fade",
                duration: 500
            }
        }).dialogExtend({
            closable: true,
            maximizable: true,
            minimizable: true,
            minimizeLocation: "right",
            collapsable: true,
            dblclick: "maximize"
        }).dialog("open");

        $("body").hideLoading();
    });
}

// fix lỗi insert link in ckeditor
$.widget("ui.dialog", $.ui.dialog, {
    /*! jQuery UI - v1.10.2 - 2013-12-12
     *  http://bugs.jqueryui.com/ticket/9087#comment:27 - bugfix
     *  http://bugs.jqueryui.com/ticket/4727#comment:23 - bugfix
     *  allowInteraction fix to accommodate windowed editors
     */
    _allowInteraction: function (event) {
        if (this._super(event)) {
            return true;
        }

        // address interaction issues with general iframes with the dialog
        if (event.target.ownerDocument != this.document[0]) {
            return true;
        }

        // address interaction issues with dialog window
        if ($(event.target).closest(".cke_dialog").length) {
            return true;
        }

        // address interaction issues with iframe based drop downs in IE
        if ($(event.target).closest(".cke").length) {
            return true;
        }
    },
    /*! jQuery UI - v1.10.2 - 2013-10-28
     *  http://dev.ckeditor.com/ticket/10269 - bugfix
     *  moveToTop fix to accommodate windowed editors
     */
    _moveToTop: function (event, silent) {
        if (!event || !this.options.modal) {
            this._super(event, silent);
        }
    }
});
