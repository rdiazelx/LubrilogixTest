
/*Para obtener las referencias de los elementos botones*/
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("addProductModal");
    var openModalBtn = document.getElementById("openModalBtn");
    var closeModalBtn = document.getElementById("closeModalBtn");
    var saveProductButton = document.getElementById("saveProductButton");

    // Abrir el modal
    openModalBtn.onclick = function () {
        modal.style.display = "block";
    }

    //Cuando el usuario le de click al boton cerrar
    closeModalBtn.onclick = function () {
        modal.style.display = "none";
    }
    
    //Cierra el modal cuando el usuario hace clic fuera del modal
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    //Maneja el evento de agregar el producto
    saveProductButton.onclick = function () {
        //Obtiene la informacion de los campos del form
        var productoNombre = document.getElementById("productoNombre").value;
        var productoCategoria = document.getElementById("productoCategoria").value;
        var productoSub = document.getElementById("productoSub").value;
        //logica para enviar los datos al servidos
        console.log('Producto:', productoNombre);
        console.log('Catagoria:', productoCategoria);
        console.log('Subcategaria:', productoSub);

        // Cerrar el modal después de agregar
        modal.style.display = "none";
    }
    
});