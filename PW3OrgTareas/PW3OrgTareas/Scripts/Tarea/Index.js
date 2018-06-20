function completarTarea(element, idTarea) {
    $.ajax({
        dataType: 'html',
        type: 'POST',
        url: "/Tarea/CompletarTarea?idTarea=" + idTarea,
        async: true,
        success: function () {
            $(element).prop("disabled", true);
            $("#tdComplete_" + idTarea).text("Sí");
        }
    });
}