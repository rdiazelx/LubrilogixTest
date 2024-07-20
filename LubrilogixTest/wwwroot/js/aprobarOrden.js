function aprobarOrden(orderId) {
    if (confirm("¿Está seguro de que desea aprobar esta orden?")) {
        fetch(`/Lubrilogix/UpdateOrderState?tnIdOrden=${orderId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert("Orden aprobada exitosamente.");

                    // Trigger the view update for option 3
                    const viewSelector = document.getElementById("viewSelector");
                    if (viewSelector) {
                        viewSelector.value = 'option3'; // Select "Ordenes de compra pendientes"
                        const event = new Event('change');
                        viewSelector.dispatchEvent(event); // Trigger change event to reload the view
                    }
                } else {
                    return response.text().then(text => { throw new Error(text) });
                }
            })
            .catch(error => {
                alert("Error al aprobar la orden: " + error.message);
                console.error('Error:', error);
            });
    }
}
