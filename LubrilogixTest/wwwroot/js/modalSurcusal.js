document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("addSucursalModal");
    var openSurcusalBtn = document.getElementById("openSurcusalBtn");
    var closeModalBtn = document.getElementById("closeModalBtn");
    var saveSucursalButton = document.getElementById("saveSucursalButton");

    if (!modal || !openSurcusalBtn || !closeModalBtn || !saveSucursalButton) {
        console.error("Uno o más elementos no se encuentran en el DOM.");
        return;
    }

    // Abrir el modal
    openSurcusalBtn.onclick = function () {
        modal.style.display = "block";
    }

    // Cerrar el modal
    closeModalBtn.onclick = function () {
        modal.style.display = "none";
        clearFormFields();//Funcion que se usa pra vaciar los espacios
    }

    // Cerrar el modal al hacer clic fuera de él
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

// Manejador para el botón de guardar sucursal
document.getElementById('saveSucursalButton').onclick = function () {
    // Obtener los valores de los campos del formulario de sucursal
    var sucursalNombre = document.getElementById("sucursalNombre").value;
    var sucursalProvincia = document.getElementById("sucursalProvincia").value;
    var sucursalDireccion = document.getElementById("sucursalDireccion").value;
    var sucursalTelefono = document.getElementById("sucursalTelefono").value;
    var sucursalCorreo = document.getElementById("sucursalCorreo").value;
    var sucursalEstado = document.getElementById("sucursalEstado").value;
    var sucursalComentario = document.getElementById("sucursalComentario").value;

    // Lógica para enviar los datos al servidor
    fetch('/Lubrilogix/AgregarSucursal', { //Donde se envia la informacion
        method: 'POST',// Método HTTP POST para enviar datos
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            TcNombre: sucursalNombre,
            TcProvincia: sucursalProvincia,
            TcDireccion: sucursalDireccion,
            TcTelefono: sucursalTelefono,
            TcCorreo: sucursalCorreo,
            TcEstado: sucursalEstado,
            TcComentarios: sucursalComentario
        })
    })
        .then(response => {
            if (response.ok) {
                alert("Sucursal agregada exitosamente.");
                document.getElementById('addSucursalForm').reset();
                document.getElementById('addSucursalModal').style.display = 'none';
            } else {
                return response.text().then(text => { throw new Error(text) });
            }
        })
        .catch(error => {
            alert("Error al agregar la sucursal: " + error.message);
            console.error('Error:', error);
        });
}

// Función para vaciar los campos del formulario
function clearFormFields() {
    document.getElementById('addSucursalForm').reset();
}
});

