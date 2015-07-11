
//$(window).load(function () {
//    //var errorDiv = $('.TKW-home:visible').first();
//    //var scrollPos = errorDiv.offset().top;
//    //alert(scrollPos);
//    $(window).scrollTop(515);
//}); 

$(document).ready(function () {
    var owl = $("#owl-demo");
    owl.owlCarousel({
        items: 2, //10 items above 1000px browser width
        itemsDesktop: [1000, 2], //5 items between 1000px and 901px
        itemsDesktopSmall: [900, 2], // betweem 900px and 601px
        itemsTablet: [600, 2], //2 items between 600 and 0
        itemsMobile: [500, 1]// itemsMobile disabled - inherit from itemsTablet option
    });

    $("#dichvu-owl-demo").owlCarousel({
        autoPlay: 3000,
        stopOnHover: true,
        navigation: true,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true
    });

    $(".fancybox-effects-c").fancybox({
        wrapCSS: 'fancybox-custom',
        closeClick: true,

        openEffect: 'none',

        helpers: {
            title: {
                type: 'inside'
            },
        }
    });

    $(".dropdown").hover(
            function () { $('.dropdown-menu', this).fadeIn(0); },
            function () { $('.dropdown-menu', this).fadeOut(0); }
        );
});


$(function () {
    
});

/*==SCROLL TOP==*/
$(function () {
    $(".FDI-uptop").hide();
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.FDI-uptop').fadeIn();
        } else {
            $('.FDI-uptop').fadeOut();
        }
    });
    $('.FDI-uptop a').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 500);
        return false;
    });
});


new WOW().init();

new UISearch(document.getElementById('sb-search'));


//jQuery(document).ready(function () {
//    jQuery("img.product-img").lazy({
//        delay: 100,
//    });
//});
