
$(document).ready(function () {

    var url, data;
    $('.send').click(function (e) {
        //------------------------POST-----------------------
        url = "Ajax/File001A?t=" + Math.random();
        data = {
            "UserName:": $("#appForm :text[name='username']").val(),
            "Password:": $("#appForm :password[name='password']").val()
        }
        $.post(url, $("#appForm").serialize(), function (dt, status) {
            if (status == "success") {
                //$(".info").addClass("bg01").text("co loi xay ra");
                $(".info").addClass("bg01").text(dt);
                $("#appForm *").remove(".txtError");
                //$(".txtError").empty();
                $("#appForm :text[name='username']").addClass("bg01").after("<span class='txtError'>Sai tài khoản</span>");
                $("#appForm :password[name='password']").addClass("bg01").after("<span class='txtError'>Sai mật khẩu</span>");
            }
            //$("#info").html(dt);
        });
    });
})