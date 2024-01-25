const _modeloProyecto = {
    ClaProyecto: 0,
    NomProyecto : ""
}

function mostrarProyectos() {
    fetch("/Proyecto/ListaProyectos")
    .then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    })
    .then(responseJson => {
        if(responseJson.length > 0){
            $("#tablaProyectos tbody").html("");

            responseJson.forEach((Proyecto) => {
                $("#tablaProyectos tbody").append(
                    $("<tr>").append(
                        $("<td>").text(Proyecto.claProyecto),
                        $("<td>").text(Proyecto.nomProyecto),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-primary btn-sm boton-editar-proyecto").text("Editar").data("dataProyecto", Proyecto),
                            $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-proyecto").text("Eliminar").data("dataProyecto", Proyecto)
                        )
                    )
                )
            })
        }
    })
}

document.addEventListener("DOMContentLoaded", function () {
    mostrarProyectos();

}, false)


function mostrarModal() {

    $("#ClaProyecto").val(_modeloProyecto.ClaProyecto);
    $("#NomProyecto").val(_modeloProyecto.NomProyecto);

    $("#modalProyecto").modal("show");
}

$(document).on("click", ".boton-nuevo-proyecto", function () {

    _modeloProyecto.ClaProyecto = 0;
    _modeloProyecto.NomProyecto = ""

    mostrarModal();

})

$(document).on("click", ".boton-editar-proyecto", function () {

    const _proyecto = $(this).data("dataProyecto");

    _modeloProyecto.ClaProyecto = _proyecto.claProyecto;
    _modeloProyecto.NomProyecto = _proyecto.nomProyecto;

    mostrarModal();


})


$(document).on("click", ".boton-guardar-cambios-proyecto", function () {

    const modelo = {
        ClaProyecto: $("#ClaProyecto").val(),
        NomProyecto: $("#NomProyecto").val()
    }

    console.log(modelo.ClaProyecto);

    if (modelo.ClaProyecto == 0) {

        fetch("/Proyecto/Insertar", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalProyecto").modal("hide");
                    Swal.fire("Listo!", "Proyecto fue creado", "success");
                    mostrarProyectos();
                    
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Proyecto/Editar", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalProyecto").modal("hide");
                    Swal.fire("Listo!", "Proyecto fue actualizado", "success");
                    mostrarProyectos();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})


$(document).on("click", ".boton-eliminar-proyecto", function () {

    const _proyecto = $(this).data("dataProyecto");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Proyecto "${_proyecto.NomProyecto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Proyecto/Eliminar/?ClaProyecto=${_proyecto.ClaProyecto}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Proyecto fue elminado", "success");
                        mostrarProyectos();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})