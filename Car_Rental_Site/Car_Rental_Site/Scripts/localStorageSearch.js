//Save information of the last 5 searches to local storage
function Save2LocalStorage(toSave) {
    if (!window.localStorage) {
        return;
    }
    if (localStorage.getItem("Search1") == null) {
        localStorage.setItem("Search1", toSave);
        return;
    }
    if (localStorage.getItem("Search2") == null) {
        localStorage.setItem("Search2", toSave);
        return;
    }
    if (localStorage.getItem("Search3") == null) {
        localStorage.setItem("Search3", toSave);
        return;
    }
    if (localStorage.getItem("Search4") == null) {
        localStorage.setItem("Search4", toSave);
        return;
    }
    if (localStorage.getItem("Search5") == null) {
        localStorage.setItem("Search5", toSave);
        return;
    }
    if (localStorage.getItem("Search5") != null) {
        localStorage.setItem("Search1", localStorage.getItem("Search2"));
        localStorage.setItem("Search2", localStorage.getItem("Search3"));
        localStorage.setItem("Search3", localStorage.getItem("Search4"));
        localStorage.setItem("Search4", localStorage.getItem("Search5"));
        localStorage.setItem("Search5", toSave);
        return;
    }
}

//Load last searchs from local storage
function loadFromLocalStorage() {
    if (!window.localStorage) {
        return;
    }
    $("#lstLastSearchs").html('');
    if (localStorage.getItem("Search1") == null) {
        return;
    }
    $("#DivLastSearchs").css("visibility", "visible");

    LoadHelper('Search1');
    LoadHelper('Search2');
    LoadHelper('Search3');
    LoadHelper('Search4');
    LoadHelper('Search5');
}

//Help to load the information from local storage and add/build it to the page view
//Also set an click action for them
function LoadHelper(StorageName) {
    if (localStorage.getItem(StorageName) == null) {
        return;
    }
    var SearchText = localStorage.getItem(StorageName).split(",");
    var startDate = SearchText[0].split("=")[1];
    var endDate = SearchText[1].split("=")[1];
    var template = '<li class="lookLikeLink" id="' + StorageName + '">Search available cars from ' + startDate + ' to ' + endDate + '</li>';
    $(template).appendTo($("#lstLastSearchs"));
    $("#" + StorageName).click(function () { clickOnList(startDate, endDate); });
}

//Search from local storage information
function clickOnList(startDate, endDate) {
    $("#fromDate").val(startDate);
    $("#toDate").val(endDate);
    $("#btnSearch").css("visibility", "visible");
    $("#btnSearch").click();
}