$(document).ready(function () {
   
    // Toggle filter input visibility on filter icon click
    $('#nombreFilterIcon').on('click', function () {
        $('#nombreFilter').toggle();
    });

    $('#provinciaFilterIcon').on('click', function () {
        $('#provinciaFilter').toggle();
    });

    $('#estadoFilterIcon').on('click', function () {
        $('#estadoFilter').toggle();
    });

    // Filter by Nombre
    $('#nombreFilter').on('keyup', function () {
        console.log('Filtering by Nombre:', this.value); // Debugging line
        $('#proveedoresTable').DataTable().column(1).search(this.value).draw();
    });

    // Filter by Provincia
    $('#provinciaFilter').on('keyup', function () {
        console.log('Filtering by Provincia:', this.value); // Debugging line
        $('#proveedoresTable').DataTable().column(3).search(this.value).draw();
    });

    // Set initial filter value and apply it
    $('#estadoFilter').val('Activo').trigger('change');

    // Apply the filter on page load
    $('#proveedoresTable').DataTable().column(5).search('^Activo$', true, false).draw();

    // Filter by Estado
    $('#estadoFilter').on('change', function () {
        const filterValue = this.value;
        console.log('Filtering by Estado:', filterValue); // Debugging line
        $('#proveedoresTable').DataTable().column(5).search(filterValue ? '^' + filterValue + '$' : '', true, false).draw();
    });
});
