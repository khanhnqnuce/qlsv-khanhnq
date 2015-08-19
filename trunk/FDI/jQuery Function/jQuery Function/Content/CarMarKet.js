// tạo dialog
function createMessage(title, message) {

    //$.notify(message, 'success');

    $("#dialog-message").attr("title", title);
    $("#dialog-message").html("<p>" + message + "</p>");
    $("#dialog-message").dialog({
        resizable: true,
        height: 'auto',
        width: 'auto',
        modal: true,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");
            }
        }
    });
}