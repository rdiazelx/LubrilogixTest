document.addEventListener('DOMContentLoaded', function () {
    // Obtener referencias a los elementos del DOM
    var openModalBtn = document.getElementById('openRegistrarProBtn');
    var closeModalBtn = document.getElementById('closeModalBtn');
    var saveProveedorBtn = document.getElementById('saveProveedorButton');
    var modal = document.getElementById('addRegistrarProModal');
    var form = document.getElementById('addRegistrarProForm');

    // Verificar si la página actual es la página de inicio
    var isIndexPage = document.body.getAttribute('data-page') === 'true';

    // Mostrar u ocultar el botón según la página
    if (!isIndexPage && openModalBtn) {
        openModalBtn.style.display = 'block';
    }

    // Abre el modal cuando se hace clic en el botón correspondiente
    openModalBtn.addEventListener('click', function () {
        modal.style.display = 'block';
    });

    // Cierra el modal y limpia los campos del formulario cuando se hace clic en el botón de cerrar
    closeModalBtn.addEventListener('click', function () {
        modal.style.display = 'none';
        clearFormFields();
    });

    // Guarda el proveedor cuando se hace clic en el botón de guardar
    saveProveedorBtn.addEventListener('click', function () {
        // Obtener valores de los campos del formulario
        var proveedorNombre = document.getElementById("proveedorNombre").value;
        var proveedorProvincia = document.getElementById("proveedorProvincia").value;
        var proveedorDireccion = document.getElementById("proveedorDireccion").value;
        var proveedorEmail = document.getElementById("proveedorEmail").value;

        // Validar campos del formulario
        if (!proveedorNombre || !proveedorProvincia || !proveedorDireccion || !proveedorEmail) {
            alert("Todos los campos son requeridos.");
            return;
        }

        // Enviar los datos al servidor usando fetch
        fetch('/Home/RegistroProveedor', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                TcNombre: proveedorNombre,
                TcProvincia: proveedorProvincia,
                TcDireccion: proveedorDireccion,
                TcEmail: proveedorEmail
                // TcEstado se establecerá por defecto en el controlador
            })
        })
            .then(response => {
                if (response.ok) {
                    // Si la respuesta es exitosa, muestra un mensaje y cierra el modal
                    alert("Proveedor agregado exitosamente.");
                    form.reset(); // Resetea el formulario
                    modal.style.display = 'none';
                } else {
                    // Si la respuesta no es exitosa, lanza un error con el mensaje del servidor
                    return response.text().then(text => { throw new Error(text) });
                }
            })
            .catch(error => {
                // Muestra un mensaje de error en caso de fallo
                alert("Error al agregar el proveedor: " + error.message);
                console.error('Error:', error);
            });
    });

    // Cierra el modal si se hace clic fuera del área del modal
    window.addEventListener('click', function (event) {
        if (event.target === modal) {
            modal.style.display = 'none';
            clearFormFields();
        }
    });

    // Limpia los campos del formulario
    function clearFormFields() {
        form.reset();
    }
});

