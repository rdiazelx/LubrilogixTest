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
    }

    // Cerrar el modal al hacer clic fuera de él
    window.onclick = function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    }

    // Manejar el evento de agregar proveedor
    saveProveedorButton.onclick = function () {
        var proveedorNombre = document.getElementById("proveedorNombre").value;
        var proveedorProducto = document.getElementById("proveedorProducto").value;
        var proveedorDireccion = document.getElementById("proveedorDireccion").value;
        var proveedorEmail = document.getElementById("proveedorEmail").value;

        // Lógica para enviar los datos al servidor
        console.log('Proveedor:', proveedorNombre);
        console.log('Producto:', proveedorProducto);
        console.log('Dirección:', proveedorDireccion);
        console.log('Email:', proveedorEmail);

        // Cerrar el modal después de agregar
        modal.style.display = "none";
    }
});
