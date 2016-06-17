//Set actions for the search text box to search while the employee type
$(function () {
    $("#txtCarNumber").keydown(function () {
        FilterNumbers();
    });
    $("#txtCarNumber").keyup(function () {
        FilterNumbers();
    });
    $("#txtCarNumber").keypress(function () {
        FilterNumbers();
    });
    $("#txtCarNumber").change(function () {
        FilterNumbers();
    });
});

//Do the search of the car number the employee want to return
function FilterNumbers() {
    $(".CarNumber").each(function () {
        var numberToSearch = $("#txtCarNumber").val().replace("-", "").replace("-", "");
        var thisNumber = $(this).html().replace("-", "").replace("-", "");
        if (thisNumber.indexOf(numberToSearch) == -1) {
            $(this).parent("tr").hide();
        }
        else {
            $(this).parent("tr").show();
        }
    });
}

//Set dialog to the employee to be sure he want to return this car
$(function () {
    $("#dialog").dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        show: {
            effect: "scale",
            duration: 500
        },
        hide: {
            effect: "scale",
            duration: 500
        },
        buttons: {

            "Yes": function () {
                location.href = $(location).attr('origin') + "/Employee/ReturnVehicle/Edit/" + $("#rentalID").html();

            },
            "No": function () {
                $(this).dialog("close");
            }
        }
    });

    $(".btnEditVehicle").click(function () {
        $("#rentalID").html($(this).attr("id"));
        $("#dialog").dialog("open");
    });
    $(".btnReturnVehicle").click(function () {
        $("#rentalID").html($(this).attr("id"));
        location.href = $(location).attr('origin') + "/Employee/ReturnVehicle/ReturnVehicle?rentalID=" + $("#rentalID").html();
    })
});