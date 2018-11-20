// Wouldn't usually write Javascript like this but for such a simple front end
// this was a quick and easy way to get the job done

var apiClient = apiClient || {
    getCitiesList: function () {
        var request = new XMLHttpRequest();
        request.addEventListener("load", apiClient.appendCitiesToDropdown);
        request.open("GET",
            "http://localhost:5000/api/weather/GetCities");
        request.send();
    },
    appendCitiesToDropdown: function () {
        var response = JSON.parse(this.responseText);
        var select = document.getElementById("cities");
        for (var i = 0; i < response.length; i++) {
            var opt = document.createElement('option');
            opt.value = response[i];
            opt.text = response[i];
            select.appendChild(opt);
        }
    },
    getCityWeather: function () {
        var selectedCityOption = document.getElementById("cities"),
            selectedCityValue = selectedCityOption.options[selectedCityOption.selectedIndex].value;

        if (parseInt(selectedCityValue) !== -1) {
            var request = new XMLHttpRequest();
            request.addEventListener("load", apiClient.displayCityWeather);
            request.open("GET",
                "http://localhost:5000/api/weather/GetWeather/" + selectedCityValue);
            request.send();
        }
    },
    displayCityWeather: function () {
        var response = JSON.parse(this.responseText),
            summary = document.getElementById("summary"),
            temp = document.getElementById("temp"),
            uv = document.getElementById("uv");

        summary.textContent = response.summary;
        temp.textContent = response.temperature;
        uv.textContent = response.uvIndex;
    }
};

var cityDropdown = document.getElementById("cities");
cityDropdown.addEventListener("change", apiClient.getCityWeather);

apiClient.getCitiesList();