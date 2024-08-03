document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("addProveedorModal");
    var openModalProveedorBtn = document.getElementById("openModalProveedorBtn");
    var closeModalBtn = document.getElementById("closeModalBtn");
    var saveProveedorButton = document.getElementById("saveProveedorButton");

    // Verifica si los elementos existen en el DOM
    if (!modal || !openModalProveedorBtn || !closeModalBtn || !saveProveedorButton) {
        console.error("Uno o más elementos no se encuentran en el DOM.");
        return;
    }

    // Abrir el modal
    openModalProveedorBtn.onclick = function () {
        modal.style.display = "block";
    }

    // Cerrar el modal
    closeModalBtn.onclick = function () {
        modal.style.display = "none";
        clearFormFields(); // Función que se usa para vaciar los espacios
    }

    // Cerrar el modal al hacer clic fuera de él
    window.onclick = function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    }

    // Función para manejar el clic en el botón "Agregar proveedor"
    saveProveedorButton.onclick = function () {
        // Obtener los valores de los campos del formulario
        var proveedorNombre = document.getElementById('proveedorNombre').value;
        var proveedorProvincia = document.getElementById('proveedorProvincia').value;
        var proveedorDireccion = document.getElementById('proveedorDireccion').value;
        var proveedorEmail = document.getElementById('proveedorEmail').value;
        var proveedorEstado = document.getElementById('proveedorEstado').value;

        // Enviar los datos al servidor
        fetch('/Lubrilogix/AgregarProveedor', { // La URL donde se envía la información
            method: 'POST', // Método HTTP POST para enviar datos
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                TcNombre: proveedorNombre,
                TcProvincia: proveedorProvincia,
                TcDireccion: proveedorDireccion,
                TcEmail: proveedorEmail,
                TcEstado: proveedorEstado
            })
        })
            .then(response => {
                if (response.ok) {
                    alert("Proveedor agregado exitosamente.");
                    document.getElementById('addProveedorForm').reset(); // Limpiar el formulario
                    document.getElementById('addProveedorModal').style.display = 'none'; // Cerrar el modal
                } else {
                    return response.text().then(text => { throw new Error(text) });
                }
            })
            .catch(error => {
                alert("Error al agregar el proveedor: " + error.message);
                console.error('Error:', error);
            });
    }

    function clearFormFields() {
        document.getElementById('addProveedorForm').reset();
    }
});
