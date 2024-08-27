// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let resourcePath = window.location.pathname;

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
    let citiesList = document.getElementById('cities-list');
    citiesList.addEventListener('click', (event) => {
        const cityData = {
            name: event.target.getAttribute('city'),
            country: event.target.getAttribute('country'),
            latitude: event.target.getAttribute('latitude'),
            longitude: event.target.getAttribute('longitude')
        }

        //switch (resourcePath) {
        //    case '/':
        //        sendCityRequest(cityData);
        //        break;

        //    case '/home.html':
        //        sendCityRequest(cityData);
        //        break;

        //    case '/hourly.html':
        //        sendHourlyRequest(cityData.latitude, cityData.longitude)
        //        break;

        //    case '/daily.html':

        //        break;
        //}

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
    const weatherInfo = document.getElementById('weather-info');
    const current = weatherInfo.children[0];
    const cityName = current.children[0];

    weatherInfo.innerHTML = weatherData;

    console.log(weatherData);
}

function sendHourlyRequest(latitude, longitude) {
    $.ajax({
        url: 'https://localhost:7152/Hourly',
        data: {
            latitude: latitude,
            longitude: longitude
        },
        success: function (hourlyData) {
            displayHourlyData(hourlyData);
        }
    })
}

function displayHourlyData(hourlyData) {
    const weatherInfo = document.getElementById('weather-info');
    weatherInfo.innerHTML = '';
    weatherInfo.innerHTML = hourlyData;
}

function sendDailyRequest(latitude, longitude) {
    $.ajax({
        url: 'https://localhost:7152/Daily',
        data: {
            latitude: latitude,
            longitude: longitude
        },
        success: function (dailyData) {
            displayDailyData(dailyData);
        }
    })
}

function displayDailyData(dailyData) {
    const weatherInfo = document.getElementById('weather-info');
    weatherInfo.innerHTML = '';
    weatherInfo.innerHTML = dailyData;
}