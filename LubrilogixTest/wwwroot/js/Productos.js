$(document).ready(function () {
    // Ensure userInSpecificGroup is defined
    if (typeof userInSpecificGroup === 'undefined') {
        console.error('userInSpecificGroup is not defined.');
        return;
    }

    // Initialize DataTable for the main table
    let table = $('#productosTable').DataTable({
        responsive: true,
        select: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        dom: 'lfrtip',
        order: [], // Prevent initial sorting
        columnDefs: [
            { orderable: false, targets: userInSpecificGroup ? [4] : [] } // Disable sorting for the Edit column if present
        ],
        columns: [
            { data: "TnIdProducto" },
            { data: "TcNombre" },
            { data: "TcCategoria" },
            { data: "TcSubcategoria" },
            { data: userInSpecificGroup ? "Edit" : null } // Include Edit column if present
        ]
    });

    // Show or hide the Edit column based on the userInSpecificGroup value
    if (userInSpecificGroup) {
        table.column(4).visible(true); // Show the Edit column
        $('.editColumn').show(); // Make sure the placeholder column is shown
    } else {
        table.column(4).visible(false); // Hide the Edit column
    }

    // Handle Edit icon click
    $('#productosTable tbody').on('click', '.editIcon', function () {
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
    $('#productosTable tbody').on('click', '.saveIcon', function () {
        let row = $(this).closest('tr');
        let newData = {};

        // Collect updated values
        row.children('td').not(':last').each(function (index) {
            let cell = $(this);
            let input = cell.find('input');
            let newValue = input.val();
            cell.text(newValue);

            // Map new values to corresponding data properties
            switch (index) {
                case 0:
                    newData["TnIdProducto"] = newValue;
                    break;
                case 1:
                    newData["TcNombre"] = newValue;
                    break;
                case 2:
                    newData["TcCategoria"] = newValue;
                    break;
                case 3:
                    newData["TcSubcategoria"] = newValue;
                    break;
            }
        });

        // Add placeholder for the Edit column
        newData["Edit"] = '<i class="fas fa-edit editIcon"></i><i class="fas fa-save saveIcon" style="display:none;"></i>';

        // Update the DataTable
        table.row(row).data(newData).draw();

        // Toggle visibility of icons
        row.find('.editIcon').show();
        $(this).hide();

        console.log('Updated row data:', newData);

        // Call a function to update the database with newData
        updateProductoData(newData);
    });

    function updateProductoData(data) {
        console.log('Updating product with data:', data);

        // Construct the query string
        let queryString = `?TnIdProducto=${data.TnIdProducto}&TcNombre=${encodeURIComponent(data.TcNombre)}&TcCategoria=${encodeURIComponent(data.TcCategoria)}&TcSubcategoria=${encodeURIComponent(data.TcSubcategoria)}`;

        // Send the data to the server via AJAX
        $.ajax({
            url: `/Lubrilogix/UpdateProductoData${queryString}`,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log('Producto actualizado exitosamente', response);
            },
            error: function (error) {
                console.error('Error actualizando el producto:', error);
            }
        });
    }
});
