// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let timeout = null;

const searchBar = document.getElementById('search-bar');
searchBar.addEventListener('keyup', function () {
    clearTimeout(timeout);

    let query = this.value;
    if (query.length >= 3) {
        timeout = setTimeout(function () {
            searchCities(query);
        }, 900);
    } else {
        document.getElementById('cities-box').innerHTML = '';
    }
});

function searchCities(city) {
    const culture = window.navigator.language;
    const language = culture.substring(0, 2);

    $.ajax({
        url: 'https://geocoding-api.open-meteo.com/v1/search',
        //url: 'http://127.0.0.1:5500/db.json',
        method: 'GET',
         data: {
             name: city, 
             count: 10,
             language: language ? language : pt,
             format: 'json'
         },
        datatype: 'aplication/json',
        success: function (data) {
            let { results } = data;
            displaySuggestions(results);
        },
        error: function (error) {
            displayError(error);
        }
    });
}

function displaySuggestions(cities) {
    let suggestionsContainer = document.getElementById('cities-box');
    suggestionsContainer.innerHTML = '';

    let uList = document.createElement('ul')
    uList.setAttribute('id', 'cities-list')

    suggestionsContainer.appendChild(uList)

    cities.forEach(city => {
        let suggestion = document.createElement('li');
        suggestion.textContent = `${city.name}, ${city.country}`;
        suggestion.setAttribute('city', city.name);
        suggestion.setAttribute('country', city.country);
        suggestion.setAttribute('latitude', city.latitude);
        suggestion.setAttribute('longitude', city.longitude);

        uList.appendChild(suggestion);
    });

    selectOption();
}

function displayError(error) {
    let suggestionsContainer = document.getElementById('cities-box');
    suggestionsContainer.innerHTML = '';
    suggestionsContainer.innerHTML = 'Ocorreu um problema ao tentar localizar a cidade. Por favor, tente novamente mais tarde'
}

function toggleDropdown() {
    let dropdown = document.getElementById('cities-box');
    if (dropdown.style.display === 'none' || dropdown.style.display === '') {
        dropdown.style.display = 'block';
    } else {
        dropdown.style.display = 'none';
    }
}

function showDropdown() {
    let dropdown = document.getElementById('cities-box');
    dropdown.style.display = 'block';
}

document.addEventListener('click', function (event) {
    let isClickInside = document.querySelector('.search-container').contains(event.target);
    if (!isClickInside) {
        document.getElementById('cities-box').style.display = 'none';
    }
});

function selectOption() {
    let resourcePath = window.location.pathname;
    console.log(resourcePath);

    let citiesList = document.getElementById('cities-list');
    citiesList.addEventListener('click', (event) => {
        const cityData = {
            name: event.target.getAttribute('city'),
            country: event.target.getAttribute('country'),
            latitude: event.target.getAttribute('latitude'),
            longitude: event.target.getAttribute('longitude')
        }

        switch (resourcePath) {
            case '/':
                sendCityRequest(cityData);
                break;

            case '/Hourly':
                sendHourlyRequest(cityData)
                break;
            case '/ByHourly':
                sendHourlyRequest(cityData)
                break;

            case '/Daily':
                sendDailyRequest(cityData)
                break;
            case '/ByDaily':
                sendDailyRequest(cityData)
                break;
        }

        sendCityRequest(cityData);

        document.getElementById('cities-box').style.display = 'none';
    })
}

function sendCityRequest(cityData) {
    $.ajax({
        url: 'https://localhost:7152/Weather',
        method: 'GET',
        data: {
            latitude: cityData.latitude,
            longitude: cityData.longitude,
            city: cityData.name,
            country: cityData.country
        },
        success: function (weatherData) {
            displayWeatherData(cityData, weatherData);
        }
    });
}

function displayWeatherData(cityData, weatherData) {
    // const weatherInfo = document.getElementById('weather-info');
    const mainContent = document.getElementById('main-content');
    const current = document.getElementById('current-weather');
    const cityName = current.children[0];

    mainContent.innerHTML = '';
    mainContent.innerHTML = weatherData;

    setCurrentWeatherBackground()
    setAiqColor()
}

function sendHourlyRequest(cityData) {
    $.ajax({
        url: 'https://localhost:7152/Hourly',
        data: {
            latitude: cityData.latitude,
            longitude: cityData.longitude,
            city: cityData.name,
            country: cityData.country
        },
        success: function (hourlyData) {
            displayHourlyData(hourlyData);
        }
    })
}

function displayHourlyData(hourlyData) {
    const mainContent = document.getElementById('main-content');
    mainContent.innerHTML = '';
    mainContent.innerHTML = hourlyData;
}

function sendDailyRequest(cityData) {
    $.ajax({
        url: 'https://localhost:7152/Daily',
        data: {
            latitude: cityData.latitude,
            longitude: cityData.longitude,
            city: cityData.name,
            country: cityData.country
        },
        success: function (dailyData) {
            displayDailyData(dailyData);
        }
    })
}

function displayDailyData(dailyData) {
    const mainContent = document.getElementById('main-content');
    mainContent.innerHTML = '';
    mainContent.innerHTML = dailyData;
}

let actualBodyClass = '';
function setCurrentWeatherBackground() {
    const wmoCode = getCookie('WMO-Code');
    const { codes } = wmo;

    const code = codes[wmoCode];
    const { bodyStyle } = code;
    const { classToAdd } = code;
    const { url } = code;

    const body = document.body;
    const currentWeather = document.getElementById('current-weather');

    if (actualBodyClass !== '') {
        body.classList.remove(actualBodyClass);
        actualBodyClass = '';
    }

    body.classList.add(bodyStyle);
    actualBodyClass = bodyStyle;

    if (currentWeather) {
        currentWeather.style.backgroundImage = `url(${url})`;
        if (classToAdd)
            currentWeather.classList.add(classToAdd);
    }
}

setCurrentWeatherBackground()

function getCookie(cookieName) {
    const cookieDecoded = decodeURIComponent(document.cookie);
    const regex = new RegExp(`${cookieName}=([^;]+)`);
    const cookie = cookieDecoded.match(regex);

    if (cookie)
        return cookie[1];
}

function setAiqColor() {
    const aiqInfos = document.getElementsByClassName('aiq-infos')[0];
    const aiqValue = aiqInfos.children[2];

    const color = getAQIColor(aiqValue.innerHTML);
    aiqValue.style.backgroundColor = color;
}

setAiqColor()

function getAQICondition(AqiValue) {
    const ranges = {
        '0-50': 'good',
        '51-100': 'moderate',
        '101-150': 'unhealthySensitive',
        '151-200': 'unhealthy',
        '201-300': 'veryUnhealthy',
        '301-500': 'hazardous',
    }

    const rangesValues = ['0-50', '51-100', '101-150', '151-200', '201-300', '301-500']

    let condition;

    for (let index = 0; index < rangesValues.length; index++) {
        const element = rangesValues[index];
        const splited = element.split('-');
        const min = +splited[0];
        const max = +splited[1];

        if (parseInt(AqiValue) >= min && AqiValue !== max && parseInt(AqiValue) <= max) {
            condition = ranges[element]
            return condition;
        }
    }
}

function getAQIColor(AqiValue) {
    const conditions = {
        good: '#50ccaa',
        moderate: '#f0e641',
        unhealthySensitive: '#f0a741',
        unhealthy: '#ff5050',
        veryUnhealthy: '#960032',
        hazardous: '#7d2181'
    }

    const condition = getAQICondition(AqiValue);

    return conditions[condition];
}