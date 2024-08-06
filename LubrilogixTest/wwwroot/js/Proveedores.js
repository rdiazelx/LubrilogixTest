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
            { data: "TnIdProveedor" },
            { data: "TcNombre" },
            { data: "TcDireccion" },
            { data: "TcProvincia" },
            { data: "TcEmail" },
            { data: "TcEstado" },
            { data: userInSpecificGroup ? "Edit" : null } // Include Edit column if present
        ]
    });

    if (userInSpecificGroup) {
        table.column(6).visible(true); // Show the Edit column (6th column)
        $('.editColumn').show(); // Make sure the placeholder column is shown
    } else {
        table.column(6).visible(false); // Hide the Edit column (6th column)
    }

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
        let newData = {};

        // Collect updated values
        row.children('td').not('.editColumn').each(function (index) {
            let cell = $(this);
            let input = cell.find('input');
            let newValue = input.val();
            cell.text(newValue);

            // Map new values to corresponding data properties
            switch (index) {
                case 0:
                    newData["TnIdProveedor"] = newValue;
                    break;
                case 1:
                    newData["TcNombre"] = newValue;
                    break;
                case 2:
                    newData["TcDireccion"] = newValue;
                    break;
                case 3:
                    newData["TcProvincia"] = newValue;
                    break;
                case 4:
                    newData["TcEmail"] = newValue;
                    break;
                case 5:
                    newData["TcEstado"] = newValue;
                    break;
            }
        });

        // Add placeholder for the Edit column if it exists
        if (userInSpecificGroup) {
            newData["Edit"] = '<i class="fas fa-edit editIcon"></i><i class="fas fa-save saveIcon" style="display:none;"></i>';
        }

        // Update the DataTable
        table.row(row).data(newData).draw();

        // Toggle visibility of icons
        row.find('.editIcon').show();
        $(this).hide();

        console.log('Updated row data:', newData);

        // Call a function to update the database with newData
        updateProveedorData(newData);
    });

    function updateProveedorData(data) {
        console.log('Updating provider with data:', data);

        // Construct the query string
        let queryString = `?TnIdProveedor=${data.TnIdProveedor}&TcNombre=${encodeURIComponent(data.TcNombre)}&TcDireccion=${encodeURIComponent(data.TcDireccion)}&TcProvincia=${encodeURIComponent(data.TcProvincia)}&TcEmail=${encodeURIComponent(data.TcEmail)}&TcEstado=${encodeURIComponent(data.TcEstado)}`;

        // Send the data to the server via AJAX
        $.ajax({
            url: `/Lubrilogix/UpdateProveedorData${queryString}`,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log('Proveedor actualizado exitosamente', response);
            },
            error: function (error) {
                console.error('Error actualizando proveedor:', error);
            }
        });
    }

});

