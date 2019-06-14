$(document).ready(function () {
    //Init des variables pour l'export
    var today = new Date();
    var dd = today.getDate();
    var month = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    var hh = today.getHours();
    var mm = today.getMinutes();

    if (dd < 10) { dd = '0' + dd }
    if (month < 10) { month = '0' + month }
    if (hh < 10) { hh = '0' + hh }
    if (mm < 10) { mm = '0' + mm }
    today = dd + '/' + month + '/' +yyyy + ' ' + hh + 'h' + mm;

    //Définition de la table
    if ($('.datatable').length > 0) {
        $('.datatable').DataTable({
            "paging": true,
            "ordering": true,
            "info": false,
            "filter": true,
            dom: 'Bfrtip',
            "language": {
                "url": "/Content/DataTables/French.json"
            },
            buttons: [
                {
                    extend: 'excelHtml5',
                    title: 'Export des données du ' + today,
                },
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    title: 'Export des données du ' + today,
                    messageBottom: ""
                }],
            "lengthMenu": [[10, 30, -1], [10, 30, "Tout"]]
        });
    }
});
