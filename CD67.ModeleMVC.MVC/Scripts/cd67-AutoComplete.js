// autocompletion multiterme le champ recherche_autocomplete
var el = $('.auto');
$('.auto').autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/' + $(el).attr("data-auto") + '/AutoComplete',
            dataType: 'json',
            data: {
                terms: request.term
            },
            success: function (data) {
                response($.map(data.facet_counts.facet_fields.recherche, function (item) {
                    return {
                        label: item[0] + ' (' + item[1] + ')',
                        value: item[0]
                    };
                }));
            }
        });
    }
});
