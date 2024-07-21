$(document).ready(function () {

    // Filter by Nombre
    $('#nombreFilterIcon').on('click', function () {
        $('#nombreFilter').toggle();
    });
    $('#nombreFilter').on('keyup', function () {
        table.column(1).search(this.value).draw();
    });

    // Filter by Categoría
    $('#categoriaFilterIcon').on('click', function () {
        $('#categoriaFilter').toggle();
    });
    $('#categoriaFilter').on('keyup', function () {
        table.column(2).search(this.value).draw();
    });

    // Filter by Subcategoría
    $('#subCategoriaFilterIcon').on('click', function () {
        $('#subCategoriaFilter').toggle();
    });
    $('#subCategoriaFilter').on('keyup', function () {
        table.column(3).search(this.value).draw();
    });


});