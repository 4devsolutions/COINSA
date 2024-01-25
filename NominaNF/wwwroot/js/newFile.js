$(document).on("click", ".boton-editar-proyecto", function() {

    const _proyecto = $(this).data("dataProyecto");

    $("#ClaProyecto").val(_proyecto.ClaProyecto);
    $("#NomProyecto").val(_proyecto.NomProyecto);

    mostrarModal();


});
