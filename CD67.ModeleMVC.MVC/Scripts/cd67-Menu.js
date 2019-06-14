jQuery(function () {
    var $overlay = jQuery('.navbar-collapse');
    var $toggle = jQuery('#open-button');
    var toggleOverlay = function (evt) {
        if (!jQuery(evt.target).closest($overlay).length) {
            jQuery("body").removeClass("show-menu");
            $overlay.addClass('hidden');
            $toggle.removeClass('menu-button--open');
        }
        else {
            jQuery(document).one('click', toggleOverlay);
        }
    }
    $toggle.click(function (evt) {
        jQuery("body").toggleClass("show-menu");
        $toggle.toggleClass('menu-button--open');
        evt.preventDefault();
        evt.stopPropagation();
        $overlay.toggleClass('hidden');
        jQuery(document).one('click', toggleOverlay);
    });

    $('.menu-1').bind("click", function () {
        var menu = this, submenu = $(menu).closest("li").find(".submenu");
        $(".submenu").not(submenu).addClass("hidden");
        $(menu).closest("li").find(".submenu").toggleClass('hidden');
    });

    $('.icon-search').bind("click", function () {
        console.log("test");
        $(".input-search").toggleClass('hidden');
    });
});