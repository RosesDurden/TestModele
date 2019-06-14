$(document).ready(function () {
    //gestion des listes déroulantes imbriquées
    $(".liste-principale").change(function () {
        var id = $(this).val();
        console.log(id);
        var dataurl = $(this).attr("data");
        $.ajax({
            url: dataurl,
            dataType: 'json',
            data: { Id: id },
            success: function (data) {
                var select = $(".liste-secondaire");
                select.empty();
                $.each(data, function (index, itemData) {
                    select.append($('<option/>', {
                        value: itemData.Value != null ? itemData.Value : '',
                        text: itemData.Text
                    }));
                });

                //On appelle explicitement la fonction de changement sur la seconde liste
                if ($('.liste-secondaire option').length > 1) $('.liste-secondaire').change();
            }
        });
    });
});
