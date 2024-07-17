function aprobarOrden(orderId) {
    if (confirm("¿Está seguro de que desea aprobar esta orden?")) {
        fetch(`/Lubrilogix/ApproveOrder?orderId=${orderId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-Token': document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        })
        .then(response => {
            if (response.ok) {
                alert("Orden aprobada exitosamente.");
                location.reload(); // Reload the page to see changes
            } else {
                alert("Error al aprobar la orden.");
            }
        })
        .catch(error => console.error('Error:', error));
    }
}
