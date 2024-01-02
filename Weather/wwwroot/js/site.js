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

            for (let i = 0; i < result[0].hours.length; i++) {
                //let divElement = document.createElement('div');
                //divElement.classList.add('item');
                //divElement.innerHTML = `<h1>${result[0].hours[i].timeOfHours[1]}</h1>`;
                //document.querySelector('#owl-demo').appendChild(divElement);
            }

            $('#firstDay .temp').text('Температура воздуха: ' + result[0].minTemp + ' °C' + ' ... ' + result[0].maxTemp + ' °C ');
            $('#firstDay .windGust').text('Ветер до: ' + result[0].windGust + ' м/с');
           /* $('#firstDay .tempIco').html('<img src= "' + result[0].weatherImg + '" alt = "' + result[0].weatherText + '"title = "' + result[0].weatherText + '"/>');*/
            $('#firstDay .visible').text('Горизонтальная видимость: ' + result[0].avgVisInKm + 'км');
            $('#firstDay .humidity').text('Влажность: ' + result[0].humidity);

            //morning
            $('#temp-morning').text(result[0].hours[7].tempC + ' °C');
            $('#weather-ico-morning').html('<img src= "' + result[0].hours[7].weatherImg + '" alt = "' + result[0].hours[7].weatherText + '"title = "' + result[0].hours[7].weatherText + '"/>');
            $('#wt-text-morning').text(result[0].hours[7].weatherText);
            $('#pressure-morning').text(result[0].hours[7].pressure);
            $('#humidity-morning').text(result[0].hours[7].humidity + ' %');
            $('#wind-morning').text(result[0].hours[7].windSpeed);
            $('#like-morning').text(result[0].hours[7].feelsLikeC + ' °C');
            //day
            $('#temp-day').text(result[0].hours[13].tempC + ' °C');
            $('#weather-ico-day').html('<img src= "' + result[0].hours[13].weatherImg + '" alt = "' + result[0].hours[13].weatherText + '"title = "' + result[0].hours[13].weatherText + '"/>');
            $('#wt-text-day').text(result[0].hours[13].weatherText);
            $('#pressure-day').text(result[0].hours[13].pressure);
            $('#humidity-day').text(result[0].hours[13].humidity + ' %');
            $('#wind-day').text(result[0].hours[13].windSpeed);
            $('#like-day').text(result[0].hours[13].feelsLikeC + ' °C');
            //evening
            $('#temp-evening').text(result[0].hours[19].tempC + ' °C');
            $('#weather-ico-evening').html('<img src= "' + result[0].hours[19].weatherImg + '" alt = "' + result[0].hours[19].weatherText + '"title = "' + result[0].hours[19].weatherText + '"/>');
            $('#wt-text-evening').text(result[0].hours[19].weatherText);
            $('#pressure-evening').text(result[0].hours[19].pressure);
            $('#humidity-evening').text(result[0].hours[19].humidity + ' %');
            $('#wind-evening').text(result[0].hours[19].windSpeed);
            $('#like-evening').text(result[0].hours[19].feelsLikeC + ' °C');
            //night
            $('#temp-night').text(result[0].hours[1].tempC + ' °C');
            $('#weather-ico-night').html('<img src= "' + result[0].hours[1].weatherImg + '" alt = "' + result[0].hours[1].weatherText + '"title = "' + result[0].hours[1].weatherText + '"/>');
            $('#wt-text-night').text(result[0].hours[1].weatherText);
            $('#pressure-night').text(result[0].hours[1].pressure);
            $('#humidity-night').text(result[0].hours[1].humidity + ' %');
            $('#wind-night').text(result[0].hours[1].windSpeed);
            $('#like-night').text(result[0].hours[1].feelsLikeC + ' °C');


            $('#secondDay .temp').text('Температура воздуха: ' + result[1].minTemp + ' °C' + ' ... ' + result[1].maxTemp + ' °C ');
            $('#secondDay .windGust').text('Ветер до: ' + result[1].windGust + ' м/с');
            /*$('#secondDay .tempIco').html('<img src= "' + result[1].weatherImg + '" alt = "' + result[1].weatherText + '"title = "' + result[1].weatherText + '"/>');*/
            $('#secondDay .visible').text('Горизонтальная видимость: ' + result[1].avgVisInKm + 'км');
            $('#secondDay .humidity').text('Влажность: ' + result[1].humidity);

            //morning
            $('#temp2-morning').text(result[1].hours[7].tempC + ' °C');
            $('#weather2-ico-morning').html('<img src= "' + result[1].hours[7].weatherImg + '" alt = "' + result[1].hours[7].weatherText + '"title = "' + result[1].hours[7].weatherText + '"/>');
            $('#wt2-text-morning').text(result[1].hours[7].weatherText);
            $('#pressure2-morning').text(result[1].hours[7].pressure);
            $('#humidity2-morning').text(result[1].hours[7].humidity + ' %');
            $('#wind2-morning').text(result[1].hours[7].windSpeed);
            $('#like2-morning').text(result[1].hours[7].feelsLikeC + ' °C');
            //day
            $('#temp2-day').text(result[0].hours[13].tempC + ' °C');
            $('#weather2-ico-day').html('<img src= "' + result[1].hours[13].weatherImg + '" alt = "' + result[1].hours[13].weatherText + '"title = "' + result[1].hours[13].weatherText + '"/>');
            $('#wt2-text-day').text(result[1].hours[13].weatherText);
            $('#pressure2-day').text(result[1].hours[13].pressure);
            $('#humidity2-day').text(result[1].hours[13].humidity + ' %');
            $('#wind2-day').text(result[1].hours[13].windSpeed);
            $('#like2-day').text(result[1].hours[13].feelsLikeC + ' °C');
            //evening
            $('#temp2-evening').text(result[1].hours[19].tempC + ' °C');
            $('#weather2-ico-evening').html('<img src= "' + result[1].hours[19].weatherImg + '" alt = "' + result[1].hours[19].weatherText + '"title = "' + result[1].hours[19].weatherText + '"/>');
            $('#wt2-text-evening').text(result[1].hours[19].weatherText);
            $('#pressure2-evening').text(result[1].hours[19].pressure);
            $('#humidity2-evening').text(result[1].hours[19].humidity + ' %');
            $('#wind2-evening').text(result[1].hours[19].windSpeed);
            $('#like2-evening').text(result[1].hours[19].feelsLikeC + ' °C');
            //night
            $('#temp2-night').text(result[1].hours[1].tempC + ' °C');
            $('#weather2-ico-night').html('<img src= "' + result[1].hours[1].weatherImg + '" alt = "' + result[1].hours[1].weatherText + '"title = "' + result[1].hours[1].weatherText + '"/>');
            $('#wt2-text-night').text(result[1].hours[1].weatherText);
            $('#pressure2-night').text(result[1].hours[1].pressure);
            $('#humidity2-night').text(result[1].hours[1].humidity + ' %');
            $('#wind2-night').text(result[1].hours[1].windSpeed);
            $('#like2-night').text(result[1].hours[1].feelsLikeC + ' °C');
            

            $('#threeDay .temp').text('Температура воздуха: ' + result[2].minTemp + ' °C' + ' ... ' + result[2].maxTemp + ' °C ');
            $('#threeDay .windGust').text('Ветер до: ' + result[2].windGust + ' м/с');
            /*$('#threeDay .tempIco').html('<img src= "' + result[2].weatherImg + '" alt = "' + result[2].weatherText + '"title = "' + result[2].weatherText + '"/>');*/
            $('#threeDay .visible').text('Горизонтальная видимость: ' + result[2].avgVisInKm + 'км');
            $('#threeDay .humidity').text('Влажность: ' + result[2].humidity);

            //morning
            $('#temp3-morning').text(result[2].hours[7].tempC + ' °C');
            $('#weather3-ico-morning').html('<img src= "' + result[2].hours[7].weatherImg + '" alt = "' + result[2].hours[7].weatherText + '"title = "' + result[2].hours[7].weatherText + '"/>');
            $('#wt3-text-morning').text(result[2].hours[7].weatherText);
            $('#pressure3-morning').text(result[2].hours[7].pressure);
            $('#humidity3-morning').text(result[2].hours[7].humidity + ' %');
            $('#wind3-morning').text(result[2].hours[7].windSpeed);
            $('#like3-morning').text(result[2].hours[7].feelsLikeC + ' °C');
            //day
            $('#temp3-day').text(result[2].hours[13].tempC + ' °C');
            $('#weather3-ico-day').html('<img src= "' + result[2].hours[13].weatherImg + '" alt = "' + result[2].hours[13].weatherText + '"title = "' + result[2].hours[13].weatherText + '"/>');
            $('#wt3-text-day').text(result[2].hours[13].weatherText);
            $('#pressure3-day').text(result[2].hours[13].pressure);
            $('#humidity3-day').text(result[2].hours[13].humidity + ' %');
            $('#wind3-day').text(result[2].hours[13].windSpeed);
            $('#like3-day').text(result[2].hours[13].feelsLikeC + ' °C');
            //evening
            $('#temp3-evening').text(result[2].hours[19].tempC + ' °C');
            $('#weather3-ico-evening').html('<img src= "' + result[2].hours[19].weatherImg + '" alt = "' + result[2].hours[19].weatherText + '"title = "' + result[2].hours[19].weatherText + '"/>');
            $('#wt3-text-evening').text(result[2].hours[19].weatherText);
            $('#pressure3-evening').text(result[2].hours[19].pressure);
            $('#humidity3-evening').text(result[2].hours[19].humidity + ' %');
            $('#wind3-evening').text(result[2].hours[19].windSpeed);
            $('#like3-evening').text(result[2].hours[19].feelsLikeC + ' °C');
            //night
            $('#temp3-night').text(result[2].hours[1].tempC + ' °C');
            $('#weather3-ico-night').html('<img src= "' + result[2].hours[1].weatherImg + '" alt = "' + result[2].hours[1].weatherText + '"title = "' + result[2].hours[1].weatherText + '"/>');
            $('#wt3-text-night').text(result[2].hours[1].weatherText);
            $('#pressure3-night').text(result[2].hours[1].pressure);
            $('#humidity3-night').text(result[2].hours[1].humidity + ' %');
            $('#wind3-night').text(result[2].hours[1].windSpeed);
            $('#like3-night').text(result[2].hours[1].feelsLikeC + ' °C');


            var variant = document.querySelectorAll('.variant li');
        }
    });
};

$(document).ready(function () {
    $('.tab-header a').click(function (e) {
        e.preventDefault(); // Предотвращает переход по ссылке
        // Убираем класс 'active' у всех вкладок
        $('.tab-header li').removeClass('active');
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