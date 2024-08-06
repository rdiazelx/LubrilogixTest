window.addEventListener('DOMContentLoaded', (event) => {
    var sidebar = document.getElementById('sidebar');
    var mainContainer = document.querySelector('.main-container');

    sidebar.classList.add('collapsed');

    // Function to handle the blur effect
    function handleBlur() {
        if (sidebar.classList.contains('collapsed')) {
            mainContainer.classList.remove('blurred');
        } else {
            mainContainer.classList.add('blurred');
        }
    }

    // Initial check for blur effect
    handleBlur();

    document.getElementById('toggleButton').addEventListener('click', function () {
        sidebar.classList.toggle('collapsed');
        handleBlur();
    });

    // Add hover event listener to handle blur on hover
    sidebar.addEventListener('mouseenter', function () {
        sidebar.classList.remove('collapsed');
        handleBlur();
    });

    sidebar.addEventListener('mouseleave', function () {
        sidebar.classList.add('collapsed');
        handleBlur();
    });
});
