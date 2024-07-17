$(document).ready(function () {
    // Initialize DataTable for the Inventario table
    let table = new DataTable('#inventarioTable', {
        select: true,
        responsive: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        dom: 'lfrtip',
        order: [], // Prevent initial sorting
        columnDefs: [
            { orderable: false, targets: [10] } // Disable sorting for the Edit column
        ],
        buttons: [
            {
                extend: 'csvHtml5',
                text: 'Export CSV',
                className: 'btn btn-primary',
                title: 'Inventario Data' // Optional: Set the title for the CSV file
            },
            {
                extend: 'excelHtml5',
                text: 'Export Excel',
                className: 'btn btn-primary',
                title: 'Inventario Data' // Optional: Set the title for the Excel file
            }
        ],
    });

    // Filter by Fecha
    $('#fechaFilterIcon').on('click', function () {
        $('#fechaFilter').toggle();
    });
    $('#fechaFilter').on('keyup', function () {
        table.columns(1).search(this.value).draw();
    });

    // Filter by Sucursal
    $('#sucursalFilterIcon').on('click', function () {
        $('#sucursalFilter').toggle();
    });
    $('#sucursalFilter').on('keyup', function () {
        table.columns(2).search(this.value).draw();
    });

    // Filter by Producto
    $('#productoFilterIcon').on('click', function () {
        $('#productoFilter').toggle();
    });
    $('#productoFilter').on('keyup', function () {
        table.columns(3).search(this.value).draw();
    });

    // Filter by Estado
    $('#estadoFilterIcon').on('click', function () {
        $('#estadoFilter').toggle();
    });
    $('#estadoFilter').on('change', function () {
        table.columns(10).search(this.value).draw();
    });

    // Handle Edit icon click
    $('#inventarioTable tbody').on('click', '.editIcon', function () {
        let row = $(this).closest('tr');
        let data = table.row(row).data();

        // Show input fields for editing
        row.children('td').not(':last').each(function () {
            let cell = $(this);
            let originalValue = cell.text();
            let input = $('<input type="text">').val(originalValue);
            cell.html(input);
        });

        // Toggle visibility of icons
        $(this).hide();
        row.find('.saveIcon').show();
    });

    // Handle Save icon click
    $('#inventarioTable tbody').on('click', '.saveIcon', function () {
        let row = $(this).closest('tr');
        let newData = [];

        // Collect updated values
        row.children('td').not(':last').each(function () {
            let cell = $(this);
            let input = cell.find('input');
            let newValue = input.val();
            cell.text(newValue);
            newData.push(newValue);
        });

        // Add placeholder for the Edit column
        newData.push('<i class="fas fa-edit editIcon"></i><i class="fas fa-save saveIcon" style="display:none;"></i>');

        // Update the DataTable
        table.row(row).data(newData).draw();

        // Toggle visibility of icons
        row.find('.editIcon').show();
        $(this).hide();

        console.log('Updated row data:', newData);
        // Call a function to update the database with `newData`
    });
});
