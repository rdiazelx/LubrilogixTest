$(document).ready(function () {
    // Ensure userInSpecificGroup is defined
    if (typeof userInSpecificGroup === 'undefined') {
        console.error('userInSpecificGroup is not defined.');
        return;
    }

    // Initialize DataTable for the main table
    let table = $('#proveedoresTable').DataTable({
        responsive: true,
        select: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        dom: 'lfrtip',
        order: [], // Prevent initial sorting
        columnDefs: [
            { orderable: false, targets: userInSpecificGroup ? [6] : [] } // Disable sorting for the Edit column if present
        ],
        columns: [
            { data: "ID Proveedor" },
            { data: "Nombre" },
            { data: "Direccion" },
            { data: "Provincia" },
            { data: "Email" },
            { data: "Estado" },
            { data: userInSpecificGroup ? "Edit" : null } // Include Edit column if present
        ]
    });

    if (userInSpecificGroup) {
        table.column(6).visible(true); // Show the Edit column (6th column)
        $('.editColumn').show(); // Make sure the placeholder column is shown
    } else {
        table.column(6).visible(false); // Hide the Edit column (6th column)
    }

    // Filter by Nombre
    $('#nombreFilter').on('keyup', function () {
        console.log('Filtering by Nombre:', this.value); // Debugging line
        table.column(1).search(this.value).draw();
    });

    // Filter by Provincia
    $('#provinciaFilter').on('keyup', function () {
        console.log('Filtering by Provincia:', this.value); // Debugging line
        table.column(3).search(this.value).draw();
    });

    // Set initial filter value and apply it
    $('#estadoFilter').val('Activo').trigger('change');

    // Apply the filter on page load
    table.column(5).search('^Activo$', true, false).draw();

    // Filter by Estado
    $('#estadoFilter').on('change', function () {
        const filterValue = this.value;
        console.log('Filtering by Estado:', filterValue); // Debugging line
        table.column(5).search(filterValue ? '^' + filterValue + '$' : '', true, false).draw();
    });

    // Handle Edit icon click
    $('#proveedoresTable tbody').on('click', '.editIcon', function () {
        let row = $(this).closest('tr');
        let data = table.row(row).data();

        // Show input fields for editing
        row.children('td').not('.editColumn').each(function () {
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
        row.children('td').not('.editColumn').each(function () {
            let cell = $(this);
            let input = cell.find('input');
            let newValue = input.val();
            cell.text(newValue);
            newData.push(newValue);
        });

        // Add placeholder for the Edit column if it exists
        if (userInSpecificGroup) {
            newData.push('<i class="fas fa-edit editIcon"></i><i class="fas fa-save saveIcon" style="display:none;"></i>');
        }

        // Update the DataTable
        table.row(row).data(newData).draw();

        // Toggle visibility of icons
        row.find('.editIcon').show();
        $(this).hide();

        console.log('Updated row data:', newData);
        // Call a function to update the database with `newData`
    });
});
