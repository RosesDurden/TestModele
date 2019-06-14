$(function () {
    $.datepicker.setDefaults($.datepicker.regional["fr"]);
    $(".datefield").datepicker();
});

//Override de la vérification du format de date pour éviter le problème de format
$(document).ready(function () {
    $('.calendarPicker').datepicker({ dateFormat: "dd/mm/yy" });
    if (typeof $.validator != 'undefined') {
        $.validator.addMethod('date',
            function (value, element, params) {
                if (this.optional(element)) {
                    return true;
                }

                var ok = true;
                try {
                    $.datepicker.parseDate('dd/mm/yy', value);
                }
                catch (err) {
                    ok = false;
                }
                return ok;
            });
    }
    else {
        console.log("pas de $.validator sur cette page");
        return true;
    }
});
