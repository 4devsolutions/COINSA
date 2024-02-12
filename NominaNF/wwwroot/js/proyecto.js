const _modeloProyecto = {
    ClaProyecto: 0,
    NomProyecto : ""
}

function mostrarProyectos() {
    fetch("/Proyecto/ObtieneProyectos")
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
                        $("<td>").text(Proyecto.bajaLogica),
                        $("<td>").addClass("tdAcciones").append(
                            //$("<button>").addClass("btn btn-primary btn-sm boton-editar-usuario").text("Editar").data("dataUsuario", Usuario),
                            //$("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-usuario").text("Eliminar").data("dataUsuario", Usuario)
                            $("<button>").addClass("btn-primary boton-editar-proyecto").append(`<i class="icon-editar bi bi-pencil"></i>`).data("dataProyecto", Proyecto),
                            $("<button>").addClass("btn-danger boton-eliminar-proyecto").append(`<i class="bi bi-trash"></i>`).data("dataProyecto", Proyecto)
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

        fetch("/Proyecto/AgregaProyecto", {
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

        fetch("/Proyecto/EditaProyecto", {
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

    const modelo = {
        ClaProyecto: _proyecto.claProyecto,
        NomProyecto: _proyecto.nomProyecto
    }

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Proyecto "${_proyecto.nomProyecto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch("/Proyecto/EliminaProyecto", {
                method: "DELETE",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(modelo)
            })
            //fetch(`/Proyecto/Eliminar/?ClaProyecto=${_proyecto.ClaProyecto}`, {
            //    method: "DELETE"
            //})

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