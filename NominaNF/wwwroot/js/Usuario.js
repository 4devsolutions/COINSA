const _modeloUsuario = {
    claUsuario: 0,
    nomUsuario: "",
    apellidoPaterno: "",
    apellidoMaterno: "",
    claPuesto: 0,
    claProyecto: 0,
    claUbicacion: 0    
}


document.addEventListener("DOMContentLoaded", function () {
    mostrarUsuarios();
    fetchProyectos();
    fetchPuestos();
    fetchUbicaciones();

}, false)


function mostrarUsuarios() {
    fetch("/Usuario/ObtieneUsuarios")
    .then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    })
    .then(responseJson => {
        if(responseJson.length > 0){
            $("#tablaUsuarios tbody").html("");
            responseJson.forEach((Usuario) => {
                $("#tablaUsuarios tbody").append(
                    $("<tr>").append(
                        $("<td>").text(Usuario.claUsuario),
                        $("<td>").text(Usuario.nombre),
                        $("<td>").text(Usuario.apellidoPaterno),
                        $("<td>").text(Usuario.apellidoMaterno),
                        $("<td>").addClass("tdOculta").text(Usuario.claPuesto),
                        $("<td>").text(Usuario.nomPuesto),
                        $("<td>").addClass("tdOculta").text(Usuario.claProyecto),
                        $("<td>").text(Usuario.nomProyecto),
                        $("<td>").addClass("tdOculta").text(Usuario.claUbicacion),
                        $("<td>").text(Usuario.nomUbicacion),
                        $("<td>").text(Usuario.bajaLogica),
                        $("<td>").addClass("tdAcciones").append(
                            //$("<button>").addClass("btn btn-primary btn-sm boton-editar-usuario").text("Editar").data("dataUsuario", Usuario),
                            //$("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-usuario").text("Eliminar").data("dataUsuario", Usuario)
                             $("<button>").addClass("btn-primary boton-editar-usuario").append(`<i class="bi bi-pencil"></i>`).data("dataUsuario", Usuario),
                            $("<button>").addClass("btn-danger boton-eliminar-usuario").append(`<i class="bi bi-trash"></i>`).data("dataUsuario", Usuario)
                        )
                    )
                )
            })
        }
    })
}

$(document).on("click", ".boton-nuevo-Usuario", function () {

    _modeloUsuario.claUsuario = 0;
    _modeloUsuario.nomUsuario = "";
    _modeloUsuario.apellidoPaterno = "";
    _modeloUsuario.apellidoMaterno = "";
    _modeloUsuario.claPuesto = 0;
    _modeloUsuario.claProyecto = 0;
    _modeloUsuario.claUbicacion = 0;

    mostrarModal();

})

$(document).on("click", ".boton-editar-usuario", function () {

    const _Usuario = $(this).data("dataUsuario");

    _modeloUsuario.claUsuario = _Usuario.claUsuario;
    _modeloUsuario.nomUsuario = _Usuario.nombre;
    _modeloUsuario.apellidoPaterno = _Usuario.apellidoPaterno;
    _modeloUsuario.apellidoMaterno = _Usuario.apellidoMaterno;
    _modeloUsuario.claPuesto = _Usuario.claPuesto;
    _modeloUsuario.claProyecto = _Usuario.claProyecto;
    _modeloUsuario.claUbicacion = _Usuario.claUbicacion;

    mostrarModal();


})

document.addEventListener("click", function (event) {
    if (event.target.classList.contains("boton-guardar-cambios-usuario")) {

        var puestoValue = document.getElementById("puestoDropDown").value;
        var proyectoValue = document.getElementById("proyectoDropDown").value;
        var ubicacionValue = document.getElementById("ubicacionDropDown").value;

        var modelo = {
            claUsuario: document.getElementById("ClaUsuario").value,
            nombre: document.getElementById("NomUsuario").value,
            apellidoPaterno: document.getElementById("ApellidoPaterno").value,
            apellidoMaterno: document.getElementById("ApellidoMaterno").value,
            claPuesto: puestoValue,
            claProyecto: proyectoValue,
            claUbicacion: ubicacionValue
        };


        if (modelo.claUsuario == 0) {

            fetch("/Usuario/AgregaUsuario", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(modelo)
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        $("#modalUsuario").modal("hide");
                        Swal.fire("Listo!", "Usuario fue creado", "success");
                        mostrarUsuarios();

                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo crear", "error");
                })

        } else {

            fetch("/Usuario/EditaUsuario", {
                method: "PUT",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(modelo)
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        $("#modalUsuario").modal("hide");
                        Swal.fire("Listo!", "Usuario fue actualizado", "success");
                        mostrarUsuarios();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
                })

        }


    }
})

$(document).on("click", ".boton-eliminar-usuario", function () {

    
    const _Usuario = $(this).data("dataUsuario");

    const modelo = {
        ClaUsuario: _Usuario.claUsuario,
        NomUsuario: _Usuario.nomUsuario
    }

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Usuario "${_Usuario.nomUsuario}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch("/Usuario/EliminaUsuario", {
                method: "DELETE",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(modelo)
            })
            //fetch(`/Usuario/Eliminar/?ClaUsuario=${_Usuario.ClaUsuario}`, {
            //    method: "DELETE"
            //})

                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Usuario fue elminado", "success");
                        mostrarUsuarios();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})

function mostrarModal() {

    $("#ClaUsuario").val(_modeloUsuario.claUsuario);
    $("#NomUsuario").val(_modeloUsuario.nomUsuario);
    $("#ApellidoPaterno").val(_modeloUsuario.apellidoPaterno);
    $("#ApellidoMaterno").val(_modeloUsuario.apellidoMaterno);
    $("#puestoDropDown").val(_modeloUsuario.claPuesto);
    $("#proyectoDropDown").val(_modeloUsuario.claProyecto);
    $("#ubicacionDropDown").val(_modeloUsuario.claUbicacion);

   
    $("#modalUsuario").modal("show");
}

/* Combo Proyectos */


// Function to fetch projects from the controller route
async function fetchProyectos() {
    try {
        const response = await fetch('/ProyectoCmb/ObtieneProyectosCmb');
        if (!response.ok) {
            throw new Error('Failed to fetch proyectos');
        }
        const proyectos = await response.json();

        populateDropdown1(proyectos);
    } catch (error) {
        console.error('Error fetching proyectos:', error);
    }
}

// Function to populate the dropdown with projects
function populateDropdown1(proyectos) {


    const dropdown = document.getElementById('proyectoDropDown');

    dropdown.innerHTML = ''; // Clear previous options
    proyectos.forEach(proyecto => {
        const option = document.createElement('option');
        option.value = proyecto.claProyecto;
        option.textContent = proyecto.nomProyecto;
        dropdown.appendChild(option);

        dropdown.value = "";
    });
}

// Event listener for the change event of the dropdown
document.getElementById('proyectoDropDown').addEventListener('change', async function () {
    const selectedProjectId = this.value;
    // Make an AJAX request or perform any desired action with the selected project ID
    //console.log('Selected Proyecto ID:', selectedProjectId);
});


/* Combo Puestos */


// Function to fetch puestos from the controller route
async function fetchPuestos() {
    try {
        const response = await fetch('/PuestoCmb/ObtienePuestosCmb');
        if (!response.ok) {
            throw new Error('Failed to fetch puestos');
        }
        const puestos = await response.json();

        populateDropdown2(puestos);
    } catch (error) {
        console.error('Error fetching puestos:', error);
    }
}

// Function to populate the dropdown with projects
function populateDropdown2(puestos) {
    const dropdown = document.getElementById('puestoDropDown');
    dropdown.innerHTML = ''; // Clear previous options
    puestos.forEach(puesto => {
        const option = document.createElement('option');
        option.value = puesto.claPuesto;
        option.textContent = puesto.nomPuesto;
        dropdown.appendChild(option);

        dropdown.value = "";
    });
}

// Event listener for the change event of the dropdown
document.getElementById('puestoDropDown').addEventListener('change', async function () {
    const selectedProjectId = this.value;
    // Make an AJAX request or perform any desired action with the selected project ID
    //console.log('Selected Puesto ID:', selectedProjectId);
});


/* Combo Ubicaciones */


// Function to fetch ubicaciones from the controller route
async function fetchUbicaciones() {
    try {
        const response = await fetch('/UbicacionCmb/ObtieneUbicacionesCmb');
        if (!response.ok) {
            throw new Error('Failed to fetch ubicacion');
        }
        const ubicaciones = await response.json();

        populateDropdown3(ubicaciones);
    } catch (error) {
        console.error('Error fetching ubicaciones:', error);
    }
}

// Function to populate the dropdown with projects
function populateDropdown3(ubicaciones) {
    const dropdown = document.getElementById('ubicacionDropDown');
    dropdown.innerHTML = ''; // Clear previous options
    ubicaciones.forEach(ubicacion => {
        const option = document.createElement('option');
        option.value = ubicacion.claUbicacion;
        option.textContent = ubicacion.nomUbicacion;
        dropdown.appendChild(option);

        dropdown.value = "";
    });
}

// Event listener for the change event of the dropdown
document.getElementById('ubicacionDropDown').addEventListener('change', async function () {
    const selectedProjectId = this.value;
    // Make an AJAX request or perform any desired action with the selected project ID
    //console.log('Selected Ubicacion ID:', selectedProjectId);
});


