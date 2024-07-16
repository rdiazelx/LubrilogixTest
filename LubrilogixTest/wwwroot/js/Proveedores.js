$(document).ready(function () {
    // Initialize DataTable for the main table
    let table = new DataTable('#proveedoresTable', {
        responsive: true,
        select: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        dom: 'lfrtip',
        order: [], // Prevent initial sorting
        columnDefs: [
            { orderable: false, targets: [6] } // Disable sorting for the Edit column
        ],
        buttons: [], // Disable all buttons
    });

    // Filter by Nombre
    $('#provinciaFilterIcon').on('click', function () {
        $('#nombreFilter').toggle();
    });
    $('#nombreFilter').on('keyup', function () {
        table.columns(1).search(this.value).draw();
    });

    // Filter by provincia
    $('#provinciaFilterIcon').on('click', function () {
        $('#provinciaFilter').toggle();
    });
    $('#provinciaFilter').on('keyup', function () {
        table.columns(3).search(this.value).draw();
    });


    // Set initial filter value and apply it
    $('#estadoFilter').val('Activo').trigger('keyup');

    // Apply the filter on page load
    table.columns(5).search('^Activo$', true, false).draw();

    // Filter by Estado
    $('#estadoFilterIcon').on('click', function () {
        $('#estadoFilter').toggle();
    });

    $('#estadoFilter').on('change', function () {
        const filterValue = this.value;
        table.columns(6).search(filterValue ? '^' + filterValue + '$' : '', true, false).draw();
    });

    // Handle Edit icon click
    $('#proveedoresTable tbody').on('click', '.editIcon', function () {
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
    $('#proveedoresTable tbody').on('click', '.saveIcon', function () {
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
