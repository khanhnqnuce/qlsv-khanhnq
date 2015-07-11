(function ($) {
    function getURLParameter(url, name) {
        return (RegExp(name + '=' + '(.+?)(&|$)').exec(url) || [, null])[1];
    }
    $.fn.extend({
        expandoControl: function () {            
            var _this = $(this);
            _this.each(function (index, element) {
                var controller = $(element);
                var glyph = $("<span class=\"expando-glyph-container\"><span class=\"expando-glyph\"></span>&#8203;</span>");

                glyph.click(function () {
                    var __this = $(this);
                    if (__this.hasClass("open") || __this.hasClass("opening")) {
                        controller.parent().find('ul').slideUp(300, function () { $(this).removeClass('showing'); __this.removeClass("closing").removeClass("open opening").addClass("closed"); });
                        __this.addClass("closing");
                    }
                    else {                        
                        controller.parent().find('ul').slideDown(300, function () { $(this).addClass('showing'); __this.removeClass("opening").removeClass("closed").addClass("open"); });
                        __this.addClass("opening");
                    }

                    return false;
                });
                controller.prepend(glyph);
            });

            $('.menuItems li a').each(function () {
                
                $(this).parent().parent().parent().find('h3 span:first').addClass('closed');
                
                //alert($(this).attr(type));
                //var type = window.location.search.substring(1);
                
                if (window.location.pathname == "/Admin/ProductTopCategory") {
                    var windowhref = window.location.href;
                    var url = $(this).attr('href');
                    var cat = getURLParameter(url, 'CategoryID');
                    var cat2 = getURLParameter(windowhref, 'CategoryID');

                    //calling the ajax function

                    if (cat == cat2) {
                        var _parents = $(this).parent().parent().parent();
                        $(this).parent().parent().css({ 'display': 'block' }).addClass('showing');
                        $(this).parent().addClass('selected');
                        _parents.find('h3').addClass('selected');
                        _parents.find('h3 span:first').removeClass('closed').addClass('open opening');
                    }
                } else {


                    if (window.location.pathname == $(this).attr('href')) {
                        if (window.location.pathname != "/Admin/ProductTopCategory") {
                            var _parents = $(this).parent().parent().parent();
                            $(this).parent().parent().css({ 'display': 'block' }).addClass('showing');
                            $(this).parent().addClass('selected');
                            _parents.find('h3').addClass('selected');
                            _parents.find('h3 span:first').removeClass('closed').addClass('open opening');
                        }
                    }
                }
            });

            return this;
        }
    });
})(jQuery);