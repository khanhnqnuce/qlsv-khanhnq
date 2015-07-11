var Layout = function () {
    var handleSearch = function () {
        $('.search-btn').click(function () {
            if ($('.search-btn').hasClass('show-search-icon')) {
                if ($(window).width() > 767) {
                    $('.search-box').fadeOut(300);
                } else {
                    $('.search-box').fadeOut(0);
                }
                $('.search-btn').removeClass('show-search-icon');
            } else {
                if ($(window).width() > 767) {
                    $('.search-box').fadeIn(300);
                } else {
                    $('.search-box').fadeIn(0);
                }
                $('.search-btn').addClass('show-search-icon');
            }
        });
    }
    return {
        init: function () {
            handleSearch();
        },
    };
}();