//Get all vehicles types to edit/create vehicle type page
$(function () {
    //Get by ajax list of all vehicles types
    $.ajax({
        type: "GET",
        url: '/Manage/VehiclesTypes/VehiclesTypesList',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $('<option value=' + data[i]['VehicleTypeID'] + '>' +
                  data[i]['DetailsString'] + '</option>').appendTo('#VehicleTypeList');
            }
        },
        error: function (err) {
            openErrorsDialog(err.statusText, false);
        },
        cache: false
    });
    //Get by ajax list of all branches
    $.ajax({
        type: "GET",
        url: '/Manage/Branches/BranchList',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $('<option value=' + data[i]['BranchID'] + '>' +
                  data[i]['DetailsString'] + '</option>').appendTo('#BranchList');
            }
        },
        error: function (err) {
            openErrorsDialog(err.statusText, false);
        },
        cache: false
    });
   
});

//Set the current vehicle type for edit page
function selectVehiclesTypesToEdit(valueToEdit) {
    $("#BranchList").children("option[value=" + valueToEdit + "]").attr("selected", "selected");
}
