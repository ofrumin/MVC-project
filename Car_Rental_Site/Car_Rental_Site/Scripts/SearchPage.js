/// <reference path="localStorageSearch.js" />

//Set two jquery ui date pickers to start date and end date
//And call function that load information from local storage - localStorageSearch.js file

$(function () {
    $("#fromDate").datepicker({
        format: 'mm/dd/YYYY 00:00:00',
        defaultDate: "+1d",
        numberOfMonths: 2,
        minDate: "d",
        showAnim: "slideDown",
        changeMonth: true,
        onSelect: function (selectedDate) {
            $("#toDate").datepicker("option", "minDate", selectedDate);
            if ($("#toDate").val() != "") {
                $("#btnSearch").css("visibility", "visible");
            }
        }
    });

    $("#toDate").datepicker({
        format: 'mm/dd/YYYY 00:00:00',
        defaultDate: "+1d",
        numberOfMonths: 2,
        minDate: "d",
        showAnim: "slideDown",
        changeMonth: true,
        onSelect: function (selectedDate) {
            $("#fromDate").datepicker("option", "maxDate", selectedDate);
            if ($("#fromDate").val()) {
                $("#btnSearch").css("visibility", "visible");
            }
        }
    });

    loadFromLocalStorage();             //this function is in - localStorageSearch.js file
});

var dataToFilter;                       //The original data that comes after the search - save it for the local filtering
var FilteringData;                      //copy of this data - this is also for the local filtering

//Send request to the server by ajax that looking looking for available vehicle types between two dates
$(function () {
    $("#btnSearch").click(function () {
        $.ajax({
            type: "GET",
            url: $(location).attr('origin') + '/SearchByDates/SearchResults',
            data: { startDate: $("#fromDate").val(), endDate: $("#toDate").val() },
            success: function (data) {
                dataToFilter = data;                                //Save the results to global variable - save for local filtering
                $("#DivLastSearchs").css("display", "none");
                FillSearchResultsDiv(data, true);                   //Call function that display the results - Next function

                Save2LocalStorage("startDate=" + $("#fromDate").val() + ",endDate=" + $("#toDate").val());//->
                //-> Save the search details to local storage - localStorageSearch.js file

                $("#btnSearchFilter").click();
            },
            error: function (err) {
                openErrorsDialog(err.statusText, false);
            },
            cache: false
        });
    });
});

//Show the search results
function FillSearchResultsDiv(data, IsSetFilter) {
    var template = $("#SearchResults-template").clone().html();
    var temp = '';

    if (data.length == 0) {
        temp = "<h3 class='NoMatch'>No vehicles were found match to the search dates or to the filtering!</h3>";
    }
    else {
        for (var i = 0; i < data.length; i++) {
            for (var key in data[i]) {
                template = template.replace("{{" + key + "}}", data[i][key]);
            }
            temp = temp + template;
            template = $("#SearchResults-template").clone().html();
        }
    }
    if (IsSetFilter) {
        SetDivFilter();     //call function that set and display the filter menu - Two next function
    }

    $("#DivSearchResults").html(temp);
    SetButtonsLinks();      //call function to set results buttons link - Next function 
}

//Set the link of the buttons in any results to continue to order page
function SetButtonsLinks() {
    $(".OrderButton").click(function () {
        location.href = '/Order/OrderPage?startDate=' + $("#fromDate").val() + '&endDate=' + $("#toDate").val() + '&vehicleTypeID=' + $(this).attr('id');
    });
}


//Set the filter menu show and display it
function SetDivFilter() {
    $("#FilterMenu").css("visibility", "visible");
    $("#CloseFilterMenu").css({
        "position": "fixed",
        "left": parseInt($("#FilterMenu").css("width")) + 15,
        "top": $("#FilterMenu").css("top"),
        "visibility": "visible"
    });

    $("#OpenFilterMenu").css({
        "position": "fixed",
        "left": 15,
        "top": $("#FilterMenu").css("top"),
        "visibility": "visible"
    });

    $("#OpenFilterMenu").hide();

    var minPrice = dataToFilter[0]["DailyRentCost"], MaxPrice = minPrice;
    for (var i = 1; i < dataToFilter.length; i++) {
        if (dataToFilter[i]["DailyRentCost"] > MaxPrice) {
            MaxPrice = dataToFilter[i]["DailyRentCost"];
        }
        if (dataToFilter[i]["DailyRentCost"] < minPrice) {
            minPrice = dataToFilter[i]["DailyRentCost"];
        }
    }
    PriceSlider(MaxPrice, minPrice);
    setBtnFilter();
    $("#btnSearchFilter").button();
}

//Set change actions to all filter menu options to filter while working
$(function () {
    $("#SearchManufacturer").change(function () { $("#btnSearchFilter").click(); });
    $("#SearchModels").change(function () { $("#btnSearchFilter").click(); });
    $("#SearchYear").change(function () { $("#btnSearchFilter").click(); });
    $("#Manual").change(function () { $("#btnSearchFilter").click(); });
    $("#Auto").change(function () { $("#btnSearchFilter").click(); });
});

//Set two options to the filter menu open/close to not interfere in the search
$(function () {
    $("#OpenFilterMenu").click(function () {
        $("#FilterMenu").show('slide', { direction: 'left' }, 1000);

        $(this).fadeOut(1000);
        $("#CloseFilterMenu").fadeIn(1000);
    });

    $("#CloseFilterMenu").click(function () {
        $("#FilterMenu").hide('slide', { direction: 'left' }, 1000);

        $(this).fadeOut(1000);
        $("#OpenFilterMenu").fadeIn(1000);
    });
});

//Set jquery ui slider of prices to show on the filter menu 
function PriceSlider(MaxVal, MinVla) {
    $("#PriceBetweenSlider").slider({
        range: true,
        min: MinVla,
        max: MaxVal,
        values: [MinVla, MaxVal],
        step: 25,
        slide: function (event, ui) {
            $("#lblPriceBetween").html("$" + ui.values[0] + " - $" + ui.values[1]);
            $("#btnSearchFilter").click();
        }
    });
    $("#lblPriceBetween").html("$" + MinVla + " - $" + MaxVal);
}

//The actual data filtering - click on the "Filter" button or fake click
function setBtnFilter() {
    $("#btnSearchFilter").click(function () {

        Filteringdata = dataToFilter.slice(0);              //Duplicate the data to not ruin the original

        if ($("#SearchManufacturer").val() != 'All') {      //Remove the data that not fit to the filtering data - Manufacturer
            for (var i = 0; i < Filteringdata.length; i++) {
                if (Filteringdata[i]['Manufacturer'] != $("#SearchManufacturer").val()) {
                    Filteringdata.splice(i, 1);
                    i--;
                }
            }
        }

        if ($("#SearchModels").val() != 'All') {            //Remove the data that not fit to the filtering data - Models 
            for (var i = 0; i < Filteringdata.length; i++) {
                if (Filteringdata[i]['Model'] != $("#SearchModels").val()) {
                    Filteringdata.splice(i, 1);
                    i--;
                }
            }
        }

        if ($("#SearchYear").val() != 'All') {              //Remove the data that not fit to the filtering data - Year
            for (var i = 0; i < Filteringdata.length; i++) {
                if (Filteringdata[i]['ManufactureYear'] != $("#SearchYear").val()) {
                    Filteringdata.splice(i, 1);
                    i--;
                }
            }
        }

        for (var i = 0; i < Filteringdata.length; i++) {    //Remove the data that not fit to the filtering data - Transmission
            if (Filteringdata[i]['Transmission'] == "Auto") {
                if (!$("#Auto").is(':checked')) {
                    Filteringdata.splice(i, 1);
                    i--;
                }
            }
            else if (Filteringdata[i]['Transmission'] == "Manual") {
                if (!$("#Manual").is(':checked')) {
                    Filteringdata.splice(i, 1);
                    i--;
                }
            }
        }

        for (var i = 0; i < Filteringdata.length; i++) {    //Remove the data that not fit to the filtering data - Rent Cost
            if (Filteringdata[i]['DailyRentCost'] < $("#PriceBetweenSlider").slider("values", 0) ||
                Filteringdata[i]['DailyRentCost'] > $("#PriceBetweenSlider").slider("values", 1)) {
                Filteringdata.splice(i, 1);
                i--;
            }
        }

        if ($("#txtFreeText").val() != '') {                //Remove the data that not fit to the filtering data - Free text
            for (var i = 0; i < Filteringdata.length; i++) {
                if ($("#txtFreeText").val().indexOf(Filteringdata[i]['Manufacturer']) == -1 &&
                    $("#txtFreeText").val().indexOf(Filteringdata[i]['Model']) == -1 &&
                    $("#txtFreeText").val().indexOf(Filteringdata[i]['ManufactureYear']) == -1 &&
                    $("#txtFreeText").val().indexOf(Filteringdata[i]['Transmission']) == -1 &&
                    $("#txtFreeText").val().indexOf(Filteringdata[i]['DailyRentCost']) == -1) {
                    Filteringdata.splice(i, 1);
                    i--;
                }
            }
        }

        FillSearchResultsDiv(Filteringdata, false);        //Call function that display the new data after the filtering
    });
}