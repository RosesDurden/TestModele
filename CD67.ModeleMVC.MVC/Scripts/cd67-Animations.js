// Class : animationFadeOnClick
// Au click, devient plus clair puis revient à une opacité normale
// Peut servir de feedback visuel (ex : avec la copie dans le presse papier)
$(document).on('click', ".animationFadeOnClick", function () {
    $(this).animate({
        opacity: '0.4'
    });
    $(this).animate({
        opacity: '1'
    });
});