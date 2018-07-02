function ocultarCompletadas(element) {
    var rows = $("#misTareas").find("tr");

    $.each(rows, function (index, row) {
        if ($(row).data("completada").toLowerCase() === "true") {
            if ($(element).prop("checked") == true) {
                $(row).hide();
            }
            else {
                $(row).show();
            }
        }
    });
}

function ocultarNoCompletadas(element) {
    var rows = $("#misTareas").find("tr");

    $.each(rows, function (index, row) {
        if ($(row).data("completada").toLowerCase() === "false") {
            if ($(element).prop("checked") == true) {
                $(row).hide();
            }
            else {
                $(row).show();
            }
        }
    });
}