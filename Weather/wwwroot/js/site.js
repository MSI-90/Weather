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


    //const blocks = document.querySelectorAll('#firstDay, #secondDay, #threeDay');

    //blocks.forEach(block => {
    //    block.addEventListener('click', () => {
    //        const data = block.querySelector('h5').textContent;
    //    });
    //});

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
            
            let dayColor = [];
            for (let i = 0; i < 3; i++) {
                if (result[i].dayOfWeek === "Суббота" || result[i].dayOfWeek === "Воскресенье") {
                    dayColor.push('red');
                }
                else {
                    dayColor.push('blue');
                }
            }
            console.log(result);

            $('.three-days').css('display', 'block');

            $('.tab1 a').text(result[0].date);
            $('.tab2 a').text(result[1].date);
            $('.tab3 a').text(result[2].date);


            /*$('#firstDay h5').text(result[0].date);*/
            /*$('#firstDay h5').css('color', dayColor[0]);*/
            $('#firstDay .temp').text('Температура воздуха: ' + result[0].minTemp + ' °C' + ' ... ' + result[0].maxTemp + ' °C ');
            $('#firstDay .windGust').text('Ветер до: ' + result[0].windGust + ' м/с');
            $('#firstDay .tempIco').html('<img src= "' + result[0].weatherImg + '" alt = "' + result[0].weatherText + '"title = "' + result[0].weatherText + '"/>');
            $('#firstDay .visible').text('Горизонтальная видимость: ' + result[0].avgVisInKm + 'км');
            $('#firstDay .humidity').text('Влажность: ' + result[0].humidity);

            //$('#secondDay h5').text(result[1].date);
            $/*('#secondDay h5').css('color', dayColor[1]);*/
            $('#secondDay .temp').text('Температура воздуха: ' + result[1].minTemp + ' °C' + ' ... ' + result[1].maxTemp + ' °C ');
            $('#secondDay .windGust').text('Ветер до: ' + result[1].windGust + ' м/с');
            $('#secondDay .tempIco').html('<img src= "' + result[1].weatherImg + '" alt = "' + result[1].weatherText + '"title = "' + result[1].weatherText + '"/>');
            $('#secondDay .visible').text('Горизонтальная видимость: ' + result[1].avgVisInKm + 'км');
            $('#secondDay .humidity').text('Влажность: ' + result[1].humidity);

            //$('#threeDay h5').text(result[2].date);
            //$('#threeDay h5').css('color', dayColor[2]);
            $('#threeDay .temp').text('Температура воздуха: ' + result[2].minTemp + ' °C' + ' ... ' + result[2].maxTemp + ' °C ');
            $('#threeDay .windGust').text('Ветер до: ' + result[2].windGust + ' м/с');
            $('#threeDay .tempIco').html('<img src= "' + result[2].weatherImg + '" alt = "' + result[2].weatherText + '"title = "' + result[2].weatherText + '"/>');
            $('#threeDay .visible').text('Горизонтальная видимость: ' + result[2].avgVisInKm + 'км');
            $('#threeDay .humidity').text('Влажность: ' + result[2].humidity);
        }
    });
};

$(document).ready(function () {
    $('.nav-tabs a').click(function (e) {
        e.preventDefault(); // Предотвращает переход по ссылке

        // Убираем класс 'active' у всех вкладок
        $('.nav-tabs li').removeClass('active');
        // Добавляем класс 'active' только к выбранной вкладке
        $(this).parent('li').addClass('active');

        // Получаем идентификатор выбранной вкладки
        var tabId = $(this).attr('href');
        // Скрываем все контенты вкладок
        $('.tab-pane').removeClass('active');
        // Отображаем только контент выбранной вкладки
        $(tabId).addClass('active');
    });
});