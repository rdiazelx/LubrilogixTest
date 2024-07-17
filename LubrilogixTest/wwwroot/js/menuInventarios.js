document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("viewSelector").addEventListener("change", function () {
        var selectedValue = this.value;
        var viewContainer = document.getElementById("viewContainer");

        if (selectedValue) {
            let url;

            switch (selectedValue) {
                case 'option1':
                    url = '/Lubrilogix/TotalInventory';
                    break;
                case 'option2':
                    url = '/Lubrilogix/Inventario';
                    break;
                case 'option3':
                    url = '/Lubrilogix/inventarioPendiente';
                    break;
                default:
                    viewContainer.innerHTML = '';
                    return;
            }

            fetch(url + '?option=' + selectedValue)
                .then(response => {
                    console.log('Response status:', response.status);
                    return response.text();
                })
                .then(data => {
                    viewContainer.innerHTML = data;
                })
                .catch(error => console.error('Error loading view:', error));
        } else {
            viewContainer.innerHTML = '';
        }
    });
});
