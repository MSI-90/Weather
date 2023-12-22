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
function ChangeColorOfTemperature() {
    var element = document.getElementById('temp-number');
    var temp = parseFloat(element.innerHTML);

    if (temp > 0 && temp < 30) {
        element.style.color = '#ff5900';
    }
    else if (temp >= 30 && temp < 34)
    {
        element.style.color = '#ff9900';
    }
    else if (temp >= 34) {
        element.style.color = "#ff3300";
    }
    else if (temp < 0) {
        element.style.color = '#2b63a0';
    }
    else if (temp >= -20 && temp <= -30) {
        element.style.color = "#00ccff";
    }

    else {
        element.style.color = 'green';
    }
}
ChangeColorOfTemperature()

function ChangeColorOfFreesTemp() {
    var element = document.getElementById('feelslike-temp');
    var temp = parseFloat(element.innerHTML);

    if (temp > 0 && temp < 30) {
        element.style.color = '#ff5900';
    }
    else if (temp >= 30 && temp < 34) {
        element.style.color = '#ff9900';
    }
    else if (temp >= 34) {
        element.style.color = "#ff3300";
    }
    else if (temp < 0) {
        element.style.color = '#2b63a0';
    }
    else if (temp >= -20 && temp <= -30) {
        element.style.color = "#00ccff";
    }
    else {
        element.style.color = 'green';
    }
    
}
ChangeColorOfFreesTemp()

function ChangeColorOfWind() {
    var element = document.getElementById('windSpeed');
    var temp = parseFloat(element.innerHTML);

    if (temp >= 11) {
        element.style.color = "red";
    }
}
ChangeColorOfWind()

function ChangeColorOfGustWind() {
    var element = document.getElementById('gust-wind');
    var temp = parseFloat(element.innerHTML);

    if (temp >= 11) {
        element.style.color = "red";
        var count = 0;
        var interval = setInterval(function () {
            if (count % 2 === 0) {
                element.style.opacity = '0.25';
            } else {
                element.style.opacity = '1';
            }
            count++;
            if (count > 18) { // Blink for 8 seconds (15 times with a 0.5-second interval)
                clearInterval(interval);
                element.style.opacity = '1'; // Ensure visibility is restored after blinking
            }
        }, 500);
    }
    
}
ChangeColorOfGustWind()

$(document).ready(function () {
    $('.forecast').click(function () {
        Forecast();
    });
});

function Forecast() {
    let obj = {
        City: $('#сityName').val(),
        DayOfWeek: 3
    }

    $.ajax({
        url: "/forecast",
        data: JSON.stringify(obj),
        method: 'POST',
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('.three-days').css('display', 'block');
            $('#firstDay').text(result.current.tempC);
        }
    });
}