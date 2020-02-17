var setting = {
    data: null,
    dbName: "WeatherTest",
    dbObjectStoreName: "LocationStore"
};

$(document).ready(function () {
    indexedDB_init();   //New IndexedDB
    var locations = getLocation();
    $('#location_name').autocomplete({
        source: getLocationDesc(locations),
        select: function (event, ui) {
            var id = locations.find(x => x.name === ui.item.value).id;
            $("#location_id").val(id);
            this.value = ui.item.value;
            return false;
        }
    });

});

function getLocationDesc(data) {
    var result = [];
    data.forEach(function (item) {
        result.push(item.name);
    });
    return result;
}

function getLocation() {
    if (localStorage.LocationCache == null || localStorage.LocationCache == 'undefined' ||
        localStorage.ExpireTime == null || localStorage.ExpireTime < new Date().getTime()) {
        sendAjax('/weather/AjaxLocation', null, true); //fromAPI
    }
    return JSON.parse(localStorage.LocationCache);  //from indexedDB
}

function indexedDB_init() {
    var setting = {
        data: null,
        dbName: "WeatherTest",
        dbObjectStoreName: "LocationStore"
    };

    $.indexedDB(setting.dbName, {
        "version": 2,
        "schema": {
            "2": function (versionTransaction) {
                console.log('create ' + setting.dbObjectStoreName);
                var catalog = versionTransaction.createObjectStore(setting.dbObjectStoreName, {
                    "keyPath": "key"
                });
            }
        }
    });
}

function indexedDB_save(data) {
    let objStr;
    if (data == null || data.length == 0) {
        objStr == null;
    }else{
        objStr = JSON.stringify(data);
    }
    localStorage.setItem('LocationCache', objStr);
    localStorage.setItem('ExpireTime', new Date().setHours(23, 59, 59));
}

function btn_onClick()
{
    if (!$('#location_id').val()) {
        alert("Choose Location First");
        return;
    }
    sendAjax('/weather/AjaxWeather', { id: $('#location_id').val() }, false);
}

function sendAjax(url, data, save) {
    var result;
    $.ajax({
        type: 'GET',
        url: url,
        cache: false,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function OnSuccess(response) {
            if (save) {
                saveLocation(response);
            } else {
                showWeather(response);
            }
        },
        failure: function (response) {
            alert(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(ajaxOptions);
        }
    });
}

function saveLocation(response) {
    var locations = [];
    if (response != null) {
        response.forEach(function (item) {
            var itemInfo = {
                id: item.id,
                name: item.name
            };
            locations.push(itemInfo);
        });
    }
    indexedDB_save(locations);
}

function showWeather(response) {
    if (response == null || response == 'Fail') {
        alert("Can't Get Weather Data");
    }
    else {
        var allKeys = Object.keys(response);
        show(allKeys, response);
       
    }
}

function show(keys, response) {
    keys.forEach(function (e, idx) {
        if ($('#' + e).length > 0) {
            $('#' + e)[0].innerText = response[e];
        }  
    });
    $('#weatherTable').show();
}