// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*_Layout*/
function clearDefaultText() {
    var input = document.getElementById('City');
    if (input.value === 'Введите наименование н.п.') {
        input.value = '';
        document.getElementById('button').style.display = 'block';
    }
}

/*Details*/
var element = document.querySelector('.col-sm-3 span.temp');
var temp = parseFloat(element.textContent);

if (temp > 0)
    element.style.color = 'red';
else
    element.style.color = 'blue';
    
if (temp == 0)
    element.style.color = 'green';