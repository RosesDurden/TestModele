jQuery(function($){
    $(window).bind('message', function(event){
        var oEvent = event.originalEvent;

        if (oEvent.origin == 'https://referentiel.bas-rhin.fr') {
            //console.log(oEvent.data);
            var datatype = oEvent.data.datatype, datasource = eval("oEvent.data."+ datatype);
            $.each(datasource, function(k, value) {
                var current = datatype + "." + k;
                console.log('type = ' + oEvent.data.type)
                var filtreType = '';
                if (oEvent.data.type != null) filtreType = "div[data-picker-type='" + oEvent.data.type + "'] ";
                if ($(filtreType + "input[data-picker='" + current + "']").length > 0) $(filtreType + "input[data-picker='" + current + "']").val(String(value).replace(/_@_/g, "'"));

                /* custom */
                var span = $(filtreType + "span[data-picker='" + current + "']");
                if (span.length > 0) {
                    span.closest(".panel").show();
                    span.html(String(value).replace(/_@_/g, "'"));
                }
            });
        }
        else return false;

        $(".modal-window").colorbox.close();
        
    });
});
