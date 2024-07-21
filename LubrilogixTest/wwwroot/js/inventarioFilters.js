$(document).ready(function () {


    // Filter by ID Orden
    $('#idOrdenFilterIcon').on('click', function () {
        $('#idOrdenFilter').toggle();
    });
    $('#idOrdenFilter').on('keyup', function () {
        table.column(0).search(this.value).draw();
    });

    // Filter by Fecha
    $('#fechaFilterIcon').on('click', function () {
        $('#fechaFilter').toggle();
    });
    $('#fechaFilter').on('keyup', function () {
        table.column(1).search(this.value).draw();
    });

    // Filter by ID Sucursal
    $('#sucursalFilterIcon').on('click', function () {
        $('#sucursalFilter').toggle();
    });
    $('#sucursalFilter').on('keyup', function () {
        table.column(2).search(this.value).draw();
    });

    // Filter by IdProducto
    $('#productoFilterIcon').on('click', function () {
        $('#productoFilter').toggle();
    });
    $('#productoFilter').on('keyup', function () {
        table.column(4).search(this.value).draw();
    });

    // Filter by Nombre Producto
    $('#nombreProductoFilterIcon').on('click', function () {
        $('#nombreProductoFilter').toggle();
    });
    $('#nombreProductoFilter').on('keyup', function () {
        table.column(5).search(this.value).draw();
    });

    // Filter by Nombre Tipo Operacion
    $('#tipoOperacionFilterIcon').on('click', function () {
        $('#tipoOperacionFilter').toggle();
    });
    $('#tipoOperacionFilter').on('change', function () {
        table.column(8).search(this.value).draw();
    });

    // Filter by Nombre Proveedor
    $('#proveedorFilterIcon').on('click', function () {
        $('#proveedorFilter').toggle();
    });
    $('#proveedorFilter').on('keyup', function () {
        table.column(11).search(this.value).draw();
    });

    // Filter by Estado
    $('#estadoFilterIcon').on('click', function () {
        $('#estadoFilter').toggle();
    });
    $('#estadoFilter').on('change', function () {
        table.column(13).search(this.value).draw();
    });
});