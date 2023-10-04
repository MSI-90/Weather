// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function clearDefaultText() {
    var input = document.getElementById('City');
    if (input.value === 'Введите наименование н.п.') {
        input.value = '';
        document.getElementById('button').style.display = 'block';
    }
}
