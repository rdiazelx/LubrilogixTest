$(document).ready(function () {
    // Initialize DataTable for the main table
    let table = new DataTable('#myTable', {
        responsive: true,
        select: true,
        dom: 'Bfrtip',
        order: [], // Prevent initial sorting
        columnDefs: [
            { orderable: false, targets: [3] } // Disable sorting for the Edit column
        ],
        buttons: [], // Disable all buttons
    });

    // Filter by Nombre
    $('#nombreFilterIcon').on('click', function () {
        $('#nombreFilter').toggle();
    });
    $('#nombreFilter').on('keyup', function () {
        table.columns(0).search(this.value).draw();
    });

    // Filter by Apellido
    $('#apellidoFilterIcon').on('click', function () {
        $('#apellidoFilter').toggle();
    });
    $('#apellidoFilter').on('keyup', function () {
        table.columns(1).search(this.value).draw();
    });

    // Filter by Genero
    $('#generoFilterIcon').on('click', function () {
        $('#generoFilter').toggle();
    });
    $('#generoFilter').on('keyup', function () {
        table.columns(2).search(this.value).draw();
    });

    // Handle Edit icon click
    $('#myTable tbody').on('click', '.editIcon', function () {
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
    $('#myTable tbody').on('click', '.saveIcon', function () {
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
