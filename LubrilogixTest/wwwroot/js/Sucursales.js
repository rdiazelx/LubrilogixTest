$(document).ready(function () {
    if (typeof userInSpecificGroup === 'undefined') {
        console.error('userInSpecificGroup is not defined.');
        return;
    }

    // Initialize DataTable for the main table
    //let table = new DataTable('#sucursalesTable', {
    let table = $('#sucursalesTable').DataTable({
        responsive: true,
        select: true,
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        dom: 'lfrtip',
        order: [], // Prevent initial sorting
        columnDefs: [
            { orderable: false, targets: userInSpecificGroup ? [8] : [] } // Disable sorting for the Edit column if present
        ],
        columns: [
            { data: "TnIdSucursal" },
            { data: "TcNombre" },
            { data: "TcProvincia" },
            { data: "TcDireccion" },
            { data: "TcTelefono" },
            { data: "TcCorreo" },
            { data: "TcEstado" },
            { data: "TcComentarios" },
            { data: userInSpecificGroup ? "Edit" : null } // Include Edit column if present
        ]
    });

    // Show or hide the Edit column based on the userInSpecificGroup value

    if (userInSpecificGroup) {
        table.column(8).visible(true); // Show the Edit column (6th column)
        $('.editColumn').show(); // Make sure the placeholder column is shown
    } else {
        table.column(8).visible(false); // Hide the Edit column (6th column)
    }

    //// Filter by Nombre
    //$('#nombreFilterIcon').on('click', function () {
    //    $('#nombreFilter').toggle();
    //});
    //$('#nombreFilter').on('keyup', function () {
    //    table.columns(1).search(this.value).draw();
    //});

    //// Filter by Provincia
    //$('#provinciaFilterIcon').on('click', function () {
    //    $('#provinciaFilter').toggle();
    //});
    //$('#provinciaFilter').on('keyup', function () {
    //    table.columns(2).search(this.value).draw();
    //});

    //// Filter by Estado
    //$('#estadoFilterIcon').on('click', function () {
    //    $('#estadoFilter').toggle();
    //});

    //$('#estadoFilter').on('change', function () {
    //    const filterValue = this.value;
    //    table.columns(6).search(filterValue ? '^' + filterValue + '$' : '', true, false).draw();
    //});

    // Handle Edit icon click
    $('#sucursalesTable tbody').on('click', '.editIcon', function () {
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
    $('#sucursalesTable tbody').on('click', '.saveIcon', function () {
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
                    newData["TnIdSucursal"] = newValue;
                    break;
                case 1:
                    newData["TcNombre"] = newValue;
                    break;
                case 2:
                    newData["TcProvincia"] = newValue;
                    break;
                case 3:
                    newData["TcDireccion"] = newValue;
                    break;
                case 4:
                    newData["TcTelefono"] = newValue;
                    break;
                case 5:
                    newData["TcCorreo"] = newValue;
                    break;
                case 6:
                    newData["TcEstado"] = newValue;
                    break;
                case 7:
                    newData["TcComentarios"] = newValue;
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
        UpdateSucursalesData(newData);
    });



    function UpdateSucursalesData(data) {
        console.log('Updating sucur with data:', data);

        // Construct the query string
        //let queryString = `?TnIdSucursal=${data.TN_IdSucursal}&TcNombre=${encodeURIComponent(data.TC_Nombre)}&TcDireccion=${encodeURIComponent(data.TC_Direccion)}&TcProvincia=${encodeURIComponent(data.TC_Provincia)}&TcTelefono=${encodeURIComponent(data.TC_Telefono)}&TcCorreo=${encodeURIComponent(data.TC_Correo)}&TcEstado=${encodeURIComponent(data.TC_Estado)}&TcComentarios=${encodeURIComponent(data.TC_Comentarios)}`;
        let queryString = `?TnIdSucursal=${data.TnIdSucursal}&TcNombre=${encodeURIComponent(data.TcNombre)}&TcDireccion=${encodeURIComponent(data.TcDireccion)}&TcProvincia=${encodeURIComponent(data.TcProvincia)}&TcTelefono=${encodeURIComponent(data.TcTelefono)}&TcCorreo=${encodeURIComponent(data.TcCorreo)}&TcEstado=${encodeURIComponent(data.TcEstado)}&TcComentarios=${encodeURIComponent(data.TcComentarios)}`;
        // Send the data to the server via AJAX
        $.ajax({
            url: `/Lubrilogix/UpdateSucursalesData${queryString}`,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log('Sucursal actualizada exitosamente', response);
            },
            error: function (error) {
                console.error('Error actualizando sucursal:', error);
            }
        });
    }
});



