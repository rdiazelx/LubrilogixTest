// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Add the 'collapsed' class on page load
window.addEventListener('DOMContentLoaded', (event) => {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.add('collapsed');
});

document.getElementById('toggleButton').addEventListener('click', function () {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('collapsed');
});
