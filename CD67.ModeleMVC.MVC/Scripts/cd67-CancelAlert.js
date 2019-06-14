// A utiliser sur les liens de retour
$(document).ready(function () {
    $(".confirm").click(function () {
        var texte = $(this).attr("data-texte");

        if (confirm(texte))
        {
            return true;
        }
        else
        {
            return false;
        }
    });
});


$(function () {
    if ($('form').length > 0) {
        $('form').areYouSure();
    }
});