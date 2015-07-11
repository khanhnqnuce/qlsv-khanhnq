//create by BienLV 30-11-2013
(function ($) {
    $.fn.extend({
        clock: function (options) {

            var defaults = {
                optionDefault: 20
            };

            var obj = $.extend(defaults, options);

            var _this = $(this);
            _this.html("<p><span class='dateNow'></span> - <span class='hourNow'></span> : <span class='minuteNow'></span> : <span class='secondNow'></span></p>");

            var monthNames = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"];
            var dayNames = ["Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"];

            var newDate = new Date();
            newDate.setDate(newDate.getDate());  
            $('.dateNow').html(dayNames[newDate.getDay()] + ", " + newDate.getDate() + ' ' + monthNames[newDate.getMonth()] + ' ' + newDate.getFullYear());

            setInterval(function () {
                var seconds = new Date().getSeconds();
                $(".secondNow").html((seconds < 10 ? "0" : "") + seconds);
            }, 1000);

            setInterval(function () {
                var minutes = new Date().getMinutes();
                $(".minuteNow").html((minutes < 10 ? "0" : "") + minutes);
            }, 1000);

            setInterval(function () {
                var hours = new Date().getHours();
                $(".hourNow").html((hours < 10 ? "0" : "") + hours);
            }, 1000);
        }
    });
})(jQuery);