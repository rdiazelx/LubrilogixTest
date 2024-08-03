/*Para obtener las referencias de los elementos botones*/
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("addProductModal");
    var openProductosBtn = document.getElementById("openProductosBtn");
    var closeModalBtn = document.getElementById("closeModalBtn");
    var saveProductButton = document.getElementById("saveProductButton");

    // Abrir el modal
    openProductosBtn.onclick = function () {
        modal.style.display = "block";
    }

    // Cuando el usuario le de click al boton cerrar
    closeModalBtn.onclick = function () {
        modal.style.display = "none";
        clearFormFields();//Funcion que se usa pra vaciar los espacios
    }

    // Cierra el modal cuando el usuario hace clic fuera del modal
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    // Agregar producto
    saveProductButton.onclick = function () {
        var productoNombreP = document.getElementById("productoNombreP").value;
        var productoCategoriaP = document.getElementById("productoCategoriaP").value;
        var productoSubP = document.getElementById("productoSubP").value;

        // Lógica para enviar los datos al servidor
        fetch('/Lubrilogix/AgregarProducto', { // La URL donde se envia la informacion
            method: 'POST', // Método HTTP POST para enviar datos
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                tcNombre: productoNombreP,
                tcCategoria: productoCategoriaP,
                tcSubcategoria: productoSubP
            })
        })
            .then(response => {
                if (response.ok) {
                    alert("Producto agregado exitosamente.");
                    document.getElementById('addProductForm').reset();
                    document.getElementById('addProductModal').style.display = 'none';
                } else {
                    return response.text().then(text => { throw new Error(text) });
                }
            })
            .catch(error => {
                alert("Error al agregar el producto: " + error.message);
                console.error('Error:', error);
            });
    }

// Función para vaciar los campos del formulario
function clearFormFields() {
    document.getElementById('addProductForm').reset();
}
});
