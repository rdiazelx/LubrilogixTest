$(document).ready(function () {
    // Initialize DataTable
    let table = $('#inventarioTable').DataTable({
        select: true,
        responsive: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        dom: 'lfrtip',
        order: [],
        columnDefs: [
            { orderable: false, targets: [14] } // Disable sorting for the Edit column if needed
        ],
        buttons: [
            {
                extend: 'csvHtml5',
                text: 'Export CSV',
                className: 'btn btn-primary',
                title: 'Inventario Data'
            },
            {
                extend: 'excelHtml5',
                text: 'Export Excel',
                className: 'btn btn-primary',
                title: 'Inventario Data'
            }
        ]
    });

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

      // Handle Edit icon click
    $('#inventarioTable tbody').on('click', '.editIcon', function () {
        let row = $(this).closest('tr');
        let data = table.row(row).data();

        // Enable textboxes for editing
        row.find('td').each(function (index) {
            let cell = $(this);
            if (index < 14) { // Adjust the range if necessary
                let originalValue = cell.text();
                let input = $('<input type="text">').val(originalValue);
                cell.html(input);
            }
        });

        // Toggle visibility of icons
        $(this).hide();
        row.find('.saveIcon').show();
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
        row.find('.editIcon').hide();
        row.find('.saveIcon').show();
    });

    // Handle Edit icon click
    $('#inventarioTable tbody').on('click', '.editIcon', function () {
        let row = $(this).closest('tr');
        let data = table.row(row).data();

        // Log the data to debug
        console.log('Original Data:', data);

        // Show input fields for editing
        row.children('td').not(':last').each(function (index) {
            let cell = $(this);
            let originalValue = cell.text().trim(); // Ensure we trim any extra spaces
            console.log(`Cell ${index} original value:`, originalValue);

            let input = $('<input type="text">').val(originalValue);
            cell.html(input);
        });

        // Toggle visibility of icons
        row.find('.editIcon').hide();
        row.find('.saveIcon').show();
    });

    // Handle Save icon click
    $('#inventarioTable tbody').on('click', '.saveIcon', function () {
        let row = $(this).closest('tr');
        let newData = [];

        // Collect updated values
        row.children('td').not(':last').each(function (index) {
            let cell = $(this);
            let input = cell.find('input');
            if (input.length) {
                let newValue = input.val().trim(); // Ensure we trim any extra spaces
                cell.text(newValue);
                newData.push(newValue);
            } else {
                newData.push(cell.text().trim()); // Add existing value if not edited
            }
        });

        // Add placeholder for the Edit column
        newData.push('<i class="fas fa-edit editIcon"></i><i class="fas fa-save saveIcon" style="display:none;"></i>');

        // Update the DataTable
        table.row(row).data(newData).draw();

        // Toggle visibility of icons
        row.find('.editIcon').show();
        $(this).hide();

        console.log('Updated row data:', newData);
        // Call a function to update the database with `newData` if necessary
    });
});
