
$(document).ready(function () {

    var url, data;

    $('.process').click(function (e) {
        //---------------------LOAD-----------------------------------
        /*url = "Ajax/File001";
        $("#info").load(url);

        ------- .load(url,data) ------
        url = "Ajax/File001A?mode=edit&t=" + Math.random();
        data = { "title": "demo ajax", "value": "send content" }
        $("#info").load(url, data);

        ------- .load(url,data) ------

        url = "Ajax/File001A?t=" + Math.random();
        $("#info").load(url, function (response,status,xmlhttpRequest) {
            console.log(response);
        });

        url = "Models/ajaxdemo.xml";
        $("#info").load(url, function (response,status,xmlhttpRequest) {
            console.log(response);
            alert(response);
        });*/

        //---------------------------GET------------------------------------

        /*url = "Ajax/File001?t=" + Math.random() + "&id=10&name=Apple";
        $.get(url, function (dt, status, jqXhr) {
            console.log($.type(dt));
            $('.info').html(dt);
            $('.info').append(dt);
            $(dt).appendTo('.info');
            $(dt).prependTo('.info');
            $('.info').after(dt);
            $('.info').before(dt);
            $('.info').prepend(dt);
            });

        url = "Ajax/File001?t=" + Math.random();
        data = { "id": "10", "name": "Apple" }
        $.get(url, data, function (dt, status, jqXhr) {
            console.log($.type(dt) + " " + status);
            $('.info').html(dt);
        });*/

        /*url = "Models/ajaxdemo.xml";
        $.get(url, function (dt) {
            console.log(dt);
            console.log($(dt).find("to"));
            var txt = "";
            $(dt).find("to").each(function (i, val) {
                //console.log(i);
                //console.log(val);

                console.log($(val).text());
                txt += $(this).text() + "<br/>";
            });
            $('.info').html(txt);
        }, 'xml'); //xml, script, html,json*/

        url = "Ajax/FileJson";
        $.get(url,function (dt) {
            console.log(dt);
            var t = " ";
            $.each(dt, function (i, val) {
                t += i + ": " + val + "<br/>";
            });

            //$.each($.parseJSON(dt), function (i, val) {
            //    t += i + ": " + val + "<br/>";
            //});//dùng khi datatype không phải là json
            $('.info').html(t);
        },'json');

        /*url = "Content/Test.js";
        $.get(url,function(dt) {
            console.log(dt);
            Hello();
        });*/
    });
    
})